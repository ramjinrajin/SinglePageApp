Create table Gs_Feed
(
FeedID int Primary key identity(1,1),
Title varchar(100),
[Description] nvarchar(max),
Images varchar(100),
link varchar(100),
Rowstatus char(1) default 'A',
createdate datetime default getdate(),
modifieddate datetime,
createdby int,
modifedby int
)


--gs_users
go

Create table Gs_users
(
UserID int Primary key identity(1,1),
Username varchar(100),
EmailID  varchar(50),
mobilenno int,
description nvarchar(max),
Rowstatus char(1) default getdate(),
createdate datetime default getdate(),
modifieddate datetime
)

go
Create table Gs_UserActivity
(
ActivityID int primary key identity(1,1),
UserID int Foreign Key References Gs_Users(UserID),
FeedID int Foreign Key References Gs_Feed(FeedID),
description nvarchar(max),
Rowstatus char(1) default getdate(),
createdate datetime default getdate(),
modifieddate datetime
)

GO

Create table Gs_Log
(
LogID int primary key identity(1,1),
Logtitle varchar(10),
ErrorDesc nvarchar(max),
Formatmsgs nvarchar(max),
createdate datetime default getdate()
)

















