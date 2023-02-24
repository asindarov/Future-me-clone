--liquibase formatted sql

--changeset Asadbek:2
START TRANSACTION;  ALTER TABLE "EmailDetails" RENAME COLUMN "MessageDetail" TO "Message";  INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES ('20230224124136_RenameEmailDetailsMessageDetailField', '7.0.2');  COMMIT; 
