using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using System.Data;

namespace BLL
{
    public class NguoiDungBLL
    {
        NguoiDungDAL nguoiDungDAL = new NguoiDungDAL();
        private NguoiDungDAL dal;
        public NguoiDungBLL() {
            dal = new NguoiDungDAL();
        }

        public NguoiDung ValidateUser(string username, string password, out string message)
        {
            message = string.Empty;
            var user = nguoiDungDAL.Login(username, password);

            if (user == null)
            {
                message = "Tên đăng nhập hoặc mật khẩu không đúng.";
                return null;
            }

            if (!user.KichHoat.GetValueOrDefault())
            {
                message = "Tài khoản của bạn đã bị khóa.";
                return null;
            }

            return user;
        }



        public List<string> GetUserPermissions(int userId)
        {
            return nguoiDungDAL.GetUserPermissions(userId);
        }

        public List<NguoiDung> getAllNguoiDungBLL()
        {
            return nguoiDungDAL.getAllNguoiDungDAL();
        }

        // Code thuan`
        public DataTable GetAllNguoiDung()
        {
            return dal.GetAllNguoiDung();
        }

        public DataTable GetAllNhomNguoiDung()
        {
            return dal.GetAllNhomNguoiDung();
        }

        public string GetTenNhomNguoiDungById(int maNhomNguoiDung)
        {
            return dal.GetTenNhomNguoiDungById(maNhomNguoiDung);
        }

        public bool CheckUnique(string tenDangNhap, string email)
        {
            return dal.CheckUnique(tenDangNhap, email);
        }

        public void InsertNguoiDung(DTO.NguoiDung nguoiDung)
        {
            dal.InsertNguoiDung(nguoiDung);
        }

        public bool CheckUnique(string tenDangNhap, string email, int nguoiDungID)
        {
            return dal.CheckUnique(tenDangNhap, email, nguoiDungID);
        }

        public void UpdateNguoiDung(DTO.NguoiDung nguoiDung)
        {
            dal.UpdateNguoiDung(nguoiDung);
        }
        public void DisableNguoiDung(int nguoiDungID)
        {
            dal.DisableNguoiDung(nguoiDungID);
        }
    }
}

