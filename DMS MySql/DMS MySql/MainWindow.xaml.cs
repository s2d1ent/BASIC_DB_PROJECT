﻿using System;
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
            new NetworkConnect().Show();
            this.Close();
        }
        public void Button_Settings_Open(object sender, RoutedEventArgs e)
        {

        }
    }
   
}
