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

namespace QUANLYNHAHANG
{
    public partial class MainForManager : Form
    {
        string strConn = @"Server=.\MSSQLSERVER1;Database=QLNH;Trusted_Connection=True";
        public MainForManager()
        {
            InitializeComponent();
        }
        private void MainForManager_Load(object sender, EventArgs e)
        {
            LoadHoaDon();
            XuatBaoCaoDoanhThu();
            HienThiDanhSachBan("BA");

        }
        private void LoadHoaDon()
        {
            string connectionString = @"Server=.\MSSQLSERVER1;Database=QLNH;Trusted_Connection=True";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Khởi tạo SqlCommand gọi tên Stored Procedure
                    using (SqlCommand cmd = new SqlCommand("sp_LayDanhSachHoaDonDaThanhToan", conn))
                    {
                        // Chỉ định rõ kiểu lệnh là Stored Procedure
                        cmd.CommandType = CommandType.StoredProcedure;

                        DataTable dt = new DataTable();
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }

                        dgvHoatDong.DataSource = null;
                        dgvHoatDong.Columns.Clear();
                        dgvHoatDong.AutoGenerateColumns = true;
                        dgvHoatDong.DataSource = dt;
                        dgvHoatDong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                        // Quản lý tiêu đề cột theo Index chính xác và gọn gàng
                        dgvHoatDong.Columns[0].HeaderText = "Mã Hóa Đơn";
                        dgvHoatDong.Columns[1].HeaderText = "Tên Bàn";
                        dgvHoatDong.Columns[2].HeaderText = "Nhân Viên";
                        dgvHoatDong.Columns[3].HeaderText = "Thời Gian Mở";
                        dgvHoatDong.Columns[4].HeaderText = "Thành Tiền";
                        dgvHoatDong.Columns[5].HeaderText = "Trạng Thái";

                        // Định dạng cột tiền tệ
                        dgvHoatDong.Columns[4].DefaultCellStyle.Format = "N0";
                        dgvHoatDong.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                        // Tô màu đơn giản toàn bộ dòng
                        dgvHoatDong.RowsDefaultCellStyle.BackColor = Color.LightBlue;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void XuatBaoCaoDoanhThu()
        {
            string connectionString = @"Server=.\MSSQLSERVER1;Database=QLNH;Trusted_Connection=True";
            using (SqlConnection conn = new SqlConnection(connectionString))
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

                        // Cấu hình DataGridView
                        dgvDoanhThu.DataSource = null;
                        dgvDoanhThu.AutoGenerateColumns = true; // Tự động lấy tên cột từ SQL: Tháng, Năm...
                        dgvDoanhThu.DataSource = dt;

                        // Tối ưu hiển thị
                        if (dgvDoanhThu.Columns["Thành tiền"] != null)
                        {
                            // Định dạng số có dấu phân cách hàng nghìn
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
        private void HienThiDanhSachBan(string khuVuc = null)
        {
            // 1. Thiết lập flpQLBan để các ô 1x1 nằm gọn
            flpQLBan.Controls.Clear();
            flpQLBan.AutoScroll = true;        // Hiện thanh cuộn nếu quá nhiều bàn
            flpQLBan.WrapContents = true;      // Tự động xuống dòng
            flpQLBan.Padding = new Padding(5); // Khoảng cách lề nhỏ cho gọn

            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_LayBanTheoKhu", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (!string.IsNullOrEmpty(khuVuc))
                            cmd.Parameters.AddWithValue("@Prefix", khuVuc);

                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            // 2. Tạo nút bấm hình vuông nhỏ (tỉ lệ 1x1)
                            Button btn = new Button
                            {
                                // Lọc bỏ chữ "Bàn " để chỉ hiện số/mã cho gọn trong ô nhỏ
                                Text = dr["TenBan"].ToString(),
                                Tag = dr["MaBan"].ToString(),
                                Width = 70,  // Kích thước ô nhỏ 1x1
                                Height = 70,
                                Margin = new Padding(3), // Khoảng cách giữa các ô rất nhỏ
                                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                                Cursor = Cursors.Hand,
                                FlatStyle = FlatStyle.Flat
                            };
                            btn.FlatAppearance.BorderSize = 1;
                            btn.FlatAppearance.BorderColor = Color.LightGray;

                            // 3. Màu sắc trạng thái (0: Trống, 1: Có người)
                            int status = Convert.ToInt32(dr["TrangThai"]);
                            if (status == 0)
                            {
                                btn.BackColor = Color.Green;
                                btn.ForeColor = Color.White;
                            }
                            else
                            {
                                btn.BackColor = Color.Tomato;
                                btn.ForeColor = Color.White;
                            }

                            // 4. Sự kiện Click
                            btn.Click += (s, e) => {
                                string maBan = btn.Tag.ToString();
                                // Thêm logic xử lý khi chọn bàn ở đây
                            };

                            flpQLBan.Controls.Add(btn);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void btnKA_Click(object sender, EventArgs e)
        {
            HienThiDanhSachBan("BA");
        }

        private void btnKB_Click(object sender, EventArgs e)
        {
            HienThiDanhSachBan("BB");
        }

        private void btnKVIP_Click(object sender, EventArgs e)
        {
            HienThiDanhSachBan("BV");
        }

        private void btnGoiMon_Click(object sender, EventArgs e)
        {

            frmBanHang frm = new frmBanHang();
            frm.ShowDialog();
            MainForManager_Load(sender, e);
        }

        private void btnQLNV_Click(object sender, EventArgs e)
        {
            frmQLNhanVien frm = new frmQLNhanVien();
            frm.ShowDialog();
            MainForManager_Load(sender, e);
        }

        private void MainForManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn đăng xuất và quay lại màn hình đăng nhập không?",
                "Xác nhận đăng xuất",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                // Tìm lại Form đăng nhập đang bị ẩn và hiện nó lên
                Form login = Application.OpenForms["frmDangNhap"];
                if (login != null)
                {
                    login.Show();
                    // Form hiện tại (frmNhanVien) sẽ tự đóng sau khi hàm này kết thúc
                }
                else
                {
                    // Nếu không tìm thấy (do đã bị Close trước đó), thì phải tạo mới
                    frmDangNhap newLogin = new frmDangNhap();
                    newLogin.Show();
                }
            }
            else
            {
                // Nếu chọn No, hủy bỏ việc đóng form hiện tại
                e.Cancel = true;
            }
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnQLHD_Click(object sender, EventArgs e)
        {
            frmDoanhThu frm = new frmDoanhThu();
            frm.ShowDialog();
            MainForManager_Load(sender, e);
        }

        private void btnQLBan_Click(object sender, EventArgs e)
        {
            frmAdminBan frm = new frmAdminBan();
            frm.ShowDialog();
            MainForManager_Load(sender, e);
        }

        private void btnQLMon_Click(object sender, EventArgs e)
        {
            frmAdminMon frm = new frmAdminMon();
            frm.ShowDialog();
            MainForManager_Load(sender, e);
        }
    }
}
