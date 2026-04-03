using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using DataTable = System.Data.DataTable;

namespace QUANLYNHAHANG
{
    public partial class frmKhacHang : Form
    {
        public frmKhacHang()
        {
            InitializeComponent();
        }
        string strConn = @"Server=.;Database=QLNHT;Trusted_Connection=True";
        int flag = 0;
        bool dangXemThungRac = false;
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        private void LoadKhachHang()
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("sp_LayDanhSachKhachHang", conn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    DataView dv = new DataView(dt);
                    dgvKhachHang.DataSource = dv;
                    if (dgvKhachHang.Columns.Contains("TrangThai"))
                    {
                        dgvKhachHang.Columns["TrangThai"].Visible = false;
                    }
                    DinhDangGridKhachHang(); // Hàm chỉnh HeaderText
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            }
        }
        private void DinhDangGridKhachHang()
        {
            if (dgvKhachHang.Columns.Count == 0) return;

            // 1. Cấu hình chung
            dgvKhachHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvKhachHang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvKhachHang.ReadOnly = true;
            dgvKhachHang.AllowUserToAddRows = false;
            dgvKhachHang.RowHeadersVisible = false;

            // Căn giữa các cột số
            if (dgvKhachHang.Columns.Contains("Số Điện Thoại"))
                dgvKhachHang.Columns["Số Điện Thoại"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void frmKhacHang_Load(object sender, EventArgs e)
        {
            LoadKhachHang();
            btnKhoiPhuc.Visible = false;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string tuKhoa = txtSearch.Text.Trim();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_TimKiemKhachHang", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TuKhoa", string.IsNullOrEmpty(tuKhoa) ? (object)DBNull.Value : tuKhoa);
                    //cmd.Parameters.AddWithValue("@TrangThai", dangXemThungRac ? 0 : 1);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    System.Data.DataTable dt = new System.Data.DataTable();
                    adapter.Fill(dt);

                    dgvKhachHang.DataSource = dt;
                    btnHuy_Click(sender, e);
                }
                catch (Exception) { }
            }
        }

        private void dgvKhachHang_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {  
        }

        private void dgvKhachHang_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }
        void XulyTextbox(Boolean t)
        {
            txtMaKH.ReadOnly = t;
            txtHoTen.ReadOnly = t;
            txtSDT.ReadOnly = t;
            txtDiaChi.ReadOnly = t;
            txtEmail.ReadOnly = t;
        }
        void XulyButton(Boolean t)
        {
            btnThem.Enabled = t;
            btnSua.Enabled = t;
            btnXoa.Enabled = t;
            btnLuu.Enabled = !t;
            btnHuy.Enabled = !t;
        }
        private string LayMaKhachHangTuDong()
        {
            string maMoi = "KH01"; // Giá trị dự phòng
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    // Gọi đúng tên Proc mới tạo
                    using (SqlCommand cmd = new SqlCommand("sp_GetNextCustomerCode", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        var result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            maMoi = result.ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi lấy mã khách hàng: " + ex.Message);
                }
            }
            return maMoi;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            flag = 1;
            XulyTextbox(false);
            XulyButton(false);

            txtHoTen.Clear();
            txtSDT.Clear();
            txtDiaChi.Clear();
            txtEmail.Clear();

            // Lấy mã tự động (KH01, KH02...)
            txtMaKH.Text = LayMaKhachHangTuDong();
            txtMaKH.ReadOnly = true;
            txtSearch.ReadOnly = true;
            txtHoTen.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaKH.Text))
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần sửa!");
                return;
            }
            flag = 2;
            XulyTextbox(false);
            XulyButton(false);
            txtMaKH.ReadOnly = true;
            txtSearch.ReadOnly = true;
            txtHoTen.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaKH.Text)) return;

            // Thông báo khác nhau tùy vào vị trí đang đứng
            string thongBao = !dangXemThungRac ? "Đưa khách hàng vào thùng rác?" : "XÓA VĨNH VIỄN khách hàng này?";

            if (MessageBox.Show(thongBao, "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(strConn))
                {
                    conn.Open();
                    if (!dangXemThungRac)
                    {
                        // Gọi Proc Xóa mềm (Trạng thái 1 -> 0)
                        SqlCommand cmd = new SqlCommand("sp_XoaKhachHang", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MaKH", txtMaKH.Text);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        // Lệnh xóa thật sự khỏi database
                        SqlCommand cmd = new SqlCommand("DELETE FROM KhachHang WHERE MaKhachHang=@MaKH", conn);
                        cmd.Parameters.AddWithValue("@MaKH", txtMaKH.Text);
                        cmd.ExecuteNonQuery();
                    }

                    // Tải lại đúng danh sách đang đứng
                    if (dangXemThungRac) LoadKhachHangDaXoa(); else LoadKhachHang();
                    btnHuy_Click(sender, e);
                }
            }
        }
        private void LoadKhachHangDaXoa()
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    // Bạn dùng Proc sp_LayDanhSachKhachHang nhưng sau đó lọc TrangThai = 0
                    SqlDataAdapter da = new SqlDataAdapter("sp_LayDanhSachKhachHang", conn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // LỌC RA NHỮNG NGƯỜI ĐÃ XÓA MỀM
                    dt.DefaultView.RowFilter = "TrangThai = 0";
                    dgvKhachHang.DataSource = dt.DefaultView;

                    // Đổi màu chữ hoặc Header để nhận diện
                    dgvKhachHang.Columns["MaKhachHang"].HeaderText = "Mã KH (Đã xóa)";
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            }
        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            flag = 0;
            XulyTextbox(true);
            XulyButton(true);

            txtMaKH.Clear();
            txtHoTen.Clear();
            txtSDT.Clear();
            txtDiaChi.Clear();
            txtEmail.Clear();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (flag == 0) return;

            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Tên khách hàng không được để trống!");
                return;
            }

            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    string proc = (flag == 1) ? "sp_ThemKhachHang" : "sp_SuaKhachHang";

                    using (SqlCommand cmd = new SqlCommand(proc, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@MaKH", txtMaKH.Text);
                        cmd.Parameters.AddWithValue("@TenKH", txtHoTen.Text.Trim());
                        cmd.Parameters.AddWithValue("@SĐT", txtSDT.Text.Trim());
                        cmd.Parameters.AddWithValue("@DiaChi", txtDiaChi.Text.Trim());
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());

                        // KHÔNG CÓ CBO: Mặc định truyền 1 khi thêm mới
                        // Nếu sửa, bạn có thể truyền 1 hoặc lấy giá trị ẩn từ Grid
                        cmd.Parameters.AddWithValue("@TrangThai", 1);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Lưu dữ liệu thành công!");

                        btnHuy_Click(sender, e);
                        LoadKhachHang();
                        txtSearch.ReadOnly = true;
                    }
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            }
        }

        private void btnThungRac_Click(object sender, EventArgs e)
        {
            if (!dangXemThungRac) // Vào thùng rác
            {
                dangXemThungRac = true;
                LoadKhachHangDaXoa();
                btnThungRac.Text = "Quay lại";
                btnKhoiPhuc.Visible = true;  // HIỆN nút khôi phục
                btnThem.Enabled = false;    // Khóa thêm/sửa
                btnSua.Enabled = false;
                btnXoa.Text = "Xóa vĩnh viễn";
            }
            else // Quay lại danh sách chính
            {
                dangXemThungRac = false;
                LoadKhachHang();
                btnThungRac.Text = "Thùng rác";
                btnKhoiPhuc.Visible = false; // ẨN nút khôi phục
                btnThem.Enabled = true;
                btnSua.Enabled = true;
                btnXoa.Text = "Xóa";
            }
        }

        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvKhachHang.Rows.Count)
            {
                DataGridViewRow row = dgvKhachHang.Rows[e.RowIndex];

                // Chỉ lấy những cột mà bạn đã SELECT trong Proc
                // Nếu Proc đặt tên có dấu [Mã KH], [Tên Khách Hàng] thì trong ngoặc phải y hệt
                txtMaKH.Text = row.Cells["Mã KH"].Value?.ToString() ?? "";
                txtHoTen.Text = row.Cells["Tên Khách Hàng"].Value?.ToString() ?? "";
                txtSDT.Text = row.Cells["Số Điện Thoại"].Value?.ToString() ?? "";
                txtDiaChi.Text = row.Cells["Địa Chỉ"].Value?.ToString() ?? "";
                txtEmail.Text = row.Cells["Email"].Value?.ToString() ?? "";

                // TUYỆT ĐỐI KHÔNG CÓ DÒNG NÀO GỌI ĐẾN "Trạng Thái" Ở ĐÂY
            }
        }

        private void btnKhoiPhuc_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtMaKH.Text)) return;

            if (MessageBox.Show("Bạn muốn đưa khách hàng này quay lại danh sách chính?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(strConn))
                {
                    try
                    {
                        conn.Open();
                        // SQL xử lý ngầm, C# không cần biết cột đó có trên Grid hay không
                        string sql = "UPDATE KhachHang SET TrangThai = 1 WHERE MaKhachHang = @MaKH";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@MaKH", txtMaKH.Text);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Khôi phục thành công!");
                        LoadKhachHangDaXoa(); // Nạp lại danh sách thùng rác
                        btnHuy_Click(sender, e);
                    }
                    catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
                }
            }
        }
    }
}
