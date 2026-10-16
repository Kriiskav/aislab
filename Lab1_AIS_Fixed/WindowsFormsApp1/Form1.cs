using System;
using System.Windows.Forms;
using Model;

namespace WindowsFormsApp1
{
    /// <summary>
    /// Форма магазина гномиков. Операции с товарами выполняет модель.
    /// </summary>
    public partial class Form1 : Form
    {
        private Logic logic = new Logic();
        private string selectedName;

        /// <summary>
        /// Создать форму и выбрать первый тип товара.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            typeComboBox.SelectedIndex = 0;
        }

        /// <summary>
        /// Проверить числовые поля формы.
        /// </summary>
        private bool ReadNumbers(out decimal price, out int stock)
        {
            price = 0;
            stock = 0;
            if (!decimal.TryParse(priceTextBox.Text, out price) ||
                !int.TryParse(stockTextBox.Text, out stock))
            {
                MessageBox.Show("Введите цену числом и количество целым числом.");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Добавить товар из полей ввода.
        /// </summary>
        private void addButton_Click(object sender, EventArgs e)
        {
            decimal price;
            int stock;
            if (!ReadNumbers(out price, out stock)) return;
            try
            {
                logic.AddProduct(nameTextBox.Text, typeComboBox.Text, sellerTextBox.Text, price, stock);
                RefreshList();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Показать товары по типам.
        /// </summary>
        private void groupButton_Click(object sender, EventArgs e)
        {
            string result = logic.GroupByType();
            MessageBox.Show(result == "" ? "Товаров пока нет." : result, "Группировка");
        }

        /// <summary>
        /// Купить одну единицу выбранного товара.
        /// </summary>
        private void buyButton_Click(object sender, EventArgs e)
        {
            if (!CheckSelection()) return;
            MessageBox.Show(logic.BuyProduct(selectedName));
            RefreshList();
        }

        /// <summary>
        /// Изменить выбранный товар, включая название.
        /// </summary>
        private void updateButton_Click(object sender, EventArgs e)
        {
            if (!CheckSelection()) return;
            decimal price;
            int stock;
            if (!ReadNumbers(out price, out stock)) return;
            try
            {
                logic.UpdateProduct(selectedName, nameTextBox.Text, typeComboBox.Text,
                    sellerTextBox.Text, price, stock);
                RefreshList();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Применить скидку к выбранному товару.
        /// </summary>
        private void discountButton_Click(object sender, EventArgs e)
        {
            if (!CheckSelection()) return;
            string input = Microsoft.VisualBasic.Interaction.InputBox(
                "Введите скидку от 1 до 99 процентов:", "Скидка", "10");
            if (input == "") return;
            int percent;
            if (!int.TryParse(input, out percent))
            {
                MessageBox.Show("Введите целое число.");
                return;
            }
            MessageBox.Show(logic.ApplyDiscount(selectedName, percent));
            RefreshList();
        }

        /// <summary>
        /// Удалить выбранный товар.
        /// </summary>
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (!CheckSelection()) return;
            logic.DeleteProduct(selectedName);
            RefreshList();
        }

        /// <summary>
        /// Показать все данные товаров.
        /// </summary>
        private void buttonShowAll_Click(object sender, EventArgs e)
        {
            string result = logic.GetAll();
            MessageBox.Show(result == "" ? "Товаров пока нет." : result, "Все товары");
        }

        /// <summary>
        /// Заполнить поля данными выбранного товара.
        /// </summary>
        private void productsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedName = productsListBox.SelectedItem as string;
            if (selectedName == null) return;
            Product product = logic.FindByName(selectedName);
            if (product == null) return;
            nameTextBox.Text = product.Name;
            typeComboBox.Text = product.Type;
            sellerTextBox.Text = product.SellerGnome;
            priceTextBox.Text = product.Price.ToString();
            stockTextBox.Text = product.Stock.ToString();
        }

        /// <summary>
        /// Проверить, выбран ли товар для операции.
        /// </summary>
        private bool CheckSelection()
        {
            if (selectedName != null) return true;
            MessageBox.Show("Сначала выберите товар в списке.");
            return false;
        }

        /// <summary>
        /// Обновить список и очистить поля после операции.
        /// </summary>
        private void RefreshList()
        {
            selectedName = null;
            productsListBox.Items.Clear();
            productsListBox.Items.AddRange(logic.GetNames().ToArray());
            nameTextBox.Clear();
            sellerTextBox.Clear();
            priceTextBox.Clear();
            stockTextBox.Clear();
            typeComboBox.SelectedIndex = 0;
        }
    }
}

