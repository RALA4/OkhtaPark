using Microsoft.Win32;
using Okhta_Park.bd;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
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
    /// Логика взаимодействия для EditEmployee.xaml
    /// </summary>
    public partial class EditEmployee : Window
    {
        bd.Employee empl;
        public EditEmployee(bd.Employee empl, bool add)
        {
            InitializeComponent();
            this.empl = empl;
            id = empl.CodeEmployee;
            this.DataContext = empl;
            this.add = add;
            Post.ItemsSource = App.parkentities.Post.Select(x => x.NamePost).ToList();
            List<Employee> products = App.parkentities.Employee.Where(a => a.CodeEmployee == id).ToList();
            Photo.Source = LoadImage(products[0].Image);
        }
        int id;
        bool save = false, add;

        public static bool IsValidEmailId(string InputEmail)//Формат для почты
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(InputEmail);
            if (match.Success)
                return true;
            else
                return false;
        }

        private void EditPhotoClick(object sender, RoutedEventArgs e)//Для редактирования фото
        {
            photoAdd();
        }

        private void ActionEditClick(object sender, RoutedEventArgs e)//Редактирование Сотрудника
        {
            try
            {
                string ForLogin = Login.Text;
                if (IsValidEmailId(ForLogin))
                {
                    int id_type = App.parkentities.Post.Where(c => c.NamePost == Post.Text).Select(c => c.idPost).FirstOrDefault();
                    List<Employee> products = App.parkentities.Employee.Where(a => a.CodeEmployee == id).ToList();
                    try
                    {

                        products[0].CodeEmployee = Convert.ToInt32(CodeEmployee.Text);
                        products[0].id_Post = id_type;
                        products[0].FIO = FIO.Text;
                        products[0].Login = Login.Text;
                        products[0].Password = Password.Text;
                    }
                    catch { }
                    products[0].Image = getJPGFromImageControl(Photo.Source as BitmapImage);
                    App.parkentities.Employee.AddOrUpdate();
                    App.parkentities.SaveChanges();
                    MessageBox.Show("Данные изменены");
                    Administrator showAdministartor = new Administrator();
                    showAdministartor.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Логин/Почта имеет неверный формат!");
                }
            }
            catch { }
        }

        private void ActionDeleteClick(object sender, RoutedEventArgs e)//Удаление Сотрудника
        {
            try
            {
                bd.okhta_parkEntities entities = new bd.okhta_parkEntities();//создание нового контекста данных
                if (id == null)//Если никакая строка не выделена, выводим сообщение
                {
                    MessageBox.Show("Не выбрана ни одна строка для удаления!");
                    return;
                }
                //выводим сообщение с кнопками и получаем результат нажатой кнопки
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить данные специалиста",
                "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)//если нажата кнопка да 
                {
                    App.parkentities.Employee.Remove(App.parkentities.Employee.Find(id));//удаление выделенной строки
                    App.parkentities.SaveChanges();//сохранение изменений в БД
                    Administrator showAdministrator = new Administrator();
                    showAdministrator.Show();
                    this.Close();
                }
            }
            catch { }
        }

        public void photoAdd()//Для фото
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image files (*.BMP, *.JPG, *.GIF, *.TIF, *.PNG, *.ICO, *.EMF, *.WMF, *.JPEG)|*.bmp;*.jpg;*.gif; *.tif; *.png; *.ico; *.emf; *.wmf, *.jpeg";
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
            Administrator showAdministrator = new Administrator();
            showAdministrator.Show();
            this.Close();
        }

        private static BitmapImage LoadImage(byte[] imageData)//Конвертация из byte в image
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }
    }
}
