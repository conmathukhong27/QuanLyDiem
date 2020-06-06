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
using COMExcel = Microsoft.Office.Interop.Excel;


namespace QuanLyDiem
{
    public partial class FrmInTKB : Form
    {
        public FrmInTKB()
        {
            InitializeComponent();
        }

        private void FrmInTKB_Load(object sender, EventArgs e)
        {
            DAO.OpenConnection();
            
            DAO.FillDataToCombo("SELECT MaLop FROM Thoi_Khoa_Bieu group by(MaLop)", cmbLop,
"MaLop", "MaLop");
            cmbLop.SelectedIndex = -1;
            
            
                DAO.FillDataToCombo("SELECT HocKy  FROM Thoi_Khoa_Bieu  group by(HocKy) ",
    cmbHocKy, "HocKy", "HocKy");

                cmbHocKy.SelectedIndex = -1;
            
            DAO.CloseConnection();
            
            
        }
        private void LoadDataTKB()
        {
            string str;
            str = "SELECT HocKy FROM Thoi_Khoa_Bieu WHERE MaLop = '" + cmbLop.SelectedValue + "'";
            cmbHocKy.Text = DAO.GetFieldValues(str);

        }

        private void cmbHocKy_TextChanged(object sender, EventArgs e)
        {
           

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn thoát chương trình không?", "Hỏi Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

                this.Close();
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            COMExcel.Application exApp = new COMExcel.Application();
            COMExcel.Workbook exBook; //Trong 1 chương trình Excel có nhiều Workbook
            COMExcel.Worksheet exSheet; //Trong 1 Workbook có nhiều Worksheet
            COMExcel.Range exRange;
            string sql;
            
            DataTable Thoi_Khoa_Bieu;
            exBook = exApp.Workbooks.Add(COMExcel.XlWBATemplate.xlWBATWorksheet);
            exSheet = exBook.Worksheets[1];
            // Định dạng chung
            exRange = exSheet.Cells[1, 1];
            exRange.Range["A1:B1"].Font.Size = 13;
            exRange.Range["A1:B1"].Font.Name = "Times new roman";
            exRange.Range["A1:B1"].Font.Bold = true;
            exRange.Range["A1:B1"].Font.ColorIndex = 5; //Màu xanh da trời
            exRange.Range["A1:A1"].ColumnWidth = 7;
            exRange.Range["B1:B1"].ColumnWidth = 15;
            exRange.Range["A1:B1"].MergeCells = true;
            exRange.Range["A1:B1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A1:B1"].Value = "Học Viện Ngân Hàng";
            
            exRange.Range["C2:E2"].Font.Size = 16;
            exRange.Range["C2:E2"].Font.Name = "Times new roman";
            exRange.Range["C2:E2"].Font.Bold = true;
            exRange.Range["C2:E2"].Font.ColorIndex = 3; //Màu đỏ
            exRange.Range["C2:E2"].MergeCells = true;
            exRange.Range["C2:E2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["C2:E2"].Value = "THỜI KHÓA BIỂU";
            // Biểu diễn thông tin TKB
            sql = "SELECT MaLop, MaMon, HocKy, ThuHoc,CaHoc ,MaPhong  FROM Thoi_Khoa_Bieu  WHERE MaLop = '" + cmbLop.SelectedValue.ToString() + "' AND HocKy = '"+cmbHocKy.SelectedValue.ToString()+"'";
            Thoi_Khoa_Bieu = DAO.GetDataToTable(sql);
            exRange.Range["B6:G12"].Font.Size = 12;
            exRange.Range["B6:G12"].Font.Name = "Times new roman";
            exRange.Range["B6:B6"].Value = "Mã Lớp:";
            exRange.Range["B7:B12"].MergeCells = true;
            exRange.Range["B7:B12"].Value = Thoi_Khoa_Bieu.Rows[0][0].ToString();
            exRange.Range["C6:C6"].Value = "Mã Môn:";
            exRange.Range["C7:C12"].MergeCells = true;
            exRange.Range["C7:C12"].Value = Thoi_Khoa_Bieu.Rows[0][2].ToString();
            exRange.Range["D6:D6"].Value = "Học Kỳ:";
            exRange.Range["D7:D12"].MergeCells = true;
            exRange.Range["D7:D12"].Value = Thoi_Khoa_Bieu.Rows[0][3].ToString();
            exRange.Range["E6:E6"].Value = "Thứ Học:";
            exRange.Range["E7:E12"].MergeCells = true;
            exRange.Range["E7:E12"].Value = Thoi_Khoa_Bieu.Rows[0][4].ToString();
            
        }


    }
}

