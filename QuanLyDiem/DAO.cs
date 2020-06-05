using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Windows.Forms;
using System.Windows;
using System.Data;

namespace QuanLyDiem
{
    class DAO
    {
        public static SqlConnection con;
        public static string connectionString = "Data Source=welcome-pc\\sqlexpress;Initial Catalog=QuanLyDiem;Integrated Security=True";
        public static void OpenConnection()
        {
            con = new SqlConnection();
            con.ConnectionString = connectionString;
            if (con.State == System.Data.ConnectionState.Closed)
                try
                {
                    con.Open();
                    //MessageBox.Show("Mo ket noi thanh cong");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
        }

        public static void CloseConnection()
        {
            if (con.State == System.Data.ConnectionState.Open)
                try
                {
                    con.Close();
                    //MessageBox.Show("Dong ket noi thanh cong");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
        }

        public static DataTable GetDataToTable(string sql)
        {
            SqlDataAdapter Mydata = new SqlDataAdapter(sql, DAO.con);
            DataTable table = new DataTable();
            Mydata.Fill(table);
            return table;
        }

        public static bool CheckKeyExist(string sql)
        {
            bool kq = false;

            SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
            DataTable tbl = new DataTable();
            adapter.Fill(tbl);
            if (tbl.Rows.Count > 0)
            {
                kq = true;
            }
            return kq;
        }
        public static void FillDataToCombo(string sql, ComboBox combo, string ValueField, string DisplayField)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
            DataTable mytable = new DataTable();
            adapter.Fill(mytable);
            combo.DataSource = mytable;
            combo.ValueMember = ValueField;
            combo.DisplayMember = DisplayField;
        }
        public static string GetFieldValues(string sql)
        {
            DAO.OpenConnection();
            string ma = "";
            SqlCommand cmd = new SqlCommand(sql, DAO.con);
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ma = reader.GetValue(0).ToString();
            }
            reader.Close();
            return ma;
        }


    }
}
