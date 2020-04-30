CREATE TABLE [dbo].[MonkeyRecords] (
    [recordID]   INT         NOT NULL,
    [monkeyID]   INT         NOT NULL,
    [monkeyName] VARCHAR (8) NOT NULL,
    [woodID]     INT         NOT NULL,
    [seqnr]      INT         NOT NULL,
    [treeID]     INT         NOT NULL,
    [x]          INT         NOT NULL,
    [y]          INT         NOT NULL,
    PRIMARY KEY CLUSTERED ([recordID] ASC)
);

