GO
DROP DATABASE [TodoListAppDB]

GO
CREATE DATABASE [TodoListAppDB]
GO

USE [TodoListAppDB]
GO



/****** Table Account ******/
/*CREATE TABLE [dbo].[Accounts](
	[AccountID] [int] IDENTITY(1,1) PRIMARY KEY,
    [Firstname] [nvarchar](100) NOT NULL,
	[Lastname] [nvarchar](100) NOT NULL,
	[Username] [nvarchar](100) NOT NULL,
    [Password] [varchar](200) NOT NULL,
    [PhoneNumber] [varchar](20) NOT NULL,
    [Email] [varchar](100) NOT NULL,
	[Avatar] [varchar](max) NULL,
    [Status] [varchar](20) NOT NULL,
)*/

/****** Table Labels ******/
CREATE TABLE  [dbo].[Labels](
	[LabelID] int IDENTITY(1,1) PRIMARY KEY,
	[LabelName] NVARCHAR(100) NOT NULL,
	[CreatedDate] DATETIME DEFAULT GETDATE(),
	[StartDate] DATETIME,
	[DueDate] DATETIME,
	[Status] NVARCHAR(200) NOT NULL,
)

/****** Table Tasks ******/
CREATE TABLE [dbo].[TodoTasks](
	[TodoTaskID] int IDENTITY(1,1) PRIMARY KEY,
    [Title] NVARCHAR(200) NOT NULL,
	[Description] NVARCHAR(MAX),
	[IsCompleted] BIT DEFAULT 0,
	[CreatedDate] DATETIME DEFAULT GETDATE(),
	[DueDate] DATETIME,
	[UpdatedDate] DATETIME,
	[LabelID] INT,
	CONSTRAINT FK_TodoTasks_Labels FOREIGN KEY ([LabelID])
        REFERENCES [dbo].[Labels]([LabelID])
        ON DELETE SET NULL
)


/****** Table SubTasks ******/
CREATE TABLE  [dbo].[SubTasks](
	[SubTaskID] INT PRIMARY KEY IDENTITY,
	[TodoTaskID] INT NOT NULL,
	[Title] NVARCHAR(200) NOT NULL,
	[IsCompleted] BIT DEFAULT 0
	CONSTRAINT FK_SubTasks_TodoTasks FOREIGN KEY ([TodoTaskID])
        REFERENCES [dbo].[TodoTasks]([TodoTaskID])
        ON DELETE CASCADE
)



/****** Table DailyTasks ******/
CREATE TABLE [dbo].[DailyTasks](
	[DailyTasksID] int IDENTITY(1,1) PRIMARY KEY,
    [Title] NVARCHAR(200) NOT NULL,
	[Description] NVARCHAR(MAX),
	[IsCompleted] BIT DEFAULT 0,
	[CreatedDate] DATETIME DEFAULT GETDATE(),
	[StartDate] DATETIME,
	[DueDate] DATETIME,
	[UpdatedDate] DATETIME,
)