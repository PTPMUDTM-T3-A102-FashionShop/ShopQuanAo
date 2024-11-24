using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using DB;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DonHangDAL
    {
        private DBConnection dbConnection;

        public DonHangDAL()
        {
            dbConnection = new DBConnection();
        }

        public int InsertDonHang(DonHangDTO donHang)
        {
            dbConnection.OpenConnection();
            SqlTransaction transaction = dbConnection.conn.BeginTransaction();

            try
            {
                string query = @"
                INSERT INTO DonHang (NguoiDungID, TongTien, TinhTrangDonHang, NgayDatHang, HinhThucThanhToan, TinhTrangThanhToan, NgayThanhToan)
                VALUES (@NguoiDungID, @TongTien, @TinhTrangDonHang, GETDATE(), @HinhThucThanhToan, @TinhTrangThanhToan, GETDATE());
                SELECT SCOPE_IDENTITY();";

                SqlCommand cmd = new SqlCommand(query, dbConnection.conn, transaction);
                cmd.Parameters.AddWithValue("@NguoiDungID", donHang.NguoiDungID);
                cmd.Parameters.AddWithValue("@TongTien", donHang.TongTien);
                cmd.Parameters.AddWithValue("@TinhTrangDonHang", donHang.TinhTrangDonHang);
                cmd.Parameters.AddWithValue("@HinhThucThanhToan", donHang.HinhThucThanhToan);
                cmd.Parameters.AddWithValue("@TinhTrangThanhToan", donHang.TinhTrangThanhToan);

                int donHangID = Convert.ToInt32(cmd.ExecuteScalar());
                transaction.Commit();
                return donHangID;
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                dbConnection.CloseConnection();
            }
        }

        public void InsertChiTietDonHang(ChiTietDonHangDTO chiTietDonHang)
        {
            dbConnection.OpenConnection();
            SqlTransaction transaction = dbConnection.conn.BeginTransaction();

            try
            {
                string query = @"
                INSERT INTO ChiTietDonHang (DonHangID, SanPhamID, SoLuong, DonGia)
                VALUES (@DonHangID, @SanPhamID, @SoLuong, @DonGia)";

                SqlCommand cmd = new SqlCommand(query, dbConnection.conn, transaction);
                cmd.Parameters.AddWithValue("@DonHangID", chiTietDonHang.DonHangID);
                cmd.Parameters.AddWithValue("@SanPhamID", chiTietDonHang.SanPhamID);
                cmd.Parameters.AddWithValue("@SoLuong", chiTietDonHang.SoLuong);
                cmd.Parameters.AddWithValue("@DonGia", chiTietDonHang.DonGia);

                cmd.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                dbConnection.CloseConnection();
            }
        }

        public void UpdateSoLuongTonKho(int chiTietID, int soLuong)
        {
            dbConnection.OpenConnection();
            SqlTransaction transaction = dbConnection.conn.BeginTransaction();

            try
            {
                string query = @"
                UPDATE ChiTietSanPham
                SET SoLuongTonKho = SoLuongTonKho - @SoLuong
                WHERE ChiTietID = @ChiTietID";

                SqlCommand cmd = new SqlCommand(query, dbConnection.conn, transaction);
                cmd.Parameters.AddWithValue("@SoLuong", soLuong);
                cmd.Parameters.AddWithValue("@ChiTietID", chiTietID);

                cmd.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                dbConnection.CloseConnection();
            }
        }
    }
}
