using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUANLYNHAHANG
{
    /// Business Logic Layer - Quản lý Món ăn
    internal class BLL_MonAn
    {
        private BanHang db = new BanHang();

        // =============================================
        // LẤY DANH SÁCH TẤT CẢ MÓN ĂN (có JOIN DanhMuc)
        // =============================================
        public DataTable LayDanhSachMonAn()
        {
            // SP trả về: MaMon, TenMon, GiaBan, MaDanhMuc, TenDanhMuc, HinhAnh, TrangThai
            return db.LayDuLieu("sp_Admin_LayTatCaMonAn", null);
        }

        // =============================================
        // LẤY DANH SÁCH DANH MỤC (dùng cho ComboBox)
        // =============================================
        public DataTable LayDanhSachDanhMuc()
        {
            return db.LayDuLieu("sp_Admin_LayDanhSachDanhMuc", null);
        }

        // =============================================
        // THÊM MÓN ĂN MỚI
        // =============================================
        /// <summary>
        /// Thêm món ăn mới vào CSDL
        /// </summary>
        /// <param name="maMon">Mã món (VD: MCM1, MDU2)</param>
        /// <param name="tenMon">Tên món ăn</param>
        /// <param name="giaBan">Giá bán (decimal)</param>
        /// <param name="donViTinh">Đơn vị tính (VD: Phần, Ly, Lon)</param>
        /// <param name="hinhAnh">Đường dẫn file hình ảnh</param>
        /// <param name="maDanhMuc">Mã danh mục tương ứng</param>
        /// <param name="trangThai">1: Đang bán, 0: Ngừng</param>
        public int ThemMonAn(string maMon, string tenMon, decimal giaBan,
                             string donViTinh, string hinhAnh, string maDanhMuc, int trangThai)
        {
            List<SqlParameter> pars = new List<SqlParameter>
            {
                new SqlParameter("@MaMon",      maMon),
                new SqlParameter("@TenMon",     tenMon),
                new SqlParameter("@GiaBan",     giaBan),
                new SqlParameter("@DonViTinh",  donViTinh  ?? ""),
                new SqlParameter("@HinhAnh",    hinhAnh    ?? ""),
                new SqlParameter("@MaDanhMuc",  maDanhMuc),
                new SqlParameter("@TrangThai",  trangThai)
            };
            return db.XuLy("sp_Admin_ThemMonAn", pars);
        }

        // =============================================
        // SỬA THÔNG TIN MÓN ĂN
        // =============================================
        public int SuaMonAn(string maMon, string tenMon, decimal giaBan,
                            string donViTinh, string hinhAnh, string maDanhMuc, int trangThai)
        {
            List<SqlParameter> pars = new List<SqlParameter>
            {
                new SqlParameter("@MaMon",      maMon),
                new SqlParameter("@TenMon",     tenMon),
                new SqlParameter("@GiaBan",     giaBan),
                new SqlParameter("@DonViTinh",  donViTinh  ?? ""),
                new SqlParameter("@HinhAnh",    hinhAnh    ?? ""),
                new SqlParameter("@MaDanhMuc",  maDanhMuc),
                new SqlParameter("@TrangThai",  trangThai)
            };
            return db.XuLy("sp_Admin_SuaMonAn", pars);
        }

        // =============================================
        // XOÁ MÓN ĂN (SOFT DELETE - TrangThai = 0)
        // =============================================
        public int XoaMonAn(string maMon)
        {
            List<SqlParameter> pars = new List<SqlParameter>
            {
                new SqlParameter("@MaMon", maMon)
            };
            return db.XuLy("sp_Admin_XoaMonAn", pars);
        }

        public DataTable TimKiemMonAn(string keyword, string maDanhMuc, int? trangThai)
        {
            List<SqlParameter> pars = new List<SqlParameter>
    {
        new SqlParameter("@Keyword", (object)keyword ?? DBNull.Value),
        new SqlParameter("@MaDanhMuc", (object)maDanhMuc ?? DBNull.Value),
        new SqlParameter("@TrangThai", (object)trangThai ?? DBNull.Value)
    };

            return db.LayDuLieu("sp_Admin_TimKiemMonAn", pars);
        }
    }
}