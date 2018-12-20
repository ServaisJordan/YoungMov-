using System;
using model;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DAO
{
    public interface Dao
    {
        Task<IEnumerable<User>> GetUsers(int pageSize, int pageIndex, string filter);
        Task<User> GetUser(int id);
        Task<User> GetUser(string userName, string password);
        User AddUser(User user);
        User SetUser(User user);
        Task RemoveUser(int id);
        Task<IEnumerable<Carpooling>> GetCarpoolings(int pageSize, int pageIndex, string filter);
        Task<Carpooling> GetCarpooling(int id);
        Carpooling AddCarpooling(Carpooling carpooling);
        Carpooling SetCarpooling(Carpooling carpooling);
        Task RemoveCarpooling(int id);
    }
}
