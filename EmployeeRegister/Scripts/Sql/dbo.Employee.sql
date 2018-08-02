CREATE TABLE [dbo].[Employee] (
    [Id]         INT         IDENTITY (1, 1) NOT NULL,
    [Deleted]    BIT         NULL,
    [Name]       NVARCHAR (255) NOT NULL,
    [Email]      NVARCHAR (255) NULL,
    [EmployerId] INT         NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Employee_ToEmployer] FOREIGN KEY ([EmployerId]) REFERENCES [dbo].[Employer] ([Id])
);

