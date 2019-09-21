using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G_Accounting_System.Models
{
    public class Contact
    {
        public int id { get; set; }
        public string Salutation { get; set; }
        public string Name { get; set; }
        public string Full_name_ { get; set; }
        public int CompanyId { get; set; }
        public string Company { get; set; }
        public string Designation { get; set; }
        public string Landline { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Address { get; set; }
        public string AddressLandline { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public int Vendor { get; set; }
        public int Customer { get; set; }
        public int Employee { get; set; }
        public string BankAccountNumber { get; set; }
        public string PaymentMethod { get; set; }
        public string Payment_method_ { get; set; }
        public string IsEnabled { get; set; }
        public string IsEnabled_ { get; set; }
        public int AddedBy { get; set; }
        public int UpdatedBy { get; set; }
        public int Delete_Request_By { get; set; }
        public string Delete_Status { get; set; }
        public HttpPostedFileWrapper File { get; set; }
        public string filename { get; set; }
        public string base64 { get; set; }
        public string Time { get; set; }
        public string Date { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string pFlag { get; set; }
        public string pDesc { get; set; }
        public string pContactid_Out { get; set; }
    }
}