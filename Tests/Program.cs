using System;
using System.Linq;
using System.Configuration;
using System.Data.SqlClient;
using System.Reflection;
using System.Windows.Forms;
using Business_Logical;
using DataAccessLayer;
using Model;

class Program
{
    static int count;
    static void Check(bool v, string name) { if(!v) throw new Exception(name); Console.WriteLine("PASS " + name); count++; }
    static void Reject(Action a, string name) { bool v=false; try { a(); } catch(ArgumentException) { v=true; } Check(v,name); }
    [STAThread]
    static void Main(string[] args)
    {
        decimal price;
        foreach(string culture in new[]{"ru-RU","en-US"})
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(culture);
            Check(InputParser.TryReadPrice("12,50",out price) && price==12.50m, culture+" comma");
            Check(InputParser.TryReadPrice("12.50",out price) && price==12.50m, culture+" dot");
            foreach(string bad in new[]{"","abc","1,2.3","1 234","1,000,000"})
                Check(!InputParser.TryReadPrice(bad,out price),culture+" invalid "+bad);
        }
        var context = new MarketplaceContext();
        context.Database.Initialize(false);
        var ef = new EFRepository<Product>(context);
        var dapper = new DapperRepository<Product>(ConfigurationManager.ConnectionStrings["MarketplaceDb"].ConnectionString);
        IRepository<Product> repo = args[0]=="EF" ? (IRepository<Product>)ef : dapper;
        var other = args[0]=="EF" ? (IRepository<Product>)dapper : ef;
        var logic = new Logic(repo);
        string name = "Test-" + Guid.NewGuid().ToString("N");
        logic.AddProduct(name,"Отвары","Гном",100,2);
        var p = logic.FindByName(name);
        Check(p.Id>0,"generated ID");
        Check(other.ReadById(p.Id).Stock==2,"cross-provider read");
        Reject(()=>logic.AddProduct(name,"Отвары","Гном",1,1),"duplicate rejected");
        Reject(()=>logic.AddProduct("","Отвары","Гном",1,1),"empty name");
        Reject(()=>logic.AddProduct("x","Отвары","Гном",-1,1),"negative price");
        Reject(()=>logic.AddProduct("x","Отвары","Гном",1,-1),"negative stock");
        Check(logic.UpdateProduct(name,name,"Отвары","Гном",100,2),"same-name update");
        string renamed=name+"new";
        Check(logic.UpdateProduct(name,renamed,"Отвары","Гном",100,2),"rename");
        Check(logic.FindByName(renamed).Id==p.Id,"ID preserved");
        logic.ApplyDiscount(renamed,25);
        using(var fresh=new MarketplaceContext())
            Check(new EFRepository<Product>(fresh).ReadById(p.Id).Price==75,"discount saved");
        logic.ApplyDiscount(renamed,100);
        Check(logic.FindByName(renamed).Price==75,"invalid discount");
        logic.BuyProduct(renamed);
        Check(logic.FindByName(renamed).Stock==1,"buy one");
        logic.BuyProduct(renamed); logic.BuyProduct(renamed);
        Check(logic.FindByName(renamed).Stock==0,"stock zero");
        Check(logic.GroupByType().Contains(renamed),"group");
        Check(logic.DeleteProduct(renamed) && logic.FindByName(renamed)==null,"delete");

        var form = new WindowsFormsApp1.Form1(logic);
        var flags=BindingFlags.NonPublic|BindingFlags.Instance;
        Func<string,Control> field=n=>(Control)form.GetType().GetField(n,flags).GetValue(form);
        Action<string> click=n=>form.GetType().GetMethod(n,flags).Invoke(form,new object[]{null,EventArgs.Empty});
        var list=(ListBox)field("productsListBox");
        int initial=repo.ReadAll().Count();
        click("updateButton_Click");
        Check(field("statusLabel").Text.Contains("выберите"),"no selection hint");
        field("nameTextBox").Text=name;
        field("sellerTextBox").Text="Гном";
        field("priceTextBox").Text="wrong";
        field("stockTextBox").Text="4";
        click("addButton_Click");
        Check(repo.ReadAll().Count()==initial,"invalid form input no insertion");
        field("priceTextBox").Text="12,50";
        click("addButton_Click");
        Check(repo.ReadAll().Count()==initial+1,"form adds");
        Check(list.Items.Cast<string>().Any(x=>x.Contains(name)&&x.Contains("12")&&x.Contains("4")),"list shows details");
        list.SelectedIndex=list.Items.Count-1;
        Check(field("nameTextBox").Text==name && field("stockTextBox").Text=="4","selection fills fields");
        field("nameTextBox").Text=renamed;
        click("updateButton_Click");
        Check(logic.FindByName(renamed)!=null,"form renames");
        click("groupButton_Click");
        Check(list.Items.Count==initial+1,"group has only selectable products");
        list.SelectedIndex=list.Items.Cast<string>().ToList().FindIndex(x=>x.StartsWith(renamed+" |"));
        click("buyButton_Click");
        Check(logic.FindByName(renamed).Stock==3,"buy after grouping");
        Check(field("statusLabel").Text.Contains("Куплена"),"purchase feedback");
        list.SelectedIndex=list.Items.Cast<string>().ToList().FindIndex(x=>x.StartsWith(renamed+" |"));
        click("buttonDelete_Click");
        Check(logic.FindByName(renamed)==null,"form deletes");
        click("buttonShowAll_Click");
        Check(list.Items.Count==initial,"show all refresh");
        form.ShowInTaskbar=false;
        form.StartPosition=FormStartPosition.Manual;
        form.Location=new System.Drawing.Point(-2000,-2000);
        form.Show();
        Application.DoEvents();
        using(var bitmap=new System.Drawing.Bitmap(form.Width,form.Height)) { form.DrawToBitmap(bitmap,new System.Drawing.Rectangle(0,0,form.Width,form.Height)); bitmap.Save(args[0]+"-form.png"); }
        form.Dispose();
        context.Dispose();
        Console.WriteLine("TOTAL "+count+" "+args[0]);
    }
}
