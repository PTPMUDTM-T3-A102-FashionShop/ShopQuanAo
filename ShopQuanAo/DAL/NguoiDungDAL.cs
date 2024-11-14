using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class NguoiDungDAL
    {
        DoAnKetMon_UDTMDataContext db = new DoAnKetMon_UDTMDataContext();

        public NguoiDungDAL() { }

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
    }
}

