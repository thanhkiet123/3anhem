using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace QUANLYNHAHANG
{
    public partial class frmAdminBan : Form
    {
        //-------------------------------------------------------
        private void ApplyModernTechTheme()
        {
            ConfigureButton(btnThem, ColorTranslator.FromHtml("#00ABFD"), Color.White);
            ConfigureButton(btnSua, ColorTranslator.FromHtml("#71D9FF"), Color.Black);
            ConfigureButton(btnXoa, ColorTranslator.FromHtml("#003140"), Color.White);
            ConfigureButton(btnLamMoi, Color.White, ColorTranslator.FromHtml("#00587C"));
            ConfigureButton(btnLuu, ColorTranslator.FromHtml("#28A745"), Color.White);

            dgvBan.EnableHeadersVisualStyles = false;
            dgvBan.BorderStyle = BorderStyle.None;
            dgvBan.BackgroundColor = Color.White;

            dgvBan.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#00587C");
            dgvBan.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvBan.ColumnHeadersHeight = 40;

            dgvBan.RowTemplate.Height = 30;
            dgvBan.AlternatingRowsDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#CDF1FF");

            dgvBan.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#003140");
            dgvBan.DefaultCellStyle.SelectionForeColor = Color.White;
        }
        private void SetInputState(bool enable)
        {
            txtMaBan.ReadOnly = !enable;
            txtTenBan.ReadOnly = !enable;
            txtSoChoNgoi.ReadOnly = !enable;

            cboTrangThai.Enabled = enable;

            // Nếu có thêm control thì thêm vào đây
        }
        //-------------------------------------------------------
        enum FormState
        {
            Idle,
            Adding,
            Editing,
            Deleting
        }

        private FormState currentState = FormState.Idle;
        //-------------------------------------------------------
        private void SetState(FormState state)
        {
            currentState = state;

            bool isIdle = state == FormState.Idle;

            // Button
            btnThem.Enabled = isIdle;
            btnSua.Enabled = isIdle;
            btnXoa.Enabled = isIdle;
            btnLuu.Enabled = !isIdle;

            // 🔥 Quan trọng nhất
            SetInputState(!isIdle);
        }

        private void SetButton(Button btn, bool enabled)
        {
            btn.Enabled = enabled;

            Color baseColor = originalColors[btn];

            if (enabled)
                btn.BackColor = baseColor; // luôn về màu gốc
            else
                btn.BackColor = ControlPaint.Dark(baseColor);
        }
        //-------------------------------------------------------
        private Dictionary<Button, Color> originalColors = new Dictionary<Button, Color>();

        private void ConfigureButton(Button btn, Color bg, Color text)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = bg;
            btn.ForeColor = text;
            btn.Cursor = Cursors.Hand;

            // Lưu màu gốc
            if (!originalColors.ContainsKey(btn))
                originalColors.Add(btn, bg);
        }
        public frmAdminBan()
        {
            InitializeComponent();
            ApplyModernTechTheme();
            TaiDuLieu();
            SetState(FormState.Idle);
            LoadComboTrangThai();
            txtTenBan.Enter += BlockInputWhenIdle;
            txtSoChoNgoi.Enter += BlockInputWhenIdle;
            txtMaBan.MouseDown += BlockMouseWhenIdle;
            txtTenBan.MouseDown += BlockMouseWhenIdle;
            txtSoChoNgoi.MouseDown += BlockMouseWhenIdle;
            cboKhu.MouseDown += BlockMouseWhenIdle;
            AttachBlockEvents();
        }
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
        private void AttachBlockEvents()
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is TextBox txt)
                {
                    txt.Enter += BlockInputWhenIdle;
                    txt.MouseDown += BlockMouseWhenIdle;
                }
            }
        }
        private void frmAdminBan_Load(object sender, EventArgs e)
        {
            dgvBan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            cboKhu.Items.Clear();
            cboKhu.Items.Add("A");
            cboKhu.Items.Add("B");
            cboKhu.Items.Add("V"); // VIP
            cboKhu.SelectedIndex = 0;
            SetState(FormState.Idle);
        
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            LamMoi();

            txtMaBan.Text = "(Tự động)";
            txtMaBan.ReadOnly = true;

            txtTenBan.Focus();
            SetState(FormState.Adding);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaBan.Text))
            {
                MessageBox.Show("Chọn bàn để sửa!");
                return;
            }

            SetState(FormState.Editing);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaBan.Text))
            {
                MessageBox.Show("Chọn bàn để xoá!");
                return;
            }

            SetState(FormState.Deleting);
        }
        //-------------------------------------------------------
        BLL_Ban bll = new BLL_Ban();
        private void TaiDuLieu()
        {
            dgvBan.DataSource = bll.LayDanhSachBan();
        }
        private bool KiemTraNhapLieu()
        {
            
            if (string.IsNullOrWhiteSpace(txtTenBan.Text))
            {
                MessageBox.Show("Vui lòng nhập tên bàn!");
                txtTenBan.Focus();
                return false;
            }

            if (!int.TryParse(txtSoChoNgoi.Text, out int soCho) || soCho <= 0)
            {
                MessageBox.Show("Số chỗ phải là số > 0!");
                txtSoChoNgoi.Focus();
                return false;
            }

            if (cboTrangThai.SelectedIndex < 0)
            {
                MessageBox.Show("Chọn trạng thái!");
                return false;
            }

            return true;
        }
        private void LoadComboTrangThai()
        {
            cboTrangThai.Items.Clear();
            cboTrangThai.Items.Add("Trống");      // index 0
            cboTrangThai.Items.Add("Có người");   // index 1
        }
        //-------------------------------------------------------
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!KiemTraNhapLieu()) return;

            string maBan = txtMaBan.Text.Trim();
            string tenBan = txtTenBan.Text.Trim();
            int soChoNgoi = int.Parse(txtSoChoNgoi.Text);
            int trangThai = cboTrangThai.SelectedIndex;

            int result = 0;

            switch (currentState)
            {
                case FormState.Adding:
                    {
                        // 🔥 KHÔNG dùng maBan nữa
                        string khu = cboKhu.SelectedItem.ToString();  // 👉 sau này có thể lấy từ combobox

                        string maMoi = bll.ThemBanTuDong(tenBan, soChoNgoi, trangThai, khu);

                        if (!string.IsNullOrEmpty(maMoi))
                        {
                            MessageBox.Show("✅ Thêm thành công! Mã bàn: " + maMoi);
                            txtMaBan.Text = maMoi;
                            result = 1;
                        }
                        else
                        {
                            result = 0;
                        }
                        break;
                    }

                case FormState.Editing:
                    result = bll.SuaBan(maBan, tenBan, soChoNgoi, trangThai);
                    break;

                case FormState.Deleting:
                    result = bll.XoaBan(maBan);
                    break;
            }

            if (result > 0)
            {
                MessageBox.Show("✅ Thành công!");
                LamMoi();
                SetState(FormState.Idle);
            }
            else
            {
                MessageBox.Show("❌ Thất bại!");
            }
        }
        private void LamMoi()
        {
            txtMaBan.Text = "";
            txtTenBan.Text = "";
            txtSoChoNgoi.Text = "";
            cboTrangThai.SelectedIndex = 0;

            txtMaBan.ReadOnly = false;

            TaiDuLieu();
            SetState(FormState.Idle);
        }

        private void dgvBan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvBan.Rows[e.RowIndex];

            txtMaBan.Text = row.Cells["MaBan"].Value?.ToString();
            txtTenBan.Text = row.Cells["TenBan"].Value?.ToString();
            txtSoChoNgoi.Text = row.Cells["SoChoNgoi"].Value?.ToString();

            // 👉 FIX TRANG THÁI
            if (row.Cells["TrangThai"].Value != null && cboTrangThai.Items.Count > 0)
            {
                int trangThai = Convert.ToInt32(row.Cells["TrangThai"].Value);

                cboTrangThai.SelectedIndex =
                    (trangThai >= 0 && trangThai < cboTrangThai.Items.Count)
                    ? trangThai : 0;
            }

            // 🔥 FIX KHU (THIẾU CHÍNH LÀ Ở ĐÂY)
            string maBan = row.Cells["MaBan"].Value?.ToString();

            if (!string.IsNullOrEmpty(maBan) && maBan.Length >= 2)
            {
                string khu = maBan.Substring(1, 1); // lấy ký tự thứ 2

                if (cboKhu.Items.Contains(khu))
                    cboKhu.SelectedItem = khu;
                else
                    cboKhu.SelectedIndex = 0;
            }

            txtMaBan.ReadOnly = true;
            SetState(FormState.Idle);

        }

        private void dgvBan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            SetState(FormState.Idle);
        
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LamMoi();
        }

        private void btnThoat_Click(object sender, EventArgs e)
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

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim().Replace("'", "''"); // tránh lỗi '

            DataTable dt = bll.LayDanhSachBan();

            if (!string.IsNullOrEmpty(keyword))
            {
                DataView dv = dt.DefaultView;
                dv.RowFilter = $"MaBan LIKE '%{keyword}%' OR TenBan LIKE '%{keyword}%'";
                dgvBan.DataSource = dv;
            }
            else
            {
                dgvBan.DataSource = dt;
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            dgvBan.DataSource = bll.TimBan(txtTimKiem.Text.Trim());
        }

        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            Excel.Application app = new Excel.Application();
            Excel.Workbook wb = app.Workbooks.Add(Type.Missing);
            Excel.Worksheet ws = wb.ActiveSheet;

            try
            {
                // Header
                for (int i = 0; i < dgvBan.Columns.Count; i++)
                {
                    ws.Cells[1, i + 1] = dgvBan.Columns[i].HeaderText;
                }

                // Data
                for (int i = 0; i < dgvBan.Rows.Count; i++)
                {
                    if (dgvBan.Rows[i].IsNewRow) continue;

                    for (int j = 0; j < dgvBan.Columns.Count; j++)
                    {
                        ws.Cells[i + 2, j + 1] = dgvBan.Rows[i].Cells[j].Value?.ToString();
                    }
                }

                app.Visible = true;

                MessageBox.Show("Xuất Excel thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Excel Files|*.xls;*.xlsx";

            if (ofd.ShowDialog() != DialogResult.OK) return;

            Excel.Application app = new Excel.Application();
            Excel.Workbook wb = app.Workbooks.Open(ofd.FileName);
            Excel.Worksheet ws = wb.Sheets[1];

            int row = 2;

            try
            {
                while (ws.Cells[row, 1].Value != null)
                {
                    try
                    {
                        string maBan = ws.Cells[row, 1].Value?.ToString().Trim();
                        string tenBan = ws.Cells[row, 2].Value?.ToString().Trim();

                        int soCho = 0;
                        int.TryParse(ws.Cells[row, 3].Value?.ToString(), out soCho);

                        int trangThai = 0;
                        int.TryParse(ws.Cells[row, 4].Value?.ToString(), out trangThai);

                        if (string.IsNullOrEmpty(maBan))
                        {
                            row++;
                            continue;
                        }

                        // 🔥 tránh trùng
                        try
                        {
                            bll.ThemBan(maBan, tenBan, soCho, trangThai);
                        }
                        catch
                        {
                            bll.SuaBan(maBan, tenBan, soCho, trangThai);
                        }
                    }
                    catch (Exception exRow)
                    {
                        MessageBox.Show("Lỗi dòng " + row + ": " + exRow.Message);
                    }

                    row++;
                }

                MessageBox.Show("Import thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                wb.Close(false);
                app.Quit();

                // 🔥 QUAN TRỌNG: tránh treo Excel
                Marshal.ReleaseComObject(ws);
                Marshal.ReleaseComObject(wb);
                Marshal.ReleaseComObject(app);
            }

            TaiDuLieu();
        }

        private void txtTenBan_MouseEnter(object sender, EventArgs e)
        {

        }

        private void txtTenBan_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void txtTenBan_Enter(object sender, EventArgs e)
        {

        }
    }
}
