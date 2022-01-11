using AutoMapper;
using LakDerana_HotelChainManagement.Models;
using LakDerana_HotelChainManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LakDerana_HotelChainManagement.Data
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<CustomerModel, CustomerViewModel>()
                .ReverseMap();
            CreateMap<EmployeeModel, EmployeeViewModel>()
                .ReverseMap();
            CreateMap<EmpIdentityModel, EmpIdentityViewModel>()
                .ReverseMap();
            CreateMap<HotelModel, HotelViewModel>()
                .ReverseMap();
            CreateMap<HotelRoomModel, HotelRoomViewModel>()
                .ReverseMap();
            CreateMap<ReservationModel, ReservationViewModel>()
                .ReverseMap();
            CreateMap<PaymentModel, PaymentViewModel>()
                .ReverseMap();
            CreateMap<ReservedRoomModel, ReservedRoomViewModel>()
                .ReverseMap();
            CreateMap<BarModel, BarViewModel>()
                .ReverseMap();
            CreateMap<AttendanceMaintenanceModel, AttendanceMaintenanceViewModel>()
                .ReverseMap();
            CreateMap<AttendanceSheetModel, AttendanceSheetViewModel>()
                .ReverseMap();
        }
    }
}
