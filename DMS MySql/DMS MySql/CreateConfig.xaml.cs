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
        public void btn_create_config(object sender, RoutedEventArgs e)
        {
            General general = new General();
            string Filename, Host, Username, Password; int Port;
            var PathProject = AppDomain.CurrentDomain.BaseDirectory;
            try
            {
                if (filename.Text.Length == 0)
                {
                    Filename = "config_";
                    for (var i = 1; i < 100000; i++)
                        if (!new DirectoryInfo($"{PathProject}/data/conf/{Filename + i.ToString()}").Exists)
                        {
                            Filename += i;
                            break;
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
                    if (port.Text.Length == 0)
                    {
                        MessageBox.Show("Ошибка: Не введен порт", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    /*if(!int.TryParse(host.Text,out var result))
                    {
                        MessageBox.Show("Ошибка: Введено не верное значение порта. Порт представляет набор цифр", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }*/
                    if (password.Text.Length == 0)
                    {
                        MessageBox.Show("Ошибка: Не введен пароль для подключения базы данных", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    Host = host.Text; Port = Convert.ToInt32(port.Text); Username = username.Text; Password = password.Text;
                    var result = general.SaveConnectedXml(Filename, Host, Port, Username, Password);
                    if (result)
                    {
                        Task.Run(() => {
                            this.Close();
                        });
                        MessageBox.Show("Успешно: Файл конфигурации был создан", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                        MessageBox.Show("Ошибка: Файл конфигурации не был создан", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"{ex.Message} - {ex.Source}");
            }           
        }
    }
}
