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
    public partial class FrmThoiKhoaBieu : Form
    {
        public FrmThoiKhoaBieu()
        {
            InitializeComponent();
        }

        private void FrmThoiKhoaBieu_Load(object sender, EventArgs e)
        {
            DAO.OpenConnection();
            cmbLop.Enabled = false;
            cmbMon.Enabled = false;
            cmbPhong.Enabled = false;
            btnHuy.Enabled = false;
            btnLuu.Enabled = false;
            LoadDataToGridView();
            DAO.FillDataToCombo("SELECT MaLop FROM Lop", cmbLop,
"MaLop", "MaLop");
            cmbLop.SelectedIndex = -1;
            DAO.FillDataToCombo("SELECT MaPhong  FROM PhongHoc",
cmbPhong, "MaPhong", "MaPhong");
            cmbPhong.SelectedIndex = -1;
            DAO.FillDataToCombo("SELECT MaMon FROM MonHoc", cmbMon,
"MaMon", "MaMon");
            cmbMon.SelectedIndex = -1;

            DAO.CloseConnection();
        }
        private void LoadDataToGridView()
        {
            string sql = "select * from Thoi_Khoa_Bieu";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, DAO.con);
            DataTable table = new DataTable();
            adapter.Fill(table);
            GridViewTKB.DataSource = table;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            cmbLop.Enabled = true;
            cmbMon.Enabled = true;
            cmbPhong.Enabled = true;
            cmbLop.SelectedIndex = -1;
            cmbMon.SelectedIndex = -1;
            cmbPhong.SelectedIndex = -1;
            btnHuy.Enabled = true;
            btnLuu.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnThoat.Enabled = true;

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            //Kiem tra DL
            //Các trường không được trống

            if (cmbLop.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn chưa chọn Lớp!");
                return;
            }
            if (cmbPhong.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn chưa chọn Phòng!");
                
                return;
            }
            if (cmbMon.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn chưa chọn Môn học!");
                
                return;
            }
            int strHK=-1;

            if (rdohk1.Checked == true)
                strHK = Convert.ToInt32(rdohk1.Text);
                if (rdohk2.Checked == true)
                strHK = Convert.ToInt32(rdohk2.Text);
            if (rdohk3.Checked == true)
                strHK = Convert.ToInt32(rdohk3.Text);
            if (rdohk4.Checked == true)
                strHK = Convert.ToInt32(rdohk4.Text);
            if (rdohk5.Checked == true)
                strHK = Convert.ToInt32(rdoca5.Text);
            if (rdohk6.Checked == true)
                strHK = Convert.ToInt32(rdohk6.Text);
            if (rdohk7.Checked == true)
                strHK = Convert.ToInt32(rdohk7.Text);
            if (rdohk8.Checked == true)
                strHK = Convert.ToInt32(rdohk8.Text);
            if (strHK ==-1)
                MessageBox.Show("Bạn chưa chọn Học Kỳ!");
            string strThu = "";
            if (chk2.Checked == true)
                strThu += chk2.Text + "_";
            if (chk3.Checked == true)
                strThu += chk3.Text + "_";
            if (chk4.Checked == true)
                strThu += chk4.Text + "_";
            if (chk5.Checked == true)
                strThu += chk5.Text + "_";
            if (chk6.Checked == true)
                strThu += chk6.Text + "_";
            if (chk7.Checked == true)
                strThu += chk7.Text + "";
            if (strThu == "")
                MessageBox.Show("Bạn chưa chọn Thứ học!");
            int strCa=-1 ;
            if (rdoca1.Checked == true)
                strCa = Convert.ToInt32(rdoca1.Text);
            if (rdoca2.Checked == true)
                strCa = Convert.ToInt32(rdoca2.Text);
            if (rdoca3.Checked == true)
                strCa = Convert.ToInt32(rdoca3.Text);
            if (rdoca4.Checked == true)
                strCa = Convert.ToInt32(rdoca4.Text);
            if (rdoca5.Checked == true)
                strCa = Convert.ToInt32(rdoca5.Text);
            if (strCa ==-1 )
                MessageBox.Show("Bạn chưa chọn Ca học!");


            string sql = "select * from Thoi_Khoa_Bieu where MaLop='" + cmbLop.SelectedValue.ToString() + "' and MaMon='" + cmbMon.SelectedValue.ToString() + "'" ;

            DAO.OpenConnection();
            if (DAO.CheckKeyExist(sql))
            {
                MessageBox.Show("Mã Lớp Và Mã Môn đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DAO.CloseConnection();
                cmbLop.Focus();
                cmbMon.Focus();
                return;
            }
            else
            {



                sql = "insert into Thoi_Khoa_Bieu (MaLop,MaMon,HocKy,ThuHoc,CaHoc,MaPhong) " +
                    " values ('" + cmbLop.SelectedValue.ToString() + "','" + cmbMon.SelectedValue.ToString() + "'," + strHK.ToString() + ",N'" + strThu.ToString() + "'," + strCa.ToString() + ",'" + cmbPhong.SelectedValue.ToString() + "')";
                SqlCommand cmd = new SqlCommand(sql, DAO.con);
                cmd.ExecuteNonQuery();
                DAO.CloseConnection();
                LoadDataToGridView();
            }

            }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xóa?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                string sql = "delete from Thoi_Khoa_Bieu where MaLop = '" + cmbLop.SelectedValue.ToString() + "' and MaMon='"+ cmbMon.SelectedValue.ToString()+ "'";
                DAO.OpenConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = DAO.con;
                cmd.ExecuteNonQuery();
                DAO.CloseConnection();
                LoadDataToGridView();
            }

        }

        private void GridViewTKB_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           string ma = GridViewTKB.CurrentRow.Cells["MaLop"].Value.ToString();
            cmbLop.Text = DAO.GetFieldValues("select MaLop from Lop where MaLop = '" + ma + "'");
           string na= GridViewTKB.CurrentRow.Cells["MaMon"].Value.ToString();
            cmbMon.Text = DAO.GetFieldValues("select MaMon from MonHoc where MaMon = '" + na + "'");
           string la = GridViewTKB.CurrentRow.Cells["MaPhong"].Value.ToString();
            cmbPhong.Text = DAO.GetFieldValues("select MaPhong from PhongHoc where MaPhong = '" + la + "'");



        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            cmbLop.Enabled = true;
            cmbMon.Enabled = true;
            cmbPhong.Enabled = true;
            int strHK = -1;

            if (rdohk1.Checked == true)
                strHK = Convert.ToInt32(rdohk1.Text);
            if (rdohk2.Checked == true)
                strHK = Convert.ToInt32(rdohk2.Text);
            if (rdohk3.Checked == true)
                strHK = Convert.ToInt32(rdohk3.Text);
            if (rdohk4.Checked == true)
                strHK = Convert.ToInt32(rdohk4.Text);
            if (rdohk5.Checked == true)
                strHK = Convert.ToInt32(rdoca5.Text);
            if (rdohk6.Checked == true)
                strHK = Convert.ToInt32(rdohk6.Text);
            if (rdohk7.Checked == true)
                strHK = Convert.ToInt32(rdohk7.Text);
            if (rdohk8.Checked == true)
                strHK = Convert.ToInt32(rdohk8.Text);
            
                
            string strThu = "";
            if (chk2.Checked == true)
                strThu += chk2.Text + "_";
            if (chk3.Checked == true)
                strThu += chk3.Text + "_";
            if (chk4.Checked == true)
                strThu += chk4.Text + "_";
            if (chk5.Checked == true)
                strThu += chk5.Text + "_";
            if (chk6.Checked == true)
                strThu += chk6.Text + "_";
            if (chk7.Checked == true)
                strThu += chk7.Text + "";
           
            int strCa = -1;
            if (rdoca1.Checked == true)
                strCa = Convert.ToInt32(rdoca1.Text);
            if (rdoca2.Checked == true)
                strCa = Convert.ToInt32(rdoca2.Text);
            if (rdoca3.Checked == true)
                strCa = Convert.ToInt32(rdoca3.Text);
            if (rdoca4.Checked == true)
                strCa = Convert.ToInt32(rdoca4.Text);
            if (rdoca5.Checked == true)
                strCa = Convert.ToInt32(rdoca5.Text);
            

            if (cmbLop.SelectedValue.ToString() == "")
            {
                MessageBox.Show("Bạn chưa chọn Lớp!", "Thông báo",
MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbLop.Focus();
                return;
                
            }
            if (cmbMon.SelectedValue.ToString() == "")
            {
                MessageBox.Show("Bạn chưa chọn Môn!", "Thông báo",
MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbMon.Focus();
                return;
            }
            if (cmbPhong.SelectedValue.ToString() == "")
            {
                MessageBox.Show("Bạn phải chọn Phòng", "Thông báo", MessageBoxButtons.OK,
MessageBoxIcon.Warning);
                cmbPhong.Focus();
                return;
            }
            if (strHK == -1)
            {
                MessageBox.Show("Bạn phải chọn Học Kỳ", "Thông báo",
MessageBoxButtons.OK, MessageBoxIcon.Warning);
                
                return;
            }
            if (strCa == -1)
            {
                MessageBox.Show("Bạn phải chọn Ca học", "Thông báo",
MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }
            if (strThu == "")
            {
                MessageBox.Show("Bạn phải chọn Thứ học", "Thông báo",
MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            string sql = "update Thoi_Khoa_Bieu set MaPhong = '" + cmbPhong.SelectedValue.ToString() + "', HocKy=" + strHK + ", ThuHoc=N'" +strThu.ToString() + "',CaHoc=" + strCa + "  where MaLop = '" + cmbLop.SelectedValue.ToString() + "' and MaMon='" + cmbMon.SelectedValue.ToString() +"'";

            DAO.OpenConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.Connection = DAO.con;
            cmd.ExecuteNonQuery();
            DAO.CloseConnection();
            LoadDataToGridView();

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            cmbLop.SelectedIndex = -1;
            cmbMon.SelectedIndex = -1;
            cmbPhong.SelectedIndex = -1;

            btnHuy.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
        }
    

