using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace DMS_MySql
{
    public class History
    {
        public Window win;
        public MenuItem history;
        public List<DataBase> DataBases = new List<DataBase>();
        public List<MenuItem> menuItems = new List<MenuItem>();
        string domain = AppDomain.CurrentDomain.BaseDirectory;
        public History()
        {

        }
        ~History()
        {
            GC.Collect(2, GCCollectionMode.Forced);
        }
        public void ToListFromConfig()
        {
            string xml = $"{domain}/data/history.xml";
            var doc = XDocument.Parse(File.ReadAllText(xml));
            foreach(XElement elem in doc.Element("history").Elements("Database"))
            {
                string name = (string)elem.Element("Name"),
                    host = (string)elem.Element("Host"),
                    username = (string)elem.Element("Username"),
                    port = (string)elem.Element("Port"),
                    password = (string)elem.Element("Password"),
                    database = (string)elem.Element("Database");
                MenuItem item = new MenuItem();
                if (name.Length == 0)
                    name = $"{host} - {username}";
                item.Header = name;
                DataBases.Add(new DataBase(host, port, username, password, database));
                menuItems.Add(item);
            }
        }
        public void UpdateConfig()
        {

            string name;
            string xml = $"{domain}/data/history.xml";
            var doc = XDocument.Parse(File.ReadAllText(xml));
            XDocument doc_new = new XDocument();
            XElement history = new XElement("history");
                
            for (var i = 0; i < DataBases.Count; i++)
            {
                name = (string)menuItems[i].Header;
                XElement Databas = new XElement("Database");
                XElement Name = new XElement("Name", name);
                XElement Host = new XElement("Host", DataBases[i].Host);
                XElement Username = new XElement("Username", DataBases[i].Username);
                XElement Port = new XElement("Port", DataBases[i].Port);
                XElement Password = new XElement("Password", DataBases[i].Password);
                XElement Database1 = new XElement("Database", DataBases[i].Database);
                Databas.Add(Name, Host, Username, Port, Password, Database1);
                history.Add(Databas);
            }
            //MessageBox.Show(history.Value.ToString());
            doc_new.Add(history);
            doc_new.Save(xml);
        }
        public void AddConnection(DataBase db)
        {
            Task.Run(()=> {
                MessageBox.Show(db.Host);
                List<DataBase> time = new List<DataBase>();
                time.Add(db);
                for (var i = 0; i < DataBases.Count; i++)
                    time.Add(DataBases[i]);
                DataBases.Clear();
                for (var i = 0; i < time.Count; i++)
                {
                    DataBases.Add(time[i]);
                    if (i == 4)
                        break;
                }
            });
            Task.Run(()=> {
                List<MenuItem> menu = new List<MenuItem>();
                MenuItem item = new MenuItem();
                item.Header = $"{db.Host} - {db.Username}";
                menu.Add(item);
                for (var i = 0; i < menuItems.Count; i++)
                    menu.Add(menuItems[i]);
                menuItems.Clear();
                for (var i = 0; i < menu.Count; i++)
                {
                    menuItems.Add(menu[i]);
                    if (i == 4)
                        break;
                }
            });
        }
    }
}
