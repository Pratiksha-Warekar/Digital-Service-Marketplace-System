using DSM.Models.Entities;

namespace DSM.Repository.Interface
{
    public interface IUserRepository
    {
        
        List<User> GetAll();

        User? GetById(int id);

        void Add(User user);

       
        void Update(User user);

       
        void Delete(int id);

      
        User? Login
        (
            string email,
            string password
        );
    }
}