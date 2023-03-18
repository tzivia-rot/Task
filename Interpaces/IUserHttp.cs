using Tasks;
using System.Collections.Generic;
using System.Linq;
using Tasks.model;

namespace Tasks.Interfaces
{   
    public interface IUserHttp
    {    
        public User Login(User user);
        public List<User> GetAll();
        public void Add(User user);
        public bool Delete(int id);
    }
}