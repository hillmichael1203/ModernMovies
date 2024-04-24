CREATE TABLE [dbo].[Movie] (
    [MovieID]     INT           IDENTITY (1, 1) NOT NULL,
    [MovieName]   VARCHAR (50)  NOT NULL,
    [MovieDesc]   VARCHAR (200) NULL,
    [MinRuntime]  INT           NOT NULL,
    [RatingID]    INT           NOT NULL,
    [GenreID]     INT           NOT NULL,
    [Image]       VARCHAR (200) NOT NULL,
    [ReleaseDate] DATE          NOT NULL,
    CONSTRAINT [PK_Movie] PRIMARY KEY CLUSTERED ([MovieID] ASC),
    CONSTRAINT [FK_Movie_Genre] FOREIGN KEY ([GenreID]) REFERENCES [dbo].[Genre] ([GenreID]),
    CONSTRAINT [FK_Movie_Rating] FOREIGN KEY ([RatingID]) REFERENCES [dbo].[Rating] ([RatingID])
);











