USE [Master]

GO
DROP DATABASE [TodoListAppDB]

GO
CREATE DATABASE [TodoListAppDB]

GO
USE [TodoListAppDB]

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
	[Description] NVARCHAR(MAX),
	[CreatedDate] DATETIME DEFAULT GETDATE()
)
/*EXEC sp_help [Labels];*/
INSERT INTO [dbo].[Labels] ([LabelName], [Description]) VALUES
('Project A', 'Tasks related to Project A'),
('Project B', 'Tasks related to Project B'),
('Urgent', 'High priority tasks');

/****** Table Tasks ******/
CREATE TABLE [dbo].[TodoTasks](
	[TodoTaskID] int IDENTITY(1,1) PRIMARY KEY,
    [Title] NVARCHAR(200) NOT NULL,
	[Description] NVARCHAR(MAX),
	[CreatedDate] DATETIME DEFAULT GETDATE(),
	[StartDate] DATETIME,
	[DueDate] DATETIME,
	[Status] NVARCHAR(50) NOT NULL,
	[UpdatedDate] DATETIME,
	[LabelID] INT,
	CONSTRAINT FK_TodoTasks_Labels FOREIGN KEY ([LabelID])
        REFERENCES [dbo].[Labels]([LabelID])
        ON DELETE SET NULL
)
/*DBCC CHECKIDENT ([TodoTasks], RESEED, 0);*/
/*EXEC sp_help [TodoTasks];*/

INSERT INTO [dbo].[TodoTasks] ([Title], [Description], [StartDate], [DueDate], [Status], [LabelID]) VALUES
('Task 1: Design UI', 'Design the user interface for the login page', '2024-07-01', '2024-07-08', 'Waiting', 1), 
('Task 2: Implement login', 'Implement the login functionality', '2024-07-05', '2024-07-12', 'InProgress', 1),
('Task 3: Write unit tests', 'Write unit tests for the login functionality', '2024-07-10', '2024-07-15', 'Completed', 1),
('Task 4: Design DB schema', 'Design the database schema', '2024-07-03', '2024-07-10', 'InProgress', 2),
('Task 5: Set up server', 'Set up the development server', '2024-07-12', '2024-07-19', 'Waiting', 2),
('Task 6: Prepare presentation', 'Create slides for project presentation', '2024-07-15', '2024-07-18', 'Completed', 3);

/****** Table SubTasks ******/
CREATE TABLE  [dbo].[SubTasks](
	[SubTaskID] INT PRIMARY KEY IDENTITY,
	[TodoTaskID] INT NOT NULL,
	[Description] NVARCHAR(MAX) NOT NULL,
	[IsCompleted] BIT DEFAULT 0
	CONSTRAINT FK_SubTasks_TodoTasks FOREIGN KEY ([TodoTaskID])
        REFERENCES [dbo].[TodoTasks]([TodoTaskID])
        ON DELETE CASCADE
)
/*DBCC CHECKIDENT ([SubTasks], RESEED, 0);*/
/*EXEC sp_help SubTasks;*/

INSERT INTO [dbo].[SubTasks] ([TodoTaskID], [Description], [IsCompleted]) VALUES
(1, 'Create wireframes', 1),
(1, 'Design mockups', 1),
(1, 'Get feedback', 0),
(2, 'Create user authentication', 1),
(2, 'Implement session management', 0),
(3, 'Write test cases for login', 1),
(3, 'Run tests and fix bugs', 1),
(4, 'Create ER diagram', 1), 
(4, 'Define tables and relationships', 1),
(4, 'Implement database', 0);


/****** Table DailyTasks ******/
CREATE TABLE [dbo].[DailyTasks](
	[DailyTasksID] int IDENTITY(1,1) PRIMARY KEY,
    [Title] NVARCHAR(200) NOT NULL,
	[Description] NVARCHAR(MAX),
	[StartDate] DATETIME,
	[DueDate] DATETIME,
)
INSERT INTO [dbo].[DailyTasks] ([Title], [Description], [StartDate], [DueDate]) VALUES
('Daily Meeting', 'Discuss project progress', '2024-07-08 09:00', '2024-07-08 10:00'),
('Code Review', 'Review code for Task 2', '2024-07-08 14:00', '2024-07-08 16:00'),
('Prepare Report', 'Prepare weekly progress report', '2024-07-12 10:00', '2024-07-12 17:00');