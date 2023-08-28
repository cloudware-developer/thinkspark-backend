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

CREATE TABLE [Cliente] (
    [ClienteId] int NOT NULL IDENTITY,
    [Nome] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [EmailConfirmado] bit NOT NULL,
    [Celular] nvarchar(max) NOT NULL,
    [CelularConfirmado] bit NOT NULL,
    [Nascimento] datetime2 NOT NULL,
    [Cnpj] nvarchar(max) NULL,
    [Cpf] nvarchar(max) NULL,
    [InscricaoMunicipal] nvarchar(max) NULL,
    [InscricaoEstadual] nvarchar(max) NULL,
    [Senha] nvarchar(max) NOT NULL,
    [Logotipo] nvarchar(max) NULL,
    [Key] nvarchar(max) NULL,
    [Status] int NOT NULL,
    [CriadoEm] datetime2 NOT NULL,
    [EditadoEm] datetime2 NULL,
    CONSTRAINT [PK_Cliente] PRIMARY KEY ([ClienteId])
);
GO

CREATE TABLE [Usuario] (
    [UsuarioId] int NOT NULL IDENTITY,
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
    [Status] int NOT NULL,
    [CriadoEm] datetime2 NOT NULL,
    [EditadoEm] datetime2 NULL,
    CONSTRAINT [PK_Usuario] PRIMARY KEY ([UsuarioId])
);
GO

CREATE TABLE [ClienteEndereco] (
    [ClienteEnderecoId] int NOT NULL IDENTITY,
    [ClienteId] int NOT NULL,
    [Cep] nvarchar(max) NOT NULL,
    [Logradouro] nvarchar(max) NULL,
    [Complemento] nvarchar(max) NULL,
    [Bairro] nvarchar(max) NULL,
    [Localidade] nvarchar(max) NULL,
    [Uf] nvarchar(max) NULL,
    [Ibge] nvarchar(max) NULL,
    [Ddd] nvarchar(max) NULL,
    [Latitude] nvarchar(max) NULL,
    [Longitude] nvarchar(max) NULL,
    [Entrega] bit NOT NULL,
    CONSTRAINT [PK_ClienteEndereco] PRIMARY KEY ([ClienteEnderecoId]),
    CONSTRAINT [FK_ClienteEndereco_Cliente_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [Cliente] ([ClienteId]) ON DELETE CASCADE
);
GO

CREATE TABLE [UsuarioEndereco] (
    [UsuarioEnderecoId] int NOT NULL IDENTITY,
    [UsuarioId] int NOT NULL,
    [Cep] nvarchar(max) NOT NULL,
    [Logradouro] nvarchar(max) NULL,
    [Complemento] nvarchar(max) NULL,
    [Bairro] nvarchar(max) NULL,
    [Localidade] nvarchar(max) NULL,
    [Uf] nvarchar(max) NULL,
    [Ibge] nvarchar(max) NULL,
    [Ddd] nvarchar(max) NULL,
    [Latitude] nvarchar(max) NULL,
    [Longitude] nvarchar(max) NULL,
    CONSTRAINT [PK_UsuarioEndereco] PRIMARY KEY ([UsuarioEnderecoId]),
    CONSTRAINT [FK_UsuarioEndereco_Usuario_UsuarioId] FOREIGN KEY ([UsuarioId]) REFERENCES [Usuario] ([UsuarioId]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_ClienteEndereco_ClienteId] ON [ClienteEndereco] ([ClienteId]);
GO

CREATE INDEX [IX_UsuarioEndereco_UsuarioId] ON [UsuarioEndereco] ([UsuarioId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230814202514_InitialCreate', N'6.0.0');
GO

COMMIT;
GO

