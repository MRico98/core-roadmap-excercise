USE TeamSpace;
GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
GO


CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
GO


CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
GO


CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
GO


CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
GO


CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
GO


CREATE INDEX [IX_AspNetUsers_RoleId] ON [AspNetUsers] ([RoleId]);
GO


CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
GO


CREATE INDEX [IX_Notes_CreatedBy] ON [Notes] ([CreatedBy]);
GO


CREATE INDEX [IX_Notes_SpaceId] ON [Notes] ([SpaceId]);
GO


CREATE INDEX [IX_Spaces_Owner] ON [Spaces] ([Owner]);
GO


CREATE INDEX [IX_SpaceUserRelations_SpaceId] ON [SpaceUserRelations] ([SpaceId]);
GO


CREATE INDEX [IX_SpaceUserRelations_UserId] ON [SpaceUserRelations] ([UserId]);
GO