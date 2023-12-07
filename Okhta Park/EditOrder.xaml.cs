using Okhta_Park.bd;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
    /// Логика взаимодействия для EditOrder.xaml
    /// </summary>
    public partial class EditOrder : Window
    {
        bd.okhta_parkEntities db;
        public EditOrder(bd.okhta_parkEntities db, bd.Orders thisorder)
        {
            InitializeComponent();
            this.db = db;
            this.DataContext = thisorder;
            identity = thisorder.ID;
            BaseClient.ItemsSource = App.parkentities.Clients.Select(x => x.FIO).ToList();
            FormatDate.Text = Convert.ToString(thisorder.DateСreation);
            OrderDateClos.Text = Convert.ToString(thisorder.DateClosing);
            List<Orders> products = App.parkentities.Orders.Where(a => a.ID == identity).ToList();
            
        }
        int identity;
        private void ActionEditClick(object sender, RoutedEventArgs e)//Редактирование заказа
        {
            try
            {
                int ClientsCode = App.parkentities.Clients.Where(c => c.FIO == BaseClient.Text).Select(c => c.CodeClient).FirstOrDefault();
                OrderCode.Text = ClientsCode + "/" + FormatDate.Text;
                List<Orders> products = App.parkentities.Orders.Where(a => a.ID == identity).ToList();
                try
                {
                    products[0].CodeOrder = OrderCode.Text;
                    products[0].DateСreation = Convert.ToDateTime(FormatDate.Text);
                    products[0].TimeOrder = OrderTime.Text;
                    products[0].id_CodeClient = ClientsCode;
                    products[0].Status = BaseStatus.Text;
                    products[0].DateClosing = Convert.ToDateTime(OrderDateClos.Text);
                    products[0].TimeRental = OrderTimetxt.Text;
                }
                catch { }
                App.parkentities.Orders.AddOrUpdate();
                App.parkentities.SaveChanges();
                MessageBox.Show("Данные изменены");
                ShiftSupervisor showSupervisor = new ShiftSupervisor();
                showSupervisor.Show();
                this.Close();
            }
            catch { }
        }

        private void ActionDeleteClick(object sender, RoutedEventArgs e)//Удаление заказа
        {
            try
            {
                bd.okhta_parkEntities parkEntities = new bd.okhta_parkEntities();
                if (identity == null)
                {
                    MessageBox.Show("Не выбрана строка для удаления!");
                }
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить заказ?", 
                    "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    App.parkentities.Orders.Remove(App.parkentities.Orders.Find(identity));
                    App.parkentities.ServicesOrders.Remove(App.parkentities.ServicesOrders.Find(identity));
                    App.parkentities.SaveChanges();
                    ShiftSupervisor showSupervisor = new ShiftSupervisor();
                    showSupervisor.Show();
                    this.Close();
                }
            }
            catch { }
        }

        private void ActionOutClick(object sender, RoutedEventArgs e)//Выход
        {
            ShiftSupervisor showSupervisor = new ShiftSupervisor();
            showSupervisor.Show();
            this.Close();
        }
    }
}
