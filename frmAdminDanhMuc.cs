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

namespace QUANLYNHAHANG
{
    public partial class frmAdminDanhMuc : Form
    {
        public frmAdminDanhMuc()
        {
            InitializeComponent();
            ApplyModernTheme();   
            TaiDuLieu();
        }
        //--------------------------------------------------------
        BLL_DanhMuc bll = new BLL_DanhMuc();
        private void TaiDuLieu()
        {
            dgvDanhMuc.DataSource = bll.LayDanhSachDanhMuc();
        }
        private bool KiemTraNhapLieu()
        {
            if (string.IsNullOrWhiteSpace(txtMaDanhMuc.Text))
            {
                MessageBox.Show("Nhập mã danh mục!");
                txtMaDanhMuc.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTenDanhMuc.Text))
            {
                MessageBox.Show("Nhập tên danh mục!");
                txtTenDanhMuc.Focus();
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
            cboTrangThai.Items.Add("Ẩn");       // 0
            cboTrangThai.Items.Add("Hiển thị"); // 1
        }
        private void ApplyModernTheme()
        {
            // BUTTON
            ConfigureButton(btnThem, ColorTranslator.FromHtml("#00ABFD"), Color.White);
            ConfigureButton(btnSua, ColorTranslator.FromHtml("#71D9FF"), Color.Black);
            ConfigureButton(btnXoa, ColorTranslator.FromHtml("#003140"), Color.White);
            ConfigureButton(btnLamMoi, Color.White, ColorTranslator.FromHtml("#00587C"));
            ConfigureButton(btnLuu, ColorTranslator.FromHtml("#28A745"), Color.White);

            // GRID
            dgvDanhMuc.EnableHeadersVisualStyles = false;
            dgvDanhMuc.BorderStyle = BorderStyle.None;
            dgvDanhMuc.BackgroundColor = Color.White;

            dgvDanhMuc.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#00587C");
            dgvDanhMuc.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvDanhMuc.ColumnHeadersHeight = 40;

            dgvDanhMuc.RowTemplate.Height = 30;
            dgvDanhMuc.AlternatingRowsDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#CDF1FF");

            dgvDanhMuc.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#003140");
            dgvDanhMuc.DefaultCellStyle.SelectionForeColor = Color.White;
        }

        private void ConfigureButton(Button btn, Color bg, Color text)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = bg;
            btn.ForeColor = text;
            btn.Cursor = Cursors.Hand;
        }
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
        }
        private void LamMoi()
        {
            txtMaDanhMuc.Text = "";
            txtTenDanhMuc.Text = "";
            txtHinhAnh.Text = "";
            txtMoTa.Text = "";
            cboTrangThai.SelectedIndex = 1;

            txtMaDanhMuc.ReadOnly = false;

            TaiDuLieu();
            SetState(FormState.Idle);
        }

        //---------------------------------------------------------
        private void frmAdminDanhMuc_Load(object sender, EventArgs e)
        {
            dgvDanhMuc.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            LoadComboTrangThai();
            SetState(FormState.Idle);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LamMoi();
            txtMaDanhMuc.Focus();
            SetState(FormState.Adding);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaDanhMuc.Text))
            {
                MessageBox.Show("Chọn danh mục để sửa!");
                return;
            }

            SetState(FormState.Editing);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaDanhMuc.Text))
            {
                MessageBox.Show("Chọn danh mục để xoá!");
                return;
            }

            SetState(FormState.Deleting);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {

            string ma = txtMaDanhMuc.Text.Trim();
            string ten = txtTenDanhMuc.Text.Trim();
            string hinh = txtHinhAnh.Text.Trim();
            string mota = txtMoTa.Text.Trim();
            int trangThai = cboTrangThai.SelectedIndex;

            int result = 0;

            switch (currentState)
            {
                case FormState.Adding:
                    result = bll.ThemDanhMuc(ma, ten, hinh, mota, trangThai);
                    break;

                case FormState.Editing:
                    result = bll.SuaDanhMuc(ma, ten, hinh, mota, trangThai);
                    break;

                case FormState.Deleting:
                    result = bll.XoaDanhMuc(ma);
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

        private void dgvDanhMuc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvDanhMuc.Rows[e.RowIndex];

            txtMaDanhMuc.Text = row.Cells["MaDanhMuc"].Value?.ToString();
            txtTenDanhMuc.Text = row.Cells["TenDanhMuc"].Value?.ToString();
            txtHinhAnh.Text = row.Cells["HinhAnh"].Value?.ToString();
            txtMoTa.Text = row.Cells["MoTa"].Value?.ToString();

            if (row.Cells["TrangThai"].Value != null)
            {
                int tt = Convert.ToInt32(row.Cells["TrangThai"].Value);

                if (tt >= 0 && tt < cboTrangThai.Items.Count)
                    cboTrangThai.SelectedIndex = tt;
                else
                    cboTrangThai.SelectedIndex = 0;
            }

            txtMaDanhMuc.ReadOnly = true;
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LamMoi();
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
                this.Close();
        }

        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string sourcePath = ofd.FileName;
                string fileName = System.IO.Path.GetFileName(sourcePath);

                string destFolder = Application.StartupPath + "\\Images";

                if (!System.IO.Directory.Exists(destFolder))
                {
                    System.IO.Directory.CreateDirectory(destFolder);
                }

                string destPath = destFolder + "\\" + fileName;

                System.IO.File.Copy(sourcePath, destPath, true);

                txtHinhAnh.Text = destPath;

                picPreview.Image = Image.FromFile(destPath);
            }
        }

        private void txtHinhAnh_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (System.IO.File.Exists(txtHinhAnh.Text))
                {
                    picPreview.Image = Image.FromFile(txtHinhAnh.Text);
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
            string keyword = txtTimKiem.Text.Trim().Replace("'", "''");

            DataTable dt = bll.LayDanhSachDanhMuc();

            if (!string.IsNullOrEmpty(keyword))
            {
                DataView dv = dt.DefaultView;
                dv.RowFilter = $@"
            MaDanhMuc LIKE '%{keyword}%' 
            OR TenDanhMuc LIKE '%{keyword}%' 
            OR HinhAnh LIKE '%{keyword}%'
            OR MoTa LIKE '%{keyword}%'
        ";

                dgvDanhMuc.DataSource = dv;
            }
            else
            {
                dgvDanhMuc.DataSource = dt;
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            dgvDanhMuc.DataSource = bll.TimDanhMuc(txtTimKiem.Text.Trim());
        }

        private void btnImportExcel_Click(object sender, EventArgs e)
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
                        string ma = ws.Cells[row, 1].Value?.ToString().Trim();
                        string ten = ws.Cells[row, 2].Value?.ToString().Trim();
                        string hinh = ws.Cells[row, 3].Value?.ToString().Trim();
                        string mota = ws.Cells[row, 4].Value?.ToString().Trim();

                        int trangThai = 0;
                        int.TryParse(ws.Cells[row, 5].Value?.ToString(), out trangThai);

                        if (string.IsNullOrEmpty(ma))
                        {
                            row++;
                            continue;
                        }

                        // 🔥 Tránh trùng
                        try
                        {
                            bll.ThemDanhMuc(ma, ten, hinh, mota, trangThai);
                        }
                        catch
                        {
                            bll.SuaDanhMuc(ma, ten, hinh, mota, trangThai);
                        }
                    }
                    catch (Exception exRow)
                    {
                        MessageBox.Show("Lỗi dòng " + row + ": " + exRow.Message);
                    }

                    row++;
                }

                MessageBox.Show("Import Excel thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                wb.Close(false);
                app.Quit();

                System.Runtime.InteropServices.Marshal.ReleaseComObject(ws);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(wb);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
            }

            TaiDuLieu();
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            Excel.Application app = new Excel.Application();
            Excel.Workbook wb = app.Workbooks.Add(Type.Missing);
            Excel.Worksheet ws = wb.ActiveSheet;

            try
            {
                // Header
                for (int i = 0; i < dgvDanhMuc.Columns.Count; i++)
                {
                    ws.Cells[1, i + 1] = dgvDanhMuc.Columns[i].HeaderText;
                }

                // Data
                for (int i = 0; i < dgvDanhMuc.Rows.Count; i++)
                {
                    if (dgvDanhMuc.Rows[i].IsNewRow) continue;

                    for (int j = 0; j < dgvDanhMuc.Columns.Count; j++)
                    {
                        ws.Cells[i + 2, j + 1] = dgvDanhMuc.Rows[i].Cells[j].Value?.ToString();
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
    }
}
