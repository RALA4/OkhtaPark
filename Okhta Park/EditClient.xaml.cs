using Okhta_Park.bd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
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
    /// Логика взаимодействия для EditClient.xaml
    /// </summary>
    public partial class EditClient : Window
    {
        bd.okhta_parkEntities db;
        public EditClient(bd.okhta_parkEntities db, bd.Clients client)
        {
            InitializeComponent();
            Birthday.DisplayDateEnd = DateTime.Now.AddYears(-18);//Клиенту должно быть не меньше 18 лет 
            this.db = db;
            this.DataContext = client;
            identity = client.CodeClient;
            Birthday.Text = Convert.ToString(client.Birthday);
        }
        int identity;

        public static bool IsValidEmailId(string InputEmail)//Формат для почты
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(InputEmail);
            if (match.Success)
                return true;
            else
                return false;
        }

        private void ActionEditClick(object sender, RoutedEventArgs e)//Редактирование клиента
        {
            try
            {
                string email = Email.Text;
                if (IsValidEmailId(email))
                {
                    db.SaveChanges();
                    Administrator showAdministrator = new Administrator();
                    showAdministrator.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Почта имеет неверный формат!");
                }
            }
            catch
            {

            }
        }

        private void ActionDeleteClick(object sender, RoutedEventArgs e)//Удаление клиента
        {
            bd.okhta_parkEntities parkEntities = new bd.okhta_parkEntities();
            if (identity == null)
            {
                MessageBox.Show("Не выбрана строка для удаления!");
            }
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить клиента?", 
                "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                App.parkentities.Clients.Remove(App.parkentities.Clients.Find(identity));
                App.parkentities.SaveChanges();
                Administrator showAdministrator = new Administrator();
                showAdministrator.Show();
                this.Close();
            }
        }

        private void ActionOutClick(object sender, RoutedEventArgs e)//Выход
        {
            Administrator showAdministrator = new Administrator();
            showAdministrator.Show();
            this.Close();
        }
    }
}
