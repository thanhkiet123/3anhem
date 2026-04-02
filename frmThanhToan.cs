
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Windows.Forms;

namespace QUANLYNHAHANG
{
    public partial class frmThanhToan : Form
    {
        string connectionString = @"Data source=.\MSSQLSERVER1;Initial Catalog=QLNH;Integrated Security=True";
        string maKhachHang = "";
        string maHoaDon;
        int diemDaDung = 0;
        int diemHienTai = 0;
        decimal tongTien = 0;
        decimal giamGia = 0;
        int diemCong = 0;
        int tongDiem = 0;
        DataTable dtHoaDon;


        public static class UITheme
        {
            public static Color Primary = Color.FromArgb(33, 150, 243);   // xanh
            public static Color Success = Color.FromArgb(76, 175, 80);    // xanh lá
            public static Color Danger = Color.FromArgb(244, 67, 54);     // đỏ
            public static Color Background = Color.FromArgb(245, 247, 250);
            public static Color Card = Color.White;
        }
        void StyleButton(Button btn, Color bg)
        {
            btn.BackColor = bg;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI", 11, FontStyle.Bold);

            btn.MouseEnter += (s, e) => btn.BackColor = ControlPaint.Light(bg);
            btn.MouseLeave += (s, e) => btn.BackColor = bg;
        }

        void BoGoc(Control c, int radius = 10)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(c.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(c.Width - radius, c.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, c.Height - radius, radius, radius, 90, 90);
            path.CloseAllFigures();
            c.Region = new Region(path);
        }
        public frmThanhToan(string maHD, decimal tongTien)
        {
            InitializeComponent();
            maHoaDon = maHD;
            this.tongTien = tongTien;
        }

        int flag = 0;
            
        void XulyTextbox(Boolean t)
        {
            txtTenKH.ReadOnly = t;
            txtEmail.ReadOnly = t;
            txtDiem.ReadOnly = t;
            txtSDT.ReadOnly = t;
        }
        void xoatextbox()
        {
            txtTenKH.Clear();
            txtEmail.Clear();
            txtDiem.Clear();
            txtSDT.Clear();

        }
        void XulyButton(Boolean t)
        {
            btnThem.Enabled = t;
            btnSua.Enabled = t;
            btnXoa.Enabled = t;
            btnLuu.Enabled = !t;
        }


        private void frmThanhToan_Load(object sender, EventArgs e)
        {
            lblMaHD.Text = "Mã hóa đơn: " + maHoaDon;

            LoadHoaDon(); // chỉ để hiển thị list

            CapNhatTongTien(); 
            this.BackColor = UITheme.Background;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            StyleButton(btnTienMat, UITheme.Success);
            StyleButton(button2, UITheme.Primary);
            StyleButton(btnDoiDiem, UITheme.Danger);
            BoGoc(btnTienMat);
            BoGoc(button2);
            BoGoc(groupBox1);
            BoGoc(txtGiamGia);
            BoGoc(txtDoiDiem);

        }


        void LoadHoaDon()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_LayChiTietHoaDon", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaHoaDon", maHoaDon);

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                dtHoaDon = new DataTable(); 
                da.Fill(dtHoaDon);

                flpHoaDon.Controls.Clear();

                foreach (DataRow row in dtHoaDon.Rows)
                {
                    flpHoaDon.Controls.Add(TaoItemHoaDon(row));
                }
            }
        }


        Panel TaoItemHoaDon(DataRow row)
        {
            Panel p = new Panel();
            p.Width = flpHoaDon.Width - 25;
            p.Height = 50;
            p.BackColor = Color.White;

            Label lblTen = new Label();
            lblTen.Text = row["TenMon"].ToString();
            lblTen.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblTen.Location = new Point(10, 5);
            lblTen.AutoSize = true;

            Label lblSL = new Label();
            lblSL.Text = "x" + row["SoLuong"].ToString();
            lblSL.Location = new Point(200, 5);
            lblSL.AutoSize = true;

            Label lblTien = new Label();
            lblTien.Text = Convert.ToDecimal(row["ThanhTien"]).ToString("N0") + "đ";
            lblTien.Location = new Point(300, 5);
            lblTien.AutoSize = true;
            lblTien.ForeColor = Color.Green;

            p.Controls.Add(lblTen);
            p.Controls.Add(lblSL);
            p.Controls.Add(lblTien);

            return p;
        }




        
        private void txtGiamGia_TextChanged(object sender, EventArgs e)
        {
            CapNhatTongTien();
        }
        decimal thanhToan = 0;
        void CapNhatTongTien()
        {
           
            decimal sauGiam = tongTien - giamGia;

            if (sauGiam < 0)
                sauGiam = 0;

            lblTongTien.Text = "Tổng tiền: " + tongTien.ToString("N0") + "đ";
            lblTongTienSauGiamGia.Text = "Sau giảm: " + sauGiam.ToString("N0") + "đ";

            
            txtGiamGia.Text = giamGia.ToString("N0");
        }

        private void btnTienMat_Click(object sender, EventArgs e)
        {
            CapNhatTongTien();

            if (!XacNhanThanhToan()) return;

            ThanhToan("TienMat");

            if (!string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                GuiEmailHoaDon(txtEmail.Text.Trim());
            }

            MessageBox.Show("Thanh toán tiền mặt thành công!");
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CapNhatTongTien();

            decimal tienThanhToan = tongTien - giamGia;
            if (tienThanhToan < 0) tienThanhToan = 0;

            string qrUrl = $"https://img.vietqr.io/image/970422-123456789-compact.png?amount={tienThanhToan}&addInfo={maHoaDon}";

            Form f = new Form();
            f.Text = "Quét QR thanh toán";
            f.Width = 300;
            f.Height = 350;

            PictureBox pic = new PictureBox();
            pic.Dock = DockStyle.Fill;
            pic.SizeMode = PictureBoxSizeMode.Zoom;

            try
            {
                pic.Load(qrUrl);
            }
            catch
            {
                MessageBox.Show("Không tải được QR!");
                return;
            }

            f.Controls.Add(pic);
            f.ShowDialog();

            if (!XacNhanThanhToan()) return;

            ThanhToan("ChuyenKhoan");

            if (!string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                GuiEmailHoaDon(txtEmail.Text.Trim());
            }

            MessageBox.Show("Thanh toán chuyển khoản thành công!");
            this.Close();
        }

        bool XacNhanThanhToan()
        {
            if (flpHoaDon.Controls.Count == 0)
            {
                MessageBox.Show("Hóa đơn chưa có món!");
                return false;
            }

            return MessageBox.Show("Xác nhận thanh toán?", "Thanh toán",
                MessageBoxButtons.YesNo) == DialogResult.Yes;
        }



        void ThanhToan(string phuongthuc)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_ThanhToanHoaDon", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // 🔥 PARAM CƠ BẢN
                cmd.Parameters.Add("@MaHoaDon", SqlDbType.NVarChar, 20).Value = maHoaDon;

                var pGiamGia = cmd.Parameters.Add("@GiamGia", SqlDbType.Decimal);
                pGiamGia.Precision = 18;
                pGiamGia.Scale = 2;
                pGiamGia.Value = giamGia;

                cmd.Parameters.Add("@PhuongThucThanhToan", SqlDbType.NVarChar, 50).Value = phuongthuc;

                
                if (string.IsNullOrEmpty(maKhachHang))
                    cmd.Parameters.Add("@MaKhachHang", SqlDbType.VarChar, 10).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@MaKhachHang", SqlDbType.VarChar, 10).Value = maKhachHang;

                
                cmd.Parameters.Add("@DiemSuDung", SqlDbType.Int).Value = diemDaDung;

                try
                {
                    conn.Open();



                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        if (rd.Read())
                        {
                            diemCong = rd["DiemCong"] != DBNull.Value ? Convert.ToInt32(rd["DiemCong"]) : 0;
                            tongDiem = rd["TongDiem"] != DBNull.Value ? Convert.ToInt32(rd["TongDiem"]) : 0;
                        }
                    }

                   
                    if (!string.IsNullOrEmpty(maKhachHang))
                    {
                        MessageBox.Show(
                            $"🎉 Thanh toán thành công!\n" +
                            $"+{diemCong} điểm\n" +
                            $"Tổng điểm hiện tại: {tongDiem}",
                            "Thông báo",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                    }
                    else
                    {
                        MessageBox.Show("Thanh toán thành công!", "Thông báo");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi thanh toán: " + ex.Message);
                }
            }
        }
        string TaoNoiDungHoaDonHTML()
        {
            StringBuilder rows = new StringBuilder();
            int stt = 1;

            foreach (DataRow row in dtHoaDon.Rows)
            {
                rows.Append($@"
<tr>
    <td>{stt++}</td>
    <td>{row["TenMon"]}</td>
    <td style='text-align:center'>{row["SoLuong"]}</td>
    <td style='text-align:right'>{Convert.ToDecimal(row["ThanhTien"]):N0}đ</td>
</tr>");
            }

            decimal thanhToan = tongTien - giamGia;
            if (thanhToan < 0) thanhToan = 0;

            return $@"
<html>
<body style='font-family:Segoe UI, sans-serif; background:#f4f6f8; padding:20px'>
    
<div style='max-width:600px;margin:auto;background:white;
            border-radius:12px;overflow:hidden;
            box-shadow:0 4px 20px rgba(0,0,0,0.1)'>

    <div style='background:#4CAF50;color:white;padding:20px;text-align:center'>
        <h2 style='margin:0'>🍽️ BETU RESTAURANT</h2>
        <p>Hóa đơn thanh toán</p>
    </div>

    <div style='padding:20px'>
        <p><b>Mã hóa đơn:</b> {maHoaDon}</p>
        <p><b>Thời gian:</b> {DateTime.Now:dd/MM/yyyy HH:mm}</p>
    </div>

    <table style='width:100%;border-collapse:collapse'>
        <thead>
            <tr style='background:#f0f0f0'>
                <th>STT</th>
                <th>Món</th>
                <th>SL</th>
                <th style='text-align:right'>Thành tiền</th>
            </tr>
        </thead>
        <tbody>
            {rows}
        </tbody>
    </table>

    <div style='padding:20px'>
        <p>Tổng tiền: <b>{tongTien:N0}đ</b></p>
        <p>Giảm giá: <b style='color:red'>-{giamGia:N0}đ</b></p>

        <p><b style='font-size:18px'>
            Thanh toán: 
            <span style='color:#4CAF50'>{thanhToan:N0}đ</span>
        </b></p>
    </div>

    <div style='background:#fafafa;padding:15px;text-align:center;font-size:12px;color:#888'>
        Cảm ơn quý khách ❤️
    </div>

</div>
</body>
</html>";
        }
        void GuiEmailHoaDon(string emailKhach)
        {
            try
            {
                if (dtHoaDon == null || dtHoaDon.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu hóa đơn để gửi!");
                    return;
                }

                string html = TaoNoiDungHoaDonHTML();

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("kietsoobin147@gmail.com", "BETU RESTAURANT");
                mail.To.Add(emailKhach);
                mail.Subject = $"Hóa đơn #{maHoaDon}";
                mail.Body = html;
                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential("kietsoobin147@gmail.com", "spfc jres oxzi fsos");
                smtp.EnableSsl = true;

                smtp.Send(mail);

                MessageBox.Show("Gửi email thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi gửi email: " + ex.Message);
            }
        }



        private void txtGiamGia_TextChanged_1(object sender, EventArgs e)
        {
            // nếu đang dùng điểm thì không cho nhập tay
            if (diemDaDung > 0) return;

            if (decimal.TryParse(txtGiamGia.Text, out decimal giam))
            {
                giamGia = giam;
            }
            else
            {
                giamGia = 0;
            }

            CapNhatTongTien();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSoDienThoai_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSoDienThoai_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TimKhachHang();
                e.SuppressKeyPress = true; // tránh tiếng beep
            }
        }

        void TimKhachHang()
        {
            string keyword = txtSearch.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("Nhập SĐT hoặc Email!");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_TimKhachHang", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Keyword", keyword);

                SqlDataReader rd = cmd.ExecuteReader();

                if (rd.Read())
                {
                    // ✅ TÌM THẤY
                    txtTenKH.Text = rd["TenKhachHang"].ToString();
                    txtEmail.Text = rd["Email"].ToString();
                    txtSDT.Text = rd["SoDienThoai"].ToString();
                    txtDiem.Text = rd["DiemTichLuy"].ToString();
                    diemHienTai = Convert.ToInt32(rd["DiemTichLuy"]);

                    maKhachHang = rd["MaKhachHang"].ToString(); // ⚠️ chỉ lưu nội bộ

                    // KHÓA input để tránh sửa sai khách
                    txtEmail.Enabled = false;
                    txtSDT.Enabled = false;
                }
                else
                {
                    // ❌ KHÔNG TÌM THẤY
                    txtTenKH.Clear();
                    txtEmail.Clear();
                    txtSDT.Clear();
                    txtDiem.Text = "0";

                    maKhachHang = "";

                    txtEmail.Enabled = true;
                    txtSDT.Enabled = true;

                    MessageBox.Show("Không tìm thấy khách hàng!");
                }
            }
        }

        private void btnDoiDiem_Click(object sender, EventArgs e)
        {
            // 🔥 1. Kiểm tra khách hàng
            if (string.IsNullOrEmpty(maKhachHang))
            {
                MessageBox.Show("Chưa chọn khách hàng!");
                return;
            }

            // 🔥 2. Kiểm tra input
            if (!int.TryParse(txtDoiDiem.Text.Trim(), out int diemMuonDoi) || diemMuonDoi <= 0)
            {
                MessageBox.Show("Nhập số điểm hợp lệ!");
                return;
            }

            // 🔥 3. Không cho đổi nhiều lần
            if (diemDaDung > 0)
            {
                MessageBox.Show("Bạn đã đổi điểm rồi!");
                return;
            }

            // 🔥 4. Kiểm tra đủ điểm
            if (diemMuonDoi > diemHienTai)
            {
                MessageBox.Show("Không đủ điểm!");
                return;
            }

            // 🔥 5. Quy đổi (1 điểm = 10.000đ)
            decimal tienGiam = diemMuonDoi * 10000;

            // 🔥 6. Không cho vượt quá tổng tiền
            if (tienGiam > tongTien)
            {
                diemMuonDoi = (int)(tongTien / 10000);
                tienGiam = diemMuonDoi * 10000;
            }

            // 🔥 7. Lưu lại điểm đã dùng (QUAN TRỌNG)
            diemDaDung = diemMuonDoi;

            // 🔥 8. Cập nhật dữ liệu
            giamGia = tienGiam;
            diemHienTai -= diemMuonDoi;

            // 🔥 9. Update UI
            txtDiem.Text = diemHienTai.ToString();

            // đồng bộ textbox giảm giá
            txtGiamGia.Text = giamGia.ToString("N0");

            // khóa input tránh sửa tay phá logic
            txtGiamGia.Enabled = false;
            txtDoiDiem.Enabled = false;

            // cập nhật label
            CapNhatTongTien();

            // 🔥 10. Thông báo
            MessageBox.Show(
                $"🎯 Đổi điểm thành công!\n" +
                $"- Đã dùng: {diemMuonDoi} điểm\n" +
                $"- Giảm: {tienGiam:N0}đ\n" +
                $"- Điểm còn lại: {diemHienTai}",
                "Đổi điểm",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            
        }
    }

}