IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406193615_init'
)
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406193615_init'
)
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] nvarchar(450) NOT NULL,
        [UserImg] nvarchar(max) NULL,
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
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406193615_init'
)
BEGIN
    CREATE TABLE [Categories] (
        [Id] int NOT NULL IDENTITY,
        [CategoryName] nvarchar(max) NOT NULL,
        [CreatedDate] datetime2 NULL,
        [UpdatedDate] datetime2 NULL,
        CONSTRAINT [PK_Categories] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406193615_init'
)
BEGIN
    CREATE TABLE [Contacts] (
        [Id] int NOT NULL IDENTITY,
        [FullName] nvarchar(max) NOT NULL,
        [Subject] nvarchar(max) NOT NULL,
        [PhoneNumber] nvarchar(max) NOT NULL,
        [Message] nvarchar(max) NOT NULL,
        [CreatedDate] datetime2 NULL,
        [UpdatedDate] datetime2 NULL,
        CONSTRAINT [PK_Contacts] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406193615_init'
)
BEGIN
    CREATE TABLE [Scriptwriters] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Surname] nvarchar(max) NULL,
        [Email] nvarchar(max) NULL,
        [Phone] nvarchar(max) NULL,
        [About] nvarchar(max) NULL,
        [PlotCount] int NOT NULL,
        [BirthDay] datetime2 NULL,
        [CreatedDate] datetime2 NULL,
        [UpdatedDate] datetime2 NULL,
        CONSTRAINT [PK_Scriptwriters] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406193615_init'
)
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406193615_init'
)
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406193615_init'
)
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(450) NOT NULL,
        [ProviderKey] nvarchar(450) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406193615_init'
)
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406193615_init'
)
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(450) NOT NULL,
        [Name] nvarchar(450) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406193615_init'
)
BEGIN
    CREATE TABLE [Plots] (
        [Id] int NOT NULL IDENTITY,
        [Header] nvarchar(max) NOT NULL,
        [Image] nvarchar(max) NULL,
        [Description] nvarchar(max) NOT NULL,
        [ReadCount] int NOT NULL,
        [Status] bit NOT NULL,
        [ScriptwriterId] int NOT NULL,
        [CommentedCount] int NOT NULL,
        [CreatedDate] datetime2 NULL,
        [UpdatedDate] datetime2 NULL,
        CONSTRAINT [PK_Plots] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Plots_Scriptwriters_ScriptwriterId] FOREIGN KEY ([ScriptwriterId]) REFERENCES [Scriptwriters] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406193615_init'
)
BEGIN
    CREATE TABLE [Chapters] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Content] nvarchar(max) NOT NULL,
        [Page] int NOT NULL,
        [PlotId] int NOT NULL,
        [CreatedDate] datetime2 NULL,
        [UpdatedDate] datetime2 NULL,
        CONSTRAINT [PK_Chapters] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Chapters_Plots_PlotId] FOREIGN KEY ([PlotId]) REFERENCES [Plots] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406193615_init'
)
BEGIN
    CREATE TABLE [PlotAppUsers] (
        [Id] int NOT NULL IDENTITY,
        [PlotId] int NOT NULL,
        [AppUserId] nvarchar(450) NOT NULL,
        [IsFavorite] bit NOT NULL,
        [CreatedDate] datetime2 NULL,
        [UpdatedDate] datetime2 NULL,
        CONSTRAINT [PK_PlotAppUsers] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_PlotAppUsers_AspNetUsers_AppUserId] FOREIGN KEY ([AppUserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_PlotAppUsers_Plots_PlotId] FOREIGN KEY ([PlotId]) REFERENCES [Plots] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406193615_init'
)
BEGIN
    CREATE TABLE [PlotCategories] (
        [Id] int NOT NULL IDENTITY,
        [PlotId] int NOT NULL,
        [CategoryId] int NOT NULL,
        [CreatedDate] datetime2 NULL,
        [UpdatedDate] datetime2 NULL,
        CONSTRAINT [PK_PlotCategories] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_PlotCategories_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_PlotCategories_Plots_PlotId] FOREIGN KEY ([PlotId]) REFERENCES [Plots] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406193615_init'
)
BEGIN
    CREATE TABLE [PlotRatings] (
        [Id] int NOT NULL IDENTITY,
        [PlotId] int NOT NULL,
        [AppUserId] nvarchar(450) NOT NULL,
        [Rating] int NOT NULL,
        [CreatedDate] datetime2 NULL,
        [UpdatedDate] datetime2 NULL,
        CONSTRAINT [PK_PlotRatings] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_PlotRatings_AspNetUsers_AppUserId] FOREIGN KEY ([AppUserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_PlotRatings_Plots_PlotId] FOREIGN KEY ([PlotId]) REFERENCES [Plots] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406193615_init'
)
BEGIN
    CREATE TABLE [UserScenarioFavorite] (
        [Id] int NOT NULL IDENTITY,
        [AppUserId] nvarchar(450) NOT NULL,
        [PlotId] int NOT NULL,
        [CreatedDate] datetime2 NULL,
        [UpdatedDate] datetime2 NULL,
        CONSTRAINT [PK_UserScenarioFavorite] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_UserScenarioFavorite_AspNetUsers_AppUserId] FOREIGN KEY ([AppUserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_UserScenarioFavorite_Plots_PlotId] FOREIGN KEY ([PlotId]) REFERENCES [Plots] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406193615_init'
)
BEGIN
    CREATE TABLE [Comments] (
        [Id] int NOT NULL IDENTITY,
        [Content] nvarchar(max) NOT NULL,
        [AppUserId] nvarchar(450) NOT NULL,
        [AppUserName] nvarchar(max) NOT NULL,
        [ChapterId] int NOT NULL,
        [ParentCommentId] int NULL,
        [CreatedDate] datetime2 NULL,
        [UpdatedDate] datetime2 NULL,
        CONSTRAINT [PK_Comments] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Comments_AspNetUsers_AppUserId] FOREIGN KEY ([AppUserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Comments_Chapters_ChapterId] FOREIGN KEY ([ChapterId]) REFERENCES [Chapters] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Comments_Comments_ParentCommentId] FOREIGN KEY ([ParentCommentId]) REFERENCES [Comments] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406193615_init'
)
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406193615_init'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406193615_init'
)
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406193615_init'
)
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406193615_init'
)
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406193615_init'
)
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406193615_init'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406193615_init'
)
BEGIN
    CREATE INDEX [IX_Chapters_PlotId] ON [Chapters] ([PlotId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406193615_init'
)
BEGIN
    CREATE INDEX [IX_Comments_AppUserId] ON [Comments] ([AppUserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406193615_init'
)
BEGIN
    CREATE INDEX [IX_Comments_ChapterId] ON [Comments] ([ChapterId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406193615_init'
)
BEGIN
    CREATE INDEX [IX_Comments_ParentCommentId] ON [Comments] ([ParentCommentId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406193615_init'
)
BEGIN
    CREATE INDEX [IX_PlotAppUsers_AppUserId] ON [PlotAppUsers] ([AppUserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406193615_init'
)
BEGIN
    CREATE INDEX [IX_PlotAppUsers_PlotId] ON [PlotAppUsers] ([PlotId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406193615_init'
)
BEGIN
    CREATE INDEX [IX_PlotCategories_CategoryId] ON [PlotCategories] ([CategoryId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406193615_init'
)
BEGIN
    CREATE INDEX [IX_PlotCategories_PlotId] ON [PlotCategories] ([PlotId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406193615_init'
)
BEGIN
    CREATE INDEX [IX_PlotRatings_AppUserId] ON [PlotRatings] ([AppUserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406193615_init'
)
BEGIN
    CREATE INDEX [IX_PlotRatings_PlotId] ON [PlotRatings] ([PlotId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406193615_init'
)
BEGIN
    CREATE INDEX [IX_Plots_ScriptwriterId] ON [Plots] ([ScriptwriterId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406193615_init'
)
BEGIN
    CREATE INDEX [IX_UserScenarioFavorite_AppUserId] ON [UserScenarioFavorite] ([AppUserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406193615_init'
)
BEGIN
    CREATE INDEX [IX_UserScenarioFavorite_PlotId] ON [UserScenarioFavorite] ([PlotId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406193615_init'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250406193615_init', N'8.0.0');
END;
GO

COMMIT;
GO

