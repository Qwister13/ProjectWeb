create table if not exists Profile
(
	ProfileID serial primary key,
	UserId int,
	ProfileName varchar(50),
	FirstName varchar(50),
	LastName varchar(50),
	ProfileImage varchar(100)
);
