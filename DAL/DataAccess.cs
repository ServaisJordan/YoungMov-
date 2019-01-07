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
        public async Task<IEnumerable<User>> GetUsers(int? pageSize, int pageIndex, string userNameFilter) {
            var query = context.User.Where(u => userNameFilter == null || u.UserName.Contains(userNameFilter));
            if (pageSize != null) query.Skip((int) pageSize * pageIndex).Take((int)pageSize);
            return await query.ToListAsync();
        }

        public async Task<User> GetUser(string id) {
            return await IncludeQuery().FirstOrDefaultAsync(u => u.Id == id.ToString());
        }

        /* public async Task<User> GetUser(string userName) {
            return await IncludeQuery().FirstOrDefaultAsync(u => u.UserName == userName);
        } */

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
                PasswordHash = u.PasswordHash,
                Role = u.Role
            }).FirstOrDefaultAsync(user => user.UserName == userName && user.PasswordHash == password && user.Role == role);
        }


        public async Task<User> AddUser(User user) {
            await context.User.AddAsync(user);
            await context.SaveChangesAsync();
            return user;
        }


        public async Task<User> SetUser(User user, byte[] timestamp) {
            context.Entry(user).OriginalValues["Timestamp"] = timestamp;
            if (context.Entry(user).State == EntityState.Detached)
            {
                context.Attach(user).State = EntityState.Modified;
            }

            await context.SaveChangesAsync();
            return user;
        }


        public async Task RemoveUser(User user) {
            
            context.User.Remove(user);
            await context.SaveChangesAsync();
        }
        #endregion User

        #region Carpooling
        public async Task<IEnumerable<Carpooling>> GetCarpoolings(int? pageSize, int pageIndex, string filterFrom, string filterTo) {
            var query = context.Carpooling.Where(c => filterFrom == null || c.LocalityFrom.Contains(filterFrom))
                                        .Where(c => filterTo == null || c.LocalityTo.Contains(filterTo))
                                        .Include(c => c.CreatorNavigation);
            if (pageSize != null) query.Skip((int)pageSize * pageIndex).Take((int) pageSize);
            return await query.ToListAsync();

        }

        public async Task<Carpooling> GetCarpooling(int id) =>
             await context.Carpooling.Include(c => c.Creator)
                                    .Include(c => c.CarpoolingApplicant).ThenInclude(c => c.UserNavigation)
                                    .Include(c => c.Car)
                                    .FirstOrDefaultAsync(c => c.Id == id);

        public async Task<Carpooling> AddCarpooling(Carpooling carpooling) {
            await context.Carpooling.AddAsync(carpooling);
            await context.SaveChangesAsync();
            return carpooling;
        }

        public async Task<Carpooling> SetCarpooling(Carpooling carpooling, byte[] timestamp) {
            context.Entry(carpooling).OriginalValues["Timestamp"] = timestamp;
            if (context.Entry(carpooling).State == EntityState.Detached) {
                context.Attach(carpooling).State = EntityState.Modified;
            }
            await context.SaveChangesAsync();
            return carpooling;
        }
        public async Task RemoveCarpooling(Carpooling carpooling) {
            context.Remove(carpooling);
            await context.SaveChangesAsync();
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
        public async Task<ICollection<Car>> GetCars(int pageIndex , int? pageSize) {
            var query = context.Car;
            if (pageSize != null) query.Skip((int) pageSize * pageIndex).Take(((int) pageSize));
            return await query.ToListAsync();
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

        public async Task RemoveCar(Car car) {
            context.Car.Remove(car);
            await context.SaveChangesAsync();
        }
        #endregion Car


        #region PrivateMessage
        public async Task<ICollection<PrivateMessage>> GetPrivateMessages(int pageIndex, int? pageSize) {
            var query = context.PrivateMessage;
            if (pageSize != null) query.Skip((int) pageSize * pageIndex).Take((int) pageSize);
            return await query.ToListAsync();
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

        public async Task RemovePrivateMessage(PrivateMessage privateMessage) {
            context.PrivateMessage.Remove(privateMessage);
            await context.SaveChangesAsync();
        }
        #endregion PrivateMessage

        #region TrustedCarpoolingDriver
        public async Task<TrustedCarpoolingDriver> GetTrustedCarpoolingDriver(int id) {
            return await context.TrustedCarpoolingDriver.SingleOrDefaultAsync(t => t.Id == id);
        }

        public async Task<TrustedCarpoolingDriver> AddTrustedCarpoolingDriver(TrustedCarpoolingDriver trustedCarpoolingDriver) {
            context.TrustedCarpoolingDriver.Add(trustedCarpoolingDriver);
            await context.SaveChangesAsync();
            return trustedCarpoolingDriver;
        }

        public async Task RemoveTrustedCarpoolingDriver(TrustedCarpoolingDriver trustedCarpooling) {
            context.TrustedCarpoolingDriver.Remove(trustedCarpooling);
            await context.SaveChangesAsync();
        }
        #endregion TrustedCarpoolingDriver


        #region CarpoolingApplicant
        public async Task<CarpoolingApplicant> GetCarpoolingApplicant(int id) {
            return await context.CarpoolingApplicant.SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<CarpoolingApplicant> AddCarpoolingApplicant(CarpoolingApplicant carpoolingApplicant) {
            context.CarpoolingApplicant.Add(carpoolingApplicant);
            await context.SaveChangesAsync();
            return carpoolingApplicant;
        }

        public async Task<CarpoolingApplicant> SetCarpoolingApplicant(CarpoolingApplicant carpoolingApplicant) {
            if (context.Entry(carpoolingApplicant).State == EntityState.Detached) {
                context.Attach(carpoolingApplicant).State = EntityState.Modified;
            }
            await context.SaveChangesAsync();
            return carpoolingApplicant;
        }

        public async Task RemoveCarpoolingApplicant(CarpoolingApplicant carpoolingApplicant) {
            context.CarpoolingApplicant.Remove(carpoolingApplicant);
            await context.SaveChangesAsync();
        }
        #endregion CarpoolingApplicant
    }
}
