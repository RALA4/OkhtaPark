using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Okhta_Park
{
    /// <summary>
    /// Логика взаимодействия для AddConsumables.xaml
    /// </summary>
    public partial class AddConsumables : Window
    {
        public AddConsumables()
        {
            InitializeComponent();
        }

        private void ActionAddClick(object sender, RoutedEventArgs e)//Добавить Услугу
        {
            try
            {
                bd.okhta_parkEntities db = new bd.okhta_parkEntities();
                bd.Services addService = new bd.Services
                {
                    ID = Convert.ToInt32(ForId.Text),
                    NameService = ForName.Text,
                    CodeService = ForCode.Text,
                    CostHour = Convert.ToDecimal(ForMoney.Text)
                };
                db.Services.Add(addService);
                db.SaveChanges();
                MessageBox.Show("Данные успешно добавлены!");
                Administrator showAdministartor = new Administrator();
                showAdministartor.Show();
                this.Close();
            }
            catch
            {
                MessageBox.Show("Не все данные заполнены или имееют неверный формат!");
            }
        }

        private void ActionOutClick(object sender, RoutedEventArgs e)//Выход
        {
            Administrator showAdministartor = new Administrator();
            showAdministartor.Show();
            this.Close();
        }

        private void FormatPreviewKeyDown(object sender, KeyEventArgs e)//Запрет на ввод букв 
        {
            if (e.Key == Key.NumPad0 || e.Key == Key.NumPad1 || e.Key == Key.NumPad2 || e.Key == Key.NumPad3 || e.Key == Key.NumPad4 || e.Key == Key.NumPad5 ||
                e.Key == Key.NumPad6 || e.Key == Key.NumPad7 || e.Key == Key.NumPad8 || e.Key == Key.NumPad9 || e.Key == Key.Back)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
