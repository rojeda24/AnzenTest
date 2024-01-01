CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);

BEGIN TRANSACTION;

CREATE TABLE "Submission" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Submission" PRIMARY KEY AUTOINCREMENT,
    "AccountName" TEXT NOT NULL
);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20231230070750_InitialCreate', '8.0.0');

COMMIT;

BEGIN TRANSACTION;

ALTER TABLE "Submission" ADD "EffectiveDate" TEXT NOT NULL DEFAULT '0001-01-01';

ALTER TABLE "Submission" ADD "ExpirationDate" TEXT NOT NULL DEFAULT '0001-01-01';

ALTER TABLE "Submission" ADD "Premium" TEXT NOT NULL DEFAULT '0.0';

ALTER TABLE "Submission" ADD "Sic" TEXT NOT NULL DEFAULT '';

ALTER TABLE "Submission" ADD "UwName" TEXT NOT NULL DEFAULT '';

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20231231025741_SubmissionMoreFields', '8.0.0');

COMMIT;

