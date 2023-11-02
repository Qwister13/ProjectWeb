create table if not exists DbSession (
	DbSessionId uuid primary key,
	SessionData text,
	Created timestamp,
	LastAccessed timestamp,
	UserId int 

)