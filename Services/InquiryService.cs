using AutoMapper;
using LakDerana_HotelChainManagement.Data;
using LakDerana_HotelChainManagement.Models;
using LakDerana_HotelChainManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LakDerana_HotelChainManagement.Services
{
    public class InquiryService : IService<CustomerViewModel>
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public InquiryService(ApplicationDbContext context, IMapper mapper):base(mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public override string NewID()
        {
            var Id = context.CusData
                .Max(i => i.ID);
            string NewID = "CUS0000001";
            if (Id != null)
            {
                return(GetNewID(int.Parse(Id.Substring(3, 7)),"CUS"));
            }
            return NewID;
        }

        public override List<CustomerViewModel> GetList()
        {
            var DataList = context.CusData.ToList();
            var ReservationList = context.ReservationData.ToList();
            var GetList = new List<CustomerViewModel>();
            foreach (var item in DataList)
            {
                var VM = GetMapped(item);
                VM.ReservationCount = ReservationList.Where(i => i.Customer == item.ID).Count();
                GetList.Add(VM);
            }
            return GetList;
        }

        public override List<CustomerViewModel> GetDDList()
        {
            var DataList = context.CusData.ToList();
            var GetList = new List<CustomerViewModel>();
            foreach (var item in DataList)
            {
                var VM =GetMapped(item);
                VM.CuslName = VM.CusfName + " " + VM.CuslName + ". (" + VM.ID + ")";
                GetList.Add(VM);
            }
            return GetList;
        }


        public override bool Add(CustomerViewModel collection)
        {
            try
            {
                if (collection != null)
                {
                    var MapData = GetMapped(collection);
                    context.Add(MapData);
                    context.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception err)
            {
                err.ToString();
                return false;
            }
        }


        public override async Task<bool> Update(CustomerViewModel collection)
        {
            try
            {
                if (collection != null)
                {
                    var MapData = GetMapped(collection);
                    context.Update(MapData);
                    await context.SaveChangesAsync();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception err)
            {
                err.ToString();
                return false;
            }
        }

        public override async Task<bool> Delete(string id)
        {
            try
            {
                if (id != null)
                {
                    CustomerModel FoundRecord = context.CusData.Find(id);
                    context.Remove(FoundRecord);
                    await context.SaveChangesAsync();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public override CustomerViewModel Find(string id)
        {
            var FoundRecord = context.CusData.Find(id);
            var VM = GetMapped(FoundRecord);
            return VM;
        }
    }
}
