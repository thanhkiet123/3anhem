using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUANLYNHAHANG
{
    /// Business Logic Layer - Quản lý Danh mục món ăn
    internal class BLL_DanhMuc
    {
        private BanHang db = new BanHang();

        // =============================================
        // LẤY DANH SÁCH TẤT CẢ DANH MỤC
        // =============================================
        public DataTable LayDanhSachDanhMuc()
        {
            return db.LayDuLieu("sp_Admin_LayDanhSachDanhMuc", null);
        }

        // =============================================
        // THÊM DANH MỤC MỚI
        // =============================================
        /// <summary>
        /// Thêm danh mục mới
        /// </summary>
        /// <param name="maDanhMuc">Mã danh mục (VD: DMKV, DMMC)</param>
        /// <param name="tenDanhMuc">Tên danh mục</param>
        /// <param name="hinhAnh">Đường dẫn hình ảnh</param>
        /// <param name="moTa">Mô tả ngắn</param>
        /// <param name="trangThai">1: Hiển thị, 0: Ẩn</param>
        public int ThemDanhMuc(string maDanhMuc, string tenDanhMuc, string hinhAnh, string moTa, int trangThai)
        {
            List<SqlParameter> pars = new List<SqlParameter>
            {
                new SqlParameter("@MaDanhMuc",  maDanhMuc),
                new SqlParameter("@TenDanhMuc", tenDanhMuc),
                new SqlParameter("@HinhAnh",    hinhAnh    ?? ""),
                new SqlParameter("@MoTa",       moTa       ?? ""),
                new SqlParameter("@TrangThai",  trangThai)
            };
            return db.XuLy("sp_Admin_ThemDanhMuc", pars);
        }

        // =============================================
        // SỬA DANH MỤC
        // =============================================
        public int SuaDanhMuc(string maDanhMuc, string tenDanhMuc, string hinhAnh, string moTa, int trangThai)
        {
            List<SqlParameter> pars = new List<SqlParameter>
            {
                new SqlParameter("@MaDanhMuc",  maDanhMuc),
                new SqlParameter("@TenDanhMuc", tenDanhMuc),
                new SqlParameter("@HinhAnh",    hinhAnh    ?? ""),
                new SqlParameter("@MoTa",       moTa       ?? ""),
                new SqlParameter("@TrangThai",  trangThai)
            };
            return db.XuLy("sp_Admin_SuaDanhMuc", pars);
        }

        // =============================================
        // XOÁ DANH MỤC (SOFT DELETE + kiểm tra FK)
        // =============================================
        /// <summary>
        /// SP sẽ từ chối xoá nếu danh mục còn món ăn đang hoạt động
        /// </summary>
        public int XoaDanhMuc(string maDanhMuc)
        {
            List<SqlParameter> pars = new List<SqlParameter>
            {
                new SqlParameter("@MaDanhMuc", maDanhMuc)
            };
            return db.XuLy("sp_Admin_XoaDanhMuc", pars);
        }

        public DataTable TimDanhMuc(string keyword)
        {
            using (SqlConnection conn = new SqlConnection(@"Data Source=.\MSSQLSERVER1;Initial Catalog=QLNH;Integrated Security=True"))
            {
                SqlCommand cmd = new SqlCommand("sp_TimDanhMuc", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Keyword", keyword);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
        }
    }
}
