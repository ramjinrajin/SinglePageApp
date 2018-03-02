CREATE Procedure dbo.USP_FeedInsert
@Title       varchar(100),
@Description nvarchar(max),
@Images      varchar(100)
AS
BEGIN
    SET NOCOUNT ON;

	INSERT INTO
	[dbo].[Gs_Feed]
	(
	Title,
	[Description],
	Images
	)
	SELECT
	@Title, @Description,@Images

	SET NOCOUNT OFF;
END
GO


CREATE Procedure dbo.USP_FeedUPDATE
@FeedID    int,
@Title       varchar(100),
@Description nvarchar(max),
@Images      varchar(100),
@link    varchar(100),
@Rowstatus char(1)
AS
BEGIN
    SET NOCOUNT ON;

	UPDATE GS_FEED
	SET Title=@Title,
		Description=@Description,
		Images=@Images,
		link=@link,
		Rowstatus=@Rowstatus
	WHERE FeedID=@FeedID
	
	SET NOCOUNT OFF;
END
GO


    