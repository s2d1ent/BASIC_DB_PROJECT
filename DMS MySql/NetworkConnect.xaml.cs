﻿using System;
using System.Threading.Tasks;
using System.Collections.Generic;
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
        DataBase db = new DataBase();
        Workspace wk = new Workspace();
        public NetworkConnect()
        {
            InitializeComponent();
            ButtonsEvent be = new ButtonsEvent();
            be.win = this;

            Button_connect_NetworkConnect.Click += Connect;
            // Меню
            Menu_Item_Settings.Click += be.Button_Settings_Open;
            Menu_Item_Exit.Click += be.Exit_button;
            Menu_Item_Save.Click += be.Save_config_open;
            Menu_Item_Load.Click += be.Load_config_open;
            Button_Network_Back.Click += Button_Network_BackE;
        }
        public void Button_Network_BackE(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }
        private void Connect(object sender, RoutedEventArgs e)
        {
            db = new DataBase(Host.Text, Port.Text, Username.Text, Password.Text, Database.Text);
            bool try_connect = db.TryConnect();
            if (try_connect)
            {
                wk.db = db;
                wk.Show();
                this.Close();
            }
        }
    }
}
