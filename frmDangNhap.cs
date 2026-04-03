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
    public partial class frmDangNhap : Form
    {
        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {

        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string user = txtUserName.Text.Trim();
            string pass = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connectionString = @"Server=.;Database=QLNHT;Trusted_Connection=True";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT MaNhanVien, MaVaiTro FROM NhanVien WHERE TenDangNhap = @user AND MatKhau = @pass AND TrangThai = 1";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@user", user);
                        cmd.Parameters.AddWithValue("@pass", pass);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string maNhanVien = reader["MaNhanVien"].ToString();
                                string maVaiTro = reader["MaVaiTro"].ToString();

                                MessageBox.Show("Đăng nhập thành công!", "Thông báo");
                                this.Hide();

                                switch (maVaiTro)
                                {
                                    case "VTNV":
                                        MainForEmployee empForm = new MainForEmployee();
                                        empForm.ShowDialog();
                                        break;

                                    case "VTQL":
                                        MainForManager mainForm = new MainForManager();
                                        mainForm.ShowDialog();
                                        break;
                                }
                                this.Show();
                                txtPassword.Clear();
                                txtUserName.Focus();
                            }
                            else
                            {
                                MessageBox.Show("Sai tài khoản hoặc mật khẩu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối: " + ex.Message);
                }
            }
        }
    }
}
