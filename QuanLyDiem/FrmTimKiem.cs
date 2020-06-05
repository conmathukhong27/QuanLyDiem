using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;





namespace QuanLyDiem
{
    
    public partial class FrmTimKiem : Form
    {
        DataTable tblSV;
        public FrmTimKiem()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FrmTimKiem_Load(object sender, EventArgs e)
        {
            DAO.OpenConnection();

            GridViewTimKiem.DataSource = null;
            DAO.FillDataToCombo("SELECT MaKhoa,TenKhoa FROM Khoa", cmbKhoa,
"MaKhoa", "TenKhoa");
            cmbKhoa.SelectedIndex = -1;
            DAO.FillDataToCombo("SELECT MaChuyenNganh,TenChuyenNganh  FROM ChuyenNganh",
cmbChuyenNganh, "MaChuyenNganh", "TenChuyenNganh");
            cmbChuyenNganh.SelectedIndex = -1;
            DAO.FillDataToCombo("SELECT MaQue,TenQue FROM Que", cmbQue,
"MaQue", "TenQue");
            cmbQue.SelectedIndex = -1;

            DAO.CloseConnection();
        }
        private void LoadDataToGridView()
        {
            string sql = "SELECT a.MaSV, b.Tenhang, a.TenSV, b.Dongiaban, a.Giamgia, 
a.Thanhtien FROM tblChitietHDBan AS a, tblHang AS b WHERE a.MaHDBan = N'" + 
txtMaHDBan.Text + "' AND a.Mahang=b.Mahang";


            SqlDataAdapter adapter = new SqlDataAdapter(sql, DAO.con);
            DataTable table = new DataTable();
            adapter.Fill(table);
            GridViewTimKiem.DataSource = table;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string sql;
            if ((cmbKhoa.Text == "") && (cmbChuyenNganh.Text == "") && (cmbQue.Text ==
""))
            {
                MessageBox.Show("Bạn chưa chọn điều kiện tìm kiếm!!!", "Yêu cầu chọn Khoa,Chuyên ngành,Quê",
MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            sql = "SELECT * FROM SinhVien WHERE 1=1";
            if (cmbKhoa.Text != "")
                sql = sql + " AND TenKhoa Like N'%" + cmbKhoa.SelectedValue + "%'";
            if (cmbChuyenNganh.Text != "")
                sql = sql + " AND TenChuyenNganh Like N'%" + cmbKhoa.SelectedValue + "%'";
            if (cmbQue.Text != "")
                sql = sql + " AND TenQue Like N'%" + cmbQue.SelectedValue + "%'";
            DataTable = DAO.GetDataToTable(sql);
            if (DataTable.Rows.Count == 0)
                MessageBox.Show("Không có bản ghi thỏa mãn điều kiện!!!", "Thông báo",
MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                MessageBox.Show("Có " + tblH.Rows.Count + " bản ghi thỏa mãn điều kiện!!!",
"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            DataGridView.DataSource = tblH;
            ResetValues();

        }
    }
}
