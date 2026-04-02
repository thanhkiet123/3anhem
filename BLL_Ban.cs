using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUANLYNHAHANG
{
    /// Business Logic Layer - Quản lý Bàn
    /// Xử lý toàn bộ nghiệp vụ liên quan đến bàn ăn
    internal class BLL_Ban
    {
        private BanHang db = new BanHang();

        // =============================================
        // LẤY DANH SÁCH TẤT CẢ BÀN
        // =============================================
        /// <summary>Lấy toàn bộ danh sách bàn từ CSDL</summary>
        public DataTable LayDanhSachBan()
        {
            // Không cần tham số - gọi SP trực tiếp
            return db.LayDuLieu("sp_Admin_LayDanhSachBan", null);
        }

        // =============================================
        // THÊM BÀN MỚI
        // =============================================
        /// <summary>
        /// Thêm một bàn mới vào CSDL
        /// </summary>
        /// <param name="maBan">Mã bàn (VD: BA1, BB2)</param>
        /// <param name="tenBan">Tên hiển thị của bàn</param>
        /// <param name="soChoNgoi">Số chỗ ngồi (phải > 0)</param>
        /// <param name="trangThai">0: Trống, 1: Có người</param>
        public int ThemBan(string maBan, string tenBan, int soChoNgoi, int trangThai)
        {
            List<SqlParameter> pars = new List<SqlParameter>
            {
                new SqlParameter("@MaBan",     maBan),
                new SqlParameter("@TenBan",    tenBan),
                new SqlParameter("@SoChoNgoi", soChoNgoi),
                new SqlParameter("@TrangThai", trangThai)
            };
            return db.XuLy("sp_Admin_ThemBan", pars);
        }

        // =============================================
        // SỬA THÔNG TIN BÀN
        // =============================================
        /// <summary>Cập nhật thông tin bàn theo MaBan</summary>
        public int SuaBan(string maBan, string tenBan, int soChoNgoi, int trangThai)
        {
            List<SqlParameter> pars = new List<SqlParameter>
            {
                new SqlParameter("@MaBan",     maBan),
                new SqlParameter("@TenBan",    tenBan),
                new SqlParameter("@SoChoNgoi", soChoNgoi),
                new SqlParameter("@TrangThai", trangThai)
            };
            return db.XuLy("sp_Admin_SuaBan", pars);
        }

        // =============================================
        // XOÁ BÀN (SOFT DELETE - đặt TrangThai = 0)
        // =============================================
        /// <summary>Xoá mềm bàn - chỉ đặt TrangThai = 0, không xoá khỏi DB</summary>
        public int XoaBan(string maBan)
        {
            List<SqlParameter> pars = new List<SqlParameter>
            {
                new SqlParameter("@MaBan", maBan)
            };
            return db.XuLy("sp_Admin_XoaBan", pars);
        }

        public DataTable TimBan(string keyword)
        {
            using (SqlConnection conn = new SqlConnection(@"Data Source=.\MSSQLSERVER1;Initial Catalog=QLNH;Integrated Security=True"))
            {
                SqlCommand cmd = new SqlCommand("sp_TimBan_All", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@keyword", keyword);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
        }
    }
}
