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
	created_at datetime constraint User_Date_creation default getDate(),
	updated_at datetime CONSTRAINT User_Date_modification default getDate(),
	locality varchar(250) not null,
	postalCode varchar(250) not null,
	timestamp);

create table trusted_carpooling_driver (
	[user] int foreign key references [user](id) not null,
	carpooler int foreign key references [user](id) not null,
	created_at DateTime not null constraint Trusted_carpooling_driver_Date_creation default getDate(),
	timestamp,
	constraint Pk_trusted_carpooling_driver primary key([user], carpooler));


create table car (
	id int identity(1,1) primary key,
	created_at datetime not null constraint Car_Date_creation default getDate(),
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
	created_at datetime constraint carpooling_Date_creation default getDate(),
	updated_at datetime constraint carpooling_Date_modification default getDate(),
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

delete from carpooling_applicant;
delete from carpooling;
delete from private_message;
delete from Car;
delete from trusted_carpooling_driver;
delete from [user];

insert into dbo.[User] ([role], [password], userName, email, gender, locality, postalCode) values(
	'backoffice',
	'123',
	'jordan',
	'jordanservais@hotmail.be',
	'm',
	'marcher-en-famenne',
	'6950');

	insert into dbo.Car (color, license_plate_number, car_model, [owner]) values (
		'black', 'jklm956', 'porsh', (select id from [user] where userName = 'jordan')
	);


insert into dbo.carpooling (nb_places, place_price, destination_from, destination_to, locality_from, locality_to, postalCode_From, postalCode_To, Car, creator) values (
	3,56, 'place roi albert', 'rue joseph calozet', 'marche-en-famenne', 'Namur', '6950', '7000', (select id from car where owner = (select id from [user] where username = 'jodran') and color = 'black'), (select id from [user] where userName = 'jordan')
);

insert into dbo.[User] ([role], [password], userName, email, gender, locality, postalCode) values(
	'backoffice',
	'456',
	'dylan',
	'dylanservais@hotmail.be',
	'm',
	'marcher-en-famenne',
	'6950');

insert into carpooling_applicant (carpooling, [User], has_been_accepted) values (
	(select id from carpooling where nb_places = 3), (select id from [user] where userName = 'dylan'), 1
);

insert into dbo.[User] ([role], [password], userName, email, gender, locality, postalCode) values(
	'backoffice',
	'1234567',
	'Daniellyson',
	'Daniellyson@hotmail.be',
	'm',
	'inconnu',
	'0000');