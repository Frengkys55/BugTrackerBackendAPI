  IF DB_ID('BugTracker_Demo') IS NULL
BEGIN;
	CREATE DATABASE BugTracker_Demo;
	-- Check if database was created
END;

GO

IF DB_ID('BugTracker_Demo') IS NULL
BEGIN;
	THROW 51000, 'Database creation failed.', 1;
END;
ELSE
BEGIN
	USE BugTracker_Demo;

	-- [ USERS ]-----------------------------------------------------------------------------------

	--------------------------------------[ USERS ]--------------------------------------
	IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'USERS')
	BEGIN;
		CREATE TABLE USERS (
			Id INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
			Guid NVARCHAR(40) NOT NULL UNIQUE,
			Username NVARCHAR(255) NOT NULL,
			Password NVARCHAR(255) NOT NULL,
			Email NVARCHAR(255) NOT NULL,
			FirstName NVARCHAR(255) NULL,
			LastName NVARCHAR(255) NULL,
			PhoneNumber INT NULL,
		)
	END;
	
	--------------------------------------[ USER TOKENS ]--------------------------------------
	IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'USERTOKENS')
	BEGIN;
		CREATE TABLE USERTOKENS(
			Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
			UserGuid NVARCHAR(40) NOT NULL FOREIGN KEY REFERENCES USERS(Guid),
			Token NVARCHAR(MAX) NOT NULL,
			DateCreated DATETIME NOT NULL,
			LastAccess DATETIME NOT NULL
		)
	END;

	--------------------------------------[ USER REQUESTS ]--------------------------------------
	-- This table is for which user made a requests regarding securities
	IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'USERREQUESTS')
	BEGIN
		CREATE TABLE USERREQUESTS(
			Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
			UserGuid NVARCHAR(40) NOT NULL FOREIGN KEY REFERENCES USERS(Guid),
			RequestToken NVARCHAR(MAX) NOT NULL,
			LastRequestDate DATETIME NOT NULL
		)
	END;


	-- [ PROJECTS ]-----------------------------------------------------------------------------------

	--------------------------------------[ PROJECT STATUS ]--------------------------------------
	IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'PROJECTSTATUS')
	BEGIN;
		CREATE TABLE PROJECTSTATUS(
			Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
			Guid NVARCHAR(40) NOT NULL UNIQUE,
			Name NVARCHAR(255) NOT NULL,
			Description NVARCHAR(MAX) NULL,
			IsDefault BINARY NULL
		)
	END;

	--------------------------------------[ PROJECTS ]--------------------------------------
	IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'PROJECTS')
	BEGIN;
		CREATE TABLE PROJECTS (
			Id INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
			Guid NVARCHAR(40) NOT NULL UNIQUE,
			Name NVARCHAR(MAX) NOT NULL,
			Description NVARCHAR(MAX) NULL,
			ProjectIconUrl NVARCHAR(MAX) NULL,
			ProjectBackgroundImageUrl NVARCHAR(MAX) NULL,
			DateCreated DATETIME NOT NULL,
			DateModified DATETIME NOT NULL,
			UserGuid NVARCHAR(40) NOT NULL FOREIGN KEY REFERENCES USERS(guid),
			ProjectStatusGuid NVARCHAR(40) FOREIGN KEY REFERENCES PROJECTSTATUS(Guid)
		)
	END;



	-- [ TICKETS ]-----------------------------------------------------------------------------------

	--------------------------------------[ TICKET SEVERITIES ]--------------------------------------
	IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'TICKETSEVERITIES')
	BEGIN;
		CREATE TABLE TICKETSEVERITIES(
			Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
			Guid NVARCHAR(40) NOT NULL UNIQUE,
			Title NVARCHAR(255) NOT NULL,
			Description NVARCHAR(MAX) NULL,
			SeverityIndex INT NOT NULL DEFAULT 0
		)
	END;
	ELSE
	BEGIN
		--Guid
		IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE COLUMN_NAME = 'Guid' AND TABLE_NAME = 'TICKETSEVERITIES')
		BEGIN
			ALTER TABLE TICKETSEVERITIES
			ADD Guid NVARCHAR(40)
		END
		--Title
		IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE COLUMN_NAME = 'Title' AND TABLE_NAME = 'TICKETSEVERITIES')
		BEGIN
			ALTER TABLE TICKETSEVERITIES
			ADD Title NVARCHAR(40)
		END
		--Description
		IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE COLUMN_NAME = 'Description' AND TABLE_NAME = 'TICKETSEVERITIES')
		BEGIN
			ALTER TABLE TICKETSEVERITIES
			ADD Description NVARCHAR(40)
		END
		--Severity index
		IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE COLUMN_NAME = 'SeverityIndex' AND TABLE_NAME = 'TICKETSEVERITIES')
		BEGIN
			ALTER TABLE TICKETSEVERITIES
			ADD SeverityIndex NVARCHAR(40)
		END
	END

	--------------------------------------[ TICKET TYPE ]--------------------------------------
	IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'TICKETTYPE')
	BEGIN;
		CREATE TABLE TICKETTYPE(
			Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
			Guid NVARCHAR(40) NOT NULL UNIQUE,
			Name NVARCHAR(255) NOT NULL,
			Description NVARCHAR(MAX) NULL
		)
	END;
	ELSE
	BEGIN
		--Guid
		IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE COLUMN_NAME = 'Guid' AND TABLE_NAME = 'TICKETTYPE')
		BEGIN
			ALTER TABLE TICKETTYPE
			ADD Guid NVARCHAR(40)
		END
		--Name
		IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE COLUMN_NAME = 'Name' AND TABLE_NAME = 'TICKETTYPE')
		BEGIN
			ALTER TABLE TICKETTYPE
			ADD Name NVARCHAR(255)
		END
		--Description
		IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE COLUMN_NAME = 'Description' AND TABLE_NAME = 'TICKETTYPE')
		BEGIN
			ALTER TABLE TICKETTYPE
			ADD Description NVARCHAR(MAX)
		END
	END

	--------------------------------------[ TICKETS ]--------------------------------------
	IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'TICKETS')
	BEGIN;
		CREATE TABLE TICKETS(
			Id INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
			Guid NVARCHAR(40) NOT NULL UNIQUE,
			ProjectGuid NVARCHAR(40) NOT NULL FOREIGN KEY REFERENCES PROJECTS(Guid),
			UserGuid NVARCHAR(40) NOT NULL FOREIGN KEY REFERENCES USERS(Guid),
			Name NVARCHAR(MAX) NOT NULL,
			Description NVARCHAR(MAX) NULL,
			DateCreated DATETIME NOT NULL,
			DateModified DATETIME NOT NULL,
			DateSolved DATETIME NULL,
			SeverityGuid NVARCHAR(40) NOT NULL FOREIGN KEY REFERENCES TICKETSEVERITIES(Guid),
			TypeGuid NVARCHAR(40) NOT NULL FOREIGN KEY REFERENCES TICKETTYPE(Guid)
		)
	END;
	ELSE
	BEGIN
		--Guid
		IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE COLUMN_NAME = 'Guid' AND TABLE_NAME = 'TICKETS')
		BEGIN
			ALTER TABLE TICKETS
			ADD Guid NVARCHAR(40)
		END
		--ProjectGuid
		IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE COLUMN_NAME = 'ProjectGuid' AND TABLE_NAME = 'TICKETS')
		BEGIN
			ALTER TABLE TICKETS
			ADD ProjectGuid NVARCHAR(40) NOT NULL
		END
		--UserGuid
		IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE COLUMN_NAME = 'UserGuid' AND TABLE_NAME = 'TICKETS')
		BEGIN
			ALTER TABLE TICKETS
			ADD UserGuid NVARCHAR(40) NOT NULL
		END
		--Name
		IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE COLUMN_NAME = 'Name' AND TABLE_NAME = 'TICKETS')
		BEGIN
			ALTER TABLE TICKETS
			ADD Name NVARCHAR(MAX) NOT NULL
		END
		--Description
		IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE COLUMN_NAME = 'Description' AND TABLE_NAME = 'TICKETS')
		BEGIN
			ALTER TABLE TICKETS
			ADD Description NVARCHAR(MAX) NULL
		END
		--DateCreated
		IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE COLUMN_NAME = 'DateCreated' AND TABLE_NAME = 'TICKETS')
		BEGIN
			ALTER TABLE TICKETS
			ADD DateCreated DATETIME NOT NULL
		END
		--DateModified
		IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE COLUMN_NAME = 'DateModified' AND TABLE_NAME = 'TICKETS')
		BEGIN
			ALTER TABLE TICKETS
			ADD DateModified DATETIME NOT NULL
		END
		--DateSolved
		IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE COLUMN_NAME = 'DateSolved' AND TABLE_NAME = 'TICKETS')
		BEGIN
			ALTER TABLE TICKETS
			ADD DateSolved DATETIME NULL
		END
		--SeverityGuid
		IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE COLUMN_NAME = 'SeverityGuid' AND TABLE_NAME = 'TICKETS')
		BEGIN
			ALTER TABLE TICKETS
			ADD SeverityGuid NVARCHAR(40) NOT NULL
		END
		--TypeGuid
		IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE COLUMN_NAME = 'TypeGuid' AND TABLE_NAME = 'TICKETS')
		BEGIN
			ALTER TABLE TICKETS
			ADD TypeGuid NVARCHAR(40) NOT NULL
		END
		
	END
	--------------------------------------[ TICKET COMMENTS ]--------------------------------------
	IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'TICKETCOMMENTS')
	BEGIN;
		CREATE TABLE TICKETCOMMENTS(
			Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
			Guid NVARCHAR(40) NOT NULL UNIQUE,
			TicketGuid NVARCHAR(40) NOT NULL FOREIGN KEY REFERENCES TICKETS(Guid),
			UserGuid NVARCHAR(40) NOT NULL FOREIGN KEY REFERENCES USERS(Guid),
			CommentText NVARCHAR(MAX) NOT NULL,
			DateCreated DATETIME NOT NULL
		)
	END;
	ELSE
	BEGIN
		--[ Compatibility Reason because I forgot to add datetime previously ]---------------------------------------------
		--DateCreated
		IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE COLUMN_NAME = 'DateCreated' AND TABLE_NAME = 'TICKETCOMMENTS')
		BEGIN
			ALTER TABLE TICKETCOMMENTS
			ADD DateCreated DATETIME
		END
	END;

	--------------------------------------[ TICKET COMMENT ATTACHMENT TYPE ]--------------------------------------
	IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'TICKETATTACHMENTTYPES')
	BEGIN;
		CREATE TABLE TICKETATTACHMENTTYPES(
			Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
			Guid NVARCHAR(40) NOT NULL UNIQUE,
			Name NVARCHAR(255) NOT NULL UNIQUE,
			Extensions NVARCHAR(255) NOT NULL UNIQUE,
			IconUrl NVARCHAR(MAX) NULL
		)
	END;
	
	--------------------------------------[ TICKET COMMENT ATTACHMENTS ]--------------------------------------
	IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ATTACHMENTTYPES')
	BEGIN;
		CREATE TABLE ATTACHMENTTYPES(
			Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
			Guid NVARCHAR(40) NOT NULL UNIQUE,
			CommentGuid NVARCHAR(40) NOT NULL FOREIGN KEY REFERENCES TICKETCOMMENTS(Guid),
			TypeGuid NVARCHAR(40) NOT NULL FOREIGN KEY REFERENCES TICKETATTACHMENTTYPES(Guid),
			Name NVARCHAR(MAX) NOT NULL,
			Location NVARCHAR(MAX) NOT NULL
		)
	END;

	
END;

GO

--[ BUG TRACKER VIEWS ]-------------------------------------------------------------------------------------------

--------------------------------------[ PROJECT LIST ]--------------------------------------
CREATE VIEW PROJECTLISTVIEW AS
SELECT
	PROJECTS.Guid [Guid],
	PROJECTS.Name [Name],
	PROJECTS.Description [Description],
	PROJECTS.ProjectIconUrl [IconUrl],
	PROJECTS.ProjectBackgroundImageUrl [BackgroundImageUrl],
	PROJECTS.DateCreated [DateCreated],
	PROJECTS.DateModified [DateModified],
	PROJECTSTATUS.Name [ProjectStatus],
	USERS.Username [Username]
FROM PROJECTS
INNER JOIN PROJECTSTATUS ON PROJECTS.ProjectStatusGuid = PROJECTSTATUS.Guid
INNER JOIN USERS ON PROJECTS.UserGuid = USERS.Guid;

GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.VIEWS WHERE VIEWS.TABLE_NAME = 'ShortProjectList')
BEGIN
	DROP VIEW ShortProjectList;
END
GO

--[ BUG TRACKER STORED PROCEDURES ]-------------------------------------------------------------------------------------------

--#region Add New Project ]--------------------------------------


IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'AddProject' AND ROUTINE_TYPE='PROCEDURE')
BEGIN;
	DROP PROCEDURE AddProject;
END;

GO
--#endregion

CREATE PROCEDURE AddProject
	@Guid NVARCHAR(40),
	@Name NVARCHAR(MAX),
	@Description NVARCHAR(MAX),
	@IconUrl NVARCHAR(MAX),
	@BackgroundImageUrl NVARCHAR(MAX),
	@ProjectStatus NVARCHAR(255),
	@AccessToken NVARCHAR(MAX)
AS
IF EXISTS (SELECT Token FROM USERTOKENS WHERE USERTOKENS.Token = @AccessToken)
BEGIN
	INSERT INTO PROJECTS (
		Guid,
		Name,
		Description,
		ProjectIconUrl,
		ProjectBackgroundImageUrl,
		DateCreated,
		DateModified,
		UserGuid,
		ProjectStatusGuid
	)
	VALUES(
		@Guid,
		@Name,
		@Description,
		@IconUrl,
		@BackGroundImageUrl,
		(SELECT GETDATE()),
		(SELECT GETDATE()),
		(SELECT UserGuid FROM USERTOKENS WHERE Token = @AccessToken),
		(SELECT Guid FROM PROJECTSTATUS WHERE PROJECTSTATUS.Name = @ProjectStatus)
	)
END;
ELSE
BEGIN;
	THROW 51000, 'Invalid access token.', 1;
END;
GO

--------------------------------------[ Edit Project ]--------------------------------------
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'EditProject' AND ROUTINE_TYPE = 'PROCEDURE')
	DROP PROCEDURE EditProject;
GO 

CREATE PROCEDURE EditProject
	@Guid NVARCHAR(40),
	@Name NVARCHAR(MAX),
	@Description NVARCHAR(MAX),
	@IconUrl NVARCHAR(MAX),
	@BackGroundImageUrl NVARCHAR(MAX),
	@ProjectStatus NVARCHAR(255),
	@AccessToken NVARCHAR(MAX)
AS
IF EXISTS (SELECT * FROM USERTOKENS WHERE USERTOKENS.Token = @AccessToken)
BEGIN;
	UPDATE PROJECTS SET
		PROJECTS.Name = @Name,
		PROJECTS.Description = @Description,
		PROJECTS.ProjectIconUrl = @IconUrl,
		PROJECTS.ProjectBackgroundImageUrl = @BackGroundImageUrl,
		PROJECTS.ProjectStatusGuid = (SELECT PROJECTSTATUS.Guid FROM PROJECTSTATUS WHERE PROJECTSTATUS.Name = @ProjectStatus),
		PROJECTS.DateModified = (SELECT GETDATE())
	WHERE PROJECTS.Guid = @Guid;
END;
ELSE
BEGIN;
	THROW 51000, 'Invalid acces stoken', 1;
END;
GO

--------------------------------------[ Delete Project ]--------------------------------------
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'DeleteProject' AND ROUTINE_TYPE = 'PROCEDURE')
	DROP PROCEDURE DeleteProject;
GO

CREATE PROCEDURE DeleteProject
	@Guid NVARCHAR(40),
	@AccessToken NVARCHAR(MAX)
AS
IF EXISTS (SELECT * FROM USERTOKENS WHERE USERTOKENS.Token = @AccessToken)
BEGIN;
	-- CHECK FOR TICKETS
	IF EXISTS (SELECT * FROM TICKETS WHERE TICKETS.ProjectGuid = @Guid)
	BEGIN
		-- TICKET TEMP TABLE GUID
		DECLARE @TICKETTABLE TABLE (
			Number INT IDENTITY(1, 1),
			Guid NVARCHAR(40)
		);

		INSERT INTO @TICKETTABLE
		SELECT TICKETS.Guid
		FROM TICKETS
		WHERE 
		TICKETS.ProjectGuid = @Guid;

		DECLARE @TICKETCOUNTER INT = 1;
		DECLARE @MAXTICKET INT = (SELECT COUNT(*) FROM @TICKETTABLE);

		WHILE( @TICKETCOUNTER <= @MAXTICKET)
		BEGIN
			DECLARE @CURRENTTICKETGUID NVARCHAR(40) = (SELECT Guid FROM @TICKETTABLE WHERE Number = @TICKETCOUNTER);

			BEGIN TRY
				-- DELETE COMMENTS FOR THE SELECTED TICKET
				EXEC DeleteTicket @CURRENTTICKETGUID, @AccessToken;
				SET @TICKETCOUNTER = @TICKETCOUNTER + 1;
			END TRY
			BEGIN CATCH
			-- Do nothing
			END CATCH
		END;
	END;

	IF EXISTS (SELECT * FROM PROJECTS WHERE PROJECTS.Guid = @Guid)
	BEGIN
		BEGIN TRY
			DELETE FROM PROJECTS WHERE PROJECTS.Guid = @Guid;
		END TRY
		BEGIN CATCH
			DECLARE @ERR NVARCHAR(MAX) = (SELECT ERROR_MESSAGE());
			THROW 51000, @ERR, 1;
		END CATCH
	END;
	SELECT 1 AS Result;
END
ELSE
	THROW 51000, 'Invalid access token', 1;
GO

--------------------------------------[END Delete Project ]--------------------------------------

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'GetHighestCountingTicketProject')
	DROP PROCEDURE GetHighestCountingTicketProject;
GO

CREATE PROCEDURE GetHighestCountingTicketProject
	@accesstoken NVARCHAR(MAX)
AS
BEGIN
	DECLARE @UserGuid NVARCHAR(40);
	DECLARE @Count INT;
	DECLARE @ProjectGuid NVARCHAR(40);

	IF NOT EXISTS (SELECT * FROM USERTOKENS WHERE USERTOKENS.Token = @accesstoken)
		THROW 51000, 'Invalid access token.', 1;
	ELSE
	BEGIN
		-- Get User guid
		SET @UserGuid = (SELECT USERTOKENS.UserGuid FROM USERTOKENS WHERE USERTOKENS.Token = @accesstoken);

		SELECT TOP 1 @ProjectGuid = TICKETS.ProjectGuid, @Count = COUNT(*)
		FROM TICKETS
		WHERE
			TICKETS.UserGuid = @UserGuid
		GROUP BY TICKETS.ProjectGuid
		ORDER BY COUNT(*) DESC;
	END;

	SELECT *, @Count AS TicketCount FROM GetProjectDetail(@ProjectGuid, @accesstoken);
END;

GO

--------------------------------------[ Add Ticket ]--------------------------------------
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'AddTicket' AND ROUTINE_TYPE = 'PROCEDURE')
	DROP PROCEDURE AddTicket;
GO

CREATE PROCEDURE AddTicket
	@Guid NVARCHAR(40),
	@ProjectGuid NVARCHAR(40),
	@Name NVARCHAR(MAX),
	@Description NVARCHAR(MAX),
	@Severity NVARCHAR(255),
	@Type NVARCHAR(255),
	@AccessToken NVARCHAR(MAX)
AS
BEGIN;
	IF EXISTS (SELECT * FROM USERTOKENS WHERE USERTOKENS.Token = @AccessToken)
	BEGIN;
		INSERT INTO TICKETS(
			Guid,
			ProjectGuid,
			UserGuid,
			Name,
			Description,	
			DateCreated,
			DateModified,
			SeverityGuid,
			TypeGuid
		)
		VALUES(
			@Guid,
			@ProjectGuid,
			(SELECT USERTOKENS.UserGuid FROM USERTOKENS WHERE USERTOKENS.Token = @AccessToken),
			@Name,
			@Description,
			(SELECT GETDATE()),
			(SELECT GETDATE()),
			(SELECT TICKETSEVERITIES.Guid FROM TICKETSEVERITIES WHERE TICKETSEVERITIES.Title = @Severity),
			(SELECT TICKETTYPE.Guid FROM TICKETTYPE WHERE TICKETTYPE.Name = @Type)
		)
	END;
END;
GO

--------------------------------------[ Edit Ticket ]--------------------------------------
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'EditTicket' AND ROUTINE_TYPE = 'PROCEDURE')
	DROP PROCEDURE EditTicket;
GO

CREATE PROCEDURE EditTicket
	@Guid NVARCHAR(40),
	@Name NVARCHAR(MAX),
	@Description NVARCHAR(MAX),
	@Severity NVARCHAR(MAX),
	@Type NVARCHAR(MAX),
	@AccessToken NVARCHAR(MAX)
AS
IF EXISTS (SELECT * FROM USERTOKENS WHERE USERTOKENS.Token = @AccessToken)
BEGIN;
	UPDATE TICKETS SET
		TICKETS.Name = @Name,
		TICKETS.Description = @Description,
		TICKETS.SeverityGuid = (SELECT TICKETSEVERITIES.Guid FROM TICKETSEVERITIES WHERE TICKETSEVERITIES.Title = @Severity),
		TICKETS.DateModified = (SELECT GETDATE()),
		TICKETS.TypeGuid = (SELECT TICKETTYPE.Guid FROM TICKETTYPE WHERE TICKETTYPE.Name = @Type)
	WHERE TICKETS.Guid = @Guid;
END;
ELSE
	THROW 51000, 'Invalid access token', 1;
GO

--------------------------------------[END Edit Ticket ]--------------------------------------

--------------------------------------[ Delete Ticket ( AND COMMENTS ) ]--------------------------------------
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'DeleteTicket' AND ROUTINE_TYPE = 'PROCEDURE')
	DROP PROCEDURE DeleteTicket;
GO

CREATE PROCEDURE DeleteTicket
	@Guid NVARCHAR(40),
	@AccessToken NVARCHAR(40)
AS
IF EXISTS (SELECT * FROM USERTOKENS WHERE USERTOKENS.Token = @AccessToken)
BEGIN;
	-- Delete comments
	IF EXISTS (SELECT * FROM TICKETCOMMENTS WHERE TICKETCOMMENTS.TicketGuid = @Guid)
		DELETE FROM TICKETCOMMENTS WHERE TICKETCOMMENTS.TicketGuid = @Guid;
	
	-- Delete ticket
	IF EXISTS (SELECT * FROM TICKETS WHERE TICKETS.Guid = @Guid)
		DELETE FROM TICKETS WHERE TICKETS.Guid = @Guid;
END;
GO

--------------------------------------[ Add Comment ]--------------------------------------
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'AddComment' AND ROUTINE_TYPE = 'PROCEDURE')
	DROP PROCEDURE AddComment;
GO
CREATE PROCEDURE AddComment
	@TicketGuid NVARCHAR(40),
	@CommentText NVARCHAR(MAX),
	@AccessToken NVARCHAR(MAX)
AS
BEGIN;
	IF EXISTS (SELECT * FROM USERTOKENS WHERE USERTOKENS.Token = @AccessToken)
	BEGIN;
		BEGIN TRY
			INSERT INTO TICKETCOMMENTS(
				Guid,
				TicketGuid,
				UserGuid,
				CommentText,
				DateCreated
			)
			VALUES(
				(SELECT NEWID()),
				@TicketGuid,
				(SELECT USERTOKENS.UserGuid FROM USERTOKENS WHERE USERTOKENS.Token = @AccessToken),
				@CommentText,
				GETDATE()
			)
			SELECT 1 AS Result;
		END TRY
		BEGIN CATCH
			SELECT -1 AS Result;
		END CATCH;
	END;
	ELSE
	BEGIN;
		THROW 51000, 'Invalid access token', 1;
	END;
END;
GO

--------------------------------------[ Delete ticket comment ]--------------------------------------
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'DeleteComment' AND ROUTINE_TYPE = 'PROCEDURE')
	DROP PROCEDURE DeleteComment;
GO

CREATE PROCEDURE DeleteComment
	@Guid NVARCHAR(40),
	@accesstoken NVARCHAR(MAX)
AS
BEGIN
	IF EXISTS (SELECT * FROM USERTOKENS WHERE USERTOKENS.Token = @accesstoken)
	BEGIN
		DECLARE @UserGuid NVARCHAR(40) = (SELECT USERTOKENS.UserGuid FROM USERTOKENS WHERE USERTOKENS.Token = @accesstoken);
		BEGIN TRY
			DELETE FROM TICKETCOMMENTS WHERE TICKETCOMMENTS.Guid = @Guid AND TICKETCOMMENTS.UserGuid = @UserGuid;
			SELECT 1 AS Result;
		END TRY
		BEGIN CATCH
			THROW 51000, 'Encountered a problem and cannot continue', 1;
		END CATCH;
	END;

END
GO

--------------------------------------[ List ticket comment ]--------------------------------------

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'ListComments' AND ROUTINE_TYPE = 'PROCEDURE')
	DROP PROCEDURE ListComments;
GO

CREATE PROCEDURE ListComments
	@Guid NVARCHAR(40),
	@accesstoken NVARCHAR(MAX)
AS
BEGIN
	DECLARE @UserGuid NVARCHAR(40);
	IF EXISTS (SELECT * FROM USERTOKENS WHERE USERTOKENS.Token = @accesstoken)
		SET @UserGuid = (SELECT USERTOKENS.UserGuid FROM USERTOKENS WHERE USERTOKENS.Token = @accesstoken);
	ELSE
		THROW 51000, 'Invalid access token', 1;

	SELECT
		TICKETCOMMENTS.Guid,
		TICKETS.Name AS TicketName,
		TICKETCOMMENTS.CommentText,
		TICKETCOMMENTS.DateCreated
	FROM
		TICKETCOMMENTS
	INNER JOIN TICKETS ON TICKETCOMMENTS.TicketGuid = TICKETS.Guid
	WHERE
		TICKETCOMMENTS.TicketGuid = @Guid;
	RETURN;
END;

GO
--------------------------------------[ Add User ]--------------------------------------
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'AddUserMinimal')
BEGIN;
	DROP PROCEDURE AddUserMinimal;
END;
GO

CREATE PROCEDURE AddUserMinimal
	@Guid NVARCHAR(40),
	@Username NVARCHAR(255),
	@Password NVARCHAR(255),
	@Email NVARCHAR(255)
AS
BEGIN;
	INSERT INTO USERS(
		Guid,
		Username,
		Password,
		Email
	)
	VALUES(
		@Guid,
		@Username,
		@Password,
		@Email
	)
END;
GO

--[ FUNCTIONS ] -----------------------------------------------------------------------------
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'CheckUserCredential')
BEGIN;
	DROP PROCEDURE CheckUserCredential;
END;
GO

--------------------------------------[ "CHECK" USER CREDENTIAL ]--------------------------------------
CREATE PROCEDURE CheckUserCredential
	@accesstoken NVARCHAR(MAX)
AS
BEGIN
	IF EXISTS (SELECT * FROM USERTOKENS WHERE USERTOKENS.Token = @accesstoken)
		SELECT 1 AS Result;
	ELSE
		SELECT -1 AS Result;
END;

GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'SetUserCredential')
BEGIN;
	DROP PROCEDURE SetUserCredential;
END;

GO

CREATE PROCEDURE SetUserCredential
	@Username NVARCHAR(MAX),
	@Password NVARCHAR(MAX)
AS
BEGIN
	IF EXISTS (SELECT * FROM USERS WHERE USERS.Username = @Username AND USERS.Password = @Password)
	BEGIN
		DECLARE @UserGuid NVARCHAR(40);
		DECLARE @Token NVARCHAR(MAX);
		DECLARE @DateAccess DATETIME;

		SET @Token = (SELECT NEWID());
		SET @UserGuid = (SELECT USERS.Guid FROM USERS WHERE USERS.Username = @Username AND USERS.Password = @Password);
		SET @DateAccess = GETDATE();
		INSERT INTO USERTOKENS(
			UserGuid,
			Token,
			DateCreated,
			LastAccess
		)
		VALUES(
			@UserGuid,
			@Token,
			@DateAccess,
			@DateAccess
		);
	END;
	ELSE
		THROW 51000, 'Invalid credential', 1;

	SELECT @Token Result;
END;

GO

--[Logout user]------------------------------------------------------------------------------

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'RemoveUserToken')
	DROP PROCEDURE RemoveUserToken;
GO

CREATE PROCEDURE RemoveUserToken
	@accesstoken NVARCHAR(MAX)
AS
BEGIN
	BEGIN TRY
		DELETE FROM USERTOKENS WHERE USERTOKENS.Token = @accesstoken;
		SELECT @@ROWCOUNT AS Result;
	END TRY
	BEGIN CATCH
		THROW 51000, 'Fail to remove token', 1;
	END CATCH
END;

GO

--[Logout all user tokens]------------------------------------------------------------------------------
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'RemoveAllUserToken')
	DROP PROCEDURE RemoveAllUserToken;
GO

CREATE PROCEDURE RemoveAllUserToken
	@accesstoken NVARCHAR(MAX)
AS
BEGIN
	DECLARE @UserGuid NVARCHAR(40);
	SET @UserGuid = (SELECT USERTOKENS.UserGuid FROM USERTOKENS WHERE USERTOKENS.Token = @accesstoken);
	
	BEGIN TRY
		DELETE FROM USERTOKENS WHERE USERTOKENS.UserGuid = @UserGuid;
		SELECT @@ROWCOUNT AS Result;
	END TRY
	BEGIN CATCH
		SELECT -1 AS Result;
	END CATCH
END;

GO

--------------------------------------[ Request Password Reset ]--------------------------------------

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'RequestPasswordReset')
	DROP PROCEDURE RequestPasswordReset;
GO

CREATE PROCEDURE RequestPasswordReset
	@Username NVARCHAR(255),
	@Email NVARCHAR(255)
AS
BEGIN
	IF EXISTS (SELECT * FROM USERS WHERE USERS.Username = @Username AND USERS.Email = @Email)
	BEGIN
		DECLARE @ResetToken NVARCHAR(MAX);
		SET @ResetToken = NEWID();

		INSERT INTO USERREQUESTS(
			UserGuid,
			RequestToken,
			LastRequestDate
		)
		VALUES(
			(SELECT USERS.Guid FROM USERS WHERE USERS.Username = @Username AND USERS.Email = @Email),
			@ResetToken,
			GETDATE()
		);

		SELECT @ResetToken AS Result;
	END;
	ELSE
		THROW 51000, 'Invalid request. Cannot continue', 1;
END;

GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'ResetUserPassword')
	DROP PROCEDURE ResetUserPassword;

GO

CREATE PROCEDURE ResetUserPassword
	@ResetToken NVARCHAR(MAX),
	@NewPassword NVARCHAR(255)
AS
BEGIN
	IF EXISTS (SELECT * FROM USERREQUESTS WHERE USERREQUESTS.RequestToken = @ResetToken)
	BEGIN
		DECLARE @UserGuid NVARCHAR(40);
		SET @UserGuid = (SELECT USERREQUESTS.UserGuid FROM USERREQUESTS WHERE USERREQUESTS.RequestToken = @ResetToken);

		UPDATE USERS
		SET
			USERS.Password = @NewPassword
		WHERE USERS.Guid = @UserGuid;
	END;
END;
GO

--[ENDREGION USER]---------------------------------------------------------------------------


--------------------------------------[ PROJECTS LIST (SHORT) ]--------------------------------------
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'GetAllProjectListShort' AND ROUTINE_TYPE = 'FUNCTION')
	DROP FUNCTION GetAllProjectListShort;
GO

CREATE FUNCTION GetAllProjectListShort(
	@accesstoken NVARCHAR(255)
)
RETURNS TABLE
AS
RETURN(
	SELECT
		PROJECTS.Name,
		PROJECTS.Guid,
		PROJECTS.ProjectIconUrl IconUrl,
		PROJECTS.ProjectBackgroundImageUrl BackgroundImageUrl
	FROM PROJECTS WHERE
		PROJECTS.UserGuid = (SELECT USERTOKENS.UserGuid FROM USERTOKENS WHERE USERTOKENS.Token = @accesstoken)
)

GO

--------------------------------------[ PROJECTS LIST (SHORT) ]--------------------------------------
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'GetProjectDetail' AND ROUTINE_TYPE = 'FUNCTION')
BEGIN
	DROP FUNCTION GetProjectDetail;
END
GO

CREATE FUNCTION GetProjectDetail(
	@Guid NVARCHAR(40),
	@AccessToken NVARCHAR(MAX)
)
RETURNS TABLE
AS
RETURN(
	SELECT
		PROJECTS.Guid,
		PROJECTS.Name,
		PROJECTS.Description,
		PROJECTS.ProjectIconUrl IconUrl,
		PROJECTS.ProjectBackgroundImageUrl BackgroundImageUrl,
		PROJECTS.DateCreated,
		PROJECTSTATUS.Name ProjectStatus
	FROM PROJECTS
	INNER JOIN PROJECTSTATUS ON PROJECTSTATUS.Guid = PROJECTS.ProjectStatusGuid
	WHERE PROJECTS.Guid = @Guid AND PROJECTS.UserGuid = (SELECT USERTOKENS.UserGuid FROM USERTOKENS WHERE USERTOKENS.Token = @AccessToken)

)
GO

--------------------------------------[ ALL TICKET LIST ]--------------------------------------
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'FUNCTION' AND ROUTINE_NAME = 'GetAllTicketsShort')
	DROP FUNCTION GetAllTicketsShort;
GO

CREATE FUNCTION GetAllTicketsShort(
	@accesstoken NVARCHAR(MAX)
)
RETURNS TABLE
AS
RETURN(
	SELECT
		TICKETS.Guid,
		TICKETS.Name,
		TICKETS.DateCreated
	FROM TICKETS
	WHERE TICKETS.UserGuid = (SELECT USERTOKENS.UserGuid FROM USERTOKENS WHERE USERTOKENS.Token = @accesstoken)
)
GO

--------------------------------------[ ALL TICKET LIST OF A PROJECT ]--------------------------------------

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'FUNCITON' AND ROUTINE_NAME = 'GetAllProjectTickets')
	DROP FUNCTION GetAllProjectTickets;
GO

CREATE FUNCTION GetAllProjectTickets(
	@Guid NVARCHAR(40),
	@AccessToken NVARCHAR(MAX)
)
RETURNS TABLE
AS
RETURN(
	SELECT
		TICKETS.Guid,
		TICKETS.Name,
		TICKETS.Description,
		TICKETS.DateCreated,
		TICKETS.DateSolved,
		TICKETSEVERITIES.Title Severity,
		TICKETTYPE.Name Type
	FROM TICKETS
	INNER JOIN TICKETSEVERITIES ON TICKETSEVERITIES.Guid = TICKETS.SeverityGuid
	INNER JOIN TICKETTYPE ON TICKETTYPE.Guid = TICKETS.TypeGuid
	WHERE
		TICKETS.ProjectGuid = @Guid AND
		TICKETS.UserGuid = (SELECT USERTOKENS.UserGuid FROM USERTOKENS WHERE USERTOKENS.Token = @AccessToken)
)
GO

--------------------------------------[ ALL TICKET LIST OF A PROJECT SHORT ]--------------------------------------
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'FUNCTION' AND ROUTINE_NAME = 'GetAllProjectTicketsShort')
	DROP FUNCTION GetAllProjectTicketsShort;
GO

CREATE FUNCTION GetAllProjectTicketsShort(
	@Guid NVARCHAR(40),
	@AccessToken NVARCHAR(MAX)
)
RETURNS TABLE
AS
RETURN(
	SELECT
		TICKETS.Guid,
		TICKETS.Name,
		TICKETS.DateCreated
	FROM TICKETS
	WHERE
		TICKETS.ProjectGuid = @Guid AND
		TICKETS.UserGuid = (SELECT USERTOKENS.UserGuid FROM USERTOKENS WHERE USERTOKENS.Token = @AccessToken)
)

GO

--------------------------------------[ ALL SOLVED TICKETS ]--------------------------------------
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'FUNCTION' AND ROUTINE_NAME = 'GetSolvedTicketsShort')
	DROP FUNCTION GetSolvedTicketsShort
GO

CREATE FUNCTION GetSolvedTicketsShort(
	@AccessToken NVARCHAR(40)
)
RETURNS TABLE
AS
RETURN(
	SELECT
		TICKETS.Guid,
		TICKETS.Name,
		TICKETS.DateSolved
	FROM TICKETS
	WHERE 
		TICKETS.DateSolved IS NOT NULL AND
		TICKETS.UserGuid = (SELECT USERTOKENS.UserGuid FROM USERTOKENS WHERE USERTOKENS.Token = @AccessToken)
)
GO

--------------------------------------[ ALL UNSOLVED TICKETS ]--------------------------------------
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'FUNCTION' AND ROUTINE_NAME = 'GetUnsolvedTicketsShort')
	DROP FUNCTION GetUnsolvedTicketsShort
GO

CREATE FUNCTION GetUnsolvedTicketsShort(
	@AccessToken NVARCHAR(MAX)
)
RETURNS TABLE
AS
RETURN(
	SELECT
		TICKETS.Guid,
		TICKETS.Name,
		TICKETS.DateCreated
	FROM TICKETS
	WHERE
		TICKETS.DateSolved = NULL AND
		TICKETS.UserGuid = (SELECT USERTOKENS.UserGuid FROM USERTOKENS WHERE USERTOKENS.Token = @AccessToken)
)
GO
--------------------------------------[ GET OLDEST UNSOLVED TICKET ]--------------------------------------

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'FUNCTION' AND ROUTINE_NAME = 'GetLongestUnsolvedTicket')
	DROP FUNCTION GetLongestUnsolvedTicket;
GO

CREATE FUNCTION GetLongestUnsolvedTicket(
	@AccessToken NVARCHAR(40)
)
RETURNS TABLE
AS
RETURN(
	SELECT
		TOP 1
		TICKETS.Guid,
		TICKETS.Name,
		TICKETS.Description,
		PROJECTS.Name Project,
		TICKETS.DateCreated,
		TICKETSEVERITIES.Title Severity,
		TICKETTYPE.Name Type
	FROM TICKETS
	INNER JOIN PROJECTS ON PROJECTS.Guid = TICKETS.ProjectGuid
	INNER JOIN TICKETSEVERITIES ON TICKETSEVERITIES.Guid = TICKETS.SeverityGuid
	INNER JOIN TICKETTYPE ON TICKETTYPE.Guid = TICKETS.TypeGuid
	WHERE
		TICKETS.DateSolved IS NULL AND
		TICKETS.UserGuid = (SELECT USERTOKENS.UserGuid FROM USERTOKENS WHERE USERTOKENS.Token = @AccessToken)
	ORDER BY TICKETS.DateCreated ASC
)
GO

--------------------------------------[ GET OLDEST UNSOLVED TICKETS ]--------------------------------------

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'FUNCTION' AND ROUTINE_NAME = 'GetLongestUnsolvedTickets')
	DROP FUNCTION GetLongestUnsolvedTickets;
GO

CREATE FUNCTION GetLongestUnsolvedTickets(
	@MaxItem INTEGER,
	@AccessToken NVARCHAR(40)
)
RETURNS TABLE
AS
RETURN(
	SELECT
		TOP (@MaxItem)
		TICKETS.Guid,
		TICKETS.Name,
		TICKETS.Description,
		PROJECTS.Name Project,
		TICKETS.DateCreated,
		TICKETSEVERITIES.Title Severity,
		TICKETTYPE.Name Type
	FROM TICKETS
	INNER JOIN PROJECTS ON PROJECTS.Guid = TICKETS.ProjectGuid
	INNER JOIN TICKETSEVERITIES ON TICKETSEVERITIES.Guid = TICKETS.SeverityGuid
	INNER JOIN TICKETTYPE ON TICKETTYPE.Guid = TICKETS.TypeGuid
	WHERE
		TICKETS.DateSolved IS NULL AND
		TICKETS.UserGuid = (SELECT USERTOKENS.UserGuid FROM USERTOKENS WHERE USERTOKENS.Token = @AccessToken)
	ORDER BY TICKETS.DateCreated ASC
)
GO

--------------------------------------[ ALL TICKET LIST WITH HIGH SEVERITY ]--------------------------------------
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'FUNCTION' AND ROUTINE_NAME = 'GetHighestSeverityTicketsShort')
	DROP FUNCTION GetHighestSeverityTicketsShort;
GO

CREATE FUNCTION GetHighestSeverityTicketsShort(
	@accesstoken NVARCHAR(MAX)
)
RETURNS TABLE
AS
RETURN(
	SELECT
		TICKETS.Guid,
		TICKETS.Name,
		TICKETS.DateCreated,
		TICKETSEVERITIES.Title
	FROM TICKETS
	INNER JOIN TICKETSEVERITIES ON TICKETSEVERITIES.Guid = TICKETS.SeverityGuid
	WHERE
		TICKETS.SeverityGuid = (SELECT TICKETSEVERITIES.Guid FROM TICKETSEVERITIES WHERE TICKETSEVERITIES.SeverityIndex = (SELECT MAX(TICKETSEVERITIES.SeverityIndex) FROM TICKETSEVERITIES)) AND
		TICKETS.UserGuid = (SELECT USERTOKENS.UserGuid FROM USERTOKENS WHERE USERTOKENS.Token = @accesstoken)
)
GO

--------------------------------------[ TICKET DETAIL ]--------------------------------------
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'FUNCTION' AND ROUTINE_NAME = 'GetTicketDetail')
	DROP FUNCTION GetTicketDetail
GO

CREATE FUNCTION GetTicketDetail(
	@accesstoken NVARCHAR(MAX),
	@Guid NVARCHAR(40)
)
RETURNS TABLE
AS
RETURN(
	SELECT
		TICKETS.Guid,
		PROJECTS.Name Project,
		TICKETS.Name,
		TICKETS.Description,
		TICKETS.DateCreated,
		TICKETS.DateModified,
		TICKETSEVERITIES.Title Severity,
		TICKETTYPE.Name Type
	FROM TICKETS
		INNER JOIN PROJECTS ON PROJECTS.Guid = TICKETS.ProjectGuid
		INNER JOIN TICKETSEVERITIES ON TICKETSEVERITIES.Guid = TICKETS.SeverityGuid
		INNER JOIN TICKETTYPE ON TICKETTYPE.Guid = TICKETS.TypeGuid
	WHERE
		TICKETS.Guid = @Guid AND TICKETS.UserGuid = (SELECT USERTOKENS.UserGuid FROM USERTOKENS WHERE USERTOKENS.Token = @accesstoken)
)
GO

--------------------------------------[ GET ALL DETAILED TICKET SEVERITIES ]--------------------------------------

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'FUNCTION' AND ROUTINE_NAME = 'GetAllTicketSeverity')
	DROP FUNCTION GetAllTicketSeverity
GO

CREATE FUNCTION GetAllTicketSeverity(
	@AccessToken NVARCHAR(MAX)
)
RETURNS @Result TABLE (
	Guid NVARCHAR(40),
	Title NVARCHAR(MAX),
	Description NVARCHAR(MAX),
	SeverityIndex INT
)
AS
BEGIN
	IF EXISTS (SELECT * FROM USERTOKENS WHERE USERTOKENS.Token = @AccessToken)
	BEGIN
		INSERT INTO @Result(
			Guid,
			Title,
			Description,
			SeverityIndex
		)
		SELECT 
			TICKETSEVERITIES.Guid,
			TICKETSEVERITIES.Title,
			TICKETSEVERITIES.Description,
			TICKETSEVERITIES.SeverityIndex
		FROM TICKETSEVERITIES
	END
	RETURN
END
GO

--------------------------------------[ GET ALL DETAILED TICKET TYPE DETAILS]--------------------------------------

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'FUNCTION' AND ROUTINE_NAME = 'GetAllTicketTypes')
	DROP FUNCTION GetAllTicketTypes
GO

CREATE FUNCTION GetAllTicketTypes(
	@AccessToken NVARCHAR(MAX)
)
RETURNS @Result TABLE (
	Guid NVARCHAR(40),
	Title NVARCHAR(MAX),
	Description NVARCHAR(MAX)
)
AS
BEGIN
	IF EXISTS (SELECT * FROM USERTOKENS WHERE USERTOKENS.Token = @AccessToken)
	BEGIN
		INSERT INTO @Result(
			Guid,
			Title,
			Description
		)
		SELECT 
			TICKETTYPE.Guid,
			TICKETTYPE.Name Title,
			TICKETTYPE.Description
		FROM TICKETTYPE
	END
	RETURN
END
GO

--------------------------------------[ MARK TICKET AS SOLVED ]--------------------------------------
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'MarkTicketSolved')
	DROP PROCEDURE MarkTicketSolved;
GO

CREATE PROCEDURE MarkTicketSolved
	@Guid NVARCHAR(40),
	@accesstoken NVARCHAR(MAX)
AS
BEGIN
	IF NOT EXISTS (SELECT USERTOKENS.Token FROM USERTOKENS WHERE USERTOKENS.Token = @accesstoken)
		THROW 51000, 'Invalid access token', 1;

	DECLARE @UserGuid NVARCHAR(40) = (SELECT USERTOKENS.UserGuid FROM USERTOKENS WHERE USERTOKENS.Token = @accesstoken);
	DECLARE @ProjectGuid NVARCHAR(40);

	DECLARE @TicketUserGuid NVARCHAR(40) = (SELECT TICKETS.UserGuid FROM TICKETS WHERE TICKETS.Guid = @Guid);
	
	-- Secutity checkup. Make sure that the request is valid
	
	-- Check if the requested user is the one that created the ticket
	IF (@UserGuid != @TicketUserGuid)
		THROW 51000, 'Ticket marking cannot be completed because ticket was not found', 1;

	-- Check if the ticket already been solved previously
	IF (SELECT TICKETS.DateSolved FROM TICKETS WHERE TICKETS.Guid = @Guid) IS NOT NULL
		THROW 51000, 'Ticket has been marked as solved', 1;
	BEGIN TRY
		UPDATE TICKETS
		SET
			TICKETS.DateSolved = GETDATE()
		WHERE TICKETS.Guid = @Guid AND TICKETS.UserGuid = @UserGuid
		(SELECT 1 AS Result);
	END TRY
	BEGIN CATCH
		THROW 51000, 'Encountered a problem and cannot continue', 1;
	END CATCH
END;
GO

--------------------------------------[ UN-MARK TICKET AS SOLVED ]--------------------------------------
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'UnmarkTicketSolved')
	DROP PROCEDURE UnmarkTicketSolved;
GO

CREATE PROCEDURE UnmarkTicketSolved
	@Guid NVARCHAR(40),
	@accesstoken NVARCHAR(MAX)
AS
BEGIN
	IF NOT EXISTS (SELECT * FROM USERTOKENS WHERE USERTOKENS.Token = @accesstoken)
		THROW 51000, 'Invalid access token', 1;

	DECLARE @UserGuid NVARCHAR(40) =(SELECT USERTOKENS.UserGuid FROM USERTOKENS WHERE USERTOKENS.Token = @accesstoken);

	DECLARE @TicketUserGuid NVARCHAR(40) = (SELECT TICKETS.UserGuid FROM TICKETS WHERE TICKETS.Guid = @Guid);

	IF (@UserGuid != @TicketUserGuid)
		THROW 51000, 'Encoutered a problem and cannot continue', 1;
	ELSE
	BEGIN
		BEGIN TRY
			UPDATE TICKETS
			SET
				TICKETS.DateSolved = NULL
			WHERE
				TICKETS.Guid = @Guid AND
				TICKETS.UserGuid = @UserGuid;
		END TRY
		BEGIN CATCH
			THROW 51000, 'Encountered a problem and cannot continue', 1;
		END CATCH;
	END;
END;

GO

--[ DEMO DATA ]-------------------------------------------------------------------------
--------------------------------------[ REGISTER GUEST USER ]--------------------------------------

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = 'CreateGuestDemo')
	DROP PROCEDURE CreateGuestDemo;
GO

CREATE PROCEDURE CreateGuestDemo
AS
BEGIN
	DECLARE @accesstoken NVARCHAR(40);
	DECLARE @Guid NVARCHAR(40);
	DECLARE @Username NVARCHAR(255);
	DECLARE @RandomInt NVARCHAR(MAX);
	DECLARE @ProjectStatus NVARCHAR(MAX);

	SET @Guid = NEWID();

	SET @RandomInt = (SELECT CAST(CRYPT_GEN_RANDOM(8) AS bigint));

	SET @Username = (SELECT CONCAT('GUEST_', @RandomInt));

	SET @ProjectStatus = (SELECT TOP 1 PROJECTSTATUS.Name FROM PROJECTSTATUS);

	-- Register user
	EXEC AddUserMinimal @Guid, @Username, @Guid, 'guest@guest.com';

	-- Log in guest
	BEGIN TRY
		EXEC SetUserCredential @Username, @Guid;
		-- Find the guest access token
		SET @accesstoken = (SELECT TOP 1 USERTOKENS.Token FROM USERTOKENS WHERE USERTOKENS.UserGuid = @Guid);
	END TRY
	BEGIN CATCH
		THROW 51000, 'Encountered a problem an cannot continue. Could not save Guest credential', 1;
	END CATCH

	-- Add project for demo user
	BEGIN TRY
		DECLARE @ProjectGuid NVARCHAR(40) = (SELECT NEWID());
		EXEC AddProject 
			@ProjectGuid,
			'Example Project',
			'An example project description created for you so that you can understand how about this project',
			'N/A',
			'N/A',
			@ProjectStatus,
			@accesstoken;
		END TRY
	BEGIN CATCH
		THROW 51000, 'Could not add project to guest user.', 1;
	END CATCH

	-- Add Ticket to project
	EXEC AddTicket
		NEWID,
		@ProjectGuid,
		'Sample Ticket',
		'Sample description',
		'',
		'',
		@accesstoken;
	
	-- Return access token
	SELECT @accesstoken as Result;
END;

GO