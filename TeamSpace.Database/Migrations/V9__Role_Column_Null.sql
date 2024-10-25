USE TeamSpace;
GO

ALTER TABLE [AspNetUsers]
ALTER COLUMN [RoleId] uniqueidentifier NULL;
GO