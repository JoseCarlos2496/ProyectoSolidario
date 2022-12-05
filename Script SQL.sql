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

CREATE TABLE [cliente] (
    [cli_id] int NOT NULL IDENTITY,
    [identificacion] varchar(16) NOT NULL,
    [nombre] varchar(16) NOT NULL,
    [cli_usuario] varchar(16) NOT NULL,
    [cli_contrasena] varchar(500) NOT NULL,
    [cli_salt] varchar(500) NOT NULL,
    [cli_estado] bit NOT NULL DEFAULT CAST(1 AS bit),
    CONSTRAINT [PK_cliente] PRIMARY KEY ([cli_id])
);
GO

CREATE TABLE [cuenta] (
    [cue_numero_cuenta] varchar(16) NOT NULL,
    [cue_tipo_cuenta] varchar(16) NOT NULL,
    [cue_saldo_inicial] decimal(15,3) NOT NULL,
    [cue_estado] bit NOT NULL DEFAULT CAST(1 AS bit),
    [cue_clienteId] int NOT NULL,
    [cue_id] int NOT NULL,
    CONSTRAINT [PK_cuenta] PRIMARY KEY ([cue_numero_cuenta]),
    CONSTRAINT [FK_cuenta_cliente_cue_clienteId] FOREIGN KEY ([cue_clienteId]) REFERENCES [cliente] ([cli_id]) ON DELETE CASCADE
);
GO

CREATE TABLE [movimiento] (
    [mov_id] int NOT NULL IDENTITY,
    [mov_usuario] datetime NOT NULL,
    [mov_tipo_movimiento] varchar(16) NOT NULL,
    [mov_valor] decimal(15,3) NOT NULL,
    [mov_saldo] decimal(15,3) NOT NULL,
    [mov_cuentaId] varchar(16) NOT NULL,
    CONSTRAINT [PK_movimiento] PRIMARY KEY ([mov_id]),
    CONSTRAINT [FK_movimiento_cuenta_mov_cuentaId] FOREIGN KEY ([mov_cuentaId]) REFERENCES [cuenta] ([cue_numero_cuenta]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_cuenta_cue_clienteId] ON [cuenta] ([cue_clienteId]);
GO

CREATE INDEX [IX_movimiento_mov_cuentaId] ON [movimiento] ([mov_cuentaId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221204041021_BE-Solidario', N'7.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[cliente]') AND [c].[name] = N'nombre');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [cliente] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [cliente] ALTER COLUMN [nombre] varchar(64) NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221204042955_BE-Solidario_Update_Length_Cliente_Nombre', N'7.0.0');
GO

COMMIT;
GO

