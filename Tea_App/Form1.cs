using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Tea_App
{
    // не вижу смысла реализовать ту кучу кода что раньше.
    // Так как есть textBox в которм можно самому ввести желаемый запрос.
    // Если необходимо все же прописать запросы то отошлите работу обратно.
    public partial class Form1 : Form
    {
        static string connectionString = @"Data Source=C:\Users\Artemik\source\repos\ConsoleApp13\Tea\database.db; Version=3";
        static SQLiteConnection connection = new SQLiteConnection(connectionString);
        DataSet set = null;
        SQLiteDataAdapter da = null;
        SQLiteCommandBuilder cmd = null;
        public Form1()
        {
            InitializeComponent();
        }  

        private void button1_Click_1(object sender, EventArgs e)
        {
            string query_Tea = "CREATE TABLE Tea(" +
                "Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "NAME NVARCHAR," +
                "Description NVARCHAR," +
                "Amount INTEGER," +
                "Price DECIMAL(10,2)," +
                "CountryId INTEGER," +
                "TypeId INTEGER," +
                "FOREIGN KEY (CountryId)  REFERENCES Country (Id)," +
                "FOREIGN KEY (TypeId)  REFERENCES Type (Id))";

            string query_Country = "CREATE TABLE Country(" +
                "Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "NAME NVARCHAR)";

            string query_Type = "CREATE TABLE Type(" +
                "Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "NAME NVARCHAR)";

            SQLiteCommand command2 = new SQLiteCommand(query_Country, connection);
            SQLiteCommand command3 = new SQLiteCommand(query_Type, connection);
            SQLiteCommand command1 = new SQLiteCommand(query_Tea, connection);
            connection.Open();
            command2.ExecuteNonQuery();
            command3.ExecuteNonQuery();
            command1.ExecuteNonQuery();
            connection.Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            set = new DataSet();
            string query = textBox1.Text;
            da = new SQLiteDataAdapter(query, connection);
            dataGridView1.DataSource = null;
            cmd = new SQLiteCommandBuilder(da);
            da.Fill(set);
            dataGridView1.DataSource = set.Tables[0];
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            da.Update(set);
        }
    }
}
