using ExpressMapper;
using ExpressMapper.Extensions;
using NationalNeon.Business.Interfaces;
using NationalNeon.Domain.User;
using NationalNeon.Repository;
using NationalNeon.Repository.DB;
using NationalNeon.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalNeon.Business.Concrete
{
    public class UserBusiness : IUserBusiness
    {
        private readonly UserRepository userRepository;

        public UserBusiness(IUnitOfWork unit)
        {
            userRepository = new UserRepository(unit);
        }

        public UserModel AddNewUser(UserModel model)
        {
            User user = new User();
            Mapper.Map(model, user);
            userRepository.Insert(user);
            Mapper.Map(user, model);
            return model;
        }

        public List<UserModel> GetAllUser()
        {
            var usermodel = new List<UserModel>();
            var model=userRepository.GetAll().ToList();
            return Mapper.Map(model, usermodel);
        }

        public UserModel GetUser(int userId)
        {
            return userRepository.SingleOrDefault(u => u.userId == userId).Map<User, UserModel>();
        }
        public bool IsValidUser(string email, string password)
        {
            return userRepository.Exists(u => u.username == email && u.password == password);
        }

        public void DeleteUsers(int Id)
        {
            userRepository.Delete(u => u.userId == Id);
        }

        public void Update(UserModel model)
        {
            var data = userRepository.FindBy(x => x.userId == model.userId);
            if (data != null)
            {
                data.username = model.username;
                data.updated_on = DateTime.Now;
                data.role = model.role;
                userRepository.Update(data);
            }
        }

        public UserModel GetLoginUser(string username, string password)
        {
            return userRepository.SingleOrDefault(u => u.username == username &&  u.password== password).Map<User, UserModel>();
        }

        public void passwordUpdate(string password, int userId)
        {
            var data = userRepository.FindBy(x => x.userId == userId);
            if (data != null)
            {
                data.password = password;
                userRepository.Update(data);
            }
        }
    }
}
