using AutoMapper;
using LakDerana_HotelChainManagement.Data;
using LakDerana_HotelChainManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LakDerana_HotelChainManagement.Services
{
    public class BarService : IService<BarViewModel>
    {
        private ApplicationDbContext context;
        private IMapper mapper;

        public BarService(ApplicationDbContext context, IMapper mapper):base(mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public override bool Add(BarViewModel collection)
        {
            try
            {
                if (collection != null)
                {
                    var MapData = GetMapped(collection);
                    MapData.ID = 0;
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
                    var FoundRecord = context.BarData.Find(int.Parse(id));
                    context.Remove(FoundRecord);
                    await context.SaveChangesAsync();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception er)
            {

                return false;
            }
        }

        public override BarViewModel Find(string id)
        {
            var FoundRecord = context.BarData.Where(i => i.ID == int.Parse(id)).FirstOrDefault();
            var VM = GetMapped(FoundRecord);
            return VM;
        }

        public override List<BarViewModel> GetDDList()
        {
            var DataList = context.BarData.ToList();
            var GetList = new List<BarViewModel>();
            foreach (var item in DataList)
            {
                var VM = GetMapped(item);
                VM.Status = VM.Sales + " . (" + VM.IncomeExpenseUpdatedDate + ")";
                GetList.Add(VM);
            }
            return GetList;
        }

        public override List<BarViewModel> GetList()
        {
            var DataList = context.BarData.ToList();
            var GetList = new List<BarViewModel>();
            foreach (var item in DataList)
            {
                var VM = GetMapped(item);
                GetList.Add(VM);
            }
            return GetList;
        }

        public override string NewID()
        {
            var data = context.BarData.ToList();
            int NewID = 1;
            if (data.Count > 0)
            {
                NewID = (data.Select(i => i.ID).Max()) + 1;
            }
            return NewID.ToString();
        }

        public async override Task<bool> Update(BarViewModel collection)
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

        internal string isExist(BarViewModel collection)
        {
            try
            {
                var BarData = context.BarData.ToList();
                var ExistingDataByID = BarData.Where(i=>i.ID== collection.ID).FirstOrDefault();
                var ExistingDataByDate = BarData.Where(i=>i.IncomeExpenseUpdatedDate.ToString("dd/MM/yyyy")== collection.IncomeExpenseUpdatedDate.ToString("dd/MM/yyyy")).FirstOrDefault();
                if (ExistingDataByDate == null)
                {
                    if (ExistingDataByID == null)
                    {
                        return "DataExist";
                    }
                    else return "IDNoExist";
                }
                else return "DateNoExist";

            }
            catch (Exception er)
            {
                return "Error";
            }
        }
    }
}
