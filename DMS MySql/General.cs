using System;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DMS_MySql
{
    class General
    {
        public void FoldersProject(string path)
        {
            var BasePath = path;
            var data = new DirectoryInfo($"{BasePath}/data");
            var backup = new DirectoryInfo($"{BasePath}/backups");
            if (!data.Exists)
                data.Create();
            if (!backup.Exists)
                backup.Create();
        }
        public void LastConnectedXml()
        {

        }
        /*public void SaveConnectedXml(string filename, string host, string username, string password)
        {
            var PathProject = AppDomain.CurrentDomain.BaseDirectory;
            var ConfDirectory = new DirectoryInfo($"{PathProject}/data/conf");

            if (!ConfDirectory.Exists)
                ConfDirectory.Create();
            else
            {
                XElement hostElement = new XElement("data", host);
                XElement usernameElement = new XElement("data", username);
                XElement passwordElement = new XElement("data", password);
                XElement configsElement = new XElement("config", hostElement, usernameElement, passwordElement);
                XDocument config = new XDocument(configsElement);

                File.WriteAllText($"{ConfDirectory}/{filename}.conf.xml", config.ToString());
            }
        }*/
        public async Task<bool> SaveConnectedXml(string filename, string host, string port, string username, string password, string database)
        {
            var PathProject = AppDomain.CurrentDomain.BaseDirectory;
            var ConfDirectory = new DirectoryInfo($"{PathProject}/data/conf");

            if (!ConfDirectory.Exists)
                ConfDirectory.Create();
            else
            {
                XElement hostElement = new XElement("host", host);
                XElement usernameElement = new XElement("username", username);
                XElement portElement = new XElement("port", port);
                XElement passwordElement = new XElement("psswrd", password);
                XElement databaseElement = new XElement("database", database);
                XElement configsElement = new XElement("config", hostElement, usernameElement, portElement, passwordElement, databaseElement);
                XDocument config = new XDocument(configsElement);

                Task.Run(()=> File.WriteAllText($"{ConfDirectory}/{filename}.conf.xml", config.ToString())).Wait();
                //File.WriteAllText($"{ConfDirectory}/{filename}.conf.xml", config.ToString());
            }
            if (new FileInfo($"{PathProject}/data/conf/{filename}.conf.xml").Exists)
                return true;
            else
                return false;
        }
        public void OpenAbout(object sender, RoutedEventArgs e)
        {
            about ab = new about();
            ab.Show();
        }
    }
}
