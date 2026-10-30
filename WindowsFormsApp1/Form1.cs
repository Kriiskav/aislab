using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Model;
using Business_Logical;
using DataAccessLayer;

namespace WindowsFormsApp1
{
    /// <summary>Форма магазина.</summary>
    public partial class Form1 : Form
    {
        private Logic logic;
        private string selectedName;
        private bool grouped;
        private List<Product> shownProducts = new List<Product>();

        /// <summary>Создать форму.</summary>
        public Form1()
        {
            InitializeComponent();
            typeComboBox.SelectedIndex = 0;
        }

        /// <summary>Подключить логику.</summary>
        public Form1(Logic logic) : this()
        {
            this.logic = logic;
            RefreshList();
        }

        /// <summary>Проверить числовые поля формы.</summary>
        private bool ReadNumbers(out decimal price, out int stock)
        {
            price = 0;
            stock = 0;
            if (!InputParser.TryReadPrice(priceTextBox.Text, out price) ||
                !int.TryParse(stockTextBox.Text, out stock))
            {
                statusLabel.Text = "Введите цену (12,50 или 12.50) и целое количество.";
                return false;
            }
            return true;
        }

        /// <summary>Добавить товар из полей ввода.</summary>
        private void addButton_Click(object sender, EventArgs e)
        {
            decimal price;
            int stock;
            if (!ReadNumbers(out price, out stock)) return;
            try
            {
                logic.AddProduct(nameTextBox.Text, typeComboBox.Text, sellerTextBox.Text, price, stock);
                RefreshList();
                statusLabel.Text = "Готово. Выберите товар для следующей операции.";
            }
            catch (ArgumentException ex)
            {
                statusLabel.Text = ex.Message;
            }
        }

        /// <summary>Показать товары по типам.</summary>
        private void groupButton_Click(object sender, EventArgs e)
        {
            grouped = true;
            RefreshList();
        }

        /// <summary>Купить одну единицу выбранного товара.</summary>
        private void buyButton_Click(object sender, EventArgs e)
        {
            if (!CheckSelection()) return;
            string result = logic.BuyProduct(selectedName);
            RefreshList();
            statusLabel.Text = result;
        }

        /// <summary>Изменить товар.</summary>
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
                statusLabel.Text = "Готово. Выберите товар для следующей операции.";
            }
            catch (ArgumentException ex)
            {
                statusLabel.Text = ex.Message;
            }
        }

        /// <summary>Применить скидку к выбранному товару.</summary>
        private void discountButton_Click(object sender, EventArgs e)
        {
            if (!CheckSelection()) return;
            string input = Microsoft.VisualBasic.Interaction.InputBox(
                "Введите скидку от 1 до 99 процентов:", "Скидка", "10");
            if (input == "") return;
            int percent;
            if (!int.TryParse(input, out percent))
            {
                statusLabel.Text = "Введите целое число.";
                return;
            }
            string result = logic.ApplyDiscount(selectedName, percent);
            RefreshList();
            statusLabel.Text = result;
        }

        /// <summary>Удалить выбранный товар.</summary>
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (!CheckSelection()) return;
            logic.DeleteProduct(selectedName);
            RefreshList();
        }

        /// <summary>Показать все данные товаров.</summary>
        private void buttonShowAll_Click(object sender, EventArgs e)
        {
            grouped = false;
            RefreshList();
        }

        /// <summary>Показать выбранный товар.</summary>
        private void productsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = productsListBox.SelectedIndex;
            selectedName = index >= 0 ? shownProducts[index].Name : null;
            if (selectedName == null) return;
            Product product = logic.FindByName(selectedName);
            if (product == null) return;
            nameTextBox.Text = product.Name;
            typeComboBox.Text = product.Type;
            sellerTextBox.Text = product.SellerGnome;
            priceTextBox.Text = product.Price.ToString();
            stockTextBox.Text = product.Stock.ToString();
        }

        /// <summary>Проверить выбор.</summary>
        private bool CheckSelection()
        {
            if (selectedName != null) return true;
            statusLabel.Text = "Сначала выберите товар в списке.";
            return false;
        }

        /// <summary>Обновить список.</summary>
        private void RefreshList()
        {
            selectedName = null;
            productsListBox.Items.Clear();
            shownProducts = logic.GetProducts();
            statusLabel.Text = "Товаров: " + shownProducts.Count + (grouped ? ". Группировка по типу." : ". Выберите товар для операции.");
            if (grouped) shownProducts = shownProducts.OrderBy(p => p.Type).ThenBy(p => p.Name).ToList();
            foreach (Product product in shownProducts)
                productsListBox.Items.Add(product.Name + " | " + product.Type +
                    " | гномик: " + product.SellerGnome + " | цена: " +
                    product.Price + " | остаток: " + product.Stock);
            nameTextBox.Clear();
            sellerTextBox.Clear();
            priceTextBox.Clear();
            stockTextBox.Clear();
            typeComboBox.SelectedIndex = 0;
        }
    }
}
