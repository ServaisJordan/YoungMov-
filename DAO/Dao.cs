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
        Task<User> GetUser(int id);
        Task<User> GetUser(string userName);
        Task<User> GetUser(string userName, string password, string role);
        User AddUser(User user);
        User SetUser(User user);
        Task RemoveUser(int id);
        #endregion

        #region Carpooling
        Task<IEnumerable<Carpooling>> GetCarpoolings(int pageSize, int pageIndex, string filterFrom, string filterTo);
        Task<Carpooling> GetCarpooling(int id);
        Carpooling AddCarpooling(Carpooling carpooling);
        Carpooling SetCarpooling(Carpooling carpooling);
        Task RemoveCarpooling(int id);
        #endregion

        #region NumberOfUsers
        Task<int> GetNumberOfUsers(DateTime ?date, char ?genrder);
        #endregion

        #region Car
        Task<ICollection<Car>> GetCars(int pageIndex, int pageSize);
        Task<Car> GetCar(int id);
        Task<Car> AddCar(Car car);
        Task<Car> SetCar(Car car);
        Task RemoveCar(int id);
        #endregion

        #region PrivateMessage
        Task<ICollection<PrivateMessage>> GetPrivateMessages(int pageIndex, int pageSize);
        Task<PrivateMessage> GetPrivateMessage(int id);
        Task<PrivateMessage> AddPrivateMessage(PrivateMessage privateMessage);
        Task<PrivateMessage> SetPrivateMessage(PrivateMessage privateMessage);
        Task RemovePrivateMessage(int id);
        #endregion PrivateMessage

        #region TrustedCarpoolingDriver
        Task<TrustedCarpoolingDriver> GetTrustedCarpoolingDriver(int id);
        Task<TrustedCarpoolingDriver> AddTrustedCarpoolingDriver(TrustedCarpoolingDriver trustedCarpoolingDriver);
        Task RemoveTrustedCarpoolingDriver(int id);
        #endregion TrustedCarpoolingDriver

        #region  CarpoolingApplicant
        Task<CarpoolingApplicant> GetCarpoolingApplicant(int id);
        Task<CarpoolingApplicant> AddCarpoolingApplicant(CarpoolingApplicant carpoolingApplicant);
        Task RemoveCarpoolingApplicant(int id);
        #endregion CarpoolingApplicant
    }
}
