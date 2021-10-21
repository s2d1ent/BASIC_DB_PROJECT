using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
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
    /// Логика взаимодействия для CreateConfig.xaml
    /// </summary>
    public partial class CreateConfig : Window
    {
        public CreateConfig()
        {
            InitializeComponent();
            
        }
        public async void btn_create_config(object sender, RoutedEventArgs e)
        {
            try
            {
                General general = new General();
                string Filename = filename.Text, Host, Username, Password, Port, DataBase = database.Text;
                var PathProject = AppDomain.CurrentDomain.BaseDirectory;
                if (filename.Text == "" || filename.Text.Length == 0)
                {
                    Filename = "config_";
                    for (var i = 1; i < 100; i++)
                    {
                        bool FilenameHas = new FileInfo($"{PathProject}/data/conf/{Filename}{i}.conf.xml").Exists;
                        if (FilenameHas)
                            continue;
                        else
                        {
                            Filename += i;
                            break;
                        }
                    }
                }
                if (host.Text.Length == 0)
                {
                    MessageBox.Show("Ошибка: Не введено имя хоста", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (username.Text.Length == 0)
                {
                    MessageBox.Show("Ошибка: Не введено имя пользователя базы данных", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (password.Text.Length == 0)
                {
                    MessageBox.Show("Ошибка: Не введен пароль для подключения базы данных", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                Host = host.Text; Username = username.Text; Password = password.Text;
                if (port.Text.Length == 0)
                    Port = "";
                else
                    Port = port.Text;
                var result = general.SaveConnectedXml(Filename, Host, Port, Username, Password, DataBase);
                result.Wait();
                
                if (result.Result)
                {
                    MessageBox.Show("Successful: The configuration file has been created", "Successful", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    MessageBox.Show("Error: The configuration file was not created", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Message:{ex.Message} \nData: {ex.Data} \nStackTrace{ex.StackTrace} \nHelpLink{ex.HelpLink} \nPlese, copy this message and send on developers email", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
