--liquibase formatted sql

--changeset Asadbek:1
CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (     "MigrationId" character varying(150) NOT NULL,     "ProductVersion" character varying(32) NOT NULL,     CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId") );  START TRANSACTION;  CREATE TABLE "EmailDetails" (     "Id" integer GENERATED BY DEFAULT AS IDENTITY,     "ReceiverEmail" text NOT NULL,     "MessageDetail" text NOT NULL,     "DeliveryDate" timestamp with time zone NOT NULL,     "IsSent" boolean NOT NULL,     CONSTRAINT "PK_EmailDetails" PRIMARY KEY ("Id") );  CREATE TABLE "Values" (     "Id" uuid NOT NULL,     CONSTRAINT "PK_Values" PRIMARY KEY ("Id") );  INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES ('20230224123949_InitialCreate', '7.0.2');  COMMIT; 