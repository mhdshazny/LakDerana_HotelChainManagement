using AutoMapper;
using LakDerana_HotelChainManagement.Models;
using LakDerana_HotelChainManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LakDerana_HotelChainManagement.Services
{
    public abstract class IService<T>
    {
        private readonly IMapper mapper;

        public IService(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public abstract List<T>  GetList();
        public abstract List<T>  GetDDList();
        public abstract bool Add(T collection);
        public abstract Task<bool> Update(T collection);
        public abstract Task<bool> Delete(string id);
        public abstract T Find(string id);
        public abstract string NewID();

        public string GetNewID(int OldID,string Prefix)
        {
                OldID = OldID + 1;
                string NewID = Prefix + OldID.ToString().PadLeft(7, '0');
                return NewID;
        }




        //GetMapped

        public EmpIdentityModel GetMapped(EmpIdentityViewModel FoundRecord)
        {
            return(mapper.Map<EmpIdentityModel>(FoundRecord));
        }
        public EmpIdentityViewModel GetMapped(EmpIdentityModel FoundRecord)
        {
            return(mapper.Map<EmpIdentityViewModel>(FoundRecord));
        }

        public BarModel GetMapped(BarViewModel FoundRecord)
        {
            return(mapper.Map<BarModel>(FoundRecord));
        }
        public BarViewModel GetMapped(BarModel FoundRecord)
        {
            return(mapper.Map<BarViewModel>(FoundRecord));
        }
        public ReservedRoomModel GetMapped(ReservedRoomViewModel FoundRecord)
        {
            return(mapper.Map<ReservedRoomModel>(FoundRecord));
        }
        public ReservedRoomViewModel GetMapped(ReservedRoomModel FoundRecord)
        {
            return(mapper.Map<ReservedRoomViewModel>(FoundRecord));
        }
        public PaymentModel GetMapped(PaymentViewModel FoundRecord)
        {
            return(mapper.Map<PaymentModel>(FoundRecord));
        }
        public PaymentViewModel GetMapped(PaymentModel FoundRecord)
        {
            return(mapper.Map<PaymentViewModel>(FoundRecord));
        }
        public ReservationModel GetMapped(ReservationViewModel FoundRecord)
        {
            return(mapper.Map<ReservationModel>(FoundRecord));
        }
        public ReservationViewModel GetMapped(ReservationModel FoundRecord)
        {
            return(mapper.Map<ReservationViewModel>(FoundRecord));
        }
        public HotelModel GetMapped(HotelViewModel FoundRecord)
        {
            return(mapper.Map<HotelModel>(FoundRecord));
        }
        public HotelViewModel GetMapped(HotelModel FoundRecord)
        {
            return(mapper.Map<HotelViewModel>(FoundRecord));
        }
        public EmployeeViewModel GetMapped(EmployeeModel FoundRecord)
        {
            return(mapper.Map<EmployeeViewModel>(FoundRecord));
        }
        public EmployeeModel GetMapped(EmployeeViewModel FoundRecord)
        {
            return(mapper.Map<EmployeeModel>(FoundRecord));
        }
        public CustomerViewModel GetMapped(CustomerModel FoundRecord)
        {
            return(mapper.Map<CustomerViewModel>(FoundRecord));
        }
        public CustomerModel GetMapped(CustomerViewModel FoundRecord)
        {
            return(mapper.Map<CustomerModel>(FoundRecord));
        }
    }
}
