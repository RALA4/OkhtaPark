using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    /// Логика взаимодействия для Captcha.xaml
    /// </summary>
    public partial class Captcha : Window
    {
        public Captcha()
        {
            InitializeComponent();
            ForCaptcha();
        }
        int error = 0;
        MainWindow workAutho = new MainWindow();

        public void ForCaptcha()//Генератор капчи
        {
            try
            {
                string cap = "1234567890qwertyuiopasdfghjklzxcvbnm";
                Random r = new Random();
                Letter1.Text = Convert.ToString(cap[r.Next(cap.Length - 1)]);
                Letter2.Text = Convert.ToString(cap[r.Next(cap.Length - 1)]);
                Letter3.Text = Convert.ToString(cap[r.Next(cap.Length - 1)]);
                Letter4.Text = Convert.ToString(cap[r.Next(cap.Length - 1)]);
                Letter1.Margin = new Thickness(r.Next(2, 20), r.Next(-20, 20), 0, 0);
                Letter2.Margin = new Thickness(r.Next(-20, 20), r.Next(-20, 20), 0, 0);
                Letter3.Margin = new Thickness(r.Next(-20, 20), r.Next(-20, 20), 0, 0);
                Letter4.Margin = new Thickness(r.Next(-20, 20), r.Next(-20, 20), 0, 0);
                Letter1.FontSize = r.Next(20, 50);
                Letter2.FontSize = r.Next(20, 50);
                Letter3.FontSize = r.Next(20, 50);
                Letter4.FontSize = r.Next(20, 50);
                for (int i = 0; i < 15; i++)
                {
                    Ellipse e = new Ellipse();
                    e.Width = r.Next(2, 10);
                    e.Height = e.Width;
                    e.Fill = new SolidColorBrush(Color.FromRgb(211, 211, 211));
                    e.Margin = new Thickness(r.Next(10, 220), r.Next(10, 120), 0, 0);
                    Noise.Children.Add(e);
                }
            }
            catch { }
        }

        private void TextCaptchaPreviewKeyDown(object sender, KeyEventArgs e)//Запрет на пробелы
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        int stop = 10;
        System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();

        private void CheckClick(object sender, RoutedEventArgs e)//Проверка
        {
            if (TextCaptcha.Text != Letter1.Text + Letter2.Text + Letter3.Text + Letter4.Text)
            {
                error++;
                if (error == 3)
                {
                    
                    workAutho.LoginUser.IsEnabled = false;
                    workAutho.PasswordUser.IsEnabled = false;
                    workAutho.Authoriz.IsEnabled = false;
                    Check.IsEnabled = false;
                    Restart.IsEnabled = false;
                    TextCaptcha.IsEnabled = false;
                    TextCaptcha.Clear();
                    MessageBoxResult result = MessageBox.Show("Captcha была введена много раз неверно!\n" +
                        "Подождите.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    timer.Tick += new EventHandler(timerTick);
                    timer.Interval = new TimeSpan(0, 0, 1);
                    timer.Start();
                }
                else
                {
                    RestartClick(sender, e);
                    TextCaptcha.Clear();
                    MessageBoxResult result1 = MessageBox.Show("Captcha введена неверно!\n" +
                        "Повторите ввод заново!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBoxResult result1 = MessageBox.Show("Captcha введена верно!", 
                    "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
        }

        private void timerTick(object sender, EventArgs e)//Когда время вышло
        {
            if (stop == 0)
            {
                workAutho.LoginUser.IsEnabled = true;
                workAutho.PasswordUser.IsEnabled = true;
                workAutho.Authoriz.IsEnabled = true;
                Check.IsEnabled = true;
                Restart.IsEnabled = true;
                TextCaptcha.IsEnabled = true;
                MessageBoxResult result1 = MessageBox.Show("Время вышло!\nВведите Captcha заново!", 
                    "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                timer.Stop();
                error = 0;
                stop = 10;
            }
            else
            {
                stop--;
            }

        }

        private void RestartClick(object sender, RoutedEventArgs e)//Обновить
        {
            Noise.Children.Clear();
            ForCaptcha();
            TextCaptcha.Clear();
        }
    }
}
