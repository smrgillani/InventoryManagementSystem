using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G_Accounting_System.ENT;
using G_Accounting_System.DAL;
using System.Collections;

namespace G_Accounting_System.APP
{
    public class Catalog
    {
        /*--------------------Post New Records------------------------*/
        public void InsertUpdateContacts(Contacts C)
        {
            new ContactDAL().InsertUpdateContacts(C);
        }

        public void InsertUpdateItem(Items I)
        {
            new ItemDAL().InsertUpdateItem(I);
        }

        public string AddPurchase(List<Purchases> P, Purchases po, int PremisesId, string UserId, int AddedBy)
        {
            return new PurchaseDAL().Add(P, po, PremisesId, UserId, AddedBy);
        }

        public string AddSalesOrder(List<SalesOrders> S, SalesOrders SO, int PremisesId, string UserId)
        {
            return new SalesOrderDAL().Add(S, SO, PremisesId, UserId);
        }

        public void AddPremises(Premisess P)
        {
            new PremisesDAL().Add(P);
        }

        public void InsertUpdateUser(Users U)
        {
            new UserDAL().InsertUpdateUser(U);
        }

        public void InsertUpdateCompanies(Companies C)
        {
            new CompanyDAL().InsertUpdateCompanies(C);
        }

        public void InsertUpdateCountry(Countries C)
        {
            new CountryDAL().InsertUpdateCountry(C);
        }

        public void InsertUpdateCities(Cities C)
        {
            new CityDAL().InsertUpdateCities(C);
        }

        public void InsertUpdateCategory(Categories C)
        {
            new CategoryDAL().InsertUpdateCategory(C);
        }

        public void InsertUpdateBrands(Brands B)
        {
            new BrandDAL().InsertUpdateBrands(B);
        }

        public void InsertUpdateManufacturer(Manufacturers M)
        {
            new ManufacturerDAL().InsertUpdateManufacturer(M);
        }

        public void InsertUpdateUnit(Units U)
        {
            new UnitDAL().InsertUpdateUnit(U);
        }

        public void InsertBill(Bills B)
        {
            new BillDAL().InsertBill(B);
        }

        public void InsertPayments(Payments P)
        {
            new PaymentDAL().InsertPayments(P);
        }

        public string InsertItemStock(List<Stocks> S)
        {
            return new StockDAL().InsertItemStock(S);
        }

        public void InsertSOInvoice(SO_Invoices SO)
        {
            new SalesOrderDAL().InsertSOInvoice(SO);
        }

        public void InsertSO_Payments(SO_Payments P)
        {
            new SalesOrderDAL().InsertSO_Payments(P);
        }

        public void UpdateinvoiceStatus(SO_Invoices I)
        {
            new SalesOrderDAL().UpdateinvoiceStatus(I);
        }

        public string InsertSOPackage(List<Packages> P, Packages PG, int SalesOrder_id, int AddedBy)
        {
            return new SalesOrderDAL().InsertSOPackage(P, PG, SalesOrder_id, AddedBy);
        }

        public string InsertSOShipment(List<Shipments> P, Shipments SH, int SalesOrder_id, int AddedBy)
        {
            return new SalesOrderDAL().InsertSOShipment(P, SH, SalesOrder_id, AddedBy);
        }

        public string InsertSaleReturn(List<SaleReturns> S, SaleReturns SR)
        {
            return new SalesOrderDAL().InsertSaleReturn(S, SR);
        }

        public string ReturnReceivingQty(List<SaleReturns> S)
        {
            return new SalesOrderDAL().ReturnReceivingQty(S);
        }

        public void InsertUpdateRoles(Roles R)
        {
            new AdminDAL().InsertUpdateRoles(R);
        }

        public string UpdateRolePrivileges(List<RolePrivileges> RP)
        {
            return new AdminDAL().UpdateRolePrivileges(RP);
        }

        public string UpdateUserPrivileges(List<UserPrivilegess> UP)
        {
            return new AdminDAL().UpdateUserPrivileges(UP);
        }

        public void InsertActivity(List<Activities> A)
        {
            new ActivitiesDAL().InsertActivity(A);
        }

        public void SendMessage(Messages M)
        {
            new MessagesDAL().SendMessage(M);
        }

        public void UpdateStatus(Messages M)
        {
            new MessagesDAL().UpdateStatus(M);
        }

        public void DeleteChat(Messages M)
        {
            new MessagesDAL().DeleteChat(M);
        }

        public void InsertEmail(Emails E, List<MailAttachments> mailAttachments)
        {
            new MailboxDAL().InsertEmail(E, mailAttachments);
        }

        public void AddEvents(Calenders C)
        {
            new CalenderDAL().AddEvents(C);
        }

        public void AddCalenderEvents(Calenders C)
        {
            new CalenderDAL().AddCalenderEvents(C);
        }

        public void ChangePasswordUpdate(Users U)
        {
            new AdminDAL().ChangePasswordUpdate(U);
        }

        /*----------Update All Records (With or Without Parameters)-----------*/

        //public void UpdateUser(Users U)
        //{
        //    new UserDAL().Update(U);
        //}

        //public void UpdateContact(Contacts C)
        //{
        //    new ContactDAL().Update(C);
        //}

        //public void UpdateCompany(Companies C)
        //{
        //    new CompanyDAL().Update(C);
        //}

        //public void UpdateCountry(Countries C)
        //{
        //    new CountryDAL().Update(C);
        //}

        //public void UpdateCity(Cities C)
        //{
        //    new CityDAL().Update(C);
        //}

        public void UpdatepCity(Cities C)
        {
            new CityDAL().Updatep(C);
        }

        public void UpdatepPremises(Premisess P)
        {
            new PremisesDAL().UpdatepPremises(P);
        }

        public void UpdatepPurchase(Purchases P)
        {
            new PurchaseDAL().UpdatepPurchase(P);
        }

        public void UpdatepContact(Contacts C)
        {
            new ContactDAL().UpdatepContact(C);
        }

        public void UpdatepSaleOrder(SalesOrders S)
        {
            new SalesOrderDAL().UpdatepSaleOrder(S);
        }

        public void UpdatepItem(Items I)
        {
            new ItemDAL().UpdatepItem(I);
        }

        public void UpdatepBrand(Brands B)
        {
            new BrandDAL().UpdatepBrand(B);
        }

        public void UpdatepCategory(Categories C)
        {
            new CategoryDAL().UpdatepCategory(C);
        }

        public void UpdatepManufacturer(Manufacturers M)
        {
            new ManufacturerDAL().UpdatepManufacturer(M);
        }

        public void UpdatepUnit(Units U)
        {
            new UnitDAL().UpdatepUnit(U);
        }

        public void UpdatepCountry(Countries C)
        {
            new CountryDAL().UpdatepCountry(C);
        }

        public void UpdatepCompany(Companies C)
        {
            new CompanyDAL().UpdatepCompany(C);
        }

        public void UpdatepUser(Users U)
        {
            new UserDAL().UpdatepUser(U);
        }

        public void UpdatePurchaseStatus(Purchases P)
        {
            new PurchaseDAL().UpdatePurchaseStatus(P);
        }

        public string UpdateItemStock(List<Stocks> S)
        {
            return new StockDAL().UpdateItemStock(S);
        }

        public void UpdateBillStatus(Bills B)
        {
            new BillDAL().UpdateBillStatus(B);
        }

        public void UpdateSO_InvoiceStatus(SalesOrders SO)
        {
            new SalesOrderDAL().UpdateSO_InvoiceStatus(SO);
        }

        public void UpdateSO_PackageStatus(SalesOrders SO)
        {
            new SalesOrderDAL().UpdateSO_PackageStatus(SO);
        }

        public void UpdateShipmentDeliver(Shipments S)
        {
            new SalesOrderDAL().UpdateShipmentDeliver(S);
        }

        /*----------Delete All Records (With or Without Parameters)-----------*/

        public string DelBrandRequest(List<Brands> B, string type)
        {
            return new BrandDAL().DelBrandRequest(B, type);
        }

        public string DelCategoryRequest(List<Categories> C, string type)
        {
            return new CategoryDAL().DelCategoryRequest(C, type);
        }

        public string DelManufacturerRequest(List<Manufacturers> M, string type)
        {
            return new ManufacturerDAL().DelManufacturerRequest(M, type);
        }

        public string DelUnitRequest(List<Units> U, string type)
        {
            return new UnitDAL().DelUnitRequest(U, type);
        }

        public string DelItemRequest(List<Items> I, string type)
        {
            return new ItemDAL().DelItemRequest(I, type);
        }

        public string DelPremisesRequest(List<Premisess> P, string type)
        {
            return new PremisesDAL().DelPremisesRequest(P, type);
        }

        public string DelContactRequest(List<Contacts> C, string type)
        {
            return new ContactDAL().DelContactRequest(C, type);
        }

        public string DelCountryRequest(List<Countries> C, string type)
        {
            return new CountryDAL().DelCountryRequest(C, type);
        }

        public string DelCityRequest(List<Cities> C, string type)
        {
            return new CityDAL().DelCityRequest(C, type);
        }

        public void DelUserRequest(List<Users> U, string type)
        {
            new UserDAL().DelUserRequest(U, type);
        }

        public string DelCompanyRequest(List<Companies> C, string type)
        {
            return new CompanyDAL().DelCompanyRequest(C, type);
        }

        public string DelPurchasingRequest(List<Purchases> P, string type)
        {
            return new PurchaseDAL().DelPurchasingRequest(P, type);
        }

        public string DelSalesRequest(List<SalesOrders> S, string type)
        {
            return new SalesOrderDAL().DelSalesRequest(S, type);
        }

        public void DelUser(int id)
        {
            new UserDAL().Del(id);
        }

        public void DelOffice(int id)
        {
            new PremisesDAL().Del(id);
        }

        public void DelContact(int id)
        {
            new ContactDAL().Del(id);
        }

        public void DelCompany(int id)
        {
            new CompanyDAL().Del(id);
        }

        public void DelCountry(int id)
        {
            new CountryDAL().Del(id);
        }

        public void DelCity(int id)
        {
            new CityDAL().Del(id);
        }

        public void DelCategory(int id)
        {
            new CategoryDAL().Del(id);
        }

        public void DelBrand(int id)
        {
            new BrandDAL().Del(id);
        }

        public void DelManufacture(int id)
        {
            new ManufacturerDAL().Del(id);
        }

        public void DelUnit(int id)
        {
            new UnitDAL().Del(id);
        }

        public void DelItem(int id)
        {
            new ItemDAL().Del(id);
        }

        public void DelRole(int id)
        {
            new AdminDAL().Del(id);
        }

        public void Delete(int id, string type)
        {
            new DeleteRequestDAL().Delete(id, type);
        }

        public void DeleteItemFromPurchase(int pdid)
        {
            new PurchaseDAL().DeleteItemFromPurchase(pdid);
        }

        public void DeleteItemFromSaleOrder(int sdid)
        {
            new SalesOrderDAL().DeleteItemFromSaleOrder(sdid);
        }

            public void UpdateToken(Tokens T)
            {
                new AdminDAL().UpdateToken(T);
            }

        /*----------Select All Records (With Parameters)-----------*/

        public List<Contacts> AllVendors(string Option, string search, string From, string To)
        {
            return new ContactDAL().SelectAllVendors(Option, search, From, To);
        }

        public List<Contacts> AllCustomers(string Option, string search, string From, string To)
        {
            return new ContactDAL().SelectAllCustomers(Option, search, From, To);
        }

        public List<Contacts> AllEmployees(string Option, string search, string From, string To)
        {
            return new ContactDAL().SelectAllEmployees(Option, search, From, To);
        }

        public List<Contacts> ContactByType(int Customer, int Vendor, int Employee)
        {
            return new ContactDAL().ContactByType(Customer, Vendor, Employee);
        }

        public List<Items> AllItems(string Option, string search, string From, string To)
        {
            return new ItemDAL().SelectAll(Option, search, From, To);
        }

        public List<Stocks> GetAllItemsStockList(string SD, string ED, string Search)
        {
            return new StockDAL().GetAllItemsStockList(SD, ED, Search);
        }

        public List<SalesOrders> ItemsTransactionSO(int Item_id, string Search)
        {
            return new SalesOrderDAL().ItemsTransactionSO(Item_id, Search);
        }

        public List<SO_Invoices> ItemsTransactionINV(int Item_id, string Search)
        {
            return new SalesOrderDAL().ItemsTransactionINV(Item_id, Search);
        }

        public List<Purchases> ItemsTransactionPO(int Item_id, string Search)
        {
            return new PurchaseDAL().ItemsTransactionPO(Item_id, Search);
        }

        public List<Purchases> PreviousOrders(int Item_id, int VendorId)
        {
            return new PurchaseDAL().PreviousOrders(Item_id, VendorId);
        }

        public List<Bills> ItemsTransactionBill(int Item_id, string Search)
        {
            return new PurchaseDAL().ItemsTransactionBill(Item_id, Search);
        }

        public List<Purchases> AllPurchases(string Option, string search, string From, string To, int User_id)
        {
            return new PurchaseDAL().SelectAll(Option, search, From, To, User_id);
        }

        public List<Premisess> AllStores(string Option, string search, string From, string To)
        {
            return new PremisesDAL().SelectAllStores(Option, search, From, To);
        }

        public List<Premisess> AllOffices(string Option, string search, string From, string To)
        {
            return new PremisesDAL().SelectAllOffices(Option, search, From, To);
        }

        public List<Premisess> AllShops(string Option, string search, string From, string To)
        {
            return new PremisesDAL().SelectAllShops(Option, search, From, To);
        }

        public List<Premisess> AllFactories(string Option, string search, string From, string To)
        {
            return new PremisesDAL().SelectAllFactories(Option, search, From, To);
        }

        public List<Users> AllUsers(string Option, string search, string From, string To, int? id)
        {
            return new UserDAL().SelectAllIAOA(Option, search, From, To, id);
        }

        public List<Premisess> PremisesByType(string Office, string Factories, string Stores, string Shops)
        {
            return new PremisesDAL().PremisesByType(Office, Factories, Stores, Shops);
        }

        public List<UserPrivilegess> UserPrivileges(string email, string Password)
        {
            return new AdminDAL().UserPrivileges(email, Password);
        }

        public List<Roles> AllRoles()
        {
            return new AdminDAL().AllRoles();
        }

        public List<RolePrivileges> RolePrivileges(string search)
        {
            return new AdminDAL().RolePrivileges(search);
        }

        public RolePrivileges Privileges(int Priv_id)
        {
            return new AdminDAL().Privileges(Priv_id);
        }

        public List<UserPrivilegess> UserPrivilegess(int User_id)
        {
            return new AdminDAL().UserPrivilegess(User_id);
        }

        public List<Companies> SelectAllAICompanies(string Option, string search, string From, string To, int? id)
        {
            return new CompanyDAL().SelectAllCompanies(Option, search, From, To, id);
        }

        public List<Countries> SelectAllAICountries(string Option, string search, string From, string To)
        {
            return new CountryDAL().SelectAllCountries(Option, search, From, To);
        }

        public List<Cities> SelectAllAICities(string Option, string search, string From, string To)
        {
            return new CityDAL().SelectAllCities(Option, search, From, To);
        }

        public List<Purchases> SelectAllPItems(int id)
        {
            return new PurchaseDAL().SelectAllPItems(id);
        }

        public List<Categories> AllCategories(string Option, string search, string From, string To)
        {
            return new CategoryDAL().SelectAll(Option, search, From, To);
        }

        public List<Brands> AllBrands(string Option, string search, string From, string To)
        {
            return new BrandDAL().SelectAll(Option, search, From, To);
        }

        public List<Manufacturers> AllManufaturers(string Option, string search, string From, string To)
        {
            return new ManufacturerDAL().SelectAll(Option, search, From, To);
        }

        public List<Units> AllUnits(string Option, string search, string From, string To)
        {
            return new UnitDAL().SelectAll(Option, search, From, To);
        }

        public List<Bills> AllBills(string search, string From, string To, int User_id)
        {
            return new BillDAL().SelectAll(search, From, To, User_id);
        }

        public List<SalesOrders> AllSalesOrders(string Option, string search, string From, string To, int User_id)
        {
            return new SalesOrderDAL().SelectAll(Option, search, From, To, User_id);
        }

        public List<SalesOrders> SelectAllSOItems(int id)
        {
            return new SalesOrderDAL().SelectAllSOItems(id);
        }

        public List<Purchases> SelectItemsForBillByPOid(int id)
        {
            return new PurchaseDAL().SelectItemsForBillByPOid(id);
        }

        public List<SalesOrders> SelectItemsForSaleRetrurn(int id)
        {
            return new SalesOrderDAL().SelectItemsForSaleRetrurn(id);
        }

        public List<SaleReturns> SelectAllSRItemsSO_id(int id)
        {
            return new SalesOrderDAL().SelectAllSRItemsSO_id(id);
        }

        public List<DeleteRequests> SelectAllDeleteRequests()
        {
            return new AdminDAL().SelectAllDeleteRequests();
        }

        public List<SO_Payments> SelectSO_IvnvoicePayment(int id)
        {
            return new SalesOrderDAL().SelectSO_IvnvoicePayment(id);
        }

        public List<Packages> SelectPackagesForSO(int id)
        {
            return new SalesOrderDAL().SelectPackagesForSO(id);
        }

        public List<SaleReturns> SelectSaleReturnsForSO(int id)
        {
            return new SalesOrderDAL().SelectSaleReturnsForSO(id);
        }

        public List<Packages> SelectPackagesForShipment(int id)
        {
            return new SalesOrderDAL().SelectPackagesForShipment(id);
        }

        public List<Packages> SelectPackagedItemsBySOid(int id, int SO_ID)
        {
            return new SalesOrderDAL().SelectPackagedItemsBySOid(id, SO_ID);
        }

        public List<Payments> SelectPaymentByBillId(int id)
        {
            return new PaymentDAL().SelectPaymentByBillId(id);
        }

        public List<Payments> ViewPayments(string Option, string search, string From, string To)
        {
            return new PaymentDAL().ViewPayments(Option, search, From, To);
        }

        public List<Activities> Activities(int ActivityType_id, string ActivityType)
        {
            return new ActivitiesDAL().Activities(ActivityType_id, ActivityType);
        }

        public List<DeleteRequests> AllItemsDelRequest(string type)
        {
            return new DeleteRequestDAL().AllItemsDelRequest(type);
        }

        public List<Messages> Messages(int Sender_id, int Receiver_id)
        {
            return new MessagesDAL().Messages(Sender_id, Receiver_id);
        }

        public List<Messages> MessagesNotifications(int User_id)
        {
            return new MessagesDAL().MessagesNotifications(User_id);
        }

        public List<Calenders> GetEventsName(int User_id)
        {
            return new CalenderDAL().GetEventsName(User_id);
        }

        public List<Calenders> GetCalenderEvents(int User_id)
        {
            return new CalenderDAL().GetCalenderEvents(User_id);
        }

        public List<Purchases> SelectVendorTransactions_Items(int Vendor_id, string Search)
        {
            return new PurchaseDAL().SelectVendorTransactions_Items(Vendor_id, Search);
        }

        public List<Bills> VendorTransactions_Bills(int Vendor_id, string Search)
        {
            return new BillDAL().VendorTransactions_Bills(Vendor_id, Search);
        }

        public List<Payments> VendorTransactions_Payments(int Vendor_id, string Search)
        {
            return new PaymentDAL().VendorTransactions_Payments(Vendor_id, Search);
        }

        public List<SalesOrders> CustomerTransactions_Items(int Customer_id, string Search)
        {
            return new SalesOrderDAL().CustomerTransactions_Items(Customer_id, Search);
        }

        public List<SO_Invoices> CustomerTransactions_Invoices(int Customer_id, string Search)
        {
            return new SalesOrderDAL().CustomerTransactions_Invoices(Customer_id, Search);
        }

        public List<SO_Payments> CustomerTransactions_Payments(int Customer_id, string Search)
        {
            return new SalesOrderDAL().CustomerTransactions_Payments(Customer_id, Search);
        }

        /*----------Select A Record (With One Parameter)-----------*/

        public Contacts SelectContact(int id, int vop, int cop, int eop, int? sop)
        {
            return new ContactDAL().SelectById(id, vop, cop, eop, sop);
        }

        public Contacts SelectContact(int id, int? eop)
        {
            return new ContactDAL().SelectById(id, eop);
        }

        public Items SelectItem(int id, int? eop)
        {
            return new ItemDAL().SelectById(id, eop);
        }

        public Users SelectUser(int id)
        {
            return new UserDAL().SelectById(id);
        }

        public Users Login(Users U)
        {
            return new AdminDAL().Login(U);
        }

        public Premisess SelectPremises(int id)
        {
            return new PremisesDAL().SelectById(id);
        }

        public Companies SelectCompany(int id, int? sop)
        {
            return new CompanyDAL().SelectById(id, sop);
        }

        public Countries SelectCountry(int id, int? sop)
        {
            return new CountryDAL().SelectById(id, sop);
        }

        public Cities SelectCity(int id, int? sop)
        {
            return new CityDAL().SelectById(id, sop);
        }

        public Purchases SelectPurchase(int id)
        {
            return new PurchaseDAL().SelectPById(id);
        }

        public Bills SelectBillById(int id)
        {
            return new BillDAL().SelectBById(id);
        }

        public Bills SelectBillByPId(int id)
        {
            return new BillDAL().SelectBByPId(id);
        }

        public Categories SelectCategory(int id)
        {
            return new CategoryDAL().SelectById(id);
        }

        public Brands SelectBrand(int id)
        {
            return new BrandDAL().SelectById(id);
        }

        public Manufacturers SelectManufacture(int id)
        {
            return new ManufacturerDAL().SelectById(id);
        }

        public Units SelectUnit(int id)
        {
            return new UnitDAL().SelectById(id);
        }

        public Packages SlecetPKGBySO_ID(int id)
        {
            return new SalesOrderDAL().SlecetPKGBySO_ID(id);
        }

        public SalesOrders SelectSOById(int id)
        {
            return new SalesOrderDAL().SelectSOById(id);
        }

        public SO_Invoices SelectSOInvoicePayment(int id)
        {
            return new SalesOrderDAL().SelectSOInvoicePayment(id);
        }

        public SO_Invoices InvoiceByInvoiceId(int id)
        {
            return new SalesOrderDAL().InvoiceByInvoiceId(id);
        }

        public SO_Payments LastPaymentInvoice(int id)
        {
            return new SalesOrderDAL().LastPaymentInvoice(id);
        }

        public SalesOrders SelectSOdetailItems(int item_id, int SO_id)
        {
            return new SalesOrderDAL().SelectSOdetailItems(item_id, SO_id);
        }

        public Stocks SelectStockByItemid(int id)
        {
            return new StockDAL().SelectStockByItemid(id);
        }

        public Roles SelectRole(int id)
        {
            return new AdminDAL().SelectById(id);
        }

        public Brands CheckBrandForDelete(int id)
        {
            return new BrandDAL().CheckBrandForDelete(id);
        }

        public Categories CheckCategoryForDelete(int id)
        {
            return new CategoryDAL().CheckCategoryForDelete(id);
        }

        public Manufacturers CheckManufacturerForDelete(int id)
        {
            return new ManufacturerDAL().CheckManufacturerForDelete(id);
        }

        public Units CheckUnitForDelete(int id)
        {
            return new UnitDAL().CheckUnitForDelete(id);
        }

        public Items CheckItemForDelete(int id)
        {
            return new ItemDAL().CheckItemForDelete(id);
        }

        public Premisess CheckPremisesForDelete(int id)
        {
            return new PremisesDAL().CheckPremisesForDelete(id);
        }

        public Contacts CheckContactForDelete(int id)
        {
            return new ContactDAL().CheckContactForDelete(id);
        }

        public Companies CheckCompanyForDelete(int id)
        {
            return new CompanyDAL().CheckCompanyForDelete(id);
        }

        public Countries CheckCountryForDelete(int id)
        {
            return new CountryDAL().CheckCountryForDelete(id);
        }

        public Cities CheckCityForDelete(int id)
        {
            return new CityDAL().CheckCityForDelete(id);
        }

        public Tokens GetToken(string authenticationToken)
        {
            return new AdminDAL().GetToken(authenticationToken);
        }

        public Roles GetRoleByRoleName(string Role_Name)
        {
            return new AdminDAL().GetRoleByRoleName(Role_Name);
        }

        public Packages PackageByPackageId(int Package_id)
        {
            return new SalesOrderDAL().PackageByPackageId(Package_id);
        }

        public Packages PackageItemByItemId(int Package_id, int Item_id)
        {
            return new SalesOrderDAL().PackageItemByItemId(Package_id, Item_id);
        }

        public SaleReturns SaleReturnedItemyItem_id(int Package_id, int Item_id)
        {
            return new SalesOrderDAL().SaleReturnedItemyItem_id(Package_id, Item_id);
        }

        public Categories CategoryByName(string Category_Name)
        {
            return new CategoryDAL().CategoryByName(Category_Name);
        }

        public Units UnitByName(string Unit_Name)
        {
            return new UnitDAL().UnitByName(Unit_Name);
        }

        public Manufacturers ManufacturerByName(string Manufacturer_Name)
        {
            return new ManufacturerDAL().ManufacturerByName(Manufacturer_Name);
        }

        public Brands BrandByName(string Brand_Name)
        {
            return new BrandDAL().BrandByName(Brand_Name);
        }

        public Contacts VendorByName(string Name)
        {
            return new ContactDAL().VendorByName(Name);
        }

        public string getLastBillNo()
        {
            return new BillDAL().getLastBillNo();
        }

        /*----------Dropdowns-----------*/

        public List<Dropdowns> CategoriesDropdown()
        {
            return new DropdownsDAL().CategoriesDropdown();
        }

        public List<Dropdowns> BrandsDropdown()
        {
            return new DropdownsDAL().BrandsDropdown();
        }

        public List<Dropdowns> ManufacturersDropdown()
        {
            return new DropdownsDAL().ManufacturersDropdown();
        }

        public List<Dropdowns> UnitsDropdown()
        {
            return new DropdownsDAL().UnitsDropdown();
        }

        public List<Dropdowns> VendorsDropdown()
        {
            return new DropdownsDAL().VendorsDropdown();
        }

        public List<Dropdowns> ItemsDropdown()
        {
            return new DropdownsDAL().ItemsDropdown();
        }

        public List<Dropdowns> PaymentModesDropdown()
        {
            return new DropdownsDAL().PaymentModesDropdown();
        }

        public List<Dropdowns> CustomersDropdown()
        {
            return new DropdownsDAL().CustomersDropdown();
        }

        public List<Dropdowns> CountriesDropdown()
        {
            return new DropdownsDAL().CountriesDropdown();
        }

        public List<Dropdowns> CitiesDropdown(int Country_id)
        {
            return new DropdownsDAL().CitiesDropdown(Country_id);
        }

        public List<Dropdowns> RolesDropdown()
        {
            return new DropdownsDAL().RolesDropdown();
        }

        public List<Dropdowns> CompaniesDropdown()
        {
            return new DropdownsDAL().CompaniesDropdown();
        }

        public List<Dropdowns> UsersDropdown()
        {
            return new DropdownsDAL().UsersDropdown();
        }

        #region Dashboard
        public Dashboards SalesActivity()
        {
            return new DashboardDAL().SalesActivity();
        }

        public Dashboards ProductDetails()
        {
            return new DashboardDAL().ProductDetails();
        }

        public Dashboards PurchaseOrder()
        {
            return new DashboardDAL().PurchaseOrder();
        }

        public List<Dashboards> TopSellingItems()
        {
            return new DashboardDAL().TopSellingItems();
        }

        public Dashboards SalesOrder()
        {
            return new DashboardDAL().SalesOrder();
        }

        public Dashboards InventorySummary()
        {
            return new DashboardDAL().InventorySummary();
        }

        public Dashboards SalesOrderDetail()
        {
            return new DashboardDAL().SalesOrderDetail();
        }

        #endregion

        #region Reports
        public List<Reports> Inventory_ProductSalesReport(string StartDate, string EndDate, string Search)
        {
            return new ReportsDAL().Inventory_ProductSalesReport(StartDate, EndDate, Search);
        }

        public List<Reports> Inventory_InventoryDetailsReport(string StartDate, string EndDate, string ItemName, string Search)
        {
            return new ReportsDAL().Inventory_InventoryDetailsReport(StartDate, EndDate, ItemName, Search);
        }

        public List<Reports> Inventory_InventoryValuationSummaryReport(string StartDate, string EndDate, string ItemName, string Search)
        {
            return new ReportsDAL().Inventory_InventoryValuationSummaryReport(StartDate, EndDate, ItemName, Search);
        }

        public List<Reports> Inventory_StockSummaryReport(string StartDate, string EndDate, string ItemName, string Search)
        {
            return new ReportsDAL().Inventory_StockSummaryReport(StartDate, EndDate, ItemName, Search);
        }

        public List<Reports> Sales_SalesOrderHistoryReport(string StartDate, string EndDate, string Status, string Search)
        {
            return new ReportsDAL().Sales_SalesOrderHistoryReport(StartDate, EndDate, Status, Search);
        }

        public List<Reports> Sales_OrderFulfillmentByItemReport(string StartDate, string EndDate, string Search)
        {
            return new ReportsDAL().Sales_OrderFulfillmentByItemReport(StartDate, EndDate, Search);
        }

        public List<Reports> Sales_InvoiceHistoryReport(string StartDate, string EndDate, string Status, string Search)
        {
            return new ReportsDAL().Sales_InvoiceHistoryReport(StartDate, EndDate, Status, Search);
        }

        public List<Reports> Sales_PaymentsReceivedReport(string StartDate, string EndDate, string Search)
        {
            return new ReportsDAL().Sales_PaymentsReceivedReport(StartDate, EndDate, Search);
        }

        public List<Reports> Sales_PackingHistoryReport(string StartDate, string EndDate, string Status, string Search)
        {
            return new ReportsDAL().Sales_PackingHistoryReport(StartDate, EndDate, Status, Search);
        }

        public List<Reports> Sales_SalesByCustomerReport(string StartDate, string EndDate, string CustomerName, string Search)
        {
            return new ReportsDAL().Sales_SalesByCustomerReport(StartDate, EndDate, CustomerName, Search);
        }

        public List<Reports> Sales_SalesByItemReport(string StartDate, string EndDate, string ItemName, string Search)
        {
            return new ReportsDAL().Sales_SalesByItemReport(StartDate, EndDate, ItemName, Search);
        }

        public List<Reports> Sales_SalesBySalesPersonReport(string StartDate, string EndDate, string UserName, string Search)
        {
            return new ReportsDAL().Sales_SalesBySalesPersonReport(StartDate, EndDate, UserName, Search);
        }

        public List<Reports> Purchases_PurchaseOrderHistoryReport(string StartDate, string EndDate, string Status, string Search)
        {
            return new ReportsDAL().Purchases_PurchaseOrderHistoryReport(StartDate, EndDate, Status, Search);
        }

        public List<Reports> Purchases_PurchaseByVendorReport(string StartDate, string EndDate, string Search)
        {
            return new ReportsDAL().Purchases_PurchaseByVendorReport(StartDate, EndDate, Search);
        }

        public List<Reports> Purchases_PurchaseByItemReport(string StartDate, string EndDate, string Search)
        {
            return new ReportsDAL().Purchases_PurchaseByItemReport(StartDate, EndDate, Search);
        }

        public List<Reports> Purchases_BillDetailsReport(string StartDate, string EndDate, string Search)
        {
            return new ReportsDAL().Purchases_BillDetailsReport(StartDate, EndDate, Search);
        }

        public List<Reports> ActivityLogsReport(string StartDate, string EndDate, string Search)
        {
            return new ReportsDAL().ActivityLogsReport(StartDate, EndDate, Search);
        }
        #endregion
    }
}
