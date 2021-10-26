using System;
using System.IO;
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
        
        public about()
        {
            General ge = new General();
            InitializeComponent();
            Exit.Click += ExitBtn;

            WebSite.MouseLeftButtonDown += Open_site;
            License_Agreement.MouseLeftButtonDown += ge.Open_License;

            string Json = File.ReadAllText($"{AppDomain.CurrentDomain.BaseDirectory}/data/about.json");
            About json = JsonSerializer.Deserialize<About>(Json);
            Version.Content = json.Version;
            Developers.Content = "s2d1ent";
            WebSite.Text = @"https://vk.com/s2d1entstr";
            License_Agreement.Content = "Click for read";
        }
        public void ExitBtn(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        void Open_site(object sender, MouseEventArgs e)
        {
            var myProcess = new System.Diagnostics.Process();
            myProcess.StartInfo.UseShellExecute = true;
            myProcess.StartInfo.FileName = "https://vk.com/s2d1entstr";
            myProcess.Start();
        }
        
    }
    
}
