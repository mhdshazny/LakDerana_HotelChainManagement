using AutoMapper;
using LakDerana_HotelChainManagement.Data;
using LakDerana_HotelChainManagement.Models;
using LakDerana_HotelChainManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace LakDerana_HotelChainManagement.Services
{
    public class EmployeeService : IService<EmployeeViewModel>
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly AccountService AccountService;

        public EmployeeService(ApplicationDbContext context,IMapper mapper):base(mapper)
        {
            this.context = context;
            this.mapper = mapper;
            AccountService = new AccountService(context, mapper);
        }

        public override bool Add(EmployeeViewModel collection)
        {
            try
            {
                if (collection != null)
                {
                    var MapData = GetMapped(collection);
                    context.Add(MapData);
                    int changes =context.SaveChanges();
                    bool AllChanges = false;
                    if (changes>0)
                    {
                        EmpIdentityModel NewEmpIdentity = new EmpIdentityModel()
                        {
                            HotelID = collection.EmployeeIdentityModel.HotelID,
                            EmpEmail = collection.Email,
                            EmployeeID = collection.ID,
                            EmpRole = collection.EmployeeIdentityModel.EmpRole,
                            EmpPassWord = collection.DoB.ToString("dd/MM/yyyy"),
                            EmpStatus=collection.Status,
                            
                        };

                        EmpIdentityViewModel EmpIdentityVM =GetMapped(NewEmpIdentity);
                        EmpIdentityVM.EmployeeID = collection.ID;
                        AllChanges = AccountService.SignUp(EmpIdentityVM);
                        AllChanges = AccountService.SendEmail(collection,NewEmpIdentity);
                        
                    }
                    return AllChanges;
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
                    var FoundRecord = context.EmpData.Find(id);
                    var IdentityRecord = context.EmpIdentityData.Where(i=>i.EmployeeID==id).FirstOrDefault();
                    context.RemoveRange(FoundRecord,IdentityRecord);
                    int changes = await context.SaveChangesAsync();
                    bool AllDone = false;
                    if (changes>0)
                    {
                        AllDone = SendEmail(FoundRecord,IdentityRecord);
                    }
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

        internal bool SendEmail(EmployeeModel EmpModel, EmpIdentityModel EmpIdentityData)
        {
            try
            {


                string subject;
                string body;

                //string to = RSManager.Email;
                string to = EmpModel.Email;

                subject = "LakDerana System Access Cancelled for Employee:" + EmpModel.ID + " (" + EmpModel.fName + ")";
                body = "The Access credential are no longer valid for Email"+EmpIdentityData.EmpEmail+" ----- (Please Make sure to contact system admin for any issues)";
                MailMessage mm = new MailMessage();
                mm.To.Add(to);
                mm.Subject = subject;
                mm.Body = body;
                mm.From = new MailAddress("lakderanalk@gmail.com");
                mm.IsBodyHtml = false;
                SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    UseDefaultCredentials = true,
                    EnableSsl = true,

                    Credentials = new System.Net.NetworkCredential("lakderanalk@gmail.com", "lakderanalk2022")
                };
                smtp.Send(mm);
                return true;

            }
            catch (Exception er)
            {
                return false;
            }
        }

        public override EmployeeViewModel Find(string id)
        {


            var FoundRecord = from e in context.EmpIdentityData join d in context.EmpData on e.EmployeeID equals d.ID
                              select new
                              {
                                  EmployeeModel=d,
                                  EmpIdentityModel=e
                              };
            
            var VM = GetMapped(FoundRecord.Where(i=>i.EmployeeModel.ID==id).Select(i=>i.EmployeeModel).FirstOrDefault());
            VM.EmployeeIdentityModel = GetMapped(FoundRecord.Where(i => i.EmpIdentityModel.EmployeeID == id).Select(i => i.EmpIdentityModel).FirstOrDefault());
            return VM;
        }

        public override List<EmployeeViewModel> GetDDList()
        {
            var DataList = context.EmpData.ToList();
            var GetList = new List<EmployeeViewModel>();
            foreach (var item in DataList)
            {
                var VM = GetMapped(item);
                VM.lName = VM.fName + " " + VM.lName + ". (" + VM.ID + ")";
                GetList.Add(VM);
            }
            return GetList;
        }

        public override List<EmployeeViewModel> GetList()
        {
            var DataList = context.EmpData.ToList();
            var IdentityTableList = context.EmpIdentityData.ToList();
            var GetList = new List<EmployeeViewModel>();
            foreach (var item in DataList)
            {
                var VM = GetMapped(item);
                //VM.HotelID = IdentityTableList.Where(i => i.EmployeeID == item.ID).Select(i => i.HotelID).FirstOrDefault();
                //VM.EmpRole = IdentityTableList.Where(i => i.EmployeeID == item.ID).Select(i => i.EmpRole).FirstOrDefault();
                VM.EmployeeIdentityModel = GetMapped(IdentityTableList.Where(i => i.EmployeeID == item.ID).FirstOrDefault());
                GetList.Add(VM);
            }
            return GetList;
        }

        internal ProfileViewModel GetProfileData(string empID)
        {
            try
            {
                ProfileViewModel FinalVM = new ProfileViewModel();

                var EmpIdentityData = context.EmpIdentityData.Where(i=>i.EmployeeID==empID).FirstOrDefault();
                var EmpData = context.EmpData.Find(empID);

                FinalVM.EmpIdentityVM = GetMapped(EmpIdentityData);
                FinalVM.EmployeeVM = GetMapped(EmpData);

                return FinalVM;
            }
            catch (Exception er)
            {
                return null;
            }
        }

        public override string NewID()
        {
            var Id = context.EmpData
                .Max(i => i.ID);
            string NewID = "EMP0000001";
            if (Id != null)
            {
                return (GetNewID(int.Parse(Id.Substring(3, 7)), "EMP"));
            }
            return NewID;
        }

        public override async Task<bool> Update(EmployeeViewModel collection)
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

        internal bool ChangePass(string ID, string email, string password)
        {
            try
            {
                var Data = context.EmpIdentityData.Where(i => i.EmpEmail == email && i.Id == int.Parse(ID)).FirstOrDefault();
                Data.EmpPassWord = password;
                context.Update(Data);
                int changes = context.SaveChanges();
                bool updated = false;
                if (changes>0)
                {
                    updated = ChangePasswordEmail(email,DateTime.Now);
                }
                return updated;
            }
            catch (Exception err)
            {
                err.ToString();
                return false;
            }
        }


        internal bool ChangePasswordEmail(string email, DateTime dt)
        {
            try
            {


                string subject;
                string body;

                //string to = RSManager.Email;
                string to = email;

                subject = "LakDerana System Account Password Has been Changed";
                body = "The Access credential (Password) is no longer valid for Email" + email + " ...The password has been changed on"+dt.ToShortDateString()+" ----- (Please Make sure to contact system admin for any issues)";
                MailMessage mm = new MailMessage();
                mm.To.Add(to);
                mm.Subject = subject;
                mm.Body = body;
                mm.From = new MailAddress("lakderanalk@gmail.com");
                mm.IsBodyHtml = false;
                SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    UseDefaultCredentials = true,
                    EnableSsl = true,

                    Credentials = new System.Net.NetworkCredential("lakderanalk@gmail.com", "lakderanalk2022")
                };
                smtp.Send(mm);
                return true;

            }
            catch (Exception er)
            {
                return false;
            }
        }
    }
}
