using DSM.Models.Entities;
using DSM.Repository.Data;
using DSM.Repository.Interface;

namespace DSM.Repository.Implementations
{
    public class UserRepository : IUserRepository
    {
      
        private readonly ApplicationDbContext _context;
        public UserRepository
        (
            ApplicationDbContext context
        )
        {
            _context = context;
        }


        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }


        public User? GetById(int id)
        {
            return _context.Users
                .FirstOrDefault(x =>
                    x.UserId == id);
        }

        public void Add(User user)
        {
            _context.Users.Add(user);

            _context.SaveChanges();
        }

        public void Update(User user)
        {
            _context.Users.Update(user);

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var data = _context.Users
                .FirstOrDefault(x =>
                    x.UserId == id);

            if (data != null)
            {
                _context.Users.Remove(data);

                _context.SaveChanges();
            }
        }

       
        public User? Login
        (
            string email,
            string password
        )
        {
            return _context.Users
                .FirstOrDefault(x =>
                    x.Email == email &&
                    x.Password == password);
        }
    }
}