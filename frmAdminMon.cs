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
using System.IO;

namespace QUANLYNHAHANG
{
    public partial class frmAdminMon : Form
    {
        BLL_MonAn bll = new BLL_MonAn();

        public frmAdminMon()
        {
            InitializeComponent();
            ApplyModernTheme();
            LoadDanhMuc();
            TaiDuLieu();
            AttachBlockEvents(this);
        }
        //------------------------------------------------
        enum FormState
        {
            Idle,
            Adding,
            Editing,
            Deleting
        }

        private FormState currentState = FormState.Idle;

        private void SetState(FormState state)
        {
            currentState = state;

            bool isIdle = state == FormState.Idle;

            btnThem.Enabled = isIdle;
            btnSua.Enabled = isIdle;
            btnXoa.Enabled = isIdle;

            btnLuu.Enabled = !isIdle;

            // 🔥 Khi quay về Idle → bỏ focus luôn
            if (isIdle)
                this.ActiveControl = null;
        }
        // 🔥 CHẶN NHẬP KHI IDLE
        private void BlockInputWhenIdle(object sender, EventArgs e)
        {
            if (currentState == FormState.Idle)
                this.ActiveControl = null;
        }

        private void BlockMouseWhenIdle(object sender, MouseEventArgs e)
        {
            if (currentState == FormState.Idle)
                this.ActiveControl = null;
        }
        private void AttachBlockEvents(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is TextBox txt)
                {
                    txt.Enter += BlockInputWhenIdle;
                    txt.MouseDown += BlockMouseWhenIdle;
                }

                if (ctrl is ComboBox cbo)
                {
                    cbo.MouseDown += BlockMouseWhenIdle;
                }

                // 🔥 Đệ quy cho GroupBox / Panel
                if (ctrl.HasChildren)
                {
                    AttachBlockEvents(ctrl);
                }
            }
        }
        // =============================================
        // LOAD DATA
        // =============================================
        private void TaiDuLieu()
        {
            dgvMonAn.DataSource = bll.LayDanhSachMonAn();
        }

        private void LoadDanhMuc()
        {
            DataTable dt = bll.LayDanhSachDanhMuc();
            cboDanhMuc.DataSource = dt;
            cboDanhMuc.DisplayMember = "TenDanhMuc";
            cboDanhMuc.ValueMember = "MaDanhMuc";
        }

        // =============================================
        // KIỂM TRA
        // =============================================
        private bool KiemTraNhapLieu()
        {
            if (string.IsNullOrWhiteSpace(txtMaMon.Text))
            {
                MessageBox.Show("Nhập mã món!");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTenMon.Text))
            {
                MessageBox.Show("Nhập tên món!");
                return false;
            }

            if (!decimal.TryParse(txtGiaBan.Text, out decimal gia) || gia <= 0)
            {
                MessageBox.Show("Giá không hợp lệ!");
                return false;
            }

            if (cboDanhMuc.SelectedIndex < 0)
            {
                MessageBox.Show("Chọn danh mục!");
                return false;
            }
            // Giá nhập
            if (!decimal.TryParse(txtGiaNhap.Text, out decimal giaNhap) || giaNhap < 0)
            {
                MessageBox.Show("Giá nhập không hợp lệ!");
                return false;
            }

            // Số lượng bán
            if (!int.TryParse(txtSLBan.Text, out int slBan) || slBan < 0)
            {
                MessageBox.Show("Số lượng bán không hợp lệ!");
                return false;
            }

            // Số lượng nhập
            if (!int.TryParse(txtSLNhap.Text, out int slNhap) || slNhap < 0)
            {
                MessageBox.Show("Số lượng nhập không hợp lệ!");
                return false;
            }

            return true;
        }

        // =============================================
        // THEME
        // =============================================
        private void ApplyModernTheme()
        {
            ConfigureButton(btnThem, ColorTranslator.FromHtml("#00ABFD"), Color.White);
            ConfigureButton(btnSua, ColorTranslator.FromHtml("#71D9FF"), Color.Black);
            ConfigureButton(btnXoa, ColorTranslator.FromHtml("#003140"), Color.White);
            ConfigureButton(btnLamMoi, Color.White, ColorTranslator.FromHtml("#00587C"));
            ConfigureButton(btnLuu, ColorTranslator.FromHtml("#28A745"), Color.White);
            ConfigureButton(btnChonAnh, ColorTranslator.FromHtml("#00587C"), Color.White);

            dgvMonAn.EnableHeadersVisualStyles = false;
            dgvMonAn.BorderStyle = BorderStyle.None;
            dgvMonAn.BackgroundColor = Color.White;

            dgvMonAn.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#00587C");
            dgvMonAn.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvMonAn.ColumnHeadersHeight = 40;

            dgvMonAn.RowTemplate.Height = 30;
            dgvMonAn.AlternatingRowsDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#CDF1FF");

            dgvMonAn.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#003140");
            dgvMonAn.DefaultCellStyle.SelectionForeColor = Color.White;
        }

        private void ConfigureButton(Button btn, Color bg, Color text)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = bg;
            btn.ForeColor = text;
            btn.Cursor = Cursors.Hand;
        }

        // =============================================
        // LÀM MỚI
        // =============================================
        private void LamMoi()
        {
            txtMaMon.Text = "";
            txtTenMon.Text = "";
            txtGiaBan.Text = "";
            txtDonViTinh.Text = "Phần";
            txtHinhAnh.Text = "";
            picPreview.Image = null;
            txtGiaNhap.Clear();
            txtSLBan.Clear();
            txtSLNhap.Clear();
            cboTrangThai.SelectedIndex = 1;
            cboDanhMuc.Text= null;


            TaiDuLieu();
            SetState(FormState.Idle);
        }
        //------------------------------------------------
        private void frmAdminMon_Load(object sender, EventArgs e)
        {
            cboTrangThai.Items.Add("Ngừng bán");
            cboTrangThai.Items.Add("Đang bán");
            cboTrangThai.SelectedIndex = 1;

            dgvMonAn.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            SetState(FormState.Idle);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            LamMoi();
            SetState(FormState.Adding);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaMon.Text))
            {
                MessageBox.Show("Chọn món để sửa!");
                return;
            }

            SetState(FormState.Editing);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaMon.Text))
            {
                MessageBox.Show("Chọn món để xoá!");
                return;
            }

            SetState(FormState.Deleting);
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LamMoi();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!KiemTraNhapLieu()) return;

            string ma = txtMaMon.Text.Trim();
            string ten = txtTenMon.Text.Trim();
            decimal gia = decimal.Parse(txtGiaBan.Text);
            string dvt = txtDonViTinh.Text.Trim();
            string hinh = txtHinhAnh.Text.Trim();
            string maDM = cboDanhMuc.SelectedValue.ToString();
            int trangThai = cboTrangThai.SelectedIndex;
            decimal giaNhap = decimal.Parse(txtGiaNhap.Text);
            int slBan = int.Parse(txtSLBan.Text);
            int slNhap = int.Parse(txtSLNhap.Text);

            int result = 0;

            switch (currentState)
            {
                case FormState.Adding:
                    result = bll.ThemMonAn(ma, ten, gia, giaNhap, slBan, slNhap, dvt, hinh, maDM, trangThai);
                    break;

                case FormState.Editing:
                    result = bll.SuaMonAn(ma, ten, gia, giaNhap, slBan, slNhap, dvt, hinh, maDM, trangThai);
                    break;

                case FormState.Deleting:
                    result = bll.XoaMonAn(ma);
                    break;
            }

            if (result > 0)
            {
                MessageBox.Show("✅ Thành công!");
                LamMoi();
            }
            else
            {
                MessageBox.Show("❌ Thất bại!");
            }
        }

        private void dgvMonAn_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvMonAn.Rows[e.RowIndex];

            txtMaMon.Text = row.Cells["MaMon"].Value?.ToString();
            txtTenMon.Text = row.Cells["TenMon"].Value?.ToString();
            txtGiaBan.Text = row.Cells["GiaBan"].Value?.ToString();
            txtDonViTinh.Text = row.Cells["DonViTinh"].Value?.ToString();
            txtHinhAnh.Text = row.Cells["HinhAnh"].Value?.ToString();
            txtGiaNhap.Text = row.Cells["GiaNhap"].Value?.ToString();
            txtSLBan.Text = row.Cells["SoLuongBanTrongNgay"].Value?.ToString();
            txtSLNhap.Text = row.Cells["SoLuongNhapTrongNgay"].Value?.ToString();
            cboDanhMuc.SelectedValue = row.Cells["MaDanhMuc"].Value;

            int tt = Convert.ToInt32(row.Cells["TrangThai"].Value);
            cboTrangThai.SelectedIndex = tt;

 

            // 🔥 FIX ẢNH
            string path = row.Cells["HinhAnh"].Value?.ToString();

            if (!string.IsNullOrEmpty(path) && File.Exists(path))
            {
                using (var img = Image.FromFile(path))
                {
                    picPreview.Image = new Bitmap(img);
                }
            }
            else
            {
                picPreview.Image = null;
            }
        }

        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image|*.jpg;*.png;*.jpeg";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtHinhAnh.Text = ofd.FileName;
                picPreview.Image = Image.FromFile(ofd.FileName);
            }
        }

        private void txtHinhAnh_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (System.IO.File.Exists(txtHinhAnh.Text))
                {
                    using (var img = Image.FromFile(txtHinhAnh.Text))
                    {
                        picPreview.Image = new Bitmap(img);
                    }
                }
                else
                {
                    picPreview.Image = null;
                }
            }
            catch
            {
                picPreview.Image = null;
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim();

            string maDanhMuc = cboDanhMuc.SelectedValue?.ToString();

            int? trangThai = null;
            if (cboTrangThai.SelectedIndex >= 0)
                trangThai = cboTrangThai.SelectedIndex;

            dgvMonAn.DataSource = bll.TimKiemMonAn(keyword, maDanhMuc, trangThai);
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            btnTimKiem_Click(null, null);
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            Excel.Application app = new Excel.Application();
            app.Application.Workbooks.Add(Type.Missing);

            // Header
            for (int i = 0; i < dgvMonAn.Columns.Count; i++)
            {
                app.Cells[1, i + 1] = dgvMonAn.Columns[i].HeaderText;
            }

            // Data
            for (int i = 0; i < dgvMonAn.Rows.Count; i++)
            {
                for (int j = 0; j < dgvMonAn.Columns.Count; j++)
                {
                    app.Cells[i + 2, j + 1] = dgvMonAn.Rows[i].Cells[j].Value;
                }
            }

            app.Columns.AutoFit();
            app.Visible = true;

            MessageBox.Show("Xuất Excel thành công!");
        }

        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Excel Files|*.xls;*.xlsx";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string filePath = ofd.FileName;

                Excel.Application app = new Excel.Application();
                Excel.Workbook wb = app.Workbooks.Open(filePath);
                Excel.Worksheet ws = wb.Sheets[1];

                SqlConnection conn = new SqlConnection(@"Data source=.;Initial Catalog=QLNHT;Integrated Security=True");

                try
                {
                    conn.Open();

                    int row = 2; // bỏ header

                    while (ws.Cells[row, 1].Value != null)
                    {
                        try
                        {
                            string maMon = ws.Cells[row, 1].Value?.ToString()?.Trim();
                            if (string.IsNullOrEmpty(maMon))
                            {
                                row++;
                                continue;
                            }

                            string tenMon = ws.Cells[row, 2].Value?.ToString()?.Trim();

                            decimal gia = 0;
                            decimal.TryParse(ws.Cells[row, 3].Value?.ToString(), out gia);

                            string dvt = ws.Cells[row, 4].Value?.ToString()?.Trim();

                            string maDM = ws.Cells[row, 5].Value?.ToString()?.Trim();
                            string tenDM = ws.Cells[row, 6].Value?.ToString()?.Trim();

                            string hinh = ws.Cells[row, 7].Value?.ToString()?.Trim();

                            int trangThai = 0;
                            int.TryParse(ws.Cells[row, 8].Value?.ToString(), out trangThai);

                            // 🔥 1. CHECK DANH MỤC
                            string checkDM = "SELECT COUNT(*) FROM DanhMuc WHERE MaDanhMuc = @ma";
                            SqlCommand cmdCheck = new SqlCommand(checkDM, conn);
                            cmdCheck.Parameters.AddWithValue("@ma", maDM);

                            int exists = (int)cmdCheck.ExecuteScalar();

                            // 👉 Nếu chưa có thì tự thêm
                            if (exists == 0)
                            {
                                string insertDM = "INSERT INTO DanhMuc (MaDanhMuc, TenDanhMuc) VALUES (@ma, @ten)";
                                SqlCommand cmdDM = new SqlCommand(insertDM, conn);
                                cmdDM.Parameters.AddWithValue("@ma", maDM);
                                cmdDM.Parameters.AddWithValue("@ten", tenDM);
                                cmdDM.ExecuteNonQuery();
                            }

                            // 🔥 2. INSERT MÓN ĂN
                            string insertMon = @"INSERT INTO MonAn
                        (MaMon, TenMon, GiaBan, DonViTinh, HinhAnh, MaDanhMuc, TrangThai)
                        VALUES (@ma, @ten, @gia, @dvt, @hinh, @madm, @tt)";

                            SqlCommand cmd = new SqlCommand(insertMon, conn);

                            cmd.Parameters.AddWithValue("@ma", maMon);
                            cmd.Parameters.AddWithValue("@ten", tenMon);
                            cmd.Parameters.AddWithValue("@gia", gia);
                            cmd.Parameters.AddWithValue("@dvt", dvt);
                            cmd.Parameters.AddWithValue("@hinh", hinh);
                            cmd.Parameters.AddWithValue("@madm", maDM);
                            cmd.Parameters.AddWithValue("@tt", trangThai);

                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception exRow)
                        {
                            MessageBox.Show("Lỗi tại dòng " + row + ": " + exRow.Message);
                        }

                        row++;
                    }

                    MessageBox.Show("Import Excel thành công!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối DB: " + ex.Message);
                }
                finally
                {
                    conn.Close();

                    wb.Close(false);
                    app.Quit();

                    // 🔥 Giải phóng bộ nhớ Excel (QUAN TRỌNG)
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(ws);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(wb);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
                }

                TaiDuLieu(); // reload DataGridView
            }
        }

        private void bntThoat_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show(
        "Bạn có chắc muốn thoát?",
        "Xác nhận",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question
    );

            if (rs == DialogResult.Yes)
            {
                this.Close();
            }
        }
        string TaoMaMon(string maDM)
        {
            int so = bll.DemSoMonTheoDanhMuc(maDM) + 1;

            // Bỏ "DM" ở đầu → lấy phần sau
            string maRutGon = maDM.Substring(2);

            return "M" + maRutGon + so.ToString("D1");
        }

        private void cboDanhMuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (currentState == FormState.Adding && cboDanhMuc.SelectedValue != null)
            {
                txtMaMon.Text = TaoMaMon(cboDanhMuc.SelectedValue.ToString());
            }
        }

        private void dgvMonAn_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
