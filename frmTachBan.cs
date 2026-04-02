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
    public partial class frmTachBan : Form
    {
        public frmTachBan(string banCu, string banMoi)
        {
            InitializeComponent();
            maBanCu = banCu;
            maBanMoi = banMoi;
        }

        string connectionString = @"Data source=.\MSSQLSERVER1;Initial Catalog=QLNH;Integrated Security=True";

        string maBanCu;
        string maBanMoi;
        string maHoaDonCu;

        DataTable dtBanOld = new DataTable();
        DataTable dtBanNew = new DataTable();


        void LoadHoaDonCu()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Lấy mã hóa đơn
                SqlCommand cmd = new SqlCommand("sp_LayHoaDonTheoBan", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaBan", maBanCu);

                object result = cmd.ExecuteScalar();

                if (result == null)
                {
                    MessageBox.Show("Bàn chưa có hóa đơn!");
                    return;
                }

                maHoaDonCu = result.ToString();

                // Load chi tiết
                SqlCommand cmd2 = new SqlCommand("sp_LayChiTietHoaDon", conn);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.AddWithValue("@MaHoaDon", maHoaDonCu);

                SqlDataAdapter da = new SqlDataAdapter(cmd2);

                dtBanOld.Clear();
                da.Fill(dtBanOld);

                dgvBanOld.DataSource = dtBanOld;
            }
        }

        void KhoiTaoBanMoi()
        {
            dtBanNew = new DataTable();

            dtBanNew.Columns.Add("MaMon");
            dtBanNew.Columns.Add("TenMon");
            dtBanNew.Columns.Add("SoLuong", typeof(int));
            dtBanNew.Columns.Add("DonGia", typeof(decimal));
            dtBanNew.Columns.Add("ThanhTien", typeof(decimal), "SoLuong * DonGia");

            dgvBanNew.DataSource = dtBanNew;
        }

        private void btnChuyenMon_Click(object sender, EventArgs e)
        {
            if (dgvBanOld.CurrentRow == null) return;

            DataTable dtOld = (DataTable)dgvBanOld.DataSource;
            DataTable dtNew = (DataTable)dgvBanNew.DataSource;

            string maMon = dgvBanOld.CurrentRow.Cells["MaMon"].Value.ToString();
            string tenMon = dgvBanOld.CurrentRow.Cells["TenMon"].Value.ToString();
            int soLuongOld = Convert.ToInt32(dgvBanOld.CurrentRow.Cells["SoLuong"].Value);

            if (soLuongOld <= 0) return;

            // ===== GIẢM BÊN CŨ =====
            DataRow rowOld = dtOld.Rows[dgvBanOld.CurrentRow.Index];
            rowOld["SoLuong"] = soLuongOld - 1;

            if ((int)rowOld["SoLuong"] <= 0)
                dtOld.Rows.Remove(rowOld);

            // ===== THÊM BÊN MỚI =====
            DataRow[] found = dtNew.Select($"MaMon = '{maMon}'");

            if (found.Length > 0)
            {
                found[0]["SoLuong"] = Convert.ToInt32(found[0]["SoLuong"]) + 1;
            }
            else
            {
                DataRow newRow = dtNew.NewRow();
                newRow["MaMon"] = maMon;
                newRow["TenMon"] = tenMon;
                newRow["SoLuong"] = 1;
                dtNew.Rows.Add(newRow);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (dgvBanNew.Rows.Count == 0)
            {
                MessageBox.Show("Chưa có món để tách!");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                try
                {
                    DataTable dtOld = new DataTable();
                    dtOld.Columns.Add("MaMon", typeof(string));
                    dtOld.Columns.Add("SoLuong", typeof(int));

                    foreach (DataGridViewRow row in dgvBanOld.Rows)
                    {
                        if (row.IsNewRow) continue;

                        int soLuong = Convert.ToInt32(row.Cells["SoLuong"].Value);
                        if (soLuong < 0) soLuong = 0;

                        dtOld.Rows.Add(
                            row.Cells["MaMon"].Value.ToString(),
                            soLuong
                        );
                    }

                    DataTable dtNew = new DataTable();
                    dtNew.Columns.Add("MaMon", typeof(string));
                    dtNew.Columns.Add("SoLuong", typeof(int));

                    foreach (DataGridViewRow row in dgvBanNew.Rows)
                    {
                        if (row.IsNewRow) continue;

                        int soLuong = Convert.ToInt32(row.Cells["SoLuong"].Value);
                        if (soLuong <= 0) continue;

                        dtNew.Rows.Add(
                            row.Cells["MaMon"].Value.ToString(),
                            soLuong
                        );
                    }

                    SqlCommand cmd = new SqlCommand("sp_TachBan", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@MaHoaDonCu", maHoaDonCu);
                    cmd.Parameters.AddWithValue("@MaBanMoi", maBanMoi);
                    cmd.Parameters.AddWithValue("@MaNhanVien", "NVNV1");

                    SqlParameter pOld = cmd.Parameters.AddWithValue("@DSMonCu", dtOld);
                    pOld.SqlDbType = SqlDbType.Structured;
                    pOld.TypeName = "dbo.ChiTietHoaDonType"; // 🔥 QUAN TRỌNG

                    SqlParameter pNew = cmd.Parameters.AddWithValue("@DSMonMoi", dtNew);
                    pNew.SqlDbType = SqlDbType.Structured;
                    pNew.TypeName = "dbo.ChiTietHoaDonType"; // 🔥 QUAN TRỌNG

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Tách bàn thành công!");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmTachBan_Load(object sender, EventArgs e)
        {
            LoadHoaDonCu();
            KhoiTaoBanMoi();
        }
    }
}
