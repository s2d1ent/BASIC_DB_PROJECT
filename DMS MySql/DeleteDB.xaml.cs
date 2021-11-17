using System;
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
    /// Логика взаимодействия для DeleteDB.xaml
    /// </summary>
    public partial class DeleteDB : Window
    {
        public DataBase db = new DataBase();
        public DeleteDB()
        {
            InitializeComponent();
            Create.Click += CreateF;
        }
        void CreateF(object sender, EventArgs e)
        {
            string name = Name_db.Text.Replace(" ", "");
            if (name.Length == 0)
            {
                Task.Run(() => {
                    MessageBox.Show("You not enter name db", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                });
                return;
            }
            db.DeleteDataBase(name);
        }
    }
}
