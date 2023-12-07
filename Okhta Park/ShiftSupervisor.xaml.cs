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
    /// Логика взаимодействия для ShiftSupervisor.xaml
    /// </summary>
    public partial class ShiftSupervisor : Window
    {
        public ShiftSupervisor()
        {
            InitializeComponent();
            BaseOrder.ItemsSource = App.parkentities.Orders.ToList();
            BaseServicesOrder.ItemsSource = App.parkentities.ServicesOrders.ToList();
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }

        System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
        int stop = 300;
        private void timerTick(object sender, EventArgs e)//Таймер
        {
            ActiveTime.Text = Convert.ToString(stop);

            if (stop == 0)
            {
                timer.Stop();
                stop = 300;
                ActiveTime.Foreground = Brushes.White;
                MainWindow mw = new MainWindow();
                mw.Show();
                this.Close();
            }
            else
            {
                if (stop == 5)
                {
                    ActiveTime.Foreground = Brushes.Red;
                }
                stop--;
            }
        }
        private void ActionDeleteClick(object sender, RoutedEventArgs e)//Удалить заказ
        {
            var row = BaseOrder.SelectedItem as bd.Orders;
            if(row != null)
            {
                EditOrder showEditOrder = new EditOrder(App.parkentities, row);
                showEditOrder.Show();
                showEditOrder.ActionEdit.Visibility = Visibility.Hidden;
                showEditOrder.ActionDelete.Visibility = Visibility.Visible;
                timer.Stop();
                this.Close();
            }
        }

        private void ActionAddClick(object sender, RoutedEventArgs e)//Создать заказ
        {
            AddOrder2 PlusOrder = new AddOrder2();
            PlusOrder.Show();
            timer.Stop();
            this.Close();
        }

        private void ActionEditClick(object sender, RoutedEventArgs e)//Редактировать заказ
        {
            var row = BaseOrder.SelectedItem as bd.Orders;
            if(row != null)
            {
                EditOrder showEditOrder = new EditOrder(App.parkentities, row);
                showEditOrder.Show();
                showEditOrder.ActionEdit.Visibility = Visibility.Visible;
                showEditOrder.ActionDelete.Visibility = Visibility.Hidden;
                timer.Stop();
                this.Close();
            }
        }

        private void ActionEditServicesOrderClick(object sender, RoutedEventArgs e)//Редактирование услуг в заказх
        {
            var row = BaseServicesOrder.SelectedItem as bd.ServicesOrders;
            if(row != null)
            {
                EditServicesOrder showEditServicesOrder = new EditServicesOrder(row, false);
                showEditServicesOrder.ActionEdit.Visibility = Visibility.Visible;
                showEditServicesOrder.ActionDelete.Visibility = Visibility.Hidden;
                showEditServicesOrder.Show();
                timer.Stop();
                this.Close();
            }
        }

        private void ActionAddServicesOrderClick(object sender, RoutedEventArgs e)//Добавление услуг в заказх
        {
            AddServicesOrder2 showAddServicesOrder = new AddServicesOrder2();
            showAddServicesOrder.Show();
            timer.Stop();
            this.Close();
        }

        private void ActionDeleteServicesOrderClick(object sender, RoutedEventArgs e)//Удаление услуг в заказх
        {
            var row = BaseServicesOrder.SelectedItem as bd.ServicesOrders;
            if (row != null)
            {
                EditServicesOrder showEditServicesOrder = new EditServicesOrder(row,false);
                showEditServicesOrder.ActionEdit.Visibility = Visibility.Hidden;
                showEditServicesOrder.ActionDelete.Visibility = Visibility.Visible;
                showEditServicesOrder.Show();
                timer.Stop();
                this.Close();
            }
        }

        private void OpenOrderClick(object sender, RoutedEventArgs e)//Открыть блок заказов
        {
            BlockOrderFormation.Visibility = Visibility.Visible;
            BlockServicesOrder.Visibility = Visibility.Hidden;

            ActionEdit.Visibility = Visibility.Visible;
            ActionAdd.Visibility = Visibility.Visible;
            ActionDelete.Visibility = Visibility.Visible;

            ActionEditServicesOrder.Visibility = Visibility.Hidden;
            ActionAddServicesOrder.Visibility = Visibility.Hidden;
            ActionDeleteServicesOrder.Visibility = Visibility.Hidden;
        }

        private void OpenServiceClick(object sender, RoutedEventArgs e)//Открыть блок услуг
        {
            BlockOrderFormation.Visibility = Visibility.Hidden;
            BlockServicesOrder.Visibility = Visibility.Visible;

            ActionEdit.Visibility = Visibility.Hidden;
            ActionAdd.Visibility = Visibility.Hidden;
            ActionDelete.Visibility = Visibility.Hidden;

            ActionEditServicesOrder.Visibility = Visibility.Visible;
            ActionAddServicesOrder.Visibility = Visibility.Visible;
            ActionDeleteServicesOrder.Visibility = Visibility.Visible;
        }

        private void OutClick(object sender, RoutedEventArgs e)//Выход
        {
            MainWindow showAuthor = new MainWindow();
            showAuthor.Show();
            timer.Stop();
            this.Close();
        }
    }
}
