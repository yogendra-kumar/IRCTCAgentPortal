using System;
using Mpower.Rail.Data;
using Mpower.Rail.Data.IRepository;
using Mpower.Rail.Data.Repository;
//using Mpower.Rail.Model.Request;
using Mpower.Rail.Model.Rail;
using System.Linq;
using System.Collections.Generic;

namespace Mpower.Rail.Processor.Travel
{
    public class TravelListProcessor : IDisposable
    {
        private ITravelListRepository _travelListRepository;
        private IPassengerRepository _passengerRepository;

        private ITravelPassengerListRepository _travelpassengerlistRepository;

        public TravelListProcessor(ApplicationDbContext dbcontext)
        {
            _travelListRepository = new TravelListRepository(dbcontext);
            _passengerRepository = new PassengerRepository(dbcontext);
            _travelpassengerlistRepository = new TravelPassengerListRepository(dbcontext);
        }

        /// <summary>
        /// This method is used for Create new Travel List
        /// </summary>
        /// <param name="req">req is an object type of TravelList class (request)</param>
        /// <returns>this will return TravelList object of Data class</returns>
        public TravelList CreateTravelList(Mpower.Rail.Model.Request.TravelList req)
        {
            TravelList travellst = new TravelList();
            travellst.listName = req.listName;
            travellst.description = req.description;
            travellst.loginAccount = req.loginAccount;
            _travelListRepository.Add(travellst);
            _travelListRepository.Commit();
            foreach (long pass in req.passengerIds)
            {
                _travelpassengerlistRepository
                        .Add(new TravelPassengerLists
                        {
                            passenger = pass,
                            travelList = travellst.Id
                        });
                _travelpassengerlistRepository.Commit();
            }
            return travellst;
        }

        // public bool UpdateTravelList(UpdateTravelListRequest req)
        // {
        //     TravelList travellst=_travelListRepository.FindBy(m => m.Id == req.travelListId).FirstOrDefault();
        //     if(travellst!=null)
        //     {
        //         travellst.description=req.description;
        //         travellst.listName=req.listName;
        //         _travelListRepository.Update(travellst);
        //         _travelListRepository.Commit();
        //        return true;
        //     }
        //     else
        //     {
        //         return false;
        //     }
        // }


        // public void CreatePassengerList(CreatePassengerListRequest req)
        // {

        //     foreach (Passenger psgr in req.passengerList)
        //     {
        //         Passengers passanger=new Passengers();
        //         passanger=AddPassenger(psgr,req.loginAccount);    
        //         _passengerRepository.Add(passanger);
        //         _passengerRepository.Commit();
        //         TravelPassengerLists travelPassengerlst=AddTravelPassengerList(passanger.Id,req.travelListId);
        //         _travelpassengerlistRepository.Add(travelPassengerlst);
        //         _travelpassengerlistRepository.Commit();

        //     }




        // }

        // private Passengers AddPassenger(Passenger pass,string loginAccount)
        // {
        //     Passengers _passengers=new Passengers();
        //     _passengers.loginAccount=loginAccount;
        //     _passengers.name=pass.name;
        //     _passengers.bDay=pass.bDay;
        //     _passengers.bMonth=pass.bMonth;
        //     _passengers.bYear=pass.bYear;
        //     _passengers.sex=pass.sex;
        //     _passengers.birthPf=pass.birthPreferance;
        //     _passengers.foodPf=pass.foodPreferance;
        //     _passengers.senior=pass.senior;
        //     return _passengers;
        // }

        // private TravelPassengerLists AddTravelPassengerList(long PassengerID,long TravelListID )
        // {
        //     TravelPassengerLists tpl=new TravelPassengerLists();
        //     tpl.passenger=PassengerID;
        //     tpl.travelList=TravelListID;
        //     return tpl;
        // }


        /// <summary>
        /// This Method will delete existing travel list.
        /// </summary>
        /// <param name="travellistId">accept travel list ID</param>
        /// <returns>it return boolen value.</returns>
        public bool DeleteTravelList(long travellistId)
        {
            long travellstcount = _travelListRepository.FindBy(m => m.Id == travellistId).Count();
            if (travellstcount > 0)
            {
                _travelListRepository.DeleteWhere(m => m.Id == travellistId);
                _travelpassengerlistRepository.DeleteWhere(x => x.travelList == travellistId);
                _travelListRepository.Commit();
                _travelpassengerlistRepository.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// This Method will return TravelList Details
        /// </summary>
        /// <param name="travelListId">accept travel list ID</param>
        /// <returns>it return travel list details on object</returns>
        public object GetTravelList(long travelListId)
        {
            TravelList travel = _travelListRepository.FindBy(m => m.Id == travelListId).FirstOrDefault();            
            IEnumerable<TravelPassengerLists> travelPassengerlst = _travelpassengerlistRepository.FindBy(m => m.travelList == travelListId).AsQueryable();
            List<Passengers> psgrlst = (from pl in _passengerRepository.GetAll().AsQueryable()
                                        join tp in travelPassengerlst on pl.Id equals tp.passenger
                                        orderby pl.name
                                        select pl).ToList();

            object returnObj = new { Id = travel.Id, description = travel.description, listName = travel.listName, Passengers = psgrlst };
            return returnObj;
        }

        /// <summary>
        /// This Method will check that travelid exist or not
        /// </summary>
        /// <param name="travellistId">accept travel list ID</param>
        /// <returns>it return true/false</returns>
        public bool CheckTravelList(long travellistId)
        {
            TravelList travel = _travelListRepository.FindBy(m => m.Id == travellistId).FirstOrDefault();
            if (travel != null)
                return true;
            else
                return false;
        }


        /// <summary>
        /// This Method provide the all travel list by login account
        /// </summary>
        /// <param name="loginAccount">accept loginAccount <</param>
        /// <returns>it return all travel list of that RO</returns>
        public List<TravelList> GetAllTravelList(string loginAccount)
        {
            List<TravelList> travellst = _travelListRepository.FindBy(m => m.loginAccount== loginAccount).ToList();
            return travellst;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }


    }
}