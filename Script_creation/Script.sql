
use master
go
create database horizon
go
use horizon
--Création de la table product
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='product') 
drop table [product]
go
	CREATE TABLE [dbo].[product](
	[id] [int] NOT NULL,
	[ref] [nvarchar](50) NULL,
	[name] [nvarchar](50) NULL,
	[description] [nvarchar](50) NULL,
	[price] [float] NULL,
	primary key(id),
)
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Listproducts')
DROP PROCEDURE Listproducts
GO
create procedure Listproducts as
select * from product
go
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Deleteproduct')
DROP PROCEDURE selectproductbyid
GO
create procedure Deleteproduct @id int as
delete from product where id=@id
go
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'selectproductbyid')
DROP PROCEDURE selectproductbyid
GO
create procedure selectproductbyid @id int as

select * from product where id=@id
go
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'addproduct')
DROP PROCEDURE addproduct
GO
CREATE procedure addproduct @ref nvarchar(50),@name nvarchar(50),@description nvarchar(100), @price float
as
insert into product
select isnull(max(id),0)+1, @ref,@name,@description,@price from product
go
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'updateproduct')
DROP PROCEDURE updateproduct
GO
create procedure updateproduct @id int,@ref nvarchar(50), @Name nvarchar(50),@Description nvarchar(50) ,@Price float

 as

update product 

set ref=@ref, Name=@name, description = @Description, 
price =@Price where id =@id
go