CREATE TABLE [dbo].[ShowtimeOrder] (
    [OrderNum]     INT          NOT NULL,
    [ShowtimeID]   INT          NOT NULL,
    [Quantity]     INT          NOT NULL,
    [SeatsOrdered] VARCHAR (65) NOT NULL,
    CONSTRAINT [PK_ShowtimeOrder] PRIMARY KEY CLUSTERED ([OrderNum] ASC, [ShowtimeID] ASC),
    CONSTRAINT [FK_ShowtimeOrder_Order] FOREIGN KEY ([OrderNum]) REFERENCES [dbo].[Order] ([OrderNum]),
    CONSTRAINT [FK_ShowtimeOrder_Showtime] FOREIGN KEY ([ShowtimeID]) REFERENCES [dbo].[Showtime] ([ShowtimeID])
);



