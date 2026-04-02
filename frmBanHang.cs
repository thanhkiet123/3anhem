using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace QUANLYNHAHANG
{
    public partial class frmBanHang : Form
    {
        public frmBanHang()
        {
            InitializeComponent();
        }

        BanHang db = new BanHang();

        string maBanNguon = "";
        string maBanDich = "";
        string maBanDangChon = "";

        bool dangChuyenBan = false;
        bool dangGopBan = false;
        bool dangTachBan = false;

        // ==========================
        // LOAD DATA
        // ==========================
        DataTable GetBan()
        {
            return db.LayDuLieu("sp_GetBan");
        }

        HashSet<string> LayBanDangCoHoaDon()
        {
            HashSet<string> ds = new HashSet<string>();

            DataTable dt = db.LayDuLieu("sp_GetBanDangCoHoaDon");

            foreach (DataRow row in dt.Rows)
            {
                ds.Add(row["MaBan"].ToString());
            }

            return ds;
        }

        // ==========================
        // LOAD UI BÀN
        // ==========================
        void LoadBan(string khu)
        {
            DataTable dt = GetBan();
            var banDangOrder = LayBanDangCoHoaDon();

            var dsBan = dt.AsEnumerable().Where(row =>
            {
                string ma = row["MaBan"].ToString();
                if (khu == "A") return ma.StartsWith("BA");
                if (khu == "B") return ma.StartsWith("BB");
                return ma.StartsWith("BV");
            })
            .OrderBy(row =>
            {
                string ma = row["MaBan"].ToString();
                string so = new string(ma.SkipWhile(c => !char.IsDigit(c)).ToArray());
                return int.Parse(so);
            })
            .ToList();

            flpBan.Controls.Clear();

            foreach (var row in dsBan)
            {
                string maBan = row["MaBan"].ToString();
                string tenBan = row["TenBan"].ToString();
                int soCho = Convert.ToInt32(row["SoChoNgoi"]);
                int trangThai = Convert.ToInt32(row["TrangThai"]);

                Button btn = new Button();
                btn.Width = 180;
                btn.Height = 120;
                btn.Margin = new Padding(10);
                btn.Tag = maBan;

                btn.Text = $"{tenBan}\n{maBan}\n{soCho} chỗ";
                btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);

                if (banDangOrder.Contains(maBan))
                    btn.BackColor = Color.Gold;
                else if (trangThai == 0)
                    btn.BackColor = Color.Green;
                else
                    btn.BackColor = Color.Gray;

                btn.ForeColor = Color.White;
                btn.Click += BtnBan_Click;

                flpBan.Controls.Add(btn);
            }
        }
        void StyleDefault(Button btn)
        {
            btn.BackColor = Color.White;
            btn.ForeColor = Color.Black;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 2;
            btn.FlatAppearance.BorderColor = Color.FromArgb(33, 150, 243); // xanh
        }
        void StyleActive(Button btn)
        {
            btn.BackColor = Color.FromArgb(33, 150, 243);
            btn.ForeColor = Color.White;
        }
        Button currentButton = null;

        void SetActiveButton(Button btn)
        {
            // reset button cũ
            if (currentButton != null)
                StyleDefault(currentButton);

            // set button mới
            StyleActive(btn);
            currentButton = btn;
        }
        // ==========================
        // CLICK BÀN
        // ==========================
        private void BtnBan_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string maBan = btn.Tag.ToString();

            // ===== CHUYỂN BÀN =====
            if (dangChuyenBan)
            {
                if (maBanNguon == "")
                {
                    maBanNguon = maBan;
                    MessageBox.Show("Chọn bàn đích");
                    return;
                }

                if (maBanNguon == maBan)
                {
                    MessageBox.Show("Không thể chuyển cùng 1 bàn");
                    return;
                }

                ChuyenBan(maBanNguon, maBan);
                MessageBox.Show("Chuyển bàn thành công");
                ResetTrangThai();
                return;
            }

            // ===== GỘP BÀN =====
            if (dangGopBan)
            {
                if (maBanNguon == "")
                {
                    maBanNguon = maBan;
                    MessageBox.Show("Đã chọn bàn nguồn: " + maBan + "\nChọn bàn đích");
                    return;
                }

                if (maBanNguon == maBan)
                {
                    MessageBox.Show("Không thể gộp cùng 1 bàn");
                    return;
                }

                try
                {
                    GopBan(maBanNguon, maBan);
                    MessageBox.Show("Gộp bàn thành công");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi gộp bàn: " + ex.Message);
                }

                ResetTrangThai();
                return;
            }

            // ===== TÁCH BÀN =====
            if (dangTachBan)
            {
                if (maBanNguon == "")
                {
                    maBanNguon = maBan;
                    MessageBox.Show("Chọn bàn mới");
                    return;
                }

                frmTachBan f = new frmTachBan(maBanNguon, maBan);
                f.ShowDialog();

                ResetTrangThai();
                return;
            }

            // ===== MỞ FORM MÓN =====
            maBanDangChon = maBan;
            string maHD = LayHoacTaoHoaDon(maBan);

            if (string.IsNullOrEmpty(maHD))
            {
                MessageBox.Show("Không tạo được hóa đơn!");
                return;
            }

            frmMon fMon = new frmMon(maBanDangChon, maHD);
            fMon.ShowDialog();

            LoadBan("A");

        }

        // ==========================
        // DATABASE ACTION
        // ==========================
        void ChuyenBan(string banCu, string banMoi)
        {
            var ds = LayBanDangCoHoaDon();

            if (!ds.Contains(banCu))
            {
                MessageBox.Show("Bàn nguồn chưa có hóa đơn!");
                return;
            }

            if (ds.Contains(banMoi))
            {
                MessageBox.Show("Bàn đích đã có khách!");
                return;
            }

            db.XuLy("sp_ChuyenBan", new List<SqlParameter>()
    {
        new SqlParameter("@BanCu", banCu),
        new SqlParameter("@BanMoi", banMoi)
    });
        }

        void GopBan(string ban1, string ban2)
        {
            var ds = LayBanDangCoHoaDon();

            if (!ds.Contains(ban1) && !ds.Contains(ban2))
            {
                MessageBox.Show("Cả 2 bàn đều trống!");
                return;
            }

            string banChinh = ds.Contains(ban1) ? ban1 : ban2;
            string banPhu = banChinh == ban1 ? ban2 : ban1;

            db.XuLy("sp_GopBan", new List<SqlParameter>()
    {
        new SqlParameter("@BanChinh", banChinh),
        new SqlParameter("@BanPhu", banPhu)
    });
        }

        string LayHoacTaoHoaDon(string maBan)
        {
            var pars = new List<SqlParameter>()
    {
        new SqlParameter("@MaBan", maBan),
        new SqlParameter("@MaNhanVien", "NVNV1")
    };

            DataTable dt = db.LayDuLieu("sp_LayHoacTaoHoaDon", pars);

            if (dt.Rows.Count > 0)
                return dt.Rows[0][0].ToString();

            return null;
        }

        // ==========================
        // CONTROL STATE
        // ==========================
        void ResetTrangThai()
        {
            dangChuyenBan = false;
            dangGopBan = false;
            dangTachBan = false;
            maBanNguon = "";
            maBanDich = "";

            LoadBan("A");
        }
        void AddHoverEffect(Button btn)
        {
            btn.MouseEnter += (s, e) =>
            {
                if (btn != currentButton)
                    btn.BackColor = Color.FromArgb(230, 240, 255);
            };

            btn.MouseLeave += (s, e) =>
            {
                if (btn != currentButton)
                    btn.BackColor = Color.White;
            };
        }
        // ==========================
        // EVENTS
        // ==========================
        private void frmBanHang_Load(object sender, EventArgs e)
        {
            AddHoverEffect(btnA);
            AddHoverEffect(btnB);
            AddHoverEffect(btnVip);
            StyleDefault(btnA);
            StyleDefault(btnB);
            StyleDefault(btnVip);
            StyleDefault(btnChuyenBan);
            StyleDefault(btnGopBan);
            StyleDefault(btnTachBan);
            SetActiveButton(btnA);
            LoadBan("A");

        }

        private void btnA_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnA);
            LoadBan("A");
        }

        private void btnB_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnB);
            LoadBan("B");
        }

        private void btnVip_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnVip);
            LoadBan("V");
        }

        private void btnChuyenBan_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnChuyenBan);
            dangChuyenBan = true;
            MessageBox.Show("Chọn bàn nguồn → bàn đích");
        }

        private void btnGopBan_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnGopBan);
            dangGopBan = true;
            MessageBox.Show("Chọn 2 bàn để gộp");
        }

        private void btnTachBan_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnTachBan);
            dangTachBan = true;
            MessageBox.Show("Chọn bàn nguồn → bàn mới");
        }

        private void btnDatBan_Click(object sender, EventArgs e)
        {
            
        }
    }
}