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
using Excel = Microsoft.Office.Interop.Excel;

namespace QUANLYNHAHANG
{
    public partial class frmDoanhThu : Form
    {
        public frmDoanhThu()
        {
            InitializeComponent();
        }
        string strConn = @"Server=.;Database=QLNHT;Trusted_Connection=True";

        private void frmDoanhThu_Load(object sender, EventArgs e)
        {
            LoadHoaDon();
            XuatBaoCaoDoanhThu();
            LoadCboThang();
            LoadCboNam();
            btnKhoiPhuc.Visible = false;
        }
        private void LoadHoaDon()
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    // Sử dụng Proc đã ALTER (lấy cả 0, 1, 2)
                    using (SqlCommand cmd = new SqlCommand("sp_LayHoaDonHoatDong", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        DataTable dt = new DataTable();
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            if (isThungRacMode)
                            {
                                // Lọc hóa đơn trong thùng rác
                                dt.DefaultView.RowFilter = "TrangThai = 0";
                            }
                            else
                            {
                                // Lọc hóa đơn hoạt động (1: Đã thanh toán, 2: Đang phục vụ)
                                dt.DefaultView.RowFilter = "TrangThai = 1 OR TrangThai = 2";
                            }
                        }

                        // Gán DataSource bằng DefaultView (kết quả sau khi lọc)
                        dgvHoaDon.DataSource = dt.DefaultView;

                        // Gọi hàm định dạng để đặt tên cột Tiếng Việt và Fill đầy ô
                        DinhDangLaiCotGrid();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Hàm định dạng bổ sung để hỗ trợ LoadHoaDon
        private void DinhDangLaiCotGrid()
        {
            if (dgvHoaDon.Columns.Count == 0) return;

            // 1. Đặt tên cột (Phải khớp với AS trong SQL)
            if (dgvHoaDon.Columns.Contains("MaHoaDon")) dgvHoaDon.Columns["MaHoaDon"].HeaderText = "Mã Hóa Đơn";
            if (dgvHoaDon.Columns.Contains("TenBan")) dgvHoaDon.Columns["TenBan"].HeaderText = "Tên Bàn";
            if (dgvHoaDon.Columns.Contains("TenNhanVien")) dgvHoaDon.Columns["TenNhanVien"].HeaderText = "Nhân Viên";
            if (dgvHoaDon.Columns.Contains("ThoiGian")) dgvHoaDon.Columns["ThoiGian"].HeaderText = "Thời Gian";

            if (dgvHoaDon.Columns.Contains("ThanhTien"))
            {
                dgvHoaDon.Columns["ThanhTien"].HeaderText = "Thành Tiền";
                dgvHoaDon.Columns["ThanhTien"].DefaultCellStyle.Format = "N0"; // Định dạng 100.000
            }

            if (dgvHoaDon.Columns.Contains("TrangThai"))
            {
                dgvHoaDon.Columns["TrangThai"].HeaderText = "Trạng Thái";
                // Quan trọng: Gán Name để sự kiện CellFormatting hoạt động
                dgvHoaDon.Columns["TrangThai"].Name = "TrangThai";
            }

            // 2. Tự động giãn đều các cột
            dgvHoaDon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // 3. Không cho sửa trực tiếp
            dgvHoaDon.ReadOnly = true;
            dgvHoaDon.AllowUserToAddRows = false;
        }
        private void XuatBaoCaoDoanhThu()
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_BaoCaoDoanhThuThangNam", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        DataTable dt = new DataTable();
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                        dgvDoanhThu.DataSource = null;
                        dgvDoanhThu.AutoGenerateColumns = true;
                        dgvDoanhThu.DataSource = dt;
                        if (dgvDoanhThu.Columns["Thành tiền"] != null)
                        {
                            dgvDoanhThu.Columns["Thành tiền"].DefaultCellStyle.Format = "N0";
                            dgvDoanhThu.Columns["Thành tiền"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        }

                        dgvDoanhThu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        dgvDoanhThu.RowsDefaultCellStyle.BackColor = Color.WhiteSmoke;
                        dgvDoanhThu.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xuất dữ liệu: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void LoadCboThang()
        {
            cboThang.Items.Clear();
            for (int i = 1; i <= 12; i++)
            {
                cboThang.Items.Add(i);
            }
            cboThang.SelectedItem = DateTime.Now.Month;
        }
        private void LoadCboNam()
        {
            cboNam.Items.Clear();
            int namHienTai = DateTime.Now.Year;

            for (int i = namHienTai; i >= namHienTai - 10; i--)
            {
                cboNam.Items.Add(i);
            }
            cboNam.SelectedIndex = 0;
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            if (cboThang.SelectedItem == null || cboNam.SelectedItem == null) return;

            int thang = int.Parse(cboThang.SelectedItem.ToString());
            int nam = int.Parse(cboNam.SelectedItem.ToString());

            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_LocHoaDonTheoThangNam", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Thang", thang);
                        cmd.Parameters.AddWithValue("@Nam", nam);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        dgvHoaDon.DataSource = dt;

                        if (dgvHoaDon.Columns.Contains("TongTien"))
                        {
                            dgvHoaDon.Columns["TongTien"].DefaultCellStyle.Format = "N0";
                            dgvHoaDon.Columns["TongTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                XuatBaoCaoDoanhThu();
            }
            else
            {
                TimKiemHoaDon(keyword);
            }
        }
        private void TimKiemHoaDon(string maHD)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_TimKiemHoaDonTheoMa", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MaHD", maHD);

                        DataTable dt = new DataTable();
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                        dgvHoaDon.DataSource = dt;
                        if (dgvHoaDon.Columns["Tổng Tiền"] != null)
                        {
                            dgvHoaDon.Columns["Tổng Tiền"].DefaultCellStyle.Format = "N0";
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi tìm kiếm: " + ex.Message);
                }
            }

        bool isThungRacMode = false;
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvHoaDon.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn hóa đơn cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Lấy mã hóa đơn từ dòng đang chọn
            string maHD = dgvHoaDon.CurrentRow.Cells["MaHoaDon"].Value.ToString();

            // Thiết lập nội dung cảnh báo dựa trên chế độ xem
            string messageXacNhan = isThungRacMode
                ? $"Bạn có chắc chắn muốn XÓA VĨNH VIỄN hóa đơn {maHD}?\nDữ liệu này sẽ không thể khôi phục!"
                : $"Bạn có chắc chắn muốn hủy hóa đơn {maHD} và đưa vào thùng rác?";

            DialogResult dr = MessageBox.Show(messageXacNhan, "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                isThungRacMode ? MessageBoxIcon.Stop : MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(strConn))
                {
                    try
                    {
                        conn.Open();
                        // Gọi Stored Procedure thay vì viết lệnh SQL trực tiếp
                        using (SqlCommand cmd = new SqlCommand("sp_XoaHoaDon", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@MaHD", maHD);

                            // Sử dụng ExecuteReader để nhận thông báo (Message) và mã lỗi (Code) từ Proc
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    string msg = reader["Message"].ToString();
                                    int code = Convert.ToInt32(reader["Code"]);

                                    if (code == 1) // Thành công
                                    {
                                        MessageBox.Show(msg, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else // Thất bại (Ví dụ: Xóa hóa đơn đang phục vụ)
                                    {
                                        MessageBox.Show(msg, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }
                            }
                        }

                        // Luôn load lại dữ liệu để cập nhật danh sách mới
                        LoadHoaDon();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi hệ thống khi thực hiện xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnThungRac_Click(object sender, EventArgs e)
        {
            isThungRacMode = !isThungRacMode; 

            if (isThungRacMode)
            {
                btnThungRac.Text = "Quay lại";
                btnKhoiPhuc.Visible = true;
            }
            else
            {
                btnThungRac.Text = "Thùng rác";
                btnKhoiPhuc.Visible = false;
            }
            LoadHoaDon();
        }
        private void LoadHoaDonTheoTrangThai(int trangThaiHienTai)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_LayDanhSachHoaDonDaThanhToan", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        DataTable dt = new DataTable();
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dt);
                        if (dt.Columns.Contains("TrangThai"))
                        {
                            dt.DefaultView.RowFilter = string.Format("TrangThai = {0}", trangThaiHienTai);
                        }

                        dgvHoaDon.DataSource = dt.DefaultView;

                        if (dgvHoaDon.Columns.Count > 0)
                        {
                            dgvHoaDon.Columns["TrangThai"].Visible = isThungRacMode;
                        }
                    }
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            }
        }
        private void btnKhoiPhuc_Click(object sender, EventArgs e)
        {
            if (dgvHoaDon.CurrentRow == null || !isThungRacMode) return;

            string maHD = dgvHoaDon.CurrentRow.Cells[0].Value.ToString();

            using (SqlConnection conn = new SqlConnection(strConn))
            {
                conn.Open();
                string sql = "UPDATE HoaDon SET TrangThai = 1 WHERE MaHoaDon = @id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", maHD);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Đã khôi phục hóa đơn!");
                LoadHoaDonTheoTrangThai(0);
            }
        }

        private void dgvHoaDon_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvHoaDon.Columns[e.ColumnIndex].Name == "TrangThai" && e.Value != null)
            {
                string val = e.Value.ToString();
                if (val == "2")
                {
                    e.Value = "Đã thanh toán";
                    e.CellStyle.ForeColor = Color.Green;
                }
                if (val == "1")
                {
                    e.Value = "Chưa thanh toán";
                    e.CellStyle.ForeColor = Color.Orange;
                }
                else if (val == "0")
                {
                    e.Value = "Đã hủy";
                    e.CellStyle.ForeColor = Color.Red;
                }
            }
        }

        private void btnXuatFile_Click(object sender, EventArgs e)
        {
            if (dgvHoaDon.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Documents (*.xls)|*.xls";
            sfd.FileName = "DanhSachHoaDon.xls";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;

                Excel.Application excelApp = new Excel.Application();
                excelApp.Application.Workbooks.Add(Type.Missing);

                try
                {
                    for (int i = 1; i < dgvHoaDon.Columns.Count + 1; i++)
                    {
                        excelApp.Cells[1, i] = dgvHoaDon.Columns[i - 1].HeaderText;
                        excelApp.Cells[1, i].Font.Bold = true;
                        excelApp.Cells[1, i].Interior.Color = Color.LightGray;
                    }

                    for (int i = 0; i < dgvHoaDon.Rows.Count; i++)
                    {
                        for (int j = 0; j < dgvHoaDon.Columns.Count; j++)
                        {
                            excelApp.Cells[i + 2, j + 1] = dgvHoaDon.Rows[i].Cells[j].FormattedValue.ToString();
                        }
                    }

                    excelApp.Columns.AutoFit();

                    excelApp.ActiveWorkbook.SaveCopyAs(sfd.FileName);
                    excelApp.ActiveWorkbook.Saved = true;
                    excelApp.Quit();

                    MessageBox.Show("Xuất hóa đơn ra Excel thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xuất Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
            }
        }
    }
   }
