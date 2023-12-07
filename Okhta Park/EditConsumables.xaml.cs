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
    /// Логика взаимодействия для EditConsumables.xaml
    /// </summary>
    public partial class EditConsumables : Window
    {
        bd.okhta_parkEntities db;
        public EditConsumables(bd.okhta_parkEntities db, bd.Services serv)
        {
            InitializeComponent();
            this.db = db;
            this.DataContext = serv;
            identity = serv.ID;
        }
        int identity;
        private void ActionEditClick(object sender, RoutedEventArgs e)//Редактирование Услугу
        {
            try
            {
                db.SaveChanges();
                MessageBox.Show("Данные изменены");
                Administrator showAdministrator = new Administrator();
                showAdministrator.Show();
                this.Close();
            }
            catch { }
        }

        private void ActionDeleteClick(object sender, RoutedEventArgs e)//Удаление Услуги
        {
            try
            {
                bd.okhta_parkEntities parkEntities = new bd.okhta_parkEntities();
                if (identity == null)
                {
                    MessageBox.Show("Не выбрана строка для удаления!");
                }
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить услугу?", 
                    "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    App.parkentities.Services.Remove(App.parkentities.Services.Find(identity));
                    App.parkentities.SaveChanges();
                    Administrator showAdministrator = new Administrator();
                    showAdministrator.Show();
                    this.Close();
                }
            }
            catch { }
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

        private void ActionOutClick(object sender, RoutedEventArgs e)//Выход
        {
            Administrator showAdministrator = new Administrator();
            showAdministrator.Show();
            this.Close();
        }
    }
}
