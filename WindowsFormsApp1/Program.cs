using System;
using Business_Logical;
using DataAccessLayer;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                Application.Run(new Form1(new Logic(RepositoryFactory.Create())));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось подключиться к базе. Проверьте SQL Server Express LocalDB и строку MarketplaceDb.\n" +
                    ex.GetBaseException().Message, "Ошибка подключения");
            }
        }
    }
}
