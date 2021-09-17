using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
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


namespace DMS_MySql
{
    public class ButtonsEvent
    {
        public void Connect_FolderDB(object sender, RoutedEventArgs e)
        {

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
            new FolderConnect().Show();
            //w.Close();
        }
        public void Button_Main_Network(object sender, RoutedEventArgs e)
        {
            new NetworkConnect().Show();
            //w.Close();
        }
        public void Button_Settings_Open(object sender, RoutedEventArgs e)
        {

        }
        public void Button_Network_Back(object sender, RoutedEventArgs e)
        {

        }
        public void  Button_Folder_Back(object sender, RoutedEventArgs e)
        {

        }
    }
}
