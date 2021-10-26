using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DMS_MySql
{
    public class History
    {
        public List<DMS_MySql.DataBase> List = new List<DMS_MySql.DataBase>();
        public History()
        {

        }
        ~History()
        {

        }
        public void UpdateList()
        {
            if (!File.Exists($"{AppDomain.CurrentDomain.BaseDirectory}/data/history.json"))
            {
                string json = "";
                DataBase db = new DataBase();
                Stack<string> name = new Stack<string>();
                name.Push("One"); name.Push("Two"); name.Push("Three"); name.Push("Four"); name.Push("Five");
                for(var i = 0; i < name.Count; i++)
                {
                    db_local.Name = name.Pop();
                    string Json = "";
                    Json += JsonSerializer.Serialize<DataBase>(db);
                }
            }
            else 
            {
                string json = File.ReadAllText($"{AppDomain.CurrentDomain.BaseDirectory}/data/history.json");
            }
            
        }
    }
}
