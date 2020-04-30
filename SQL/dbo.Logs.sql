CREATE TABLE [dbo].[Logs] (
    [Id]       INT            NOT NULL,
    [woodID]   INT            NOT NULL,
    [monkeyID] INT            NOT NULL,
    [message]  VARCHAR (60) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

