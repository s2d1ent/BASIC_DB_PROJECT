using System;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Windows;
using MySql;
using MySql.Data;
using MySql.Data.Types;
using MySql.Data.Common;
using MySql.Data.MySqlClient;
using MySql.Data.EntityFramework;

namespace DMS_MySql
{
    class DataBase
    {
        public string Host { get; set; }
        public string Username { get; set; }
        public int Port { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }
        public string Table { get; set; }

        private string Connector = @"";
        private MySqlConnection Client = new MySqlConnection();

        public DataBase()
        {
            Connector = $@"server={Host};userid={Username};password={Password}";
            Client.ConnectionString = $@"server={Host};userid={Username};password={Password}";
        }
        public DataBase(string host, int port, string username, string password)
        {
            this.Host = host;
            this.Port = port;
            this.Username = username;
            this.Password = password;
            this.Connector = $@"server={Host};userid={Username};password={Password}";
            Client.ConnectionString = $@"server={Host};userid={Username};password={Password}";
        }
        public DataBase(string host, int port, string username, string password, string database)
        {
            this.Host = host;
            this.Port = port;
            this.Username = username;
            this.Password = password;
            this.Database = database;
            this.Connector = $@"server={Host};userid={Username};password={Password};database={Database}";
            Client.ConnectionString = $@"server={Host};userid={Username};password={Password};database={Database}";
        }
        ~DataBase()
        {
            GC.Collect(2,GCCollectionMode.Optimized);
        }
        public void Connect()
        {
            if(Client.State == System.Data.ConnectionState.Closed)
            {
                Client.OpenAsync();
            }
        }
        public void Disconnect()
        {
            if (Client.State == System.Data.ConnectionState.Open)
            {
                Client.Close();
            }
        }
        public System.Data.ConnectionState GetStatus()
        {
            return System.Data.ConnectionState.Open;
        }
        public void GetConnectionData()
        {
            var data = $"Host: {Host}" + "\n" + $"Port: {Port}" + "\n" + $"Username: {Username}" + "\n" + $"Database: {Database}" + "\n";
            MessageBox.Show(data, "Data connection", MessageBoxButton.OK);
        }
        public void UseConfig(string host, int port, string username, string password)
        {
            this.Host = host;
            this.Port = port;
            this.Username = username;
            this.Password = password;
            this.Connector = $@"server={Host};userid={Username};password={Password}";
        }
        // TODO доделать использование конфига
        // разобраться с подключением
        public void UseConfig(string path)
        {
            string Path = path;
            this.Host = (string)XDocument.Parse(File.ReadAllText(Path)).Element("config").Element("host");
            this.Port = (int)XDocument.Parse(File.ReadAllText(Path)).Element("config").Element("port");
            this.Username = (string)XDocument.Parse(File.ReadAllText(Path)).Element("config").Element("username");
            this.Password = (string)XDocument.Parse(File.ReadAllText(Path)).Element("config").Element("psswrd");
            this.Database = (string)XDocument.Parse(File.ReadAllText(Path)).Element("config").Element("database");
            this.Connector = $@"Server={Host};UID={Username};pwd={Password};Database={Database}";
            MessageBox.Show(this.Connector);
            bool status = TryConnect();
            if (status)
            {
                MessageBox.Show("Успешно: подключение установленно", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Ошибка: Отсутствует соединение с сервером", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public bool TryConnect()
        {
            if (Client.State == System.Data.ConnectionState.Closed)
                Client.OpenAsync();
            var ping = Client.Ping();
            if (ping || Client.State == System.Data.ConnectionState.Open)
            {
                Client.CloseAsync();
                return true;
            }
            return false;
        }
    }
}
