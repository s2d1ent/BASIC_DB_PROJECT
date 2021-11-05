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
        History history = new History();
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

            // Кнопки управления
            DataBase_Update.Click += Update;

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (db.Database.Length == 0 || db.Database == "")
                {
                    List<string> databases = db.GetDatabases();
                    for (var i = 0; i < databases.Count; i++)
                    {
                        TreeViewItem tree_database = new TreeViewItem();
                        tree_database.Header = databases[i];
                        Tree_Tables_DataBase.Items.Add(tree_database);
                        List<string> tree_tables = db.GetTables(databases[i]);
                        for (var j = 0; j < tree_tables.Count; j++)
                        {
                            TreeViewItem tree_tables_elem = new TreeViewItem();
                            tree_tables_elem.Selected += TableOutAsync;
                            tree_tables_elem.Header = tree_tables[j];
                            tree_database.Items.Add(tree_tables_elem);
                        }
                    }
                }
                else
                {
                    TreeViewItem tree_database = new TreeViewItem();
                    tree_database.Header = db.Database;
                    Tree_Tables_DataBase.Items.Add(tree_database);
                    List<string> tree_tables = db.GetTables(db.Database);
                    for (var j = 0; j < tree_tables.Count; j++)
                    {
                        TreeViewItem tree_tables_elem = new TreeViewItem();
                        tree_tables_elem.Selected += TableOutAsync;
                        tree_tables_elem.Header = tree_tables[j];
                        tree_database.Items.Add(tree_tables_elem);
                    }
                    tree_database.IsExpanded = true;
                }
            }
            catch(Exception ex)
            {
                Task.Run(() => {
                    MessageBox.Show($"Message:{ex.Message} \nData: {ex.Data} \nStackTrace{ex.StackTrace} \nHelpLink{ex.HelpLink} \nPlese, copy this message and send on developers email", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                });
            }
        }
        void TableOut(object sender, RoutedEventArgs e)
        {
            string parent = ((TreeViewItem)sender).Parent.ToString();
            string str = parent.Replace("System.Windows.Controls.TreeViewItem", "")
                            .Replace("Header:", "");
            int count = str.IndexOf("Items.Count:");
            string neW = str.Substring(count);
            str = str.Replace(neW, "").Replace(" ", "");
            db.Database = str;
            db.Table = sender.ToString().Replace("System.Windows.Controls.TreeViewItem", "")
                                .Replace("Header:", "")
                                .Replace("Items.Count:0", "")
                                .Replace(" ", "");
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
            }
            foreach (DataColumn elem in table.Columns)
            {
                var col = new DataGridTextColumn();
                col.Header = elem.ColumnName;
                col.Binding = new Binding(elem.ColumnName);
                DataBase_Struct.Columns.Add(col);
            }
            foreach (DataRow elem in table.Rows)
                DataBase_Table.Items.Add(table.DefaultView[table.Rows.IndexOf(elem)]);
        }
        async void TableOutAsync(object sender, RoutedEventArgs e)
        {
            try
            {
                string parent = ((TreeViewItem)sender).Parent.ToString();
                string str = parent.Replace("System.Windows.Controls.TreeViewItem", "")
                                .Replace("Header:", "");
                int count = str.IndexOf("Items.Count:");
                string neW = str.Substring(count);
                str = str.Replace(neW, "").Replace(" ", "");
                db.Database = str;
                db.Table = sender.ToString().Replace("System.Windows.Controls.TreeViewItem", "")
                                    .Replace("Header:", "")
                                    .Replace("Items.Count:0", "")
                                    .Replace(" ", "");
                DataTable table = await db.GetTableAsync(db.Table);
                DataBase_Table.Columns.Clear();
                DataBase_Table.Items.Clear();
                DataBase_Struct.Columns.Clear();
                foreach (DataColumn elem in table.Columns)
                {
                    var col = new DataGridTextColumn();
                    col.Header = elem.ColumnName;
                    col.Binding = new Binding(elem.ColumnName);
                    DataBase_Table.Columns.Add(col);
                }
                foreach (DataColumn elem in table.Columns)
                {
                    var col = new DataGridTextColumn();
                    col.Header = elem.ColumnName;
                    col.Binding = new Binding(elem.ColumnName);
                    DataBase_Struct.Columns.Add(col);
                }
                foreach (DataRow elem in table.Rows)
                    DataBase_Table.Items.Add(table.DefaultView[table.Rows.IndexOf(elem)]);
            }
            catch (Exception ex) 
            {
                Task.Run(() => {
                    MessageBox.Show($"Message:{ex.Message} \nData: {ex.Data} \nStackTrace{ex.StackTrace} \nHelpLink{ex.HelpLink} \nPlese, copy this message and send on developers email", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                });
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Task.Run(() => { history.UpdateConfig(); });
            Task.WaitAll();
        }
        void Update(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
