CREATE TABLE [dbo].[Order] (
    [OrderNum]  INT      IDENTITY (1, 1) NOT NULL,
    [OrderTime] DATETIME NOT NULL,
    [Subtotal]  MONEY    NOT NULL,
    [UserID]    INT      NOT NULL,
    CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED ([OrderNum] ASC),
    CONSTRAINT [FK_Order_Person] FOREIGN KEY ([UserID]) REFERENCES [dbo].[Person] ([UserID])
);



