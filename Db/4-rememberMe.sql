create table if not exists UserToken (
	UserTokenId uuid primary key,
	UserId int,
	Created timestamp
)