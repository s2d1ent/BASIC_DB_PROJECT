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
    /// Логика взаимодействия для FolderConnect.xaml
    /// </summary>
    public partial class FolderConnect : Window
    {
        public FolderConnect()
        {
            InitializeComponent();
            Button_connect_FolderConnect.Click += new ButtonsEvent().Connect_FolderDB;
            Button_Folder_Back.Click += Button_Folder_BackE;
            // Меню
            Menu_Item_Settings.Click += new ButtonsEvent().Button_Settings_Open;
            Menu_Item_Exit.Click += new ButtonsEvent().Exit_button;
            Menu_Item_Save.Click += new ButtonsEvent().Save_config_open;
            Menu_Item_Load.Click += new ButtonsEvent().Load_config_open;

        }
        public void Button_Folder_BackE(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }
    }
}
