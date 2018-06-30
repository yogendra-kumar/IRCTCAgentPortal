using System;
using Mpower.Rail.Data;
using Mpower.Rail.Data.IRepository;
using Mpower.Rail.Data.Repository;
using Mpower.Rail.Model.Request;
using Mpower.Rail.Model.Rail;
using System.Linq;
using System.Collections.Generic;

namespace Mpower.Rail.Processor.Travel
{
    public class PassengerProcessor : IDisposable
    {
        private IPassengerRepository _passengerRepository;
        public PassengerProcessor(ApplicationDbContext dbcontext)
        {
            _passengerRepository = new PassengerRepository(dbcontext);
        }

        /// <summary>
        /// This Method will Create New Passenger 
        /// </summary>
        /// <param name="req">req is an object type of Passenger class</param>
        /// <returns>this will return Passengers object</returns>
        public Passengers CreatePassenger(Passenger req)
        {
            Passengers _passenger = new Passengers();
            _passenger.loginAccount = req.loginAccount;
            _passenger.name = req.name;
            _passenger.bDay = req.bDay;
            _passenger.bMonth = req.bMonth;
            _passenger.bYear = req.bYear;
            _passenger.sex = req.sex;
            _passenger.birthPf = req.birthPreferance;
            _passenger.foodPf = req.foodPreferance;
           _passenger.idCardTypeId=req.idCardTypeId;
            _passenger.idCardNumber=req.idCardNumber;
            _passenger.senior = req.senior;
            _passengerRepository.Add(_passenger);
            _passengerRepository.Commit();
            return _passenger;
        }

        /// <summary>
        /// This Method will Update Existing Passenger
        /// </summary>
        /// <param name="req">req is an object type of Passenger class</param>
        /// <returns>this will return Passengers object</returns>
        public Passengers UpdatePassenger(Passenger req)
        {
            Passengers _passenger = _passengerRepository.FindBy(m => m.Id == req.passengerId).FirstOrDefault();
            _passenger.name = req.name;
            _passenger.bDay = req.bDay;
            _passenger.bMonth = req.bMonth;
            _passenger.bYear = req.bYear;
            _passenger.sex = req.sex;
            _passenger.birthPf = req.birthPreferance;
            _passenger.foodPf = req.foodPreferance;
            _passenger.senior = req.senior;
            _passengerRepository.Update(_passenger);
            _passengerRepository.Commit();
            return _passenger;
        }

        /// <summary>
        /// This Method will Delete Existing Passenger
        /// </summary>
        /// <param name="passengerId">it accept passenger id</param>
        /// <returns>i will return teturn true if passenger deleted </returns>
        public bool DeletePassenger(long passengerId)
        {
            Passengers _passenger = _passengerRepository.FindBy(m => m.Id == passengerId).FirstOrDefault();
            if (_passenger != null)
            {
                _passengerRepository.DeleteWhere(m => m.Id == passengerId);
                _passengerRepository.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// this method return all passenger of that RO
        /// </summary>
        /// <param name="loginAccount">it accept login account number</param>
        /// <returns>it return list of passengers</returns>
        public List<Passengers> GetAllPassenger(string loginAccount)
        {
            List<Passengers> lstpassenger=_passengerRepository.FindBy(m=>m.loginAccount==loginAccount).ToList();
            return lstpassenger;
        }


        

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

    }
}