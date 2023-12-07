using Okhta_Park.bd;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
    /// Логика взаимодействия для EditServicesOrder.xaml
    /// </summary>
    public partial class EditServicesOrder : Window
    {
        bd.ServicesOrders db;
        public EditServicesOrder(bd.ServicesOrders db, bool add)
        {
            InitializeComponent();
            this.db = db;
            this.DataContext = db;
            identity = db.idServiceOrder;
            this.add = add;
            BaseServices.ItemsSource = App.parkentities.Services.Select(x => x.NameService).ToList();
            List<ServicesOrders> products = App.parkentities.ServicesOrders.Where(a => a.idServiceOrder == identity).ToList();
        }
        int identity;
        bool save = false, add;
        private void ActionEditClick(object sender, RoutedEventArgs e)//Редактирование услуг в заказах
        {
            try
            {
                int identityOrder = App.parkentities.Orders.Where(c => c.CodeOrder == OrderCode.Text).Select(c => c.ID).FirstOrDefault();
                int identityService = App.parkentities.Services.Where(c => c.NameService == BaseServices.Text).Select(c => c.ID).FirstOrDefault();
                List<ServicesOrders> products = App.parkentities.ServicesOrders.Where(a => a.idServiceOrder == identity).ToList();
                try
                {
                    products[0].Id_Order = identityOrder;
                    products[0].Services = identityService;
                }
                catch { }
                App.parkentities.ServicesOrders.AddOrUpdate();
                App.parkentities.SaveChanges();
                MessageBox.Show("Данные изменены!");
                ShiftSupervisor showSupervisor = new ShiftSupervisor();
                showSupervisor.Show();
                this.Close();
            }
            catch { }
        }

        private void ActionDeleteClick(object sender, RoutedEventArgs e)//Удаление услуг в заказах
        {
            try
            {
                bd.okhta_parkEntities parkEntities = new bd.okhta_parkEntities();
                if (identity == null)
                {
                    MessageBox.Show("Не выбрана строка для удаления!");
                }
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить услугу из заказа?", 
                    "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
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
