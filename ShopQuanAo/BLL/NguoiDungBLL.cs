using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
namespace BLL
{
    public class NguoiDungBLL
    {
        NguoiDungDAL nguoiDungDAL = new NguoiDungDAL();
        public NguoiDungBLL() { }

        public NguoiDung ValidateUser(string username, string password)
        {
            var user = nguoiDungDAL.Login(username, password);
            return user;
        }

        public List<string> GetUserPermissions(int userId)
        {
            return nguoiDungDAL.GetUserPermissions(userId);
        }
    }
}

