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
        }
        public void Connect_DB(object sender, RoutedEventArgs e)
        {
            
        }
        public void Exit_button(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        public void Save_config(object sender, RoutedEventArgs e)
        {

        }
        public void Load_config(object sender, RoutedEventArgs e)
        {

        }
        public void Button_Main_Local(object sender, RoutedEventArgs e)
        {

        }
        public void Button_Main_Network(object sender, RoutedEventArgs e)
        {

        }
        public void Button_Settings_Open(object sender, RoutedEventArgs e)
        {

        }
    }
}
