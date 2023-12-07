using MaterialDesignColors;
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
    /// Логика взаимодействия для AddClient.xaml
    /// </summary>
    public partial class AddClient : Window
    {
        public AddClient()
        {
            InitializeComponent();
            Birthday.DisplayDateEnd = DateTime.Now.AddYears(-18);//Клиенту должно быть не меньше 18 лет 
        }

        public static bool IsValidEmailId(string InputEmail)//Формат для почты
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(InputEmail);
            if (match.Success)
                return true;
            else
                return false;
        }

        private void ActionAddClick(object sender, RoutedEventArgs e)//Добавление Клиента
        {
            try
            {
                string email = Email.Text;
                string codeClient = CodeClient.Text;
                string ForSeria = Seria.Text;
                string ForNumber = Number.Text;
                if (IsValidEmailId(email))
                {
                    string FullName = Fname.Text + " " + Name.Text + " " + Sname.Text;
                    string FullPasswort = "Серия " + Seria.Text + " Номер " + Number.Text;
                    bd.okhta_parkEntities db = new bd.okhta_parkEntities();
                    bd.Clients client = new bd.Clients()
                    {
                        FIO = FullName,
                        CodeClient = Convert.ToInt32(CodeClient.Text),
                        PassportData = FullPasswort,
                        Birthday = Convert.ToDateTime(Birthday.Text),
                        Address = Address.Text,
                        Email = Email.Text,
                        Password = Password.Text
                    };
                    db.Clients.Add(client);
                    db.SaveChanges();
                    MessageBox.Show("Данные добавлены успешно!");
                    Administrator showAdministartor = new Administrator();
                    showAdministartor.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Почта имеет неверный формат!");
                }
            }
            catch
            {
                MessageBox.Show("Не все данные заполнены!");
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
            if (e.Key == Key.NumPad0 || e.Key == Key.NumPad1 || e.Key == Key.NumPad2 || e.Key == Key.NumPad3 
                || e.Key == Key.NumPad4 || e.Key == Key.NumPad5 || e.Key == Key.NumPad6 || e.Key == Key.NumPad7 
                || e.Key == Key.NumPad8 || e.Key == Key.NumPad9 || e.Key == Key.Back)
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
