CREATE TABLE [dbo].[Person] (
    [UserID]        INT           IDENTITY (1, 1) NOT NULL,
    [Name]          VARCHAR (100) NOT NULL,
    [Email]         VARCHAR (100) NOT NULL,
    [Password]      VARCHAR (100) NOT NULL,
    [PhoneNumber]   VARCHAR (15)  NULL,
    [TypeID]        INT           NOT NULL,
    [LastLoginTime] DATETIME      NOT NULL,
    CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED ([UserID] ASC),
    CONSTRAINT [FK_Person_AccountType] FOREIGN KEY ([TypeID]) REFERENCES [dbo].[AccountType] ([TypeID])
);



