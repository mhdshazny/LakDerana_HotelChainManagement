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
    public class HotelRoomService
    {
        private ApplicationDbContext context;
        private IMapper mapper;
        private HotelService HotelService;

        public HotelRoomService(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
            HotelService = new HotelService(context, mapper);
        }

        public bool Add(HotelRoomViewModel collection)
        {
            try
            {
                if (collection != null)
                {
                    collection.ID = 0;
                    var MapData = mapper.Map<HotelRoomModel>(collection);
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

        public async Task<bool> Delete(int id)
        {
            try
            {
                if (id != 0)
                {
                    var FoundRecord = context.HotelRoomData.Find(id);
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

        internal bool UpdateStatus(ReservedRoomViewModel collection)
        {
            try
            {
                if (collection!=null)
                {
                    var FoundRecord = context.HotelRoomData.Find(collection.RoomID);
                    if (collection.Status== "Reservation Approved")
                        FoundRecord.RoomStatus = "Reserved";
                    else if (collection.Status == "Reservation Cancelled")
                        FoundRecord.RoomStatus = "Available";
                    else if (collection.Status == "Reservation Closed")
                        FoundRecord.RoomStatus = "Available";
                    context.Update(FoundRecord);
                    context.SaveChanges();
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

        public HotelRoomViewModel Find(int id)
        {
            var FoundRecord = context.HotelRoomData.Find(id);
            var VM = mapper.Map<HotelRoomViewModel>(FoundRecord);

            return VM;
        }

        public List<HotelRoomViewModel> GetDDList()
        {
            var DataList = context.HotelRoomData.Where(i=>i.RoomStatus=="Available").ToList();
            var GetList = new List<HotelRoomViewModel>();
            foreach (var item in DataList)
            {
                var VM = mapper.Map<HotelRoomViewModel>(item);
                VM.HotelID ="Room No :" + VM.ID.ToString().PadLeft(2,'0') + ". (" + VM.RoomType + ")" + " ---- Price : Rs." + VM.RoomPricePerNight.ToString("0.00");
                GetList.Add(VM);
            }
            return GetList;
        }
        public List<HotelRoomViewModel> GetUpdatedDDList(string HotelID)
        {
            var DataList = context.HotelRoomData.Where(i=>i.RoomStatus=="Available" && i.HotelID==HotelID).ToList();
            var GetList = new List<HotelRoomViewModel>();
            foreach (var item in DataList)
            {
                var VM = mapper.Map<HotelRoomViewModel>(item);
                VM.HotelID ="Room No :" + VM.ID.ToString().PadLeft(2,'0') + ". (" + VM.RoomType + ")" + " ---- Price : Rs." + VM.RoomPricePerNight.ToString("0.00");
                GetList.Add(VM);
            }
            return GetList;
        }

        public List<HotelRoomViewModel> GetList()
        {
            var DataList = context.HotelRoomData.ToList();
            var HotelList = HotelService.GetList();
            var GetList = new List<HotelRoomViewModel>();
            foreach (var item in DataList)
            {
                var VM = mapper.Map<HotelRoomViewModel>(item);
                var HotelName = HotelList.Where(i => i.ID == VM.HotelID).FirstOrDefault();
                VM.HotelID = HotelName.HotelSpecialName + " ," + HotelName.HotelLocation + " ----- " + HotelName.ID.ToString();
                GetList.Add(VM);
            }
            return GetList;
        }

        public int NewID()
        {
            var data = context.HotelRoomData.ToList();
            int NewID = 1;
            if (data.Count > 0)
            {
                NewID = (data.Select(i=>i.ID).Max())+1;                
            }
            return NewID;

        }

        public async Task<bool> Update(HotelRoomViewModel collection)
        {
            try
            {
                if (collection != null)
                {
                    var MapData = mapper.Map<HotelRoomModel>(collection);
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
