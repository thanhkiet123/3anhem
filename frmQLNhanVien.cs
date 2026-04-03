using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using Excel = Microsoft.Office.Interop.Excel;

namespace QUANLYNHAHANG
{
    public partial class frmQLNhanVien : Form
    {
        public frmQLNhanVien()
        {
            InitializeComponent();
        }
        string strConn = @"Server=.;Database=QLNHT;Trusted_Connection=True";
        int flag = 0;
        private void LoadTatCaNhanVien()
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_LayTatCaNhanVien", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        DataTable dt = new DataTable();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }

                        dt.DefaultView.RowFilter = "TrangThai = 1";
                        dgvNhanVien.DataSource = dt.DefaultView;
                        dgvNhanVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                        dgvNhanVien.Columns["TrangThai"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                        if (dgvNhanVien.Columns.Count > 0)
                        {
                            // Đặt Header Text
                            dgvNhanVien.Columns["MaNhanVien"].HeaderText = "Mã Nhân Viên";
                            dgvNhanVien.Columns["TenNhanVien"].HeaderText = "Tên Nhân Viên";
                            dgvNhanVien.Columns["SoDienThoai"].HeaderText = "Số Điện Thoại";
                            dgvNhanVien.Columns["DiaChi"].HeaderText = "Địa Chỉ";
                            dgvNhanVien.Columns["TenDangNhap"].HeaderText = "Tên Đăng Nhập";
                            dgvNhanVien.Columns["MatKhau"].HeaderText = "Mật Khẩu";
                            dgvNhanVien.Columns["TenVaiTro"].HeaderText = "Tên Vai Trò";
                            dgvNhanVien.Columns["TrangThai"].HeaderText = "Trạng Thái";

                            // Ẩn cột TrangThai đi cho đẹp vì dòng nào cũng là 1
                            dgvNhanVien.Columns["MaVaiTro"].Visible = false;
                            dgvNhanVien.Columns["TenTrangThai"].Visible = false;

                            // Cho cột cuối cùng giãn ra
                            dgvNhanVien.Columns[dgvNhanVien.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        }

                        dgvNhanVien.AllowUserToAddRows = false;
                        dgvNhanVien.ReadOnly = true;
                        dgvNhanVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        DinhDangGridNhanVien();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lấy dữ liệu nhân viên: " + ex.Message, "Thông báo lỗi");
                }
            }
        }
        private void DinhDangGridNhanVien()
        {
            // 1. Chế độ mặc định là Fill để lấp đầy bảng
            dgvNhanVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // 2. Chỉnh tỉ lệ ưu tiên cho các cột quan trọng
            if (dgvNhanVien.Columns.Contains("Họ Tên"))
            {
                dgvNhanVien.Columns["Họ Tên"].FillWeight = 150; // Rộng gấp 1.5 lần cột khác
            }

            if (dgvNhanVien.Columns.Contains("Địa Chỉ"))
            {
                dgvNhanVien.Columns["Địa Chỉ"].FillWeight = 200; // Rộng gấp đôi
            }

            if (dgvNhanVien.Columns.Contains("Mã NV"))
            {
                dgvNhanVien.Columns["Mã NV"].FillWeight = 70; // Mã ngắn nên để hẹp lại
            }
        }
        private void LoadNhanVienDaXoa()
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_LayTatCaNhanVien", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        DataTable dt = new DataTable();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }

                        // --- LỌC NHÂN VIÊN CÓ TRANG THÁI = 0 (Đã nghỉ/Xóa mềm) ---
                        dt.DefaultView.RowFilter = "TrangThai = 0";
                        dgvNhanVien.DataSource = dt.DefaultView;

                        // Cấu hình hiển thị (Giữ nguyên format như hàm cũ)
                        if (dgvNhanVien.Columns.Count > 0)
                        {
                            dgvNhanVien.Columns["MaNhanVien"].HeaderText = "Mã NV (Đã xóa)";
                            dgvNhanVien.Columns["TenNhanVien"].HeaderText = "Tên Nhân Viên";

                            // Hiện cột trạng thái lên để người dùng biết đây là danh sách lưu trữ
                            if (dgvNhanVien.Columns.Contains("TrangThai"))
                            {
                                dgvNhanVien.Columns["TrangThai"].Visible = true;
                                dgvNhanVien.Columns["TrangThai"].HeaderText = "Trạng Thái (Off)";
                            }
                        }

                        // Thông báo nếu thùng rác trống
                        if (dgvNhanVien.Rows.Count == 0)
                        {
                            MessageBox.Show("Thùng rác trống!", "Thông báo");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi load thùng rác: " + ex.Message);
                }
            }
        }
        private void LoadComboBoxVaiTro()
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_LayDanhSachVaiTro", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            cbChucVu.DataSource = dt;
                            cbChucVu.DisplayMember = "TenVaiTro";
                            cbChucVu.ValueMember = "MaVaiTro";
                            cbChucVu.SelectedIndex = -1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi nạp danh sách vai trò: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void LoadComboBoxTrangThai()
        {
            var items = new[] {
        new { Text = "Hoạt Động", Value = 1 },
        new { Text = "Không Hoạt Động ", Value = 0 }
    };

            cbTrangThai.DataSource = items;
            cbTrangThai.DisplayMember = "Text";
            cbTrangThai.ValueMember = "Value";
        }
        private void frmQLNhanVien_Load(object sender, EventArgs e)
        {
            LoadTatCaNhanVien();
            LoadComboBoxVaiTro();
            LoadComboBoxTrangThai();
        }
        void XulyTextbox(Boolean t)
        {
            txtMaNV.ReadOnly = t;
            txtHoTen.ReadOnly = t;
            txtSDT.ReadOnly = t;
            txtDiaChi.ReadOnly = t;
            txtUser.ReadOnly = t;
            txtPass.ReadOnly = t;
        }
        void XulyButton(Boolean t)
        {
            btnThem.Enabled = t;
            btnSua.Enabled = t;
            btnXoa.Enabled = t;
            btnLuu.Enabled = !t;
            btnHuy.Enabled = !t;
        }
        private void dgvNhanVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvNhanVien.Rows.Count)
            {
                DataGridViewRow row = dgvNhanVien.Rows[e.RowIndex];
                txtMaNV.ReadOnly = true;

                // SỬA LẠI TÊN CỘT KHỚP VỚI SQL (MaNhanVien, TenNhanVien, SoDienThoai...)
                // Tránh dùng "Mã NV" hay "Họ Tên" vì đó là HeaderText, không phải Name.
                txtMaNV.Text = row.Cells["MaNhanVien"].Value?.ToString() ?? "";
                txtHoTen.Text = row.Cells["TenNhanVien"].Value?.ToString() ?? "";
                txtSDT.Text = row.Cells["SoDienThoai"].Value?.ToString() ?? "";
                txtDiaChi.Text = row.Cells["DiaChi"].Value?.ToString() ?? "";
                txtUser.Text = row.Cells["TenDangNhap"].Value?.ToString() ?? "";
                txtPass.Text = row.Cells["MatKhau"].Value?.ToString() ?? "";

                // Đồng bộ ComboBox Vai Trò
                if (dgvNhanVien.Columns.Contains("MaVaiTro") && row.Cells["MaVaiTro"].Value != null)
                {
                    cbChucVu.SelectedValue = row.Cells["MaVaiTro"].Value.ToString();
                }

                // Cập nhật ComboBox Trạng Thái
                cbTrangThai.SelectedValue = dangXemThungRac ? 0 : 1;
            }
        }

        private void dgvNhanVien_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvNhanVien.Columns[e.ColumnIndex].Name == "TrangThai" && e.Value != null)
            {
                int status = Convert.ToInt32(e.Value);
                if (status == 1)
                {
                    e.Value = "Hoạt Động";
                }
                else if (status == 0)
                {
                    e.Value = "Không Hoạt Động";
                }
                // Sau khi gán xong, đánh dấu là đã format để hệ thống không xử lý tiếp
                e.FormattingApplied = true;
            }
        }
        private string LayMaTuDong(string maVT)
        {
            string maMoi = "";
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_GetNextEmployeeCode", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MaVaiTro", maVT);
                        var result = cmd.ExecuteScalar();
                        if (result != null) maMoi = result.ToString();
                    }
                }
                catch (Exception ex) { MessageBox.Show("Lỗi lấy mã: " + ex.Message); }
            }
            return maMoi;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            flag = 1;
            txtSearch.ReadOnly = true;
            XulyTextbox(false);
            XulyButton(false);
            txtHoTen.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            XulyButton(false);
            if (string.IsNullOrEmpty(txtMaNV.Text))
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần sửa!");
                return;
            }
            flag = 2;
            XulyTextbox(false);
            txtMaNV.ReadOnly = true;
            txtSearch.ReadOnly = true;
            txtHoTen.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaNV.Text)) return;

            
            string message = "Bạn có chắc chắn muốn thực hiện thao tác xóa trên nhân viên này?";

            if (MessageBox.Show(message, "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(strConn))
                {
                    try
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("sp_XoaNhanVien", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MaNV", txtMaNV.Text);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Thao tác thành công!", "Thông báo");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message);
                    }
                }

                // Sau khi xóa (mềm/thật), nạp lại danh sách CHỈ HIỆN người đang làm (TrangThai = 1)
                LoadTatCaNhanVien();

                btnHuy_Click(sender, e);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (flag == 0) return;
            if (string.IsNullOrWhiteSpace(txtMaNV.Text) || string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Mã và Tên không được để trống!");
                return;
            }

            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    string proc = (flag == 1) ? "sp_ThemNhanVien" : "sp_SuaNhanVien";
                    using (SqlCommand cmd = new SqlCommand(proc, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MaNV", txtMaNV.Text.Trim());
                        cmd.Parameters.AddWithValue("@TenNV", txtHoTen.Text.Trim());
                        cmd.Parameters.AddWithValue("@SDT", txtSDT.Text.Trim());
                        cmd.Parameters.AddWithValue("@DiaChi", txtDiaChi.Text.Trim());
                        cmd.Parameters.AddWithValue("@User", txtUser.Text.Trim());
                        cmd.Parameters.AddWithValue("@Pass", txtPass.Text.Trim());
                        cmd.Parameters.AddWithValue("@MaVT", cbChucVu.SelectedValue);
                        cmd.Parameters.AddWithValue("@TrangThai", cbTrangThai.SelectedValue);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Lưu dữ liệu thành công!");

                        flag = 0; // Reset trạng thái
                        txtMaNV.ReadOnly = true;
                        LoadTatCaNhanVien();
                        XulyButton(true);
                    }
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            txtMaNV.Clear();
            txtHoTen.Clear();
            txtSDT.Clear();
            txtDiaChi.Clear();
            txtUser.Clear();
            txtPass.Clear();
            cbChucVu.SelectedIndex = -1;
            cbTrangThai.SelectedIndex = 0;
            if (flag == 0) txtMaNV.ReadOnly = true;
            XulyButton(true);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmQLNhanVien_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn thoát chương trình quản lý nhân viên không?",
                "Xác nhận thoát",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void cbChucVu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (flag == 1 && txtMaNV.ReadOnly == false && cbChucVu.Focused)
            {
                if (cbChucVu.SelectedValue != null)
                {
                    txtMaNV.Text = LayMaTuDong(cbChucVu.SelectedValue.ToString());
                }
            }
        }
        bool dangXemThungRac = false;
        private void btnThungRac_Click(object sender, EventArgs e)
        {
            if (!dangXemThungRac)
            {
                LoadNhanVienDaXoa();
                btnThungRac.Text = "Quay lại"; // Đổi text nút để người dùng biết
                btnThungRac.ForeColor = Color.Green;
                dangXemThungRac = true;

                // Vô hiệu hóa nút Xóa hoặc đổi tên thành "Xóa vĩnh viễn"
                btnXoa.Text = "Xóa vĩnh viễn";
            }
            else
            {
                LoadTatCaNhanVien(); // Hàm hiện TrangThai = 1 bạn đã có
                btnThungRac.Text = "Thùng rác";
                btnThungRac.ForeColor = Color.Black;
                dangXemThungRac = false;

                btnXoa.Text = "Xóa (Mềm)";
            }
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            if (dgvNhanVien.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo");
                return;
            }

            // 2. Khởi tạo các đối tượng Excel
            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook workbook = excelApp.Workbooks.Add(Type.Missing);
            Excel._Worksheet worksheet = null;

            try
            {
                worksheet = workbook.ActiveSheet;
                worksheet.Name = "Danh Sach Nhan Vien";

                // 3. Xuất Tiêu đề cột (Headers)
                for (int i = 1; i <= dgvNhanVien.Columns.Count; i++)
                {
                    // Chỉ xuất các cột đang hiển thị (Visible)
                    if (dgvNhanVien.Columns[i - 1].Visible)
                    {
                        worksheet.Cells[1, i] = dgvNhanVien.Columns[i - 1].HeaderText;
                        worksheet.Cells[1, i].Font.Bold = true; // Chữ đậm cho tiêu đề
                    }
                }

                // 4. Xuất Dữ liệu dòng (Rows)
                for (int i = 0; i < dgvNhanVien.Rows.Count; i++)
                {
                    for (int j = 0; j < dgvNhanVien.Columns.Count; j++)
                    {
                        if (dgvNhanVien.Columns[j].Visible)
                        {
                            // Excel bắt đầu từ index 1
                            worksheet.Cells[i + 2, j + 1] = dgvNhanVien.Rows[i].Cells[j].Value.ToString();
                        }
                    }
                }

                // 5. Mở hộp thoại lưu file
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                saveDialog.FileName = "DS_NhanVien_" + DateTime.Now.ToString("ddMMyyyy");

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    workbook.SaveAs(saveDialog.FileName);
                    MessageBox.Show("Xuất file Excel thành công!", "Thông báo");
                }

                // Hiện Excel lên sau khi xong (tùy chọn)
                excelApp.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất Excel: " + ex.Message);
            }
            finally
            {
                // Giải phóng tài nguyên
                excelApp = null;
                workbook = null;
                worksheet = null;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

            string tuKhoa = txtSearch.Text.Trim();

            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_TimKiemNhanVien", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Truyền từ khóa, nếu trống thì truyền DBNull
                        cmd.Parameters.AddWithValue("@TuKhoa", string.IsNullOrEmpty(tuKhoa) ? (object)DBNull.Value : tuKhoa);

                        // Quan trọng: Dùng biến dangXemThungRac (giống bảng KH) để lọc đúng tập dữ liệu
                        cmd.Parameters.AddWithValue("@TrangThai", dangXemThungRac ? 0 : 1);

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        System.Data.DataTable dt = new System.Data.DataTable();
                        adapter.Fill(dt);

                        // Đổ dữ liệu vào Grid
                        dgvNhanVien.DataSource = dt;
                    }
                }
                catch (Exception)
                {
                    // Bỏ qua lỗi hiển thị khi đang gõ nhanh để tránh treo máy
                }
            }
        }
    }
}

