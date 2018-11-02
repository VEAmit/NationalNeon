using NationalNeon.Repository.Concrete;
using NationalNeon.Repository.Interface;
using NationalNeon.Repository.DB;


namespace NationalNeon.Repository
{
   public class UserRepository : BaseRepository<User>
    {
        public UserRepository(IUnitOfWork unit) : base(unit)
        { }
       
    }
}
