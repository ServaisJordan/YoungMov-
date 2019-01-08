drop table carpooling_applicant;
drop table carpooling
drop table private_message;
drop table Car;
drop table trusted_carpooling_driver;
drop table [user];


create table [User] (
	id int identity(1,1) primary key,
	[password] varchar(250) not null,
	userName varchar(250) unique not null,
	[role] varchar(250) not null,
	email varchar(250) unique not null,
	email_validated_at datetime,
	gender char(1) not null,
	adresse varchar(250),
	facePhoto_Filename varchar(250),
	facePhoto_sent_at dateTime,
	facePhoto_validated_at datetime,
	identityPiece_filename varchar(250),
	identityPiece_sent_at datetime,
	identityPiece_validated_at datetime,
	phone varchar(250),
	trusted_carpooling_driver_code varchar(250),
	created_at datetime,
	updated_at datetime,
	locality varchar(250) not null,
	postalCode varchar(250) not null,
	timestamp);

create table trusted_carpooling_driver (
	id int IDENTITY(1,1) PRIMARY key,
	[user] int foreign key references [user](id) not null,
	carpooler int foreign key references [user](id) not null,
	created_at DateTime,
	timestamp);


create table car (
	id int identity(1,1) primary key,
	created_at datetime,
	validated_at datetime,
	color varchar(250) not null,
	license_plate_number varchar(250) unique not null,
	car_model varchar(250) not null,
	[owner] int foreign key references [user](id) not null);


create table carpooling (
	id int identity(1,1) primary key,
	[description] varchar(250),
	nb_places int not null,
	place_price smallmoney not null,
	created_at datetime,
	updated_at datetime,
	destination_from varchar(250) not null,
	destination_to varchar(250) not null,
	locality_from varchar(250) not null,
	locality_to varchar(250) not null,
	postalCode_To varchar(250) not null,
	postalCode_From varchar(250) not null,
	car int foreign key references car(id) not null,
	creator int foreign key references [user](id) not null,
	timestamp);


create table carpooling_applicant (
	id int IDENTITY(1,1) primary key,
	carpooling int foreign key references carpooling(id) not null,
	[User] int foreign key references [User](id) not null,
	has_been_accepted bit not null);


create table private_message (
	id int identity(1,1) primary key,
	content varchar(250) not null,
	has_been_read bit not null,
	created_at datetime not null constraint Private_message_Date_creation default getDate(),
	creator int foreign key references [user](id) not null,
	reponse int foreign key references private_message(id));

go
CREATE TRIGGER TR_User_Modification
	ON dbo.[User]
AFTER UPDATE
AS
BEGIN
	UPDATE dbo.[User]
	SET updated_at = GETDATE()
	FROM dbo.[User] U
	JOIN INSERTED new ON new.Id = U.Id
end
go


go
create TRIGGER TR_USER_CREATION
	on dbo.[user]
	after insert
as
begin
	update dbo.[user]
	set created_at = getDate() , updated_at = getdate()
	from dbo.[User] u
	join inserted new on new.id = u.id
end
go


go
create TRIGGER TR_Car_CREATION
	on dbo.car
	after insert
as
begin
	update dbo.car
	set created_at = getDate()
	from dbo.car u
	join inserted new on new.id = u.id
end
go


go
CREATE TRIGGER TR_carpooling_Modification
	ON dbo.carpooling
AFTER UPDATE
AS
BEGIN
	UPDATE dbo.carpooling
	SET updated_at = GETDATE()
	FROM dbo.carpooling C
	JOIN INSERTED new ON new.Id = C.Id
end
go

go
create TRIGGER TR_carpooling_Creation
	on dbo.Carpooling
after insert
as
begin
	update dbo.carpooling
	set created_at = getDate() , updated_at = getDate()
	from dbo.carpooling c
	join inserted new on new.Id = c.Id
end
go

go
CREATE TRIGGER TR_Trusted_Carpooler_Creation
	on dbo.trusted_carpooling_driver
after insert
as 
BEGIN
	update dbo.trusted_carpooling_driver
	set created_at = getDate()
	from dbo.trusted_carpooling_driver t
	join inserted new on new.[User] = t.[User] and new.carpooler = t.carpooler
END
go