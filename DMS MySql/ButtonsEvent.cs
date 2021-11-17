using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
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
using Microsoft.Win32;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.EntityFrameworkCore.Metadata;
using Pomelo.EntityFrameworkCore.MySql;


namespace DMS_MySql
{
    public class ButtonsEvent
    {
        DataBase db = new DataBase();
        public Window win;
        public void Connect_FolderDB(object sender, RoutedEventArgs e)
        {

        }
        public void Exit_button(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        public void Save_config_open(object sender, RoutedEventArgs e)
        {
            new CreateConfig().Show();
        }
        public void Load_config_open(object sender, RoutedEventArgs e)
        {
            try 
            {
                var PathProject = AppDomain.CurrentDomain.BaseDirectory;
                string FilePath = "";
                bool use_conf = false;
                if (!Directory.Exists($"{PathProject}/data/conf"))
                    Directory.CreateDirectory($"{PathProject}/data/conf");

                OpenFileDialog ofp = new OpenFileDialog();
                ofp.InitialDirectory = $"{PathProject}" ;
                ofp.Filter = "(*.xml)|*.xml";

                if (ofp.ShowDialog() == true)
                    FilePath = ofp.FileName;
                use_conf = db.UseConfig(FilePath);
                if (use_conf)
                    win.Close();
            }
            catch (ArgumentException ex)
            {
                //MessageBox.Show($"Message:{ex.Message} \nData: {ex.Data} \nStackTrace{ex.StackTrace} \nHelpLink{ex.HelpLink} \nPlese, copy this message and send on developers email", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
