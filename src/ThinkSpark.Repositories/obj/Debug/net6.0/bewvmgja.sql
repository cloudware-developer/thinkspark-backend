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

CREATE TABLE [Pessoa] (
    [PessoaId] int NOT NULL IDENTITY,
    [Nome] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [EmailConfirmado] bit NOT NULL,
    [Celular] nvarchar(max) NOT NULL,
    [CelularConfirmado] bit NOT NULL,
    [Nascimento] datetime2 NOT NULL,
    [Cpf] nvarchar(max) NOT NULL,
    [Rg] nvarchar(max) NULL,
    [Senha] nvarchar(max) NOT NULL,
    [Foto] nvarchar(max) NULL,
    [Key] nvarchar(max) NULL,
    [Status] int NOT NULL,
    [CriadoEm] datetime2 NOT NULL,
    [EditadoEm] datetime2 NULL,
    CONSTRAINT [PK_Pessoa] PRIMARY KEY ([PessoaId])
);
GO

CREATE TABLE [Contato] (
    [ContatoId] int NOT NULL IDENTITY,
    [PessoaId] int NOT NULL,
    [TipoContatoId] int NOT NULL,
    [Descricao] nvarchar(max) NULL,
    CONSTRAINT [PK_Contato] PRIMARY KEY ([ContatoId]),
    CONSTRAINT [FK_Contato_Pessoa_PessoaId] FOREIGN KEY ([PessoaId]) REFERENCES [Pessoa] ([PessoaId]) ON DELETE CASCADE
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'PessoaId', N'Celular', N'CelularConfirmado', N'Cpf', N'CriadoEm', N'EditadoEm', N'Email', N'EmailConfirmado', N'Foto', N'Key', N'Nascimento', N'Nome', N'Rg', N'Senha', N'Status') AND [object_id] = OBJECT_ID(N'[Pessoa]'))
    SET IDENTITY_INSERT [Pessoa] ON;
INSERT INTO [Pessoa] ([PessoaId], [Celular], [CelularConfirmado], [Cpf], [CriadoEm], [EditadoEm], [Email], [EmailConfirmado], [Foto], [Key], [Nascimento], [Nome], [Rg], [Senha], [Status])
VALUES (1, N'7206846556', CAST(1 AS bit), N'63089812061', '2023-08-26T11:18:21.1074676-03:00', NULL, N'morris@hotmail.com', CAST(1 AS bit), N'', NULL, '1984-01-18T00:00:00.0000000', N'Dale K. Morris', N'69139726010', N'b3d312594eda53ca6896ed30b539b899cac892c843221faca1c6d7a46dce1623', 1);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'PessoaId', N'Celular', N'CelularConfirmado', N'Cpf', N'CriadoEm', N'EditadoEm', N'Email', N'EmailConfirmado', N'Foto', N'Key', N'Nascimento', N'Nome', N'Rg', N'Senha', N'Status') AND [object_id] = OBJECT_ID(N'[Pessoa]'))
    SET IDENTITY_INSERT [Pessoa] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'PessoaId', N'Celular', N'CelularConfirmado', N'Cpf', N'CriadoEm', N'EditadoEm', N'Email', N'EmailConfirmado', N'Foto', N'Key', N'Nascimento', N'Nome', N'Rg', N'Senha', N'Status') AND [object_id] = OBJECT_ID(N'[Pessoa]'))
    SET IDENTITY_INSERT [Pessoa] ON;
INSERT INTO [Pessoa] ([PessoaId], [Celular], [CelularConfirmado], [Cpf], [CriadoEm], [EditadoEm], [Email], [EmailConfirmado], [Foto], [Key], [Nascimento], [Nome], [Rg], [Senha], [Status])
VALUES (2, N'4803280179', CAST(0 AS bit), N'92783770075', '2023-08-26T11:18:21.1074687-03:00', NULL, N'cooper@hotmail.com', CAST(1 AS bit), N'', NULL, '1984-01-18T00:00:00.0000000', N'Pablo G. Cooper', N'21878983008', N'b3d312594eda53ca6896ed30b539b899cac892c843221faca1c6d7a46dce1623', 1);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'PessoaId', N'Celular', N'CelularConfirmado', N'Cpf', N'CriadoEm', N'EditadoEm', N'Email', N'EmailConfirmado', N'Foto', N'Key', N'Nascimento', N'Nome', N'Rg', N'Senha', N'Status') AND [object_id] = OBJECT_ID(N'[Pessoa]'))
    SET IDENTITY_INSERT [Pessoa] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'PessoaId', N'Celular', N'CelularConfirmado', N'Cpf', N'CriadoEm', N'EditadoEm', N'Email', N'EmailConfirmado', N'Foto', N'Key', N'Nascimento', N'Nome', N'Rg', N'Senha', N'Status') AND [object_id] = OBJECT_ID(N'[Pessoa]'))
    SET IDENTITY_INSERT [Pessoa] ON;
INSERT INTO [Pessoa] ([PessoaId], [Celular], [CelularConfirmado], [Cpf], [CriadoEm], [EditadoEm], [Email], [EmailConfirmado], [Foto], [Key], [Nascimento], [Nome], [Rg], [Senha], [Status])
VALUES (3, N'9254135709', CAST(0 AS bit), N'22523862077', '2023-08-26T11:18:21.1074689-03:00', NULL, N'chatterton@hotmail.com', CAST(1 AS bit), N'', NULL, '1984-01-18T00:00:00.0000000', N'Curtis E. Chatterton', N'58549417084', N'b3d312594eda53ca6896ed30b539b899cac892c843221faca1c6d7a46dce1623', 1);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'PessoaId', N'Celular', N'CelularConfirmado', N'Cpf', N'CriadoEm', N'EditadoEm', N'Email', N'EmailConfirmado', N'Foto', N'Key', N'Nascimento', N'Nome', N'Rg', N'Senha', N'Status') AND [object_id] = OBJECT_ID(N'[Pessoa]'))
    SET IDENTITY_INSERT [Pessoa] OFF;
GO

CREATE INDEX [IX_Contato_PessoaId] ON [Contato] ([PessoaId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230826141821_Inicial', N'6.0.0');
GO

COMMIT;
GO

