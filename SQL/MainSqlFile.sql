USE [master]
GO
/****** Object:  Database [G_Accounting_Systems]    Script Date: 9/21/2019 2:13:55 PM ******/
CREATE DATABASE [G_Accounting_Systems]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'G_Accounting_Systems', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\G_Accounting_Systems.mdf' , SIZE = 16384KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'G_Accounting_Systems_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\G_Accounting_Systems_log.ldf' , SIZE = 1536KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [G_Accounting_Systems] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [G_Accounting_Systems].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [G_Accounting_Systems] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [G_Accounting_Systems] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [G_Accounting_Systems] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [G_Accounting_Systems] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [G_Accounting_Systems] SET ARITHABORT OFF 
GO
ALTER DATABASE [G_Accounting_Systems] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [G_Accounting_Systems] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [G_Accounting_Systems] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [G_Accounting_Systems] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [G_Accounting_Systems] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [G_Accounting_Systems] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [G_Accounting_Systems] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [G_Accounting_Systems] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [G_Accounting_Systems] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [G_Accounting_Systems] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [G_Accounting_Systems] SET  DISABLE_BROKER 
GO
ALTER DATABASE [G_Accounting_Systems] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [G_Accounting_Systems] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [G_Accounting_Systems] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [G_Accounting_Systems] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [G_Accounting_Systems] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [G_Accounting_Systems] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [G_Accounting_Systems] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [G_Accounting_Systems] SET RECOVERY FULL 
GO
ALTER DATABASE [G_Accounting_Systems] SET  MULTI_USER 
GO
ALTER DATABASE [G_Accounting_Systems] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [G_Accounting_Systems] SET DB_CHAINING OFF 
GO
ALTER DATABASE [G_Accounting_Systems] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [G_Accounting_Systems] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'G_Accounting_Systems', N'ON'
GO
USE [G_Accounting_Systems]
GO
/****** Object:  UserDefinedTableType [dbo].[DeleteBrandsRequested_Datatable]    Script Date: 9/21/2019 2:13:55 PM ******/
CREATE TYPE [dbo].[DeleteBrandsRequested_Datatable] AS TABLE(
	[Brand_id] [int] NULL,
	[Enable] [int] NULL,
	[Delete_Request_By] [int] NULL,
	[Delete_Status] [nvarchar](50) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[DeleteCategoryRequested_Datatable]    Script Date: 9/21/2019 2:13:55 PM ******/
CREATE TYPE [dbo].[DeleteCategoryRequested_Datatable] AS TABLE(
	[Category_id] [int] NULL,
	[Enable] [int] NULL,
	[Delete_Request_By] [int] NULL,
	[Delete_Status] [nvarchar](50) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[DeleteCitiesRequested_Datatable]    Script Date: 9/21/2019 2:13:55 PM ******/
CREATE TYPE [dbo].[DeleteCitiesRequested_Datatable] AS TABLE(
	[id] [int] NULL,
	[Enable] [int] NULL,
	[Delete_Request_By] [int] NULL,
	[Delete_Status] [nvarchar](50) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[DeleteCompaniesRequested_Datatable]    Script Date: 9/21/2019 2:13:55 PM ******/
CREATE TYPE [dbo].[DeleteCompaniesRequested_Datatable] AS TABLE(
	[id] [int] NULL,
	[Enable] [int] NULL,
	[Delete_Request_By] [int] NULL,
	[Delete_Status] [nvarchar](50) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[DeleteContactsRequested_Datatable]    Script Date: 9/21/2019 2:13:55 PM ******/
CREATE TYPE [dbo].[DeleteContactsRequested_Datatable] AS TABLE(
	[id] [int] NULL,
	[Enable] [int] NULL,
	[Delete_Request_By] [int] NULL,
	[Delete_Status] [nvarchar](50) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[DeleteCountriesRequested_Datatable]    Script Date: 9/21/2019 2:13:55 PM ******/
CREATE TYPE [dbo].[DeleteCountriesRequested_Datatable] AS TABLE(
	[id] [int] NULL,
	[Enable] [int] NULL,
	[Delete_Request_By] [int] NULL,
	[Delete_Status] [nvarchar](50) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[DeleteItemRequested_Datatable]    Script Date: 9/21/2019 2:13:55 PM ******/
CREATE TYPE [dbo].[DeleteItemRequested_Datatable] AS TABLE(
	[Item_id] [int] NULL,
	[Enable] [int] NULL,
	[Delete_Request_By] [int] NULL,
	[Delete_Status] [nvarchar](50) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[DeleteManufacturerRequested_Datatable]    Script Date: 9/21/2019 2:13:55 PM ******/
CREATE TYPE [dbo].[DeleteManufacturerRequested_Datatable] AS TABLE(
	[Manufacturer_id] [int] NULL,
	[Enable] [int] NULL,
	[Delete_Request_By] [int] NULL,
	[Delete_Status] [nvarchar](50) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[DeletePremisesRequested_Datatable]    Script Date: 9/21/2019 2:13:55 PM ******/
CREATE TYPE [dbo].[DeletePremisesRequested_Datatable] AS TABLE(
	[id] [int] NULL,
	[Enable] [int] NULL,
	[Delete_Request_By] [int] NULL,
	[Delete_Status] [nvarchar](50) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[DeletePurchasingsRequested_Datatable]    Script Date: 9/21/2019 2:13:55 PM ******/
CREATE TYPE [dbo].[DeletePurchasingsRequested_Datatable] AS TABLE(
	[id] [int] NULL,
	[Enable] [int] NULL,
	[Delete_Request_By] [int] NULL,
	[Delete_Status] [nvarchar](50) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[DeleteSalesRequested_Datatable]    Script Date: 9/21/2019 2:13:55 PM ******/
CREATE TYPE [dbo].[DeleteSalesRequested_Datatable] AS TABLE(
	[id] [int] NULL,
	[Enable] [int] NULL,
	[Delete_Request_By] [int] NULL,
	[Delete_Status] [nvarchar](50) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[DeleteUnitRequested_Datatable]    Script Date: 9/21/2019 2:13:55 PM ******/
CREATE TYPE [dbo].[DeleteUnitRequested_Datatable] AS TABLE(
	[Unit_id] [int] NULL,
	[Enable] [int] NULL,
	[Delete_Request_By] [int] NULL,
	[Delete_Status] [nvarchar](50) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[DeleteUsersRequested_Datatable]    Script Date: 9/21/2019 2:13:55 PM ******/
CREATE TYPE [dbo].[DeleteUsersRequested_Datatable] AS TABLE(
	[id] [int] NULL,
	[Enable] [int] NULL,
	[Delete_Request_By] [int] NULL,
	[Delete_Status] [nvarchar](50) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[ItemActivity_Datatable]    Script Date: 9/21/2019 2:13:55 PM ******/
CREATE TYPE [dbo].[ItemActivity_Datatable] AS TABLE(
	[ActivityType_id] [int] NULL,
	[ActivityType] [nvarchar](50) NULL,
	[ActivityName] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[Date] [nvarchar](50) NULL,
	[Time] [nvarchar](50) NULL,
	[User_id] [int] NULL,
	[Icon] [nvarchar](50) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[MailAttachments_Datatable]    Script Date: 9/21/2019 2:13:55 PM ******/
CREATE TYPE [dbo].[MailAttachments_Datatable] AS TABLE(
	[Mailbox_id] [int] NULL,
	[FileName] [nvarchar](max) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[PackageDetail_Datatable]    Script Date: 9/21/2019 2:13:55 PM ******/
CREATE TYPE [dbo].[PackageDetail_Datatable] AS TABLE(
	[SaleOrder_id] [int] NULL,
	[Package_No] [nvarchar](50) NULL,
	[Item_id] [int] NULL,
	[UnitPrice] [int] NULL,
	[Packed_Item_Qty] [nvarchar](50) NULL,
	[Package_Date] [nvarchar](50) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[PurchaseOrderDetail_Datatable]    Script Date: 9/21/2019 2:13:55 PM ******/
CREATE TYPE [dbo].[PurchaseOrderDetail_Datatable] AS TABLE(
	[pd_id] [int] NULL,
	[PurchasingId] [int] NULL,
	[ItemId] [int] NULL,
	[VendorId] [int] NULL,
	[Qty] [nvarchar](50) NULL,
	[PriceUnit] [nvarchar](50) NULL,
	[MsrmntUnit] [nvarchar](50) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[ReturnReceiving_Datatable]    Script Date: 9/21/2019 2:13:55 PM ******/
CREATE TYPE [dbo].[ReturnReceiving_Datatable] AS TABLE(
	[SaleReturn_id] [int] NULL,
	[Package_id] [int] NULL,
	[Item_id] [int] NULL,
	[Return_Qty] [nvarchar](50) NULL,
	[Received_Qty] [nvarchar](50) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[RolePrivDataTable]    Script Date: 9/21/2019 2:13:55 PM ******/
CREATE TYPE [dbo].[RolePrivDataTable] AS TABLE(
	[Role_Priv_id] [int] NULL,
	[Role_id] [int] NULL,
	[Priv_id] [int] NULL,
	[Check_Status] [nvarchar](50) NULL,
	[Enable] [nvarchar](50) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[SaleOrderDetail_Datatable]    Script Date: 9/21/2019 2:13:55 PM ******/
CREATE TYPE [dbo].[SaleOrderDetail_Datatable] AS TABLE(
	[id] [int] NULL,
	[SalesOrder_id] [int] NULL,
	[ItemId] [int] NULL,
	[Customer_id] [int] NULL,
	[Qty] [nvarchar](50) NULL,
	[PriceUnit] [nvarchar](50) NULL,
	[MsrmntUnit] [nvarchar](50) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[SaleReturn_Datatable]    Script Date: 9/21/2019 2:13:55 PM ******/
CREATE TYPE [dbo].[SaleReturn_Datatable] AS TABLE(
	[SaleReturn_id] [int] NULL,
	[Package_id] [int] NULL,
	[Item_id] [int] NULL,
	[Return_Qty] [nvarchar](50) NULL,
	[ReturnQty_Cost] [nvarchar](50) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[ShipmentPackages_Datatable]    Script Date: 9/21/2019 2:13:55 PM ******/
CREATE TYPE [dbo].[ShipmentPackages_Datatable] AS TABLE(
	[SaleOrder_id] [int] NULL,
	[Shipment_No] [nvarchar](50) NULL,
	[Package_id] [int] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[Stock_Datatable]    Script Date: 9/21/2019 2:13:55 PM ******/
CREATE TYPE [dbo].[Stock_Datatable] AS TABLE(
	[Stock_id] [int] NULL,
	[Item_id] [int] NULL,
	[Physical_Quantity] [nvarchar](50) NULL,
	[Physical_Avail_ForSale] [nvarchar](50) NULL,
	[Physical_Committed] [nvarchar](50) NULL,
	[Accounting_Quantity] [nvarchar](50) NULL,
	[Acc_Avail_ForSale] [nvarchar](50) NULL,
	[Acc_Commited] [nvarchar](50) NULL,
	[OpeningStock] [nvarchar](50) NULL,
	[ReorderLevel] [nvarchar](50) NULL,
	[Time_Of_Day] [nvarchar](50) NULL,
	[Date_Of_Day] [nvarchar](50) NULL,
	[Month_Of_Day] [nvarchar](50) NULL,
	[Year_Of_Day] [nvarchar](50) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[UserPrivDataTable]    Script Date: 9/21/2019 2:13:55 PM ******/
CREATE TYPE [dbo].[UserPrivDataTable] AS TABLE(
	[id] [int] NULL,
	[Priv_id] [int] NULL,
	[User_id] [int] NULL,
	[Add] [int] NULL,
	[Edit] [int] NULL,
	[View] [int] NULL,
	[Profile] [int] NULL
)
GO
/****** Object:  StoredProcedure [dbo].[proc_AddCalenderEvents]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Salman Ahmed>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_AddCalenderEvents]
	-- Add the parameters for the stored procedure here

	@pEvent_id								int,
	@pEvent_Start_Date						nvarchar(MAX),
	@pStartedBy								int,
	@pTime_Of_Day							nvarchar(MAX),
	@pDate_Of_Day							nvarchar(MAX),
	@pMonth_Of_Day							nvarchar(MAX),
	@pYear_Of_Day							nvarchar(MAX),
	@pFlag									bit = 0 OUTPUT,
	@pDesc									varchar(50) = NULL OUTPUT



AS
BEGIN TRAN
		INSERT INTO CalenderEvents(Event_id,Event_Start_Date,StartedBy,Time_Of_Day,Date_Of_Day,Month_Of_Day,Year_Of_Day)
	
			VALUES(@pEvent_id,@pEvent_Start_Date,@pStartedBy,@pTime_Of_Day,@pDate_Of_Day,@pMonth_Of_Day,@pYear_Of_Day)	
				IF @@Error = 0
					BEGIN	
						SET @pFlag = 1
						SET @pDesc =  'Event Started Successfully'
						COMMIT TRAN
					END
					ELSE
					BEGIN
						SET @pFlag = 0
						SET @pDesc =  @@error
						ROLLBACK  TRAN
					END
						


GO
/****** Object:  StoredProcedure [dbo].[proc_AddEvents]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Salman Ahmed>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_AddEvents]
	-- Add the parameters for the stored procedure here

	@pEventName							nvarchar(MAX),
	@pBackgroundColour					nvarchar(MAX),
	@pBorderColour						nvarchar(MAX),
	@pAddedBy							int = 0,
	@pTime_Of_Day						nvarchar(50),
	@pDate_Of_Day						nvarchar(50),
	@pMonth_Of_Day						nvarchar(50),
	@pYear_Of_Day						nvarchar(50),
	@pFlag								bit = 0 OUTPUT,
	@pDesc								varchar(50) = NULL OUTPUT,
	@pEventid_Out						varchar(50) = NULL OUTPUT






AS
BEGIN TRAN
	INSERT INTO Events(EventName,BackgroundColour,BorderColour,AddedBy,Time_Of_Day,Date_Of_Day,Month_Of_Day,Year_Of_Day)
	
			VALUES(@pEventName,@pBackgroundColour,@pBorderColour,@pAddedBy,@pTime_Of_Day,@pDate_Of_Day,@pMonth_Of_Day,@pYear_Of_Day)	
				IF @@Error = 0
					BEGIN	
						SET @pFlag = 1
						SET @pDesc =  'Event Added Successfully'
						SET @pEventid_Out = IDENT_CURRENT('Events')
						COMMIT TRAN
					END
					ELSE
					BEGIN
						SET @pFlag = 0
						SET @pDesc =  @@error
						ROLLBACK  TRAN
					END
						


GO
/****** Object:  StoredProcedure [dbo].[proc_ChangePassword]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Salman Ahmed>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_ChangePassword] --2,'1234','123'
	-- Add the parameters for the stored procedure here

	@pid								int = 0,
	@pCurrentPassword					nvarchar(MAX),
	@pNewPassword						nvarchar(MAX),
	@pFlag								bit = 0 OUTPUT,
	@pDesc								varchar(MAX) = NULL OUTPUT


AS
	BEGIN TRAN
			IF NOT Exists (SELECT password FROM Users U WHERE (password) = (@pCurrentPassword) and id = @pid)
			BEGIN
				SET @pFlag = 0
				SET @pDesc =  'Incorrect Password'
				print @pDesc
				COMMIT TRAN
			END
			ELSE
			BEGIN
			IF Exists (SELECT password FROM Users U WHERE (password) = (@pNewPassword) and id = @pid)
			BEGIN
				SET @pFlag = 0
				SET @pDesc =  'Cannot change! New password is same as previous password'
				print @pDesc
				COMMIT TRAN
			END
			ELSE
			BEGIN
			UPDATE Users
				SET password = @pNewPassword
				WHERE id = @pid
				IF @@Error = 0
				BEGIN	
					SET @pFlag = 1
					SET @pDesc =  'Password Changed Successfully'
					print @pDesc
					COMMIT TRAN
				END
				ELSE
				BEGIN
					SET @pFlag = 0
					SET @pDesc =  @@error
					print @pDesc
					ROLLBACK  TRAN
				END
			END

END

GO
/****** Object:  StoredProcedure [dbo].[proc_Check_Brands_For_Delete]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Salman Ahmed>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Check_Brands_For_Delete] 
	-- Add the parameters for the stored procedure here

	@pBrand_id							int

AS
	BEGIN TRAN
				IF Exists (SELECT Item_Brand FROM Items I WHERE I.Item_Brand = @pBrand_id AND I.Enable = 1)
				BEGIN
					SELECT Brand_Name FROM Brands WHERE Brand_id = @pBrand_id
				END
	COMMIT TRAN

GO
/****** Object:  StoredProcedure [dbo].[proc_Check_Category_For_Delete]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Salman Ahmed>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Check_Category_For_Delete] 
	-- Add the parameters for the stored procedure here

	@pCategory_id							int

AS
	BEGIN TRAN
				IF Exists (SELECT Item_Category FROM Items I WHERE Item_Category = @pCategory_id AND Enable = 1)
				BEGIN
					SELECT Category_Name FROM Categories WHERE Category_id = @pCategory_id
				END
	COMMIT TRAN

GO
/****** Object:  StoredProcedure [dbo].[proc_Check_City_For_Delete]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Salman Ahmed>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Check_City_For_Delete] 
	-- Add the parameters for the stored procedure here

	@pCity_id							int

AS
	BEGIN TRAN
			IF Exists (SELECT City FROM Companies C WHERE City = @pCity_id AND Enable = 1)
			BEGIN
				SELECT Name FROM Countries WHERE id = @pCity_id
			END
			ELSE IF Exists (SELECT City FROM Contacts C WHERE City = @pCity_id AND Enable = 1)
			BEGIN
				SELECT Name FROM Countries WHERE id = @pCity_id
			END
			ELSE IF Exists (SELECT City FROM Premises P WHERE City = @pCity_id AND Enable = 1)
			BEGIN
				SELECT Name FROM Countries WHERE id = @pCity_id
			END
	COMMIT TRAN

GO
/****** Object:  StoredProcedure [dbo].[proc_Check_Company_For_Delete]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Salman Ahmed>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Check_Company_For_Delete] --4
	-- Add the parameters for the stored procedure here

	@pCompany_id							int

AS
	BEGIN TRAN
				IF Exists (SELECT Company FROM Contacts I WHERE Company = @pCompany_id AND Enable = 1)
				BEGIN
					SELECT Name FROM Companies WHERE id = @pCompany_id
				END
	COMMIT TRAN

GO
/****** Object:  StoredProcedure [dbo].[proc_Check_Contacts_For_Delete]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Salman Ahmed>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Check_Contacts_For_Delete]
	-- Add the parameters for the stored procedure here

	@pContact_id							int,
	@pExits									nvarchar(50) = NULL OUTPUT
AS
	BEGIN TRAN
		IF Exists (SELECT VendorId FROM PurchasingDetails P WHERE VendorId = @pContact_id AND (SELECT Enable FROM Purchasing WHERE id = p.PurchasingId) = 1)
		BEGIN
			SELECT Name FROM Contacts where id = @pContact_id
		END
		ELSE IF Exists (SELECT Item_Preferred_Vendor FROM Items I WHERE Item_Preferred_Vendor = @pContact_id AND Enable = 1)
		BEGIN
			SELECT Name FROM Contacts where id = @pContact_id
		END
		

	print @pExits
	COMMIT TRAN

GO
/****** Object:  StoredProcedure [dbo].[proc_Check_Country_For_Delete]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Salman Ahmed>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Check_Country_For_Delete] 
	-- Add the parameters for the stored procedure here

	@pCountry_id							int

AS
	BEGIN TRAN
			IF Exists (SELECT Country FROM Cities C WHERE Country = @pCountry_id AND Enable = 1)
			BEGIN
				SELECT Name FROM Countries WHERE id = @pCountry_id
			END
			ELSE IF Exists (SELECT Country FROM Companies C WHERE Country = @pCountry_id AND Enable = 1)
			BEGIN
				SELECT Name FROM Countries WHERE id = @pCountry_id
			END
			ELSE IF Exists (SELECT Country FROM Contacts C WHERE Country = @pCountry_id AND Enable = 1)
			BEGIN
				SELECT Name FROM Countries WHERE id = @pCountry_id
			END
			ELSE IF Exists (SELECT Country FROM Premises P WHERE Country = @pCountry_id AND Enable = 1)
			BEGIN
				SELECT Name FROM Countries WHERE id = @pCountry_id
			END
	COMMIT TRAN

GO
/****** Object:  StoredProcedure [dbo].[proc_Check_Item_For_Delete]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Salman Ahmed>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Check_Item_For_Delete] --1 
	-- Add the parameters for the stored procedure here

	@pItem_id							int

AS
	BEGIN TRAN
			IF Exists (SELECT ItemId FROM PurchasingDetails as pd WHERE ItemId = @pItem_id AND (SELECT Enable FROM Purchasing WHERE id = pd.PurchasingId) = 1)
			BEGIN
				SELECT Item_Name FROM Items WHERE Item_id = @pItem_id
			END
			ELSE IF Exists (SELECT ItemId FROM SalesOrder_Details sd WHERE ItemId = @pItem_id AND (SELECT Enable FROM SalesOrder WHERE id = sd.SalesOrder_id) = 1)
			BEGIN
				SELECT Item_Name FROM Items WHERE Item_id = @pItem_id
			END
	COMMIT TRAN

GO
/****** Object:  StoredProcedure [dbo].[proc_Check_Manufacturer_For_Delete]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Salman Ahmed>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Check_Manufacturer_For_Delete] 
	-- Add the parameters for the stored procedure here

	@pManufacturer_id							int

AS
	BEGIN TRAN
				IF Exists (SELECT Item_Manufacturer FROM Items I WHERE Item_Manufacturer = @pManufacturer_id AND Enable = 1)
				BEGIN
					SELECT Manufacturer_Name FROM Manufacturers WHERE Manufacturer_id = @pManufacturer_id
				END
	COMMIT TRAN

GO
/****** Object:  StoredProcedure [dbo].[proc_Check_Premises_For_Delete]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Salman Ahmed>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Check_Premises_For_Delete] --19
	-- Add the parameters for the stored procedure here

	@pPremises_id							int,
	@pExits									nvarchar(50) = NULL OUTPUT
AS
	BEGIN TRAN
		IF Exists (SELECT PremisesId FROM Purchasing P WHERE PremisesId = @pPremises_id)
		BEGIN
			SELECT Name FROM Premises where id = @pPremises_id
		END
		ELSE IF Exists (SELECT PremisesId FROM SalesOrder S WHERE PremisesId = @pPremises_id)
		BEGIN
			SELECT Name FROM Premises where id = @pPremises_id
		END
		ELSE IF Exists (SELECT Premises_id FROM Users U WHERE Premises_id = @pPremises_id)
		BEGIN
			SELECT Name FROM Premises where id = @pPremises_id
		END
		

	print @pExits
	COMMIT TRAN

GO
/****** Object:  StoredProcedure [dbo].[proc_Check_Purchasing_For_Delete]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Salman Ahmed>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Check_Purchasing_For_Delete] 
	-- Add the parameters for the stored procedure here

	@pid							int

AS
	BEGIN TRAN
				IF Exists (SELECT Purchase_id FROM Bills B WHERE Purchase_id = @pid)
				BEGIN
					SELECT TempOrderNum FROM Purchasing WHERE id = @pid
				END
	COMMIT TRAN

GO
/****** Object:  StoredProcedure [dbo].[proc_Check_Unit_For_Delete]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Salman Ahmed>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Check_Unit_For_Delete] 
	-- Add the parameters for the stored procedure here

	@pUnit_id							int

AS
	BEGIN TRAN
				IF Exists (SELECT Item_Unit FROM Items I WHERE Item_Unit = @pUnit_id)
				BEGIN
					SELECT Unit_Name FROM Units WHERE Unit_id = @pUnit_id
				END
	COMMIT TRAN

GO
/****** Object:  StoredProcedure [dbo].[proc_Contact_By_Type]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Contact_By_Type] --'0','1','0'

	@pVendor				nvarchar(50) = NULL,
	@pCustomer				nvarchar(50) = NULL,
	@pEmployee				nvarchar(50) = NULL
AS
BEGIN
	SELECT 
	id,
	Salutation,
	Name,
	Company,
	Designation,
	Landline,
	Mobile,
	Email,
	Website,
	Address,
	AddressLandline,
	City,
	Country,
	BankAccountNumber,
	PaymentMethod,
	Vendor,
	Customer,
	Employee,
	Enable,
	AddedBy,
	UpdatedBy,
	Delete_Request_By,
	Delete_Status,
	TimeOfDay,
	DateOfDay,
	MonthOfDay,
	YearOfDay

	FROM Contacts AS c
	WHERE Vendor = @pVendor
	AND Customer = @pCustomer
	AND Employee = @pEmployee
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Dashboard_Inventory_Summary]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,> 
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Dashboard_Inventory_Summary] 
	

AS
BEGIN
	SELECT 
		Item_id,
		Item_Name,
		(SELECT Physical_Quantity FROM Stock WHERE Item_id = i.Item_id) as QuantityInHand


	FROM Items as i

END

GO
/****** Object:  StoredProcedure [dbo].[proc_Dashboard_ProductDetails]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Dashboard_ProductDetails] 
	

AS
BEGIN
	SELECT 
		COUNT(*) as TotalItems

	FROM Items as i
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Dashboard_PurchaseOrder]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Dashboard_PurchaseOrder] 
	

AS
BEGIN
	SELECT
		SUM(cast(Qty as int)) as QuantityOrdered,
		SUM(cast(Qty as int)*cast(PriceUnit as int)) as TotalCost

	FROM PurchasingDetails as p

END

GO
/****** Object:  StoredProcedure [dbo].[proc_Dashboard_SalesActivity]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,> 
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Dashboard_SalesActivity] 
	

AS
BEGIN
	SELECT
	(SELECT SUM(cast(s.Qty as int))-SUM(cast(Packed_Item_Qty as int)) FROM SO_Packages_Detail) as ToBePacked,
	(SELECT COUNT(Package_id) FROM SO_Packages WHERE Package_Status = 'Not Shipped') as ToBeShipped,
	(SELECT COUNT(Shipment_id) FROM Shipment WHERE Shipment_Status='Shipped') as ToBeDelivered,
	(SELECT SUM(cast(Qty as int)) FROM SalesOrder_Invoices i inner join SalesOrder_Details as sd on i.SalesOrder_id = sd.SalesOrder_id) as ToBeInvoiced
	


	FROM SalesOrder_Details as s
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Dashboard_SalesOrder]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Dashboard_SalesOrder] 
	

AS
BEGIN
	SELECT
		SUM(cast(Qty as int)) as QuantitySold,
		SUM(cast(Qty as int)*cast(PriceUnit as int)) as TotalCost

	FROM SalesOrder_Details as p

END

GO
/****** Object:  StoredProcedure [dbo].[proc_Dashboard_SO_Summary]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,> 
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Dashboard_SO_Summary] 
	

AS
BEGIN
	SELECT DISTINCT
	(SELECT COUNT(id) FROM SalesOrder WHERE SO_Status='Draft') as Draft,
	(SELECT COUNT(id) FROM SalesOrder WHERE SO_Status='Confirm') as Confirmed,
	(SELECT COUNT(id) FROM SalesOrder WHERE SO_Package_Status='True') as Packed,
	(SELECT COUNT(id) FROM SalesOrder WHERE SO_Shipment_Status='True') as Shipped,
	(SELECT COUNT(id) FROM SalesOrder WHERE SO_Invoice_Status='Invoiced') as Invoiced
	
	


	FROM SalesOrder as s

END

GO
/****** Object:  StoredProcedure [dbo].[proc_Dashboard_TopSellingItems]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Dashboard_TopSellingItems] 
	

AS
BEGIN
	SELECT TOP 4
		ItemId,
		(SELECT Item_Name FROM Items WHERE Item_id = s.ItemId) as ItemName,
		(SELECT Item_file FROM Items WHERE Item_id = s.ItemId) as ItemImage,
		SUM(cast(Qty as int)) as QuantitySold,
		COUNT(ItemId) AS most

	FROM SalesOrder_Details as s
	GROUP BY ItemId
	ORDER BY QuantitySold DESC
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Delete_Brands]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Delete_Brands]

		@pid				int,
		@ptype				nvarchar(50)

AS
	BEGIN TRAN
	
			DELETE FROM Brands where Brand_id = @pid

			IF @@Error = 0
				BEGIN	
				DECLARE @rowCount 	BIGINT
				SET @rowCount = (SELECT count FROM DeleteRequests where Type = @ptype)

					UPDATE DeleteRequests set Count = @rowCount - 1 where Type = @ptype					
				END
				
		Commit Tran			


GO
/****** Object:  StoredProcedure [dbo].[proc_Delete_Categories]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Delete_Categories]

		@pid				int,
		@ptype				nvarchar(50)

AS
	BEGIN TRAN
	
			DELETE FROM Categories where Category_id = @pid

			IF @@Error = 0
				BEGIN	
				DECLARE @rowCount 	BIGINT
				SET @rowCount = (SELECT count FROM DeleteRequests where Type = @ptype)

					UPDATE DeleteRequests set Count = @rowCount - 1 where Type = @ptype					
				END
				
		Commit Tran			


GO
/****** Object:  StoredProcedure [dbo].[proc_Delete_Cities]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Delete_Cities]

		@pid				int,
		@ptype				nvarchar(50)

AS
	BEGIN TRAN
	
			DELETE FROM Cities where id = @pid

			IF @@Error = 0
				BEGIN	
				DECLARE @rowCount 	BIGINT
				SET @rowCount = (SELECT count FROM DeleteRequests where Type = @ptype)

					UPDATE DeleteRequests set Count = @rowCount - 1 where Type = @ptype					
				END
				
		Commit Tran			


GO
/****** Object:  StoredProcedure [dbo].[proc_Delete_Companies]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Delete_Companies]

		@pid				int,
		@ptype				nvarchar(50)

AS
	BEGIN TRAN
	
			DELETE FROM Companies where id = @pid

			IF @@Error = 0
				BEGIN	
				DECLARE @rowCount 	BIGINT
				SET @rowCount = (SELECT count FROM DeleteRequests where Type = @ptype)

					UPDATE DeleteRequests set Count = @rowCount - 1 where Type = @ptype					
				END
				
		Commit Tran			


GO
/****** Object:  StoredProcedure [dbo].[proc_Delete_Contacts]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Delete_Contacts]

		@pid				int,
		@ptype				nvarchar(50)

AS
	BEGIN TRAN
	
			DELETE FROM Contacts where id = @pid

			IF @@Error = 0
				BEGIN	
				DECLARE @rowCount 	BIGINT
				SET @rowCount = (SELECT count FROM DeleteRequests where Type = @ptype)

					UPDATE DeleteRequests set Count = @rowCount - 1 where Type = @ptype					
				END
				
		Commit Tran			


GO
/****** Object:  StoredProcedure [dbo].[proc_Delete_Countries]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Delete_Countries]

		@pid				int,
		@ptype				nvarchar(50)

AS
	BEGIN TRAN
	
			DELETE FROM Countries where id = @pid

			IF @@Error = 0
				BEGIN	
				DECLARE @rowCount 	BIGINT
				SET @rowCount = (SELECT count FROM DeleteRequests where Type = @ptype)

					UPDATE DeleteRequests set Count = @rowCount - 1 where Type = @ptype					
				END
				
		Commit Tran			


GO
/****** Object:  StoredProcedure [dbo].[proc_Delete_Items]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Delete_Items]

		@pid				int,
		@ptype				nvarchar(50)

AS
	BEGIN TRAN
	
			DELETE FROM Items where Item_id = @pid

			IF @@Error = 0
				BEGIN	
				DECLARE @rowCount 	BIGINT
				SET @rowCount = (SELECT count FROM DeleteRequests where Type = @ptype)

					UPDATE DeleteRequests set Count = @rowCount - 1 where Type = @ptype					
				END
				
		Commit Tran			


GO
/****** Object:  StoredProcedure [dbo].[proc_Delete_Manufacturers]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Delete_Manufacturers]

		@pid				int,
		@ptype				nvarchar(50)

AS
	BEGIN TRAN
	
			DELETE FROM Manufacturers where Manufacturer_id = @pid

			IF @@Error = 0
				BEGIN	
				DECLARE @rowCount 	BIGINT
				SET @rowCount = (SELECT count FROM DeleteRequests where Type = @ptype)

					UPDATE DeleteRequests set Count = @rowCount - 1 where Type = @ptype					
				END
				
		Commit Tran			


GO
/****** Object:  StoredProcedure [dbo].[proc_Delete_Premises]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Delete_Premises]

		@pid				int,
		@ptype				nvarchar(50)

AS
	BEGIN TRAN
	
			DELETE FROM Premises where id = @pid

			IF @@Error = 0
				BEGIN	
				DECLARE @rowCount 	BIGINT
				SET @rowCount = (SELECT count FROM DeleteRequests where Type = @ptype)

					UPDATE DeleteRequests set Count = @rowCount - 1 where Type = @ptype					
				END
				
		Commit Tran			


GO
/****** Object:  StoredProcedure [dbo].[proc_Delete_Purchasing]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Delete_Purchasing]

		@pid				int,
		@ptype				nvarchar(50)

AS
	BEGIN TRAN
	
		DELETE FROM Purchasing where id = @pid
			IF @@Error = 0
				BEGIN	
				DECLARE @rowCount 	BIGINT
				SET @rowCount = (SELECT count FROM DeleteRequests where Type = @ptype)

					UPDATE DeleteRequests set Count = @rowCount - 1 where Type = @ptype					
				END
				
		Commit Tran			


GO
/****** Object:  StoredProcedure [dbo].[proc_Delete_Sales]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Delete_Sales]

		@pid				int,
		@ptype				nvarchar(50)

AS
	BEGIN TRAN
	
		DELETE FROM SalesOrder where id = @pid
			IF @@Error = 0
				BEGIN	
				DECLARE @rowCount 	BIGINT
				SET @rowCount = (SELECT count FROM DeleteRequests where Type = @ptype)

					UPDATE DeleteRequests set Count = @rowCount - 1 where Type = @ptype					
				END
				
		Commit Tran			


GO
/****** Object:  StoredProcedure [dbo].[proc_Delete_Units]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Delete_Units]

		@pid				int,
		@ptype				nvarchar(50)

AS
	BEGIN TRAN
	
			DELETE FROM Units where Unit_id = @pid

			IF @@Error = 0
				BEGIN	
				DECLARE @rowCount 	BIGINT
				SET @rowCount = (SELECT count FROM DeleteRequests where Type = @ptype)

					UPDATE DeleteRequests set Count = @rowCount - 1 where Type = @ptype					
				END
				
		Commit Tran			


GO
/****** Object:  StoredProcedure [dbo].[proc_Delete_Users]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Delete_Users]

		@pid				int,
		@ptype				nvarchar(50)

AS
	BEGIN TRAN
	
			DELETE FROM Users where id = @pid

			IF @@Error = 0
				BEGIN	
				DECLARE @rowCount 	BIGINT
				SET @rowCount = (SELECT count FROM DeleteRequests where Type = @ptype)

					UPDATE DeleteRequests set Count = @rowCount - 1 where Type = @ptype					
				END
				
		Commit Tran			


GO
/****** Object:  StoredProcedure [dbo].[proc_DeleteChat]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Salman Ahmed>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_DeleteChat] --2,9,0
	-- Add the parameters for the stored procedure here

		@pSender_id						int,
		@pReceiver_id					int,
		@pEnable						int,
		@pFlag							bit = 0 OUTPUT,
		@pDesc							varchar(50) = NULL OUTPUT

AS
	BEGIN TRAN
		UPDATE Messages 
		SET Enable = @pEnable
		WHERE (Sender_id = @pSender_id AND Receiver_id = @pReceiver_id)
		OR (Sender_id = @pReceiver_id AND Receiver_id = @pSender_id)
		
			IF @@Error = 0
			BEGIN	
				SET @pFlag = 1
				SET @pDesc =  'Chat Deleted Successfully'
				COMMIT TRAN
			END
			ELSE
			BEGIN
				SET @pFlag = 0
				SET @pDesc =  @@error
				ROLLBACK  TRAN
			END


GO
/****** Object:  StoredProcedure [dbo].[proc_DeleteItemFromPurchase]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Salman Ahmed>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_DeleteItemFromPurchase]
	-- Add the parameters for the stored procedure here

		@ppd_id					int


AS
	BEGIN
		DELETE FROM PurchasingDetails
		WHERE pd_id = @ppd_id

	END 

GO
/****** Object:  StoredProcedure [dbo].[proc_DeleteItemFromSales]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Salman Ahmed>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_DeleteItemFromSales]
	-- Add the parameters for the stored procedure here

		@psd_id					int


AS
	BEGIN
		DELETE FROM SalesOrder_Details
		WHERE id = @psd_id

	END 

GO
/****** Object:  StoredProcedure [dbo].[proc_Dropdown_Brands]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Dropdown_Brands] 

AS
BEGIN
	SELECT Brand_id as id, Brand_Name as name 
	FROM Brands AS c
	WHERE Enable = 1
	ORDER BY id desc
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Dropdown_Categories]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Dropdown_Categories] 

AS
BEGIN
	SELECT Category_id as id, Category_Name as name 
	FROM Categories AS c
	WHERE Enable = 1
	ORDER BY id desc
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Dropdown_Cities]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Dropdown_Cities] --1

	@pCountry_id			int

AS
BEGIN
	SELECT id as id, Name as name 
	FROM Cities AS c
	WHERE Country = @pCountry_id
	AND Enable = 1
	ORDER BY id desc
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Dropdown_Companies]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Dropdown_Companies] 

AS
BEGIN
	SELECT id as id, Name as name 
	FROM Companies AS c
	WHERE Enable = 1
	ORDER BY id desc
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Dropdown_Countries]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Dropdown_Countries] 

AS
BEGIN
	SELECT id as id, Name as name 
	FROM Countries AS c
	WHERE Enable = 1
	ORDER BY id desc
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Dropdown_Customer]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Dropdown_Customer] 

AS
BEGIN
	SELECT id as id, Name as name 
	FROM Contacts AS c
	WHERE Customer='1'
	AND Enable = 1
	ORDER BY id desc
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Dropdown_Items]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Dropdown_Items] 

AS
BEGIN
	SELECT Item_id as id, Item_Name as name 
	FROM Items AS c
	WHERE Enable = 1
	ORDER BY id desc
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Dropdown_Manufacturers]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Dropdown_Manufacturers] 

AS
BEGIN
	SELECT Manufacturer_id as id, Manufacturer_Name as name 
	FROM Manufacturers AS c
	WHERE Enable = 1
	ORDER BY id desc
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Dropdown_PaymentMode]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Dropdown_PaymentMode] 

AS
BEGIN
	SELECT id as id, Payment_Mode as name 
	FROM PaymentMode AS c
	WHERE Enable = 1
	ORDER BY id desc
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Dropdown_Roles]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Dropdown_Roles] 

AS
BEGIN
	SELECT Role_id as id, Role_Name as name 
	FROM Roles AS r
	WHERE Enable = 1
	ORDER BY id desc
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Dropdown_Units]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Dropdown_Units] 

AS
BEGIN
	SELECT Unit_id as id, Unit_Name as name 
	FROM Units AS c
	WHERE Enable = 1
	ORDER BY id desc
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Dropdown_Users]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Dropdown_Users] 

AS
BEGIN
	SELECT id as id, 
	(SELECT Name FROM Contacts WHERE id = attached_profile) as name 
	FROM Users AS c
	WHERE Enable = 1
	ORDER BY id desc
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Dropdown_Vendors]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Dropdown_Vendors] 

AS
BEGIN
	SELECT id as id, Name as name 
	FROM Contacts AS c
	WHERE Vendor='1'
	AND Enable = 1
	ORDER BY id desc
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Insert_ItemActivity]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Salman Ahmed>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Insert_ItemActivity]
	-- Add the parameters for the stored procedure here

	@dt AS ItemActivity_Datatable		READONLY


	
	AS
	BEGIN
	SET NOCOUNT ON;
		BEGIN TRAN

		
		INSERT INTO ItemActivity(ActivityType_id, ActivityType, ActivityName, Description, Date,Time, User_id, Icon)
		Select ActivityType_id, ActivityType, ActivityName, Description, Date, Time, User_id, Icon FROM @dt
		
		UPDATE	ItemActivity
		SET
			ItemActivity.ActivityType_id = Table_B.ActivityType_id,
			ItemActivity.ActivityType = Table_B.ActivityType,
			ItemActivity.ActivityName = Table_B.ActivityName,
			ItemActivity.Description = Table_B.Description,
			ItemActivity.Date = Table_B.Date,
			ItemActivity.Time = Table_B.Time,
			ItemActivity.User_id = Table_B.User_id,
			ItemActivity.Icon = Table_B.Icon

		FROM
			ItemActivity AS Table_A
			INNER JOIN @dt AS Table_B
				ON Table_A.ActivityType_id = Table_B.ActivityType_id
		WHERE Table_B.ActivityType_id <> Table_A.ActivityType_id

	Commit Tran
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Insert_Packaged_Items]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Insert_Packaged_Items]

		@dt AS [PackageDetail_Datatable]		READONLY,
		@pSaleOrder_Id							int,
		@pFlag									bit = 0 OUTPUT,
		@pDesc									nvarchar(50) = NULL OUTPUT

AS
BEGIN
		SET NOCOUNT ON;
		BEGIN TRAN	
		INSERT INTO SO_Packages_Detail (SaleOrder_id, Package_No,Item_id,UnitPrice,Packed_Item_Qty,Package_Date)
		Select [SaleOrder_id],[Package_No],[Item_id], [UnitPrice],[Packed_Item_Qty],[Package_Date] FROM @dt 
			UPDATE SO_Packages_Detail
			SET	SO_Packages_Detail.SaleOrder_id	= Table_B.SaleOrder_id,
				SO_Packages_Detail.Package_No = Table_B.Package_No,
				SO_Packages_Detail.Item_id = Table_B.Item_id,
				SO_Packages_Detail.UnitPrice = Table_B.UnitPrice,
				SO_Packages_Detail.Packed_Item_Qty = Table_B.Packed_Item_Qty,
				SO_Packages_Detail.Package_Date	= Table_B.Package_Date
			FROM
				SO_Packages_Detail AS Table_A
				INNER JOIN @dt AS Table_B
				ON Table_A.[Item_id] = Table_B.[Item_id]
			WHERE Table_B.[SaleOrder_id] <> 0 and Table_B.[Item_id] <> Table_A.[Item_id] 

			IF @@Error = 0
				BEGIN	
					UPDATE SalesOrder set SO_Status='Confirm', SO_Package_Status='1' where id=@pSaleOrder_Id							
				END
				
				SET @pFlag = 1
				SET @pDesc = 'package Created Successfully'
		Commit Tran			
END 

GO
/****** Object:  StoredProcedure [dbo].[proc_Insert_SaleReturn]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Insert_SaleReturn] 

		@dt AS [SaleReturn_Datatable]				READONLY,
		@pSaleOrder_Id								int,
		@pSaleReturnNo								nvarchar(50),
		@pSaleReturn_Date							nvarchar(50),
		@pSaleReturn_Status							nvarchar(50),
		@pAddedBy									int = 0,
		@pTime_Of_Day								nvarchar(50),
		@pDate_Of_Day								nvarchar(50),
		@pMonth_Of_Day								nvarchar(50),
		@pYear_Of_Day								nvarchar(50),
		@pFlag										bit = 0 OUTPUT,
		@pDesc										nvarchar(50) = NULL OUTPUT,
		@pSaleReturn_idOut							nvarchar(50) = NULL OUTPUT

AS
BEGIN
		SET NOCOUNT ON;
		BEGIN TRAN	

		INSERT INTO SaleReturn (SaleOrder_id,SaleReturn_Date,SaleReturnNo,SaleReturn_Status,AddedBy,Time_Of_Day,Date_Of_Day,Month_Of_Day,Year_Of_Day)
		VALUES (@pSaleOrder_Id,@pSaleReturn_Date,@pSaleReturnNo,@pSaleReturn_Status,@pAddedBy,@pTime_Of_Day,@pDate_Of_Day,@pMonth_Of_Day,@pYear_Of_Day)

		IF @@Error = 0
				BEGIN	
					SET @pSaleReturn_idOut = IDENT_CURRENT('SaleReturn')
	
		INSERT INTO SaleReturnDetail ([SaleReturn_id],[Package_id],[Item_id],[Return_Qty],[ReturnQty_Cost])
		Select @pSaleReturn_idOut,[Package_id],[Item_id],[Return_Qty],[ReturnQty_Cost] FROM @dt 
			UPDATE SaleReturnDetail
			SET	SaleReturnDetail.SaleReturn_id	= @pSaleReturn_idOut,
				SaleReturnDetail.Package_id = Table_B.[Package_id],
				SaleReturnDetail.Item_id = Table_B.[Item_id],
				SaleReturnDetail.Return_Qty = Table_B.[Return_Qty],
				SaleReturnDetail.ReturnQty_Cost = Table_B.[ReturnQty_Cost]

			FROM
				SaleReturnDetail AS Table_A
				INNER JOIN @dt AS Table_B
				ON Table_A.[Package_id] = Table_B.[Package_id]
			WHERE Table_B.[SaleReturn_id] <> 0 and Table_B.[Package_id] <> Table_A.[Package_id] 
			IF @@Error = 0
						BEGIN	
							SET @pFlag = 1
							SET @pDesc =  'Sale Return Added Successfully'
							COMMIT TRAN
						END
						ELSE
						BEGIN
							SET @pFlag = 0
							SET @pDesc =  @@error
							ROLLBACK  TRAN
						END
		END		
END 

GO
/****** Object:  StoredProcedure [dbo].[proc_Insert_SaleReturn_Receiving]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Insert_SaleReturn_Receiving] 

		@dt AS [ReturnReceiving_Datatable]				READONLY


AS
BEGIN
		SET NOCOUNT ON;
		BEGIN TRAN	
	
		--INSERT INTO SaleReturnDetail ([SaleReturn_id],[Package_id],[Item_id],[Return_Qty],[ReturnQty_Cost])
		Select SaleReturn_id,[Package_id],[Item_id],[Return_Qty],[Received_Qty] FROM @dt 
			UPDATE SaleReturnDetail
			SET	SaleReturnDetail.Package_id = Table_B.[Package_id],
				SaleReturnDetail.Item_id = Table_B.[Item_id],
				SaleReturnDetail.Return_Qty = Table_B.[Return_Qty],
				SaleReturnDetail.Received_Qty = Table_B.[Received_Qty]

			FROM
				SaleReturnDetail AS Table_A
				INNER JOIN @dt AS Table_B
				ON Table_A.[Item_id] = Table_B.[Item_id]
			WHERE Table_B.[Item_id] <> 0
			AND Table_B.[Item_id] = Table_A.[Item_id]
			Commit Tran
END 

GO
/****** Object:  StoredProcedure [dbo].[proc_Insert_Shipment_Packages]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Insert_Shipment_Packages]

		@dt AS [ShipmentPackages_Datatable]		READONLY,
		@pSaleOrder_Id							int,
		@pFlag									bit = 0 OUTPUT,
		@pDesc									nvarchar(50) = NULL OUTPUT
		

AS
BEGIN
		SET NOCOUNT ON;
		BEGIN TRAN	
		INSERT INTO ShipmentPackages ([SaleOrder_id],[Shipment_No],[Package_id])
		Select [SaleOrder_id],[Shipment_No],[Package_id] FROM @dt 
			UPDATE ShipmentPackages
			SET	ShipmentPackages.SaleOrder_id	= Table_B.[SaleOrder_id],
				ShipmentPackages.Shipment_No = Table_B.[Shipment_No],
				ShipmentPackages.Package_id = Table_B.[Package_id]

			FROM
				ShipmentPackages AS Table_A
				INNER JOIN @dt AS Table_B
				ON Table_A.[Package_id] = Table_B.[Package_id]
			WHERE Table_B.[SaleOrder_id] <> 0 and Table_B.[Package_id] <> Table_A.[Package_id] 

			IF @@Error = 0
				BEGIN	
					UPDATE SalesOrder set SO_Status='Confirm', SO_Shipment_Status='1' where id=@pSaleOrder_Id					
				END
				
				SET @pFlag = 1
				SET @pDesc = 'Shipment Created Successfully'
		Commit Tran			
END 

GO
/****** Object:  StoredProcedure [dbo].[proc_Insert_Stock]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Insert_Stock]

		@pItem_id						int,
		@pPhysical_Quantity				nvarchar(100),
		@pAccounting_Quantity			nvarchar(100),
		@pOpeningStock					nvarchar(100),
		@pReorderLevel					nvarchar(100),
		@pTime_Of_Day					nvarchar(100),
		@pDate_Of_Day					nvarchar(100),
		@pMonth_Of_Day					nvarchar(100),
		@pYear_Of_Day					nvarchar(100)

AS
BEGIN
		SET NOCOUNT ON;
		BEGIN TRAN	
		INSERT INTO Stock (Item_id,Physical_Quantity,Accounting_Quantity,OpeningStock,ReorderLevel,Time_Of_Day,Date_Of_Day,Month_Of_Day,Year_Of_Day)
		VALUES (@pItem_id,@pPhysical_Quantity,@pAccounting_Quantity,@pOpeningStock,@pReorderLevel,@pTime_Of_Day,@pDate_Of_Day,@pMonth_Of_Day,@pYear_Of_Day)
		Commit Tran
END 

GO
/****** Object:  StoredProcedure [dbo].[proc_InsertEmail]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Salman Ahmed>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_InsertEmail]
	-- Add the parameters for the stored procedure here

	@pid								int = 0,
	@pEmailTo							nvarchar(50),
	@pEmailFrom							nvarchar(50),
	@pSubject							nvarchar(MAX),
	@pBody								nvarchar(MAX),
	@pStatus							nvarchar(50),
	@pUser_id							nvarchar(50),
	@pTimeOfDay							nvarchar(50),
	@pDateOfDay							nvarchar(50),
	@pMonthOfDay						nvarchar(50),
	@pYearOfDay							nvarchar(50),
	@dt									AS MailAttachments_Datatable		READONLY,
	@pMailid_Out						varchar(50) = NULL OUTPUT,
	@pFlag								bit = 0 OUTPUT,
	@pDesc								varchar(50) = NULL OUTPUT



AS
	BEGIN TRAN 

			INSERT INTO Mailbox(EmailTo,EmailFrom,Subject,Body,Status,User_id,TimeOfDay,DateOfDay,MonthOfDay,YearOfDay)
	
				VALUES(@pEmailTo,@pEmailFrom,@pSubject,@pBody,@pStatus,@pUser_id,@pTimeOfDay,@pDateOfDay,@pMonthOfDay,@pYearOfDay)	

					SET @pMailid_Out = IDENT_CURRENT('Mailbox') 

						INSERT INTO MailBoxAttachments (Mailbox_id,FileName)
							SELECT @pMailid_Out, [FileName] FROM @dt

							UPDATE	MailBoxAttachments
							SET
								MailBoxAttachments.Mailbox_id = @pMailid_Out,
								MailBoxAttachments.FileName = Table_B.FileName
							FROM
								MailBoxAttachments AS Table_A
								INNER JOIN @dt AS Table_B
									ON Table_A.Mailbox_id = Table_B.Mailbox_id
							WHERE Table_B.Mailbox_id <> 0

							IF @@Error = 0
						BEGIN	
							SET @pFlag = 1
							SET @pDesc =  'Email Sent Successfully'
							COMMIT TRAN
						END
						ELSE
						BEGIN
							SET @pFlag = 0
							SET @pDesc =  @@error
							ROLLBACK  TRAN
						END

GO
/****** Object:  StoredProcedure [dbo].[proc_InsertUpdate_Bill]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Salman Ahmed>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_InsertUpdate_Bill]
	-- Add the parameters for the stored procedure here
								
	@pPurchase_id							int,
	@pBill_No								nvarchar(50),
	@pBill_Status							nvarchar(50),
	@pBill_Amount							nvarchar(50),
	@pEnable								nvarchar(50),
	@pBillDateTime							nvarchar(50),
	@pBillDueDate							nvarchar(50),
	@pAddedBy								int,
	@pTime_Of_Day							nvarchar(50),
	@pDate_Of_Day							nvarchar(50),
	@pMonth_Of_Day							nvarchar(50),
	@pYear_Of_Day							nvarchar(50),
	@pFlag									bit = 0 OUTPUT,
	@pDesc									varchar(50) = NULL OUTPUT,
	@pBill_id_Output						int = NULL OUTPUT

AS
	BEGIN TRAN

			INSERT INTO Bills(Purchase_id,Bill_No,Bill_Status,Bill_Amount,Enable,BillDateTime,BillDueDate, AddedBy,Time_Of_Day,Date_Of_Day,Month_Of_Day,Year_Of_Day)
	
			VALUES(@pPurchase_id,@pBill_No,@pBill_Status,@pBill_Amount,@pEnable,@pBillDateTime,@pBillDueDate,@pAddedBy,@pTime_Of_Day,@pDate_Of_Day,@pMonth_Of_Day,@pYear_Of_Day)	
			IF @@Error = 0
						BEGIN
							SET @pFlag = 1
							SET @pDesc =  'Bill Added Successfully'
							SET @pBill_id_Output = IDENT_CURRENT('Bills')
							COMMIT TRAN
						END
						ELSE
						BEGIN
							SET @pFlag = 0
							SET @pDesc =  @@error
							ROLLBACK TRAN
						
END
GO
/****** Object:  StoredProcedure [dbo].[proc_InsertUpdate_Brands]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Salman Ahmed>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_InsertUpdate_Brands]
	-- Add the parameters for the stored procedure here

	@pBrand_id							int = 0,
	@pBrand_Name						nvarchar(50),
	@pEnable							int,
	@pAddedBy							int = 0,
	@pUpdatedBy							int = 0,
	@Time_Of_Day						nvarchar(50),
	@Date_Of_Day						nvarchar(50),
	@Month_Of_Day						nvarchar(50),
	@Year_Of_Day						nvarchar(50),
	@pFlag								bit = 0 OUTPUT,
	@pDesc								varchar(50) = NULL OUTPUT,
	@pBrandid_Out					varchar(50) = NULL OUTPUT
AS
	BEGIN TRAN
			IF @pBrand_id= 0
			BEGIN 
				IF NOT Exists (SELECT Brand_Name FROM Brands U WHERE UPPER (Brand_Name) = UPPER (@pBrand_Name))
				BEGIN

					INSERT INTO Brands (Brand_Name, Enable, AddedBy, Time_Of_Day, Date_Of_Day, Month_Of_Day, Year_Of_Day)
	
					VALUES(@pBrand_Name,@pEnable,@pAddedBy,@Time_Of_Day,@Date_Of_Day,@Month_Of_Day,@Year_Of_Day)	

						IF @@Error = 0
						BEGIN	
							SET @pFlag = 1
							SET @pDesc =  'Brand Added Successfully'
							SET @pBrandid_Out = IDENT_CURRENT('Brands')
							COMMIT TRAN
						END
						ELSE
						BEGIN
							SET @pFlag = 0
							SET @pDesc =  @@error
							ROLLBACK  TRAN
						END
				END
				ELSE
				BEGIN
					SET @pFlag = 0
					SET @pDesc =  'Brand Already Exists'
					ROLLBACK  TRAN
				END					
			END
			ELSE
			BEGIN
			IF NOT Exists (SELECT Brand_Name FROM Brands U WHERE UPPER (Brand_Name) = UPPER (@pBrand_Name) and (Brand_id)  <> @pBrand_id)
					
			BEGIN	
					
					UPDATE Brands
					SET Brand_Name=@pBrand_Name,
					UpdatedBy = @pUpdatedBy
					WHERE Brand_id = @pBrand_id


					IF @@Error = 0
					BEGIN	
						SET @pFlag = 1
						SET @pDesc =  'Brand Updated Successfully'
						COMMIT TRAN
					END

					ELSE
					BEGIN
						SET @pFlag = 0
						SET @pDesc =  @@error
						ROLLBACK  TRAN
					END
				
				END
				ELSE
				BEGIN
					SET @pFlag = 0
					SET @pDesc =  'Brand Already Exists'
					ROLLBACK  TRAN
				END	
				
			
		END 

GO
/****** Object:  StoredProcedure [dbo].[proc_InsertUpdate_Category]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_InsertUpdate_Category]
	-- Add the parameters for the stored procedure here

	@pCategory_id						int = 0,
	@pCategory_Name						nvarchar(50),
	@pEnable							int,
	@pAddedBy							int = 0,
	@pUpdatedBy							int = 0,
	@Time_Of_Day						nvarchar(50),
	@Date_Of_Day						nvarchar(50),
	@Month_Of_Day						nvarchar(50),
	@Year_Of_Day						nvarchar(50),
	@pFlag								bit = 0 OUTPUT,
	@pDesc								varchar(50) = NULL OUTPUT,
	@pCategoryid_Out					varchar(50) = NULL OUTPUT
AS
	BEGIN TRAN
			IF @pCategory_id= 0
			BEGIN 
				IF NOT Exists (SELECT Category_Name FROM Categories U WHERE UPPER (Category_Name) = UPPER (@pCategory_Name))
				BEGIN

					INSERT INTO Categories(Category_Name, Enable, AddedBy, Time_Of_Day, Date_Of_Day, Month_Of_Day, Year_Of_Day)
	
					VALUES(@pCategory_Name,@pEnable,@pAddedBy,@Time_Of_Day,@Date_Of_Day,@Month_Of_Day,@Year_Of_Day)	

						IF @@Error = 0
						BEGIN	
							SET @pFlag = 1
							SET @pDesc =  'Category Added Successfully'
							SET @pCategoryid_Out = IDENT_CURRENT('Categories')
							COMMIT TRAN
						END
						ELSE
						BEGIN
							SET @pFlag = 0
							SET @pDesc =  @@error
							ROLLBACK  TRAN
						END
				END
				ELSE
				BEGIN
					SET @pFlag = 0
					SET @pDesc =  'Category Already Exists'
					ROLLBACK  TRAN
				END					
			END
			ELSE
			BEGIN
			IF NOT Exists (SELECT Category_Name FROM Categories U WHERE UPPER (Category_Name) = UPPER (@pCategory_Name) and (Category_id)  <> @pCategory_id)
					
			BEGIN	
					
					UPDATE Categories
					SET Category_Name=@pCategory_Name,
					UpdatedBy = @pUpdatedBy
					WHERE Category_id = @pCategory_id


					IF @@Error = 0
					BEGIN	
						SET @pFlag = 1
						SET @pDesc =  'Category Updated Successfully'
						COMMIT TRAN
					END

					ELSE
					BEGIN
						SET @pFlag = 0
						SET @pDesc =  @@error
						ROLLBACK  TRAN
					END
				
				END
				ELSE
				BEGIN
					SET @pFlag = 0
					SET @pDesc =  'Category Already Exists'
					ROLLBACK  TRAN
				END	
				
			
		END 

GO
/****** Object:  StoredProcedure [dbo].[proc_InsertUpdate_Cities]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Salman Ahmed>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_InsertUpdate_Cities]
	-- Add the parameters for the stored procedure here

	@pid							int = 0,
	@pName							nvarchar(50),
	@pCountry						int,
	@pEnable						nvarchar(50),
	@pAddedBy						int = 0,
	@pUpdatedBy						int = 0,
	@pTimeOfDay						nvarchar(50),
	@pDateOfDay						nvarchar(50),
	@pMonthOfDay					nvarchar(50),
	@pYearOfDay						nvarchar(50),
	@pFlag							bit = 0 OUTPUT,
	@pDesc							varchar(50) = NULL OUTPUT,
	@pCityid_Out					varchar(50) = NULL OUTPUT




AS
	BEGIN TRAN
	IF @pid= 0
			BEGIN 
				IF NOT Exists (SELECT Name FROM Cities U WHERE UPPER (Name) = UPPER (@pName))
				BEGIN

					INSERT INTO Cities (Name, Country, Enable, AddedBy, TimeOfDay, DateOfDay, MonthOfDay, YearOfDay)
	
					VALUES(@pName,@pCountry,@pEnable,@pAddedBy,@pTimeOfDay,@pDateOfDay,@pMonthOfDay,@pYearOfDay)	

						IF @@Error = 0
						BEGIN	
							SET @pFlag = 1
							SET @pDesc =  'City Added Successfully'
							SET @pCityid_Out = IDENT_CURRENT('Cities')
							COMMIT TRAN
						END
						ELSE
						BEGIN
							SET @pFlag = 0
							SET @pDesc =  @@error
							ROLLBACK  TRAN
						END
				END
				ELSE
				BEGIN
					SET @pFlag = 0
					SET @pDesc =  'City Already Exists'
					ROLLBACK  TRAN
				END					
			END
			ELSE
			BEGIN
			IF NOT Exists (SELECT Name FROM Cities U WHERE UPPER (Name) = UPPER (@pName) and (id)  <> @pid)
			BEGIN	
					
					UPDATE Cities
					SET Name=@pName,
					Country = @pCountry,
					UpdatedBy = @pUpdatedBy
					WHERE id = @pid


					IF @@Error = 0
					BEGIN	
						SET @pFlag = 1
						SET @pDesc =  'City Updated Successfully'
						COMMIT TRAN
					END

					ELSE
					BEGIN
						SET @pFlag = 0
						SET @pDesc =  @@error
						ROLLBACK  TRAN
					END
				
				END
				ELSE
				BEGIN
					SET @pFlag = 0
					SET @pDesc =  'City Already Exists'
					ROLLBACK  TRAN
				END	
END
GO
/****** Object:  StoredProcedure [dbo].[proc_InsertUpdate_Company]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Salman Ahmed>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_InsertUpdate_Company]
	-- Add the parameters for the stored procedure here

		@pid								int = 0,
		@pName								nvarchar(MAX),
		@pLandline							nvarchar(MAX),
		@pMobile							nvarchar(MAX),
		@pEmail								nvarchar(MAX),
		@pWebsite							nvarchar(MAX),
		@pAddress							nvarchar(MAX),
		@pCity								int,
		@pCountry							int,
		@pBankAccountNumber					nvarchar(MAX),
		@pPaymentMethod						nvarchar(MAX),
		@pEnable							nvarchar(MAX),
		@pAddedBy							int = 0,
		@pUpdatedBy							int = 0,
		@pTimeOfDay							nvarchar(MAX),
		@pDateOfDay							nvarchar(MAX),
		@pMonthOfDay						nvarchar(MAX),
		@pYearOfDay							nvarchar(MAX),
		@pFlag								bit = 0 OUTPUT,
		@pDesc								varchar(MAX) = NULL OUTPUT,
		@pCompanyid_Out						varchar(50) = NULL OUTPUT

AS
	BEGIN TRAN
			IF @pid= 0
			BEGIN 
				IF NOT Exists (SELECT Name FROM Companies C WHERE UPPER (Name) = UPPER (@pName))
				BEGIN

					INSERT INTO Companies (Name,Landline,Mobile,Email,Website,Address,City,Country,BankAccountNumber,PaymentMethod,Enable,AddedBy,TimeOfDay,DateOfDay,MonthOfDay,YearOfDay)
	
					VALUES(@pName,@pLandline,@pMobile,@pEmail,@pWebsite,@pAddress,@pCity,@pCountry,@pBankAccountNumber,@pPaymentMethod,@pEnable,@pAddedBy,@pTimeOfDay,@pDateOfDay,@pMonthOfDay,@pYearOfDay)	

						IF @@Error = 0
						BEGIN	
							SET @pFlag = 1
							SET @pDesc =  'Company Added Successfully'
							SET @pCompanyid_Out = IDENT_CURRENT('Companies')
							COMMIT TRAN
						END
						ELSE
						BEGIN
							SET @pFlag = 0
							SET @pDesc =  @@error
							ROLLBACK  TRAN
						END
				END
				ELSE
				BEGIN
					SET @pFlag = 0
					SET @pDesc =  'Company Already Exists'
					ROLLBACK  TRAN
				END					
			END
			ELSE
			BEGIN
			IF NOT Exists (SELECT Name FROM Companies U WHERE UPPER (Name) = UPPER (@pName) and (id)  <> @pid)
					
			BEGIN	
					
					UPDATE Companies
					SET Name = @pName,
						Landline = @pLandline,
						Mobile = @pMobile,
						Email = @pEmail,
						Website = @pWebsite,
						Address = @pAddress,
						City = @pCity,
						Country = @pCountry,
						BankAccountNumber = @pBankAccountNumber,
						PaymentMethod = @pPaymentMethod,
						Enable = @pEnable,
						UpdatedBy = @pUpdatedBy
					WHERE id = @pid


					IF @@Error = 0
					BEGIN	
						SET @pFlag = 1
						SET @pDesc =  'Company Updated Successfully'
						COMMIT TRAN
					END

					ELSE
					BEGIN
						SET @pFlag = 0
						SET @pDesc =  @@error
						ROLLBACK  TRAN
					END
				
				END
				ELSE
				BEGIN
					SET @pFlag = 0
					SET @pDesc =  'Company Already Exists'
					ROLLBACK  TRAN
				END	
				
			
		END 

GO
/****** Object:  StoredProcedure [dbo].[proc_InsertUpdate_Contacts]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Salman Ahmed>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_InsertUpdate_Contacts]
	-- Add the parameters for the stored procedure here

	@pid								int = 0,
	@pImage								nvarchar(MAX) = NULL,
	@pSalutation						int = 0,
	@pName								nvarchar(50),
	@pCompany							int = 0,
	@pDesignation						nvarchar(50),
	@pLandline							nvarchar(50),
	@pMobile							nvarchar(50),
	@pEmail								nvarchar(50),
	@pWebsite							nvarchar(50),
	@pAddress							nvarchar(50),
	@pAddressLandline					nvarchar(50),
	@pCity								nvarchar(50),
	@pCountry							nvarchar(50),			
	@pBankAccountNumber					nvarchar(50),
	@pPaymentMethod						nvarchar(50),
	@pVendor							nvarchar(50),
	@pCustomer							nvarchar(50),
	@pEmployee							nvarchar(50),
	@pEnable							nvarchar(50),
	@pAddedBy							int = 0,
	@pUpdatedBy							int = 0,
	@pTimeOfDay							nvarchar(50),
	@pDateOfDay							nvarchar(50),
	@pMonthOfDay						nvarchar(50),
	@pYearOfDay							nvarchar(50),
	@pFlag								bit = 0 OUTPUT,
	@pDesc								varchar(50) = NULL OUTPUT,
	@pContactid_Out					varchar(50) = NULL OUTPUT

				
AS
	BEGIN TRAN
			IF @pid= 0
			BEGIN 
				--IF NOT Exists (SELECT Name FROM Contacts C WHERE UPPER (Name) = UPPER (@pName))
				--BEGIN

					INSERT INTO Contacts(Image,Salutation,Name,Company,Designation,Landline,Mobile,Email,Website,Address,AddressLandline,City,Country,BankAccountNumber,PaymentMethod,Vendor,Customer,Employee,Enable,AddedBy,TimeOfDay,DateOfDay,MonthOfDay,YearOfDay)
	
					VALUES(@pImage,@pSalutation,@pName,@pCompany,@pDesignation,@pLandline,@pMobile,@pEmail,@pWebsite,@pAddress,@pAddressLandline,@pCity,@pCountry,@pBankAccountNumber,@pPaymentMethod,@pVendor,@pCustomer,@pEmployee,@pEnable,@pAddedBy,@pTimeOfDay,@pDateOfDay,@pMonthOfDay,@pYearOfDay)	

						IF @@Error = 0
						BEGIN	
							SET @pFlag = 1
							SET @pContactid_Out = IDENT_CURRENT('Contacts')
							IF @pVendor = '1'
							BEGIN
								SET @pDesc =  'Vendor Added Successfully'
							END
							ELSE IF @pCustomer = '1'
							BEGIN
								SET @pDesc =  'Customer Added Successfully'
							END
							ELSE IF @pEmployee = '1'
							BEGIN
								SET @pDesc =  'Employee Added Successfully'
							END
							COMMIT TRAN
						END
						ELSE
						BEGIN
							SET @pFlag = 0
							SET @pDesc =  @@error
							ROLLBACK  TRAN
						END
				--END
				--ELSE
				--BEGIN
				--	SET @pFlag = 0
				--		IF @pVendor = '1'
				--		BEGIN
				--			SET @pDesc =  'Vendor Already Exists'
				--		END
				--		ELSE IF @pCustomer = '1'
				--		BEGIN
				--			SET @pDesc =  'Customer Already Exists'
				--		END
				--		ELSE IF @pEmployee = '1'
				--		BEGIN
				--			SET @pDesc =  'Employee Already Exists'
				--		END
				--	ROLLBACK  TRAN
				--END					
			END
			ELSE
			BEGIN
			IF NOT Exists (SELECT Name FROM Contacts U WHERE UPPER (Name) = UPPER (@pName) and (id)  <> @pid)
					
			BEGIN	
					
					UPDATE Contacts
					SET Image = @pImage, 
					Salutation  = @pSalutation,
					Name = @pName,
					Company = @pCompany,			
					Designation = @pDesignation,
					Landline = @pLandline,			
					Mobile = @pMobile,
					Email = @pEmail,				
					Website = @pWebsite,
					Address = @pAddress,			
					AddressLandline = @pAddressLandline,	
					City = @pCity,				
					Country = @pCountry,
					BankAccountNumber = @pBankAccountNumber,	
					PaymentMethod = @pPaymentMethod,
					Vendor = @pVendor,			
					Customer = @pCustomer,
					Employee = @pEmployee,			
					Enable = @pEnable,
					UpdatedBy = @pUpdatedBy
					WHERE id = @pid




					IF @@Error = 0
					BEGIN	
						SET @pFlag = 1
						IF @pVendor = '1'
						BEGIN
							SET @pDesc =  'Vendor Updated Successfully'
						END
						ELSE IF @pCustomer = '1'
						BEGIN
							SET @pDesc =  'Customer Updated Successfully'
						END
						ELSE IF @pEmployee = '1'
						BEGIN
							SET @pDesc =  'Employee Updated Successfully'
						END
						COMMIT TRAN
					END

					ELSE
					BEGIN
						SET @pFlag = 0
						SET @pDesc =  @@error
						ROLLBACK  TRAN
					END
				
				END
				ELSE
				BEGIN
					SET @pFlag = 0
						IF @pVendor = '1'
						BEGIN
							SET @pDesc =  'Vendor Already Exists'
						END
						ELSE IF @pCustomer = '1'
						BEGIN
							SET @pDesc =  'Customer Already Exists'
						END
						ELSE IF @pEmployee = '1'
						BEGIN
							SET @pDesc =  'Employee Already Exists'
						END
					ROLLBACK  TRAN
				END	
				
			
		END 

GO
/****** Object:  StoredProcedure [dbo].[proc_InsertUpdate_Countries]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Salman Ahmed>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_InsertUpdate_Countries]
	-- Add the parameters for the stored procedure here

	@pid								int = 0,
	@pName								nvarchar(50),
	@pEnable							nvarchar(50),
	@pAddedBy							int = 0,
	@pUpdatedBy							int = 0,
	@pTimeOfDay							nvarchar(50),
	@pDateOfDay							nvarchar(50),
	@pMonthOfDay						nvarchar(50),
	@pYearOfDay							nvarchar(50),
	@pFlag								bit = 0 OUTPUT,
	@pDesc								varchar(50) = NULL OUTPUT,
	@pCountryid_Out						varchar(50) = NULL OUTPUT




AS
	BEGIN TRAN
			IF @pid= 0
			BEGIN 
				IF NOT Exists (SELECT Name FROM Countries U WHERE UPPER (Name) = UPPER (@pName))
				BEGIN

					INSERT INTO Countries (Name, Enable, AddedBy, TimeOfDay, DateOfDay, MonthOfDay, YearOfDay)
	
					VALUES(@pName,@pEnable,@pAddedBy,@pTimeOfDay,@pDateOfDay,@pMonthOfDay,@pYearOfDay)	

						IF @@Error = 0
						BEGIN	
							SET @pFlag = 1
							SET @pDesc =  'Country Added Successfully'
							SET @pCountryid_Out = IDENT_CURRENT('Countries')
							COMMIT TRAN
						END
						ELSE
						BEGIN
							SET @pFlag = 0
							SET @pDesc =  @@error
							ROLLBACK  TRAN
						END
				END
				ELSE
				BEGIN
					SET @pFlag = 0
					SET @pDesc =  'Country Already Exists'
					ROLLBACK  TRAN
				END					
			END
			ELSE
			BEGIN
			IF NOT Exists (SELECT Name FROM Countries U WHERE UPPER (Name) = UPPER (@pName) and (id)  <> @pid)
					
			BEGIN	
					
					UPDATE Countries
					SET Name=@pName,
					UpdatedBy = @pUpdatedBy
					WHERE id = @pid


					IF @@Error = 0
					BEGIN	
						SET @pFlag = 1
						SET @pDesc =  'Country Updated Successfully'
						COMMIT TRAN
					END

					ELSE
					BEGIN
						SET @pFlag = 0
						SET @pDesc =  @@error
						ROLLBACK  TRAN
					END
				
				END
				ELSE
				BEGIN
					SET @pFlag = 0
					SET @pDesc =  'Country Already Exists'
					ROLLBACK  TRAN
				END	
				
			
		END 

GO
/****** Object:  StoredProcedure [dbo].[proc_InsertUpdate_Items]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_InsertUpdate_Items]
	-- Add the parameters for the stored procedure here

	@pItem_id								int = 0,								
	@pItem_type								nvarchar(50),
	@pItem_file								nvarchar(50),
	@pItem_Name								nvarchar(50),
	@pItem_Sku								nvarchar(50),
	@pItem_Category							nvarchar(50),
	@pItem_Unit								nvarchar(50),
	@pItem_Manufacturer						nvarchar(50),
	@pItem_Upc								nvarchar(50),
	@pItem_Brand							nvarchar(50),
	@pItem_Mpn								nvarchar(50),
	@pItem_Ean								nvarchar(50),
	@pItem_Isbn								nvarchar(50),
	@pItem_Sell_Price						nvarchar(50),
	@pItem_Tax								nvarchar(50),
	@pItem_Purchase_Price					nvarchar(50),
	@pItem_Preferred_Vendor					nvarchar(50),
	@pEnable								nvarchar(50),
	@pAddedBy								int = 0,
	@pUpdatedBy								int = 0,
	@pTime_Of_Day							nvarchar(50),
	@pDate_Of_Day							nvarchar(50),
	@pMonth_Of_Day							nvarchar(50),
	@pYear_Of_Day							nvarchar(50),
	@pFlag									bit = 0 OUTPUT,
	@pDesc									varchar(50) = NULL OUTPUT,
	@pItem_id_Out							varchar(50) = NULL OUTPUT

AS
	BEGIN TRAN
			IF @pItem_id= 0
			BEGIN 
				IF NOT Exists (SELECT Item_Name FROM Items U WHERE UPPER (Item_Name) = UPPER (@pItem_Name))
				BEGIN

					INSERT INTO Items(Item_type,Item_file,Item_Name,Item_Sku,Item_Category,Item_Unit,Item_Manufacturer,Item_Upc,Item_Brand,Item_Mpn,Item_Ean,Item_Isbn,Item_Sell_Price,Item_Tax,Item_Purchase_Price,Item_Preferred_Vendor,Enable,AddedBy,Time_Of_Day,Date_Of_Day,Month_Of_Day,Year_Of_Day)
	
					VALUES(@pItem_type,@pItem_file,@pItem_Name,@pItem_Sku,@pItem_Category,@pItem_Unit,@pItem_Manufacturer,@pItem_Upc,@pItem_Brand,@pItem_Mpn,@pItem_Ean,@pItem_Isbn,@pItem_Sell_Price,@pItem_Tax,@pItem_Purchase_Price,@pItem_Preferred_Vendor,@pEnable,@pAddedBy,@pTime_Of_Day,@pDate_Of_Day,@pMonth_Of_Day,@pYear_Of_Day)	
					
						IF @@Error = 0
						BEGIN	
							SET @pFlag = 1
							SET @pDesc =  'Item Added Successfully'
							SET @pItem_id_Out = IDENT_CURRENT('Items')
							COMMIT TRAN
						END
						ELSE
						BEGIN
							SET @pFlag = 0
							SET @pDesc =  @@error
							ROLLBACK  TRAN
						END
				END
				ELSE
				BEGIN
					SET @pFlag = 0
					SET @pDesc =  'Item Already Exists'
					ROLLBACK  TRAN
				END					
			END
			ELSE
			BEGIN
			IF NOT Exists (SELECT Item_Name FROM Items U WHERE UPPER (Item_Name) = UPPER (@pItem_Name) and (Item_id)  <> @pItem_id)
					
			BEGIN	
					
					UPDATE Items
					SET Item_Name = @pItem_Name,
						Item_type = @pItem_type,			
						Item_file = @pItem_file,				
						Item_Sku = @pItem_Sku,				
						Item_Category = @pItem_Category,			
						Item_Unit = @pItem_Unit,				
						Item_Manufacturer = @pItem_Manufacturer,		
						Item_Upc = @pItem_Upc,				
						Item_Brand = @pItem_Brand,				
						Item_Mpn = @pItem_Mpn,				
						Item_Ean = @pItem_Ean,				
						Item_Isbn = @pItem_Isbn,				
						Item_Sell_Price = @pItem_Sell_Price,		
						Item_Tax = @pItem_Tax,				
						Item_Purchase_Price = @pItem_Purchase_Price,	
						Item_Preferred_Vendor = @pItem_Preferred_Vendor,
						UpdatedBy = @pUpdatedBy
		
					WHERE Item_id = @pItem_id


					IF @@Error = 0
					BEGIN	
						SET @pFlag = 1
						SET @pDesc =  'Item Updated Successfully'
						SET @pItem_id_Out = @pItem_id
						COMMIT TRAN
					END

					ELSE
					BEGIN
						SET @pFlag = 0
						SET @pDesc =  @@error
						ROLLBACK  TRAN
					END
				
				END
				ELSE
				BEGIN
					SET @pFlag = 0
					SET @pDesc =  'Item Already Exists'
					ROLLBACK  TRAN
				END	
				
			
		END 

GO
/****** Object:  StoredProcedure [dbo].[proc_InsertUpdate_Manufacturer]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_InsertUpdate_Manufacturer]
	-- Add the parameters for the stored procedure here

	@pManufacturer_id						int = 0,
	@pManufacturer_Name						nvarchar(50),
	@pEnable								int,
	@pAddedBy								int = 0,
	@pUpdatedBy								int = 0,
	@Time_Of_Day							nvarchar(50),
	@Date_Of_Day							nvarchar(50),
	@Month_Of_Day							nvarchar(50),
	@Year_Of_Day							nvarchar(50),
	@pFlag									bit = 0 OUTPUT,
	@pDesc									varchar(50) = NULL OUTPUT,
	@pManufacturerid_Out						varchar(50) = NULL OUTPUT

AS
	BEGIN TRAN
			IF @pManufacturer_id= 0
			BEGIN 
				IF NOT Exists (SELECT Manufacturer_Name FROM Manufacturers U WHERE UPPER (Manufacturer_Name) = UPPER (@pManufacturer_Name))
				BEGIN

					INSERT INTO Manufacturers(Manufacturer_Name, Enable, AddedBy, Time_Of_Day, Date_Of_Day, Month_Of_Day, Year_Of_Day)
	
					VALUES(@pManufacturer_Name,@pEnable,@pAddedBy,@Time_Of_Day,@Date_Of_Day,@Month_Of_Day,@Year_Of_Day)	

						IF @@Error = 0
						BEGIN	
							SET @pFlag = 1
							SET @pDesc =  'Manufacturer Added Successfully'
							SET @pManufacturerid_Out = IDENT_CURRENT('Manufacturers')
							COMMIT TRAN
						END
						ELSE
						BEGIN
							SET @pFlag = 0
							SET @pDesc =  @@error
							ROLLBACK  TRAN
						END
				END
				ELSE
				BEGIN
					SET @pFlag = 0
					SET @pDesc =  'Manufacturer Already Exists'
					ROLLBACK  TRAN
				END					
			END
			ELSE
			BEGIN
			IF NOT Exists (SELECT Manufacturer_Name FROM Manufacturers U WHERE UPPER (Manufacturer_Name) = UPPER (@pManufacturer_Name) and (Manufacturer_id)  <> @pManufacturer_id)
					
			BEGIN	
					
					UPDATE Manufacturers
					SET Manufacturer_Name=@pManufacturer_Name,
					UpdatedBy = @pUpdatedBy
					WHERE Manufacturer_id = @pManufacturer_id


					IF @@Error = 0
					BEGIN	
						SET @pFlag = 1
						SET @pDesc =  'Manufacturer Updated Successfully'
						COMMIT TRAN
					END

					ELSE
					BEGIN
						SET @pFlag = 0
						SET @pDesc =  @@error
						ROLLBACK  TRAN
					END
				
				END
				ELSE
				BEGIN
					SET @pFlag = 0
					SET @pDesc =  'Manufacturer Already Exists'
					ROLLBACK  TRAN
				END	
				
			
		END 

GO
/****** Object:  StoredProcedure [dbo].[proc_InsertUpdate_Payments]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_InsertUpdate_Payments]

	@pBill_id							int,
	@pPayment_Mode						int,
	@pPayment_Date						nvarchar(50),
	@pTotal_Amount						float,
	@pPaid_Amount						float,
	@pBalance_Amount					float,
	@pTime_Of_Day						nvarchar(50),
	@pDate_Of_Day						nvarchar(50),
	@pMonth_Of_Day						nvarchar(50),
	@pYear_Of_Day						nvarchar(50),
	@pFlag								bit = 0 OUTPUT,
	@pDesc								varchar(50) = NULL OUTPUT



AS
	BEGIN TRAN
			INSERT INTO Payments(Bill_id,Payment_Mode,Payment_Date,Total_Amount,Paid_Amount,Balance_Amount,Time_Of_Day,Date_Of_Day,Month_Of_Day,Year_Of_Day)
			VALUES(@pBill_id,@pPayment_Mode,@pPayment_Date,@pTotal_Amount,@pPaid_Amount,@pBalance_Amount,@pTime_Of_Day,@pDate_Of_Day,@pMonth_Of_Day,@pYear_Of_Day)	

			IF @@Error = 0
				BEGIN	
					SET @pFlag = 1
					SET @pDesc =  'Payment Added Successfully'
				COMMIT TRAN
				END
				ELSE
				BEGIN
					SET @pFlag = 0
					SET @pDesc =  @@error
					ROLLBACK  TRAN
	END

GO
/****** Object:  StoredProcedure [dbo].[proc_InsertUpdate_Premises]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Salman Ahmed>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_InsertUpdate_Premises]
	-- Add the parameters for the stored procedure here
								
	@pid								int,
	@pName								nvarchar(255),
	@pPc_Mac_Address					nvarchar(50),
	@pPhone								nvarchar(50),
	@pCity								int,
	@pCountry							int,
	@pAddress							nvarchar(255),
	@pOffice							nvarchar(50),
	@pFactory							nvarchar(50),
	@pStore								nvarchar(50),
	@pShop								nvarchar(50),
	@pEnable							int = 0,
	@pAddedBy							int = 0,
	@pUpdatedBy							int = 0,
	@pTime_Of_Day						nvarchar(50),
	@pDate_Of_Day						nvarchar(50),
	@pMonth_Of_Day						nvarchar(50),
	@pYear_Of_Day						nvarchar(50),
	@pFlag								bit = 0 OUTPUT,
	@pDesc								varchar(50) = NULL OUTPUT,
	@pPremisesid_Out					varchar(50) = NULL OUTPUT
			
			

AS

	BEGIN TRAN
			IF @pid= 0
			BEGIN 
				IF NOT Exists (SELECT Name FROM Premises U WHERE UPPER (Name) = UPPER (@pName))
				BEGIN

					INSERT INTO Premises(Name,Pc_Mac_Address,Phone,City,Country,Address,Office,Factory,Store,Shop,Enable,AddedBy,Time_Of_Day,Date_Of_Day,Month_Of_Day,Year_Of_Day)
	
			VALUES(@pName,@pPc_Mac_Address,@pPhone,@pCity,@pCountry,@pAddress,@pOffice,@pFactory,@pStore,@pShop,@pEnable,@pAddedBy,@pTime_Of_Day,@pDate_Of_Day,@pMonth_Of_Day,@pYear_Of_Day)	

						IF @@Error = 0
						BEGIN	
							SET @pFlag = 1
							SET @pDesc =  'Premises Added Successfully'
							SET @pPremisesid_Out = IDENT_CURRENT('Premises')
							COMMIT TRAN
						END
						ELSE
						BEGIN
							SET @pFlag = 0
							SET @pDesc =  @@error
							ROLLBACK  TRAN
						END
				END
				ELSE
				BEGIN
					SET @pFlag = 0
					SET @pDesc =  'Premises Already Exists'
					ROLLBACK  TRAN
				END					
			END
			ELSE
			BEGIN
			IF NOT Exists (SELECT Name FROM Premises U WHERE UPPER (Name) = UPPER (@pName) and (id)  <> @pid)
					
			BEGIN	
					
					UPDATE Premises
					SET Name = @pName, 
						Pc_Mac_Address = @pPc_Mac_Address,
						Phone = @pPhone,
						City = @pCity,
						Country = @pCountry,
						Address = @pAddress,
						Office = @pOffice,
						Factory = @pFactory,
						Store = @pStore,
						Shop = @pShop,
						UpdatedBy = @pUpdatedBy
					WHERE id = @pid


					IF @@Error = 0
					BEGIN	
						SET @pFlag = 1
						SET @pDesc =  'Premises Updated Successfully'
						COMMIT TRAN
					END

					ELSE
					BEGIN
						SET @pFlag = 0
						SET @pDesc =  @@error
						ROLLBACK  TRAN
					END
				
				END
				ELSE
				BEGIN
					SET @pFlag = 0
					SET @pDesc =  'Premises Already Exists'
					ROLLBACK  TRAN
				END	
				
			
		END 
GO
/****** Object:  StoredProcedure [dbo].[proc_InsertUpdate_PurchaseOrder]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Salman Ahmed>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_InsertUpdate_PurchaseOrder]
	-- Add the parameters for the stored procedure here
			
	@pid								int = 0,					
	@pTempOrderNum						nvarchar(50),
	@pPremisesId						int,
	@pUserId							nvarchar(50),
	@pTotalItems						nvarchar(50),
	@pApproved							nvarchar(50),
	@pApprovedByUI						nvarchar(50),
	@pRecieveStatus						nvarchar(50),
	@pEnable							int,
	@pAddedBy							nvarchar(50),
	@pUpdatedBy							nvarchar(50),
	@pTime_Of_Day						nvarchar(50),
	@pDate_Of_Day						nvarchar(50),
	@pMonth_Of_Day						nvarchar(50),
	@pYear_Of_Day						nvarchar(50),
	@pFlag								bit = 0 OUTPUT,
	@pDesc								nvarchar(50) = NULL OUTPUT,
	@pPO_Output							nvarchar(50) = NULL OUTPUT



AS
	BEGIN TRAN
	
	IF @pid= 0
			BEGIN 
				IF NOT Exists (SELECT TempOrderNum FROM Purchasing U WHERE UPPER (TempOrderNum) = UPPER (@pTempOrderNum))
				BEGIN

					INSERT INTO Purchasing(TempOrderNum , PremisesId , UserId , TotalItems ,Approved , ApprovedByUI, RecieveStatus,Enable,AddedBy, Time_Of_Day,Date_Of_Day,Month_Of_Day,Year_Of_Day)
	
					VALUES(@pTempOrderNum,@pPremisesId,@pUserId,@pTotalItems,@pApproved,@pApprovedByUI,@pRecieveStatus,@pEnable,@pAddedBy,@pTime_Of_Day,@pDate_Of_Day,@pMonth_Of_Day,@pYear_Of_Day)	

						IF @@Error = 0
						BEGIN	
							SET @pFlag = 1
							SET @pDesc =  'Purchase Order Created Successfully'
							SET @pPO_Output =  IDENT_CURRENT('Purchasing')
							COMMIT TRAN
						END
						ELSE
						BEGIN
							SET @pFlag = 0
							SET @pDesc =  @@error
							ROLLBACK  TRAN
						END
				END
				ELSE
				BEGIN
					SET @pFlag = 0
					SET @pDesc =  'Purchase Order Already Exists'
					ROLLBACK  TRAN
				END					
			END
			ELSE
			BEGIN
			IF NOT Exists (SELECT TempOrderNum FROM Purchasing U WHERE UPPER (TempOrderNum) = UPPER (@pTempOrderNum) and (id)  <> @pid)
					
			BEGIN	
					
					UPDATE Purchasing
					SET UpdatedBy = @pUpdatedBy,
					TotalItems = @pTotalItems
					WHERE id = @pid


					IF @@Error = 0
					BEGIN	
						SET @pFlag = 1
						SET @pDesc =  'Purchase Order Updated Successfully'
						COMMIT TRAN
					END

					ELSE
					BEGIN
						SET @pFlag = 0
						SET @pDesc =  @@error
						ROLLBACK  TRAN
					END
				
				END
				ELSE
				BEGIN
					SET @pFlag = 0
					SET @pDesc =  'Purchase Order Already Exists'
					ROLLBACK  TRAN
				END	
				
			
		END 
GO
/****** Object:  StoredProcedure [dbo].[proc_InsertUpdate_PurchaseOrderDetail]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_InsertUpdate_PurchaseOrderDetail]

		@dt AS [PurchaseOrderDetail_Datatable]		READONLY
AS
BEGIN
		SET NOCOUNT ON;
		BEGIN TRAN
		INSERT INTO PurchasingDetails(PurchasingId,ItemId,VendorId,Qty,PriceUnit,MsrmntUnit)
		Select [PurchasingId],[ItemId],[VendorId],[Qty],[PriceUnit],[MsrmntUnit] FROM @dt WHERE pd_id = 0
			UPDATE PurchasingDetails
			SET	PurchasingDetails.PurchasingId = Table_B.[PurchasingId],
				PurchasingDetails.ItemId = Table_B.[ItemId],
				PurchasingDetails.VendorId = Table_B.[VendorId],
				PurchasingDetails.Qty = Table_B.[Qty],
				PurchasingDetails.PriceUnit = Table_B.[PriceUnit],
				PurchasingDetails.MsrmntUnit = Table_B.[MsrmntUnit]
				
			FROM
				PurchasingDetails AS Table_A
				INNER JOIN @dt AS Table_B
				ON Table_A.[ItemId] = Table_B.[ItemId]
			WHERE Table_B.[ItemId] <> 0
			AND Table_B.[ItemId] = Table_A.[ItemId]
			AND Table_B.[PurchasingId] = Table_A.[PurchasingId]
			AND Table_B.[PriceUnit] = Table_A.[PriceUnit]
		Commit Tran
END 

GO
/****** Object:  StoredProcedure [dbo].[proc_InsertUpdate_RolePrivileges]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_InsertUpdate_RolePrivileges] --4,1,102,1,NULL,NULL,NULL
		
		@dt AS RolePrivDataTable		READONLY
	
AS
	BEGIN
	SET NOCOUNT ON;
		BEGIN TRAN

		
		INSERT INTO RolePrivileges(Role_id, Priv_id, Check_Status, Enable)
		Select Role_id, Priv_id, Check_Status, Enable FROM @dt where Role_Priv_id = 0 and Enable = 1
		
		UPDATE	RolePrivileges
		SET
			RolePrivileges.Role_id = Table_B.Role_id,
			RolePrivileges.Priv_id = Table_B.Priv_id,
			RolePrivileges.Check_Status = Table_B.Check_Status,
			RolePrivileges.Enable = Table_B.Enable
		FROM
			RolePrivileges AS Table_A
			INNER JOIN @dt AS Table_B
				ON Table_A.Role_Priv_id = Table_B.Role_Priv_id
		WHERE Table_B.Role_Priv_id <> 0

	Commit Tran
END
GO
/****** Object:  StoredProcedure [dbo].[proc_InsertUpdate_Roles]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Salman Ahmed>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_InsertUpdate_Roles]
	-- Add the parameters for the stored procedure here

	@pRole_id							int = 0,
	@pRole_Name							nvarchar(50),
	@pEnable							int,
	@pAddedBy							int = 0,
	@pUpdatedBy							int = 0,
	@Time_Of_Day						nvarchar(50),
	@Date_Of_Day						nvarchar(50),
	@Month_Of_Day						nvarchar(50),
	@Year_Of_Day						nvarchar(50),
	@pFlag								bit = 0 OUTPUT,
	@pDesc								varchar(50) = NULL OUTPUT

AS
	BEGIN TRAN
			IF @pRole_id= 0
			BEGIN 
				IF NOT Exists (SELECT Role_Name FROM Roles U WHERE UPPER (Role_Name) = UPPER (@pRole_Name))
				BEGIN

					INSERT INTO Roles (Role_Name, Enable, AddedBy, Time_Of_Day, Date_Of_Day, Month_Of_Day, Year_Of_Day)
	
					VALUES(@pRole_Name,@pEnable,@pAddedBy,@Time_Of_Day,@Date_Of_Day,@Month_Of_Day,@Year_Of_Day)	

						IF @@Error = 0
						BEGIN	
							SET @pFlag = 1
							SET @pDesc =  'Role Added Successfully'
							COMMIT TRAN
						END
						ELSE
						BEGIN
							SET @pFlag = 0
							SET @pDesc =  @@error
							ROLLBACK  TRAN
						END
				END
				ELSE
				BEGIN
					SET @pFlag = 0
					SET @pDesc =  'Role Already Exists'
					ROLLBACK  TRAN
				END					
			END
			ELSE
			BEGIN
			IF NOT Exists (SELECT Role_Name FROM Roles U WHERE UPPER (Role_Name) = UPPER (@pRole_Name) and (Role_id)  <> @pRole_id)
					
			BEGIN	
					
					UPDATE Roles
					SET Role_Name=@pRole_Name,
					UpdatedBy = @pUpdatedBy
					WHERE Role_id = @pRole_id


					IF @@Error = 0
					BEGIN	
						SET @pFlag = 1
						SET @pDesc =  'Role Updated Successfully'
						COMMIT TRAN
					END

					ELSE
					BEGIN
						SET @pFlag = 0
						SET @pDesc =  @@error
						ROLLBACK  TRAN
					END
				
				END
				ELSE
				BEGIN
					SET @pFlag = 0
					SET @pDesc =  'Role Already Exists'
					ROLLBACK  TRAN
				END	
				
			
		END 

GO
/****** Object:  StoredProcedure [dbo].[proc_InsertUpdate_SaleOrderOrderDetail]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_InsertUpdate_SaleOrderOrderDetail]

		@dt AS [SaleOrderDetail_Datatable]		READONLY
AS
BEGIN
		SET NOCOUNT ON;
		BEGIN TRAN
		INSERT INTO SalesOrder_Details(SalesOrder_id,ItemId,Customer_id,Qty,PriceUnit,MsrmntUnit)
		Select [SalesOrder_id],[ItemId],[Customer_id],[Qty],[PriceUnit],[MsrmntUnit] FROM @dt WHERE id = 0
			UPDATE SalesOrder_Details
			SET	SalesOrder_Details.SalesOrder_id = Table_B.[SalesOrder_id],
				SalesOrder_Details.ItemId = Table_B.[ItemId],
				SalesOrder_Details.Customer_id = Table_B.[Customer_id],
				SalesOrder_Details.Qty = Table_B.[Qty],
				SalesOrder_Details.PriceUnit = Table_B.[PriceUnit],
				SalesOrder_Details.MsrmntUnit = Table_B.[MsrmntUnit]
				
			FROM
				SalesOrder_Details AS Table_A
				INNER JOIN @dt AS Table_B
				ON Table_A.[ItemId] = Table_B.[ItemId]
			WHERE Table_B.[ItemId] <> 0
			AND Table_B.[ItemId] = Table_A.[ItemId]
			AND Table_B.[SalesOrder_id] = Table_A.[SalesOrder_id]
			AND Table_B.[PriceUnit] = Table_A.[PriceUnit]
		Commit Tran
END 

GO
/****** Object:  StoredProcedure [dbo].[proc_InsertUpdate_SalesOrder]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Salman Ahmed>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_InsertUpdate_SalesOrder]
	-- Add the parameters for the stored procedure here
								
	@pid										nvarchar(50),
	@pSaleOrderNo								nvarchar(50),
	@pPremisesId								int,
	@pUserId									int,
	@pTotalItems								nvarchar(50),
	@pSO_Total_Amount							nvarchar(50),
	@pSO_Status									nvarchar(50),
	@pSO_Invoice_Status							nvarchar(50),
	@pSO_Shipment_Status						bit,
	@pSO_Package_Status							bit,
	@pSO_DateTime								nvarchar(50),
	@pEnable									int,
	@pAddedBy									int = 0,
	@pUpdatedBy									int = 0,
	@pTime_Of_Day								nvarchar(50),
	@pDate_Of_Day								nvarchar(50),
	@pMonth_Of_Day								nvarchar(50),
	@pYear_Of_Day								nvarchar(50),
	@pFlag										bit = 0 OUTPUT,
	@pDesc										nvarchar(50) = NULL OUTPUT,
	@pSO_Output									nvarchar(50) = NULL OUTPUT

AS
	BEGIN TRAN
	IF @pid= 0
			BEGIN 
				IF NOT Exists (SELECT SaleOrderNo FROM SalesOrder S WHERE UPPER (SaleOrderNo) = UPPER (@pSaleOrderNo))
				BEGIN

					INSERT INTO SalesOrder(SaleOrderNo,PremisesId,UserId,TotalItems,SO_Total_Amount,SO_Status,SO_Invoice_Status,SO_Shipment_Status,SO_Package_Status,SO_DateTime,Enable,AddedBy,Time_Of_Day,Date_Of_Day,Month_Of_Day,Year_Of_Day)
	
					VALUES(@pSaleOrderNo,@pPremisesId,@pUserId,@pTotalItems,@pSO_Total_Amount,@pSO_Status,@pSO_Invoice_Status,@pSO_Shipment_Status,@pSO_Package_Status,@pSO_DateTime,@pEnable,@pAddedBy,@pTime_Of_Day,@pDate_Of_Day,@pMonth_Of_Day,@pYear_Of_Day)	

						IF @@Error = 0
						BEGIN	
							SET @pFlag = 1
							SET @pDesc =  'Sales Order Created Successfully'
							SET @pSO_Output =  IDENT_CURRENT('SalesOrder')
							COMMIT TRAN
						END
						ELSE
						BEGIN
							SET @pFlag = 0
							SET @pDesc =  @@error
							ROLLBACK  TRAN
						END
				END
				ELSE
				BEGIN
					SET @pFlag = 0
					SET @pDesc =  'Sales Order Already Exists'
					ROLLBACK  TRAN
				END					
			END
			ELSE
			BEGIN
			IF NOT Exists (SELECT SaleOrderNo FROM SalesOrder U WHERE UPPER (SaleOrderNo) = UPPER (@pSaleOrderNo) and (id)  <> @pid)
					
			BEGIN	
					
					UPDATE SalesOrder
					SET TotalItems=@pTotalItems,
					SO_Total_Amount = @pSO_Total_Amount,
					UpdatedBy = @pUpdatedBy
					WHERE id = @pid


					IF @@Error = 0
					BEGIN	
						SET @pFlag = 1
						SET @pDesc =  'Sales Order Updated Successfully'
						SET @pSO_Output =  IDENT_CURRENT('SalesOrder')
						COMMIT TRAN
					END

					ELSE
					BEGIN
						SET @pFlag = 0
						SET @pDesc =  @@error
						ROLLBACK  TRAN
					END
				
				END
				ELSE
				BEGIN
					SET @pFlag = 0
					SET @pDesc =  'Sales Order Already Exists'
					ROLLBACK  TRAN
				END	
				
			
		END 






















			

GO
/****** Object:  StoredProcedure [dbo].[proc_InsertUpdate_SO_Invoice]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Salman Ahmed>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_InsertUpdate_SO_Invoice] --4,'1','','0.00','0.00','0.00','','','','','',''
	-- Add the parameters for the stored procedure here

		@pSalesOrder_id							int,
		@pCustomer_id							int,
		@pInvoice_No							nvarchar(50),
		@pInvoice_Status						nvarchar(MAX),
		@pInvoice_Amount						nvarchar(50),
		@pAmount_Paid							nvarchar(50),
		@pBalance_Amount						nvarchar(50),
		@pInvoiceDateTime						nvarchar(50),
		@pInvoiceDueDate						nvarchar(50),
		@pAddedBy								int = 0,
		@pTime_Of_Day							nvarchar(50),
		@pDate_Of_Day							nvarchar(50),
		@pMonth_Of_Day							nvarchar(50),
		@pYear_Of_Day							nvarchar(50),
		@pFlag									bit = 0 OUTPUT,
		@pDesc									varchar(50) = NULL OUTPUT,
		@pinvoice_id_ouput						varchar(50) = NULL OUTPUT


AS
BEGIN TRAN
	BEGIN 
			
			IF NOT Exists (SELECT Invoice_No FROM SalesOrder_Invoices U WHERE UPPER (Invoice_No) = UPPER (@pInvoice_No))
				BEGIN

					INSERT INTO SalesOrder_Invoices(SalesOrder_id,Invoice_No,Customer_id,Invoice_Status,Invoice_Amount,Amount_Paid,Balance_Amount,InvoiceDateTime,InvoiceDueDate, AddedBy,Time_Of_Day,Date_Of_Day,Month_Of_Day,Year_Of_Day)

					VALUES(@pSalesOrder_id,@pInvoice_No,@pCustomer_id,@pInvoice_Status,@pInvoice_Amount,@pAmount_Paid,@pBalance_Amount,@pInvoiceDateTime,@pInvoiceDueDate,@pAddedBy,@pTime_Of_Day,@pDate_Of_Day,@pMonth_Of_Day,@pYear_Of_Day)	

						IF @@Error = 0
						BEGIN	
							SET @pFlag = 1
							SET @pDesc =  'Invoice Created Successfully'
							SET @pinvoice_id_ouput = SCOPE_IDENTITY()
							COMMIT TRAN
						END
						ELSE
						BEGIN
							SET @pFlag = 0
							SET @pDesc =  @@error
							SET @pinvoice_id_ouput = NULL
							ROLLBACK  TRAN
						END
				END
				ELSE
				BEGIN
					SET @pFlag = 0
					SET @pDesc =  'Invoice No Already Exists'
					ROLLBACK  TRAN
				END	
END

				
GO
/****** Object:  StoredProcedure [dbo].[proc_InsertUpdate_SO_packages]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_InsertUpdate_SO_packages]

		@pSaleOrder_id					int,
		@pPackage_No					nvarchar(50),
		@pPackage_Cost					nvarchar(50) = Null,
		@pPackage_Status				nvarchar(50),
		@pPackage_Date					nvarchar(50),
		@pAddedBy						int = 0,	
		@pTime_Of_Day					nvarchar(50),
		@pDate_Of_Day					nvarchar(50),
		@pMonth_Of_Day					nvarchar(50),
		@pYear_Of_Day					nvarchar(50),
		@pFlag							bit = 0 OUTPUT,
		@pDesc							varchar(50) = NULL OUTPUT

AS
	BEGIN TRAN
	
			INSERT INTO SO_Packages(SaleOrder_id,Package_No,Package_Cost,Package_Status,Package_Date,AddedBy,Time_Of_Day,Date_Of_Day,Month_Of_Day,Year_Of_Day)
			VALUES(@pSaleOrder_id,@pPackage_No,@pPackage_Cost,@pPackage_Status,@pPackage_Date,@pAddedBy,@pTime_Of_Day,@pDate_Of_Day,@pMonth_Of_Day,@pYear_Of_Day)	
			
			IF @@Error = 0
				BEGIN	
					SET @pFlag = 1
					SET @pDesc =  'Package Created Successfully'
					
				COMMIT TRAN
				END
				ELSE
				BEGIN
					SET @pFlag = 0
					SET @pDesc =  @@error
					ROLLBACK  
	END
GO
/****** Object:  StoredProcedure [dbo].[proc_InsertUpdate_SO_Shipment]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_InsertUpdate_SO_Shipment]

		@pSaleOrder_id					int,
		@pShipment_No					nvarchar(50),
		@pShipment_Cost					nvarchar(50) = Null,
		@pShipment_Status				nvarchar(50),
		@pShipment_Date					nvarchar(50),
		@pAddedBy						int = 0,	
		@pTime_Of_Day					nvarchar(50),
		@pDate_Of_Day					nvarchar(50),
		@pMonth_Of_Day					nvarchar(50),
		@pYear_Of_Day					nvarchar(50),
		@pFlag							bit = 0 OUTPUT,
		@pDesc							varchar(50) = NULL OUTPUT,
		@pShipementIdout				int = 0 OUTPUT

AS
	BEGIN TRAN
	
			INSERT INTO Shipment(SaleOrder_id,Shipment_No,Shipment_Cost,Shipment_Status,Shipment_Date,AddedBy,Time_Of_Day,Date_Of_Day,Month_Of_Day,Year_Of_Day)
			VALUES(@pSaleOrder_id,@pShipment_No,@pShipment_Cost,@pShipment_Status,@pShipment_Date,@pAddedBy,@pTime_Of_Day,@pDate_Of_Day,@pMonth_Of_Day,@pYear_Of_Day)	
			
			IF @@Error = 0
				BEGIN	
					SET @pFlag = 1
					SET @pDesc =  'Shipment Created Successfully'
									COMMIT TRAN
				END
				ELSE
				BEGIN
					SET @pFlag = 0
					SET @pDesc =  @@error
					SET @pShipementIdout = IDENT_CURRENT('Shipment')
					ROLLBACK  
	END
GO
/****** Object:  StoredProcedure [dbo].[proc_InsertUpdate_SOInvoice_Payments]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_InsertUpdate_SOInvoice_Payments]

		@pSO_Invoice_id						int,
		@pSO_Payment_Mode					int,
		@pSO_Payment_Date					nvarchar(50),
		@pSO_Total_Amount					float,
		@pSO_Paid_Amount					float,
		@pSO_Balance_Amount					float,
		@pAddedBy							int = 0,
		@pTime_Of_Day						nvarchar(50),
		@pDate_Of_Day						nvarchar(50),
		@pMonth_Of_Day						nvarchar(50),
		@pYear_Of_Day						nvarchar(50),
		@pFlag								bit = 0 OUTPUT,
		@pDesc								varchar(50) = NULL OUTPUT,
		@pinv_id_output						int = 0 OUTPUT,
		@pbalance_output					float = 0 OUTPUT


AS
	BEGIN TRAN
			INSERT INTO SO_Payments(SO_Invoice_id,SO_Payment_Mode,SO_Payment_Date,SO_Total_Amount,SO_Paid_Amount,SO_Balance_Amount,AddedBy,Time_Of_Day,Date_Of_Day,Month_Of_Day,Year_Of_Day			)
			VALUES(@pSO_Invoice_id,@pSO_Payment_Mode,@pSO_Payment_Date,@pSO_Total_Amount,@pSO_Paid_Amount,@pSO_Balance_Amount,@pAddedBy,@pTime_Of_Day,@pDate_Of_Day,@pMonth_Of_Day,@pYear_Of_Day)	
			
			IF @@Error = 0
				BEGIN	
					SET @pFlag = 1
					SET @pDesc =  'Payment Added Successfully'
					SET @pinv_id_output = @pSO_Invoice_id
					SET	@pbalance_output = @pSO_Balance_Amount
					Update SalesOrder_Invoices set Balance_Amount = @pbalance_output where id= @pinv_id_output
				COMMIT TRAN
				END
				ELSE
				BEGIN
					SET @pFlag = 0
					SET @pDesc =  @@error
					ROLLBACK  
	END
	print @pinv_id_output
	print @pbalance_output
GO
/****** Object:  StoredProcedure [dbo].[proc_InsertUpdate_Unit]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_InsertUpdate_Unit]
	-- Add the parameters for the stored procedure here

	@pUnit_id							int = 0,
	@pUnit_Name							nvarchar(50),
	@pEnable							int,
	@pAddedBy							int = 0,
	@pUpdatedBy							int = 0,
	@Time_Of_Day						nvarchar(50),
	@Date_Of_Day						nvarchar(50),
	@Month_Of_Day						nvarchar(50),
	@Year_Of_Day						nvarchar(50),
	@pFlag								bit = 0 OUTPUT,
	@pDesc								varchar(50) = NULL OUTPUT,
	@pUnitid_Out						varchar(50) = NULL OUTPUT

AS
	BEGIN TRAN
			IF @pUnit_id= 0
			BEGIN 
				IF NOT Exists (SELECT Unit_Name FROM Units U WHERE UPPER (Unit_Name) = UPPER (@pUnit_Name))
				BEGIN

					INSERT INTO Units(Unit_Name, Enable, AddedBy, Time_Of_Day, Date_Of_Day, Month_Of_Day, Year_Of_Day)
	
					VALUES(@pUnit_Name,@pEnable,@pAddedBy,@Time_Of_Day,@Date_Of_Day,@Month_Of_Day,@Year_Of_Day)	

						IF @@Error = 0
						BEGIN	
							SET @pFlag = 1
							SET @pDesc =  'Unit Added Successfully'
							SET @pUnitid_Out = IDENT_CURRENT('Units')
							COMMIT TRAN
						END
						ELSE
						BEGIN
							SET @pFlag = 0
							SET @pDesc =  @@error
							ROLLBACK  TRAN
						END
				END
				ELSE
				BEGIN
					SET @pFlag = 0
					SET @pDesc =  'Unit Already Exists'
					ROLLBACK  TRAN
				END					
			END
			ELSE
			BEGIN
			IF NOT Exists (SELECT Unit_Name FROM Units U WHERE UPPER (Unit_Name) = UPPER (@pUnit_Name) and (Unit_id)  <> @pUnit_id)
					
			BEGIN	
					
					UPDATE Units
					SET Unit_Name=@pUnit_Name,
					UpdatedBy = @pUpdatedBy
					WHERE Unit_id = @pUnit_id


					IF @@Error = 0
					BEGIN	
						SET @pFlag = 1
						SET @pDesc =  'Unit Updated Successfully'
						COMMIT TRAN
					END

					ELSE
					BEGIN
						SET @pFlag = 0
						SET @pDesc =  @@error
						ROLLBACK  TRAN
					END
				
				END
				ELSE
				BEGIN
					SET @pFlag = 0
					SET @pDesc =  'Unit Already Exists'
					ROLLBACK  TRAN
				END	
				
			
		END 

GO
/****** Object:  StoredProcedure [dbo].[proc_InsertUpdate_UserPrivileges]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_InsertUpdate_UserPrivileges] --4,1,102,1,NULL,NULL,NULL
		
		@dt AS UserPrivDataTable		READONLY
	
AS
	BEGIN
	SET NOCOUNT ON;
		BEGIN TRAN

		
		INSERT INTO User_Privileges([Priv_id], [User_id], [Add], [Edit], [View], [Profile])
		Select [Priv_id], [User_id], [Add], [Edit], [View], [Profile] FROM @dt WHERE id = 0
		
		UPDATE	User_Privileges
		SET
			User_Privileges.[Priv_id] = Table_B.Priv_id,
			User_Privileges.[User_id] = Table_B.[User_id],
			User_Privileges.[Add] = Table_B.[Add],
			User_Privileges.[Edit] = Table_B.[Edit],
			User_Privileges.[View] = Table_B.[View],
			User_Privileges.[Profile] = Table_B.[Profile]
		FROM
			User_Privileges AS Table_A
			INNER JOIN @dt AS Table_B
				ON Table_A.id = Table_B.id
		WHERE Table_B.id <> 0



	Commit Tran
END
GO
/****** Object:  StoredProcedure [dbo].[proc_InsertUpdate_Users]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Salman Ahmed>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_InsertUpdate_Users]
	-- Add the parameters for the stored procedure here

	@pid								int = 0,
	@pemail								nvarchar(50),
	@ppassword							nvarchar(50),
	@pattached_profile					nvarchar(50),
	@pPremises_id						int = 0,
	@pRole_id							int = 0,
	--@ppao								nvarchar(50),
	--@ppaf								nvarchar(50),
	--@ppas								nvarchar(50),
	--@ppas_								nvarchar(50),
	--@ppav								nvarchar(50),
	--@ppap								nvarchar(50),
	--@ppac								nvarchar(50),
	--@ppas__								nvarchar(50),
	--@ppae								nvarchar(50),
	--@ppap_								nvarchar(50),
	--@ppai								nvarchar(50),
	--@ppas___							nvarchar(50),
	--@ppau								nvarchar(50),
	--@ppuo								nvarchar(50),
	--@ppuf								nvarchar(50),
	--@ppus								nvarchar(50),
	--@ppus_								nvarchar(50),
	--@ppuv								nvarchar(50),
	--@ppup								nvarchar(50),
	--@ppuc								nvarchar(50),
	--@ppus__								nvarchar(50),
	--@ppue								nvarchar(50),
	--@ppup_								nvarchar(50),
	--@ppui								nvarchar(50),
	--@ppus___							nvarchar(50),
	--@ppuu								nvarchar(50),
	--@ppdo								nvarchar(50),
	--@ppdf								nvarchar(50),
	--@ppds								nvarchar(50),
	--@ppds_								nvarchar(50),
	--@ppdv								nvarchar(50),
	--@ppdp								nvarchar(50),
	--@ppdc								nvarchar(50),
	--@ppds__								nvarchar(50),
	--@ppde								nvarchar(50),
	--@ppdp_								nvarchar(50),
	--@ppdi								nvarchar(50),
	--@ppds___							nvarchar(50),
	--@ppdu								nvarchar(50),
	--@ppvo								nvarchar(50),
	--@ppvf								nvarchar(50),
	--@ppvs								nvarchar(50),
	--@ppvs_								nvarchar(50),
	--@ppvv								nvarchar(50),
	--@ppvp								nvarchar(50),
	--@ppvc								nvarchar(50),
	--@ppvs__								nvarchar(50),
	--@ppve								nvarchar(50),
	--@ppvp_								nvarchar(50),
	--@ppvi								nvarchar(50),
	--@ppvs___							nvarchar(50),
	--@ppvu								nvarchar(50),
	--@ppvol								nvarchar(50),
	--@ppvfl								nvarchar(50),
	--@ppvsl								nvarchar(50),
	--@ppvsl_								nvarchar(50),
	--@ppvvl								nvarchar(50),
	--@ppvpl								nvarchar(50),
	--@ppvcl								nvarchar(50),
	--@ppvsl__							nvarchar(50),
	--@ppvel								nvarchar(50),
	--@ppvpl_								nvarchar(50),
	--@ppvil								nvarchar(50),
	--@ppvsl___							nvarchar(50),
	--@ppvul								nvarchar(50),
	@pEnable							nvarchar(50),
	@pAddedBy							int = 0,
	@pUpdatedBy							int = 0,
	@pTime_Of_Day						nvarchar(50),
	@pDate_Of_Day						nvarchar(50),
	@pMonth_Of_Day						nvarchar(50),
	@pYear_Of_Day						nvarchar(50),
	@pFlag								bit = 0 OUTPUT,
	@pDesc								varchar(50) = NULL OUTPUT,
	@pUserid_Out						varchar(50) = NULL OUTPUT


AS
	BEGIN TRAN
			IF @pid= 0
			BEGIN 
				IF NOT Exists (SELECT email FROM Users U WHERE UPPER (email) = UPPER (@pemail))
				BEGIN

					INSERT INTO Users (email,password,attached_profile,Premises_id,Role_id,Enable,AddedBy,Time_Of_Day,Date_Of_Day,Month_Of_Day,Year_Of_Day)
	
					VALUES(@pemail,@ppassword,@pattached_profile,@pPremises_id,@pRole_id,@pEnable,@pAddedBy,@pTime_Of_Day,@pDate_Of_Day,@pMonth_Of_Day,@pYear_Of_Day)	

						IF @@Error = 0
						BEGIN	
							SET @pFlag = 1
							SET @pDesc =  'User Added Successfully'
							SET @pUserid_Out = IDENT_CURRENT('Users')
							COMMIT TRAN
						END
						ELSE
						BEGIN
							SET @pFlag = 0
							SET @pDesc =  @@error
							ROLLBACK  TRAN
						END
				END
				ELSE
				BEGIN
					SET @pFlag = 0
					SET @pDesc =  'User Already Exists'
					ROLLBACK  TRAN
				END					
			END
			ELSE
			BEGIN
			IF NOT Exists (SELECT email FROM Users U WHERE UPPER (email) = UPPER (@pemail) and (id)  <> @pid)
					
			BEGIN	
					
					UPDATE Users
					SET email = @pemail,						
						password = @ppassword,					
						attached_profile = @pattached_profile,
						Premises_id = @pPremises_id,
						Role_id=@pRole_id,		
						UpdatedBy = @pUpdatedBy	
					WHERE id = @pid	


					IF @@Error = 0
					BEGIN	
						SET @pFlag = 1
						SET @pDesc =  'User Updated Successfully'
						COMMIT TRAN
					END

					ELSE
					BEGIN
						SET @pFlag = 0
						SET @pDesc =  @@error
						ROLLBACK  TRAN
					END
				
				END
				ELSE
				BEGIN
					SET @pFlag = 0
					SET @pDesc =  'User Already Exists'
					ROLLBACK  TRAN
				END	
				
			
		END 














	--	AS
	--BEGIN TRAN
	--		IF @pid= 0
	--		BEGIN 
	--			IF NOT Exists (SELECT email FROM Users U WHERE UPPER (email) = UPPER (@pemail))
	--			BEGIN

	--				INSERT INTO Users (email,password,attached_profile,Premises_id,Role_id,pao,paf,pas,pas_,pav,pap,pac,pas__,pae,pap_,pai,pas___,pau,puo,puf,pus,pus_,puv,pup,puc,pus__,pue,pup_,pui,pus___,puu,pdo,pdf,pds,pds_,pdv,pdp,pdc,pds__,pde,pdp_,pdi,pds___,pdu,pvo,pvf,pvs,pvs_,pvv,pvp,pvc,pvs__,pve,pvp_,pvi,pvs___,pvu,pvol,pvfl,pvsl,pvsl_,pvvl,pvpl,pvcl,pvsl__,pvel,pvpl_,pvil,pvsl___,pvul,Enable,AddedBy,Time_Of_Day,Date_Of_Day,Month_Of_Day,Year_Of_Day)
	
	--				VALUES(@pemail,@ppassword,@pattached_profile,@pPremises_id,@pRole_id,@ppao,@ppaf,@ppas,@ppas_,@ppav,@ppap,@ppac,@ppas__,@ppae,@ppap_,@ppai,@ppas___,@ppau,@ppuo,@ppuf,@ppus,@ppus_,@ppuv,@ppup,@ppuc,@ppus__,@ppue,@ppup_,@ppui,@ppus___,@ppuu,@ppdo,@ppdf,@ppds,@ppds_,@ppdv,@ppdp,@ppdc,@ppds__,@ppde,@ppdp_,@ppdi,@ppds___,@ppdu,@ppvo,@ppvf,@ppvs,@ppvs_,@ppvv,@ppvp,@ppvc,@ppvs__,@ppve,@ppvp_,@ppvi,@ppvs___,@ppvu,@ppvol,@ppvfl,@ppvsl,@ppvsl_,@ppvvl,@ppvpl,@ppvcl,@ppvsl__,@ppvel,@ppvpl_,@ppvil,@ppvsl___,@ppvul,@pEnable,@pAddedBy,@pTime_Of_Day,@pDate_Of_Day,@pMonth_Of_Day,@pYear_Of_Day)	

	--					IF @@Error = 0
	--					BEGIN	
	--						SET @pFlag = 1
	--						SET @pDesc =  'User Added Successfully'
	--						SET @pUserid_Out = IDENT_CURRENT('Users')
	--						COMMIT TRAN
	--					END
	--					ELSE
	--					BEGIN
	--						SET @pFlag = 0
	--						SET @pDesc =  @@error
	--						ROLLBACK  TRAN
	--					END
	--			END
	--			ELSE
	--			BEGIN
	--				SET @pFlag = 0
	--				SET @pDesc =  'User Already Exists'
	--				ROLLBACK  TRAN
	--			END					
	--		END
	--		ELSE
	--		BEGIN
	--		IF NOT Exists (SELECT email FROM Users U WHERE UPPER (email) = UPPER (@pemail) and (id)  <> @pid)
					
	--		BEGIN	
					
	--				UPDATE Users
	--				SET email = @pemail,						
	--					password = @ppassword,					
	--					attached_profile = @pattached_profile,
	--					Premises_id = @pPremises_id,
	--					Role_id=@pRole_id,				
	--					pao = @ppao,						
	--					paf = @ppaf,						
	--					pas = @ppas,						
	--					pas_ = @ppas_,						
	--					pav = @ppav,						
	--					pap = @ppap,						
	--					pac = @ppac,						
	--					pas__ = @ppas__,						
	--					pae = @ppae,						
	--					pap_ = @ppap_,						
	--					pai = @ppai,						
	--					pas___ = @ppas___,					
	--					pau = @ppau,						
	--					puo = @ppuo,						
	--					puf = @ppuf,						
	--					pus = @ppus,						
	--					pus_ = @ppus_,						
	--					puv = @ppuv,						
	--					pup = @ppup,						
	--					puc = @ppuc,						
	--					pus__ = @ppus__,						
	--					pue = @ppue,						
	--					pup_ = @ppup_,						
	--					pui = @ppui,						
	--					pus___ = @ppus___,					
	--					puu = @ppuu,						
	--					pdo = @ppdo,						
	--					pdf = @ppdf,						
	--					pds = @ppds,						
	--					pds_ = @ppds_,						
	--					pdv = @ppdv,						
	--					pdp = @ppdp,						
	--					pdc = @ppdc,						
	--					pds__ = @ppds__,						
	--					pde = @ppde,						
	--					pdp_ = @ppdp_,						
	--					pdi = @ppdi,						
	--					pds___ = @ppds___,					
	--					pdu = @ppdu,						
	--					pvo = @ppvo,						
	--					pvf = @ppvf,						
	--					pvs = @ppvs,						
	--					pvs_ = @ppvs_,						
	--					pvv = @ppvv,						
	--					pvp = @ppvp,						
	--					pvc = @ppvc,						
	--					pvs__ = @ppvs__,						
	--					pve = @ppve,						
	--					pvp_ = @ppvp_,						
	--					pvi = @ppvi,						
	--					pvs___ = @ppvs___,					
	--					pvu = @ppvu,						
	--					pvol = @ppvol,						
	--					pvfl = @ppvfl,						
	--					pvsl = @ppvsl,						
	--					pvsl_ = @ppvsl_,						
	--					pvvl = @ppvvl,						
	--					pvpl = @ppvpl,						
	--					pvcl = @ppvcl,						
	--					pvsl__ = @ppvsl__,					
	--					pvel = @ppvel,						
	--					pvpl_ = @ppvpl_,						
	--					pvil =  @ppvil,						
	--					pvsl___ = @ppvsl___,					
	--					pvul = @ppvul,						
	--					UpdatedBy = @pUpdatedBy	
	--				WHERE id = @pid	


	--				IF @@Error = 0
	--				BEGIN	
	--					SET @pFlag = 1
	--					SET @pDesc =  'User Updated Successfully'
	--					COMMIT TRAN
	--				END

	--				ELSE
	--				BEGIN
	--					SET @pFlag = 0
	--					SET @pDesc =  @@error
	--					ROLLBACK  TRAN
	--				END
				
	--			END
	--			ELSE
	--			BEGIN
	--				SET @pFlag = 0
	--				SET @pDesc =  'User Already Exists'
	--				ROLLBACK  TRAN
	--			END	
				
			
	--	END 

GO
/****** Object:  StoredProcedure [dbo].[proc_Premises_By_Type]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Premises_By_Type]  --'0','0','0','1'

	@pOffice				nvarchar(50) = NULL,
	@pFactory				nvarchar(50) = NULL,
	@pStore					nvarchar(50) = NULL,
	@pShop					nvarchar(50) = NULL

AS
BEGIN
	SELECT 
	id,
	Name,
	Pc_Mac_Address,
	Phone,
	City,
	(SELECT Name FROM Cities Where id = p.City) as CityName,
	Country,
	(SELECT Name FROM Countries Where id = p.Country) as CountryName,
	Address,
	Office,
	Factory,
	Store,
	Shop,
	Enable,
	AddedBy,
	UpdatedBy,
	Delete_Request_By,
	Delete_Status,
	UserId,
	Time_Of_Day,
	Date_Of_Day,
	Month_Of_Day,
	Year_Of_Day

	FROM Premises AS p
	WHERE Office = @pOffice 
	AND Factory = @pFactory
	AND Store = @pStore
	AND Shop = @pShop
	
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Reports_Activity_Logs]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,> 
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Reports_Activity_Logs] --'1/7/2019','31/7/2019','Issued'
	
	@pFrom							nvarchar(50) = NULL,
	@pTo							nvarchar(50) = NULL

AS
BEGIN
SELECT 
	Date,
	Time,
	ActivityType,
	ActivityName,
	Description

	FROM ItemActivity as a
	WHERE ((@pFrom is null) or (convert(datetime, @pFrom, 103) <= (convert(datetime, a.Date, 103))))
	AND ((@pTo is null) or (convert(datetime, @pTo, 103) >= (convert(datetime, a.Date, 103))))


END

GO
/****** Object:  StoredProcedure [dbo].[proc_Reports_Bill_Details]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,> 
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Reports_Bill_Details] --'1/1/2019','31/12/2019','Issued'
	
	@pFrom							nvarchar(50) = NULL,
	@pTo							nvarchar(50) = NULL

AS
BEGIN
SELECT 
	Bill_Status,
	BillDateTime,
	BillDueDate,
	Bill_No,
	(SELECT DISTINCT Vendorid FROM PurchasingDetails WHERE PurchasingId = b.Purchase_id) as Vendor_id,
	(SELECT Name FROM Contacts WHERE id = (SELECT DISTINCT Vendorid FROM PurchasingDetails WHERE PurchasingId = b.Purchase_id)) as VendorName,
	Bill_Amount,
	Balance_Amount

	FROM Bills as b
	WHERE ((@pFrom is null) or (convert(nvarchar, @pFrom, 103) <= b.BillDateTime))
	AND ((@pTo is null) or (convert(nvarchar, @pTo, 103) >= b.BillDateTime))
	GROUP BY b.Bill_Status,b.BillDateTime,b.BillDueDate,b.Bill_No,b.Purchase_id,Bill_Amount,b.Balance_Amount

END

GO
/****** Object:  StoredProcedure [dbo].[proc_Reports_Inventory_Details]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,> 
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Reports_Inventory_Details] --'3/6/2019','3/6/2019','cocacola'
	
	@pFrom							nvarchar(50) = NULL,
	@pTo							nvarchar(50) = NULL,
	@pItemName						nvarchar(50) = NULL

AS
BEGIN

	SELECT Distinct
	(SELECT Item_Id FROM Items WHERE Item_id = pd.ItemId) as Item_Id,
	(SELECT Item_Name FROM Items WHERE Item_id = pd.ItemId) as ItemName,
	(SELECT Item_Sku FROM Items WHERE Item_id = pd.ItemId) as Item_Sku,
	(SELECT Physical_Quantity FROM Stock WHERE Item_id = pd.Itemid) as StockInHand,
	(SELECT Physical_Committed FROM Stock WHERE Item_id = pd.Itemid) as CommittedStock,
	(SELECT Physical_Avail_ForSale FROM Stock WHERE Item_id = pd.Itemid) as AvailableForSale,
	(SELECT SUM(CAST(Qty as int)) FROM PurchasingDetails WHERE ItemId = pd.Itemid) as QuantityOrdered,
	(SELECT SUM(CAST(Qty as int)) FROM PurchasingDetails pd inner join Purchasing as p on p.id = pd.PurchasingId WHERE p.RecieveStatus='Issued' AND pd.ItemId = i.Item_id) as QuantityIn,
	(SELECT SUM(CAST(Qty as int)) FROM SalesOrder_Details sd inner join SalesOrder as s on s.id = sd.SalesOrder_id WHERE s.SO_Invoice_Status='Invoiced' AND sd.ItemId = i.Item_id) as QuantityOut

	
	FROM PurchasingDetails as pd
	INNER JOIN Items as i on
	pd.ItemId = i.Item_id
	INNER JOIN purchasing as p
	on p.id = pd.PurchasingId
	WHERE ((@pFrom is null) or (convert(nvarchar, p.Date_Of_Day, 103) >=  convert(nvarchar, @pFrom, 103)))
	AND ((@pTo is null) or (convert(nvarchar, p.Date_Of_Day, 103) <= convert(nvarchar, @pTo, 103)))
	AND ((@pItemName is null) or (i.Item_Name = @pItemName))

END

GO
/****** Object:  StoredProcedure [dbo].[proc_Reports_Inventory_Valuation_Summary]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,> 
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Reports_Inventory_Valuation_Summary] --'3/6/2019','3/6/2019','cocacola'
	
	@pFrom							nvarchar(50) = NULL,
	@pTo							nvarchar(50) = NULL,
	@pItemName						nvarchar(50) = NULL

AS
BEGIN
	SELECT 
		(SELECT Item_id FROM Items WHERE Item_id = s.Item_id) as Item_id,
		(SELECT Item_Name FROM Items WHERE Item_id = s.Item_id) as ItemName,
		(SELECT Item_Sku FROM Items WHERE Item_id = s.Item_id) as SKU,
		Accounting_Quantity as StockInHand,
		(SELECT cast(Item_Purchase_Price as int) FROM Items WHERE Item_id = s.Item_id) as PurchasePrice,
		(CAST(Accounting_Quantity as int) * (SELECT cast(Item_Purchase_Price as int) FROM Items WHERE Item_id = s.Item_id)) as InventoryAssetValue
		
	FROM Stock as s
	WHERE ((@pItemName is null) or ((SELECT Item_Name FROM Items WHERE Item_id = s.Item_id) = @pItemName))
	AND s.Accounting_Quantity > 0
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Reports_Invoice_History]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,> 
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Reports_Invoice_History] --'01/01/2019','31/12/2019',null,'overdue',null
	
	@pFrom								nvarchar(50) = NULL,
	@pTo								nvarchar(50) = NULL,
	@pInvoice_Status					nvarchar(50) = NULL,
	@pOverdue							nvarchar(50) = NULL,
	@pPartiallyPaid						nvarchar(50) = NULL

AS
BEGIN
	SELECT
	(case when convert(datetime, SYSDATETIME(), 103) > convert(datetime,InvoiceDueDate,103) then 'Overdue' 
	when Balance_Amount > '0' and Balance_Amount < Invoice_Amount then 'Partially Paid' else Invoice_Status end) as Invoice_Status, 
	InvoiceDateTime,
	InvoiceDueDate,
	Invoice_No,
	(SELECT SaleOrderNo FROM SalesOrder WHERE id = so.SalesOrder_id) as SalesOrderNo,
	(SELECT DISTINCT Customer_id FROM SalesOrder_Details WHERE SalesOrder_id = so.SalesOrder_id) as Customer_id,
	(SELECT Name FROM Contacts WHERE id = (SELECT DISTINCT Customer_id FROM SalesOrder_Details WHERE SalesOrder_id = so.SalesOrder_id)) as CustomerName,
	Invoice_Amount,
	Balance_Amount

	FROM SalesOrder_Invoices as so
	WHERE ((@pFrom is null) or (convert(nvarchar, InvoiceDateTime, 103)>= convert(nvarchar, @pFrom, 103)))
	AND ((@pTo is null) or (convert(nvarchar, InvoiceDateTime, 103) <= convert(nvarchar, @pTo, 103)))
	AND ((@pInvoice_Status is null) or (Invoice_Status = @pInvoice_Status))
	AND ((@pOverdue is null) or ((select convert(datetime,InvoiceDueDate,103)) < (select convert(datetime, SYSDATETIME(), 103))))
	AND ((@pPartiallyPaid is null) or (Balance_Amount>'0' and Balance_Amount < Invoice_Amount))

END

GO
/****** Object:  StoredProcedure [dbo].[proc_Reports_Order_Fulfillment_By_Item]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,> 
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Reports_Order_Fulfillment_By_Item] --'01/01/2019','31/12/2019','Confirm',null,false
	
	@pFrom								nvarchar(50) = NULL,
	@pTo								nvarchar(50) = NULL

AS
BEGIN
	SELECT
		(SELECT Item_id FROM Items WHERE Item_id = sd.ItemId) as Item_id,
		(SELECT Item_Name FROM Items WHERE Item_id = sd.ItemId) as Item_Name,
		(SELECT Item_Sku FROM Items WHERE Item_id = sd.ItemId) as Item_Sku,
		SUM(CAST(sd.Qty as int)) as Ordered,
		(SELECT SUM(CAST(Packed_Item_Qty as int)) FROM SO_Packages_Detail WHERE Item_id = i.Item_id) as Packed,
		(SELECT SUM(CAST(Packed_Item_Qty as int)) FROM SO_Packages_Detail WHERE Item_id = i.Item_id AND sh.Shipment_Status='Not Delivered') as DropShipped,
		(SELECT SUM(CAST(Qty as int)) FROM SalesOrder_Details as sd LEFT OUTER JOIN SalesOrder as s on s.id = sd.SalesOrder_id WHERE Itemid = i.Item_id AND s.SO_Status='Closed') as Fulfilled,
		(SELECT SUM(CAST(Packed_Item_Qty as int)) FROM SO_Packages_Detail WHERE Item_id = i.Item_id AND sh.Shipment_Status='Shipped') as Shipped,
		(SUM(CAST(sd.Qty as int))-(SELECT SUM(CAST(Packed_Item_Qty as int)) FROM SO_Packages_Detail WHERE Item_id = i.Item_id)) ToBePacked,
		(SELECT case when (SELECT SUM(CAST(Packed_Item_Qty as int)) FROM SO_Packages_Detail WHERE Item_id = i.Item_id AND sh.Shipment_Status='Shipped') is null then (SELECT SUM(CAST(Packed_Item_Qty as int)) FROM SO_Packages_Detail WHERE Item_id = i.Item_id) else ((SELECT SUM(CAST(Packed_Item_Qty as int)) FROM SO_Packages_Detail WHERE Item_id = i.Item_id) - (SELECT SUM(CAST(Packed_Item_Qty as int)) FROM SO_Packages_Detail WHERE Item_id = i.Item_id AND sh.Shipment_Status='Shipped')) end) as ToBeShipped,
		(SELECT case when (SELECT SUM(CAST(Packed_Item_Qty as int)) FROM SO_Packages_Detail WHERE Item_id = i.Item_id AND sh.Shipment_Status!='Delivered') is null then (SELECT SUM(CAST(Packed_Item_Qty as int)) FROM SO_Packages_Detail WHERE Item_id = i.Item_id) else ((SELECT SUM(CAST(Packed_Item_Qty as int)) FROM SO_Packages_Detail WHERE Item_id = i.Item_id) - (SELECT SUM(CAST(Packed_Item_Qty as int)) FROM SO_Packages_Detail WHERE Item_id = i.Item_id AND sh.Shipment_Status!='Delivered')) end) as ToBeDelivered


	FROM Items as i
	INNER JOIN SalesOrder_Details as sd on i.Item_Id = sd.Itemid
	LEFT OUTER JOIN SO_Packages_Detail as pd on i.Item_id = pd.Item_id
	LEFT OUTER JOIN SO_Packages as p on pd.Package_No = p.Package_No
	LEFT OUTER JOIN ShipmentPackages as sp on p.Package_id = sp.Package_id
	LEFT OUTER JOIN Shipment as sh on sp.Shipment_No = sh.Shipment_No
	LEFT OUTER JOIN SalesOrder_Invoices as si on si.SalesOrder_id = sd.SalesOrder_id
	WHERE ((@pFrom is null) or (SELECT convert(nvarchar, Date_Of_Day, 103) FROM SalesOrder WHERE id = sd.SalesOrder_id) >=  convert(nvarchar, @pFrom, 103))
	AND ((@pTo is null) or (SELECT convert(nvarchar, Date_Of_Day, 103) FROM SalesOrder WHERE id = sd.SalesOrder_id) <= convert(nvarchar, @pTo, 103))
	GROUP BY sd.ItemId, i.Item_id,sh.Shipment_Status

END

GO
/****** Object:  StoredProcedure [dbo].[proc_Reports_Packing_History]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,> 
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Reports_Packing_History] --'01/01/2019','31/12/2019',null
	
	@pFrom								nvarchar(50) = NULL,
	@pTo								nvarchar(50) = NULL,
	@pPackage_Status					nvarchar(50) = NULL


AS
BEGIN
	SELECT
	Date_Of_Day,
	Package_No,
	(SELECT SaleOrderNo FROM SalesOrder WHERE id = p.SaleOrder_id) as SaleOrderNo,
	Package_Status,
	(SELECT SUM(CAST(Packed_Item_Qty as int)) FROM SO_Packages_Detail WHERE Package_No = p.Package_No) as Quantity
	
	FROM SO_Packages as p
	WHERE ((@pFrom is null) or (convert(nvarchar, Date_Of_Day, 103) >= convert(nvarchar, @pFrom, 103)))
	AND ((@pTo is null) or (convert(nvarchar, Date_Of_Day, 103) <= convert(nvarchar, @pTo, 103)))
	AND ((@pPackage_Status is null) or (Package_Status = @pPackage_Status))


END

GO
/****** Object:  StoredProcedure [dbo].[proc_Reports_Payment_Made]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,> 
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Reports_Payment_Made] --'1/1/2019','31/12/2019','Issued'
	
	--@pFrom							nvarchar(50) = NULL,
	--@pTo							nvarchar(50) = NULL

AS
BEGIN
SELECT 
	b.BillDateTime,
	b.Bill_No,
	pd.VendorId,
	(SELECT Name FROM Contacts WHERE id = pd.VendorId) as VendorName,
	p.Payment_Mode,
	(SELECT Payment_Mode FROM PaymentMode WHERE id = p.Payment_Mode) as PaymentMethod,
	SUM(CAST(p.Paid_Amount as decimal(10,2))) as Amount 


	FROM Payments as p
	INNER JOIN Bills as b ON p.Bill_id = b.Bill_id
	LEFT JOIN PurchasingDetails as pd ON b.Purchase_id = pd.PurchasingId
	--WHERE ((@pFrom is null) or (convert(nvarchar, @pFrom, 103) <= b.BillDateTime))
	--AND ((@pTo is null) or (convert(nvarchar, @pTo, 103) >= b.BillDateTime))
	GROUP BY b.Bill_id,b.BillDateTime,b.Bill_No,pd.VendorId,p.Payment_Mode

END

GO
/****** Object:  StoredProcedure [dbo].[proc_Reports_Payments_Received]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,> 
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Reports_Payments_Received] --'01/01/2019','31/12/2019'
	
	@pFrom								nvarchar(50) = NULL,
	@pTo								nvarchar(50) = NULL


AS
BEGIN
	SELECT DISTINCT
	(SELECT TOP 1 SO_Payment_id FROM SO_Payments WHERE SO_Invoice_id = si.id) as PaymentNo,
	(SELECT TOP 1 SO_Payment_Date FROM SO_Payments WHERE SO_Invoice_id = si.id) as SO_Payment_Date,
	si.Invoice_No,
	(SELECT Name FROM Contacts WHERE id = (SELECT DISTINCT Customer_id FROM SalesOrder_Details WHERE SalesOrder_id = si.SalesOrder_id)) as CustomerName,
	si.Invoice_Amount,
	(SELECT Payment_Mode FROM PaymentMode WHERE id = (SELECT TOP 1 SO_Payment_Mode FROM SO_Payments WHERE SO_Invoice_id = si.id)) as Payment_Mode

	
	FROM SalesOrder_Invoices as si
	INNER JOIN SO_Payments as p on si.id = p.SO_Invoice_id
	WHERE ((@pFrom is null) or (convert(nvarchar, SO_Payment_Date, 103) >= convert(nvarchar, @pFrom, 103)))
	AND ((@pTo is null) or (convert(nvarchar, SO_Payment_Date, 103) <= convert(nvarchar, @pTo, 103)))


END

GO
/****** Object:  StoredProcedure [dbo].[proc_Reports_Product_Sales_Report]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,> 
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Reports_Product_Sales_Report] --'01/02/2019', '03/07/2019'
	
	@pFrom					nvarchar(50) = NULL,
	@pTo					nvarchar(50) = NULL

AS
BEGIN
	SELECT 
	sd.ItemId as Item_id,
	(SELECT Item_Name FROM Items WHERE Item_id = sd.ItemId) as ItemName,
	(SELECT Item_Sku FROM Items WHERE Item_id = sd.ItemId) as SKU,
	SUM(CAST(sd.Qty as int)) as QuantitySold,
	SUM(CAST(sd.Qty as int) * CAST(sd.PriceUnit as int)) as TotalSalePrice
	
	FROM SalesOrder_Details as sd
	WHERE ((@pFrom is null) or ((SELECT convert(datetime, Date_Of_Day, 103) FROM SalesOrder WHERE id = sd.SalesOrder_id) >= convert(datetime, @pFrom, 103)))
	AND ((@pTo is null) or ((SELECT convert(datetime, Date_Of_Day, 103) FROM SalesOrder WHERE id = sd.SalesOrder_id) <= convert(datetime, @pTo, 103)))
	GROUP BY sd.ItemId
	
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Reports_Purchase_By_Item]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,> 
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Reports_Purchase_By_Item] --'1/1/2019','31/12/2019','Issued'
	
	@pFrom							nvarchar(50) = NULL,
	@pTo							nvarchar(50) = NULL,
	@pRecieveStatus					nvarchar(50) = NULL

AS
BEGIN
SELECT 
	ItemId as Item_id,
	(SELECT Item_Name FROM Items WHERE Item_id = pd.ItemId) as ItemName,
	SUM(CAST(Qty as int)) as QuantityPurchased,
	SUM(CAST(pd.Qty as int) * CAST(pd.PriceUnit as int)) as Amount,
	(SUM(CAST(pd.Qty as int) * CAST(pd.PriceUnit as int)))/(SUM(CAST(pd.Qty as int))) as AveragePrice

	FROM PurchasingDetails as pd
	WHERE ((@pFrom is null) or (convert(datetime, @pFrom, 103) <= (SELECT convert(datetime, Date_Of_Day, 103) FROM Purchasing WHERE id = pd.PurchasingId)))
	AND ((@pTo is null) or (convert(datetime, @pTo, 103) >= (SELECT convert(datetime, Date_Of_Day, 103) FROM Purchasing WHERE id = pd.PurchasingId)))
	--AND ((@pRecieveStatus is null) or (@pRecieveStatus = RecieveStatus))
	GROUP BY pd.ItemId

END

GO
/****** Object:  StoredProcedure [dbo].[proc_Reports_Purchase_By_Vendor]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,> 
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Reports_Purchase_By_Vendor] --'5/7/2019','5/7/2019'
	
	@pFrom							nvarchar(50) = NULL,
	@pTo							nvarchar(50) = NULL

AS
BEGIN
SELECT 
	VendorId,
	(SELECT Name FROM Contacts WHERE id = pd.VendorId) as VendorName,
	SUM(CAST(Qty as int)) as QuantityOrdered,
	--SUM(CAST(pd.Qty as int)) as QuantityReceived
	(SUM(CAST(Qty as int) * CAST(PriceUnit as int))) as Amount

	FROM PurchasingDetails as pd
	WHERE ((@pFrom is null) or convert(datetime, @pFrom, 103)<=	(SELECT (convert(datetime, Date_Of_Day, 103)) FROM Purchasing WHERE id = pd.PurchasingId))
	AND ((@pTo is null) or convert(datetime, @pTo, 103) >= (SELECT (convert(datetime, Date_Of_Day, 103)) FROM Purchasing WHERE id = pd.PurchasingId))
	GROUP BY pd.VendorId
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Reports_Purchase_Order_History]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,> 
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Reports_Purchase_Order_History] --'1/1/2019','31/12/2019','Issued'
	
	@pFrom							nvarchar(50) = NULL,
	@pTo							nvarchar(50) = NULL,
	@pRecieveStatus					nvarchar(50) = NULL

AS
BEGIN
SELECT 
	Date_Of_Day,
	TempOrderNum as PurchaseOrderNo,
	(SELECT DISTINCT VendorId FROM PurchasingDetails WHERE PurchasingId = p.id) as Vendor_id,
	(SELECT Name FROM Contacts WHERE id = (SELECT DISTINCT VendorId FROM PurchasingDetails WHERE PurchasingId = p.id)) as VendorName,
	RecieveStatus,
	(SELECT SUM(CAST(Qty as int)) FROM PurchasingDetails WHERE PurchasingId = p.id) as QuantityOrdered,
	(SELECT SUM(CAST(Qty as int)) FROM PurchasingDetails WHERE PurchasingId = p.id AND p.RecieveStatus = 'Issued') as QuantityReceived,
	(SELECT SUM(CAST(Qty as int) * CAST(PriceUnit as int)) FROM PurchasingDetails WHERE PurchasingId = p.id) as Amount

	FROM Purchasing as p
	WHERE ((@pFrom is null) or convert(datetime, @pFrom, 103)<= (convert(datetime, p.Date_Of_Day, 103)))
	AND ((@pTo is null) or convert(datetime, @pTo, 103) >= (convert(datetime, p.Date_Of_Day, 103)))
	AND ((@pRecieveStatus is null) or (@pRecieveStatus = RecieveStatus))

END

GO
/****** Object:  StoredProcedure [dbo].[proc_Reports_Sales_By_Customer]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,> 
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Reports_Sales_By_Customer] --'30/6/2019','6/7/2019',null
	
	@pFrom								nvarchar(50) = NULL,
	@pTo								nvarchar(50) = NULL,
	@pCustomerName						nvarchar(50) = NULL


AS
BEGIN
	SELECT 
	si.Customer_id as Customer_id,
	(SELECT Name FROM Contacts WHERE id = si.Customer_id) as CustomerName,
	COUNT(id) as InvoiceCount,
	SUM(CAST(si.Invoice_Amount as int)) as Sales
	--(SELECT SO_Total_Amount FROM SalesOrder WHERE id = si.SalesOrder_id)
	
	FROM SalesOrder_Invoices as si
	--INNER JOIN SalesOrder AS S ON s.id = SalesOrder_id
	WHERE ((@pFrom is null) or (convert(datetime, InvoiceDateTime, 103) >= convert(datetime, @pFrom, 103)))
	AND ((@pTo is null) or (convert(datetime, InvoiceDateTime, 103) <= convert(datetime, @pTo, 103)))
	AND ((@pCustomerName is null) or (@pCustomerName = (SELECT Name FROM Contacts WHERE id = si.Customer_id)))
	GROUP BY si.Customer_id

END

GO
/****** Object:  StoredProcedure [dbo].[proc_Reports_Sales_By_Item]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,> 
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Reports_Sales_By_Item] --'30/6/2019','6/7/2019',null
	
	@pFrom								nvarchar(50) = NULL,
	@pTo								nvarchar(50) = NULL,
	@pItemName							nvarchar(50) = NULL


AS
BEGIN
	SELECT 
	sd.ItemId as Item_id,
	(SELECT Item_Name FROM Items WHERE Item_id = sd.ItemId) as ItemName,
	SUM(CAST(sd.Qty as int)) as QuantitySold,
	SUM(CAST(sd.Qty as int) * CAST(sd.PriceUnit as int)) as Amount,
	(SUM(CAST(sd.Qty as int) * CAST(sd.PriceUnit as int)))/(SUM(CAST(sd.Qty as int))) as AveragePrice
	
	FROM SalesOrder_Details as sd
	WHERE ((@pFrom is null) or ((SELECT convert(datetime, Date_Of_Day, 103) FROM SalesOrder WHERE id = sd.SalesOrder_id) >= convert(datetime, @pFrom, 103)))
	AND ((@pTo is null) or ((SELECT convert(datetime, Date_Of_Day, 103) FROM SalesOrder WHERE id = sd.SalesOrder_id) <= convert(datetime, @pTo, 103)))
	AND ((@pItemName is null) or (@pItemName = (SELECT Item_Name FROM Items WHERE Item_id = sd.ItemId)))
	GROUP BY sd.ItemId

END

GO
/****** Object:  StoredProcedure [dbo].[proc_Reports_Sales_By_SalesPerson]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,> 
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Reports_Sales_By_SalesPerson] --'30/6/2019','6/7/2019',null
	
	@pFrom								nvarchar(50) = NULL,
	@pTo								nvarchar(50) = NULL,
	@pUserName							nvarchar(50) = NULL


AS
BEGIN
	SELECT 
	si.AddedBy as AddedBy,
	(SELECT Name FROM Contacts WHERE id = si.AddedBy) as AddedByName,
	COUNT(id) as InvoiceCount,
	SUM(CAST(si.Invoice_Amount as int)) as Sales
	--(SELECT SO_Total_Amount FROM SalesOrder WHERE id = si.SalesOrder_id)
	
	FROM SalesOrder_Invoices as si
	--INNER JOIN SalesOrder AS S ON s.id = SalesOrder_id
	WHERE ((@pFrom is null) or (convert(datetime, InvoiceDateTime, 103) >= convert(datetime, @pFrom, 103)))
	AND ((@pTo is null) or (convert(datetime, InvoiceDateTime, 103) <= convert(datetime, @pTo, 103)))
	AND ((@pUserName is null) or (@pUserName = (SELECT Name FROM Contacts WHERE id = si.AddedBy)))
	GROUP BY si.AddedBy

END

GO
/****** Object:  StoredProcedure [dbo].[proc_Reports_Sales_Order_History]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,> 
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Reports_Sales_Order_History] --'01/07/2019','31/07/2019','Confirm',null,null
	
	@pFrom								nvarchar(50) = NULL,
	@pTo								nvarchar(50) = NULL,
	@pStatus							nvarchar(50) = NULL,
	@pInvoiceStatus						nvarchar(50) = NULL,
	@pShipmentStatus					nvarchar(50) = NULL

AS
BEGIN
	SELECT
		id as SalesOrder_id,
		SaleOrderNo,
		(SELECT DISTINCT Customer_id FROM SalesOrder_Details WHERE SalesOrder_id = s.id) as Customer_id,
		(SELECT Salutation FROM Contacts WHERE id = (SELECT DISTINCT Customer_id FROM SalesOrder_Details WHERE SalesOrder_id = s.id)) as Salutation,
		(SELECT Name FROM Contacts WHERE id = (SELECT DISTINCT Customer_id FROM SalesOrder_Details WHERE SalesOrder_id = s.id)) as CustomerName,
		SO_Total_Amount,
		Date_Of_Day,
		SO_Status
	FROM SalesOrder as s
	WHERE ((@pFrom is null) or (convert(nvarchar, s.Date_Of_Day, 103) >=  convert(nvarchar, @pFrom, 103)))
	AND ((@pTo is null) or (convert(nvarchar, s.Date_Of_Day, 103) <= convert(nvarchar, @pTo, 103)))
	AND ((@pStatus is null) or (s.SO_Status = @pStatus))
	AND ((@pInvoiceStatus is null) or (s.SO_Invoice_Status = @pInvoiceStatus))
	AND ((@pShipmentStatus is null) or (s.SO_Shipment_Status = @pShipmentStatus))

END

GO
/****** Object:  StoredProcedure [dbo].[proc_Reports_Stock_Summary]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,> 
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Reports_Stock_Summary] --'3/6/2019','3/6/2019','cocacola'
	
	@pFrom							nvarchar(50) = NULL,
	@pTo							nvarchar(50) = NULL,
	@pItemName						nvarchar(50) = NULL

AS
BEGIN
	SELECT
		Item_id,
		Item_Name,
		Item_Sku,
		(SELECT SUM(CAST(Qty as int)) FROM PurchasingDetails WHERE ItemId =i.Item_id) as QuantityIn,
		(SELECT SUM(CAST(Qty as int)) FROM SalesOrder_Details WHERE ItemId =i.Item_id) as QuantityOut,
		(SELECT SUM(CAST(Qty as int)) FROM PurchasingDetails WHERE ItemId =i.Item_id) - (SELECT SUM(CAST(Qty as int)) FROM SalesOrder_Details WHERE ItemId =i.Item_id) as ClosingStock
	FROM Items as i
	WHERE ((@pItemName is null) or (Item_Name = @pItemName))

END

GO
/****** Object:  StoredProcedure [dbo].[proc_Role_Privileges]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Role_Privileges] --2

	@pRoleID 			BIGINT
AS
	BEGIN
	SET NOCOUNT ON;
	 

		SELECT PR.Role_Priv_id,P.Priv_id, P.Priv_Name,  1 as Check_Status
			FROM Privileges P
					inner join RolePrivileges PR
					on P.Priv_id = PR.Priv_id
				WHERE P.Enable IN (1) 
				and PR.Role_id = @pRoleID 
				AND PR.Enable = 1
		UNION
		SELECT PR.Role_Priv_id,P.Priv_id, P.Priv_Name,  0 as Check_Status
			FROM Privileges P
					inner join RolePrivileges PR
					on P.Priv_id = PR.Priv_id
				WHERE PR.Enable IN (0) 
				and PR.Role_id = @pRoleID 
				AND P.Enable = 1
		UNION
		SELECT 0 AS Role_Priv_id,P.Priv_id, P.Priv_Name,  0 as Check_Status
			FROM Privileges P
				WHERE P.Enable IN (1) AND P.Priv_id NOT IN (
					SELECT PR.Priv_id 
						FROM RolePrivileges PR
							WHERE PR.Role_id = @pRoleID)
			order by P.Priv_id asc
		
	END

GO
/****** Object:  StoredProcedure [dbo].[proc_SalesActivity]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,> 
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_SalesActivity] 
	

AS
BEGIN
	SELECT
	(SELECT SUM(cast(s.Qty as int))-SUM(cast(Packed_Item_Qty as int)) FROM SO_Packages_Detail) as ToBePacked,
	(SELECT COUNT(Package_id) FROM SO_Packages WHERE Package_Status = 'Not Shipped') as ToBeShipped,
	(SELECT COUNT(Shipment_id) FROM Shipment WHERE Shipment_Status='Shipped') as ToBeDelivered,
	(SELECT SUM(cast(Qty as int)) FROM SalesOrder_Invoices i inner join SalesOrder_Details as sd on i.SalesOrder_id = sd.SalesOrder_id) as ToBeInvoiced
	


	FROM SalesOrder_Details as s
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_Activitytype]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_Activitytype] --'Brand 5'
	
	@pActivityType				nvarchar(50) = NULL

AS
BEGIN
	SELECT 
	id,
	ActivityType
	FROM ActivityType
	WHERE ActivityType = @pActivityType
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_BillItems_By_PO_ID]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_BillItems_By_PO_ID] --23

	@pPurchasingId				nvarchar(50)
AS
BEGIN
	SELECT 
		pd_id,
		PurchasingId as id,
		ItemId,
		VendorId,
		Qty,
		PriceUnit,
		MsrmntUnit

	FROM PurchasingDetails AS p
	WHERE p.PurchasingId=@pPurchasingId
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_Bills]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_Bills] --'',null,null,2

	@pBill_No					nvarchar(50) = NULL,
	@pFrom						nvarchar(50) = NULL,
	@pTo						nvarchar(50) = NULL,
	@pAddedBy					int

AS
BEGIN
	SELECT 
		Bill_id,
		Purchase_id,
		Bill_No,
		(SELECT TempOrderNum FROM Purchasing WHERE id=b.Purchase_id) as OrderNo,
		Bill_Status,
		Bill_Amount as Total_Amount,
		(SELECT TOP 1 Paid_Amount FROM Payments WHERE Bill_id=b.Bill_id ORDER BY payment_id desc) as Amount_Paid,
		(SELECT TOP 1 Balance_Amount FROM Payments WHERE Bill_id=b.Bill_id ORDER BY payment_id desc) as Balance_Amount,
		Enable,
		BillDateTime,
		BillDueDate,
		Time_Of_Day,
		Date_Of_Day,
		Month_Of_Day,
		Year_Of_Day,
		(SELECT id FROM Purchasing WHERE id=b.Purchase_id) as Purchase_id,
		(SELECT TempOrderNum FROM Purchasing WHERE id=b.Purchase_id) as OrderNo

	FROM Bills AS b
	WHERE ((@pBill_No is null) or Bill_No LIKE '%' + @pBill_No + '%')
	AND ((@pFrom is null) or (convert(datetime, b.Date_Of_Day, 103) >= convert(datetime, @pFrom, 103)))
	AND ((@pTo is null) or (convert(datetime, b.Date_Of_Day, 103) <= convert(datetime, @pTo, 103)))
	AND (b.AddedBy = @pAddedBy)
	ORDER BY b.Bill_No desc
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_Bills_By_CustomerId]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_Bills_By_CustomerId] --26

	@pCustomer_id					int,
	@pSearch						nvarchar(50) = NULL
	
	
AS
BEGIN
	SELECT DISTINCT
	(SELECT id FROM SalesOrder_Invoices WHERE SalesOrder_id = s.SalesOrder_id) as Invoice_id,
	SalesOrder_id,
	(SELECT Invoice_No FROM SalesOrder_Invoices WHERE SalesOrder_id = s.SalesOrder_id) as Invoice_No,
	(SELECT Invoice_Status FROM SalesOrder_Invoices WHERE SalesOrder_id = s.SalesOrder_id) as Invoice_Status,
	(SELECT Invoice_Amount FROM SalesOrder_Invoices WHERE SalesOrder_id = s.SalesOrder_id) as Invoice_Amount,
	(SELECT Balance_Amount FROM SalesOrder_Invoices WHERE SalesOrder_id = s.SalesOrder_id) as Balance_Amount,
	(SELECT Date_Of_Day FROM SalesOrder_Invoices WHERE SalesOrder_id = s.SalesOrder_id) as Date_Of_Day,
	(SELECT Time_Of_Day FROM SalesOrder_Invoices WHERE SalesOrder_id = s.SalesOrder_id) as Time_Of_Day,
	(SELECT InvoiceDueDate FROM SalesOrder_Invoices WHERE SalesOrder_id = s.SalesOrder_id) as InvoiceDueDate


	FROM SalesOrder_Details as s
	
	WHERE ((@pSearch is null) or (SELECT Invoice_No FROM SalesOrder_Invoices WHERE SalesOrder_id = s.SalesOrder_id) LIKE '%' + @pSearch + '%' or (SELECT Date_Of_Day FROM SalesOrder_Invoices WHERE SalesOrder_id = s.SalesOrder_id) LIKE '%' + @pSearch + '%' or (SELECT Time_Of_Day FROM SalesOrder_Invoices WHERE SalesOrder_id = s.SalesOrder_id) LIKE '%' + @pSearch + '%')
	AND (s.Customer_id = @pCustomer_id)

	--ORDER BY pd.pd_id DESC
END



GO
/****** Object:  StoredProcedure [dbo].[proc_Select_Bills_By_ID]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_Bills_By_ID] --11

	@pBill_id				nvarchar(50)
AS
BEGIN
	SELECT 
		Bill_id,
		Bill_No,
		Bill_Status,
		Bill_Amount as Total_Amount,
		(SELECT TOP 1 Paid_Amount FROM Payments WHERE Bill_id=b.Bill_id ORDER BY payment_id desc) as Amount_Paid,
		(SELECT TOP 1 Balance_Amount FROM Payments WHERE Bill_id=b.Bill_id ORDER BY payment_id desc) as Balance_Amount,
		(SELECT COUNT(Payment_id) FROM Payments WHERE Bill_id=b.Bill_id) as paymentcount,
		BillDateTime,
		BillDueDate,
		Time_Of_Day,
		Date_Of_Day,
		Month_Of_Day,
		Year_Of_Day,
		(SELECT id FROM Purchasing WHERE id=b.Purchase_id) as Purchase_id,
		(SELECT TempOrderNum FROM Purchasing WHERE id=b.Purchase_id) as OrderNo

	FROM Bills AS b
	WHERE b.Bill_id=@pBill_id
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_Bills_By_ItemID]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_Bills_By_ItemID] --1
	
	@pItem_id				int = 0,
	@pSearch				nvarchar(50) = NULL

AS
BEGIN
	SELECT 
		b.Bill_id as Bill_id,
		b.Bill_No,
		pd.VendorId as Vendor_id,
		(SELECT Name FROM Contacts WHERE id = pd.VendorId) as VendorName,
		pd.PriceUnit as Price,
		pd.Qty as QtyPurchased,
		b.Bill_Status,
		b.BillDateTime
	FROM PurchasingDetails AS pd
	INNER JOIN Purchasing as p on pd.PurchasingId = p.id
	INNER JOIN Bills as b on p.id = b.Purchase_id
	WHERE ((@pSearch is null) or b.Bill_No LIKE '%' + @pSearch + '%' or (SELECT Name FROM Contacts WHERE id = pd.VendorId) LIKE '%' + @pSearch + '%')
	AND pd.ItemId = @pItem_id
	ORDER BY p.id desc
	
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_Bills_By_PID]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_Bills_By_PID] --5

	@pPurchase_id				nvarchar(50) 
AS
BEGIN
	SELECT 
		Bill_id,
		Bill_No,
		Bill_Status,
		(SELECT id FROM Purchasing WHERE id=b.Purchase_id) as Purchase_id,
		(SELECT TOP 1 Total_Amount FROM Payments WHERE Bill_id=b.Bill_id ORDER BY payment_id desc) as Total_Amount,
		(SELECT TOP 1 Paid_Amount FROM Payments WHERE Bill_id=b.Bill_id ORDER BY payment_id desc) as Amount_Paid,
		(SELECT TOP 1 Balance_Amount FROM Payments WHERE Bill_id=b.Bill_id ORDER BY payment_id desc) as Balance_Amount,
		Enable,
		BillDateTime,
		BillDueDate,
		Time_Of_Day,
		Date_Of_Day,
		Month_Of_Day,
		Year_Of_Day,
		(SELECT id FROM Purchasing WHERE id=b.Purchase_id) as Purchase_id,
		(SELECT TempOrderNum FROM Purchasing WHERE id=b.Purchase_id) as OrderNo,
		(SELECT Delete_Request_By FROM Purchasing WHERE id=b.Purchase_id) as Delete_Request_By,
		(SELECT Delete_Status FROM Purchasing WHERE id=b.Purchase_id) as Delete_Status,
		(SELECT Enable FROM Purchasing WHERE id=b.Purchase_id) as Enable

	FROM Bills AS b
	WHERE Purchase_id = @pPurchase_id
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_Bills_By_VendorId]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_Bills_By_VendorId] --26

	@pVendorId						int,
	@pSearch						nvarchar(50) = NULL
	
	
AS
BEGIN
	SELECT DISTINCT
	(SELECT Bill_id FROM Bills WHERE Purchase_id = p.PurchasingId) as Bill_id,
	PurchasingId,
	(SELECT Bill_No FROM Bills WHERE Purchase_id = p.PurchasingId) as Bill_No,
	(SELECT Bill_Status FROM Bills WHERE Purchase_id = p.PurchasingId) as Bill_Status,
	(SELECT Bill_Amount FROM Bills WHERE Purchase_id = p.PurchasingId) as Bill_Amount,
	(SELECT Balance_Amount FROM Bills WHERE Purchase_id = p.PurchasingId) as Balance_Amount,
	(SELECT Date_Of_Day FROM Bills WHERE Purchase_id = p.PurchasingId) as Date_Of_Day,
	(SELECT Time_Of_Day FROM Bills WHERE Purchase_id = p.PurchasingId) as Time_Of_Day,
	(SELECT BillDueDate FROM Bills WHERE Purchase_id = p.PurchasingId) as BillDueDate


	FROM PurchasingDetails as p
	
	WHERE ((@pSearch is null) or (SELECT Bill_No FROM Bills WHERE Purchase_id = p.PurchasingId) LIKE '%' + @pSearch + '%' or (SELECT Date_Of_Day FROM Bills WHERE Purchase_id = p.PurchasingId) LIKE '%' + @pSearch + '%' or (SELECT Time_Of_Day FROM Bills WHERE Purchase_id = p.PurchasingId) LIKE '%' + @pSearch + '%')
	AND (p.VendorId = @pVendorId)

	--ORDER BY pd.pd_id DESC
END



GO
/****** Object:  StoredProcedure [dbo].[proc_Select_Brand_By_Name]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_Brand_By_Name] --'',null,null,null

	@pBrand_Name						nvarchar(50) = NULL
		
AS
BEGIN
	SELECT
	Brand_id,
	Brand_Name,
	Brand_Code,
	Enable,
	AddedBy,
	UpdatedBy,
	Delete_Request_By,
	Delete_Status,
	Time_Of_Day,
	Date_Of_Day,
	Month_Of_Day,
	Year_Of_Day

	FROM Brands as b
	WHERE Brand_Name = @pBrand_Name
END



GO
/****** Object:  StoredProcedure [dbo].[proc_Select_Brands]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_Brands] --'','01/10/2018','16/08/2019',null
	
	@pBrand_Name				nvarchar(50) = NULL,
	@pFrom						nvarchar(50) = NULL,
	@pTo						nvarchar(50) = NULL,
	@pEnable					int = NULL 


AS
BEGIN
	SELECT 
	Brand_id,
	Brand_Name,
	Brand_Code,
	Enable,
	Delete_Request_By,
	Delete_Status,
	Time_Of_Day,
	Date_Of_Day,
	Month_Of_Day,
	Year_Of_Day
	FROM Brands as b
	WHERE ((@pBrand_Name is null) or Brand_Name LIKE '%' + @pBrand_Name + '%')
	AND ((@pFrom is null) or (convert(datetime, b.Date_Of_Day, 103) >= convert(datetime, @pFrom, 103)))
	AND ((@pTo is null) or (convert(datetime, b.Date_Of_Day, 103) <= convert(datetime, @pTo, 103)))
	AND ((@pEnable is null) or (Enable = @pEnable))
	ORDER BY Brand_id desc
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_Brands_By_ID]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_Brands_By_ID] --4

	@pBrand_id			nvarchar(50)
	
AS
BEGIN
	SELECT 
	Brand_id,
	Brand_Name,
	Brand_Code,
	Enable,
	Delete_Request_By,
	Delete_Status,
	Time_Of_Day,
	Date_Of_Day,
	Month_Of_Day,
	Year_Of_Day
	FROM Brands AS i
	WHERE Brand_id = @pBrand_id
	ORDER BY Brand_id desc

END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_CalenderEvents_UserID]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_CalenderEvents_UserID] --2
	
	@pUser_id				int = 0

AS
BEGIN
	SELECT 
	id,
	Event_id,
	(SELECT EventName FROM Events WHERE id = c.Event_id) as title,
	(SELECT BackgroundColour FROM Events WHERE id = c.Event_id) as backgroundColor,
	(SELECT BorderColour FROM Events WHERE id = c.Event_id) as borderColor,
	Event_Start_Date as start,
	StartedBy,
	Time_Of_Day,
	Date_Of_Day,
	Month_Of_Day,
	Year_Of_Day
	FROM CalenderEvents as c
	WHERE StartedBy = @pUser_id
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_Categories]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_Categories] --'wweew',null,null
	
	@pCategory_Name				nvarchar(50) = NULL,
	@pFrom						nvarchar(50) = NULL,
	@pTo						nvarchar(50) = NULL,
	@pEnable					int = NULL 

AS
BEGIN
	SELECT 
	Category_id,
	Category_Name,
	Category_Code,
	Enable,
	Delete_Request_By,
	Delete_Status,
	Time_Of_Day,
	Date_Of_Day,
	Month_Of_Day,
	Year_Of_Day
	FROM Categories as c
	WHERE ((@pCategory_Name is null) or Category_Name LIKE '%' + @pCategory_Name + '%')
	AND ((@pFrom is null) or (convert(datetime, c.Date_Of_Day, 103) >= convert(datetime, @pFrom, 103)))
	AND ((@pTo is null) or (convert(datetime, c.Date_Of_Day, 103) <= convert(datetime, @pTo, 103)))
	AND ((@pEnable is null) or (Enable = @pEnable))
	ORDER BY Category_id desc
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_Categories_By_ID]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_Categories_By_ID] 

	@pCategory_id			nvarchar(50)
	
AS
BEGIN
	SELECT 
	Category_id,
	Category_Name,
	Category_Code,
	Enable,
	Delete_Request_By,
	Delete_Status,
	Time_Of_Day,
	Date_Of_Day,
	Month_Of_Day,
	Year_Of_Day
	FROM Categories AS i

	WHERE Category_id = @pCategory_id 
	ORDER BY Category_id desc

END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_Category_By_Name]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_Category_By_Name] --'',null,null,null

	@pCategory_Name						nvarchar(50) = NULL
		
AS
BEGIN
	SELECT
	Category_id,
	Category_Name,
	Category_Code,
	Enable,
	AddedBy,
	UpdatedBy,
	Delete_Request_By,
	Delete_Status,
	Time_Of_Day,
	Date_Of_Day,
	Month_Of_Day,
	Year_Of_Day

	FROM  Categories as c
	WHERE Category_Name = @pCategory_Name
END



GO
/****** Object:  StoredProcedure [dbo].[proc_Select_Cities]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_Cities] --'',null,null,null

	@pName						nvarchar(50) = NULL,
	@pFrom						nvarchar(50) = NULL,
	@pTo						nvarchar(50) = NULL,
	@pEnable					int = NULL
	
AS
BEGIN
	SELECT
	id,
	Name,
	Country,
	Enable,
	AddedBy,
	UpdatedBy,
	Delete_Request_By,
	Delete_Status,
	TimeOfDay,
	DateOfDay,
	MonthOfDay,
	YearOfDay

	FROM  Cities as c
	WHERE ((@pName is null) or Name LIKE '%' + @pName + '%')
	AND ((@pFrom is null) or (convert(datetime, c.DateOfDay, 103) >= convert(datetime, @pFrom, 103)))
	AND ((@pTo is null) or (convert(datetime, c.DateOfDay, 103) <= convert(datetime, @pTo, 103)))
	AND ((@pEnable is null) or (Enable = @pEnable))

	ORDER BY c.id DESC
END



GO
/****** Object:  StoredProcedure [dbo].[proc_Select_Companies]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_Companies] --'',null,null,null

	@pName						nvarchar(50) = NULL,
	@pFrom						nvarchar(50) = NULL,
	@pTo						nvarchar(50) = NULL,
	@pid						int = NULL,
	@pEnable					int = NULL 
	
AS
BEGIN
	SELECT
	id,
	Name,
	Landline,
	Mobile,
	Email,
	Website,
	Address,
	City,
	Country,
	BankAccountNumber,
	PaymentMethod,
	Enable,
	AddedBy,
	UpdatedBy,
	Delete_Request_By,
	Delete_Status,
	TimeOfDay,
	DateOfDay,
	MonthOfDay,
	YearOfDay

	FROM  Companies as c
	WHERE ((@pName is null) or Name LIKE '%' + @pName + '%')
	AND ((@pFrom is null) or (convert(datetime, c.DateOfDay, 103) >= convert(datetime, @pFrom, 103)))
	AND ((@pTo is null) or (convert(datetime, c.DateOfDay, 103) <= convert(datetime, @pTo, 103)))
	AND ((@pEnable is null) or (Enable = @pEnable))
	AND ((@pid is null) or (c.id = @pid))

	ORDER BY c.id DESC
END



GO
/****** Object:  StoredProcedure [dbo].[proc_Select_ContactByID]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_ContactByID] --'',null,null,2

	@pid							int,
	@pVendor						nvarchar(50) = NULL,
	@pCustomer						nvarchar(50) = NULL,
	@pEmployee						int = NULL 

AS
BEGIN
	SELECT 
	id,
	Salutation,
	Image,
	Name,
	Company,
	Designation,
	Landline,
	Mobile,
	Email,
	Website,
	Address,
	AddressLandline,
	City,
	Country,
	BankAccountNumber,
	PaymentMethod,
	Vendor,
	Customer,
	Employee,
	Enable,
	AddedBy,
	UpdatedBy,
	Delete_Request_By,
	Delete_Status,
	TimeOfDay,
	DateOfDay,
	MonthOfDay,
	YearOfDay

	FROM Contacts AS c
	WHERE id = @pid
	AND Vendor = @pVendor
	AND Customer = @pCustomer
	AND Employee = @pEmployee

END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_Countries]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_Countries] --'','26/07/2019','26/07/2019'

	@pName						nvarchar(50) = NULL,
	@pFrom						nvarchar(50) = NULL,
	@pTo						nvarchar(50) = NULL,
	@pEnable					int = NULL 

AS
BEGIN
	SELECT 
	id,
	Name,
	Enable,
	AddedBy,
	UpdatedBy,
	Delete_Request_By,
	Delete_Status,
	TimeOfDay,
	DateOfDay,
	MonthOfDay,
	YearOfDay

	FROM Countries AS c
	WHERE ((@pName is null) or Name LIKE '%' + @pName + '%')
	AND ((@pFrom is null) or (convert(datetime, c.DateOfDay, 103) >= convert(datetime, @pFrom, 103)))
	AND ((@pTo is null) or (convert(datetime, c.DateOfDay, 103) <= convert(datetime, @pTo, 103)))
	AND ((@pEnable is null) or (Enable = @pEnable))
	ORDER BY c.id desc
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_Customers]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_Customers] --'',null,null,2

	@pName						nvarchar(50) = NULL,
	@pFrom						nvarchar(50) = NULL,
	@pTo						nvarchar(50) = NULL,
	@pEnable					int = NULL 

AS
BEGIN
	SELECT 
	id,
	Salutation,
	Image,
	Name,
	Company,
	Designation,
	Landline,
	Mobile,
	Email,
	Website,
	Address,
	AddressLandline,
	City,
	Country,
	BankAccountNumber,
	PaymentMethod,
	Vendor,
	Customer,
	Employee,
	Enable,
	AddedBy,
	UpdatedBy,
	Delete_Request_By,
	Delete_Status,
	TimeOfDay,
	DateOfDay,
	MonthOfDay,
	YearOfDay

	FROM Contacts AS c
	WHERE ((@pName is null) or Name LIKE '%' + @pName + '%')
	AND ((@pFrom is null) or (convert(datetime, c.DateOfDay, 103) >= convert(datetime, @pFrom, 103)))
	AND ((@pTo is null) or (convert(datetime, c.DateOfDay, 103) <= convert(datetime, @pTo, 103)))
	AND (c.Customer = '1')
	AND ((@pEnable is null) or (Enable = @pEnable))
	ORDER BY c.id desc
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_DeleteRequests]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_DeleteRequests] 

AS
BEGIN
	SELECT 
	id,
	Type,
	Count
	FROM DeleteRequests
	ORDER BY id desc
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_DelRequested_Brands]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_DelRequested_Brands] 

	
AS
BEGIN
	SELECT 
	Brand_id AS id,
	Brand_Name AS Name,
	Delete_Request_By,
	(SELECT email FROM Users WHERE id = b.Delete_Request_By) as username
	
	FROM Brands AS b
	WHERE b.Delete_Status = 'Requested'
	AND Enable = 0
	ORDER BY b.Brand_id desc

END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_DelRequested_Categories]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_DelRequested_Categories] 

	
AS
BEGIN
	SELECT 
	Category_id AS id,
	Category_Name AS Name,
	Delete_Request_By,
	(SELECT email FROM Users WHERE id = c.Delete_Request_By) as username
	
	FROM Categories AS c
	WHERE c.Delete_Status = 'Requested'
	AND Enable = 0
	ORDER BY c.Category_id desc

END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_DelRequested_Cities]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_DelRequested_Cities] 

	
AS
BEGIN
	SELECT 
	id,
	Name,
	Delete_Request_By,
	(SELECT email FROM Users WHERE id = c.Delete_Request_By) as username
	
	FROM Cities AS c
	WHERE c.Delete_Status = 'Requested'
	AND Enable = 0
	ORDER BY c.id desc

END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_DelRequested_Companies]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_DelRequested_Companies] 

	
AS
BEGIN
	SELECT 
	id,
	Name,
	Delete_Request_By,
	(SELECT email FROM Users WHERE id = c.Delete_Request_By) as username
	
	FROM Companies AS c
	WHERE c.Delete_Status = 'Requested'
	AND Enable = 0
	ORDER BY c.id desc

END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_DelRequested_Contacts]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_DelRequested_Contacts] 

	
AS
BEGIN
	SELECT 
	id,
	Name,
	Delete_Request_By,
	(SELECT email FROM Users WHERE id = c.Delete_Request_By) as username
	
	FROM Contacts AS c
	WHERE c.Delete_Status = 'Requested'
	AND Enable = 0
	ORDER BY c.id desc

END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_DelRequested_Countries]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_DelRequested_Countries] 

	
AS
BEGIN
	SELECT 
	id,
	Name,
	Delete_Request_By,
	(SELECT email FROM Users WHERE id = c.Delete_Request_By) as username
	
	FROM Countries AS c
	WHERE c.Delete_Status = 'Requested'
	AND Enable = 0
	ORDER BY c.id desc

END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_DelRequested_Customers]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_DelRequested_Customers] 

	
AS
BEGIN
	SELECT 
	id,
	Name,
	Delete_Request_By,
	(SELECT email FROM Users WHERE id = c.Delete_Request_By) as username
	
	FROM Contacts AS c
	WHERE c.Delete_Status = 'Requested' 
	AND c.Customer = '1'
	AND Enable = 0
	ORDER BY c.id desc

END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_DelRequested_Employees]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_DelRequested_Employees] 

	
AS
BEGIN
	SELECT 
	id,
	Name,
	Delete_Request_By,
	(SELECT email FROM Users WHERE id = c.Delete_Request_By) as username
	
	FROM Contacts AS c
	WHERE c.Delete_Status = 'Requested' 
	AND c.Employee = '1'
	AND Enable = 0
	ORDER BY c.id desc

END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_DelRequested_Factories]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_DelRequested_Factories] 

	
AS
BEGIN
	SELECT 
	id,
	Name,
	Delete_Request_By,
	(SELECT email FROM Users WHERE id = p.Delete_Request_By) as username
	
	FROM Premises AS p
	WHERE p.Delete_Status = 'Requested' 
	AND p.Factory = '1'
	AND Enable = 0
	ORDER BY p.id desc

END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_DelRequested_Items]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_DelRequested_Items] 

	
AS
BEGIN
	SELECT 
	Item_id AS id,
	Item_Name AS name,
	Delete_Request_By,
	(SELECT email FROM Users WHERE id = i.Delete_Request_By) as username
	
	FROM Items AS i
	WHERE i.Delete_Status = 'Requested'
	AND Enable = 0
	ORDER BY i.Item_id desc

END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_DelRequested_Manufacturers]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_DelRequested_Manufacturers] 

	
AS
BEGIN
	SELECT 
	Manufacturer_id AS id,
	Manufacturer_Name AS Name,
	Delete_Request_By,
	(SELECT email FROM Users WHERE id = m.Delete_Request_By) as username
	
	FROM Manufacturers AS m
	WHERE m.Delete_Status = 'Requested'
	AND Enable = 0
	ORDER BY m.Manufacturer_id desc

END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_DelRequested_Offices]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_DelRequested_Offices] 

	
AS
BEGIN
	SELECT 
	id,
	Name,
	Delete_Request_By,
	(SELECT email FROM Users WHERE id = p.Delete_Request_By) as username
	
	FROM Premises AS p
	WHERE p.Delete_Status = 'Requested' 
	AND p.Office = '1'
	AND Enable = 0
	ORDER BY p.id desc

END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_DelRequested_Purchases]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_DelRequested_Purchases] 

	
AS
BEGIN
	SELECT 
	id,
	TempOrderNum as Name,
	Delete_Request_By,
	(SELECT email FROM Users WHERE id = p.Delete_Request_By) as username
	
	FROM Purchasing AS p
	WHERE p.Delete_Status = 'Requested'
	AND Enable = 0
	ORDER BY P.id desc

END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_DelRequested_Sales]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_DelRequested_Sales] 

	
AS
BEGIN
	SELECT 
	id,
	SaleOrderNo as Name,
	Delete_Request_By,
	(SELECT email FROM Users WHERE id = s.Delete_Request_By) as username
	
	FROM SalesOrder AS s
	WHERE s.Delete_Status = 'Requested'
	AND Enable = 0
	ORDER BY s.id desc

END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_DelRequested_Shops]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_DelRequested_Shops] 

	
AS
BEGIN
	SELECT 
	id,
	Name,
	Delete_Request_By,
	(SELECT email FROM Users WHERE id = p.Delete_Request_By) as username
	
	FROM Premises AS p
	WHERE p.Delete_Status = 'Requested' 
	AND p.Shop = '1'
	AND Enable = 0
	ORDER BY p.id desc

END


GO
/****** Object:  StoredProcedure [dbo].[proc_Select_DelRequested_Stores]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_DelRequested_Stores] 

	
AS
BEGIN
	SELECT 
	id,
	Name,
	Delete_Request_By,
	(SELECT email FROM Users WHERE id = p.Delete_Request_By) as username
	
	FROM Premises AS p
	WHERE p.Delete_Status = 'Requested' 
	AND p.Store = '1'
	AND Enable = 0
	ORDER BY p.id desc

END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_DelRequested_Units]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_DelRequested_Units] 

	
AS
BEGIN
	SELECT 
	Unit_id AS id,
	Unit_Name AS Name,
	Delete_Request_By,
	(SELECT email FROM Users WHERE id = u.Delete_Request_By) as username
	
	FROM Units AS u
	WHERE u.Delete_Status = 'Requested'
	AND Enable = 0
	ORDER BY u.Unit_id desc

END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_DelRequested_Users]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_DelRequested_Users] 

	
AS
BEGIN
	SELECT 
	id,
	email as Name,
	Delete_Request_By,
	(SELECT email FROM Users WHERE id = u.Delete_Request_By) as username
	
	FROM Users AS u
	WHERE u.Delete_Status = 'Requested'
	AND Enable = 0
	ORDER BY u.id desc

END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_DelRequested_Vendors]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_DelRequested_Vendors] 

	
AS
BEGIN
	SELECT 
	id,
	Name,
	Delete_Request_By,
	(SELECT email FROM Users WHERE id = c.Delete_Request_By) as username
	
	FROM Contacts AS c
	WHERE c.Delete_Status = 'Requested' 
	AND c.Vendor = '1'
	AND Enable = 0
	ORDER BY c.id desc

END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_Employees]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_Employees] --'','26/07/2019','26/07/2019'

	@pName						nvarchar(50) = NULL,
	@pFrom						nvarchar(50) = NULL,
	@pTo						nvarchar(50) = NULL,
	@pEnable					int = NULL 

AS
BEGIN
	SELECT 
	id,
	Salutation,
	Image,
	Name,
	Company,
	Designation,
	Landline,
	Mobile,
	Email,
	Website,
	Address,
	AddressLandline,
	City,
	Country,
	BankAccountNumber,
	PaymentMethod,
	Vendor,
	Customer,
	Employee,
	Enable,
	AddedBy,
	UpdatedBy,
	Delete_Request_By,
	Delete_Status,
	TimeOfDay,
	DateOfDay,
	MonthOfDay,
	YearOfDay

	FROM Contacts AS c
	WHERE ((@pName is null) or Name LIKE '%' + @pName + '%')
	AND ((@pFrom is null) or (convert(datetime, c.DateOfDay, 103) >= convert(datetime, @pFrom, 103)))
	AND ((@pTo is null) or (convert(datetime, c.DateOfDay, 103) <= convert(datetime, @pTo, 103)))
	AND (c.Employee = '1')
	AND ((@pEnable is null) or (Enable = @pEnable))
	ORDER BY c.id desc
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_EventsName_UserID]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_EventsName_UserID] --2
	
	@pUser_id				int = 0

AS
BEGIN
	SELECT 
	id,
	EventName,
	BackgroundColour,
	BorderColour,
	AddedBy,
	UpdatedBy,
	Delete_Request_By,
	Delete_Status,
	Time_Of_Day,
	Date_Of_Day,
	Month_Of_Day,
	Year_Of_Day
	FROM Events
	WHERE AddedBy = @pUser_id
	ORDER BY id desc
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_Factories]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_Factories]
	
	@pName					nvarchar(50) = NULL,
	@pFrom					nvarchar(50) = NULL,
	@pTo					nvarchar(50) = NULL,
	@pEnable				int = NULL 

AS
BEGIN
	SELECT 
	id,
	Name,
	Pc_Mac_Address,
	Phone,
	City,
	(SELECT Name FROM Cities Where id = p.City) as CityName,
	Country,
	(SELECT Name FROM Countries Where id = p.Country) as CountryName,
	Address,
	Office,
	Factory,
	Store,
	Shop,
	Enable,
	AddedBy,
	UpdatedBy,
	Delete_Request_By,
	Delete_Status,
	UserId,
	Time_Of_Day,
	Date_Of_Day,
	Month_Of_Day,
	Year_Of_Day

	FROM Premises AS p
WHERE ((@pName is null) or (Name LIKE '%' + @pName + '%' or Pc_Mac_Address LIKE '%' + @pName + '%' or Phone LIKE '%' + @pName + '%' or (SELECT Name FROM Countries Where id = p.Country) LIKE '%' + @pName + '%' or (SELECT Name FROM Cities Where id = p.City) LIKE '%' + @pName + '%' or Address LIKE '%' + @pName + '%'))
	AND ((@pFrom is null) or (convert(datetime, p.Date_Of_Day, 103) >= convert(datetime, @pFrom, 103)))
	AND ((@pTo is null) or (convert(datetime, p.Date_Of_Day, 103) <= convert(datetime, @pTo, 103)))
	AND Factory = '1'
	AND ((@pEnable is null) or (Enable = @pEnable))
	ORDER BY id desc

END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_Invoices_By_ItemID]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_Invoices_By_ItemID] --1
	
	@pItem_id				int = 0,
	@pSearch				nvarchar(50) = NULL

AS
BEGIN
	SELECT 
		i.id as Invoice_id,
		i.Invoice_No,
		s.Customer_id,
		(SELECT Name FROM Contacts WHERE id = s.Customer_id) as CustomerName,
		PriceUnit as Price,
		Qty as QtyOrdered,
		i.Invoice_Status,
		i.InvoiceDateTime
	FROM SalesOrder_Details AS s
	INNER JOIN SalesOrder_Invoices as i on s.id=i.SalesOrder_id
	WHERE ((@pSearch is null) or i.Invoice_No LIKE '%' + @pSearch + '%' or (SELECT Name FROM Contacts WHERE id = s.Customer_id) LIKE '%' + @pSearch + '%')
	AND (s.ItemId = @pItem_id)
	ORDER BY (SELECT SaleOrderNo FROM SalesOrder WHERE id = s.SalesOrder_id) desc
	
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_ItemActivity]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_ItemActivity] --9,'Sales Order'
	
	@pActivityType_id					int,
	@pActivityType						nvarchar(50) = NULL

AS
BEGIN
	SELECT 
	id,
	ActivityType_id,
	ActivityType,
	--Item_id,
	--(SELECT Item_Name FROM Items WHERE Item_id = @pItem_id) as ItemName,
	ActivityName,
	Description,
	Date,
	Time,
	User_id,
	(SELECT Name FROM Contacts WHERE id = (SELECT attached_profile FROM Users WHERE id = User_id)) as UserName,
	Icon
	 FROM ItemActivity
	WHERE ActivityType_id = @pActivityType_id
	AND ActivityType = @pActivityType
	ORDER BY id desc
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_Items]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_Items] --'CocaCola'
	
	@pItem_Name				nvarchar(50) = NULL,
	@pFrom					nvarchar(50) = NULL,
	@pTo					nvarchar(50) = NULL,
	@pEnable					int = NULL 

AS
BEGIN
	SELECT 
		Item_id,
		Item_type,
		Item_file,
		Item_Name,
		Item_Sku,
		Item_Upc,
		Item_Mpn,
		Item_Ean,
		Item_Isbn,
		Item_Sell_Price,
		Item_Tax,
		Item_Purchase_Price,
		Enable,
		Time_Of_Day,
		Date_Of_Day,
		Month_Of_Day,
		Year_Of_Day,
		Delete_Request_By,
		Delete_Status,
		(SELECT Category_id FROM Categories WHERE Category_id=i.Item_Category) as Catgeory_id,
		(SELECT Category_Name FROM Categories WHERE Category_id=i.Item_Category) as Category,
		(SELECT Brand_id FROM Brands WHERE Brand_id=i.Item_Brand) as Brand_id,
		(SELECT Brand_Name FROM Brands WHERE Brand_id=i.Item_Brand) as Brand,
		(SELECT Unit_id FROM Units WHERE Unit_id=i.Item_Unit) as Unit_id,
		(SELECT Unit_Name FROM Units WHERE Unit_id=i.Item_Unit) as Unit,
		(SELECT Manufacturer_id FROM Manufacturers WHERE Manufacturer_id=i.Item_Manufacturer) as Manufacturer_id,
		(SELECT Manufacturer_Name FROM Manufacturers WHERE Manufacturer_id=i.Item_Manufacturer) as Manufacturer,
		(SELECT id FROM Contacts WHERE id=i.Item_Preferred_Vendor) as Vendor_id,
		(SELECT Name FROM Contacts WHERE id=i.Item_Preferred_Vendor) as Vendor,
		(SELECT Stock_id FROM Stock WHERE Item_id=i.Item_id) as Stock_id,
		(SELECT OpeningStock FROM Stock WHERE Item_id=i.Item_id) as OpeningStock,
		(SELECT ReorderLevel FROM Stock WHERE Item_id=i.Item_id) as ReorderLevel

	FROM Items AS i
	WHERE ((@pItem_Name is null) or Item_Name LIKE '%' + @pItem_Name + '%')
	AND ((@pFrom is null) or (convert(datetime, i.Date_Of_Day, 103) >= convert(datetime, @pFrom, 103)))
	AND ((@pTo is null) or (convert(datetime, i.Date_Of_Day, 103) <= convert(datetime, @pTo, 103)))
	AND ((@pEnable is null) or (Enable = @pEnable))
	ORDER BY i.Item_id desc
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_Items_By_ID]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_Items_By_ID] --8
	@pItem_id		nvarchar(MAX)
	--@pEnable		nvarchar(MAX) = NULL
AS
BEGIN
	SELECT 
		Item_id,
		Item_type,
		Item_file,
		Item_Name,
		Item_Sku,
		Item_Upc,
		Item_Mpn,
		Item_Ean,
		Item_Isbn,
		Item_Sell_Price,
		Item_Tax,
		Item_Purchase_Price,
		Enable,
		Time_Of_Day,
		Date_Of_Day,
		Month_Of_Day,
		Year_Of_Day,
		Delete_Request_By,
		Delete_Status,
		(SELECT Category_id FROM Categories WHERE Category_id=i.Item_Category) as Catgeory_id,
		(SELECT Category_Name FROM Categories WHERE Category_id=i.Item_Category) as Category,
		(SELECT Brand_id FROM Brands WHERE Brand_id=i.Item_Brand) as Brand_id,
		(SELECT Brand_Name FROM Brands WHERE Brand_id=i.Item_Brand) as Brand,
		(SELECT Unit_id FROM Units WHERE Unit_id=i.Item_Unit) as Unit_id,
		(SELECT Unit_Name FROM Units WHERE Unit_id=i.Item_Unit) as Unit,
		(SELECT Manufacturer_id FROM Manufacturers WHERE Manufacturer_id=i.Item_Manufacturer) as Manufacturer_id,
		(SELECT Manufacturer_Name FROM Manufacturers WHERE Manufacturer_id=i.Item_Manufacturer) as Manufacturer,
		(SELECT id FROM Contacts WHERE id=i.Item_Preferred_Vendor) as Vendor_id,
		(SELECT Name FROM Contacts WHERE id=i.Item_Preferred_Vendor) as Vendor,
		(SELECT Stock_id FROM Stock WHERE Item_id=i.Item_id) as Stock_id,
		(SELECT OpeningStock FROM Stock WHERE Item_id=i.Item_id) as OpeningStock,
		(SELECT ReorderLevel FROM Stock WHERE Item_id=i.Item_id) as ReorderLevel

	FROM Items AS i
	WHERE i.Item_id = @pItem_id
	--AND i.Enable = ISNULL(@pEnable, 1)
	ORDER BY I.Item_id desc
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_Items_By_SO_ID]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_Items_By_SO_ID] --14
	
	@pSalesOrder_id					int
AS
BEGIN
	SELECT
		SalesOrder_id,
		ItemId,
		(SELECT Item_Name FROM Items WHERE Item_id=s.ItemId)
		Customer_id,
		Qty,
		PriceUnit,
		MsrmntUnit

	FROM  SalesOrder_Details as s
	WHERE s.SalesOrder_id = @pSalesOrder_id

	ORDER BY s.id DESC
END



GO
/****** Object:  StoredProcedure [dbo].[proc_Select_Items_SaleReturn]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_Items_SaleReturn] --24
	
	@pSalesOrder_id		nvarchar(MAX)

AS
BEGIN
	SELECT 
		SO_PackageDetail_id,
		s.SaleOrder_id as SalesOrder_id,
		s.Package_No,
		p.Package_id,
		p.Package_Status,
		Item_Status,
		Item_id as ItemId,
		(SELECT case when Return_Qty = 'NULL' then '0' else Return_Qty end FROM SaleReturnDetail where Item_id=s.Item_id and SaleReturn_id = (SELECT SaleReturn_id FROM SaleReturn where SaleOrder_id = @pSalesOrder_id)) as ReturnQty,
		(SELECT Item_Name FROM Items WHERE Item_id = s.Item_id) as ItemName,
		(SELECT Qty FROM SalesOrder_Details WHERE ItemId = s.Item_id and SalesOrder_id = @pSalesOrder_id) as Qty,
		(SELECT MsrmntUnit FROM SalesOrder_Details WHERE ItemId = s.Item_id and SalesOrder_id = @pSalesOrder_id) as MsrmntUnit,
		(SELECT PriceUnit FROM SalesOrder_Details WHERE ItemId = s.Item_id and SalesOrder_id = @pSalesOrder_id) as PriceUnit,
		(SELECT Customer_id  FROM SalesOrder_Details WHERE ItemId = s.Item_id and SalesOrder_id = @pSalesOrder_id) as Customer_id,
		(SELECT Name FROM Contacts WHERE id = (SELECT Customer_id  FROM SalesOrder_Details WHERE ItemId = s.Item_id and SalesOrder_id = @pSalesOrder_id)) as CustomerName,
		(SELECT Address FROM Contacts WHERE id = (SELECT Customer_id  FROM SalesOrder_Details WHERE ItemId = s.Item_id and SalesOrder_id = @pSalesOrder_id)) as CustomerAddress,
		(SELECT Landline FROM Contacts WHERE id = (SELECT Customer_id  FROM SalesOrder_Details WHERE ItemId = s.Item_id and SalesOrder_id = @pSalesOrder_id)) as CustomerLandline,
		(SELECT Mobile FROM Contacts WHERE id = (SELECT Customer_id  FROM SalesOrder_Details WHERE ItemId = s.Item_id and SalesOrder_id = @pSalesOrder_id)) as CustomerMobile,
		(SELECT Email FROM Contacts WHERE id = (SELECT Customer_id  FROM SalesOrder_Details WHERE ItemId = s.Item_id and SalesOrder_id = @pSalesOrder_id)) as CustomerEmail,
		Total_Qty,
		Packed_Item_Qty as PackgingQty,
		p.Package_Date,
		p.Package_Cost

	FROM SO_Packages_Detail AS s
	inner join SO_Packages as p on s.Package_No=p.Package_No
	where s.SaleOrder_id=@pSalesOrder_id
	and (p.Package_Status='Delivered' or p.Package_Status='Shipped')
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_Last_BillNo]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_Last_BillNo]


AS
BEGIN

	SELECT TOP 1
		Bill_No	as LastBillNo
		
	FROM Bills AS b

	ORDER BY b.Bill_id DESC

END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_Last_Payment_Invoice]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_Last_Payment_Invoice] --9
	
	@pInvoiceId					int
AS
BEGIN
	SELECT TOP 1
	SO_Payment_id,
	SO_Invoice_id,
	SO_Payment_Mode,
	SO_BankCharges,
	SO_Payment_Date,
	SO_Total_Amount,
	SO_Paid_Amount,
	SO_Balance_Amount,
	AddedBy,
	UpdatedBy,
	Time_Of_Day,
	Date_Of_Day,
	Month_Of_Day,
	Year_Of_Day

	FROM  SO_Payments as p
	WHERE SO_Invoice_id = @pInvoiceId

	ORDER BY p.SO_Payment_id DESC
END



GO
/****** Object:  StoredProcedure [dbo].[proc_Select_Manufacturer_By_Name]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_Manufacturer_By_Name] --'',null,null,null

	@pManufacturer_Name						nvarchar(50) = NULL
		
AS
BEGIN
	SELECT
	Manufacturer_id,
	Manufacturer_Name,
	Manufacturer_Code,
	Enable,
	AddedBy,
	UpdatedBy,
	Delete_Request_By,
	Delete_Status,
	Time_Of_Day,
	Date_Of_Day,
	Month_Of_Day,
	Year_Of_Day

	FROM Manufacturers as u
	WHERE Manufacturer_Name = @pManufacturer_Name
END



GO
/****** Object:  StoredProcedure [dbo].[proc_Select_Manufacturers]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_Manufacturers] --'te'
	
	@pManufacturer_Name				nvarchar(50) = NULL,
	@pFrom							nvarchar(50) = NULL,
	@pTo							nvarchar(50) = NULL,
	@pEnable					int = NULL 

AS
BEGIN
	SELECT 
	Manufacturer_id,
	Manufacturer_Name,
	Manufacturer_Code,
	Enable,
	Delete_Request_By,
	Delete_Status,
	Time_Of_Day,
	Date_Of_Day,
	Month_Of_Day,
	Year_Of_Day
	FROM Manufacturers as m
	WHERE ((@pManufacturer_Name is null) or Manufacturer_Name LIKE '%' + @pManufacturer_Name + '%')
	AND ((@pFrom is null) or (convert(datetime, m.Date_Of_Day, 103) >= convert(datetime, @pFrom, 103)))
	AND ((@pTo is null) or (convert(datetime, m.Date_Of_Day, 103) <= convert(datetime, @pTo, 103)))
	AND ((@pEnable is null) or (Enable = @pEnable))
	ORDER BY Manufacturer_id desc
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_Manufacturers_By_ID]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_Manufacturers_By_ID] --2
	
	@pManufacturer_id			nvarchar(50)

AS
BEGIN
	SELECT 
	Manufacturer_id,
	Manufacturer_Name,
	Manufacturer_Code,
	Enable,
	Delete_Request_By,
	Delete_Status,
	Time_Of_Day,
	Date_Of_Day,
	Month_Of_Day,
	Year_Of_Day
	FROM Manufacturers AS i 
	WHERE Manufacturer_id = @pManufacturer_id
	ORDER BY Manufacturer_id desc
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_Messages]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_Messages] --2,2

	@pSender_id				int,
	@pReceiver_id			int=0
	
AS
BEGIN
	SELECT 

	id,
	Sender_id,
	(SELECT attached_profile FROM Users WHERE id=@pSender_id) as Sender_attachedprofile,
	Receiver_id,
	(SELECT attached_profile FROM Users WHERE id=@pReceiver_id) as Receiver_attachedprofile,
	Message,
	Date,
	Time,
	Month,
	Year

	FROM Messages AS m
	WHERE ((m.Sender_id = @pSender_id AND m.Receiver_id = @pReceiver_id)
	OR (m.Sender_id = @pReceiver_id AND m.Receiver_id = @pSender_id))
	AND (m.Enable = 1)

END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_MessagesNotifications]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_MessagesNotifications] --2

	@pReceiver_id			int
	
AS
BEGIN
	SELECT 
	--TOP 1
	id,
	Sender_id,
	(SELECT attached_profile FROM Users WHERE id=m.Sender_id) as Sender_attachedprofile,
	Receiver_id,
	(SELECT attached_profile FROM Users WHERE id=@pReceiver_id) as Receiver_attachedprofile,
	Message,
	Status,
	Date,
	Time,
	Month,
	Year

	FROM Messages AS m
	WHERE m.Receiver_id = @pReceiver_id
	AND m.Status='Unread'
	ORDER BY m.id DESC
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_Offices]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_Offices]
	
	@pName					nvarchar(50) = NULL,
	@pFrom					nvarchar(50) = NULL,
	@pTo					nvarchar(50) = NULL,
	@pEnable				int = NULL 

AS
BEGIN
	SELECT 
	id,
	Name,
	Pc_Mac_Address,
	Phone,
	City,
	(SELECT Name FROM Cities Where id = p.City) as CityName,
	Country,
	(SELECT Name FROM Countries Where id = p.Country) as CountryName,
	Address,
	Office,
	Factory,
	Store,
	Shop,
	Enable,
	AddedBy,
	UpdatedBy,
	Delete_Request_By,
	Delete_Status,
	UserId,
	Time_Of_Day,
	Date_Of_Day,
	Month_Of_Day,
	Year_Of_Day

	FROM Premises AS p
	WHERE ((@pName is null) or (Name LIKE '%' + @pName + '%' or Pc_Mac_Address LIKE '%' + @pName + '%' or Phone LIKE '%' + @pName + '%' or (SELECT Name FROM Countries Where id = p.Country) LIKE '%' + @pName + '%' or (SELECT Name FROM Cities Where id = p.City) LIKE '%' + @pName + '%' or Address LIKE '%' + @pName + '%'))
	AND ((@pFrom is null) or (convert(datetime, p.Date_Of_Day, 103) >= convert(datetime, @pFrom, 103)))
	AND ((@pTo is null) or (convert(datetime, p.Date_Of_Day, 103) <= convert(datetime, @pTo, 103)))
	AND Office = '1'
	AND ((@pEnable is null) or (Enable = @pEnable))
	ORDER BY id desc

END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_Package_By_PackageId]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_Package_By_PackageId] 

	@pPackage_id				int = 0
AS
BEGIN
	SELECT 
	Package_id,
	SaleOrder_id,
	Package_No,
	Package_Cost,
	Package_Status,
	Package_Date,
	AddedBy,
	UpdatedBy,
	Time_Of_Day,
	Date_Of_Day,
	Month_Of_Day,
	Year_Of_Day

	FROM SO_Packages AS p
	WHERE Package_id = @pPackage_id
	
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_PackageItem_By_ItemId]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_PackageItem_By_ItemId] --7,3

	@pPackage_id				int = 0,
	@pItem_id					int = 0
AS
BEGIN
	SELECT 
	SO_PackageDetail_id,
	SaleOrder_id,
	Package_No,
	(SELECT Package_id FROM SO_Packages WHERE Package_No = p.Package_No) as Package_id,
	Package_Status,
	Item_Status,
	Item_id,
	Total_Qty,
	(SELECT PriceUnit FROM SalesOrder_Details WHERE SalesOrder_id = p.SaleOrder_id and ItemId = @pItem_id) as PriceUnit,  
	Packed_Item_Qty,
	Package_Date,
	Package_Cost,
	Time_Of_Day,
	Date_Of_Day,
	Month_Of_Day,
	Year_Of_Day

	FROM SO_Packages_Detail AS p
	WHERE (SELECT Package_id FROM SO_Packages WHERE Package_No = p.Package_No) = @pPackage_id
	AND Item_id = @pItem_id
	
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_Packages_By_SO_ID]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_Packages_By_SO_ID] --3
	
	@pSalesOrder_id				int

AS
BEGIN
	SELECT 
		Package_id,
		SaleOrder_id,
		Package_No,
		Package_Cost,
		Package_Status,
		Package_Date,
		Time_Of_Day,
		Date_Of_Day,
		Month_Of_Day,
		Year_Of_Day
		FROM SO_Packages AS p
		WHERE p.SaleOrder_id = @pSalesOrder_id
		ORDER BY p.SaleOrder_id desc
	
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_PackagesDetail_By_Package_ID]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_PackagesDetail_By_Package_ID] --31
	
	@pSalesOrder_id				int

AS
	BEGIN
		SELECT 
		pd.SO_PackageDetail_id,
		p.Package_id,
		pd.Item_id,
		(SELECT Item_Name FROM Items WHERE Item_id=pd.Item_id) as Item_Name,
		(SELECT Package_Status FROM SO_Packages WHERE Package_id=p.Package_id) as Package_Status,
		(SELECT SalesOrder_id FROM SO_Packages WHERE Package_id=p.Package_id) as SaleOrder_id,
		Total_Qty as OrderedQuantity,
		Packed_Item_Qty
		FROM SO_Packages_Detail AS pd
		inner join SO_Packages as p on p.Package_id=pd.Package_id
		WHERE p.SalesOrder_id = @pSalesOrder_id
		ORDER BY p.SalesOrder_id desc
	
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_PackagesDetail_By_SO_ID]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_PackagesDetail_By_SO_ID] --2,14

	@pItem_Id					int,
	@pSalesOrder_id				int

AS
BEGIN
		SELECT 
		Package_id,
		Item_Status,
		(SELECT SUM(cast(Package_Cost as int)) FROM SO_Packages WHERE SalesOrder_id=@pSalesOrder_id) as PackageCost,
		(SELECT Item_id FROM Items WHERE Item_id=@pItem_Id) as Item_Id,
		(SELECT Item_Name FROM Items WHERE Item_id=@pItem_Id) as ItemName,
		(SELECT PriceUnit FROM SalesOrder_Details WHERE Itemid=@pItem_Id and SalesOrder_id=@pSalesOrder_id) as price,
		Total_Qty,
		Packed_Item_Qty
		FROM SO_Packages_Detail AS p
		WHERE p.Item_id = @pItem_Id
		ORDER BY p.Item_id desc
	
END


GO
/****** Object:  StoredProcedure [dbo].[proc_Select_PackagesForShipment_By_SO_ID]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_PackagesForShipment_By_SO_ID] --5
	
	@pSalesOrder_id				int

AS
BEGIN
	SELECT 
		Package_id,
		SaleOrder_id,
		Package_No,
		Package_Cost,
		Package_Status,
		Package_Date,
		Time_Of_Day,
		Date_Of_Day,
		Month_Of_Day,
		Year_Of_Day
		FROM SO_Packages AS p
		WHERE p.SaleOrder_id = @pSalesOrder_id
		ORDER BY p.SaleOrder_id desc
	
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_Payments_By_BillID]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_Payments_By_BillID] --2
	
	@pBill_id				int = 0

AS
BEGIN
	SELECT 
	Payment_id,
	Bill_id,
	(SELECT Bill_No FROM Bills where bill_id=p.bill_id) as Bill_No,
	(SELECT Bill_Amount FROM Bills where bill_id=p.bill_id) as Bill_Amount,
	Payment_Mode,
	Payment_Date,
	Total_Amount,
	Paid_Amount,
	Balance_Amount,
	Time_Of_Day,
	Date_Of_Day,
	Month_Of_Day,
	Year_Of_Day

	FROM Payments AS p
	WHERE p.Bill_id = CASE WHEN @pBill_id IS NULL THEN p.Bill_id ELSE @pBill_id END
	ORDER BY p.Payment_id desc
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_Payments_By_CustomerId]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_Payments_By_CustomerId] --27

	@pCustomer_id					int,
	@pSearch						nvarchar(50) = NULL
	
	
AS
BEGIN
	SELECT DISTINCT
	p.SO_Payment_id,
	p.SO_Invoice_id,
	i.Invoice_No,
	(SELECT Payment_Mode FROM PaymentMode WHERE id = p.SO_Payment_Mode) as SO_Payment_Mode,
	p.SO_Payment_Date,
	p.SO_Total_Amount,
	--(SELECT CAST(SO_Total_Amount as decimal(10,2)) FROM SalesOrder WHERE id = sd.SalesOrder_id) as SO_Total_Amount,
	CAST(p.SO_Paid_Amount as decimal(10,2)) as SO_Paid_Amount,
	p.Time_Of_Day,
	p.Date_Of_Day

	FROM SalesOrder_Details as sd
	INNER JOIN SalesOrder_Invoices as i on sd.SalesOrder_id = i.SalesOrder_id
	INNER JOIN SO_Payments as p on i.id = p.SO_Invoice_id
	
	WHERE ((@pSearch is null) or i.Invoice_No LIKE '%' + @pSearch + '%' or (SELECT Payment_Mode FROM PaymentMode WHERE id = p.SO_Payment_Mode) LIKE '%' + @pSearch + '%' or p.Date_Of_Day LIKE '%' + @pSearch + '%' or p.Time_Of_Day LIKE '%' + @pSearch + '%')
	AND sd.Customer_id = @pCustomer_id
	Group By Invoice_No,p.SO_Payment_id,p.SO_Invoice_id,SO_Payment_Mode,p.SO_Payment_Date,SO_Total_Amount,p.SO_Paid_Amount,p.Time_Of_Day,p.Date_Of_Day,sd.SalesOrder_id
	--ORDER BY pd.pd_id DESC
END



GO
/****** Object:  StoredProcedure [dbo].[proc_Select_Payments_By_VendorId]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_Payments_By_VendorId] --26

	@pVendorId						int,
	@pSearch						nvarchar(50) = NULL
	
	
AS
BEGIN
	SELECT DISTINCT
	p.Payment_id,
	p.Bill_id,
	b.Bill_No,
	(SELECT Payment_Mode FROM PaymentMode WHERE id = p.Payment_Mode) as Payment_Mode,
	p.Payment_Date,
	p.Total_Amount,
	p.Paid_Amount,
	p.Time_Of_Day,
	p.Date_Of_Day

	FROM PurchasingDetails as pd
	INNER JOIN Bills as b on pd.PurchasingId = b.Purchase_id
	INNER JOIN Payments as p on b.Bill_id = p.Bill_id
	
	WHERE ((@pSearch is null) or b.Bill_No LIKE '%' + @pSearch + '%' or (SELECT Payment_Mode FROM PaymentMode WHERE id = p.Payment_Mode) LIKE '%' + @pSearch + '%' or p.Date_Of_Day LIKE '%' + @pSearch + '%' or p.Time_Of_Day LIKE '%' + @pSearch + '%')
	AND pd.VendorId = @pVendorId
	Group By Bill_No,p.Payment_id,p.Bill_id,Payment_Mode,p.Payment_Date,p.Total_Amount,p.Paid_Amount,p.Time_Of_Day,p.Date_Of_Day
	--ORDER BY pd.pd_id DESC
END



GO
/****** Object:  StoredProcedure [dbo].[proc_Select_PO_By_ItemID]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_PO_By_ItemID] --1
	
	@pItem_id				int = 0,
	@pSearch				nvarchar(50) = NULL

AS
BEGIN
	SELECT 
		p.id as PurchaseOrder_id,
		p.TempOrderNum as TempOrderNum,
		pd.VendorId as Vendor_id,
		(SELECT Name FROM Contacts WHERE id = pd.VendorId) as VendorName,
		pd.PriceUnit as Price,
		pd.Qty as QtyOrdered,
		p.RecieveStatus,
		p.Recieve_DateTime

	FROM PurchasingDetails AS pd
	INNER JOIN Purchasing as p on pd.PurchasingId = p.id
	WHERE ((@pSearch is null) or p.TempOrderNum LIKE '%' + @pSearch + '%' or (SELECT Name FROM Contacts WHERE id = pd.VendorId) LIKE '%' + @pSearch + '%')
	AND pd.ItemId = @pItem_id
	ORDER BY p.id desc
	
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_Premises_By_ID]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_Premises_By_ID] --32

	@pid					nvarchar(50)
	
AS
BEGIN
	SELECT 
	id,
	Name,
	Pc_Mac_Address,
	Phone,
	City,
	(SELECT Name FROM Cities Where id = p.City) as CityName,
	Country,
	(SELECT Name FROM Countries Where id = p.Country) as CountryName,
	Address,
	Office,
	Factory,
	Store,
	Shop,
	Enable,
	AddedBy,
	UpdatedBy,
	Delete_Request_By,
	Delete_Status,
	UserId,
	Time_Of_Day,
	Date_Of_Day,
	Month_Of_Day,
	Year_Of_Day

	FROM Premises AS p
	WHERE id = @pid
	ORDER BY id desc

END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_PreviousPurchaseOrders]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_PreviousPurchaseOrders] --0,15
	
	@pItem_id				int = 0,
	@pVendor_id				int = 0,
	@pSearch				nvarchar(50) = NULL

AS
BEGIN
	SELECT TOP 5
		p.id as PurchaseOrder_id,
		pd.ItemId,
		(SELECT Item_Name FROM Items WHERE Item_id = pd.itemId) as Item_Name,
		p.TempOrderNum as TempOrderNum,
		pd.VendorId as Vendor_id,
		(SELECT Name FROM Contacts WHERE id = pd.VendorId) as VendorName,
		pd.PriceUnit as Price,
		pd.Qty as QtyOrdered,
		p.RecieveStatus,
		p.Recieve_DateTime

	FROM PurchasingDetails AS pd
	INNER JOIN Purchasing as p on pd.PurchasingId = p.id
	WHERE ((@pSearch is null) or p.TempOrderNum LIKE '%' + @pSearch + '%' or (SELECT Name FROM Contacts WHERE id = pd.VendorId) LIKE '%' + @pSearch + '%')
	AND ((@pItem_id = 0) or (pd.ItemId = @pItem_id))
	AND ((@pVendor_id = 0) or (pd.VendorId = @pVendor_id))
	ORDER BY p.id desc
	
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_Purchased_Items_By_VendorId]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_Purchased_Items_By_VendorId] --26

	@pVendorId						int,
	@pSearch						nvarchar(50) = NULL
	
	
AS
BEGIN
	SELECT
	pd_id,
	PurchasingId,
	p.TempOrderNum,
	ItemId,
	(SELECT Item_Name FROM Items WHERE Item_id = pd.ItemId) as Item_Name,
	VendorId,
	Qty,
	PriceUnit,
	MsrmntUnit,
	p.Date_Of_Day,
	p.Time_Of_Day

	FROM  PurchasingDetails as pd
	INNER JOIN Purchasing as p on pd.PurchasingId = p.id
	WHERE ((@pSearch is null) or p.TempOrderNum LIKE '%' + @pSearch + '%' or (SELECT Item_Name FROM Items WHERE Item_id = pd.ItemId) LIKE '%' + @pSearch + '%' or p.Date_Of_Day LIKE '%' + @pSearch + '%' or p.Time_Of_Day LIKE '%' + @pSearch + '%')
	AND (pd.VendorId = @pVendorId)

	ORDER BY pd.pd_id DESC
END



GO
/****** Object:  StoredProcedure [dbo].[proc_Select_Purchasing]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_Purchasing] --'',null,null,2

	@pTempOrderNum				nvarchar(50) = NULL,
	@pFrom						nvarchar(50) = NULL,
	@pTo						nvarchar(50) = NULL,
	@pAddedBy					int,
	@pEnable					int = NULL 
	
AS
BEGIN
	SELECT
		p.id,
		p.TempOrderNum,
		p.PremisesId,
		p.UserId,
		p.TotalItems,
		p.Approved,
		p.ApprovedByUI,
		p.RecieveStatus,
		BillStatus as Bill_Stat,
		Rec_Stat as Rec_Stat,
		(SELECT Bill_Status FROM Bills where Purchase_id=p.id) AS Bill_Status,
		p.Recieve_DateTime,
		Delete_Request_By,
		Delete_Status,
		Enable,
		p.Time_Of_Day,
		p.Date_Of_Day,
		p.Month_Of_Day,
		p.Year_Of_Day

	FROM  Purchasing as p 
	WHERE ((@pTempOrderNum is null) or TempOrderNum LIKE '%' + @pTempOrderNum + '%')
	AND ((@pFrom is null) or (convert(datetime, p.Date_Of_Day, 103) >= convert(datetime, @pFrom, 103)))
	AND ((@pTo is null) or (convert(datetime, p.Date_Of_Day, 103) <= convert(datetime, @pTo, 103)))
	AND p.AddedBy = @pAddedBy 
	AND ((@pEnable is null) or (Enable = @pEnable))

	ORDER BY p.id DESC
END



GO
/****** Object:  StoredProcedure [dbo].[proc_Select_Purchasing_By_ID]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_Purchasing_By_ID] --30
	
	@pPurchasing_id					int
AS
BEGIN
	SELECT
		id,
		TempOrderNum,
		PremisesId,
		UserId,
		TotalItems,
		Approved,
		ApprovedByUI,
		RecieveStatus,
		BillStatus as Bill_Stat,
		Rec_Stat as Rec_Stat,
		(SELECT Bill_Status FROM Bills where Purchase_id=p.id) AS Bill_Status,
		Recieve_DateTime,
		Delete_Request_By,
		Delete_Status,
		Enable,
		Time_Of_Day,
		Date_Of_Day,
		Month_Of_Day,
		Year_Of_Day

	FROM  Purchasing as p 
	WHERE p.id = @pPurchasing_id

	ORDER BY p.id DESC
END



GO
/****** Object:  StoredProcedure [dbo].[proc_Select_PurchasingDetails_By_ID]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_PurchasingDetails_By_ID] --30
	
	@ppd_id					int
AS
BEGIN
	SELECT
	pd_id,
	PurchasingId,
	ItemId,
	VendorId,
	Qty,
	PriceUnit,
	MsrmntUnit

	FROM  PurchasingDetails as pd 
	WHERE pd.pd_id = @ppd_id
END



GO
/****** Object:  StoredProcedure [dbo].[proc_Select_Purchasnig_Details]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_Purchasnig_Details] --2
	
	@pPO_id				int

AS
BEGIN
	SELECT
	(SELECT id FROM Purchasing WHERE id=p.purchasingid) as Purchasing_id,
	(SELECT TempOrderNum FROM Purchasing WHERE id=p.purchasingid) as POnumber,
	(SELECT TotalItems FROM Purchasing WHERE id=p.purchasingid) as TotalItems,
	(SELECT Approved FROM Purchasing WHERE id=p.purchasingid) as Approved,
	(SELECT Time_Of_Day FROM Purchasing WHERE id=p.purchasingid) as Time_Of_Day,
	(SELECT Date_Of_Day FROM Purchasing WHERE id=p.purchasingid) as Date_Of_Day,
	(SELECT Month_Of_Day FROM Purchasing WHERE id=p.purchasingid) as Month_Of_Day,
	(SELECT Year_Of_Day FROM Purchasing WHERE id=p.purchasingid) as Year_Of_Day,
	id as purchasingDetail,
	PurchasingId,
	ItemId,
	VendorId,
	Qty,
	PriceUnit,
	MsrmntUnit,
	(SELECT Item_Name FROM Items WHERE Item_id=p.ItemId) as Item_Name,
	(SELECT Item_type FROM Items WHERE Item_id=p.ItemId) as Item_type,
	(SELECT Item_Sku FROM Items WHERE Item_id=p.ItemId) as Item_Sku,
	(SELECT Item_Category FROM Items WHERE Item_id=p.ItemId) as Item_Category,
	(SELECT Item_Unit FROM Items WHERE Item_id=p.ItemId) as Item_Unit,
	(SELECT Item_Manufacturer FROM Items WHERE Item_id=p.ItemId) as Item_Manufacturer,
	(SELECT Item_Upc FROM Items WHERE Item_id=p.ItemId) as Item_Upc,
	(SELECT Item_Brand FROM Items WHERE Item_id=p.ItemId) as Item_Brand,
	(SELECT Item_Mpn FROM Items WHERE Item_id=p.ItemId) as Item_Mpn,
	(SELECT Item_Ean FROM Items WHERE Item_id=p.ItemId) as Item_Ean,
	(SELECT Item_Isbn FROM Items WHERE Item_id=p.ItemId) as Item_Isbn,
	(SELECT Item_Sell_Price FROM Items WHERE Item_id=p.ItemId) as Item_Sell_Price,
	(SELECT Item_Tax FROM Items WHERE Item_id=p.ItemId) as Item_Tax,
	(SELECT Item_Purchase_Price FROM Items WHERE Item_id=p.ItemId) as Item_Purchase_Price,
	(SELECT Item_Preferred_Vendor FROM Items WHERE Item_id=p.ItemId) as Item_Preferred_Vendor,
	(SELECT Name FROM Contacts WHERE id=p.VendorId) as Vendor_Name
	FROM PurchasingDetails AS p
	WHERE p.PurchasingId=@pPO_id
	ORDER BY p.PurchasingId DESC
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_RoleId_By_RoleName]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_RoleId_By_RoleName] --'Admin'

	@pRole_Name				nvarchar(50)
AS
BEGIN
	SELECT 
	Role_id,
	Role_Name,
	Enable,
	AddedBy,
	UpdatedBy,
	Time_Of_Day,
	Date_Of_Day,
	Month_Of_Day,
	Year_Of_Day

	FROM Roles AS r
	WHERE r.Role_Name = @pRole_Name
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_Roles]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_Roles] 
	

AS
BEGIN
	SELECT 
	Role_id,
	Role_Name,
	Enable,
	Time_Of_Day,
	Date_Of_Day,
	Month_Of_Day,
	Year_Of_Day
	FROM Roles
	ORDER BY Role_id desc
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_Roles_By_ID]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_Roles_By_ID] 
	
	@pRole_id			int = 0

AS
BEGIN
	SELECT 
	Role_id,
	Role_Name,
	Enable,
	Time_Of_Day,
	Date_Of_Day,
	Month_Of_Day,
	Year_Of_Day
	FROM Roles
	WHERE Role_id = @pRole_id
	ORDER BY Role_id desc
	
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_SaleReturnedItems_By_ItemId]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_SaleReturnedItems_By_ItemId] --7,3
	
	@pPackage_id				int,
	@pItem_id					int

AS
BEGIN
	SELECT 
		sd.SaleReturnDetail_id,
		s.SaleOrder_id,
		s.SaleReturn_id,
		Package_id,
		Item_id,
		(SELECT Item_Name FROM Items where Item_id = sd.Item_id) as Item_Name,
		Return_Qty,
		Received_Qty,
		ReturnQty_Cost

		FROM SaleReturn AS s
		INNER JOIN SaleReturnDetail as sd on s.SaleReturn_id = sd.SaleReturn_id

		
		WHERE sd.Package_id = @pPackage_id
		AND Item_id = @pItem_id
		
		--ORDER BY s.SaleOrder_id desc
	
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_SaleReturnedItems_By_SO_ID]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_SaleReturnedItems_By_SO_ID] --7
	
	@pSalesOrder_id				int

AS
BEGIN
	SELECT 
		sd.SaleReturnDetail_id,
		s.SaleOrder_id,
		(SELECT PriceUnit FROM SalesOrder_Details WHERE SalesOrder_id = @pSalesOrder_id and ItemId = sd.Item_id) as PriceUnit,
		(SELECT Qty FROM SalesOrder_Details WHERE SalesOrder_id = @pSalesOrder_id and ItemId = sd.Item_id) as Qty,
		s.SaleReturn_id,
		Package_id,
		Item_id,
		(SELECT Item_Name FROM Items where Item_id = sd.Item_id) as Item_Name,
		Return_Qty,
		Received_Qty,
		ReturnQty_Cost

		FROM SaleReturn AS s
		INNER JOIN SaleReturnDetail as sd on s.SaleReturn_id = sd.SaleReturn_id

		
		WHERE s.SaleOrder_id = @pSalesOrder_id
		
		--ORDER BY s.SaleOrder_id desc
	
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_SaleReturns_By_SO_ID]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_SaleReturns_By_SO_ID] --5
	
	@pSalesOrder_id				int

AS
BEGIN
	SELECT 
		SaleReturn_id,
		SaleOrder_id,
		SaleReturnNo,
		SaleReturn_Date,
		SaleReturn_Status,
		(SELECT SUM(cast(ReturnQty_Cost as int)) FROM SaleReturnDetail where SaleReturn_id = s.SaleReturn_id) as TotalReturn_Cost,
		Time_Of_Day,
		Date_Of_Day,
		Month_Of_Day,
		Year_Of_Day
		FROM SaleReturn AS s
		WHERE s.SaleOrder_id = @pSalesOrder_id
		ORDER BY s.SaleOrder_id desc
	
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_SalesOrder_By_ID]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_SalesOrder_By_ID] --1
	
	@pSalesOrder_id					int
AS
BEGIN
	SELECT
		id as SalesOrder_id,
		SaleOrderNo,
		--(SELECT case when (SELECT Package_Status FROM SO_Packages WHERE SaleOrder_id = 1) = 'NULL' then null else (SELECT Package_Status FROM SO_Packages WHERE SaleOrder_id = 1) end) as DeliveryStatus,
		(SELECT SUM(cast(Package_Cost as int)) FROM SO_Packages WHERE SaleOrder_id = @pSalesOrder_id) as Package_Cost,
		(SELECT SUM(cast(Shipment_Cost as int)) FROM Shipment WHERE SaleOrder_id = @pSalesOrder_id) as Shipment_Cost,
		(SELECT case when s.SO_Invoice_Status = 'Not Invoiced' then 'NULL' else Invoice_No end FROM SalesOrder_Invoices WHERE SalesOrder_id = s.id) as InvoiceNo,
		(SELECT id FROM SalesOrder_Details WHERE id = s.id) as salesOrderDetail_id,
		(SELECT DISTINCT Customer_id FROM SalesOrder_Details WHERE SalesOrder_id = s.id) as Customer_id,
		(SELECT Name FROM Contacts WHERE id = (SELECT DISTINCT Customer_id FROM SalesOrder_Details WHERE SalesOrder_id = s.id )) as CustomerName,
		(SELECT Address FROM Contacts WHERE id = (SELECT DISTINCT Customer_id FROM SalesOrder_Details WHERE SalesOrder_id = s.id )) as CustomerAddress,
		(SELECT Landline FROM Contacts WHERE id = (SELECT DISTINCT Customer_id FROM SalesOrder_Details WHERE SalesOrder_id = s.id )) as CustomerLandline,
		(SELECT Mobile FROM Contacts WHERE id = (SELECT DISTINCT Customer_id FROM SalesOrder_Details WHERE SalesOrder_id = s.id )) as CustomerMobile,
		(SELECT Email FROM Contacts WHERE id = (SELECT DISTINCT Customer_id FROM SalesOrder_Details WHERE SalesOrder_id = s.id )) as CustomerEmail,
		PremisesId,
		(SELECT Name FROM Premises WHERE id=s.UserId) as PremisesName,
		UserId,
		(SELECT attached_profile FROM Users WHERE id=s.UserId) as UserName,
		(SELECT id FROM SalesOrder_Invoices WHERE SalesOrder_id=s.id) as SO_Invoice_id,
		TotalItems,
		SO_Total_Amount,
		SO_Shipping_Charges,
		SO_Status,
		SO_Invoice_Status,
		SO_Shipment_Status,
		SO_Package_Status,
		SO_DateTime,
		SO_Expected_Shipment_Date,
		Enable,
		Time_Of_Day,
		Date_Of_Day,
		Month_Of_Day,
		Year_Of_Day,
		AddedBy

	FROM  SalesOrder as s
	WHERE s.id = @pSalesOrder_id

	ORDER BY s.id DESC
END



GO
/****** Object:  StoredProcedure [dbo].[proc_Select_SalesOrder_Item]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_SalesOrder_Item] --1
	
	@pSalesOrder_id					int
AS
BEGIN
	SELECT
		id,
		SalesOrder_id,
		ItemId,
		(SELECT DISTINCT Item_Name FROM Items WHERE Item_id = s.ItemId) as ItemName,
		Customer_id,
		(SELECT case when Packed_Item_Qty = 'NULL' then '0' else Packed_Item_Qty end FROM SO_Packages_Detail WHERE Item_id = s.ItemId and SaleOrder_id = @pSalesOrder_id and UnitPrice = s.PriceUnit) as PackgingQty,
		(SELECT case when Packed_Item_Qty = 'NULL' then '0' else Package_Cost end FROM SO_Packages_Detail WHERE Item_id = s.ItemId and SaleOrder_id = @pSalesOrder_id and UnitPrice = s.PriceUnit) as Package_Cost,
		(SELECT case when Packed_Item_Qty = 'NULL' then '0' else Package_No end FROM SO_Packages_Detail WHERE Item_id = s.ItemId and SaleOrder_id = @pSalesOrder_id and UnitPrice = s.PriceUnit) as Package_No,
		(SELECT case when SO_Invoice_Status = 'Not Invoiced' then CAST(0 AS BIT) else CAST(1 AS BIT) end FROM SalesOrder WHERE id = s.SalesOrder_id) as InvoicedStatus,
		(SELECT case when (SELECT SO_Invoice_Status FROM SalesOrder WHERE id = @pSalesOrder_id) = 'Not Invoiced' then '0' else Qty end FROM SalesOrder_Details WHERE SalesOrder_id = @pSalesOrder_id and ItemId = s.ItemId and PriceUnit = s.PriceUnit) as InvoicedQty,
		(SELECT Name FROM Contacts WHERE id = s.Customer_id) as CustomerName,
		(SELECT Address FROM Contacts WHERE id = s.Customer_id) as CustomerAddress,
		(SELECT Landline FROM Contacts WHERE id = s.Customer_id) as CustomerLandline,
		(SELECT Mobile FROM Contacts WHERE id = s.Customer_id) as CustomerMobile,
		(SELECT Email FROM Contacts WHERE id = s.Customer_id) as CustomerEmail,
		Qty,
		PriceUnit,
		MsrmntUnit

	FROM  SalesOrder_Details as s
	WHERE s.SalesOrder_id = @pSalesOrder_id

	ORDER BY s.id DESC
END
--CAST(0 AS BIT)


GO
/****** Object:  StoredProcedure [dbo].[proc_Select_SalesOrder_NotPackedItem]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_SalesOrder_NotPackedItem] --16
	
	@pSalesOrder_id					int,
	@pPackedQty_out					varchar(50) = NULL OUTPUT
AS
BEGIN
	SELECT
		id,
		SalesOrder_id,
		ItemId,
		(SELECT Item_Name FROM Items WHERE Item_id = s.ItemId) as ItemName,
		Customer_id,
		(SELECT case when Packed_Item_Qty = 'NULL' then '0' else Packed_Item_Qty end FROM SO_Packages_Detail WHERE Item_id = s.ItemId and SaleOrder_id = @pSalesOrder_id) as PackedQty,
		(SELECT Name FROM Contacts WHERE id = s.Customer_id) as CustomerName,
		(SELECT Address FROM Contacts WHERE id = s.Customer_id) as CustomerAddress,
		(SELECT Landline FROM Contacts WHERE id = s.Customer_id) as CustomerLandline,
		(SELECT Mobile FROM Contacts WHERE id = s.Customer_id) as CustomerMobile,
		(SELECT Email FROM Contacts WHERE id = s.Customer_id) as CustomerEmail,
		Qty,
		PriceUnit,
		MsrmntUnit
		

	FROM  SalesOrder_Details as s
	WHERE s.SalesOrder_id = @pSalesOrder_id
	--AND @pPackedQty_out.PackgingQty<>0

	ORDER BY s.id DESC
END
--CAST(0 AS BIT)


GO
/****** Object:  StoredProcedure [dbo].[proc_Select_SalesOrderDetail_BY_ItemID]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_SalesOrderDetail_BY_ItemID] --4,14
	
	@pItemId						int,
	@SalesOrder_id					int = NULL

AS
BEGIN
	SELECT
		id,
		SalesOrder_id,
		ItemId,
		(SELECT SO_Invoice_Status FROM SalesOrder WHERE id= s.SalesOrder_id) as SO_Invoice_Status,
		(SELECT Item_Name FROM Items WHERE Item_id = s.ItemId) as ItemName,
		Customer_id,
		(SELECT Name FROM Contacts WHERE id = s.Customer_id) as CustomerName,
		(SELECT Address FROM Contacts WHERE id = s.Customer_id) as CustomerAddress,
		(SELECT Landline FROM Contacts WHERE id = s.Customer_id) as CustomerLandline,
		(SELECT Mobile FROM Contacts WHERE id = s.Customer_id) as CustomerMobile,
		(SELECT Email FROM Contacts WHERE id = s.Customer_id) as CustomerEmail,
		Qty,
		PriceUnit,
		MsrmntUnit

	FROM  SalesOrder_Details as s
	WHERE s.ItemId = @pItemId
	AND s.SalesOrder_id= @SalesOrder_id

	ORDER BY s.ItemId DESC
END



GO
/****** Object:  StoredProcedure [dbo].[proc_Select_SalesOrders]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_SalesOrders] --'',2
	
	@pSaleOrderNo				nvarchar(50) = NULL,
	@pFrom						nvarchar(50) = NULL,
	@pTo						nvarchar(50) = NULL,
	@pAddedBy					int,
	@pEnable					int = NULL 

AS
BEGIN
	SELECT 
		id as SalesOrder_id,
		SaleOrderNo,
		--(SELECT case when (SELECT Package_Status FROM SO_Packages WHERE SaleOrder_id = s.id) = 'NULL' then null else (SELECT Package_Status FROM SO_Packages WHERE SaleOrder_id = s.id) end) as DeliveryStatus,
		(SELECT SUM(cast(Package_Cost as int)) FROM SO_Packages WHERE SaleOrder_id = s.id) as Package_Cost,
		(SELECT SUM(cast(Shipment_Cost as int)) FROM Shipment WHERE SaleOrder_id = s.id) as Shipment_Cost,
		(SELECT DISTINCT Customer_id FROM SalesOrder_Details WHERE SalesOrder_id = s.id ) as Customer_id,
		(SELECT Name FROM Contacts WHERE id = (SELECT DISTINCT Customer_id FROM SalesOrder_Details WHERE SalesOrder_id = s.id )) as CustomerName,
		(SELECT Address FROM Contacts WHERE id = (SELECT DISTINCT Customer_id FROM SalesOrder_Details WHERE SalesOrder_id = s.id )) as CustomerAddress,
		(SELECT Landline FROM Contacts WHERE id = (SELECT DISTINCT Customer_id FROM SalesOrder_Details WHERE SalesOrder_id = s.id )) as CustomerLandline,
		(SELECT Mobile FROM Contacts WHERE id = (SELECT DISTINCT Customer_id FROM SalesOrder_Details WHERE SalesOrder_id = s.id )) as CustomerMobile,
		(SELECT Email FROM Contacts WHERE id = (SELECT DISTINCT Customer_id FROM SalesOrder_Details WHERE SalesOrder_id = s.id )) as CustomerEmail,
		PremisesId,
		(SELECT Name FROM Premises WHERE id=s.PremisesId) as PremisesName,
		UserId,
		(SELECT attached_profile FROM Users WHERE id=s.UserId) as UserName,
		(SELECT id FROM SalesOrder_Invoices WHERE SalesOrder_id=s.id) as SO_Invoice_id,
		TotalItems,
		SO_Total_Amount,
		SO_Shipping_Charges,
		SO_Status,
		SO_Invoice_Status,
		SO_Shipment_Status,
		SO_Package_Status,
		SO_DateTime,
		SO_Expected_Shipment_Date,
		Enable,
		Time_Of_Day,
		Date_Of_Day,
		Month_Of_Day,
		Year_Of_Day,
		AddedBy
	FROM SalesOrder AS s
	WHERE ((@pSaleOrderNo is null) or SaleOrderNo LIKE '%' + @pSaleOrderNo + '%')
	AND ((@pFrom is null) or (convert(datetime, s.Date_Of_Day, 103) >= convert(datetime, @pFrom, 103)))
	AND ((@pTo is null) or (convert(datetime, s.Date_Of_Day, 103) <= convert(datetime, @pTo, 103)))
	AND s.AddedBy = @pAddedBy
	AND ((@pEnable is null) or (s.Enable = @pEnable))
	ORDER BY s.id desc
	
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_Shops]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_Shops]
	
	@pName					nvarchar(50) = NULL,
	@pFrom					nvarchar(50) = NULL,
	@pTo					nvarchar(50) = NULL,
	@pEnable				int = NULL 

AS
BEGIN
	SELECT 
	id,
	Name,
	Pc_Mac_Address,
	Phone,
	City,
	(SELECT Name FROM Cities Where id = p.City) as CityName,
	Country,
	(SELECT Name FROM Countries Where id = p.Country) as CountryName,
	Address,
	Office,
	Factory,
	Store,
	Shop,
	Enable,
	AddedBy,
	UpdatedBy,
	Delete_Request_By,
	Delete_Status,
	UserId,
	Time_Of_Day,
	Date_Of_Day,
	Month_Of_Day,
	Year_Of_Day

	FROM Premises AS p
	WHERE ((@pName is null) or (Name LIKE '%' + @pName + '%' or Pc_Mac_Address LIKE '%' + @pName + '%' or Phone LIKE '%' + @pName + '%' or (SELECT Name FROM Countries Where id = p.Country) LIKE '%' + @pName + '%' or (SELECT Name FROM Cities Where id = p.City) LIKE '%' + @pName + '%' or Address LIKE '%' + @pName + '%'))
	AND ((@pFrom is null) or (convert(datetime, p.Date_Of_Day, 103) >= convert(datetime, @pFrom, 103)))
	AND ((@pTo is null) or (convert(datetime, p.Date_Of_Day, 103) <= convert(datetime, @pTo, 103)))
	AND Shop = '1'
	AND ((@pEnable is null) or (Enable = @pEnable))
	ORDER BY id desc

END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_SO_By_ItemID]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_SO_By_ItemID] --1, 'Test Customer New'
	
	@pItem_id				int = 0,
	@pSearch				nvarchar(50) = NULL

AS
BEGIN
	SELECT 
		SalesOrder_id as SalesOrder_id,
		(SELECT SaleOrderNo FROM SalesOrder WHERE id = s.SalesOrder_id) as SaleOrderNo,
		Customer_id,
		(SELECT Name FROM Contacts WHERE id = Customer_id) as CustomerName,
		Qty as QtyOrdered,
		PriceUnit as Price,
		(SELECT SO_Total_Amount FROM SalesOrder WHERE id = s.SalesOrder_id) as SO_Total_Amount,
		(SELECT SO_Status FROM SalesOrder WHERE id = s.SalesOrder_id) as SO_Status,
		(SELECT SO_DateTime FROM SalesOrder WHERE id = s.SalesOrder_id) as SO_DateTime

	FROM SalesOrder_Details AS s
	WHERE ((@pSearch is null) or (SELECT SaleOrderNo FROM SalesOrder WHERE id = s.SalesOrder_id) LIKE '%' + @pSearch + '%' or (SELECT Name FROM Contacts WHERE id = Customer_id) LIKE '%' + @pSearch + '%')
	AND (ItemId = @pItem_id)
	ORDER BY (SELECT SaleOrderNo FROM SalesOrder WHERE id = s.SalesOrder_id) desc
	
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_SO_Invoice_By_ID]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_SO_Invoice_By_ID] --11
	
	@pSalesOrder_id					int
AS
BEGIN
	SELECT
		id,
		SalesOrder_id,
		Invoice_No,
		Invoice_Status,
		Invoice_Amount,
		Amount_Paid,
		Balance_Amount,
		InvoiceDateTime,
		InvoiceDueDate,
		Time_Of_Day,
		Date_Of_Day,
		Month_Of_Day,
		Year_Of_Day

	FROM  SalesOrder_Invoices as s
	WHERE s.SalesOrder_id = @pSalesOrder_id

	ORDER BY s.SalesOrder_id DESC
END



GO
/****** Object:  StoredProcedure [dbo].[proc_Select_SO_Invoice_By_InvoiceID]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_SO_Invoice_By_InvoiceID] --11
	
	@pInvoiceId					int
AS
BEGIN
	SELECT
		id,
		SalesOrder_id,
		Invoice_No,
		Invoice_Status,
		Invoice_Amount,
		Amount_Paid,
		Balance_Amount,
		InvoiceDateTime,
		InvoiceDueDate,
		Time_Of_Day,
		Date_Of_Day,
		Month_Of_Day,
		Year_Of_Day

	FROM  SalesOrder_Invoices as s
	WHERE s.id = @pInvoiceId

	ORDER BY s.SalesOrder_id DESC
END



GO
/****** Object:  StoredProcedure [dbo].[proc_Select_SO_Payments_By_invoiceID]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_SO_Payments_By_invoiceID] --44
	
	@pSO_Invoice_id				int = 0

AS
BEGIN
	SELECT 
		SO_Payment_id,
		SO_Invoice_id,
		(SELECT Invoice_No FROM SalesOrder_Invoices where id=p.SO_Invoice_id) as Invoice_No,
		(SELECT Invoice_Amount FROM SalesOrder_Invoices where id=p.SO_Invoice_id) as Invoice_Amount,
		SO_Payment_Mode,
		SO_BankCharges,
		SO_Payment_Date,
		SO_Total_Amount,
		SO_Paid_Amount,
		SO_Balance_Amount,
		Time_Of_Day,
		Date_Of_Day,
		Month_Of_Day,
		Year_Of_Day

	FROM SO_Payments AS p
	WHERE p.SO_Invoice_id = @pSO_Invoice_id 
	ORDER BY p.SO_Payment_id desc
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_Sold_Items_By_CustomerId]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_Sold_Items_By_CustomerId] --26

	@pCustomer_id					int,
	@pSearch						nvarchar(50) = NULL
	
	
AS
BEGIN
	SELECT
	sd.id,
	SalesOrder_id,
	s.SaleOrderNo,
	sd.ItemId,
	(SELECT Item_Name FROM Items WHERE Item_id = sd.ItemId) as Item_Name,
	sd.Customer_id,
	sd.Qty,
	sd.PriceUnit,
	sd.MsrmntUnit,
	s.Date_Of_Day,
	s.Time_Of_Day

	FROM  SalesOrder_Details as sd
	INNER JOIN SalesOrder as s on sd.SalesOrder_id = s.id
	WHERE ((@pSearch is null) or s.SaleOrderNo LIKE '%' + @pSearch + '%' or (SELECT Item_Name FROM Items WHERE Item_id = sd.ItemId) LIKE '%' + @pSearch + '%' or s.Date_Of_Day LIKE '%' + @pSearch + '%' or s.Time_Of_Day LIKE '%' + @pSearch + '%')
	AND (sd.Customer_id = @pCustomer_id)

	ORDER BY s.id DESC
END



GO
/****** Object:  StoredProcedure [dbo].[proc_Select_Stock]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_Stock] --11

	@pName						nvarchar(50) = NULL,
	@pFrom						nvarchar(50) = NULL,
	@pTo						nvarchar(50) = NULL

AS
BEGIN
	SELECT 
	Stock_id,
	Item_id,
	(SELECT Item_Name From Items Where item_id = s.Item_id) as Item_Name,
	CASE WHEN Physical_Quantity IS NULL THEN '0' ELSE Physical_Quantity END as In_Stock,
	(SELECT CASE WHEN CAST(SUM(CAST(Qty as int)) as nvarchar)  IS NULL THEN '0' ELSE CAST(SUM(CAST(Qty as int)) as nvarchar) END FROM SalesOrder_Details WHERE ItemId = s.Item_id) as Sold
		
	FROM Stock AS s
	WHERE ((@pName is null) or (SELECT Item_Name From Items Where item_id = s.Item_id) LIKE '%' + @pName + '%')
	AND ((@pFrom is null) or (convert(datetime, s.Date_Of_Day, 103) >= convert(datetime, @pFrom, 103)))
	AND ((@pTo is null) or (convert(datetime, s.Date_Of_Day, 103) <= convert(datetime, @pTo, 103)))
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_Stock_By_ItemID]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_Stock_By_ItemID] --9
	@pItem_id		nvarchar(MAX)

AS
BEGIN
	SELECT 
	Stock_id,
	Item_id,
	Physical_Quantity,
	Physical_Avail_ForSale,
	Physical_Committed,
	Accounting_Quantity,
	Acc_Avail_ForSale,
	Acc_Commited,
	OpeningStock,
	ReorderLevel,
	Time_Of_Day,
	Date_Of_Day,
	Month_Of_Day,
	Year_Of_Day

	FROM Stock AS s
	WHERE s.Item_id = @pItem_id
	ORDER BY s.Item_id desc
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_Stores]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_Stores]

	@pName					nvarchar(50) = NULL,
	@pFrom					nvarchar(50) = NULL,
	@pTo					nvarchar(50) = NULL,
	@pEnable				int = NULL 
	
AS
BEGIN
	SELECT 
	id,
	Name,
	Pc_Mac_Address,
	Phone,
	City,
	(SELECT Name FROM Cities Where id = p.City) as CityName,
	Country,
	(SELECT Name FROM Countries Where id = p.Country) as CountryName,
	Address,
	Office,
	Factory,
	Store,
	Shop,
	Enable,
	AddedBy,
	UpdatedBy,
	Delete_Request_By,
	Delete_Status,
	UserId,
	Time_Of_Day,
	Date_Of_Day,
	Month_Of_Day,
	Year_Of_Day

	FROM Premises AS p
	WHERE ((@pName is null) or (Name LIKE '%' + @pName + '%' or Pc_Mac_Address LIKE '%' + @pName + '%' or Phone LIKE '%' + @pName + '%' or (SELECT Name FROM Countries Where id = p.Country) LIKE '%' + @pName + '%' or (SELECT Name FROM Cities Where id = p.City) LIKE '%' + @pName + '%' or Address LIKE '%' + @pName + '%'))
	AND ((@pFrom is null) or (convert(datetime, p.Date_Of_Day, 103) >= convert(datetime, @pFrom, 103)))
	AND ((@pTo is null) or (convert(datetime, p.Date_Of_Day, 103) <= convert(datetime, @pTo, 103)))
	AND Store = '1'
	AND ((@pEnable is null) or (Enable = @pEnable))
	ORDER BY id desc

END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_Token]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Salman Ahmed>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_Token] --1
	-- Add the parameters for the stored procedure here

	@pauthenticationToken						nvarchar(MAX)


AS
	SELECT 
	id,
	User_id,
	Token,
	IssueDate,
	ExpiryDate

	FROM Tokens 
	WHERE Token = @pauthenticationToken







GO
/****** Object:  StoredProcedure [dbo].[proc_Select_Unit_By_Name]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_Unit_By_Name] --'',null,null,null

	@pUnit_Name						nvarchar(50) = NULL
		
AS
BEGIN
	SELECT
	Unit_id,
	Unit_Name,
	Unit_Code,
	Enable,
	AddedBy,
	UpdatedBy,
	Delete_Request_By,
	Delete_Status,
	Time_Of_Day,
	Date_Of_Day,
	Month_Of_Day,
	Year_Of_Day

	FROM  Units as u
	WHERE Unit_Name = @pUnit_Name
END



GO
/****** Object:  StoredProcedure [dbo].[proc_Select_Units]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_Units] 

	@pUnit_Name					nvarchar(50) = NULL,
	@pFrom						nvarchar(50) = NULL,
	@pTo						nvarchar(50) = NULL,
	@pEnable					int = NULL 
	
AS
BEGIN
	SELECT 
	Unit_id,
	Unit_Name,
	Unit_Code,
	Enable,
	Delete_Request_By,
	Delete_Status,
	Time_Of_Day,
	Date_Of_Day,
	Month_Of_Day,
	Year_Of_Day
	FROM Units AS i
	WHERE ((@pUnit_Name is null) or Unit_Name LIKE '%' + @pUnit_Name + '%')
	AND ((@pFrom is null) or (convert(datetime, i.Date_Of_Day, 103) >= convert(datetime, @pFrom, 103)))
	AND ((@pTo is null) or (convert(datetime, i.Date_Of_Day, 103) <= convert(datetime, @pTo, 103)))
	AND ((@pEnable is null) or (Enable = @pEnable))
	ORDER BY Unit_id desc
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_Units_By_ID]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_Units_By_ID] 
	
	@pUnit_id			nvarchar(50)

AS
BEGIN
	SELECT 
	Unit_id,
	Unit_Name,
	Unit_Code,
	Enable,
	Delete_Request_By,
	Delete_Status,
	Time_Of_Day,
	Date_Of_Day,
	Month_Of_Day,
	Year_Of_Day
	FROM Units AS i
	WHERE Unit_id = @pUnit_id
	ORDER BY Unit_id desc
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_Users]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_Users] 
	
	@pemail					nvarchar(50) = NULL,
	@pFrom					nvarchar(50) = NULL,
	@pTo					nvarchar(50) = NULL,
	@pEnable				int = NULL 

AS
BEGIN
	SELECT 
	id,
	email,
	password,
	attached_profile,
	(SELECT Salutation FROM Contacts WHERE id = u.attached_profile) as Salutation,
	(SELECT Name FROM Contacts WHERE id = u.attached_profile) as Name,
	(SELECT Company FROM Contacts WHERE id = u.attached_profile) as Company,
	(SELECT Designation FROM Contacts WHERE id = u.attached_profile) as Designation,
	(SELECT Landline FROM Contacts WHERE id = u.attached_profile) as Landline,
	(SELECT Mobile FROM Contacts WHERE id = u.attached_profile) as Mobile,
	(SELECT Email FROM Contacts WHERE id = u.attached_profile) as ContactEmail,
	(SELECT Address FROM Contacts WHERE id = u.attached_profile) as Address,
	(SELECT AddressLandline FROM Contacts WHERE id = u.attached_profile) as AddressLandline,
	(SELECT City FROM Contacts WHERE id = u.attached_profile) as City,
	(SELECT Name FROM Cities WHERE id = (SELECT City FROM Contacts WHERE id = u.attached_profile)) as CityName,
	(SELECT Country FROM Contacts WHERE id = u.attached_profile) as Country,
	(SELECT Name FROM Countries WHERE id = (SELECT Country FROM Contacts WHERE id = u.attached_profile)) as CountryName,

	(SELECT BankAccountNumber FROM Contacts WHERE id = u.attached_profile) as BankAccountNumber,

	Premises_id,
	(SELECT Name FROM Premises WHERE id = u.Premises_id) as PremisesName,
	(SELECT Phone FROM Premises WHERE id = u.Premises_id) as PremisesPhone,
	(SELECT City FROM Premises WHERE id = u.Premises_id) as PremisesCity,
	(SELECT Name FROM Cities WHERE id = (SELECT City FROM Premises WHERE id = u.Premises_id)) as PremisesCityName,
	(SELECT Country FROM Premises WHERE id = u.Premises_id) as PremisesCountry,
	(SELECT Name FROM Countries WHERE id = (SELECT Country FROM Premises WHERE id = u.Premises_id)) as PremisesCountryName,
	(SELECT Address FROM Premises WHERE id = u.Premises_id) as PremisesAddress,
	--pao,
	--paf,
	--pas,
	--pas_,
	--pav,
	--pap,
	--pac,
	--pas__,
	--pae,
	--pap_,
	--pai,
	--pas___,
	--pau,
	--puo,
	--puf,
	--pus,
	--pus_,
	--puv,
	--pup,
	--puc,
	--pus__,
	--pue,
	--pup_,
	--pui,
	--pus___,
	--puu,
	--pdo,
	--pdf,
	--pds,
	--pds_,
	--pdv,
	--pdp,
	--pdc,
	--pds__,
	--pde,
	--pdp_,
	--pdi,
	--pds___,
	--pdu,
	--pvo,
	--pvf,
	--pvs,
	--pvs_,
	--pvv,
	--pvp,
	--pvc,
	--pvs__,
	--pve,
	--pvp_,
	--pvi,
	--pvs___,
	--pvu,
	--pvol,
	--pvfl,
	--pvsl,
	--pvsl_,
	--pvvl,
	--pvpl,
	--pvcl,
	--pvsl__,
	--pvel,
	--pvpl_,
	--pvil,
	--pvsl___,
	--pvul,
	Enable,
	Delete_Request_By,
	Delete_Status,
	Time_Of_Day,
	Date_Of_Day,
	Month_Of_Day,
	Year_Of_Day
	FROM Users as u
	WHERE ((@pemail is null) or email LIKE '%' + @pemail + '%')
	AND ((@pFrom is null) or (convert(datetime, u.Date_Of_Day, 103) >= convert(datetime, @pFrom, 103)))
	AND ((@pTo is null) or (convert(datetime, u.Date_Of_Day, 103) <= convert(datetime, @pTo, 103)))
	AND ((@pEnable is null) or (Enable = @pEnable))
	ORDER BY id desc
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_Users_By_ID]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_Users_By_ID] 
	
	@pid				int = 0

AS
BEGIN
	SELECT 
	id,
	email,
	password,
	attached_profile,
	(SELECT Salutation FROM Contacts WHERE id = u.attached_profile) as Salutation,
	(SELECT Name FROM Contacts WHERE id = u.attached_profile) as Name,
	(SELECT Company FROM Contacts WHERE id = u.attached_profile) as Company,
	(SELECT Designation FROM Contacts WHERE id = u.attached_profile) as Designation,
	(SELECT Landline FROM Contacts WHERE id = u.attached_profile) as Landline,
	(SELECT Mobile FROM Contacts WHERE id = u.attached_profile) as Mobile,
	(SELECT Email FROM Contacts WHERE id = u.attached_profile) as ContactEmail,
	(SELECT Address FROM Contacts WHERE id = u.attached_profile) as Address,
	(SELECT AddressLandline FROM Contacts WHERE id = u.attached_profile) as AddressLandline,
	(SELECT City FROM Contacts WHERE id = u.attached_profile) as City,
	(SELECT Name FROM Cities WHERE id = (SELECT City FROM Contacts WHERE id = u.attached_profile)) as CityName,
	(SELECT Country FROM Contacts WHERE id = u.attached_profile) as Country,
	(SELECT Name FROM Countries WHERE id = (SELECT Country FROM Contacts WHERE id = u.attached_profile)) as CountryName,

	(SELECT BankAccountNumber FROM Contacts WHERE id = u.attached_profile) as BankAccountNumber,

	Premises_id,
	(SELECT Name FROM Premises WHERE id = u.Premises_id) as PremisesName,
	(SELECT Phone FROM Premises WHERE id = u.Premises_id) as PremisesPhone,
	(SELECT City FROM Premises WHERE id = u.Premises_id) as PremisesCity,
	(SELECT Name FROM Cities WHERE id = (SELECT City FROM Premises WHERE id = u.Premises_id)) as PremisesCityName,
	(SELECT Country FROM Premises WHERE id = u.Premises_id) as PremisesCountry,
	(SELECT Name FROM Countries WHERE id = (SELECT Country FROM Premises WHERE id = u.Premises_id)) as PremisesCountryName,
	(SELECT Address FROM Premises WHERE id = u.Premises_id) as PremisesAddress,
	--pao,
	--paf,
	--pas,
	--pas_,
	--pav,
	--pap,
	--pac,
	--pas__,
	--pae,
	--pap_,
	--pai,
	--pas___,
	--pau,
	--puo,
	--puf,
	--pus,
	--pus_,
	--puv,
	--pup,
	--puc,
	--pus__,
	--pue,
	--pup_,
	--pui,
	--pus___,
	--puu,
	--pdo,
	--pdf,
	--pds,
	--pds_,
	--pdv,
	--pdp,
	--pdc,
	--pds__,
	--pde,
	--pdp_,
	--pdi,
	--pds___,
	--pdu,
	--pvo,
	--pvf,
	--pvs,
	--pvs_,
	--pvv,
	--pvp,
	--pvc,
	--pvs__,
	--pve,
	--pvp_,
	--pvi,
	--pvs___,
	--pvu,
	--pvol,
	--pvfl,
	--pvsl,
	--pvsl_,
	--pvvl,
	--pvpl,
	--pvcl,
	--pvsl__,
	--pvel,
	--pvpl_,
	--pvil,
	--pvsl___,
	--pvul,
	Enable,
	Delete_Request_By,
	Delete_Status,
	Time_Of_Day,
	Date_Of_Day,
	Month_Of_Day,
	Year_Of_Day
	FROM Users as u
	WHERE id = @pid
	ORDER BY id desc
END

GO
/****** Object:  StoredProcedure [dbo].[proc_Select_Vendor_By_Name]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_Vendor_By_Name] --'',null,null,null

	@pName						nvarchar(50) = NULL
		
AS
BEGIN
	SELECT
	id,
	Salutation,
	Image,
	Name,
	Company,
	Designation,
	Landline,
	Mobile,
	Email,
	Website,
	Address,
	AddressLandline,
	City,
	Country,
	BankAccountNumber,
	PaymentMethod,
	Vendor,
	Customer,
	Employee,
	Enable,
	AddedBy,
	UpdatedBy,
	Delete_Request_By,
	Delete_Status,
	TimeOfDay,
	DateOfDay
	MonthOfDay,
	YearOfDay

	FROM Contacts as c
	WHERE Name = @pName
	AND Vendor = 1
	
END



GO
/****** Object:  StoredProcedure [dbo].[proc_Select_Vendors]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Select_Vendors]  --'','26/07/2019','26/07/2019'

	@pName						nvarchar(50) = NULL,
	@pFrom						nvarchar(50) = NULL,
	@pTo						nvarchar(50) = NULL,
	@pEnable				int = NULL 

AS
BEGIN
	SELECT 
	id,
	Salutation,
	Image,
	Name,
	Company,
	Designation,
	Landline,
	Mobile,
	Email,
	Website,
	Address,
	AddressLandline,
	City,
	Country,
	BankAccountNumber,
	PaymentMethod,
	Vendor,
	Customer,
	Employee,
	Enable,
	AddedBy,
	UpdatedBy,
	Delete_Request_By,
	Delete_Status,
	TimeOfDay,
	DateOfDay,
	MonthOfDay,
	YearOfDay

	FROM Contacts AS c
	WHERE ((@pName is null) or Name LIKE '%' + @pName + '%')
	AND ((@pFrom is null) or (convert(datetime, c.DateOfDay, 103) >= convert(datetime, @pFrom, 103)))
	AND ((@pTo is null) or (convert(datetime, c.DateOfDay, 103) <= convert(datetime, @pTo, 103)))
	AND c.Vendor = '1'
	AND ((@pEnable is null) or (Enable = @pEnable))
	ORDER BY c.id desc
END

GO
/****** Object:  StoredProcedure [dbo].[proc_SendMessage]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Salman Ahmed>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_SendMessage]
	-- Add the parameters for the stored procedure here

		@pSender_id						int,
		@pReceiver_id					int,
		@pMessage						nvarchar(MAX),
		@pStatus						nvarchar(MAX),
		@pEnable						int,
		@pDate							nvarchar(50),
		@pTime							nvarchar(50),
		@pMonth							nvarchar(50),
		@pYear							nvarchar(50),
		@pFlag							bit = 0 OUTPUT,
		@pDesc							varchar(MAX) = NULL OUTPUT

AS
BEGIN
 
 BEGIN TRAN	
					INSERT INTO Messages (Sender_id,Receiver_id,Message,Status,Enable,Date,Time,Month,Year)
	
					VALUES(@pSender_id,@pReceiver_id,@pMessage,@pStatus,@pEnable,@pDate,@pTime,@pMonth,@pYear)	

					IF @@Error = 0
						BEGIN	
							SET @pFlag = 1
							SET @pDesc =  'Message Sent Successfully'
							COMMIT TRAN
						END
						ELSE
						BEGIN
							SET @pFlag = 0
							SET @pDesc =  @@error
							ROLLBACK  TRAN
						END

END 

GO
/****** Object:  StoredProcedure [dbo].[proc_Update_Brand_Delete_Request]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Update_Brand_Delete_Request]

		@dt AS [DeleteBrandsRequested_Datatable]		READONLY,
		@ptype				nvarchar(50)

AS
	BEGIN TRAN
	
			Select [Brand_id],[Enable],[Delete_Request_By],[Delete_Status] FROM @dt 
			UPDATE Brands
			SET	Brands.Enable	= Table_B.Enable,
				Brands.Delete_Request_By	= Table_B.Delete_Request_By,
				Brands.Delete_Status = Table_B.Delete_Status
			FROM
				Brands AS Table_A
				INNER JOIN @dt AS Table_B
				ON Table_A.[Brand_id] = Table_B.[Brand_id]
			WHERE Table_B.[Brand_id] <> 0 and Table_B.[Brand_id] = Table_A.[Brand_id] 

			IF @@Error = 0
				BEGIN	
				DECLARE @prerowCount 	BIGINT
				DECLARE @rowCount 	BIGINT
				SET @prerowCount = (SELECT case when count is NULL then 0 else count end FROM DeleteRequests where Type = @ptype)
				SET @rowCount = (SELECT COUNT(*) FROM @dt)

					UPDATE DeleteRequests set Count = @prerowCount + @rowCount where Type = @ptype					
				END
		Commit Tran		


GO
/****** Object:  StoredProcedure [dbo].[proc_Update_Category_Delete_Request]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Update_Category_Delete_Request]

		@dt AS [DeleteCategoryRequested_Datatable]		READONLY,
		@ptype				nvarchar(50)

AS
	BEGIN TRAN
	
			Select [Category_id],[Enable],[Delete_Request_By],[Delete_Status] FROM @dt 
			UPDATE Categories
			SET	Categories.Enable	= Table_B.Enable,
				Categories.Delete_Request_By	= Table_B.Delete_Request_By,
				Categories.Delete_Status = Table_B.Delete_Status
			FROM
				Categories AS Table_A
				INNER JOIN @dt AS Table_B
				ON Table_A.[Category_id] = Table_B.[Category_id]
			WHERE Table_B.[Category_id] <> 0 and Table_B.[Category_id] = Table_A.[Category_id] 

			IF @@Error = 0
				BEGIN	
				DECLARE @prerowCount 	BIGINT
				DECLARE @rowCount 	BIGINT
				SET @prerowCount = (SELECT case when count is NULL then 0 else count end FROM DeleteRequests where Type = @ptype)
				SET @rowCount = (SELECT COUNT(*) FROM @dt)

					UPDATE DeleteRequests set Count = @prerowCount + @rowCount where Type = @ptype					
				END
		Commit Tran	
GO
/****** Object:  StoredProcedure [dbo].[proc_Update_Cities_Delete_Request]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Update_Cities_Delete_Request]

		@dt AS [DeleteCitiesRequested_Datatable]		READONLY,
		@ptype				nvarchar(50)

AS
	BEGIN TRAN
	
			Select [id],[Enable],[Delete_Request_By],[Delete_Status] FROM @dt 
			UPDATE Cities
			SET	Cities.Enable	= Table_B.Enable,
				Cities.Delete_Request_By	= Table_B.Delete_Request_By,
				Cities.Delete_Status = Table_B.Delete_Status
			FROM
				Cities AS Table_A
				INNER JOIN @dt AS Table_B
				ON Table_A.[id] = Table_B.[id]
			WHERE Table_B.[id] <> 0 and Table_B.[id] = Table_A.[id] 

			IF @@Error = 0
				BEGIN	
				DECLARE @prerowCount 	BIGINT
				DECLARE @rowCount 	BIGINT
				SET @prerowCount = (SELECT case when count is NULL then 0 else count end FROM DeleteRequests where Type = @ptype)
				SET @rowCount = (SELECT COUNT(*) FROM @dt)

					UPDATE DeleteRequests set Count = @prerowCount + @rowCount where Type = @ptype					
				END
		Commit Tran	


GO
/****** Object:  StoredProcedure [dbo].[proc_Update_Company_Delete_Request]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Update_Company_Delete_Request]

		@dt AS [DeleteCompaniesRequested_Datatable]		READONLY,
		@ptype				nvarchar(50)

AS
	BEGIN TRAN
	
			Select [id],[Enable],[Delete_Request_By],[Delete_Status] FROM @dt 
			UPDATE Companies
			SET	Companies.Enable	= Table_B.Enable,
				Companies.Delete_Request_By	= Table_B.Delete_Request_By,
				Companies.Delete_Status = Table_B.Delete_Status
			FROM
				Companies AS Table_A
				INNER JOIN @dt AS Table_B
				ON Table_A.[id] = Table_B.[id]
			WHERE Table_B.[id] <> 0 and Table_B.[id] = Table_A.[id] 

			IF @@Error = 0
				BEGIN	
				DECLARE @prerowCount 	BIGINT
				DECLARE @rowCount 	BIGINT
				SET @prerowCount = (SELECT case when count is NULL then 0 else count end FROM DeleteRequests where Type = @ptype)
				SET @rowCount = (SELECT COUNT(*) FROM @dt)

					UPDATE DeleteRequests set Count = @prerowCount + @rowCount where Type = @ptype					
				END
		Commit Tran			
GO
/****** Object:  StoredProcedure [dbo].[proc_Update_Contacts_Delete_Request]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Update_Contacts_Delete_Request]

		@dt AS [DeleteContactsRequested_Datatable]		READONLY,
		@ptype				nvarchar(50)

AS
	BEGIN TRAN
	
			Select [id],[Enable],[Delete_Request_By],[Delete_Status] FROM @dt 
			UPDATE Contacts
			SET	Contacts.Enable	= Table_B.Enable,
				Contacts.Delete_Request_By	= Table_B.Delete_Request_By,
				Contacts.Delete_Status = Table_B.Delete_Status
			FROM
				Contacts AS Table_A
				INNER JOIN @dt AS Table_B
				ON Table_A.[id] = Table_B.[id]
			WHERE Table_B.[id] <> 0 and Table_B.[id] = Table_A.[id] 

			IF @@Error = 0
				BEGIN	
				DECLARE @prerowCount 	BIGINT
				DECLARE @rowCount 	BIGINT
				SET @prerowCount = (SELECT case when count is NULL then 0 else count end FROM DeleteRequests where Type = @ptype)
				SET @rowCount = (SELECT COUNT(*) FROM @dt)

					UPDATE DeleteRequests set Count = @prerowCount + @rowCount where Type = @ptype					
				END
		Commit Tran			


GO
/****** Object:  StoredProcedure [dbo].[proc_Update_Countries_Delete_Request]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Update_Countries_Delete_Request]

		@dt AS [DeleteCountriesRequested_Datatable]		READONLY,
		@ptype				nvarchar(50)

AS
	BEGIN TRAN
	
			Select [id],[Enable],[Delete_Request_By],[Delete_Status] FROM @dt 
			UPDATE Countries
			SET	Countries.Enable	= Table_B.Enable,
				Countries.Delete_Request_By	= Table_B.Delete_Request_By,
				Countries.Delete_Status = Table_B.Delete_Status
			FROM
				Countries AS Table_A
				INNER JOIN @dt AS Table_B
				ON Table_A.[id] = Table_B.[id]
			WHERE Table_B.[id] <> 0 and Table_B.[id] = Table_A.[id] 

			IF @@Error = 0
				BEGIN	
				DECLARE @prerowCount 	BIGINT
				DECLARE @rowCount 	BIGINT
				SET @prerowCount = (SELECT case when count is NULL then 0 else count end FROM DeleteRequests where Type = @ptype)
				SET @rowCount = (SELECT COUNT(*) FROM @dt)

					UPDATE DeleteRequests set Count = @prerowCount + @rowCount where Type = @ptype					
				END
		Commit Tran		


GO
/****** Object:  StoredProcedure [dbo].[proc_Update_Item_Delete_Request]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Update_Item_Delete_Request]

		@dt AS [DeleteItemRequested_Datatable]		READONLY,
		@ptype				nvarchar(50)

AS
	BEGIN TRAN
	
			Select [Item_id],[Enable],[Delete_Request_By],[Delete_Status] FROM @dt 
			UPDATE Items
			SET	Items.Enable	= Table_B.Enable,
				Items.Delete_Request_By	= Table_B.Delete_Request_By,
				Items.Delete_Status = Table_B.Delete_Status
			FROM
				Items AS Table_A
				INNER JOIN @dt AS Table_B
				ON Table_A.[Item_id] = Table_B.[Item_id]
			WHERE Table_B.[Item_id] <> 0 and Table_B.[Item_id] = Table_A.[Item_id] 

			IF @@Error = 0
				BEGIN	
				DECLARE @prerowCount 	BIGINT
				DECLARE @rowCount 	BIGINT
				SET @prerowCount = (SELECT case when count is NULL then 0 else count end FROM DeleteRequests where Type = @ptype)
				SET @rowCount = (SELECT COUNT(*) FROM @dt)

					UPDATE DeleteRequests set Count = @prerowCount + @rowCount where Type = @ptype					
				END
		Commit Tran			

GO
/****** Object:  StoredProcedure [dbo].[proc_Update_Manufacturer_Delete_Request]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Update_Manufacturer_Delete_Request]

		@dt AS [DeleteManufacturerRequested_Datatable]		READONLY,
		@ptype				nvarchar(50)

AS
	BEGIN TRAN
	
			Select [Manufacturer_id],[Enable],[Delete_Request_By],[Delete_Status] FROM @dt 
			UPDATE Manufacturers
			SET	Manufacturers.Enable	= Table_B.Enable,
				Manufacturers.Delete_Request_By	= Table_B.Delete_Request_By,
				Manufacturers.Delete_Status = Table_B.Delete_Status
			FROM
				Manufacturers AS Table_A
				INNER JOIN @dt AS Table_B
				ON Table_A.[Manufacturer_id] = Table_B.[Manufacturer_id]
			WHERE Table_B.[Manufacturer_id] <> 0 and Table_B.[Manufacturer_id] = Table_A.[Manufacturer_id] 

			IF @@Error = 0
				BEGIN	
				DECLARE @prerowCount 	BIGINT
				DECLARE @rowCount 	BIGINT
				SET @prerowCount = (SELECT case when count is NULL then 0 else count end FROM DeleteRequests where Type = @ptype)
				SET @rowCount = (SELECT COUNT(*) FROM @dt)

					UPDATE DeleteRequests set Count = @prerowCount + @rowCount where Type = @ptype					
				END
		Commit Tran				


GO
/****** Object:  StoredProcedure [dbo].[proc_Update_Premises_Delete_Request]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Update_Premises_Delete_Request]

		@dt AS [DeletePremisesRequested_Datatable]		READONLY,
		@ptype				nvarchar(50)

AS
	BEGIN TRAN
	
			Select [id],[Enable],[Delete_Request_By],[Delete_Status] FROM @dt 
			UPDATE Premises
			SET	Premises.Enable	= Table_B.Enable,
				Premises.Delete_Request_By	= Table_B.Delete_Request_By,
				Premises.Delete_Status = Table_B.Delete_Status
			FROM
				Premises AS Table_A
				INNER JOIN @dt AS Table_B
				ON Table_A.[id] = Table_B.[id]
			WHERE Table_B.[id] <> 0 and Table_B.[id] = Table_A.[id] 

			IF @@Error = 0
				BEGIN	
				DECLARE @prerowCount 	BIGINT
				DECLARE @rowCount 	BIGINT
				SET @prerowCount = (SELECT case when count is NULL then 0 else count end FROM DeleteRequests where Type = @ptype)
				SET @rowCount = (SELECT COUNT(*) FROM @dt)

					UPDATE DeleteRequests set Count = @prerowCount + @rowCount where Type = @ptype					
				END
		Commit Tran			


GO
/****** Object:  StoredProcedure [dbo].[proc_Update_Premises_Visibility]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Salman Ahmed>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Update_Premises_Visibility]
	-- Add the parameters for the stored procedure here

	@pid					int,
	@pEnable				int,
	@pFlag					bit = 0 OUTPUT,
	@pDesc					varchar(50) = NULL OUTPUT



AS
	BEGIN TRAN 
	Update Premises 
	SET Enable=@pEnable
	WHERE id=@pid
			
		IF @@Error = 0
			BEGIN	
				SET @pFlag = 1
				SET @pDesc =  'Premises Visibility Updated Successfully.'
				COMMIT TRAN
			END
			ELSE
			BEGIN
				SET @pFlag = 0
				SET @pDesc =  @@error
				ROLLBACK  TRAN
			END

GO
/****** Object:  StoredProcedure [dbo].[proc_Update_Purchasings_Delete_Request]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Update_Purchasings_Delete_Request]

		@dt AS [DeletePurchasingsRequested_Datatable]		READONLY,
		@ptype				nvarchar(50)

AS
	BEGIN TRAN
	
			Select [id],[Enable],[Delete_Request_By],[Delete_Status] FROM @dt 
			UPDATE Purchasing
			SET	Purchasing.Enable	= Table_B.Enable,
				Purchasing.Delete_Request_By	= Table_B.Delete_Request_By,
				Purchasing.Delete_Status = Table_B.Delete_Status
			FROM
				Purchasing AS Table_A
				INNER JOIN @dt AS Table_B
				ON Table_A.[id] = Table_B.[id]
			WHERE Table_B.[id] <> 0 and Table_B.[id] = Table_A.[id] 

			IF @@Error = 0
				BEGIN	
				DECLARE @prerowCount 	BIGINT
				DECLARE @rowCount 	BIGINT
				SET @prerowCount = (SELECT case when count is NULL then 0 else count end FROM DeleteRequests where Type = @ptype)
				SET @rowCount = (SELECT COUNT(*) FROM @dt)

					UPDATE DeleteRequests set Count = @prerowCount + @rowCount where Type = @ptype					
				END
		Commit Tran		


GO
/****** Object:  StoredProcedure [dbo].[proc_Update_Sales_Delete_Request]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Update_Sales_Delete_Request]

		@dt AS [DeleteSalesRequested_Datatable]		READONLY,
		@ptype				nvarchar(50)

AS
	BEGIN TRAN
	
			Select [id],[Enable],[Delete_Request_By],[Delete_Status] FROM @dt 
			UPDATE SalesOrder
			SET	SalesOrder.Enable	= Table_B.Enable,
				SalesOrder.Delete_Request_By	= Table_B.Delete_Request_By,
				SalesOrder.Delete_Status = Table_B.Delete_Status
			FROM
				SalesOrder AS Table_A
				INNER JOIN @dt AS Table_B
				ON Table_A.[id] = Table_B.[id]
			WHERE Table_B.[id] <> 0 
			AND Table_B.[id] = Table_A.[id] 

			IF @@Error = 0
				BEGIN	
				DECLARE @prerowCount 	BIGINT
				DECLARE @rowCount 	BIGINT
				SET @prerowCount = (SELECT case when count is NULL then 0 else count end FROM DeleteRequests where Type = @ptype)
				SET @rowCount = (SELECT COUNT(*) FROM @dt)

					UPDATE DeleteRequests set Count = @prerowCount + @rowCount where Type = @ptype					
				END
		Commit Tran		


GO
/****** Object:  StoredProcedure [dbo].[proc_Update_Shipment_Deliver]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Salman Ahmed>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Update_Shipment_Deliver]
	-- Add the parameters for the stored procedure here

	@pPackage_Status					nvarchar(50),
	@pPackage_id						int,
	@pFlag								bit = 0 OUTPUT,
	@pDesc								varchar(50) = NULL OUTPUT



AS
	BEGIN TRAN 
	Update SO_Packages 
	SET Package_Status=@pPackage_Status
	WHERE Package_id=@pPackage_id
			
		IF @@Error = 0
			BEGIN	
				SET @pFlag = 1
				SET @pDesc =  'Marked as Delivered Successfully'
				COMMIT TRAN
			END
			ELSE
			BEGIN
				SET @pFlag = 0
				SET @pDesc =  @@error
				ROLLBACK  TRAN
			END

GO
/****** Object:  StoredProcedure [dbo].[proc_Update_Stock]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Update_Stock]

		@dt AS [Stock_Datatable]		READONLY
AS
BEGIN
		SET NOCOUNT ON;
		BEGIN TRAN
		INSERT INTO Stock (Item_id,Physical_Quantity,Physical_Avail_ForSale,Physical_Committed,Accounting_Quantity,Acc_Avail_ForSale,Acc_Commited,OpeningStock,ReorderLevel,Time_Of_Day,Date_Of_Day,Month_Of_Day,Year_Of_Day)
		Select [Item_id],[Physical_Quantity],[Physical_Avail_ForSale],[Physical_Committed],[Accounting_Quantity],[Acc_Avail_ForSale],[Acc_Commited],[OpeningStock],[ReorderLevel],[Time_Of_Day],[Date_Of_Day],[Month_Of_Day],[Year_Of_Day] FROM @dt WHERE [Stock_id] = 0
			UPDATE Stock
			SET	Stock.Item_id	= Table_B.Item_id,
				Stock.Physical_Quantity = Table_B.Physical_Quantity,
				Stock.Physical_Avail_ForSale = Table_B.Physical_Avail_ForSale,
				Stock.Physical_Committed = Table_B.Physical_Committed,
				Stock.Accounting_Quantity = Table_B.Accounting_Quantity,
				Stock.Acc_Avail_ForSale = Table_B.Acc_Avail_ForSale,
				Stock.Acc_Commited = Table_B.Acc_Commited,
				Stock.OpeningStock = Table_B.OpeningStock,
				Stock.ReorderLevel = Table_B.ReorderLevel,
				Stock.Time_Of_Day = Table_B.Time_Of_Day,
				Stock.Date_Of_Day = Table_B.Date_Of_Day,
				Stock.Month_Of_Day = Table_B.Month_Of_Day,
				Stock.Year_Of_Day = Table_B.Year_Of_Day
				
			FROM
				Stock AS Table_A
				INNER JOIN @dt AS Table_B
				ON Table_A.[Item_id] = Table_B.[Item_id]
			WHERE Table_B.[Item_id] <> 0

		Commit Tran
END 

GO
/****** Object:  StoredProcedure [dbo].[proc_Update_Tokens]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Salman Ahmed>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Update_Tokens]
	-- Add the parameters for the stored procedure here

	@pUser_id						int = 0,
	@pToken							nvarchar(MAX),
	@pIssueDate						nvarchar(50),
	@pExpiryDate					nvarchar(50),
	@pToken_Out						varchar(50) = NULL OUTPUT


AS
	BEGIN TRAN
				IF NOT Exists (SELECT User_id FROM Tokens T WHERE User_id = @pUser_id)
				BEGIN

					INSERT INTO Tokens (User_id, Token, IssueDate, ExpiryDate)
	
					VALUES(@pUser_id,@pToken,@pIssueDate,@pIssueDate)	

						IF @@Error = 0
						BEGIN	
							SET @pToken_Out = @pToken
							COMMIT TRAN
						END

				END
				ELSE
				BEGIN
					UPDATE Tokens
					SET Token = @pToken,
					IssueDate = @pIssueDate,
					ExpiryDate = @pIssueDate
					WHERE User_id = @pUser_id


					IF @@Error = 0
					BEGIN	
						SET @pToken_Out = @pToken
						COMMIT TRAN
					END
				
			END




GO
/****** Object:  StoredProcedure [dbo].[proc_Update_Unit_Delete_Request]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Update_Unit_Delete_Request]

		@dt AS [DeleteUnitRequested_Datatable]		READONLY,
		@ptype				nvarchar(50)

AS
	BEGIN TRAN
	
			Select [Unit_id],[Enable],[Delete_Request_By],[Delete_Status] FROM @dt 
			UPDATE Units
			SET	Units.Enable	= Table_B.Enable,
				Units.Delete_Request_By	= Table_B.Delete_Request_By,
				Units.Delete_Status = Table_B.Delete_Status
			FROM
				Units AS Table_A
				INNER JOIN @dt AS Table_B
				ON Table_A.[Unit_id] = Table_B.[Unit_id]
			WHERE Table_B.[Unit_id] <> 0 and Table_B.[Unit_id] = Table_A.[Unit_id] 

			IF @@Error = 0
				BEGIN	
				DECLARE @prerowCount 	BIGINT
				DECLARE @rowCount 	BIGINT
				SET @prerowCount = (SELECT case when count is NULL then 0 else count end FROM DeleteRequests where Type = @ptype)
				SET @rowCount = (SELECT COUNT(*) FROM @dt)

					UPDATE DeleteRequests set Count = @prerowCount + @rowCount where Type = @ptype					
				END
		Commit Tran		


GO
/****** Object:  StoredProcedure [dbo].[proc_Update_Users_Delete_Request]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Update_Users_Delete_Request]

		@dt AS [DeleteUsersRequested_Datatable]		READONLY,
		@ptype				nvarchar(50)

AS
	BEGIN TRAN
	
			Select [id],[Enable],[Delete_Request_By],[Delete_Status] FROM @dt 
			UPDATE Users
			SET	Users.Enable	= Table_B.Enable,
				Users.Delete_Request_By	= Table_B.Delete_Request_By,
				Users.Delete_Status = Table_B.Delete_Status
			FROM
				Users AS Table_A
				INNER JOIN @dt AS Table_B
				ON Table_A.[id] = Table_B.[id]
			WHERE Table_B.[id] <> 0 and Table_B.[id] = Table_A.[id] 

			IF @@Error = 0
				BEGIN	
				DECLARE @prerowCount 	BIGINT
				DECLARE @rowCount 	BIGINT
				SET @prerowCount = (SELECT case when count is NULL then 0 else count end FROM DeleteRequests where Type = @ptype)
				print @prerowCount
				SET @rowCount = (SELECT COUNT(*) FROM @dt)


					UPDATE DeleteRequests set Count = @prerowCount + @rowCount where Type = @ptype					
				END
		Commit Tran	


GO
/****** Object:  StoredProcedure [dbo].[proc_UpdateBrandVisibility]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Salman Ahmed>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_UpdateBrandVisibility]
	-- Add the parameters for the stored procedure here

		@pid					int,
		@pEnable				int


AS
	BEGIN
		UPDATE Brands 
		SET Enable=@pEnable
		WHERE Brand_id = @pid

	END 

GO
/****** Object:  StoredProcedure [dbo].[proc_UpdateCategoryVisibility]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Salman Ahmed>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_UpdateCategoryVisibility]
	-- Add the parameters for the stored procedure here

		@pid					int,
		@pEnable				int


AS
	BEGIN
		UPDATE Categories 
		SET Enable=@pEnable
		WHERE Category_id = @pid

	END 

GO
/****** Object:  StoredProcedure [dbo].[proc_UpdateCityVisibility]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Salman Ahmed>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_UpdateCityVisibility]
	-- Add the parameters for the stored procedure here

		@pid					int,
		@pEnable				int


AS
	BEGIN
		UPDATE Cities 
		SET Enable=@pEnable
		WHERE id = @pid

	END 

GO
/****** Object:  StoredProcedure [dbo].[proc_UpdateCompanyVisibility]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Salman Ahmed>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_UpdateCompanyVisibility]
	-- Add the parameters for the stored procedure here

		@pid					int,
		@pEnable				int


AS
	BEGIN
		UPDATE Companies 
		SET Enable=@pEnable
		WHERE id = @pid

	END 

GO
/****** Object:  StoredProcedure [dbo].[proc_UpdateContactVisibility]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Salman Ahmed>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_UpdateContactVisibility]
	-- Add the parameters for the stored procedure here

		@pid					int,
		@pEnable				int


AS
	BEGIN
		UPDATE Contacts 
		SET Enable=@pEnable
		WHERE id = @pid

	END 

GO
/****** Object:  StoredProcedure [dbo].[proc_UpdateCountryVisibility]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Salman Ahmed>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_UpdateCountryVisibility]
	-- Add the parameters for the stored procedure here

		@pid					int,
		@pEnable				int


AS
	BEGIN
		UPDATE Countries 
		SET Enable=@pEnable
		WHERE id = @pid
		IF @@Error = 0
		BEGIN	
		UPDATE Cities 
		SET Enable=@pEnable
		WHERE Country = cast(@pid as nvarchar)

		END
	END 

GO
/****** Object:  StoredProcedure [dbo].[proc_UpdateItemVisibility]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Salman Ahmed>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_UpdateItemVisibility]
	-- Add the parameters for the stored procedure here

		@pid					int,
		@pEnable				int


AS
	BEGIN
		UPDATE Items 
		SET Enable=@pEnable
		WHERE Item_id = @pid

	END 

GO
/****** Object:  StoredProcedure [dbo].[proc_UpdateManufacturerVisibility]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Salman Ahmed>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_UpdateManufacturerVisibility]
	-- Add the parameters for the stored procedure here

		@pid					int,
		@pEnable				int


AS
	BEGIN
		UPDATE Manufacturers 
		SET Enable=@pEnable
		WHERE Manufacturer_id = @pid

	END 

GO
/****** Object:  StoredProcedure [dbo].[proc_UpdateMessageStatus]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Salman Ahmed>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_UpdateMessageStatus]
	-- Add the parameters for the stored procedure here

		@pReceiver_id					int,
		@pStatus						nvarchar(MAX)


AS
	BEGIN
		UPDATE Messages 
		SET Status=@pStatus
		WHERE Sender_id = @pReceiver_id

	END 

GO
/****** Object:  StoredProcedure [dbo].[proc_UpdatePremisesVisibility]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Salman Ahmed>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_UpdatePremisesVisibility]
	-- Add the parameters for the stored procedure here

		@pid					int,
		@pEnable				int


AS
	BEGIN
		UPDATE Premises 
		SET Enable=@pEnable
		WHERE id = @pid

	END 

GO
/****** Object:  StoredProcedure [dbo].[proc_UpdatePurchaseVisibility]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Salman Ahmed>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_UpdatePurchaseVisibility]
	-- Add the parameters for the stored procedure here

		@pid					int,
		@pEnable				int


AS
	BEGIN
		UPDATE Purchasing 
		SET Enable=@pEnable
		WHERE id = @pid

	END 

GO
/****** Object:  StoredProcedure [dbo].[proc_UpdateSaleOrderVisibility]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Salman Ahmed>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_UpdateSaleOrderVisibility]
	-- Add the parameters for the stored procedure here

		@pid					int,
		@pEnable				int


AS
	BEGIN
		UPDATE SalesOrder 
		SET Enable=@pEnable
		WHERE id = @pid

	END 

GO
/****** Object:  StoredProcedure [dbo].[proc_UpdateUnitVisibility]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Salman Ahmed>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_UpdateUnitVisibility]
	-- Add the parameters for the stored procedure here

		@pid					int,
		@pEnable				int


AS
	BEGIN
		UPDATE Units 
		SET Enable=@pEnable
		WHERE Unit_id = @pid

	END 

GO
/****** Object:  StoredProcedure [dbo].[proc_UpdateUserVisibility]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Salman Ahmed>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_UpdateUserVisibility]
	-- Add the parameters for the stored procedure here

		@pid					int,
		@pEnable				int


AS
	BEGIN
		UPDATE Users 
		SET Enable=@pEnable
		WHERE id = @pid

	END 

GO
/****** Object:  StoredProcedure [dbo].[proc_User_Login]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Salman Ahmed>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_User_Login] --'admin','123'
	-- Add the parameters for the stored procedure here
	@UserEmail			VARCHAR(50),
	@UserPassword 		VARCHAR(50),
	@ipAddress			VARCHAR(50) = NULL,
	@pPKID				BIGINT = 0 OUTPUT,
	@pFlag				bit = NULL OUTPUT,
	@pDesc				VARCHAR(500) = NULL OUTPUT

AS
	
	DECLARE @rowCount 	BIGINT
	DECLARE @Role 		BIGINT
	DECLARE @userID 	BIGINT
	Declare @sessionID	bigint

	SET @rowCount = (SELECT COUNT(*) FROM Users U WHERE UPPER(U.[email]) = @UserEmail AND U.password = @UserPassword)
	
	IF @rowCount = 1
	BEGIN	

		SET @Role = (SELECT U.Role_id FROM Users U WHERE LOWER(U.[email]) = @UserEmail AND U.password = @UserPassword)

		SET @rowCount = (SELECT COUNT(*) FROM Roles G WHERE G.Role_id = @Role AND G.Enable = '1')
		
		IF @rowCount = 1
		BEGIN
			SET @userID = (SELECT U.[id] FROM Users U WHERE LOWER(U.[email]) = @UserEmail AND U.password = @UserPassword)

		--For Inserting New session_id for user
			--INSERT INTO Session([User_id], [Login_Date], [Login_Time],[ipAddress])

			--VALUES(@userID, SUBSTRING(CONVERT(CHAR(30),CURRENT_TIMESTAMP),1,11), SUBSTRING(CONVERT(CHAR(30),CURRENT_TIMESTAMP),12,12), @ipAddress)

			--SET @sessionID = IDENT_CURRENT('tbl_session')

--For Collecting User Information
			SELECT U.id,U.email,U.attached_profile,
			(SELECT Name FROM Contacts WHERE id=U.attached_profile) as Name, 
			Role_id,
			(SELECT Role_Name FROM Roles WHERE Role_id = U.Role_id) as Role,
			u.Premises_id,
			S.Login_Date,
			S.ipAddress,
			S.Login_Time, 
			@sessionID [Session ID]
			
			FROM Users U
				LEFT OUTER JOIN Session S
					on u.id = S.User_id 
			WHERE UPPER(U.[email]) =@UserEmail 	
			AND  U.password = @UserPassword			
		END
	END
	ELSE
	BEGIN
		SET @pFlag = 0
		SET @pDesc = 'Invalid Login/Password'
	END


GO
/****** Object:  StoredProcedure [dbo].[proc_User_Privileges]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_User_Privileges] --'admin@gmail.com', '123'
	
	@UserEmail			VARCHAR(50),
	@UserPassword 		VARCHAR(50),
	@ipAddress			VARCHAR(50) = NULL,
	@pPKID				BIGINT = 0 OUTPUT,
	@pFlag				bit = NULL OUTPUT,
	@pDesc				VARCHAR(500) = NULL OUTPUT

AS
	
	DECLARE @rowCount 	BIGINT
	DECLARE @Role 		BIGINT
	DECLARE @userID 	BIGINT
	Declare @sessionID	bigint

	SET @rowCount = (SELECT COUNT(*) FROM Users U WHERE UPPER(U.[email]) = @UserEmail AND U.password = @UserPassword)
	
	IF @rowCount = 1
	BEGIN	

		SET @Role = (SELECT U.Role_id FROM Users U WHERE UPPER(U.[email]) = @UserEmail AND U.password = @UserPassword)

		SET @rowCount = (SELECT COUNT(*) FROM Roles G WHERE G.Role_id = @Role AND G.Enable = '1')

		IF @rowCount = 1
		BEGIN
			--For Collect Active Group Priviliges
			SELECT P.Priv_id  FROM Privileges P WHERE P.priv_id IN(
			SELECT RP.Priv_id FROM RolePrivileges RP WHERE RP.Enable = '1' AND RP.Role_id IN (
			SELECT R.Role_id FROM Roles R WHERE R.Enable = '1' AND R.Role_id = @Role))
			union
			SELECT P.Priv_Type_id as priv_ID FROM Privileges P WHERE P.Enable = '1' AND P.Priv_id IN(
			SELECT RP.Priv_id FROM RolePrivileges RP WHERE RP.Enable = '1' AND RP.Role_id IN (
			SELECT R.Role_id FROM Roles R WHERE R.Enable = '1' AND R.Role_id = @Role))

			SET @pFlag = 1
			SET @pDesc = 'Successful'
		END
			
		ELSE
		BEGIN
			SET @pFlag = 0
			SET @pDesc = 'Role Not Active'
		END				
		
			
	END
	ELSE
	BEGIN

		SET @pFlag = 0
		SET @pDesc = 'Invalid Login/Password'
	END
GO
/****** Object:  StoredProcedure [dbo].[proc_Userss_Privileges]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Userss_Privileges] --2

	@pUserID 			BIGINT
	--@pRoleID 			BIGINT

AS
	BEGIN
	SET NOCOUNT ON;
	 

		SELECT id,P.Priv_id, P.Priv_Name, UP.[Add], UP.Edit, UP.[View], Profile, UP.ControllerName
			FROM Privileges P
					inner join User_Privileges UP
					on P.Priv_id = UP.Priv_id
				WHERE P.Enable IN (1) 
				and UP.User_id = @pUserID 
				--AND UP.Enable = 1
		UNION
		SELECT id,P.Priv_id, P.Priv_Name, UP.[Add], UP.Edit, UP.[View], Profile, UP.ControllerName
			FROM Privileges P
					inner join User_Privileges UP
					on P.Priv_id = UP.Priv_id
					and UP.User_id = @pUserID 
				--AND P.Enable = 1

		UNION
		SELECT 0 AS id,P.Priv_id, P.Priv_Name, 0 AS [Add], 0 AS Edit, 0 AS [View], 0 AS Profile, '' as ControllerName
			FROM Privileges P
				WHERE P.Enable IN (1) AND P.Priv_id NOT IN (
					SELECT UP.Priv_id 
						FROM User_Privileges UP
							WHERE UP.User_id = @pUserID )
			order by P.Priv_id asc



		--	SELECT id,P.Priv_id, P.Priv_Name, UP.[Add], UP.[Update], UP.[Delete],UP.[View], Profile, UP.ControllerName
		--	FROM Privileges P
		--			inner join User_Privileges UP
		--			on P.Priv_id = UP.Priv_id
		--		WHERE P.Enable IN (1) 
		--		and UP.User_id = @pUserID 
		--		--AND UP.Enable = 1
		--UNION
		--SELECT id,P.Priv_id, P.Priv_Name, UP.[Add], UP.[Update], UP.[Delete], UP.[View], Profile, UP.ControllerName
		--	FROM Privileges P
		--			inner join User_Privileges UP
		--			on P.Priv_id = UP.Priv_id
		--			and UP.User_id = @pUserID 
		--		--AND P.Enable = 1

		--UNION
		--SELECT 0 AS id,P.Priv_id, P.Priv_Name, 0 AS [Add], 0 AS [Update], 0 AS [Delete], 0 AS [View], 0 AS Profile,  '' as ControllerName
		--	FROM Privileges P
		--		WHERE P.Enable IN (1) AND P.Priv_id NOT IN (
		--			SELECT UP.Priv_id 
		--				FROM User_Privileges UP
		--					WHERE UP.User_id = @pUserID )
		--	order by P.Priv_id asc
		
	END

GO
/****** Object:  StoredProcedure [dbo].[proc_ViewPayments]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_ViewPayments] --2
	
	@pBill_id					int = 0,
	@pBillNo					nvarchar(50) = NULL,
	@pFrom						nvarchar(50) = NULL,
	@pTo						nvarchar(50) = NULL

AS
BEGIN
	SELECT 
	Payment_id,
	Bill_id,
	(SELECT Bill_No FROM Bills where bill_id=p.bill_id) as Bill_No,
	(SELECT Bill_Amount FROM Bills where bill_id=p.bill_id) as Bill_Amount,
	Payment_Mode,
	Payment_Date,
	Total_Amount,
	Paid_Amount,
	Balance_Amount,
	Time_Of_Day,
	Date_Of_Day,
	Month_Of_Day,
	Year_Of_Day

	FROM Payments AS p
	--WHERE p.Bill_id = CASE WHEN @pBill_id IS NULL THEN p.Bill_id ELSE @pBill_id END

	WHERE (p.Bill_id = @pBill_id)
	AND ((@pBillNo is null) or (SELECT Bill_No FROM Bills where bill_id=p.bill_id) = @pBillNo)
	AND ((@pFrom is null) or (convert(datetime, p.Date_Of_Day, 103) >= convert(datetime, @pFrom, 103)))
	AND ((@pTo is null) or (convert(datetime, p.Date_Of_Day, 103) <= convert(datetime, @pTo, 103)))



	ORDER BY p.Bill_id desc
END

GO
/****** Object:  Table [dbo].[ActivityType]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActivityType](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ActivityType] [nvarchar](50) NULL,
 CONSTRAINT [PK_ActivityType] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Bills]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bills](
	[Bill_id] [int] IDENTITY(1,1) NOT NULL,
	[Purchase_id] [int] NULL,
	[Vendor_id] [int] NULL,
	[Bill_No] [nvarchar](50) NULL,
	[Bill_Status] [nvarchar](50) NULL,
	[Bill_Amount] [decimal](10, 2) NULL,
	[Amount_Paid] [decimal](10, 2) NULL,
	[Balance_Amount] [decimal](10, 2) NULL,
	[Enable] [nvarchar](max) NULL,
	[BillDateTime] [nvarchar](50) NULL,
	[BillDueDate] [nvarchar](50) NULL,
	[AddedBy] [int] NULL,
	[UpdatedBy] [int] NULL,
	[Time_Of_Day] [nvarchar](max) NULL,
	[Date_Of_Day] [nvarchar](max) NULL,
	[Month_Of_Day] [nvarchar](max) NULL,
	[Year_Of_Day] [nvarchar](max) NULL,
 CONSTRAINT [PK_Bills] PRIMARY KEY CLUSTERED 
(
	[Bill_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Brands]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Brands](
	[Brand_id] [int] IDENTITY(1,1) NOT NULL,
	[Brand_Name] [nvarchar](max) NULL,
	[Brand_Code] [nvarchar](max) NULL,
	[Enable] [int] NULL,
	[AddedBy] [int] NULL,
	[UpdatedBy] [int] NULL,
	[Delete_Request_By] [int] NULL,
	[Delete_Status] [nvarchar](50) NULL,
	[Time_Of_Day] [nvarchar](max) NULL,
	[Date_Of_Day] [nvarchar](max) NULL,
	[Month_Of_Day] [nvarchar](max) NULL,
	[Year_Of_Day] [nvarchar](max) NULL,
 CONSTRAINT [PK_Brands] PRIMARY KEY CLUSTERED 
(
	[Brand_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CalenderEvents]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CalenderEvents](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Event_id] [int] NULL,
	[Event_Start_Date] [nvarchar](50) NULL,
	[StartedBy] [int] NULL,
	[Time_Of_Day] [nvarchar](max) NULL,
	[Date_Of_Day] [nvarchar](max) NOT NULL,
	[Month_Of_Day] [nvarchar](max) NULL,
	[Year_Of_Day] [nvarchar](max) NULL,
 CONSTRAINT [PK_CalenderEvents] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Categories]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Category_id] [int] IDENTITY(1,1) NOT NULL,
	[Category_Name] [nvarchar](max) NULL,
	[Category_Code] [nvarchar](max) NULL,
	[Enable] [int] NULL,
	[AddedBy] [int] NULL,
	[UpdatedBy] [int] NULL,
	[Delete_Request_By] [int] NULL,
	[Delete_Status] [nvarchar](50) NULL,
	[Time_Of_Day] [nvarchar](max) NULL,
	[Date_Of_Day] [nvarchar](max) NULL,
	[Month_Of_Day] [nvarchar](max) NULL,
	[Year_Of_Day] [nvarchar](max) NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[Category_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Cities]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cities](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Country] [nvarchar](max) NULL,
	[Enable] [int] NULL,
	[AddedBy] [int] NULL,
	[UpdatedBy] [int] NULL,
	[Delete_Request_By] [int] NULL,
	[Delete_Status] [nvarchar](50) NULL,
	[TimeOfDay] [nvarchar](max) NULL,
	[DateOfDay] [nvarchar](max) NULL,
	[MonthOfDay] [nvarchar](max) NULL,
	[YearOfDay] [nvarchar](max) NULL,
 CONSTRAINT [PK_Cities] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Companies]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Companies](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Landline] [nvarchar](max) NULL,
	[Mobile] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[Website] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
	[City] [int] NULL,
	[Country] [int] NULL,
	[BankAccountNumber] [nvarchar](max) NULL,
	[PaymentMethod] [nvarchar](max) NULL,
	[Enable] [int] NULL,
	[AddedBy] [int] NULL,
	[UpdatedBy] [int] NULL,
	[Delete_Request_By] [int] NULL,
	[Delete_Status] [nvarchar](50) NULL,
	[TimeOfDay] [nvarchar](max) NULL,
	[DateOfDay] [nvarchar](max) NULL,
	[MonthOfDay] [nvarchar](max) NULL,
	[YearOfDay] [nvarchar](max) NULL,
 CONSTRAINT [PK_Companies] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Contacts]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contacts](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Salutation] [int] NULL,
	[Image] [nvarchar](max) NULL,
	[Name] [nvarchar](max) NULL,
	[Company] [int] NULL,
	[Designation] [nvarchar](max) NULL,
	[Landline] [nvarchar](max) NULL,
	[Mobile] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[Website] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
	[AddressLandline] [nvarchar](max) NULL,
	[City] [nvarchar](max) NULL,
	[Country] [nvarchar](max) NULL,
	[BankAccountNumber] [nvarchar](max) NULL,
	[PaymentMethod] [nvarchar](max) NULL,
	[Vendor] [nvarchar](max) NULL,
	[Customer] [nvarchar](max) NULL,
	[Employee] [nvarchar](max) NULL,
	[Enable] [int] NULL,
	[AddedBy] [int] NULL,
	[UpdatedBy] [int] NULL,
	[Delete_Request_By] [int] NULL,
	[Delete_Status] [nvarchar](50) NULL,
	[TimeOfDay] [nvarchar](max) NULL,
	[DateOfDay] [nvarchar](max) NULL,
	[MonthOfDay] [nvarchar](max) NULL,
	[YearOfDay] [nvarchar](max) NULL,
 CONSTRAINT [PK_Contacts] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Countries]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Countries](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Enable] [int] NULL,
	[AddedBy] [int] NULL,
	[UpdatedBy] [int] NULL,
	[Delete_Request_By] [int] NULL,
	[Delete_Status] [nvarchar](50) NULL,
	[TimeOfDay] [nvarchar](max) NULL,
	[DateOfDay] [nvarchar](max) NULL,
	[MonthOfDay] [nvarchar](max) NULL,
	[YearOfDay] [nvarchar](max) NULL,
 CONSTRAINT [PK_Countries] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DeleteRequests]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeleteRequests](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](50) NULL,
	[Count] [int] NULL,
 CONSTRAINT [PK_DeleteRequests] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Events]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Events](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[EventName] [nvarchar](max) NULL,
	[BackgroundColour] [nvarchar](max) NULL,
	[BorderColour] [nvarchar](max) NULL,
	[AddedBy] [int] NULL,
	[UpdatedBy] [int] NULL,
	[Delete_Request_By] [int] NULL,
	[Delete_Status] [nvarchar](50) NULL,
	[Time_Of_Day] [nvarchar](max) NULL,
	[Date_Of_Day] [nvarchar](max) NOT NULL,
	[Month_Of_Day] [nvarchar](max) NULL,
	[Year_Of_Day] [nvarchar](max) NULL,
 CONSTRAINT [PK_Events] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ItemActivity]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemActivity](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Item_id] [int] NULL,
	[ActivityType_id] [int] NULL,
	[ActivityType] [nvarchar](50) NULL,
	[ActivityName] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Date] [nvarchar](50) NULL,
	[Time] [nvarchar](50) NULL,
	[User_id] [int] NULL,
	[Icon] [nvarchar](max) NULL,
 CONSTRAINT [PK_ItemActivity] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Items]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Items](
	[Item_id] [int] IDENTITY(1,1) NOT NULL,
	[Item_type] [nvarchar](255) NULL,
	[Item_file] [nvarchar](255) NULL,
	[Item_Name] [nvarchar](255) NULL,
	[Item_Sku] [nvarchar](255) NULL,
	[Item_Category] [int] NULL,
	[Item_Unit] [int] NULL,
	[Item_Manufacturer] [int] NULL,
	[Item_Upc] [nvarchar](255) NULL,
	[Item_Brand] [int] NULL,
	[Item_Mpn] [nvarchar](255) NULL,
	[Item_Ean] [nvarchar](255) NULL,
	[Item_Isbn] [nvarchar](255) NULL,
	[Item_Sell_Price] [nvarchar](255) NULL,
	[Item_Tax] [nvarchar](255) NULL,
	[Item_Purchase_Price] [nvarchar](255) NULL,
	[Item_Preferred_Vendor] [int] NULL,
	[Enable] [int] NULL,
	[AddedBy] [int] NULL,
	[UpdatedBy] [int] NULL,
	[Delete_Request_By] [int] NULL,
	[Delete_Status] [nvarchar](50) NULL,
	[Time_Of_Day] [nvarchar](255) NULL,
	[Date_Of_Day] [nvarchar](255) NULL,
	[Month_Of_Day] [nvarchar](255) NULL,
	[Year_Of_Day] [nvarchar](255) NULL,
 CONSTRAINT [PK_Items] PRIMARY KEY CLUSTERED 
(
	[Item_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Mailbox]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mailbox](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[EmailTo] [nvarchar](50) NULL,
	[EmailFrom] [nvarchar](50) NULL,
	[Subject] [nvarchar](max) NULL,
	[Body] [nvarchar](max) NULL,
	[Status] [nvarchar](50) NULL,
	[User_id] [int] NULL,
	[TimeOfDay] [nvarchar](max) NULL,
	[DateOfDay] [nvarchar](max) NULL,
	[MonthOfDay] [nvarchar](max) NULL,
	[YearOfDay] [nvarchar](max) NULL,
 CONSTRAINT [PK_Mailbox] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MailBoxAttachments]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MailBoxAttachments](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Mailbox_id] [int] NULL,
	[FileName] [nvarchar](max) NULL,
 CONSTRAINT [PK_MailBoxAttachments] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Manufacturers]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Manufacturers](
	[Manufacturer_id] [int] IDENTITY(1,1) NOT NULL,
	[Manufacturer_Name] [nvarchar](max) NULL,
	[Manufacturer_Code] [nvarchar](max) NULL,
	[Enable] [int] NULL,
	[AddedBy] [int] NULL,
	[UpdatedBy] [int] NULL,
	[Delete_Request_By] [int] NULL,
	[Delete_Status] [nvarchar](50) NULL,
	[Time_Of_Day] [nvarchar](max) NULL,
	[Date_Of_Day] [nvarchar](max) NULL,
	[Month_Of_Day] [nvarchar](max) NULL,
	[Year_Of_Day] [nvarchar](max) NULL,
 CONSTRAINT [PK_Manufacturers] PRIMARY KEY CLUSTERED 
(
	[Manufacturer_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Messages]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Messages](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Sender_id] [int] NULL,
	[Receiver_id] [int] NULL,
	[Message] [nvarchar](max) NULL,
	[Status] [nvarchar](50) NULL,
	[Enable] [int] NULL,
	[Date] [nvarchar](50) NULL,
	[Time] [nvarchar](50) NULL,
	[Month] [nvarchar](50) NULL,
	[Year] [nvarchar](50) NULL,
 CONSTRAINT [PK_Messaging] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Messages_Detail]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Messages_Detail](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Message_id] [int] NULL,
	[SenderMessage] [nvarchar](max) NULL,
	[ReceiverMessage] [nvarchar](max) NULL,
	[Date] [nvarchar](50) NULL,
	[Time] [nvarchar](50) NULL,
	[Month] [nvarchar](50) NULL,
	[Year] [nvarchar](50) NULL,
 CONSTRAINT [PK_Messages_Detail] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PaymentMode]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentMode](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Payment_Mode] [nvarchar](50) NULL,
	[AddedBy] [int] NULL,
	[UpdatedBy] [int] NULL,
	[Enable] [int] NULL,
	[Time_Of_Day] [nvarchar](max) NULL,
	[Date_Of_Day] [nvarchar](max) NULL,
	[Month_Of_Day] [nvarchar](max) NULL,
	[Year_Of_Day] [nvarchar](max) NULL,
 CONSTRAINT [PK_PaymentMode] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Payments]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payments](
	[Payment_id] [int] IDENTITY(1,1) NOT NULL,
	[Bill_id] [int] NULL,
	[Payment_Mode] [int] NULL,
	[BankCharges] [nvarchar](50) NULL,
	[Payment_Date] [nvarchar](50) NULL,
	[Total_Amount] [decimal](10, 2) NULL,
	[Paid_Amount] [decimal](10, 2) NULL,
	[Balance_Amount] [decimal](10, 2) NULL,
	[AddedBy] [int] NULL,
	[UpdatedBy] [int] NULL,
	[Enable] [int] NULL,
	[Time_Of_Day] [nvarchar](max) NULL,
	[Date_Of_Day] [nvarchar](max) NULL,
	[Month_Of_Day] [nvarchar](max) NULL,
	[Year_Of_Day] [nvarchar](max) NULL,
 CONSTRAINT [PK_Payments] PRIMARY KEY CLUSTERED 
(
	[Payment_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Premises]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Premises](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[Pc_Mac_Address] [nvarchar](50) NULL,
	[Phone] [nvarchar](max) NULL,
	[City] [int] NULL,
	[Country] [int] NULL,
	[Address] [nvarchar](255) NULL,
	[Office] [nvarchar](max) NULL,
	[Factory] [nvarchar](max) NULL,
	[Store] [nvarchar](max) NULL,
	[Shop] [nvarchar](max) NULL,
	[Enable] [int] NULL,
	[UserId] [int] NULL,
	[AddedBy] [int] NULL,
	[UpdatedBy] [int] NULL,
	[Delete_Request_By] [int] NULL,
	[Delete_Status] [nvarchar](50) NULL,
	[Time_Of_Day] [nvarchar](max) NULL,
	[Date_Of_Day] [nvarchar](max) NULL,
	[Month_Of_Day] [nvarchar](max) NULL,
	[Year_Of_Day] [nvarchar](max) NULL,
 CONSTRAINT [PK_Premises] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Privileges]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Privileges](
	[Priv_id] [int] NOT NULL,
	[Priv_Name] [nvarchar](50) NOT NULL,
	[Priv_Type_id] [bigint] NOT NULL,
	[Enable] [int] NULL,
	[Time_Of_Day] [nvarchar](max) NULL,
	[Date_Of_Day] [nvarchar](max) NULL,
	[Month_Of_Day] [nvarchar](max) NULL,
	[Year_Of_Day] [nvarchar](max) NULL,
 CONSTRAINT [PK_Privileges] PRIMARY KEY CLUSTERED 
(
	[Priv_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PrivilegesType]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PrivilegesType](
	[Priv_Type_id] [bigint] NOT NULL,
	[Priv_Type_Name] [nvarchar](50) NOT NULL,
	[Priv_Type_Desc] [nvarchar](50) NULL,
	[Enable] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_PrivilegesType] PRIMARY KEY CLUSTERED 
(
	[Priv_Type_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Purchasing]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Purchasing](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[TempOrderNum] [nvarchar](max) NULL,
	[PremisesId] [nvarchar](max) NULL,
	[UserId] [nvarchar](max) NULL,
	[TotalItems] [nvarchar](max) NULL,
	[Approved] [nvarchar](max) NULL,
	[ApprovedByUI] [nvarchar](max) NULL,
	[RecieveStatus] [nvarchar](50) NULL,
	[Recieve_DateTime] [nvarchar](50) NULL,
	[Enable] [int] NULL,
	[Rec_Stat] [nvarchar](50) NULL,
	[BillStatus] [nvarchar](50) NULL,
	[AddedBy] [int] NULL,
	[UpdatedBy] [int] NULL,
	[Delete_Request_By] [int] NULL,
	[Delete_Status] [nvarchar](50) NULL,
	[Time_Of_Day] [nvarchar](max) NULL,
	[Date_Of_Day] [nvarchar](max) NULL,
	[Month_Of_Day] [nvarchar](max) NULL,
	[Year_Of_Day] [nvarchar](max) NULL,
 CONSTRAINT [PK_Purchasing] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PurchasingDetails]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchasingDetails](
	[pd_id] [int] IDENTITY(1,1) NOT NULL,
	[PurchasingId] [int] NULL,
	[ItemId] [int] NULL,
	[VendorId] [int] NULL,
	[Qty] [nvarchar](max) NULL,
	[PriceUnit] [nvarchar](max) NULL,
	[MsrmntUnit] [nvarchar](max) NULL,
 CONSTRAINT [PK_PurchasingDetails] PRIMARY KEY CLUSTERED 
(
	[pd_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Recievings]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Recievings](
	[Recieving_id] [int] IDENTITY(1,1) NOT NULL,
	[Item_id] [nvarchar](50) NULL,
	[Vendor_id] [int] NULL,
	[GRN] [nvarchar](50) NULL,
	[Quantity_Recieved] [nvarchar](50) NULL,
	[Enable] [nvarchar](max) NULL,
	[Time_Of_Day] [nvarchar](max) NULL,
	[Date_Of_Day] [nvarchar](max) NULL,
	[Month_Of_Day] [nvarchar](max) NULL,
	[Year_Of_Day] [nvarchar](max) NULL,
 CONSTRAINT [PK_Recievings] PRIMARY KEY CLUSTERED 
(
	[Recieving_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RolePrivileges]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RolePrivileges](
	[Role_Priv_id] [int] IDENTITY(1,1) NOT NULL,
	[Role_id] [int] NOT NULL,
	[Priv_id] [int] NOT NULL,
	[Check_Status] [nvarchar](50) NULL,
	[Enable] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_RolePrivileges] PRIMARY KEY CLUSTERED 
(
	[Role_Priv_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Roles]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Role_id] [int] IDENTITY(1,1) NOT NULL,
	[Role_Name] [nvarchar](50) NOT NULL,
	[Enable] [nvarchar](50) NOT NULL,
	[AddedBy] [int] NULL,
	[UpdatedBy] [int] NULL,
	[Time_Of_Day] [nvarchar](255) NULL,
	[Date_Of_Day] [nvarchar](255) NULL,
	[Month_Of_Day] [nvarchar](255) NULL,
	[Year_Of_Day] [nvarchar](255) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Role_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SaleReturn]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SaleReturn](
	[SaleReturn_id] [int] IDENTITY(1,1) NOT NULL,
	[SaleOrder_id] [int] NULL,
	[SaleReturnNo] [nvarchar](50) NULL,
	[SaleReturn_Date] [nvarchar](50) NULL,
	[SaleReturn_Status] [nvarchar](50) NULL,
	[TotalReturn_Cost] [nvarchar](50) NULL,
	[AddedBy] [int] NULL,
	[UpdatedBy] [int] NULL,
	[Time_Of_Day] [nvarchar](max) NULL,
	[Date_Of_Day] [nvarchar](max) NULL,
	[Month_Of_Day] [nvarchar](max) NULL,
	[Year_Of_Day] [nvarchar](max) NULL,
 CONSTRAINT [PK_SaleReturn] PRIMARY KEY CLUSTERED 
(
	[SaleReturn_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SaleReturnDetail]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SaleReturnDetail](
	[SaleReturnDetail_id] [int] IDENTITY(1,1) NOT NULL,
	[SaleReturn_id] [int] NULL,
	[Package_id] [int] NULL,
	[Item_id] [int] NULL,
	[Return_Qty] [nvarchar](50) NULL,
	[Received_Qty] [nvarchar](50) NULL,
	[ReturnQty_Cost] [nvarchar](50) NULL,
 CONSTRAINT [PK_SaleReturnDetail] PRIMARY KEY CLUSTERED 
(
	[SaleReturnDetail_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SalesOrder]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesOrder](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[SaleOrderNo] [nvarchar](max) NULL,
	[PremisesId] [nvarchar](max) NULL,
	[UserId] [nvarchar](max) NULL,
	[TotalItems] [nvarchar](max) NULL,
	[SO_Total_Amount] [nvarchar](50) NULL,
	[SO_Shipping_Charges] [nvarchar](50) NULL,
	[SO_Status] [nvarchar](max) NULL,
	[SO_Invoice_Status] [nvarchar](50) NULL,
	[SO_Shipment_Status] [bit] NULL,
	[SO_Package_Status] [bit] NULL,
	[SO_DateTime] [nvarchar](50) NULL,
	[SO_Expected_Shipment_Date] [nvarchar](50) NULL,
	[Enable] [int] NULL,
	[Delete_Request_By] [int] NULL,
	[Delete_Status] [nvarchar](50) NULL,
	[AddedBy] [int] NULL,
	[UpdatedBy] [int] NULL,
	[Time_Of_Day] [nvarchar](max) NULL,
	[Date_Of_Day] [nvarchar](max) NULL,
	[Month_Of_Day] [nvarchar](max) NULL,
	[Year_Of_Day] [nvarchar](max) NULL,
 CONSTRAINT [PK_SalesOrder] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SalesOrder_Details]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesOrder_Details](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[SalesOrder_id] [int] NULL,
	[ItemId] [int] NULL,
	[Customer_id] [int] NULL,
	[Qty] [nvarchar](max) NULL,
	[PriceUnit] [nvarchar](max) NULL,
	[MsrmntUnit] [nvarchar](max) NULL,
 CONSTRAINT [PK_SalesOrder_Details] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SalesOrder_Invoices]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesOrder_Invoices](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[SalesOrder_id] [int] NULL,
	[Invoice_No] [int] NULL,
	[Customer_id] [int] NULL,
	[Invoice_Status] [nvarchar](50) NULL,
	[Invoice_Amount] [decimal](10, 2) NULL,
	[Amount_Paid] [decimal](10, 2) NULL,
	[Balance_Amount] [decimal](10, 2) NULL,
	[InvoiceDateTime] [nvarchar](max) NULL,
	[InvoiceDueDate] [nvarchar](max) NULL,
	[AddedBy] [int] NULL,
	[UpdatedBy] [int] NULL,
	[Time_Of_Day] [nvarchar](max) NULL,
	[Date_Of_Day] [nvarchar](max) NULL,
	[Month_Of_Day] [nvarchar](max) NULL,
	[Year_Of_Day] [nvarchar](max) NULL,
 CONSTRAINT [PK_SalesOrder_Invoices] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Session]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Session](
	[Session_id] [bigint] IDENTITY(1,1) NOT NULL,
	[User_id] [bigint] NULL,
	[Login_Date] [datetime] NULL,
	[Logout_Date] [datetime] NULL,
	[Login_Time] [datetime] NULL,
	[LogoutTime] [datetime] NULL,
	[ipAddress] [nvarchar](50) NULL,
 CONSTRAINT [PK_Session] PRIMARY KEY CLUSTERED 
(
	[Session_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Shipment]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Shipment](
	[Shipment_id] [int] IDENTITY(1,1) NOT NULL,
	[SaleOrder_id] [int] NULL,
	[Shipment_No] [nvarchar](max) NULL,
	[Shipment_Cost] [nvarchar](50) NULL,
	[Shipment_Status] [nvarchar](50) NULL,
	[Shipment_Date] [nvarchar](50) NULL,
	[AddedBy] [int] NULL,
	[UpdatedBy] [int] NULL,
	[Time_Of_Day] [nvarchar](max) NULL,
	[Date_Of_Day] [nvarchar](max) NULL,
	[Month_Of_Day] [nvarchar](max) NULL,
	[Year_Of_Day] [nvarchar](max) NULL,
 CONSTRAINT [PK_Shipment] PRIMARY KEY CLUSTERED 
(
	[Shipment_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ShipmentPackages]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShipmentPackages](
	[ShipmentDetail_id] [int] IDENTITY(1,1) NOT NULL,
	[SaleOrder_id] [int] NULL,
	[Shipment_No] [nvarchar](max) NULL,
	[Package_id] [int] NULL,
	[AddedBy] [int] NULL,
	[UpdatedBy] [int] NULL,
 CONSTRAINT [PK_ShipmentPackages] PRIMARY KEY CLUSTERED 
(
	[ShipmentDetail_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SO_Packages]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SO_Packages](
	[Package_id] [int] IDENTITY(1,1) NOT NULL,
	[SaleOrder_id] [int] NULL,
	[Package_No] [nvarchar](max) NULL,
	[Package_Cost] [nvarchar](50) NULL,
	[Package_Status] [nvarchar](50) NULL,
	[Package_Date] [nvarchar](50) NULL,
	[AddedBy] [int] NULL,
	[UpdatedBy] [int] NULL,
	[Time_Of_Day] [nvarchar](max) NULL,
	[Date_Of_Day] [nvarchar](max) NULL,
	[Month_Of_Day] [nvarchar](max) NULL,
	[Year_Of_Day] [nvarchar](max) NULL,
 CONSTRAINT [PK_SO_Packages] PRIMARY KEY CLUSTERED 
(
	[Package_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SO_Packages_Detail]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SO_Packages_Detail](
	[SO_PackageDetail_id] [int] IDENTITY(1,1) NOT NULL,
	[SaleOrder_id] [int] NULL,
	[Package_No] [nvarchar](max) NULL,
	[Package_Status] [nvarchar](50) NULL,
	[Item_Status] [nvarchar](50) NULL,
	[Item_id] [int] NULL,
	[UnitPrice] [nvarchar](50) NULL,
	[Total_Qty] [nvarchar](50) NULL,
	[Packed_Item_Qty] [nvarchar](50) NULL,
	[Package_Date] [nvarchar](50) NULL,
	[Package_Cost] [nvarchar](50) NULL,
	[Time_Of_Day] [nvarchar](max) NULL,
	[Date_Of_Day] [nvarchar](max) NULL,
	[Month_Of_Day] [nvarchar](max) NULL,
	[Year_Of_Day] [nvarchar](max) NULL,
 CONSTRAINT [PK_SO_Packages_Detail] PRIMARY KEY CLUSTERED 
(
	[SO_PackageDetail_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SO_Payments]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SO_Payments](
	[SO_Payment_id] [int] IDENTITY(1,1) NOT NULL,
	[SO_Invoice_id] [int] NULL,
	[SO_Payment_Mode] [int] NULL,
	[SO_BankCharges] [nvarchar](max) NULL,
	[SO_Payment_Date] [nvarchar](max) NULL,
	[SO_Total_Amount] [decimal](10, 2) NULL,
	[SO_Paid_Amount] [decimal](10, 2) NOT NULL,
	[SO_Balance_Amount] [decimal](10, 2) NULL,
	[AddedBy] [int] NULL,
	[UpdatedBy] [int] NULL,
	[Time_Of_Day] [nvarchar](max) NULL,
	[Date_Of_Day] [nvarchar](max) NULL,
	[Month_Of_Day] [nvarchar](max) NULL,
	[Year_Of_Day] [nvarchar](max) NULL,
 CONSTRAINT [PK_SO_Payments] PRIMARY KEY CLUSTERED 
(
	[SO_Payment_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Stock]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stock](
	[Stock_id] [int] IDENTITY(1,1) NOT NULL,
	[Item_id] [int] NULL,
	[Physical_Quantity] [nvarchar](50) NULL,
	[Physical_Avail_ForSale] [nvarchar](50) NULL,
	[Physical_Committed] [nvarchar](50) NULL,
	[Accounting_Quantity] [nvarchar](50) NULL,
	[Acc_Avail_ForSale] [nvarchar](50) NULL,
	[Acc_Commited] [nvarchar](50) NULL,
	[OpeningStock] [nvarchar](50) NULL,
	[ReorderLevel] [nvarchar](50) NULL,
	[Enable] [nvarchar](max) NULL,
	[Time_Of_Day] [nvarchar](max) NULL,
	[Date_Of_Day] [nvarchar](max) NULL,
	[Month_Of_Day] [nvarchar](max) NULL,
	[Year_Of_Day] [nvarchar](max) NULL,
 CONSTRAINT [PK_Stock] PRIMARY KEY CLUSTERED 
(
	[Stock_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tokens]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tokens](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[User_id] [int] NULL,
	[Token] [nvarchar](max) NULL,
	[IssueDate] [nvarchar](50) NULL,
	[ExpiryDate] [nvarchar](50) NULL,
 CONSTRAINT [PK_Tokens] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Units]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Units](
	[Unit_id] [int] IDENTITY(1,1) NOT NULL,
	[Unit_Name] [nvarchar](max) NULL,
	[Unit_Code] [nvarchar](max) NULL,
	[Enable] [int] NULL,
	[AddedBy] [int] NULL,
	[UpdatedBy] [int] NULL,
	[Delete_Request_By] [int] NULL,
	[Delete_Status] [nvarchar](50) NULL,
	[Time_Of_Day] [nvarchar](max) NULL,
	[Date_Of_Day] [nvarchar](max) NULL,
	[Month_Of_Day] [nvarchar](max) NULL,
	[Year_Of_Day] [nvarchar](max) NULL,
 CONSTRAINT [PK_Units] PRIMARY KEY CLUSTERED 
(
	[Unit_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User_Privileges]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_Privileges](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Priv_id] [int] NULL,
	[User_id] [int] NULL,
	[Add] [int] NULL,
	[Edit] [int] NULL,
	[Delete] [int] NULL,
	[View] [int] NULL,
	[Profile] [int] NULL,
	[Receive] [int] NULL,
	[BillAdd] [int] NULL,
	[BillView] [int] NULL,
	[PaymentAdd] [int] NULL,
	[PayementView] [int] NULL,
	[ControllerName] [nvarchar](50) NULL,
	[Time_Of_Day] [nvarchar](255) NULL,
	[Date_Of_Day] [nvarchar](255) NULL,
	[Month_Of_Day] [nvarchar](255) NULL,
	[Year_Of_Day] [nvarchar](255) NULL,
 CONSTRAINT [PK_User_Privileges] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 9/21/2019 2:13:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[email] [nvarchar](max) NULL,
	[password] [nvarchar](max) NULL,
	[attached_profile] [nvarchar](max) NULL,
	[Premises_id] [int] NULL,
	[Role_id] [int] NULL,
	[UserImage] [nvarchar](max) NULL,
	[pao] [nvarchar](max) NULL,
	[paf] [nvarchar](max) NULL,
	[pas] [nvarchar](max) NULL,
	[pas_] [nvarchar](max) NULL,
	[pav] [nvarchar](max) NULL,
	[pap] [nvarchar](max) NULL,
	[pac] [nvarchar](max) NULL,
	[pas__] [nvarchar](max) NULL,
	[pae] [nvarchar](max) NULL,
	[pap_] [nvarchar](max) NULL,
	[pai] [nvarchar](max) NULL,
	[pas___] [nvarchar](max) NULL,
	[pau] [nvarchar](max) NULL,
	[puo] [nvarchar](max) NULL,
	[puf] [nvarchar](max) NULL,
	[pus] [nvarchar](max) NULL,
	[pus_] [nvarchar](max) NULL,
	[puv] [nvarchar](max) NULL,
	[pup] [nvarchar](max) NULL,
	[puc] [nvarchar](max) NULL,
	[pus__] [nvarchar](max) NULL,
	[pue] [nvarchar](max) NULL,
	[pup_] [nvarchar](max) NULL,
	[pui] [nvarchar](max) NULL,
	[pus___] [nvarchar](max) NULL,
	[puu] [nvarchar](max) NULL,
	[pdo] [nvarchar](max) NULL,
	[pdf] [nvarchar](max) NULL,
	[pds] [nvarchar](max) NULL,
	[pds_] [nvarchar](max) NULL,
	[pdv] [nvarchar](max) NULL,
	[pdp] [nvarchar](max) NULL,
	[pdc] [nvarchar](max) NULL,
	[pds__] [nvarchar](max) NULL,
	[pde] [nvarchar](max) NULL,
	[pdp_] [nvarchar](max) NULL,
	[pdi] [nvarchar](max) NULL,
	[pds___] [nvarchar](max) NULL,
	[pdu] [nvarchar](max) NULL,
	[pvo] [nvarchar](max) NULL,
	[pvf] [nvarchar](max) NULL,
	[pvs] [nvarchar](max) NULL,
	[pvs_] [nvarchar](max) NULL,
	[pvv] [nvarchar](max) NULL,
	[pvp] [nvarchar](max) NULL,
	[pvc] [nvarchar](max) NULL,
	[pvs__] [nvarchar](max) NULL,
	[pve] [nvarchar](max) NULL,
	[pvp_] [nvarchar](max) NULL,
	[pvi] [nvarchar](max) NULL,
	[pvs___] [nvarchar](max) NULL,
	[pvu] [nvarchar](max) NULL,
	[pvol] [nvarchar](max) NULL,
	[pvfl] [nvarchar](max) NULL,
	[pvsl] [nvarchar](max) NULL,
	[pvsl_] [nvarchar](max) NULL,
	[pvvl] [nvarchar](max) NULL,
	[pvpl] [nvarchar](max) NULL,
	[pvcl] [nvarchar](max) NULL,
	[pvsl__] [nvarchar](max) NULL,
	[pvel] [nvarchar](max) NULL,
	[pvpl_] [nvarchar](max) NULL,
	[pvil] [nvarchar](max) NULL,
	[pvsl___] [nvarchar](max) NULL,
	[pvul] [nvarchar](max) NULL,
	[Enable] [int] NULL,
	[AddedBy] [int] NULL,
	[UpdatedBy] [int] NULL,
	[Delete_Request_By] [int] NULL,
	[Delete_Status] [nvarchar](50) NULL,
	[Time_Of_Day] [nvarchar](max) NULL,
	[Date_Of_Day] [nvarchar](max) NULL,
	[Month_Of_Day] [nvarchar](max) NULL,
	[Year_Of_Day] [nvarchar](max) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
USE [master]
GO
ALTER DATABASE [G_Accounting_Systems] SET  READ_WRITE 
GO
