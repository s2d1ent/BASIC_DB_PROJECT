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
        Dictionary<string, object> Editing = new Dictionary<string, object>();
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
            create_database.Click += CreateDB;
            delete_database.Click += DeleteDB;
            update_database.Click += updateTree;
            DataBase_Table.CellEditEnding += CelLEdit;

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
                    DataBase_Name.Text = db.Database;
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
        //TODO
        async void ChekingTable()
        {
            DataTable tb = new DataTable();
            foreach (DataColumn elem in Tree_Tables_DataBase.Items)
            {
                tb.Columns.Add(elem.ColumnName);
            }
            foreach (DataRow elem in Tree_Tables_DataBase.Items)
            {
                tb.Rows.Add(elem);
            }
        }
        void TableOut(object sender, RoutedEventArgs e)
        {
            lock(new object())
            {
                string parent = ((TreeViewItem)sender).Parent.ToString();
                db.Database = parent.Replace("System.Windows.Controls.TreeViewItem", "")
                                .Replace("Header:", "")
                                .Replace(parent.Substring(parent.IndexOf("Items.Count:")), "")
                                .Replace(" ", "");
                db.Table = sender.ToString()
                                    .Replace("System.Windows.Controls.TreeViewItem", "")
                                    .Replace("Header:", "")
                                    .Replace("Items.Count:0", "")
                                    .Replace(" ", "");
                FillTables(db.GetTable(db.Table));
                DataBase_Name.Text = db.Database;
            }
        }
        // TODO async
        async void TableOutAsync(object sender, RoutedEventArgs e)
        {
            try
            {
                await Task.Run(async()=> {
                    string parent = ((TreeViewItem)sender).Parent.ToString();
                    db.Database = parent.Replace("System.Windows.Controls.TreeViewItem", "")
                                    .Replace("Header:", "")
                                    .Replace(parent.Substring(parent.IndexOf("Items.Count:")), "")
                                    .Replace(" ", "");
                    db.Table = sender.ToString()
                                        .Replace("System.Windows.Controls.TreeViewItem", "")
                                        .Replace("Header:", "")
                                        .Replace("Items.Count:0", "")
                                        .Replace(" ", "");
                });
                DataTable table = await db.GetTableAsync(db.Table);
                FillTables(table);
                DataBase_Name.Text = db.Database;
            }
            catch (Exception ex) 
            {
                Task.Run(() => {
                    MessageBox.Show($"Message:{ex.Message} \nData: {ex.Data} \nStackTrace{ex.StackTrace} \nHelpLink{ex.HelpLink} \nPlese, copy this message and send on developers email", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                });
            }
        }
        void CelLEdit(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var column = e.Column as DataGridBoundColumn;
                if (column != null)
                {
                    var bindingPath = (column.Binding as Binding).Path.Path;
                    if (bindingPath == "Col2")
                    {
                        int rowIndex = e.Row.GetIndex();
                        var el = e.EditingElement as TextBox;
                    }
                }
            }
        }
        private void FillTables(DataTable dt)
        {
            DataBase_Table.Columns.Clear();
            DataBase_Table.Items.Clear();
            DataBase_Struct.Columns.Clear();
            foreach (DataColumn elem in dt.Columns)
            {
                var col = new DataGridTextColumn();
                col.Header = elem.ColumnName;
                col.Binding = new Binding(elem.ColumnName);
                DataBase_Table.Columns.Add(col);
            }
            foreach (DataColumn elem in dt.Columns)
            {
                var col = new DataGridTextColumn();
                col.Header = elem.ColumnName;
                col.Binding = new Binding(elem.ColumnName);
                DataBase_Struct.Columns.Add(col);
            }
            foreach (DataRow elem in dt.Rows)
            {
                DataBase_Table.Items.Add(dt.DefaultView[dt.Rows.IndexOf(elem)]);
            }
            foreach (DataColumn elem in dt.Columns)
            {
                var col = new DataGridTextColumn();
                col.Header = elem.ColumnName;
                col.Binding = new Binding(elem.ColumnName);
                Editing.Add(col.Header.ToString(), null);
            }
        }
        void CellSellected(object sender, EventArgs e)
        {
            MessageBox.Show($"{sender}");
        }
        void CreateDB(object sender, EventArgs e)
        {
            CreateDB cdb = new CreateDB();
            cdb.db = db;
            cdb.Show();
        }
        void DeleteDB(object sender, EventArgs e)
        {
            DeleteDB cdb = new DeleteDB();
            cdb.db = db;
            cdb.Show();
        }
        void updateTree(object sender, EventArgs e)
        {
            try
            {
                Tree_Tables_DataBase.Items.Clear();
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
                    DataBase_Name.Text = db.Database;
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
            catch (Exception ex)
            {
                Task.Run(() => {
                    MessageBox.Show($"Message:{ex.Message} \nData: {ex.Data} \nStackTrace{ex.StackTrace} \nHelpLink{ex.HelpLink} \nPlese, copy this message and send on developers email", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                });
            }
        }
        async void CreateDBAsync(object sender, EventArgs e)
        {
            await Task.Run(()=> { CreateDB(sender, e); });
        }
        async void DeleteDBAsync(object sender, EventArgs e)
        {
            await Task.Run(() => { DeleteDB(sender, e); });
        }
        void test(object sender, EventArgs e)
        {
            //MessageBox.Show($"{sender}");
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Task.Run(() => { history.UpdateConfig(); });
            Task.WaitAll();
        }

        void Update(object sender, RoutedEventArgs e)
        {
            var a = DataBase_Table.SelectedItem;
            /*foreach(var row in DataBase_Table.Sele)
            {

            }*/
            MessageBox.Show($"{DataBase_Table}");
        }
    }
}
