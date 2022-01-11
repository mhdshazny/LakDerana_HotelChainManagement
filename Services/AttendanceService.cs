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
    public class AttendanceService
    {
        private ApplicationDbContext context;
        private IMapper mapper;

        public AttendanceService(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        internal AttendanceViewModel SetAttendance(string logID, string DayDescription, DateTime dateTimeForLog)
        {
            try
            {
                AttendanceMaintenanceModel NewMaintenanceLog = new AttendanceMaintenanceModel()
                {
                    AttendanceDate = dateTimeForLog,
                    AttendanceLogID = newID(),
                    DayDescription = DayDescription,
                    Status = "Approved"
                };

                /////
                ///

                List<AttendanceSheetModel> AttendanceSheet = new List<AttendanceSheetModel>();
                var ActiveEmployees = context.EmpData.Where(i => i.Status == "Active").ToList();

                foreach (var item in ActiveEmployees)
                {
                    AttendanceSheetModel Model = new AttendanceSheetModel()
                    {
                        AttendanceLogID = NewMaintenanceLog.AttendanceLogID,
                        AttendanceStatus = "Absent",
                        EmpID = item.ID,
                        ID = 0
                    };
                    AttendanceSheet.Add(Model);
                }

                context.Add(NewMaintenanceLog);
                context.AddRange(AttendanceSheet);


                int Changes = context.SaveChanges();

                AttendanceViewModel AttendanceVM = GetAttendanceVM(NewMaintenanceLog,AttendanceSheet,ActiveEmployees);


                return AttendanceVM;


            }
            catch (Exception er)
            {

                return null;
            }
        }

        internal bool Absent(int data)
        {
            try
            {
                var Record = context.AttendanceSheetData.Find(data);
                Record.AttendanceStatus = "Absent";

                context.Update(Record);
                int Changes = context.SaveChanges();
                if (Changes > 0)
                {
                    return true;
                }
                return false;

            }
            catch (Exception er)
            {
                return false;
            }
        }

        internal bool Present(int data)
        {
            try
            {
                var Record = context.AttendanceSheetData.Find(data);
                Record.AttendanceStatus = "Present";

                context.Update(Record);
                int Changes= context.SaveChanges();
                if (Changes>0)
                {
                    return true;
                }
                return false;

            }
            catch (Exception er)
            {
                return false;
            }
        }

        private AttendanceViewModel GetAttendanceVM(AttendanceMaintenanceModel MaintenanceLog, List<AttendanceSheetModel> AttendanceSheet, List<EmployeeModel> ActiveEmployees)
        {
            AttendanceViewModel AttendanceVM = new AttendanceViewModel();
            AttendanceVM.MaintenanceLog = mapper.Map<AttendanceMaintenanceViewModel>(MaintenanceLog);


            List<AttendanceSheetViewModel> AttendanceSheetVMList = new List<AttendanceSheetViewModel>();
            foreach (var item in AttendanceSheet)
            {
                AttendanceSheetViewModel AttendanceSheetVM = mapper.Map<AttendanceSheetViewModel>(item);
                var Employee = ActiveEmployees.Where(i => i.ID == AttendanceSheetVM.EmpID).FirstOrDefault();
                AttendanceSheetVM.Employee = mapper.Map<EmployeeViewModel>(Employee);

                AttendanceSheetVMList.Add(AttendanceSheetVM);
            }

            AttendanceVM.AttendanceSheet = AttendanceSheetVMList;

            return AttendanceVM;
        }

        internal ReturnToView Exist(string logID, DateTime dateTimeForLog)
        {
            List<AttendanceMaintenanceModel> MaintenanceLog = context.AttendanceLogData.ToList();

            ///Returns
            bool Exist = false;
            AttendanceMaintenanceModel ExistingLog = null;
            ////

            foreach (var item in MaintenanceLog)
            {
                if (item.AttendanceDate.Date.Day == dateTimeForLog.Date.Day && item.AttendanceDate.Date.Month == dateTimeForLog.Date.Month)
                {
                    ExistingLog = item;
                    Exist = true;
                }
            }

            if (Exist)
            {
                ///Returns
                List<AttendanceSheetModel> AttendanceSheet = context.AttendanceSheetData.Where(i => i.AttendanceLogID == ExistingLog.AttendanceLogID).ToList();
                List<EmployeeModel> Employees = context.EmpData.Where(i=>i.Status=="Active").ToList();
                ///

                AttendanceViewModel Final = GetAttendanceVM(ExistingLog,AttendanceSheet,Employees);

                ReturnToView ReturnData = new ReturnToView() { Exist = Exist, AttendanceModel = Final };

                return ReturnData;
            }
            else
            {
                ReturnToView ReturnData = new ReturnToView() { Exist = Exist, AttendanceModel = null };
                return ReturnData;

            }

        }

        public class ReturnToView{
            public bool Exist { get; set; }
            public AttendanceViewModel AttendanceModel { get; set; }
        }

        internal string newID()
        {
            var Id = context.AttendanceLogData
                .Max(i => i.AttendanceLogID);
            string NewID = "ALG0000001";
            if (Id != null)
            {
                return (GetNewID(int.Parse(Id.Substring(3, 7)), "ALG"));

            }
            return NewID;
        }
        public string GetNewID(int OldID, string Prefix)
        {
            OldID = OldID + 1;
            string NewID = Prefix + OldID.ToString().PadLeft(7, '0');
            return NewID;
        }
    }
}
