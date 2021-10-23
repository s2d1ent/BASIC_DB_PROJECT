using System;
using System.IO;
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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var PathProject = AppDomain.CurrentDomain.BaseDirectory;
            ButtonsEvent be = new ButtonsEvent();
            be.win = this;
            new General().FoldersProject(PathProject);

            MainWindow_local_btn.Click += Button_Main_Local;
            MainWindow_network_btn.Click += Button_Main_Network;
            // Меню
            Menu_Item_Settings.Click += be.Button_Settings_Open;
            Menu_Item_Exit.Click += be.Exit_button;
            Menu_Item_Save.Click += be.Save_config_open;
            Menu_Item_Load.Click += be.Load_config_open;
        }
        public void Button_Main_Local(object sender, RoutedEventArgs e)
        {
            new FolderConnect().Show();
            this.Close();

        }
        public void Button_Main_Network(object sender, RoutedEventArgs e)
        {
            new NetworkConnect().Show();
            this.Close();
        }
    }
   
}
