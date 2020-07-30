CREATE TABLE [Common].[Users]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] NCHAR(100) NOT NULL, 
    [Surename] NCHAR(100) NOT NULL, 
    [Email] NCHAR(100) NULL, 
    [RoleId] INT NOT NULL,

    CONSTRAINT [FK_Users_Roles] FOREIGN KEY ([RoleId]) REFERENCES [Common].[Roles]([Id])
)
