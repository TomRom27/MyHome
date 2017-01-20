
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/17/2017 10:52:32
-- Generated from EDMX file: C:\Projects\tr\MyHome\MyHome\DataModel\DataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [MyHomeTR];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_DeviceAddress]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DeviceSet] DROP CONSTRAINT [FK_DeviceAddress];
GO
IF OBJECT_ID(N'[dbo].[FK_DeviceReadout]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ReadoutSet] DROP CONSTRAINT [FK_DeviceReadout];
GO
IF OBJECT_ID(N'[dbo].[FK_OwnerGroupAddress]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AddressSet] DROP CONSTRAINT [FK_OwnerGroupAddress];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[AddressSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AddressSet];
GO
IF OBJECT_ID(N'[dbo].[ReadoutSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ReadoutSet];
GO
IF OBJECT_ID(N'[dbo].[DeviceSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DeviceSet];
GO
IF OBJECT_ID(N'[dbo].[OwnerGroupSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OwnerGroupSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'AddressSet'
CREATE TABLE [dbo].[AddressSet] (
    [AddressId] nvarchar(40)  NOT NULL,
    [FriendlyName] nvarchar(max)  NOT NULL,
    [Owner_Id] int  NOT NULL
);
GO

-- Creating table 'ReadoutSet'
CREATE TABLE [dbo].[ReadoutSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [At] datetime  NOT NULL,
    [Value] decimal(18,3)  NOT NULL,
    [ActionOn] bit  NOT NULL,
    [Device_DeviceId] nvarchar(40)  NOT NULL
);
GO

-- Creating table 'DeviceSet'
CREATE TABLE [dbo].[DeviceSet] (
    [DeviceId] nvarchar(40)  NOT NULL,
    [ActionState] int  NOT NULL,
    [SensorType] int  NOT NULL,
    [DeviceAddress_AddressId] nvarchar(40)  NOT NULL
);
GO

-- Creating table 'OwnerGroupSet'
CREATE TABLE [dbo].[OwnerGroupSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [AddressId] in table 'AddressSet'
ALTER TABLE [dbo].[AddressSet]
ADD CONSTRAINT [PK_AddressSet]
    PRIMARY KEY CLUSTERED ([AddressId] ASC);
GO

-- Creating primary key on [Id] in table 'ReadoutSet'
ALTER TABLE [dbo].[ReadoutSet]
ADD CONSTRAINT [PK_ReadoutSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [DeviceId] in table 'DeviceSet'
ALTER TABLE [dbo].[DeviceSet]
ADD CONSTRAINT [PK_DeviceSet]
    PRIMARY KEY CLUSTERED ([DeviceId] ASC);
GO

-- Creating primary key on [Id] in table 'OwnerGroupSet'
ALTER TABLE [dbo].[OwnerGroupSet]
ADD CONSTRAINT [PK_OwnerGroupSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [DeviceAddress_AddressId] in table 'DeviceSet'
ALTER TABLE [dbo].[DeviceSet]
ADD CONSTRAINT [FK_DeviceAddress]
    FOREIGN KEY ([DeviceAddress_AddressId])
    REFERENCES [dbo].[AddressSet]
        ([AddressId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DeviceAddress'
CREATE INDEX [IX_FK_DeviceAddress]
ON [dbo].[DeviceSet]
    ([DeviceAddress_AddressId]);
GO

-- Creating foreign key on [Device_DeviceId] in table 'ReadoutSet'
ALTER TABLE [dbo].[ReadoutSet]
ADD CONSTRAINT [FK_DeviceReadout]
    FOREIGN KEY ([Device_DeviceId])
    REFERENCES [dbo].[DeviceSet]
        ([DeviceId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DeviceReadout'
CREATE INDEX [IX_FK_DeviceReadout]
ON [dbo].[ReadoutSet]
    ([Device_DeviceId]);
GO

-- Creating foreign key on [Owner_Id] in table 'AddressSet'
ALTER TABLE [dbo].[AddressSet]
ADD CONSTRAINT [FK_OwnerGroupAddress]
    FOREIGN KEY ([Owner_Id])
    REFERENCES [dbo].[OwnerGroupSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OwnerGroupAddress'
CREATE INDEX [IX_FK_OwnerGroupAddress]
ON [dbo].[AddressSet]
    ([Owner_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------