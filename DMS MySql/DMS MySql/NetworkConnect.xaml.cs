using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DMS_MySql
{
    /// <summary>
    /// Логика взаимодействия для NetworkConnect.xaml
    /// </summary>
    public partial class NetworkConnect : Window
    {

        public NetworkConnect()
        {
            InitializeComponent();
            Button_connect_NetworkConnect.Click += new ButtonsEvent().Connect_DB;
            // Меню
            Menu_Item_Settings.Click += new ButtonsEvent().Button_Settings_Open;
            Menu_Item_Exit.Click += new ButtonsEvent().Exit_button;
            Menu_Item_Save.Click += new ButtonsEvent().Save_config;
            Menu_Item_Load.Click += new ButtonsEvent().Load_config;
            Button_Network_Back.Click += Button_Network_BackE;
        }
        public void Button_Network_BackE(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }
    }
}
