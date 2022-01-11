using AutoMapper;
using LakDerana_HotelChainManagement.Data;
using LakDerana_HotelChainManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LakDerana_HotelChainManagement.Services
{
    public class HotelService: IService<HotelViewModel>
    {
        private ApplicationDbContext context;
        private IMapper mapper;
        private EmployeeService EmpService;

        public HotelService(ApplicationDbContext context, IMapper mapper):base(mapper)
        {
            this.context = context;
            this.mapper = mapper;
            EmpService = new EmployeeService(context,mapper);
        }

        public override bool Add(HotelViewModel collection)
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

        public async override Task<bool> Delete(string id)
        {
            try
            {
                if (id != null)
                {
                    var FoundRecord = context.HotelData.Find(id);
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

        public override HotelViewModel Find(string id)
        {
            var FoundRecord = context.HotelData.Find(id);
            var VM = GetMapped(FoundRecord);

            return VM;
        }

        public override List<HotelViewModel> GetDDList()
        {
            var DataList = context.HotelData.ToList();
            var GetList = new List<HotelViewModel>();
            foreach (var item in DataList)
            {
                var VM = GetMapped(item);
                VM.HotelSpecialName = VM.HotelSpecialName + " " + VM.HotelLocation + ". (" + VM.ID + ")";
                GetList.Add(VM);
            }
            return GetList;
        }

        public override List<HotelViewModel> GetList()
        {
            var DataList = context.HotelData.ToList();
            var EmpList = EmpService.GetList();
            var GetList = new List<HotelViewModel>();
            foreach (var item in DataList)
            {
                var VM = GetMapped(item);
                var ManagerName = EmpList.Where(i=>i.ID==VM.HotelManager).FirstOrDefault();
                var RSManagerName = EmpList.Where(i=>i.ID==VM.RoomServiceManager).FirstOrDefault();
                VM.HotelManager = ManagerName.fName+" "+ManagerName.lName+ " ----- " + ManagerName.ID;
                VM.RoomServiceManager = RSManagerName.fName+" "+RSManagerName.lName+" ----- "+RSManagerName.ID;
                GetList.Add(VM);
            }
            return GetList;
        }

        public override string NewID()
        {
            var Id = context.HotelData
                .Max(i => i.ID);
            string NewID = "HOT0000001";
            if (Id != null)
            {
                return (GetNewID(int.Parse(Id.Substring(3, 7)), "HOT"));

            }
            return NewID;
        }

        public async override Task<bool> Update(HotelViewModel collection)
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
    }
}
