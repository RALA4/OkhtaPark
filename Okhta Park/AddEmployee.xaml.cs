using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
    /// Логика взаимодействия для AddEmployee.xaml
    /// </summary>
    public partial class AddEmployee : Window
    {
        public AddEmployee(bd.okhta_parkEntities db, bd.Employee empl, bool add)
        {
            InitializeComponent();
            Post.ItemsSource = App.parkentities.Post.Select(x => x.NamePost).ToList();

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

        private void EditPhotoClick(object sender, RoutedEventArgs e)//Редактирование фото
        {
            photoAdd();
        }

        private void ActionAddClick(object sender, RoutedEventArgs e)//Добавление Клиента
        {
            try
            {
                string email = Login.Text;
                string codeEmployee = CodeEmployee.Text;
                if (IsValidEmailId(email))
                {
                    string FullName = Fname.Text + " " + Name.Text + " " + Sname.Text;
                    bd.okhta_parkEntities db = new bd.okhta_parkEntities();
                    int CodePost = db.Post.Where(x => x.NamePost == Post.Text).Select(c => c.idPost).FirstOrDefault();
                    bd.Employee empl = new bd.Employee()
                    {
                        CodeEmployee = Convert.ToInt32(CodeEmployee.Text),
                        id_Post = CodePost,
                        FIO = FullName,
                        Login = Login.Text,
                        Password = Password.Text,
                        Image = getJPGFromImageControl(Photo.Source as BitmapImage)

                    };
                    db.Employee.Add(empl);
                    db.SaveChanges();
                    MessageBox.Show("Данные добавлены успешно!");
                    Administrator showAdministartor = new Administrator();
                    showAdministartor.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Логин/Почта имеет неверный формат!");
                }
            }
            catch
            {
                MessageBox.Show("Не все данные заполнены или имееют неверный формат!");
            }
        }

        public void photoAdd()//Для фото
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image files (*.BMP, *.JPG, *.GIF, *.TIF, *.PNG, *.ICO, *.EMF, *.WMF, *.JPEG)|*.bmp;*.jpg;*.gif; *.tif; *.png; *.ico; *.emf; *.wmf; *.jpeg";
            if (fileDialog.ShowDialog() == true)
            {
                string fileName = fileDialog.FileName;
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.UriSource = new Uri(fileName);
                Photo.Source = bi;
                bi.EndInit();
                getJPGFromImageControl(bi);
            }
        }

        public byte[] getJPGFromImageControl(BitmapImage image)//Для фото
        {
            MemoryStream memStream = new MemoryStream();
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));
            encoder.Save(memStream);
            return memStream.ToArray();
        }

        private void ActionOutClick(object sender, RoutedEventArgs e)//Выход
        {
            Administrator showAdministartor = new Administrator();
            showAdministartor.Show();
            this.Close();
        }

        private void FormatPreviewKeyDown(object sender, KeyEventArgs e)//Запрет на ввод букв
        {
            if (e.Key == Key.NumPad0 || e.Key == Key.NumPad1 || e.Key == Key.NumPad2 || e.Key == Key.NumPad3 || e.Key == Key.NumPad4 || e.Key == Key.NumPad5 ||
               e.Key == Key.NumPad6 || e.Key == Key.NumPad7 || e.Key == Key.NumPad8 || e.Key == Key.NumPad9 || e.Key == Key.Back)
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
