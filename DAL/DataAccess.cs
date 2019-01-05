using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using model;
using DAO;
using Exceptions;

namespace DAL
{
    public class DataAccess : Dao
    {
        private readonly YoungMovContext context;
        public DataAccess(YoungMovContext context) {
            this.context = context;
        }

        #region User
        public async Task<IEnumerable<User>> GetUsers(int pageSize, int pageIndex, string userNameFilter) {
            return await context.User.Skip(pageSize*pageIndex)
                                    .Take(pageSize)
                                    .Where(u => userNameFilter == null || u.UserName.Contains(userNameFilter))
                                    .ToListAsync();
        }

        public async Task<User> GetUser(int id) {
            return await IncludeQuery().FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetUser(string userName) {
            return await IncludeQuery().FirstOrDefaultAsync(u => u.UserName == userName);
        }

        public Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<User, ICollection<Car>> IncludeQuery() {
            return context.User.Include(user => user.Carpooling).ThenInclude(c => c.CarNavigation)
                                .Include(user => user.CarpoolingApplicant)
                                .Include(user => user.TrustedCarpoolingDriverUserNavigation)
                                .Include(user => user.PrivateMessage).ThenInclude(p => p.ReponseNavigation)
                                .Include(user => user.Car);
        } 

        public async Task<User> GetUser(string userName, string password, string role) {
            return await context.User.Select(u => new User() {
                UserName = u.UserName,
                Password = u.Password,
                Role = u.Role
            }).FirstOrDefaultAsync(user => user.UserName == userName && user.Password == password && user.Role == role);
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
            User user = await context.User.Include(u => u.Car).Include(u => u.Carpooling).ThenInclude(c => c.CarpoolingApplicant).FirstOrDefaultAsync(u => u.Id == id);
            if (user != null) {
                context.User.Remove(user);
                context.SaveChanges();
            }
        }
        #endregion User

        #region Carpooling
        public async Task<IEnumerable<Carpooling>> GetCarpoolings(int pageSize, int pageIndex, string filterFrom, string filterTo) => 
        await context.Carpooling.Skip(pageSize * pageIndex)
                                .Take(pageSize)
                                .Where(c => filterFrom == null || c.LocalityFrom.Contains(filterFrom))
                                .Where(c => filterTo == null || c.LocalityTo.Contains(filterTo))
                                .ToListAsync();

        public async Task<Carpooling> GetCarpooling(int id) =>
             await context.Carpooling.Include(c => c.Creator)
                                    .Include(c => c.CarpoolingApplicant).ThenInclude(c => c.UserNavigation)
                                    .Include(c => c.Car)
                                    .FirstOrDefaultAsync(c => c.Id == id);

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
                await context.SaveChangesAsync();
            }
        }
        #endregion Carpooling

        #region NumberOfUsers
        public async Task<int> GetNumberOfUsers(DateTime? date, char? gender) {
            var users = await context.User.Where(u => date == null || DateTime.Compare((DateTime) date, (DateTime) u.CreatedAt) < 0)
                                        .Where(u => gender == null || u.Gender == gender.ToString())
                                        .ToListAsync();
            return users.Count();
        }
        #endregion NumberOfUsers


        #region Car

        public async Task<ICollection<Car>> GetCars(int pageIndex , int pageSize) {
            return await context.Car.Skip(pageIndex * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();
        }
        public async Task<Car> GetCar(int id) {
            return await context.Car.Include(c => c.OwnerNavigation)
                                    .SingleOrDefaultAsync(c => c.Id == id);
        }
        public async Task<Car> AddCar(Car car) {
            context.Car.Add(car);
            await context.SaveChangesAsync();
            return car;
        }

        public async Task<Car> SetCar(Car car) {
            if (context.Entry(car).State == EntityState.Detached)
                context.Attach(car).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return car;
        }

        public async Task RemoveCar(int id) {
            Car car = await GetCar(id);
            context.Car.Remove(car);
            await context.SaveChangesAsync();
        }
        #endregion Car


        #region PrivateMessage
        public async Task<ICollection<PrivateMessage>> GetPrivateMessages(int pageIndex, int pageSize) {
            return await context.PrivateMessage.Skip(pageIndex * pageSize)
                                            .Take(pageSize)
                                            .ToListAsync();
        }

        public async Task<PrivateMessage> GetPrivateMessage(int id) {
            return await context.PrivateMessage.Include(p => p.CreatorNavigation)
                                            .SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<PrivateMessage> AddPrivateMessage(PrivateMessage privateMessage) {
            context.PrivateMessage.Add(privateMessage);
            await context.SaveChangesAsync();
            return privateMessage;
        }

        public async Task<PrivateMessage> SetPrivateMessage(PrivateMessage privateMessage) {
            if (context.Entry(privateMessage).State == EntityState.Detached)
                context.Attach(privateMessage).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return privateMessage;
        }

        public async Task RemovePrivateMessage(int id) {
            context.PrivateMessage.Remove(await GetPrivateMessage(id));
            await context.SaveChangesAsync();
        }
        #endregion PrivateMessage
    }
}
