using NationalNeon.Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalNeon.Business.Interfaces
{
   public interface IUserBusiness
    {
        UserModel GetUser(int userId);

        UserModel GetLoginUser(string username, string password);
        bool IsValidUser(string email, string password);
        List<UserModel> GetAllUser();
        UserModel AddNewUser(UserModel model);

        void DeleteUsers(int userId);

        void Update(UserModel model);

        void passwordUpdate(string password,int userId);
    }
}
