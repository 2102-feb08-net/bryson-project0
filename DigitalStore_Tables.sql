--SQL File used to generate tables and initial data for Project 0
--By Bryson Ewell


--DROP TABLE Product
CREATE TABLE Product (
	Id	INT NOT NULL IDENTITY PRIMARY KEY,
	Name NVARCHAR(100) NOT NULL UNIQUE CHECK(LEN(Name) >= 1),
	Category NVARCHAR(100) NOT NULL,
	UnitPrice MONEY NOT NULL
);

--DROP TABLE Address
CREATE TABLE Address (
	Id INT NOT NULL IDENTITY PRIMARY KEY,
	Address1 NVARCHAR(100) NOT NULL,
	Address2 NVARCHAR(100) NULL,
	City NVARCHAR(100) NOT NULL,
	State NVARCHAR(100) NOT NULL,
	Country NVARCHAR(100) NOT NULL,
	ZipCode INT NOT NULL,
);

--DROP TABLE StoreLocation
CREATE TABLE StoreLocation (
	Id INT NOT NULL IDENTITY PRIMARY KEY,
	Name NVARCHAR(100) NOT NULL UNIQUE,
	AddressId INT NOT NULL UNIQUE,
	FOREIGN KEY (AddressId) REFERENCES Address(Id),
);

--DROP TABLE Inventory
CREATE TABLE Inventory (
	Id INT NOT NULL IDENTITY PRIMARY KEY,
	ProductId INT NOT NULL,
	StoreId INT NOT NULL,
	Quantity INT NOT NULL,
	FOREIGN KEY (ProductId) REFERENCES Product(Id),
	FOREIGN KEY (StoreId) REFERENCES StoreLocation(Id)
);

--DROP TABLE Customer
CREATE TABLE Customer (
	Id INT NOT NULL IDENTITY PRIMARY KEY,
	FirstName NVARCHAR(100) NOT NULL CHECK(LEN(FirstName) > 1),
	LastName NVARCHAR(100) NULL,
	--AddressId INT NOT NULL,
);

--DROP TABLE PurchaseOrder
CREATE TABLE PurchaseOrder (
	Id INT NOT NULL IDENTITY PRIMARY KEY,
	CustomerId INT NOT NULL,
	StoreLocationId INT NOT NULL,
	DateProcessed DATETIMEOFFSET NOT NULL,
	FOREIGN KEY (CustomerId) REFERENCES Customer(Id),
	FOREIGN KEY (StoreLocationId) REFERENCES StoreLocation(Id)
)


--DROP TABLE OrderLine
CREATE TABLE OrderLine (
	Id INT NOT NULL IDENTITY PRIMARY KEY,
	PurchaseOrderId INT NOT NULL,
	ProductId INT NOT NULL,
	PurchaseUnitPrice MONEY NOT NULL,
	Quantity INT NOT NULL
	FOREIGN KEY (ProductId) REFERENCES Product(Id),
	FOREIGN KEY (PurchaseOrderId) REFERENCES PurchaseOrder(Id)
);
GO

--DROP FUNCTION GetProductId
CREATE FUNCTION GetProductId (@productName NVARCHAR(100))
RETURNS INT
AS
BEGIN
RETURN (
	SELECT Id
	FROM Product
	WHERE Name = @productName
	);
END
GO

--DROP FUNCTION GetStoreLocationId
CREATE FUNCTION GetStoreLocationId (@locationName NVARCHAR(100))
RETURNS INT
AS
BEGIN
RETURN (
	SELECT Id
	FROM StoreLocation
	WHERE Name = @locationName
	);
END
GO

INSERT INTO Product (Name, Category, UnitPrice) VALUES
	('Apple', 'Food', 1.29),
	('Banana', 'Food', 0.79),
	('Orange', 'Food', 1.49)


INSERT INTO Address (Address1, Address2, City, State, Country, ZipCode) VALUES
	('3927 Kenwood Place', NULL, 'Orlando', 'Florida', 'United States', 32801),
	('1163 Rockbridge Road', NULL, 'Apopka', 'Florida', 'United States', 32704),
	('1478 Hamill Avenue', NULL, 'San Diego', 'California', 'United States', 92123)

INSERT INTO StoreLocation (Name, AddressId) VALUES
	('HQ', (SELECT Id FROM Address WHERE Address1 = '3927 Kenwood Place')),
	('Warehouse_East1', (SELECT Id FROM Address WHERE Address1 = '1163 Rockbridge Road')),
	('Warehouse_West1', (SELECT Id FROM Address WHERE Address1 = '1478 Hamill Avenue'))

INSERT INTO Inventory (StoreId, ProductId, Quantity) VALUES
	(dbo.GetStoreLocationId('HQ'), dbo.GetProductId('Apple'), 54),
	(dbo.GetStoreLocationId('HQ'), dbo.GetProductId('Banana'), 92),
	(dbo.GetStoreLocationId('HQ'), dbo.GetProductId('Orange'), 122),
	(dbo.GetStoreLocationId('Warehouse_West1'), dbo.GetProductId('Banana'), 63),
	(dbo.GetStoreLocationId('Warehouse_East1'), dbo.GetProductId('Orange'), 152)

SELECT * FROM Product

SELECT * FROM Address

SELECT * FROM StoreLocation

SELECT * FROM Inventory

SELECT * FROM Customer

SELECT * FROM PurchaseOrder

SELECT * FROM OrderLine