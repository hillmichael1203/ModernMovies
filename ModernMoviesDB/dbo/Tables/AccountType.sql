CREATE TABLE [dbo].[AccountType] (
    [TypeID]   INT          NOT NULL,
    [TypeName] VARCHAR (20) NOT NULL,
    CONSTRAINT [PK_AccountType] PRIMARY KEY CLUSTERED ([TypeID] ASC)
);

