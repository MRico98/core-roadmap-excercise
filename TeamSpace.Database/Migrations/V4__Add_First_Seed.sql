USE TeamSpace;
GO

INSERT INTO Roles (Id, Name, CreatedAt)
VALUES ('472b6ed5-b3ec-477c-b93d-a37883097065', 'Admin', GETDATE()),
       ('82a63cd9-7029-4c24-8b67-c9a16162cd89', 'User', GETDATE());
GO

INSERT INTO Users (Id, Username, Password, RoleId, CreatedAt)
VALUES ('5750bf75-9551-49e7-a5f6-1867bc2c4ce8', 'admin', 'admin', '472b6ed5-b3ec-477c-b93d-a37883097065', GETDATE());
GO

INSERT INTO Spaces (Id, Name, Description, Owner, CreatedAt)
VALUES ('ccfff139-a86d-4001-96f5-bce00e707d01', 'Default', 'Default space', '5750bf75-9551-49e7-a5f6-1867bc2c4ce8', GETDATE());
GO

INSERT INTO SpaceUserRelations (Id, SpaceId, UserId, CreatedAt)
VALUES ('63eee6c7-c50b-4245-84c1-2a11c03916d6', 'ccfff139-a86d-4001-96f5-bce00e707d01', '5750bf75-9551-49e7-a5f6-1867bc2c4ce8', GETDATE());
GO

INSERT INTO Notes (Id, Title, Content, SpaceId, CreatedBy, CreatedAt)
VALUES ('f1b3b3b4-1b3b-4b3b-8b3b-1b3b3b3b3b3b', 'Welcome', 'Welcome to TeamSpace!', 'ccfff139-a86d-4001-96f5-bce00e707d01', '5750bf75-9551-49e7-a5f6-1867bc2c4ce8', GETDATE());
GO