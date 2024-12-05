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
    public class NguoiDungDAL
    {
        DoAnKetMon_UDTMDataContext db = new DoAnKetMon_UDTMDataContext();
        private DBConnection dbcon;
        public NguoiDungDAL() {
            dbcon = new DBConnection();
        }

        // Kiểm tra đăng nhập
        public NguoiDung Login(string username, string password)
        {
            var user = db.NguoiDungs
                         .Where(u => u.TenDangNhap == username && u.MatKhau == password)
                         .FirstOrDefault();
            return user;
        }

        public List<string> GetUserPermissions(int userId)
        {
            var userPermissions = (from pq in db.PhanQuyens
                                   join nd in db.NguoiDungs on pq.MaNhomNguoiDung equals nd.MaNhomNguoiDung
                                   where nd.NguoiDungID == userId
                                   select pq.MaManHinh).ToList();
            return userPermissions;
        }

        public List<NguoiDung> getAllNguoiDungDAL()
        {

            return db.NguoiDungs.Select(nd => nd).ToList<NguoiDung>();
        }

        // Code thuan`
        public DataTable GetAllNguoiDung()
        {
            DataTable dt = new DataTable();
            try
            {
                dbcon.conn.Open();
                dbcon.cmd.CommandText = "SELECT TenDangNhap, MatKhau, HoTen, Email, SoDienThoai, DiaChi, NgaySinh, MaNhomNguoiDung, GioiTinh, KichHoat FROM NguoiDung where Train = 0";
                SqlDataAdapter da = new SqlDataAdapter(dbcon.cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
            finally
            {
                dbcon.conn.Close();
            }
            return dt;
        }

        public DataTable GetAllNhomNguoiDung()
        {
            DataTable dt = new DataTable();
            try
            {
                dbcon.conn.Open();
                dbcon.cmd.CommandText = "SELECT MaNhomNguoiDung, TenNhomNguoiDung FROM NhomNguoiDung";
                SqlDataAdapter da = new SqlDataAdapter(dbcon.cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
            finally
            {
                dbcon.conn.Close();
            }
            return dt;
        }

        public string GetTenNhomNguoiDungById(int maNhomNguoiDung)
        {
            string tenNhomNguoiDung = string.Empty;
            try
            {
                dbcon.conn.Open();
                dbcon.cmd.CommandText = "SELECT TenNhomNguoiDung FROM NhomNguoiDung WHERE MaNhomNguoiDung = @MaNhomNguoiDung";
                dbcon.cmd.Parameters.AddWithValue("@MaNhomNguoiDung", maNhomNguoiDung);
                tenNhomNguoiDung = dbcon.cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
            finally
            {
                dbcon.conn.Close();
            }
            return tenNhomNguoiDung;
        }

        public bool CheckUnique(string tenDangNhap, string email)
        {
            bool isUnique = false;
            try
            {
                dbcon.conn.Open();
                string checkQuery = "SELECT COUNT(*) FROM NguoiDung WHERE TenDangNhap = @TenDangNhap OR Email = @Email";
                SqlCommand checkCmd = new SqlCommand(checkQuery, dbcon.conn);
                checkCmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                checkCmd.Parameters.AddWithValue("@Email", email);
                int count = (int)checkCmd.ExecuteScalar();
                isUnique = count == 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
            finally
            {
                dbcon.conn.Close();
            }
            return isUnique;
        }

        public void InsertNguoiDung(DTO.NguoiDung nguoiDung)
        {
            try
            {
                dbcon.conn.Open();
                string insertQuery = "INSERT INTO NguoiDung (TenDangNhap, MatKhau, HoTen, Email, SoDienThoai, DiaChi, NgaySinh, MaNhomNguoiDung, GioiTinh, KichHoat) " +
                                     "VALUES (@TenDangNhap, @MatKhau, @HoTen, @Email, @SoDienThoai, @DiaChi, @NgaySinh, @MaNhomNguoiDung, @GioiTinh, @KichHoat)";
                SqlCommand insertCmd = new SqlCommand(insertQuery, dbcon.conn);
                insertCmd.Parameters.AddWithValue("@TenDangNhap", nguoiDung.TenDangNhap);
                insertCmd.Parameters.AddWithValue("@MatKhau", nguoiDung.MatKhau);
                insertCmd.Parameters.AddWithValue("@HoTen", nguoiDung.HoTen);
                insertCmd.Parameters.AddWithValue("@Email", nguoiDung.Email);
                insertCmd.Parameters.AddWithValue("@SoDienThoai", nguoiDung.SoDienThoai);
                insertCmd.Parameters.AddWithValue("@DiaChi", nguoiDung.DiaChi);
                insertCmd.Parameters.AddWithValue("@NgaySinh", nguoiDung.NgaySinh);
                insertCmd.Parameters.AddWithValue("@MaNhomNguoiDung", nguoiDung.MaNhomNguoiDung);
                insertCmd.Parameters.AddWithValue("@GioiTinh", nguoiDung.GioiTinh);
                insertCmd.Parameters.AddWithValue("@KichHoat", nguoiDung.KichHoat);
                insertCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
            finally
            {
                dbcon.conn.Close();
            }

        }

        public bool CheckUnique(string tenDangNhap, string email, int nguoiDungID)
        {
            bool isUnique = false;
            try
            {
                dbcon.conn.Open();
                string checkQuery = "SELECT COUNT(*) FROM NguoiDung WHERE (TenDangNhap = @TenDangNhap OR Email = @Email) AND NguoiDungID != @NguoiDungID";
                SqlCommand checkCmd = new SqlCommand(checkQuery, dbcon.conn);
                checkCmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                checkCmd.Parameters.AddWithValue("@Email", email);
                checkCmd.Parameters.AddWithValue("@NguoiDungID", nguoiDungID);
                int count = (int)checkCmd.ExecuteScalar();
                isUnique = count == 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
            finally
            {
                dbcon.conn.Close();
            }
            return isUnique;
        }

        public void UpdateNguoiDung(DTO.NguoiDung nguoiDung)
        {
            try
            {
                dbcon.conn.Open();
                string updateQuery = "UPDATE NguoiDung SET TenDangNhap = @TenDangNhap, MatKhau = @MatKhau, HoTen = @HoTen, Email = @Email, SoDienThoai = @SoDienThoai, DiaChi = @DiaChi, NgaySinh = @NgaySinh, MaNhomNguoiDung = @MaNhomNguoiDung, GioiTinh = @GioiTinh, KichHoat = @KichHoat WHERE NguoiDungID = @NguoiDungID";
                SqlCommand updateCmd = new SqlCommand(updateQuery, dbcon.conn);
                updateCmd.Parameters.AddWithValue("@TenDangNhap", nguoiDung.TenDangNhap);
                updateCmd.Parameters.AddWithValue("@MatKhau", nguoiDung.MatKhau);
                updateCmd.Parameters.AddWithValue("@HoTen", nguoiDung.HoTen);
                updateCmd.Parameters.AddWithValue("@Email", nguoiDung.Email);
                updateCmd.Parameters.AddWithValue("@SoDienThoai", nguoiDung.SoDienThoai);
                updateCmd.Parameters.AddWithValue("@DiaChi", nguoiDung.DiaChi);
                updateCmd.Parameters.AddWithValue("@NgaySinh", nguoiDung.NgaySinh);
                updateCmd.Parameters.AddWithValue("@MaNhomNguoiDung", nguoiDung.MaNhomNguoiDung);
                updateCmd.Parameters.AddWithValue("@GioiTinh", nguoiDung.GioiTinh);
                updateCmd.Parameters.AddWithValue("@KichHoat", nguoiDung.KichHoat);
                updateCmd.Parameters.AddWithValue("@NguoiDungID", nguoiDung.NguoiDungID);
                updateCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
            finally
            {
                dbcon.conn.Close();
            }
        }

        public void DisableNguoiDung(int nguoiDungID)
        {
            try
            {
                dbcon.conn.Open();
                string updateQuery = "UPDATE NguoiDung SET KichHoat = 0 WHERE NguoiDungID = @NguoiDungID";
                SqlCommand updateCmd = new SqlCommand(updateQuery, dbcon.conn);
                updateCmd.Parameters.AddWithValue("@NguoiDungID", nguoiDungID);
                updateCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
            finally
            {
                dbcon.conn.Close();
            }
        }
    }
}

