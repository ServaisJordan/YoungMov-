using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using model;
using DAO;

namespace DAL
{
    public class DataAccess : Dao
    {
        private readonly smartCityContext context;
        public DataAccess(smartCityContext context) {
            this.context = context;
        }

        #region User
        public async Task<IEnumerable<User>> GetUsers(int pageSize, int pageIndex, string filter) {
            return await context.User.Skip(pageSize*pageIndex)
                                    .Take(pageSize)
                                    .Where(u => filter == null || u.UserName.Contains(filter))
                                    .ToListAsync();
        }

        public async Task<User> GetUser(int id) {
            return await IncludeQuery().FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetUser(string userName) {
            return await IncludeQuery().FirstOrDefaultAsync(u => u.UserName == userName);
        }

        public Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<User, ICollection<Car>> IncludeQuery() {
            return context.User.Include(user => user.Carpooling)
                                    .Include(user => user.PrivateMessage)
                                    .ThenInclude(p => p.ReponseNavigation)
                                    .Include(user => user.Car);
        } 

        public async Task<User> GetUser(string userName, string password) {
            return await context.User.Select(u => new User() {
                UserName = u.UserName,
                Password = u.Password,
                Role = u.Role
            }).FirstOrDefaultAsync(user => user.UserName == userName && user.Password == password);
        }


        public User AddUser(User user) {
            context.User.Add(user);
            context.SaveChanges();
            return user;
        }


        public User SetUser(User user) {
            
            if (context.Entry(user).State == EntityState.Detached)
            {
                context.Attach(user).State = EntityState.Modified;
            }

            context.SaveChanges();
            return user;
        }


        public async Task RemoveUser(int id) {
            User user = await GetUser(id);
            if (user != null) {
                context.Remove(user);
                context.SaveChanges();
            }
        }
        #endregion

        #region Carpooling
        public async Task<IEnumerable<Carpooling>> GetCarpoolings(int pageSize, int pageIndex, string filter) => 
        await context.Carpooling.Skip(pageSize * pageIndex)
                                .Take(pageSize)
                                .Where(c => filter == null || c.LocalityFrom.Contains(filter))
                                .ToListAsync();

        public async Task<Carpooling> GetCarpooling(int id) =>
             await context.Carpooling.Include(c => c.Creator)
                                    .FirstOrDefaultAsync();

        public Carpooling AddCarpooling(Carpooling carpooling) {
            context.Carpooling.Add(carpooling);
            context.SaveChanges();
            return carpooling;
        }

        public Carpooling SetCarpooling(Carpooling carpooling) {
            if (context.Entry(carpooling).State == EntityState.Detached) {
                context.Attach(carpooling).State = EntityState.Modified;
            }
            context.SaveChanges();
            return carpooling;
        }
        public async Task RemoveCarpooling(int id) {
            Carpooling carpooling = await GetCarpooling(id);
            if (carpooling != null) {
                context.Remove(carpooling);
                context.SaveChanges();
            }
        }
        #endregion
    }
}
