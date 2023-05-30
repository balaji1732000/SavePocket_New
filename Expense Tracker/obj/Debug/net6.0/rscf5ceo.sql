BEGIN TRANSACTION;
GO

IF EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230517105927_AccountID_Integration_in_Trasaction_model1')
BEGIN
    ALTER TABLE [Transaction] DROP CONSTRAINT [FK_Transaction_Account_AccountId1];
END;
GO

IF EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230517105927_AccountID_Integration_in_Trasaction_model1')
BEGIN
    DROP INDEX [IX_Transaction_AccountId1] ON [Transaction];
END;
GO

IF EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230517105927_AccountID_Integration_in_Trasaction_model1')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Transaction]') AND [c].[name] = N'AccountId1');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Transaction] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Transaction] DROP COLUMN [AccountId1];
END;
GO

IF EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230517105927_AccountID_Integration_in_Trasaction_model1')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Transaction]') AND [c].[name] = N'ReceiverAccountId');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Transaction] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [Transaction] DROP COLUMN [ReceiverAccountId];
END;
GO

IF EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230517105927_AccountID_Integration_in_Trasaction_model1')
BEGIN
    EXEC sp_rename N'[Transaction].[SenderAccountId]', N'AccountId', N'COLUMN';
END;
GO

IF EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230517105927_AccountID_Integration_in_Trasaction_model1')
BEGIN
    CREATE INDEX [IX_Transaction_AccountId] ON [Transaction] ([AccountId]);
END;
GO

IF EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230517105927_AccountID_Integration_in_Trasaction_model1')
BEGIN
    ALTER TABLE [Transaction] ADD CONSTRAINT [FK_Transaction_Account_AccountId] FOREIGN KEY ([AccountId]) REFERENCES [Account] ([AccountId]) ON DELETE CASCADE;
END;
GO

IF EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230517105927_AccountID_Integration_in_Trasaction_model1')
BEGIN
    DELETE FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20230517105927_AccountID_Integration_in_Trasaction_model1';
END;
GO

COMMIT;
GO

