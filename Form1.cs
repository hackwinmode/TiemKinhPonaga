using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace TiemKinhPonaga
{
    public partial class mainForm : Form
    {
        static int Id = 0;
        static string HovaTen;
        public mainForm()
        {
            InitializeComponent();

            dataviewKhachHang.MultiSelect = true;
            dataviewKhachHang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            XulyDuLieu.createTable();
            loadData("Select id, hovaten as [Họ và Tên], sodienthoai as [Số điện thoại], " +
                     "SmatTrai as [S Trái], AmatTrai as [A Trái], CmatTrai as [C Trái]," +
                     "SmatPhai as [S Phải], AmatPhai as [A Phải], CmatPhai as [C Phải], addMat from tbl_khachhang");
        }

        public void loadData(string SQL)
        {
            DataSet ds = XulyDuLieu.getData(SQL);
            dataviewKhachHang.DataSource = ds.Tables[0];
            dataviewKhachHang.Columns[0].Visible = false;
            dataviewKhachHang.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataviewKhachHang.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataviewKhachHang.Columns[3].Visible = false;
            dataviewKhachHang.Columns[4].Visible = false;
            dataviewKhachHang.Columns[5].Visible = false;
            dataviewKhachHang.Columns[6].Visible = false;
            dataviewKhachHang.Columns[7].Visible = false;
            dataviewKhachHang.Columns[8].Visible = false;
            dataviewKhachHang.Columns[9].Visible = false;

        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            
            String name = txtTimTen.Text.ToString();
            String SDT = txtTimSDT.Text.ToString();
            if (!String.IsNullOrEmpty(name))
            {
                String SQL = String.Format("Select id, hovaten as [Họ và Tên], sodienthoai as [Số điện thoại], " +
                            "SmatTrai as [S Trái], AmatTrai as [A Trái], CmatTrai as [C Trái], " +
                            "SmatPhai as [S Phải], AmatPhai as [A Phải], CmatPhai as [C Phải], addMat " +
                            "from tbl_khachhang WHERE hovaten LIKE '%{0}%'", name);
                loadData(SQL);
            }
            else if (!String.IsNullOrEmpty(SDT))
            {
                String SQL = String.Format("Select id, hovaten as [Họ và Tên], sodienthoai as [Số điện thoại], " +
            "SmatTrai as [S Trái], AmatTrai as [A Trái], CmatTrai as [C Trái], " +
            "SmatPhai as [S Phải], AmatPhai as [A Phải], CmatPhai as [C Phải], addMat " +
            "from tbl_khachhang WHERE sodienthoai LIKE '%{0}%' ", SDT);
                loadData(SQL);
            }

        }

        private void btnSuaThongtin_Click(object sender, EventArgs e)
        {
            if (Id > 0)
            {
                txtThongTinHoTen.ReadOnly = false;
                txtThongTinSoDT.ReadOnly = false;

                txtCmatTrai.ReadOnly = false;
                txtSmatTrai.ReadOnly = false;
                txtAmatTrai.ReadOnly = false;

                txtCmatPhai.ReadOnly = false;
                txtSmatPhai.ReadOnly = false;
                txtAmatPhai.ReadOnly = false;
                txtADD.ReadOnly = false;

                btnSuaThongtin.Visible = false;
                btnXoa.Visible = false;
                btnXacnhan.Visible = false;
                btnThemThongTin.Visible = false;
                btnLuuthaydoi.Visible = true;
            }
            else
            {
                string message = "Bạn chưa chọn khách hàng";
                string caption = "Sửa thông tin khách hàng!";
                MessageBoxButtons buttons = MessageBoxButtons.OK;

                MessageBox.Show(message, caption, buttons);
            }

        }

        private void btnLuuthaydoi_Click(object sender, EventArgs e)
        {


            if (txtThongTinHoTen.Text != "")
            {
                string hovaten = txtThongTinHoTen.Text;
                string Sdt = txtThongTinSoDT.Text;

                string SmatTrai = txtSmatTrai.Text;
                string AmatTrai = txtAmatTrai.Text;
                string CmatTrai = txtCmatTrai.Text;

                string SmatPhai = txtSmatPhai.Text;
                string AmatPhai = txtAmatPhai.Text;
                string CmatPhai = txtCmatPhai.Text;
                string Add = txtADD.Text;

                string message = "Bạn có muốn lưu lại thay đổi?";
                string caption = "Thay đổi thông tin khách hàng!";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;

                result = MessageBox.Show(message, caption, buttons);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    XulyDuLieu.updateTable(hovaten, Sdt, SmatTrai, AmatTrai, CmatTrai, SmatPhai, AmatPhai, CmatPhai, Add, Id);

                    LamMoiGroup();

                    Id = 0;

                    btnSuaThongtin.Visible = true;
                    btnThemThongTin.Visible = true;
                    btnXoa.Visible = true;

                    btnXacnhan.Visible = false;
                    btnLuuthaydoi.Visible = false;
                }
            }
            else
            {
                MessageBox.Show("Đừng để trống tên khách hàng");
            }
            loadData("Select id, hovaten as [Họ và Tên], sodienthoai as [Số điện thoại], " +
                     "SmatTrai as [S Trái], AmatTrai as [A Trái], CmatTrai as [C Trái]," +
                     "SmatPhai as [S Phải], AmatPhai as [A Phải], CmatPhai as [C Phải], addMat from tbl_khachhang");
        }

        private void btnHuysuadoi_Click(object sender, EventArgs e)
        {
            LamMoiGroup();

            btnLuuthaydoi.Visible = false;
            btnXacnhan.Visible = false;

            btnThemThongTin.Visible = true;
            btnSuaThongtin.Visible = true;
            btnXoa.Visible = true;

            Id = 0;
        }

        private void btnThemThongTin_Click(object sender, EventArgs e)
        {
            foreach (TextBox childControl in grbThiLuc.Controls.OfType<TextBox>())
            {
                childControl.ResetText();
                childControl.ReadOnly = false;
            }
            foreach (TextBox childControl in grbThongtin.Controls.OfType<TextBox>())
            {
                childControl.ResetText();
                childControl.ReadOnly = false;
            }

            btnLuuthaydoi.Visible = false;
            btnSuaThongtin.Visible = false;
            btnThemThongTin.Visible = false;

            btnXoa.Visible = false;
            btnXacnhan.Visible = true;
        }

        private void btnXacnhan_Click(object sender, EventArgs e)
        {
            if (txtThongTinHoTen.Text != "") {
                string hovaten = txtThongTinHoTen.Text;
                string Sdt = txtThongTinSoDT.Text;

                string SmatTrai = txtSmatTrai.Text;
                string AmatTrai = txtAmatTrai.Text;
                string CmatTrai = txtCmatTrai.Text;

                string SmatPhai = txtSmatPhai.Text;
                string AmatPhai = txtAmatPhai.Text;
                string CmatPhai = txtCmatPhai.Text;
                string Add = txtADD.Text;

                string message = "Bạn có muốn thêm khách hàng này vào cơ sở dữ liệu";
                string caption = "Thêm khách hàng!";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;

                result = MessageBox.Show(message, caption, buttons);

                if (result == System.Windows.Forms.DialogResult.Yes)
                {

                    XulyDuLieu.insertTable(hovaten, Sdt, SmatTrai, AmatTrai, CmatTrai, SmatPhai, AmatPhai, CmatPhai, Add);
                    btnThemThongTin.Visible = true;
                    btnSuaThongtin.Visible = true;
                    btnXoa.Visible = true;

                    btnXacnhan.Visible = false;

                    LamMoiGroup();

                    Id = 0;
                }
            }
            else
            {
                MessageBox.Show("Đừng để trống tên khách hàng");
            }
            loadData("Select id, hovaten as [Họ và Tên], sodienthoai as [Số điện thoại], " +
                     "SmatTrai as [S Trái], AmatTrai as [A Trái], CmatTrai as [C Trái]," +
                     "SmatPhai as [S Phải], AmatPhai as [A Phải], CmatPhai as [C Phải], addMat from tbl_khachhang");
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dataviewKhachHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtThongTinHoTen.Text = dataviewKhachHang.CurrentRow.Cells[1].Value.ToString();
            txtThongTinSoDT.Text = dataviewKhachHang.CurrentRow.Cells[2].Value.ToString();

            txtSmatTrai.Text = dataviewKhachHang.CurrentRow.Cells[3].Value.ToString();
            txtAmatTrai.Text = dataviewKhachHang.CurrentRow.Cells[4].Value.ToString();
            txtCmatTrai.Text = dataviewKhachHang.CurrentRow.Cells[5].Value.ToString();

            txtSmatPhai.Text = dataviewKhachHang.CurrentRow.Cells[6].Value.ToString();
            txtAmatPhai.Text = dataviewKhachHang.CurrentRow.Cells[7].Value.ToString();
            txtCmatPhai.Text = dataviewKhachHang.CurrentRow.Cells[8].Value.ToString();
            txtADD.Text = dataviewKhachHang.CurrentRow.Cells[9].Value.ToString();

            int.TryParse(dataviewKhachHang.CurrentRow.Cells[0].Value.ToString(), out Id);
            HovaTen = txtThongTinHoTen.Text;
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {

            loadData("Select id, hovaten as [Họ và Tên], sodienthoai as [Số điện thoại], " +
                     "SmatTrai as [S Trái], AmatTrai as [A Trái], CmatTrai as [C Trái]," +
                     "SmatPhai as [S Phải], AmatPhai as [A Phải], CmatPhai as [C Phải], addMat from tbl_khachhang");

            txtTimSDT.Text = "";
            txtTimTen.Text = "";

            LamMoiGroup();

            Id = 0;
        }

        private void LamMoiGroup()
        {
            foreach (TextBox childControl in grbThiLuc.Controls.OfType<TextBox>())
            {
                childControl.ResetText();
                childControl.ReadOnly = true;
            }
            foreach (TextBox childControl in grbThongtin.Controls.OfType<TextBox>())
            {
                childControl.ResetText();
                childControl.ReadOnly = true;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (Id > 0)
            {
                string message = string.Format("Bạn có muốn xóa khách hàng {0} khỏi cơ sở dữ liệu", HovaTen);
                string caption = "Xóa khách hàng!";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;

                result = MessageBox.Show(message, caption, buttons);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    XulyDuLieu.deleteRecord(Id);
                    loadData("Select id, hovaten as [Họ và Tên], sodienthoai as [Số điện thoại], " +
                            "SmatTrai as [S Trái], AmatTrai as [A Trái], CmatTrai as [C Trái]," +
                            "SmatPhai as [S Phải], AmatPhai as [A Phải], CmatPhai as [C Phải], addMat from tbl_khachhang");
                    HovaTen = "";
                }
            }
            else
            {
                string message = "Bạn chưa chọn khách hàng";
                string caption = "Xóa khách hàng!";
                MessageBoxButtons buttons = MessageBoxButtons.OK;

                MessageBox.Show(message, caption, buttons);
            }

            
        }

    }
}
