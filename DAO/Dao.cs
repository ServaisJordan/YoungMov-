using System;
using model;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DAO
{
    public interface Dao
    {
        #region User
        Task<IEnumerable<User>> GetUsers(int pageSize, int pageIndex, string filter);
        Task<User> GetUser(string id);
        //Task<User> GetUser(string userName);
        Task<User> GetUser(string userName, string password, string role);
        Task<User> AddUser(User user);
        Task<User> SetUser(User user, byte[] timestamp);
        Task RemoveUser(User user);
        #endregion

        #region Carpooling
        Task<IEnumerable<Carpooling>> GetCarpoolings(int pageSize, int pageIndex, string filterFrom, string filterTo);
        Task<Carpooling> GetCarpooling(int id);
        Task<Carpooling> AddCarpooling(Carpooling carpooling);
        Task<Carpooling> SetCarpooling(Carpooling carpooling, byte[] timestamp);
        Task RemoveCarpooling(Carpooling carpooling);
        #endregion

        #region NumberOfUsers
        Task<int> GetNumberOfUsers(DateTime ?date, char ?genrder);
        #endregion

        #region Car
        Task<ICollection<Car>> GetCars(int pageIndex, int pageSize);
        Task<Car> GetCar(int id);
        Task<Car> AddCar(Car car);
        Task<Car> SetCar(Car car);
        Task RemoveCar(Car car);
        #endregion

        #region PrivateMessage
        Task<ICollection<PrivateMessage>> GetPrivateMessages(int pageIndex, int pageSize);
        Task<PrivateMessage> GetPrivateMessage(int id);
        Task<PrivateMessage> AddPrivateMessage(PrivateMessage privateMessage);
        Task<PrivateMessage> SetPrivateMessage(PrivateMessage privateMessage);
        Task RemovePrivateMessage(PrivateMessage privateMessage);
        #endregion PrivateMessage

        #region TrustedCarpoolingDriver
        Task<TrustedCarpoolingDriver> GetTrustedCarpoolingDriver(int id);
        Task<TrustedCarpoolingDriver> AddTrustedCarpoolingDriver(TrustedCarpoolingDriver trustedCarpoolingDriver);
        Task RemoveTrustedCarpoolingDriver(TrustedCarpoolingDriver trustedCarpoolingDriver);
        #endregion TrustedCarpoolingDriver

        #region  CarpoolingApplicant
        Task<CarpoolingApplicant> GetCarpoolingApplicant(int id);
        Task<CarpoolingApplicant> AddCarpoolingApplicant(CarpoolingApplicant carpoolingApplicant);
        Task<CarpoolingApplicant> SetCarpoolingApplicant(CarpoolingApplicant carpoolingApplicant);
        Task RemoveCarpoolingApplicant(CarpoolingApplicant carpoolingApplicant);
        #endregion CarpoolingApplicant
    }
}
