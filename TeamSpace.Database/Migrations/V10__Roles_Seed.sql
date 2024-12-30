USE TeamSpace;
GO

INSERT INTO [AspNetRoles] ([Id], [CreatedAt], [Name], [NormalizedName], [ConcurrencyStamp])
VALUES 
(NEWID(), GETDATE(), 'Admin', 'ADMIN', NEWID()),
(NEWID(), GETDATE(), 'User', 'USER', NEWID());
GO

IF EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_AspNetUsers_RoleId')
BEGIN
    DROP INDEX [IX_AspNetUsers_RoleId] ON [AspNetUsers];
END

UPDATE [AspNetUsers]
SET [RoleId] = (SELECT [Id] FROM [AspNetRoles] WHERE [Name] = 'Admin')
WHERE [UserName] IS NULL;

ALTER TABLE [AspNetUsers]
ALTER COLUMN RoleId uniqueidentifier NOT NULL;

CREATE INDEX IX_AspNetUsers_RoleId ON AspNetUsers(RoleId);
GO