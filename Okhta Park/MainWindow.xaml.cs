using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Okhta_Park
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoginUser.ItemsSource = App.parkentities.Employee.Select(x => x.FIO).ToList();//Вывод сотрудников
        }

        public class CheckClick : TextBox//Используется для Captcha
        {
            public int error { get; set; }
        }

        private void PasswordUserPreviewKeyDown(object sender, KeyEventArgs e)//Запрет на пробел в Логине и Пароле
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }
        int Attempts;
        DateTime now = DateTime.Now;
        private void AuthorizClick(object sender, RoutedEventArgs e)//Авторизация
        {

            try
            {
                var ForEmployees = App.parkentities.Employee.FirstOrDefault(x => x.FIO == LoginUser.Text && x.Password == PasswordUser.Password);//для входа
                int CodeEmployees = App.parkentities.Employee.Where(x => x.FIO == LoginUser.Text).Select(c => c.CodeEmployee).FirstOrDefault();//для таблицы Вход в систему
                if (LoginUser == null || PasswordUser == null)
                {
                    MessageBox.Show("Заполните данные!");
                }    
                else
                {
                    if(ForEmployees != null)
                    {
                        switch (ForEmployees.id_Post)
                        {
                            case 1://Администратор
                                Administrator showAdmin = new Administrator();
                                showAdmin.Show();
                                this.Close();
                                break;
                            case 2://Продавец
                                Seller showSeller = new Seller();
                                showSeller.Show();
                                this.Close();
                                break;
                            case 3://Старший смены
                                ShiftSupervisor showShiftSupervisor = new ShiftSupervisor();
                                showShiftSupervisor.Show();
                                this.Close();
                                break;
                        }

                        bd.okhta_parkEntities db = new bd.okhta_parkEntities();//Для сохранения данных в таблицу Вход в систему
                        bd.LogInSystem addLogInSystem = new bd.LogInSystem
                        {
                            id_CodeEmployee = CodeEmployees,
                            LastEntry = now.Date,
                            InputType = "Успешно!"
                        };
                        db.LogInSystem.Add(addLogInSystem);
                        db.SaveChanges();
                    }
                    else
                    {
                        Attempts++;
                        UsersFalse();
                        bd.okhta_parkEntities db = new bd.okhta_parkEntities();
                        bd.LogInSystem addLogInSystem = new bd.LogInSystem
                        {
                            id_CodeEmployee = CodeEmployees,
                            LastEntry = now.Date,
                            InputType = "Не удача!"
                        };
                        db.LogInSystem.Add(addLogInSystem);
                        db.SaveChanges();
                    }
                }
            }
            catch { }
        }

        System.Windows.Threading.DispatcherTimer timer1 = new System.Windows.Threading.DispatcherTimer();
        public void UsersFalse()//Блок неверной капчи
        {
            if (Attempts == 1 || Attempts == 2)
            {
                MessageBoxResult result = MessageBox.Show("Неверно введёт логин или пароль.\nПроверьте введённые вами данные!", 
                    "Информация", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            if (Attempts == 3)
            {
                Captcha captha = new Captcha();
                captha.ShowDialog();
            }
            if (Attempts >= 4)//если пользователь продолжает вводить неверно логин и пароль
            {//вход продолжает блокироваться на 10 секунд
                timer1.Tick += new EventHandler(timerTick2);
                timer1.Interval = new TimeSpan(0, 0, 1);
                timer1.Start();
                if (stop != 0)
                {
                    LoginUser.IsEnabled = false;
                    PasswordUser.IsEnabled = false;
                    Authoriz.IsEnabled = false;
                    MessageBox.Show(String.Format("Вход заблокирован на {0:0} секунд", stop));
                }
            }
        }

        int stop = 10;
        private void timerTick2(object sender, EventArgs e)//Блокировка входа и заполнения при последующих неверных входах
        {
            if (stop == 0)
            {
                LoginUser.IsEnabled = true;
                PasswordUser.IsEnabled = true;
                Authoriz.IsEnabled = true;
                MessageBoxResult result = MessageBox.Show("Время вышло!\nПовторите вход заново!",
                    "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                timer1.Stop();
                stop = 10;
            }
            else
            {
                stop--;
            }

        }
    }
}
