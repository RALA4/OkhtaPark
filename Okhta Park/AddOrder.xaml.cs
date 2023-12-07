using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
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
    /// Логика взаимодействия для AddOrder.xaml
    /// </summary>
    public partial class AddOrder : Window
    {
        public AddOrder()
        {
            InitializeComponent();
            BaseClient.ItemsSource = App.parkentities.Clients.Select(x => x.FIO).ToList();
        }
        private void ActionAddClick(object sender, RoutedEventArgs e)//Доавление заказа
        {
            try
            {
                bd.okhta_parkEntities db = new bd.okhta_parkEntities();
                int ClientsCode = db.Clients.Where(x => x.FIO == BaseClient.Text).Select(c => c.CodeClient).FirstOrDefault();
                OrderCode.Text = ClientsCode + "/" + OrderDate.Text;
                bd.Orders addOrder = new bd.Orders
                {
                    CodeOrder = OrderCode.Text,
                    DateСreation = Convert.ToDateTime(OrderDate.Text),
                    TimeOrder = OrderTime.Text,
                    id_CodeClient = ClientsCode,
                    Status = BaseStatus.Text,
                    TimeRental = OrderTimetxt.Text

                };
                db.Orders.Add(addOrder);
                db.SaveChanges();
                MessageBox.Show("Данные успешно добавлены!");
                Seller showSeller = new Seller();
                showSeller.Show();
                this.Close();
            }
            catch 
            {
                MessageBox.Show("Не все данные заполнены или имееют неверный формат!");
            }
        }

        private void ActionOutClick(object sender, RoutedEventArgs e)//Выход
        {
            Seller showSeller = new Seller();
            showSeller.Show();
            this.Close();
        }
    }
}
