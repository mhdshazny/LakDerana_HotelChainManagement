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
    public class AccountService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AccountService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool VerifyEmail(string EmpEmail)
        {
            if (EmpEmail != null)
            {
                var EmpList = _context.EmpIdentityData.Where(i => i.EmpEmail == EmpEmail).ToList();

                if (EmpList != null)
                    return true;
                else
                    return false;
            }

            else
                return false;
        }

        internal SessionData verify(string username, string password)
        {
            var data = _context.EmpIdentityData.Where(i => i.EmpEmail == username && i.EmpPassWord == password).FirstOrDefault();
            if (data !=null)
            {
                SessionData Final = new SessionData()
                {
                    Email=username,
                    ID= data.EmployeeID,
                    Role=data.EmpRole,
                    HotelID=data.HotelID,
                    Status=true
                };
                return Final;
            }
            else
            {
                SessionData Final = new SessionData()
                {
                    Status = false
                };
                return Final;
            }
        }

        public class SessionData
        {
            public string ID { get; set; }
            public string Role { get; set; }
            public string Email { get; set; }
            public string HotelID { get; set; }
            public bool Status { get; set; }
        }

        public bool VerifyFromDB(string EmpEmail, string EmpPassWord)
        {

            var ConfirmData = _context.EmpIdentityData.Where(i => i.EmpEmail == EmpEmail && i.EmpPassWord == EmpPassWord).ToList();
            if (ConfirmData.Count > 0)
                return true;
            else
                return false;
        }

        internal bool SignUp(EmpIdentityViewModel data)
        {
            try
            {
                var ConfirmData = _mapper.Map<EmpIdentityModel>(data);
                _context.Add(ConfirmData);
                int changes =_context.SaveChanges();
                return true;
            }
            catch (Exception er)
            {
                return false;
            }




        }

        internal bool SendEmail(EmployeeViewModel EmpVM, EmpIdentityModel EmpIdentityData)
        {
            try
            {


                    string subject;
                    string body;

                    //string to = RSManager.Email;
                    string to = EmpVM.Email;

                        subject = "LakDerana System Access Generated for Employee:"+EmpVM.ID+" ("+EmpVM.fName+")";
                        body = "The Access Email :" + EmpIdentityData.EmpEmail + " And Password is your Date of Birth in the format (dd/MM/yyyy) ----- (Please Make sure to change your password, once you logged in)";
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

        internal EmpIdentityViewModel Find(string email, string passw)
        {
            var data = _context.EmpIdentityData
                .Where(i => i.EmpEmail == email && i.EmpPassWord == passw)
                .FirstOrDefault();

            var EmpData = _context.EmpData.Where(x => x.ID == data.EmployeeID).FirstOrDefault();
            var VMData = _mapper.Map<EmpIdentityViewModel>(data);
            return VMData;

        }
    }
}
