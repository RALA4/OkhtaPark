using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Логика взаимодействия для AddOrder2.xaml
    /// </summary>
    public partial class AddOrder2 : Window
    {
        public AddOrder2()
        {
            InitializeComponent();
            BaseClient.ItemsSource = App.parkentities.Clients.Select(x => x.FIO).ToList();
        }

        private void ActionAddClick(object sender, RoutedEventArgs e)//Добавление заказа
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
                ShiftSupervisor showSupervisor = new ShiftSupervisor();
                showSupervisor.Show();
                this.Close();
            }
            catch 
            {
                MessageBox.Show("Не все данные заполнены или имееют неверный формат!");
            }
        }

        private void ActionOutClick(object sender, RoutedEventArgs e)//Выход
        {
            ShiftSupervisor showSupervisor = new ShiftSupervisor();
            showSupervisor.Show();
            this.Close();
        }
    }
}
