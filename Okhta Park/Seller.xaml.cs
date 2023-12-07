using Okhta_Park.bd;
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
    /// Логика взаимодействия для Seller.xaml
    /// </summary>
    public partial class Seller : Window
    {
        public Seller()
        {
            InitializeComponent();
            BaseOrder.ItemsSource = App.parkentities.Orders.ToList();
            BaseOrderService.ItemsSource = App.parkentities.ServicesOrders.ToList();
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
        private void ActionAddClick(object sender, RoutedEventArgs e)//Добавление заказа
        {
            AddOrder plusOrder = new AddOrder();
            plusOrder.Show();
            timer.Stop();
            this.Close();
        }

        private void OpenOrderClick(object sender, RoutedEventArgs e)//Открыть блок заказов
        {
            BlockOrderFormation.Visibility = Visibility.Visible;
            BlockOrderService.Visibility = Visibility.Hidden;

            ActionAdd.Visibility = Visibility.Visible;
            ActionAddService.Visibility = Visibility.Hidden;
        }

        private void OpenServiceClick(object sender, RoutedEventArgs e)//Открыть блок услуг
        {
            BlockOrderService.Visibility = Visibility.Visible;
            BlockOrderFormation.Visibility = Visibility.Hidden;

            ActionAdd.Visibility = Visibility.Hidden;
            ActionAddService.Visibility = Visibility.Visible;
        }

        private void ActionAddServiceClick(object sender, RoutedEventArgs e)//Добавление услуги в заказ
        {
            AddServicesOrder showServicesOrder = new AddServicesOrder();
            showServicesOrder.Show();
            timer.Stop();
            this.Close();
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
