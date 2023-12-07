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
    /// Логика взаимодействия для Administrator.xaml
    /// </summary>
    public partial class Administrator : Window
    {
        public Administrator()
        {
            InitializeComponent();
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }
        bd.okhta_parkEntities db = new bd.okhta_parkEntities();
        System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
        int stop=300;
        private void timerTick(object sender, EventArgs e)//таймер
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

        private void LoginHistoryClick(object sender, RoutedEventArgs e)//блок Вход в систему
        {
            BlockLogInSystem.Visibility = Visibility.Visible;
            BlockServices.Visibility = Visibility.Hidden;
            BlockClient.Visibility = Visibility.Hidden;
            BlockEmployee.Visibility = Visibility.Hidden;
            EditingСlients.Visibility = Visibility.Hidden;
            EditingConsumables.Visibility = Visibility.Hidden;
            EditingEmployee.Visibility = Visibility.Hidden;
            GetOut.Visibility = Visibility.Visible;

            BaseLogInSystem.ItemsSource = App.parkentities.LogInSystem.ToList();
        }

        private void ConsumablesClick(object sender, RoutedEventArgs e)//блок Расходные материалы
        {
            BlockServices.Visibility = Visibility.Visible;
            BlockLogInSystem.Visibility = Visibility.Hidden;
            BlockClient.Visibility = Visibility.Hidden;
            BlockEmployee.Visibility = Visibility.Hidden;
            EditingСlients.Visibility = Visibility.Hidden;
            EditingConsumables.Visibility = Visibility.Visible;
            EditingEmployee.Visibility = Visibility.Hidden;
            GetOut.Visibility = Visibility.Hidden;

            BaseServices.ItemsSource = App.parkentities.Services.ToList();
        }

        private void ViewingСlientsClick(object sender, RoutedEventArgs e)//блок Клиенты
        {
            BlockClient.Visibility = Visibility.Visible;
            BlockLogInSystem.Visibility = Visibility.Hidden;
            BlockServices.Visibility = Visibility.Hidden;
            BlockEmployee.Visibility = Visibility.Hidden;
            EditingСlients.Visibility = Visibility.Visible;
            EditingConsumables.Visibility = Visibility.Hidden;
            EditingEmployee.Visibility = Visibility.Hidden;
            GetOut.Visibility = Visibility.Hidden;

            BaseClient.ItemsSource = App.parkentities.Clients.ToList();
        }

        private void ViewingEmployeeClick(object sender, RoutedEventArgs e)//блок Сотрудники
        {
            BlockEmployee.Visibility = Visibility.Visible;
            BlockClient.Visibility = Visibility.Hidden;
            BlockLogInSystem.Visibility = Visibility.Hidden;
            BlockServices.Visibility = Visibility.Hidden;
            EditingEmployee.Visibility = Visibility.Visible;
            EditingСlients.Visibility = Visibility.Hidden;
            EditingConsumables.Visibility = Visibility.Hidden;
            GetOut.Visibility = Visibility.Hidden;

            BaseEmployee.ItemsSource = App.parkentities.Employee.ToList();
        }
        private void OutClick(object sender, RoutedEventArgs e)//Кнопка Выход 
        {
            MainWindow showAutho = new MainWindow();
            showAutho.Show();
            timer.Stop();
            this.Close();
        }

        private void AddConsumablesClick(object sender, RoutedEventArgs e)//Добавление услуг
        {
            AddConsumables showConsumables = new AddConsumables();
            showConsumables.Show();
            timer.Stop();
            this.Close();
        }

        private void EditConsumablesClick(object sender, RoutedEventArgs e)//Редактирование услуг
        {
            var row = BaseServices.SelectedItem as bd.Services;
            if (row != null)
            {
                EditConsumables showConsumables = new EditConsumables(App.parkentities, row);
                showConsumables.Show();
                showConsumables.ActionEdit.Visibility = Visibility.Visible;
                showConsumables.ActionDelete.Visibility = Visibility.Hidden;
                timer.Stop();
                this.Close();
            }
            else
            {
                MessageBox.Show("Вы не выбрали строку для изменения.");
            }
        }

        private void DeleteConsumablesClick(object sender, RoutedEventArgs e)//Удаление услуг
        {
            var row = BaseServices.SelectedItem as bd.Services;
            if (row != null)
            {
                EditConsumables showConsumables = new EditConsumables(App.parkentities, row);
                showConsumables.Show();
                showConsumables.ActionEdit.Visibility = Visibility.Hidden;
                showConsumables.ActionDelete.Visibility = Visibility.Visible;
                timer.Stop();
                this.Close();
            }
            else
            {
                MessageBox.Show("Вы не выбрали строку для удаления.");
            }
        }

        private void AddClientsClick(object sender, RoutedEventArgs e)//Добавление клиента
        {
            AddClient showClient = new AddClient();
            showClient.Show();
            timer.Stop();
            this.Close();
        }

        private void EditClientsClick(object sender, RoutedEventArgs e)//Редактирование клиента
        {
            var row = BaseClient.SelectedItem as bd.Clients;
            if (row != null)
            {
                EditClient showClient = new EditClient(App.parkentities, row);
                showClient.Show();
                showClient.ActionDelete.Visibility = Visibility.Hidden;
                showClient.ActionEdit.Visibility = Visibility.Visible;
                timer.Stop();
                this.Close();
            }
            else
            {
                MessageBox.Show("Вы не выбрали строку для изменения.");
            }
        }

        private void DeleteClientsClick(object sender, RoutedEventArgs e)//Удаление клиента
        {
            var row = BaseClient.SelectedItem as bd.Clients;
            if (row != null)
            {
                EditClient showClient = new EditClient(App.parkentities, row);
                showClient.Show();
                showClient.ActionDelete.Visibility = Visibility.Visible;
                showClient.ActionEdit.Visibility = Visibility.Hidden;
                timer.Stop();
                this.Close();
            }
            else
            {
                MessageBox.Show("Вы не выбрали строку для удаления.");
            }
        }

        private void AddEmployeesClick(object sender, RoutedEventArgs e)//Добавление сотрудника
        {
            var emplo = new bd.Employee();
            AddEmployee showEmployee = new AddEmployee(App.parkentities,emplo,true);
            showEmployee.Show();
            timer.Stop();
            this.Close();
        }

        private void EditEmployeesClick(object sender, RoutedEventArgs e)//Редактирование сотрудника
        {
            var row = BaseEmployee.SelectedItem as bd.Employee;
            if (row != null)
            {
                EditEmployee showEmployee = new EditEmployee(row, false);
                showEmployee.Show();
                showEmployee.ActionDelete.Visibility = Visibility.Hidden;
                showEmployee.ActionEdit.Visibility = Visibility.Visible;
                timer.Stop();
                this.Close();
            }
            else
            {
                MessageBox.Show("Вы не выбрали строку для изменения.");
            }
        }

        private void DeleteEmployeesClick(object sender, RoutedEventArgs e)//Удаление сотрудника 
        {
            var row = BaseEmployee.SelectedItem as bd.Employee;
            if (row != null)
            {
                EditEmployee showEmployee = new EditEmployee(row, false);
                showEmployee.Show();
                showEmployee.ActionDelete.Visibility = Visibility.Visible;
                showEmployee.ActionEdit.Visibility = Visibility.Hidden;
                timer.Stop();
                this.Close();
            }
            else
            {
                MessageBox.Show("Вы не выбрали строку для удаления.");
            }
        }
    }
}
