USE TeamSpace;
GO

CREATE TABLE [AspNetRoles] (
    [Id] uniqueidentifier NOT NULL,
    [CreatedAt] datetime2 NOT NULL DEFAULT ((getdate())),
    [ModifiedAt] datetime2 NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK__Roles__3214EC079224D859] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] uniqueidentifier NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);
GO


CREATE TABLE [AspNetUsers] (
    [Id] uniqueidentifier NOT NULL,
    [CreatedAt] datetime2 NOT NULL DEFAULT ((getdate())),
    [ModifiedAt] datetime2 NULL,
    [RoleId] uniqueidentifier NOT NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK__Users__3214EC075F6317FE] PRIMARY KEY ([Id]),
    CONSTRAINT [FK__Users__RoleId__398D8EEE] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id])
);
GO


CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] uniqueidentifier NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO


CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO


CREATE TABLE [AspNetUserRoles] (
    [UserId] uniqueidentifier NOT NULL,
    [RoleId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO


CREATE TABLE [AspNetUserTokens] (
    [UserId] uniqueidentifier NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO


CREATE TABLE [Spaces] (
    [Id] uniqueidentifier NOT NULL,
    [CreatedAt] datetime2 NOT NULL DEFAULT ((getdate())),
    [ModifiedAt] datetime2 NULL,
    [Name] nvarchar(50) NOT NULL,
    [Description] text NULL,
    [Owner] uniqueidentifier NOT NULL,
    CONSTRAINT [PK__Spaces__3214EC070606833D] PRIMARY KEY ([Id]),
    CONSTRAINT [FK__Spaces__Owner__3C69FB99] FOREIGN KEY ([Owner]) REFERENCES [AspNetUsers] ([Id])
);
GO


CREATE TABLE [Notes] (
    [Id] uniqueidentifier NOT NULL,
    [CreatedAt] datetime2 NOT NULL DEFAULT ((getdate())),
    [ModifiedAt] datetime2 NULL,
    [Title] nvarchar(100) NULL,
    [Content] text NULL,
    [SpaceId] uniqueidentifier NOT NULL,
    [CreatedBy] uniqueidentifier NOT NULL,
    CONSTRAINT [PK__Notes__3214EC072905E66E] PRIMARY KEY ([Id]),
    CONSTRAINT [FK__Notes__CreatedBy__440B1D61] FOREIGN KEY ([CreatedBy]) REFERENCES [AspNetUsers] ([Id]),
    CONSTRAINT [FK__Notes__SpaceId__4316F928] FOREIGN KEY ([SpaceId]) REFERENCES [Spaces] ([Id])
);
GO


CREATE TABLE [SpaceUserRelations] (
    [Id] uniqueidentifier NOT NULL,
    [CreatedAt] datetime2 NOT NULL DEFAULT ((getdate())),
    [ModifiedAt] datetime2 NULL,
    [SpaceId] uniqueidentifier NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK__SpaceUse__3214EC07F151B4D8] PRIMARY KEY ([Id]),
    CONSTRAINT [FK__SpaceUser__Space__3F466844] FOREIGN KEY ([SpaceId]) REFERENCES [Spaces] ([Id]),
    CONSTRAINT [FK__SpaceUser__UserI__403A8C7D] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id])
);
GO