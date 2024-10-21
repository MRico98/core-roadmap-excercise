USE TeamSpace;
GO

INSERT INTO Spaces (Id, Name, Description, Owner, CreatedAt)
VALUES ('bfb207ad-14d2-4efd-8847-454e11023f38', 'Other Space', 'This is a description', '5750bf75-9551-49e7-a5f6-1867bc2c4ce8', GETDATE()),
        ('bfb207ad-14d2-4efd-8847-454e11023f39', 'Another Space', 'This is a description', '5750bf75-9551-49e7-a5f6-1867bc2c4ce8', GETDATE());
GO