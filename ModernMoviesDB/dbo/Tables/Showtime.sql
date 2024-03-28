CREATE TABLE [dbo].[Showtime] (
    [ShowtimeID] INT          IDENTITY (1, 1) NOT NULL,
    [Time]       DATETIME     NOT NULL,
    [SeatsTaken] VARCHAR (65) NOT NULL,
    [MovieID]    INT          NOT NULL,
    CONSTRAINT [PK_Showtime] PRIMARY KEY CLUSTERED ([ShowtimeID] ASC),
    CONSTRAINT [FK_Showtime_Movie] FOREIGN KEY ([MovieID]) REFERENCES [dbo].[Movie] ([MovieID])
);



