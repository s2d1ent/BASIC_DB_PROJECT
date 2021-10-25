using System;
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
            //MessageBox.Show($"{db.Host} - {db.Username} - {db.Password} - {db.Port} - {db.Database}", "WS");
            List<string> databases = db.GetDatabases();
            for (var i = 0; i < databases.Count; i++)
            {
                TreeViewItem tree_database = new TreeViewItem();
                tree_database.Header = databases[i];
               // tree_database.MouseLeftButtonDown += test;
                Tree_Tables_DataBase.Items.Add(tree_database);
                List<string> tree_tables = db.GetTables(databases[i]);
                for (var j = 0; j < tree_tables.Count; j++)
                {
                    TreeViewItem tree_tables_elem = new TreeViewItem();
                   // tree_tables_elem.MouseLeftButtonDown += test;
                    tree_tables_elem.Header = tree_tables[j];
                    tree_database.Items.Add(tree_tables_elem);
                }
            }
            DataTable u = db.GetTable("wp_terms");
            foreach (DataColumn elem in u.Columns)
            {
                var col = new DataGridTextColumn();
                col.Header = elem.ColumnName;
                col.Binding = new Binding(elem.ColumnName);
                DataBase_Struct.Columns.Add(col);
            }
            foreach (DataColumn elem in u.Columns)
            {
                var col = new DataGridTextColumn();
                col.Header = elem.ColumnName;
                col.Binding = new Binding(elem.ColumnName);
                DataBase_Table.Columns.Add(col);
            }
            foreach (DataRow elem in u.Rows)
            {
                DataBase_Table.Items.Add(u.DefaultView[u.Rows.IndexOf(elem)]);
            }
        }
    }
}
