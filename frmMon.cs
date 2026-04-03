using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace QUANLYNHAHANG
{
    public partial class frmMon : Form
    {
        string connectionString = @"Data source=.;Initial Catalog=QLNHT;Integrated Security=True";

        DataTable gioHang = new DataTable();
        DataTable dtHienThi = new DataTable();
        Button currentCategoryButton = null;
        string maBan;
        string maHoaDon;

        public frmMon(string maBan, string maHoaDon)
        {
            InitializeComponent();
            this.maBan = maBan;
            this.maHoaDon = maHoaDon;
        }

        
        void StyleDefault(Button btn)
        {
            btn.BackColor = Color.White;
            btn.ForeColor = Color.Black;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 2;
            btn.FlatAppearance.BorderColor = Color.FromArgb(33, 150, 243);
        }
        void StyleActive(Button btn)
        {
            btn.BackColor = Color.FromArgb(33, 150, 243);
            btn.ForeColor = Color.White;
        }
        void SetActiveCategory(Button btn)
        {
            if (currentCategoryButton != null)
                StyleDefault(currentCategoryButton);

            StyleActive(btn);
            currentCategoryButton = btn;
        }
        private void frmMon_Load(object sender, EventArgs e)
        {
            // style tất cả
            StyleDefault(btnKhaiVi);
            StyleDefault(btnComMi);
            StyleDefault(btnMonChinh);
            StyleDefault(btnLau);
            StyleDefault(btnTrangMieng);
            StyleDefault(btnDoUong);

            // chọn mặc định
            SetActiveCategory(btnKhaiVi);
            TaoGioHang();
            TaoBangHienThi();
            LoadMon("DMKV");
            LoadHoaDon();
        }

        // =============================
        // LOAD MÓN
        // =============================
        void LoadMon(string maDanhMuc)
        {
            flpMon.Controls.Clear();

            int panelWidth = (flpMon.Width / 3) - 15;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_LayMonTheoDanhMuc", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaDanhMuc", maDanhMuc);

                SqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    Panel pnl = new Panel();
                    pnl.Width = panelWidth;
                    pnl.Height = 200;
                    pnl.Margin = new Padding(5);
                    pnl.BackColor = Color.White;
                    pnl.BorderStyle = BorderStyle.FixedSingle;

                    PictureBox pic = new PictureBox();
                    pic.Width = pnl.Width - 10;
                    pic.Height = 110;
                    pic.Top = 5;
                    pic.Left = 5;
                    pic.SizeMode = PictureBoxSizeMode.Zoom;

                    string imgPath = rd["HinhAnh"].ToString();

                    try
                    {
                        if (!string.IsNullOrEmpty(imgPath) && System.IO.File.Exists(imgPath))
                            pic.Image = Image.FromFile(imgPath);
                    }
                    catch { }

                    Label lbl = new Label();
                    lbl.Width = pnl.Width - 10;
                    lbl.Height = 70;
                    lbl.Top = 120;
                    lbl.Left = 5;
                    lbl.TextAlign = ContentAlignment.MiddleCenter;
                    lbl.Font = new Font("Segoe UI", 10, FontStyle.Bold);

                    lbl.Text = rd["TenMon"] + "\n" +
                               Convert.ToDecimal(rd["GiaBan"]).ToString("N0") + "đ";

                    string maMon = rd["MaMon"].ToString();

                    pnl.Tag = maMon;
                    pic.Tag = maMon;
                    lbl.Tag = maMon;

                    pnl.Click += BtnMon_Click;
                    pic.Click += BtnMon_Click;
                    lbl.Click += BtnMon_Click;

                    pnl.Controls.Add(pic);
                    pnl.Controls.Add(lbl);

                    flpMon.Controls.Add(pnl);
                }
            }
        }

        private void BtnMon_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(maHoaDon))
            {
                MessageBox.Show("Chưa có hóa đơn!");
                return;
            }

            string maMon = ((Control)sender).Tag.ToString();

            string tenMon = "";
            decimal gia = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_LayThongTinMon", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaMon", maMon);

                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    tenMon = rd["TenMon"].ToString();
                    gia = Convert.ToDecimal(rd["GiaBan"]);
                }
            }

            DataRow row = gioHang.AsEnumerable()
                .FirstOrDefault(r => r["MaMon"].ToString() == maMon);

            if (row != null)
                row["SoLuong"] = Convert.ToInt32(row["SoLuong"]) + 1;
            else
                gioHang.Rows.Add(maMon, tenMon, 1, gia);

            LoadHoaDon();
        }

        // =============================
        // LOAD HÓA ĐƠN
        // =============================
        void LoadHoaDon()
        {
            dtHienThi.Clear();
            dgvHoaDon.DataSource = null;

            decimal tong = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_LayChiTietHoaDon", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaHoaDon", maHoaDon);

                SqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    decimal thanhTien = Convert.ToDecimal(rd["ThanhTien"]);

                    dtHienThi.Rows.Add(
                        rd["MaMon"],
                        rd["TenMon"],
                        rd["SoLuong"],
                        rd["DonGia"],
                        thanhTien,
                        "OLD"
                    );

                    tong += thanhTien;
                }
            }

            foreach (DataRow row in gioHang.Rows)
            {
                decimal thanhTien = Convert.ToDecimal(row["ThanhTien"]);

                dtHienThi.Rows.Add(
                    row["MaMon"],
                    row["TenMon"],
                    row["SoLuong"],
                    row["DonGia"],
                    thanhTien,
                    "NEW"
                );

                tong += thanhTien;
            }

            dgvHoaDon.DataSource = dtHienThi;
            lblTongTien.Text = "Tổng tiền: " + tong.ToString("N0") + "đ";
        }

        // =============================
        // GIỎ HÀNG
        // =============================
        void TaoGioHang()
        {
            gioHang.Columns.Clear();

            gioHang.Columns.Add("MaMon");
            gioHang.Columns.Add("TenMon");
            gioHang.Columns.Add("SoLuong", typeof(int));
            gioHang.Columns.Add("DonGia", typeof(decimal));
            gioHang.Columns.Add("ThanhTien", typeof(decimal), "SoLuong * DonGia");
        }

        void TaoBangHienThi()
        {
            dtHienThi.Columns.Clear();

            dtHienThi.Columns.Add("MaMon");
            dtHienThi.Columns.Add("TenMon");
            dtHienThi.Columns.Add("SoLuong", typeof(int));
            dtHienThi.Columns.Add("DonGia", typeof(decimal));
            dtHienThi.Columns.Add("ThanhTien", typeof(decimal));
            dtHienThi.Columns.Add("TrangThai");

            dgvHoaDon.DataSource = dtHienThi;
        }

        // =============================
        // ORDER
        // =============================
        private void btnOrder_Click(object sender, EventArgs e)
        {
            if (gioHang.Rows.Count == 0)
            {
                MessageBox.Show("Chưa có món!");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                foreach (DataRow row in gioHang.Rows)
                {
                    SqlCommand cmd = new SqlCommand("sp_ThemMon", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                    cmd.Parameters.AddWithValue("@MaMon", row["MaMon"]);
                    cmd.Parameters.AddWithValue("@SoLuong", row["SoLuong"]);

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Order thành công!");

            gioHang.Clear();
            LoadHoaDon();
        }

        // =============================
        // XÓA MÓN
        // =============================
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvHoaDon.CurrentRow == null)
            {
                MessageBox.Show("Chọn món!");
                return;
            }

            string maMon = dgvHoaDon.CurrentRow.Cells["MaMon"].Value.ToString();
            string trangThai = dgvHoaDon.CurrentRow.Cells["TrangThai"].Value.ToString();

            if (trangThai == "NEW")
            {
                DataRow row = gioHang.AsEnumerable()
                    .FirstOrDefault(r => r["MaMon"].ToString() == maMon);

                if (row != null)
                {
                    int sl = Convert.ToInt32(row["SoLuong"]);
                    if (sl > 1) row["SoLuong"] = sl - 1;
                    else gioHang.Rows.Remove(row);
                }
            }
            else
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("sp_XoaMon", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                    cmd.Parameters.AddWithValue("@MaMon", maMon);

                    cmd.ExecuteNonQuery();
                }
            }

            LoadHoaDon();
        }

        // =============================
        // THANH TOÁN
        // =============================
        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(maHoaDon))
            {
                MessageBox.Show("Chưa có hóa đơn!");
                return;
            }

            decimal tongTien = TinhTongTienHienTai(); // 👉 lấy tổng hiện tại

            frmThanhToan f = new frmThanhToan(maHoaDon, tongTien);
            f.ShowDialog();

            this.Close();
        }
        decimal TinhTongTienHienTai()
        {
            decimal tong = 0;

            foreach (DataRow row in dtHienThi.Rows)
            {
                tong += Convert.ToDecimal(row["ThanhTien"]);
            }

            return tong;
        }

        private void btnKVBan_Click(object sender, EventArgs e)
        {
            SetActiveCategory(btnKhaiVi);
            this.Close();
        }

        private void btnComMi_Click(object sender, EventArgs e)
        {
            SetActiveCategory(btnComMi);
            LoadMon("DMCM");
        }

        private void btnMonChinh_Click(object sender, EventArgs e)
        {
            SetActiveCategory(btnMonChinh);
            LoadMon("DMMC");
        }

        private void btnLau_Click(object sender, EventArgs e)
        {
            SetActiveCategory(btnLau);
            LoadMon("DMML");
        }

        private void btnTrangMieng_Click(object sender, EventArgs e)
        {
            SetActiveCategory(btnTrangMieng);
            LoadMon("DMTM");
        }

        private void btnDoUong_Click(object sender, EventArgs e)
        {
            SetActiveCategory(btnDoUong);
            LoadMon("DMDU");
        }

        private void btnKhaiVi_Click(object sender, EventArgs e)
        {

            SetActiveCategory(btnKhaiVi);
            LoadMon("DMKV");
        }
    }
}