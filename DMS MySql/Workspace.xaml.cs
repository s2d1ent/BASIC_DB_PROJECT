using System;
using System.Threading;
using System.Threading.Tasks;
using System.Data;
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
    /// Логика взаимодействия для Workspace.xaml
    /// </summary>
    public partial class Workspace : Window
    {
        public DataBase db;
        static object locker = new object();
        static Mutex mutexObj = new Mutex();
        public Workspace()
        {
            InitializeComponent();
            var PathProject = AppDomain.CurrentDomain.BaseDirectory;
            ButtonsEvent be = new ButtonsEvent();
            General gn = new General();
            be.win = this;
            gn.FoldersProject(PathProject);

            // Меню
            Menu_Item_Settings.Click += be.Button_Settings_Open;
            Menu_Item_Exit.Click += be.Exit_button;
            Menu_Item_Save.Click += be.Save_config_open;
            Menu_Item_Load.Click += be.Load_config_open;

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(db.Database.Length == 0)
            {
                List<string> databases = db.GetDatabases();
                for (var i = 0; i < databases.Count; i++)
                {
                    TreeViewItem tree_database = new TreeViewItem();
                    tree_database.Header = databases[i];
                    databases.Add(databases[i]);
                    tree_database.Selected += DatabaseOut;
                    Tree_Tables_DataBase.Items.Add(tree_database);
                    List<string> tree_tables = db.GetTables(databases[i]);
                    for (var j = 0; j < tree_tables.Count; j++)
                    {
                        TreeViewItem tree_tables_elem = new TreeViewItem();
                        tree_tables_elem.Selected += TableOut;
                        tree_tables_elem.Header = tree_tables[j];
                        tree_database.Items.Add(tree_tables_elem);
                    }
                }
            }
            else
            {
                TreeViewItem tree_database = new TreeViewItem();
                tree_database.Header = db.Database;
                tree_database.Selected += DatabaseOut;
                Tree_Tables_DataBase.Items.Add(tree_database);
                List<string> tree_tables = db.GetTables(db.Database);
                for (var j = 0; j < tree_tables.Count; j++)
                {
                    TreeViewItem tree_tables_elem = new TreeViewItem();
                    tree_tables_elem.Selected += TableOut;
                    tree_tables_elem.Header = tree_tables[j];
                    tree_database.Items.Add(tree_tables_elem);
                }
                tree_database.IsExpanded = true;
            }
            DataTable table = db.GetTable("wp_terms");
            foreach (DataColumn elem in table.Columns)
            {
                var col = new DataGridTextColumn();
                col.Header = elem.ColumnName;
                col.Binding = new Binding(elem.ColumnName);
                DataBase_Struct.Columns.Add(col);
            }
            foreach (DataColumn elem in table.Columns)
            {
                var col = new DataGridTextColumn();
                col.Header = elem.ColumnName;
                col.Binding = new Binding(elem.ColumnName);
                DataBase_Table.Columns.Add(col);
            }
            foreach (DataRow elem in table.Rows)
            {
                DataBase_Table.Items.Add(table.DefaultView[table.Rows.IndexOf(elem)]);
            }
        }
        // TODO сделать заполнение
        void TableOut(object sender, RoutedEventArgs e)
        {
            db.Table = sender.ToString().Replace("System.Windows.Controls.TreeViewItem", "")
                                .Replace("Header:", "")
                                .Replace("Items.Count:0", "")
                                .Replace(" ", "");
            MessageBox.Show($"{db.Table}");
            DataTable table = db.GetTable(db.Table);
            DataBase_Table.Columns.Clear();
            DataBase_Table.Items.Clear();
            DataBase_Struct.Columns.Clear();
            foreach (DataColumn elem in table.Columns)
            {
                var col = new DataGridTextColumn();
                col.Header = elem.ColumnName;
                col.Binding = new Binding(elem.ColumnName);
                DataBase_Table.Columns.Add(col);
                DataBase_Struct.Columns.Add(col);
            }
            foreach (DataRow elem in table.Rows)
            {
                DataBase_Table.Items.Add(table.DefaultView[table.Rows.IndexOf(elem)]);
            }
            
        }
        void DatabaseOut(object sender, RoutedEventArgs e)
        {
            string str = sender.ToString().Replace("System.Windows.Controls.TreeViewItem", "")
                            .Replace("Header:", "");
            int count = str.IndexOf("Items.Count:");
            string neW = str.Substring(count);
            str = str.Replace(neW, "").Replace(" ", "");
            // MessageBox.Show($"{str}");
        }
        void TableWrite(DataTable table)
        {
            //mutexObj.WaitOne();
            
            //mutexObj.ReleaseMutex();
        }
    }
}
