CREATE TABLE [dbo].[Employee] (
    [Id]         INT         NOT NULL IDENTITY,
    [Deleted]    BIT         NULL,
    [Name]       NCHAR (255) NOT NULL,
    [Email]      NCHAR (255) NULL,
    [EmployerId] INT         NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Employee_ToEmployer] FOREIGN KEY ([EmployerId]) REFERENCES [dbo].[Employer] ([Id])
);

