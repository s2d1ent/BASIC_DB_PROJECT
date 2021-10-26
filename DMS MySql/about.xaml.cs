using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
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
    /// Логика взаимодействия для about.xaml
    /// </summary>
    public partial class about : Window
    {
        public string version { get; set; }
        public string developers { get; set; }
        public string website { get; set; }
        public string license_agreement { get; set; }
        public about()
        {
            InitializeComponent();
            Exit.Click += ExitBtn;
            GetInfo();
        }
        void GetInfo()
        {
            JsonSerializer json 
        }
        public void ExitBtn(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
