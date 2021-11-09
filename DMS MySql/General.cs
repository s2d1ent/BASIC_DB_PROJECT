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
        public Window win;
        public void FoldersProject(string path)
        {
            var BasePath = path;
            if (!Directory.Exists($"{BasePath}/data"))
                Directory.CreateDirectory($"{BasePath}/data");
            if (!Directory.Exists($"{BasePath}/backups"))
                Directory.CreateDirectory($"{BasePath}/backups");
            if (!Directory.Exists($"{BasePath}/data/conf"))
                Directory.CreateDirectory($"{BasePath}/data/conf");
        }
        public List<MenuItem> MenuLoad()
        {
            ButtonsEvent be = new ButtonsEvent();
            List<MenuItem> menu = new List<MenuItem>();
            // File
            var File = new MenuItem();
            File.Header = "File";
            var open = new MenuItem();
            open.Header = "Open";
            var save = new MenuItem();
            save.Header = "Save";
            var settings = new MenuItem();
            settings.Header = "Settings";
            settings.Name = "Menu_Item_Settings";
            settings.Click += be.Button_Settings_Open;
            var exit = new MenuItem();
            exit.Header = "Exit";
            exit.Name = "Menu_Item_Exit";
            exit.Click += be.Exit_button;

            File.Items.Add(open);
            File.Items.Add(save);
            File.Items.Add(settings);
            File.Items.Add(exit);
            // Edit
            // Connect
            var Connect = new MenuItem();
            Connect.Header = "Connect";
            var save_connect = new MenuItem();
            save_connect.Header = "Save config";
            save_connect.Name = "Menu_Item_Save";
            save_connect.Click += be.Save_config_open;
            var load_connect = new MenuItem();
            load_connect.Header = "Load config";
            load_connect.Name = "Menu_Item_Load";
            load_connect.Click += be.Load_config_open;
            var history_connect = new MenuItem();
            history_connect.Header = "History";
            load_connect.Name = "History_connection";

            Connect.Items.Add(save_connect);
            Connect.Items.Add(load_connect);
            Connect.Items.Add(history_connect);
            // Preference
            var Reference = new MenuItem();
            Reference.Header = "Reference";
            var license_agreement_ref = new MenuItem();
            license_agreement_ref.Header = "License Agreement";
            license_agreement_ref.Name = "Menu_Item_License";
            license_agreement_ref.Click += this.Open_License;
            var check_update_ref = new MenuItem();
            check_update_ref.Header = "Check Update";
            check_update_ref.Name = "Menu_Item_Update";
            var about_ref = new MenuItem();
            about_ref.Header = "About";
            about_ref.Name = "Menu_Item_About";
            about_ref.Click += this.OpenAbout;

            Reference.Items.Add(license_agreement_ref);
            Reference.Items.Add(check_update_ref);
            Reference.Items.Add(about_ref);

            menu.Add(File);
            menu.Add(Connect);
            menu.Add(Reference);
            return menu;
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

                await Task.Run(()=> File.WriteAllText($"{ConfDirectory}/{filename}.conf.xml", config.ToString()));
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
        public void Open_License(object sender, MouseEventArgs e)
        {
            var myProcess = new System.Diagnostics.Process();
            myProcess.StartInfo.UseShellExecute = true;
            myProcess.StartInfo.FileName = $"{AppDomain.CurrentDomain.BaseDirectory}/data/license.html";
            myProcess.Start();
        }
        public void Open_License(object sender, RoutedEventArgs e)
        {
            var myProcess = new System.Diagnostics.Process();
            myProcess.StartInfo.UseShellExecute = true;
            myProcess.StartInfo.FileName = $"{AppDomain.CurrentDomain.BaseDirectory}/data/license.html";
            myProcess.Start();
        }
    }
}
