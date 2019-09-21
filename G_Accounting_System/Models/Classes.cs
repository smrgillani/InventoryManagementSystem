using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G_Accounting_System.Models
{
    public class Classes
    {
        public List<Contact> Employees { get; set; }
        public List<Contact> Vendors { get; set; }
        public List<Contact> Customers { get; set; }
        public List<Country> Countries { get; set; }
        public List<Premises> Offices { get; set; }
        public List<Premises> Stores { get; set; }
        public List<Premises> Shops { get; set; }
        public List<Premises> Factories { get; set; }
        public List<Contact> Contacts { get; set; }
        public List<Premises> Premisess { get; set; }
        public List<City> Cities { get; set; }
        public List<Purchase> Items { get; set; }
        public List<SalesOrder> SOItems { get; set; }
        public List<Dropdown> Dropdowns { get; set; }
        public List<Payment> Payments { get; set; }
        public List<Package> PackageItems { get; set; }
        public List<Package> Packages { get; set; }
        public List<Shipment> Shipments { get; set; }
        public List<SaleReturn> SaleReturnItems { get; set; }
        public List<SaleReturn> SaleReturns { get; set; }
        public List<Brand> Brands { get; set; }
        public List<Brand> BrandsNotDelete { get; set; }
        public List<Category> Categories { get; set; }
        public List<Manufacturer> Manufacturers { get; set; }
        public List<Unit> Units { get; set; }
        public List<Stock> Stocks { get; set; }
        public List<Dropdown> VendorDropdown { get; set; }
        public List<Dropdown> BrandDropdown { get; set; }
        public List<Dropdown> CategoriesDropdown { get; set; }
        public List<Dropdown> ManufacturersDropdown { get; set; }
        public List<Dropdown> UnitsDropdown { get; set; }
        public List<Activity> Activity { get; set; }


        public Contact Contact { get; set; }
        public Country Country { get; set; }
        public Company Company { get; set; }
        public User User { get; set; }
        public Premises Premises { get; set; }
        public City City { get; set; }
        public Category Category { get; set; }
        public Brand Brand { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public Unit Unit { get; set; }
        public Item Item { get; set; }
        public Stock Stock { get; set; }
        public Bill Bill { get; set; }
        public Purchase Purchases { get; set; }
        public SalesOrder saleOrders { get; set; }
        public SO_Invoice Invoice { get; set; }
        public Dropdown Dropdown { get; set; }
        public Payment Payment { get; set; }
        public SO_Payment SOPayment { get; set; }
        public Package package { get; set; }
        public Shipment Shipment { get; set; }
        public SaleReturn SaleReturn { get; set; }
        public Activity Activitys { get; set; }
    }
}