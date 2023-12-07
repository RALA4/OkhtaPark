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
    /// Логика взаимодействия для AddServicesOrder.xaml
    /// </summary>
    public partial class AddServicesOrder : Window
    {

        public AddServicesOrder()
        {
            InitializeComponent();
            BaseOrder.ItemsSource = App.parkentities.Orders.Select(x=>x.CodeOrder).ToList();
            BaseServices.ItemsSource = App.parkentities.Services.Select(x => x.NameService).ToList();
            BaseServices1.ItemsSource = App.parkentities.Services.Select(x => x.NameService).ToList();
            BaseServices2.ItemsSource = App.parkentities.Services.Select(x => x.NameService).ToList();
            BaseServices3.ItemsSource = App.parkentities.Services.Select(x => x.NameService).ToList();
        }
        int clickPlus = 4, clickMinus = 4;
        private void ActionPlusClick(object sender, RoutedEventArgs e)//Прибавление услуги
        {
            clickPlus--;
            if(clickPlus == 3)
            {
                clickMinus = 2;
                this.Height = 240;  
            }
            else
            {
                if(clickPlus == 2)
                {
                    clickMinus = 3;
                    this.Height = 285;
                }
                else
                {
                    if(clickPlus == 1)
                    {
                        clickMinus = 4;
                        this.Height = 330;
                    }
                    else
                    {
                        if(clickPlus == 0)
                        {
                            MessageBox.Show("В заказ можно добавить максимум 4 услуги!");
                        }
                    }
                }
            }
            
        }

        private void ActionMinusClick(object sender, RoutedEventArgs e)//Уменьшение услугу
        {
            clickMinus--;
            if (clickMinus == 3)
            {
                clickPlus = 2;
                this.Height = 285;
                BaseServices3.Text = null;
            }
            else
            {
                if (clickMinus == 2)
                {
                    clickPlus = 3;
                    this.Height = 240;
                    BaseServices2.Text = null;
                }
                else
                {
                    if (clickMinus == 1)
                    {
                        clickPlus = 4;
                        this.Height = 190;
                        BaseServices1.Text = null;
                    }
                    else
                    {
                        if (clickMinus == 0)
                        {
                            MessageBox.Show("В заказе должна быть хотя бы одна услуга");
                        }
                    }
                }
            }
        }

        private void ActionAddClick(object sender, RoutedEventArgs e)//Добавление услуг в заказ
        {
            try
            {
                bd.okhta_parkEntities db = new bd.okhta_parkEntities();
                int idServic = db.Services.Where(x => x.NameService == BaseServices.Text).Select(c => c.ID).FirstOrDefault();
                int idServic1 = db.Services.Where(x => x.NameService == BaseServices1.Text).Select(c => c.ID).FirstOrDefault();
                int idServic2 = db.Services.Where(x => x.NameService == BaseServices2.Text).Select(c => c.ID).FirstOrDefault();
                int idServic3 = db.Services.Where(x => x.NameService == BaseServices3.Text).Select(c => c.ID).FirstOrDefault();
                int idOrder = db.Orders.Where(x => x.CodeOrder == BaseOrder.Text).Select(c => c.ID).FirstOrDefault();

                if (BaseOrder == null && BaseServices == null)
                {
                    MessageBox.Show("Данные не заполнены!");
                }
                else
                {
                    bd.ServicesOrders addServicesOrder = new bd.ServicesOrders
                    {
                        Id_Order = idOrder,
                        Services = idServic
                    };
                    db.ServicesOrders.Add(addServicesOrder);
                    db.SaveChanges();
                    MessageBox.Show("Данные успешно добавлены!");
                    Seller showSeller = new Seller();
                    showSeller.Show();
                    this.Close();

                    if (idServic1 != null)
                    {
                        bd.ServicesOrders addServicesOrder1 = new bd.ServicesOrders
                        {
                            Id_Order = idOrder,
                            Services = idServic1,
                        };
                        db.ServicesOrders.Add(addServicesOrder1);
                        db.SaveChanges();
                        MessageBox.Show("Данные успешно добавлены!");
                        Seller showSeller1 = new Seller();
                        showSeller.Show();
                        this.Close();

                        if (idServic2 != null)
                        {
                            bd.ServicesOrders addServicesOrder2 = new bd.ServicesOrders
                            {
                                Id_Order = idOrder,
                                Services = idServic2
                            };
                            db.ServicesOrders.Add(addServicesOrder2);
                            db.SaveChanges();
                            MessageBox.Show("Данные успешно добавлены!");
                            Seller showSeller2 = new Seller();
                            showSeller.Show();
                            this.Close();

                            if (idServic3 != null)
                            {
                                bd.ServicesOrders addServicesOrder3 = new bd.ServicesOrders
                                {
                                    Id_Order = idOrder,
                                    Services = idServic3
                                };
                                db.ServicesOrders.Add(addServicesOrder3);
                                db.SaveChanges();
                                MessageBox.Show("Данные успешно добавлены!");
                                Seller showSeller3 = new Seller();
                                showSeller.Show();
                                this.Close();
                            }
                        }
                    }
                }
               
            }
            catch 
            {
                MessageBox.Show("Не все данные заполнены или имеют неверный формат!");
            }
        }

        
    }
}
