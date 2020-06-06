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
        private void ResetValues()
        {
            cmbKhoa.Text = "";
            cmbChuyenNganh.Text = "";
            cmbQue.Text = "";
            
        }
        private void LoadDataToGridView()
        {
            GridViewTimKiem.AllowUserToAddRows = false;
            GridViewTimKiem.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string sql;
            if ((cmbKhoa.Text == "") && (cmbChuyenNganh.Text == "") && (cmbQue.Text == ""))
            {
                MessageBox.Show("Hãy chọn một điều kiện tìm kiếm!!!", "Yêu cầu nhập Khoa,Chuyên Ngành,Quê", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            sql = "select  a.MaSV,a.TenSV,a.MaKhoa,a.MaLop,a.NgaySinh,a.GioiTinh,a.MaQue,a.MaDanToc,a.MaChuyenNganh,a.MaHDT,a.MaChucVu" +
                " from SinhVien a join ChuyenNganh b on a.MaChuyenNganh=b.MaChuyenNganh join Que c on a.MaQue=c.MaQue where 1=1";
            if (cmbKhoa.Text != "")
            {
                sql = sql + " AND a.MaKhoa Like '%" + cmbKhoa.SelectedValue + "%'";
            }
            if (cmbChuyenNganh.Text != "")
            {
                sql = sql + " AND b.MaChuyenNganh Like '%" + cmbChuyenNganh.SelectedValue + "%'";
            }
            if (cmbQue.Text != "")
            {
                sql = sql + " AND c.MaQue Like '%" + cmbQue.SelectedValue + "%'";
            }

            tblSV = DAO.GetDataToTable(sql);
            if (tblSV.Rows.Count == 0)
            {
                MessageBox.Show("Không có bản ghi nào thỏa mãn điều kiện!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Có " + tblSV.Rows.Count + " bản ghi thỏa mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            GridViewTimKiem.DataSource = tblSV;
            LoadDataToGridView();

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn thoát chương trình không?", "Hỏi Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                this.Close();
        }
    }
}
