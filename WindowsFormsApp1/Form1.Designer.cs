namespace WindowsFormsApp1
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.ComboBox typeComboBox;
        private System.Windows.Forms.TextBox sellerTextBox;
        private System.Windows.Forms.TextBox priceTextBox;
        private System.Windows.Forms.TextBox stockTextBox;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button groupButton;
        private System.Windows.Forms.Button buyButton;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.Button discountButton;
        private System.Windows.Forms.ListBox productsListBox;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonShowAll;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.typeComboBox = new System.Windows.Forms.ComboBox();
            this.sellerTextBox = new System.Windows.Forms.TextBox();
            this.priceTextBox = new System.Windows.Forms.TextBox();
            this.stockTextBox = new System.Windows.Forms.TextBox();
            this.addButton = new System.Windows.Forms.Button();
            this.groupButton = new System.Windows.Forms.Button();
            this.buyButton = new System.Windows.Forms.Button();
            this.updateButton = new System.Windows.Forms.Button();
            this.discountButton = new System.Windows.Forms.Button();
            this.productsListBox = new System.Windows.Forms.ListBox();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonShowAll = new System.Windows.Forms.Button();
            this.SuspendLayout();

            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 20);
            this.label1.Text = "Название";

            this.nameTextBox.Location = new System.Drawing.Point(150, 17);
            this.nameTextBox.Size = new System.Drawing.Size(180, 20);

            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 50);
            this.label2.Text = "Тип";

            this.typeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeComboBox.Location = new System.Drawing.Point(150, 47);
            this.typeComboBox.Size = new System.Drawing.Size(180, 20);
            this.typeComboBox.Items.AddRange(new object[] {
                "Лечебная травка",
                "Волшебный порошочек",
                "Отвары"});

            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 80);
            this.label3.Text = "Гномик";

            this.sellerTextBox.Location = new System.Drawing.Point(150, 77);
            this.sellerTextBox.Size = new System.Drawing.Size(180, 20);

            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 110);
            this.label4.Text = "Цена";

            this.priceTextBox.Location = new System.Drawing.Point(150, 107);
            this.priceTextBox.Size = new System.Drawing.Size(180, 20);

            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 140);
            this.label5.Text = "Количество";

            this.stockTextBox.Location = new System.Drawing.Point(150, 137);
            this.stockTextBox.Size = new System.Drawing.Size(180, 20);

            this.addButton.Location = new System.Drawing.Point(20, 170);
            this.addButton.Size = new System.Drawing.Size(90, 30);
            this.addButton.Text = "Добавить";
            this.addButton.Click += new System.EventHandler(this.addButton_Click);

            this.groupButton.Location = new System.Drawing.Point(120, 170);
            this.groupButton.Size = new System.Drawing.Size(90, 30);
            this.groupButton.Text = "Группировка";
            this.groupButton.Click += new System.EventHandler(this.groupButton_Click);

            this.buyButton.Location = new System.Drawing.Point(220, 170);
            this.buyButton.Size = new System.Drawing.Size(90, 30);
            this.buyButton.Text = "Купить";
            this.buyButton.Click += new System.EventHandler(this.buyButton_Click);

            this.updateButton.Location = new System.Drawing.Point(320, 170);
            this.updateButton.Size = new System.Drawing.Size(90, 30);
            this.updateButton.Text = "Изменить";
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);

            this.discountButton.Location = new System.Drawing.Point(420, 170);
            this.discountButton.Size = new System.Drawing.Size(90, 30);
            this.discountButton.Text = "Скидка";
            this.discountButton.Click += new System.EventHandler(this.discountButton_Click);

            this.productsListBox.Location = new System.Drawing.Point(20, 250);
            this.productsListBox.Size = new System.Drawing.Size(500, 200);

            this.ClientSize = new System.Drawing.Size(550, 480);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.typeComboBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.sellerTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.priceTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.stockTextBox);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.groupButton);
            this.Controls.Add(this.buyButton);
            this.Controls.Add(this.updateButton);
            this.Controls.Add(this.discountButton);
            this.Controls.Add(this.productsListBox);
            this.buttonDelete.Location = new System.Drawing.Point(20, 210);
            this.buttonDelete.Size = new System.Drawing.Size(90, 30);
            this.buttonDelete.Text = "Удалить";
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            this.buttonShowAll.Location = new System.Drawing.Point(120, 210);
            this.buttonShowAll.Size = new System.Drawing.Size(140, 30);
            this.buttonShowAll.Text = "Показать все";
            this.buttonShowAll.Click += new System.EventHandler(this.buttonShowAll_Click);
            this.productsListBox.SelectedIndexChanged += new System.EventHandler(this.productsListBox_SelectedIndexChanged);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonShowAll);
            this.Text = "Маркетплейс гномиков — выберите товар в списке";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}


