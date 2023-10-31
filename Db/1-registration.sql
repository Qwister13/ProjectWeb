create table if not exists  AppUser (
    UserId serial primary key,
    Email varchar(50),
    Password varchar(100),
    Salt varchar(50),
    Status int
);

create table if not exists UserSecurity (
    UserSecurityId serial primary key,
    UserId int, 
    VerificationCode varchar(50)
);

create table if not exists EmailQueue (
    EmailQueueId serial primary key,
    EmailTo varchar(200),
    EmailFrom varchar(200),
    EmailSubject varchar(200), 
    EmailBody text,
    Created time,
    ProcessingId varchar(100),
    Retry int
);


create index if not exists IX_AppUser_Email on AppUser(
	Email
);

alter table AppUser drop if exists FirstName;
alter table AppUser drop if exists LastName;
alter table AppUser drop if exists ProfileImage;
