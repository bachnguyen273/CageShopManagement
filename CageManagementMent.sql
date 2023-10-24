create database CageShopManagement

Create table [Role] (
  RoleID int identity(1,1) primary key,
  RoleName nvarchar(20) not null
)

Create table [User] (
  UserID int identity(1,1) primary key,
  UserName nvarchar(20) not null,
  [Password] nvarchar(20) not null,
  Email nvarchar(50) not null,
  Phone nvarchar(20) not null,
  RoleID int not null,
  IsActive bit not null,
  Constraint FK_User_Role_RoleID Foreign key (RoleID) references [Role](RoleID)
)

Create table Material (
  MaterialID int identity(1,1) primary key,
  MaterialName nvarchar(20) not null,
  Price decimal(19,4) not null,
  Quantity int not null
)

Create table BirdCage (
  CageID int identity(1,1) primary key,
  CageName nvarchar(30) not null,
  ImagePath nvarchar(255),
  BirdType nvarchar(30) not null,
  Size float not null,
  NumOfPerches int not null,
  Accessories nvarchar(100) not null,
  Price decimal(19,4) not null,
  Quantity int not null,
  CreatedByCustomer int,
  IsAvailable bit not null,
  Constraint FK_BirdCage_User_CreatedByCustomer Foreign key (CreatedByCustomer) references [User](UserID)
)

Create table BirdCageMaterial (
  CageID int,
  MaterialID int,
  Quantity int not null,
  Constraint FK_BirdCageMaterial_BirdCage_CageID Foreign key (CageID) references BirdCage(CageID),
  Constraint FK_BirdCageMaterial_Material_MaterialID Foreign key (MaterialID) references Material(MaterialID)
)

Create table [Status] (
  StatusID int identity(1,1) primary key,
  StatusName nvarchar(20) not null
)

Create table [Order] (
  OrderID int identity(1,1) primary key,
  OrderDate DateTime not null,
  DeliveryDate DateTime not null,
  ReceiverName nvarchar(50) not null,
  [Address] nvarchar(100) not null,
  TotalPrice decimal(19,4) not null,
  CustomerID int not null,
  StatusID int not null,
  Constraint FK_Order_User_CustomerID Foreign key (CustomerID) references [User](UserID),
  Constraint FK_Order_Status_StatusID Foreign key (StatusID) references [Status](StatusID)
)


Create table OrderDetail (
  OrderDetailID int identity (1,1) primary key,
  OrderID int not null,
  CageID int,
  Quantity int not null,
  Subtotal decimal(19,4) not null,
  Constraint FK_OrderDetail_Order_OrderID Foreign key (OrderID) references [Order](OrderID),
  Constraint FK_OrderDetail_BirdCage_CageID Foreign key (CageID) references BirdCage(CageID),
)

GO -- Separating the trigger creation as a separate batch

-- Create a trigger on the OrderDetail table
CREATE TRIGGER CalculateOrderDetailSubtotal
ON OrderDetail
AFTER INSERT, UPDATE
AS
BEGIN
  SET NOCOUNT ON;

  -- Update the Subtotal column for the inserted/updated rows
  UPDATE od
  SET od.Subtotal = c.Price * od.Quantity
  FROM OrderDetail od
  INNER JOIN inserted i ON od.OrderDetailID = i.OrderDetailID
  INNER JOIN BirdCage c ON od.CageID = c.CageID;
END
