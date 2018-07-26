CREATE TABLE [dbo].[Employer] (
    [Id]         INT         NOT NULL IDENTITY,
    [Name]       NCHAR (255) NOT NULL,
    [Deleted]    BIT         NULL,
    [EmployerId] INT         NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Name] ASC),
    CONSTRAINT [FK_Employer_ToEmployer] FOREIGN KEY ([EmployerId]) REFERENCES [dbo].[Employer] ([Id])
);

