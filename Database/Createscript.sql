-- Creates tables for database.

USE circulaire_ICT_keten
GO

CREATE TABLE User_Data (
	userID					int 			NOT NULL 		identity,
	businessID				int				NULL,							-- null wanneer de user niet gerelateerd is aan de business.
	roleID					int				NOT NULL		default(0),		-- Rol geeft aan welke autoriteit de gebruiker heeft.
	password				varchar(64)		NOT NULL,						-- Wachtwoord moet gehashed in de database komen.
	firstname				varchar(32)		NOT NULL,
	lastname				varchar(32)		NOT NULL,
	date_of_birth			date			NULL,							-- Leeftijd moet voldoen aan bepaalde eisen. 
	street					varchar(64)		NOT NULL,						-- straatnaam
	street_number			smallint		NOT NULL,						-- 123
	street_add				varchar(2)		NULL,							-- A
	postalcode				varchar(7)		NOT NULL,						-- 0000 AA
	city					varchar(32)		NOT NULL,						-- Heerlen
	email					varchar(128)	NOT NULL,						-- email@name.com
	phone					int				NOT NULL,						-- 0612345678
	balance					int				NOT NULL		default(0),		-- User krijgt credits na inleveren van product.
	
	PRIMARY KEY (userID),
	CONSTRAINT UC_User_Data UNIQUE (email),
	CONSTRAINT CH_User_email CHECK (email like '%@%' and email like '%.%'),
	CONSTRAINT CH_User_Firstname CHECK (firstname not like '%[^a-zA-Z]%'),
	CONSTRAINT CH_User_Lastname CHECK (lastname not like '%[^a-zA-Z]%'),
	CONSTRAINT CH_User_Street CHECK (street not like '%[^a-zA-Z]%'),
	CONSTRAINT CH_User_Postalcode CHECK (postalcode not like '%[^a-zA-Z0-9]%'),
);
GO

CREATE TABLE Business_Data (
	businessID				int				NOT NULL		identity,
	business_name			varchar(64)		NOT NULL,						-- Naam van het bedrijf.
	business_email			varchar(128)	NOT NULL		unique,			-- email@name.com
	business_phone			int				NOT NULL,						-- 0612345678

	PRIMARY KEY (businessID),
	CONSTRAINT CH_Business_Name CHECK (business_name not like '%[^a-zA-Z]%'),
	CONSTRAINT UC_Business_Data UNIQUE (business_email, business_name),
	CONSTRAINT CH_Business_email CHECK (business_email like '%@%' and business_email like '%.%'),
);
GO

CREATE TABLE Product (
	productID				int				NOT NULL		identity,
	categoryID				int				NOT NULL,
	statusID				int				NOT NULL,
	conditionID				int				NOT NULL,
	name					varchar(56)		NOT NULL,
	description				varchar(MAX)	NULL,
	brand					varchar(24)		NULL,
	credit_value			int				NOT NULL,						-- Waarde van het product.
	
	PRIMARY KEY (productID),
	CONSTRAINT UC_Product UNIQUE (name),
	CONSTRAINT CH_Product_Name CHECK (name not like '%[^a-zA-Z]%')
);

CREATE TABLE Role (
	roleID					int				NOT NULL		identity,
	role_name				varchar(24)		NOT NULL,

	PRIMARY KEY (roleID),
	CONSTRAINT UC_Role UNIQUE (role_name),
	CONSTRAINT CH_Role_Name CHECK (role_name not like '%[^a-zA-Z]%')
);

CREATE TABLE Remark (
	remarkID				int				NOT NULL		identity,
	productID				int				NOT NULL,						-- Het product dat commentaar krijgt.
	remark_date				datetime		NOT NULL,						-- Wanneer het commentaar is toegevoegd.
	remark					varchar(MAX)	NOT NULL,

	PRIMARY KEY (remarkID),
);

CREATE TABLE Status (
	statusID				int				NOT NULL		identity,
	status					varchar(24)		NOT NULL,						-- Bijvoorbeeld uitgeleend.

	PRIMARY KEY (statusID),
	CONSTRAINT UC_Status UNIQUE (status),
	CONSTRAINT CH_Status_Title CHECK (status not like '%[^a-zA-Z]%')
);

CREATE TABLE Category (
	categoryID				int				NOT NULL		identity,
	category				varchar(24)		NOT NULL,						-- Bijvoorbeeld laptop.
	
	PRIMARY KEY (categoryID),
	CONSTRAINT UC_Category UNIQUE (category),
	CONSTRAINT CH_Category_Title CHECK (category not like '%[^a-zA-Z]%')
);
CREATE TABLE Condition (
	conditionID				int				NOT NULL		identity,
	condition				varchar(24)		NOT NULL,						-- Bijvoorbeeld kapot.
	
	PRIMARY KEY (conditionID),
	CONSTRAINT UC_Condition UNIQUE (condition),
	CONSTRAINT CH_Condition_Title CHECK (condition not like '%[^a-zA-Z]%')
);

CREATE TABLE Loaned_Product	(
	loanedID				int				NOT NULL		identity,
	userID					int				NOT NULL,						-- User die het product leent.
	productID				int				NOT NULL,						-- Product dat geleend wordt.
	date_start				datetime		NOT NULL,
	date_end				datetime		NULL,
	
	PRIMARY KEY (loanedID),
);

CREATE TABLE Product_Log (
	logID					int				NOT NULL		identity,
	productID				int				NOT NULL,
	employeeID				int				NOT NULL,						
	change_date				datetime		NOT NULL,
	change_log				varchar(MAX)	NOT NULL,

	PRIMARY KEY (logID),
);

ALTER TABLE User_Data
ADD CONSTRAINT FK_User_Business FOREIGN KEY (businessID) REFERENCES Business_Data(businessID);

ALTER TABLE User_Data
ADD CONSTRAINT FK_User_Role FOREIGN KEY (roleID) REFERENCES Role(roleID);

ALTER TABLE Product
ADD CONSTRAINT FK_Product_Category FOREIGN KEY (categoryID) REFERENCES Category(categoryID);

ALTER TABLE Product
ADD CONSTRAINT FK_Product_Status FOREIGN KEY (statusID) REFERENCES Status(statusID);

ALTER TABLE Condition
ADD CONSTRAINT FK_Product_Condition FOREIGN KEY (conditionID) REFERENCES Condition(conditionID);

ALTER TABLE Remark
ADD CONSTRAINT FK_Remark_Product FOREIGN KEY (productID) REFERENCES Product(productID);

ALTER TABLE Loaned_Product
ADD CONSTRAINT FK_Loaned_User FOREIGN KEY (userID) REFERENCES User_Data(userID);

ALTER TABLE Loaned_Product
ADD CONSTRAINT FK_Loaned_Product FOREIGN KEY (productID) REFERENCES Product(productID);

ALTER TABLE Product_Log
ADD CONSTRAINT FK_Log_Product FOREIGN KEY (productID) REFERENCES Product(productID);

ALTER TABLE Product_Log
ADD CONSTRAINT FK_Log_User FOREIGN KEY (employeeID) REFERENCES User_Data(userID);

USE MASTER
GO

