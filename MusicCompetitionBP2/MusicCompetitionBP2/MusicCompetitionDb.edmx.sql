
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/04/2021 13:54:59
-- Generated from EDMX file: D:\PROJEKTI\DiplomskiProjekatBP\MusicCompetitionBP2\MusicCompetitionBP2\MusicCompetitionDb.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [BP2DB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_PublishingHouseOrganize]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Organizations] DROP CONSTRAINT [FK_PublishingHouseOrganize];
GO
IF OBJECT_ID(N'[dbo].[FK_CompetitionOrganize]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Organizations] DROP CONSTRAINT [FK_CompetitionOrganize];
GO
IF OBJECT_ID(N'[dbo].[FK_JuryMemberHiredFor]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HiredForSet] DROP CONSTRAINT [FK_JuryMemberHiredFor];
GO
IF OBJECT_ID(N'[dbo].[FK_CompetitionHiredFor]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HiredForSet] DROP CONSTRAINT [FK_CompetitionHiredFor];
GO
IF OBJECT_ID(N'[dbo].[FK_CompetitionPossessesA]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PossessesASet] DROP CONSTRAINT [FK_CompetitionPossessesA];
GO
IF OBJECT_ID(N'[dbo].[FK_GenrePossessesA]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PossessesASet] DROP CONSTRAINT [FK_GenrePossessesA];
GO
IF OBJECT_ID(N'[dbo].[FK_JuryMemberIsExpert]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IsExpertSet] DROP CONSTRAINT [FK_JuryMemberIsExpert];
GO
IF OBJECT_ID(N'[dbo].[FK_GenreIsExpert]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IsExpertSet] DROP CONSTRAINT [FK_GenreIsExpert];
GO
IF OBJECT_ID(N'[dbo].[FK_MusicPerformanceGenre]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MusicPerformances] DROP CONSTRAINT [FK_MusicPerformanceGenre];
GO
IF OBJECT_ID(N'[dbo].[FK_IsExpertEvaluate]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Evaluations] DROP CONSTRAINT [FK_IsExpertEvaluate];
GO
IF OBJECT_ID(N'[dbo].[FK_MusicPerformanceEvaluate]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Evaluations] DROP CONSTRAINT [FK_MusicPerformanceEvaluate];
GO
IF OBJECT_ID(N'[dbo].[FK_OrganizeReserve]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Reservations] DROP CONSTRAINT [FK_OrganizeReserve];
GO
IF OBJECT_ID(N'[dbo].[FK_PerformanceHallReserve]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Reservations] DROP CONSTRAINT [FK_PerformanceHallReserve];
GO
IF OBJECT_ID(N'[dbo].[FK_CompetitorCompetiting]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Competitings] DROP CONSTRAINT [FK_CompetitorCompetiting];
GO
IF OBJECT_ID(N'[dbo].[FK_OrganizeCompetiting]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Competitings] DROP CONSTRAINT [FK_OrganizeCompetiting];
GO
IF OBJECT_ID(N'[dbo].[FK_CompetitingMusicPerformance]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MusicPerformances] DROP CONSTRAINT [FK_CompetitingMusicPerformance];
GO
IF OBJECT_ID(N'[dbo].[FK_JuryMember_inherits_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Singers_JuryMember] DROP CONSTRAINT [FK_JuryMember_inherits_User];
GO
IF OBJECT_ID(N'[dbo].[FK_Competitor_inherits_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Singers_Competitor] DROP CONSTRAINT [FK_Competitor_inherits_User];
GO
IF OBJECT_ID(N'[dbo].[FK_Administrator_inherits_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Singers_Administrator] DROP CONSTRAINT [FK_Administrator_inherits_User];
GO
IF OBJECT_ID(N'[dbo].[FK_EventOrganizer_inherits_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Singers_EventOrganizer] DROP CONSTRAINT [FK_EventOrganizer_inherits_User];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Singers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Singers];
GO
IF OBJECT_ID(N'[dbo].[Competitions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Competitions];
GO
IF OBJECT_ID(N'[dbo].[Genres]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Genres];
GO
IF OBJECT_ID(N'[dbo].[MusicPerformances]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MusicPerformances];
GO
IF OBJECT_ID(N'[dbo].[PublishingHouses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PublishingHouses];
GO
IF OBJECT_ID(N'[dbo].[PerformanceHalls]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PerformanceHalls];
GO
IF OBJECT_ID(N'[dbo].[Organizations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Organizations];
GO
IF OBJECT_ID(N'[dbo].[HiredForSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[HiredForSet];
GO
IF OBJECT_ID(N'[dbo].[PossessesASet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PossessesASet];
GO
IF OBJECT_ID(N'[dbo].[IsExpertSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IsExpertSet];
GO
IF OBJECT_ID(N'[dbo].[Evaluations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Evaluations];
GO
IF OBJECT_ID(N'[dbo].[Reservations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Reservations];
GO
IF OBJECT_ID(N'[dbo].[Competitings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Competitings];
GO
IF OBJECT_ID(N'[dbo].[Singers_JuryMember]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Singers_JuryMember];
GO
IF OBJECT_ID(N'[dbo].[Singers_Competitor]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Singers_Competitor];
GO
IF OBJECT_ID(N'[dbo].[Singers_Administrator]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Singers_Administrator];
GO
IF OBJECT_ID(N'[dbo].[Singers_EventOrganizer]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Singers_EventOrganizer];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [JMBG_SIN] bigint  NOT NULL,
    [FIRSTNAME_SIN] nvarchar(max)  NOT NULL,
    [LASTNAME_SIN] nvarchar(max)  NOT NULL,
    [BIRTHDATE_SIN] datetime  NOT NULL,
    [EMAIL_SIN] nvarchar(max)  NOT NULL,
    [PHONE_NO_SIN] nvarchar(max)  NOT NULL,
    [ADDRESS_SIN_HOME_NUMBER] nvarchar(max)  NOT NULL,
    [ADDRESS_SIN_CITY] nvarchar(max)  NOT NULL,
    [ADDRESS_SIN_STREET] nvarchar(max)  NOT NULL,
    [Type] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NULL
);
GO

-- Creating table 'Competitions'
CREATE TABLE [dbo].[Competitions] (
    [ID_COMP] int IDENTITY(1,1) NOT NULL,
    [DATE_START] datetime  NOT NULL,
    [DATE_END] datetime  NOT NULL,
    [NAME_COMP] nvarchar(max)  NOT NULL,
    [MAX_COMPETITORS] int  NOT NULL
);
GO

-- Creating table 'Genres'
CREATE TABLE [dbo].[Genres] (
    [ID_GENRE] int IDENTITY(1,1) NOT NULL,
    [GENRE_NAME] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'MusicPerformances'
CREATE TABLE [dbo].[MusicPerformances] (
    [ID_PERF] int IDENTITY(1,1) NOT NULL,
    [ORIG_PERFORMER] nvarchar(max)  NOT NULL,
    [SONG_NAME] nvarchar(max)  NOT NULL,
    [SONG_AUTHOR] nvarchar(max)  NOT NULL,
    [DATE_PERF] datetime  NOT NULL,
    [GenreID_GENRE] int  NOT NULL,
    [CompetitingCompetitorJMBG_SIN] bigint  NULL,
    [CompetitingOrganizePublishingHouseID_PH] int  NULL,
    [CompetitingOrganizeCompetitionID_COMP] int  NULL
);
GO

-- Creating table 'PublishingHouses'
CREATE TABLE [dbo].[PublishingHouses] (
    [ID_PH] int IDENTITY(1,1) NOT NULL,
    [NAME_PH] nvarchar(max)  NOT NULL,
    [ADR_PH_HOME_NUMBER] nvarchar(max)  NOT NULL,
    [ADR_PH_CITY] nvarchar(max)  NOT NULL,
    [ADR_PH_STREET] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'PerformanceHalls'
CREATE TABLE [dbo].[PerformanceHalls] (
    [ID_HALL] int IDENTITY(1,1) NOT NULL,
    [NAME_HALL] nvarchar(max)  NOT NULL,
    [CAPACITY] int  NOT NULL
);
GO

-- Creating table 'Organizations'
CREATE TABLE [dbo].[Organizations] (
    [PublishingHouseID_PH] int  NOT NULL,
    [CompetitionID_COMP] int  NOT NULL
);
GO

-- Creating table 'HiredForSet'
CREATE TABLE [dbo].[HiredForSet] (
    [JuryMemberJMBG_SIN] bigint  NOT NULL,
    [CompetitionID_COMP] int  NOT NULL
);
GO

-- Creating table 'PossessesASet'
CREATE TABLE [dbo].[PossessesASet] (
    [CompetitionID_COMP] int  NOT NULL,
    [GenreID_GENRE] int  NOT NULL
);
GO

-- Creating table 'IsExpertSet'
CREATE TABLE [dbo].[IsExpertSet] (
    [JuryMemberJMBG_SIN] bigint  NOT NULL,
    [GenreID_GENRE] int  NOT NULL
);
GO

-- Creating table 'Evaluations'
CREATE TABLE [dbo].[Evaluations] (
    [MARK] smallint  NOT NULL,
    [COMMENT] nvarchar(max)  NOT NULL,
    [IsExpertJuryMemberJMBG_SIN] bigint  NOT NULL,
    [IsExpertGenreID_GENRE] int  NOT NULL,
    [MusicPerformanceID_PERF] int  NOT NULL
);
GO

-- Creating table 'Reservations'
CREATE TABLE [dbo].[Reservations] (
    [DATE_RES] datetime  NOT NULL,
    [START_TIME] time  NOT NULL,
    [END_TIME] time  NOT NULL,
    [OrganizePublishingHouseID_PH] int  NOT NULL,
    [OrganizeCompetitionID_COMP] int  NOT NULL,
    [PerformanceHallID_HALL] int  NOT NULL
);
GO

-- Creating table 'Competitings'
CREATE TABLE [dbo].[Competitings] (
    [CompetitorJMBG_SIN] bigint  NOT NULL,
    [OrganizePublishingHouseID_PH] int  NOT NULL,
    [OrganizeCompetitionID_COMP] int  NOT NULL
);
GO

-- Creating table 'Users_JuryMember'
CREATE TABLE [dbo].[Users_JuryMember] (
    [JMBG_SIN] bigint  NOT NULL
);
GO

-- Creating table 'Users_Competitor'
CREATE TABLE [dbo].[Users_Competitor] (
    [JMBG_SIN] bigint  NOT NULL
);
GO

-- Creating table 'Users_Administrator'
CREATE TABLE [dbo].[Users_Administrator] (
    [JMBG_SIN] bigint  NOT NULL
);
GO

-- Creating table 'Users_EventOrganizer'
CREATE TABLE [dbo].[Users_EventOrganizer] (
    [JMBG_SIN] bigint  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [JMBG_SIN] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([JMBG_SIN] ASC);
GO

-- Creating primary key on [ID_COMP] in table 'Competitions'
ALTER TABLE [dbo].[Competitions]
ADD CONSTRAINT [PK_Competitions]
    PRIMARY KEY CLUSTERED ([ID_COMP] ASC);
GO

-- Creating primary key on [ID_GENRE] in table 'Genres'
ALTER TABLE [dbo].[Genres]
ADD CONSTRAINT [PK_Genres]
    PRIMARY KEY CLUSTERED ([ID_GENRE] ASC);
GO

-- Creating primary key on [ID_PERF] in table 'MusicPerformances'
ALTER TABLE [dbo].[MusicPerformances]
ADD CONSTRAINT [PK_MusicPerformances]
    PRIMARY KEY CLUSTERED ([ID_PERF] ASC);
GO

-- Creating primary key on [ID_PH] in table 'PublishingHouses'
ALTER TABLE [dbo].[PublishingHouses]
ADD CONSTRAINT [PK_PublishingHouses]
    PRIMARY KEY CLUSTERED ([ID_PH] ASC);
GO

-- Creating primary key on [ID_HALL] in table 'PerformanceHalls'
ALTER TABLE [dbo].[PerformanceHalls]
ADD CONSTRAINT [PK_PerformanceHalls]
    PRIMARY KEY CLUSTERED ([ID_HALL] ASC);
GO

-- Creating primary key on [PublishingHouseID_PH], [CompetitionID_COMP] in table 'Organizations'
ALTER TABLE [dbo].[Organizations]
ADD CONSTRAINT [PK_Organizations]
    PRIMARY KEY CLUSTERED ([PublishingHouseID_PH], [CompetitionID_COMP] ASC);
GO

-- Creating primary key on [JuryMemberJMBG_SIN], [CompetitionID_COMP] in table 'HiredForSet'
ALTER TABLE [dbo].[HiredForSet]
ADD CONSTRAINT [PK_HiredForSet]
    PRIMARY KEY CLUSTERED ([JuryMemberJMBG_SIN], [CompetitionID_COMP] ASC);
GO

-- Creating primary key on [CompetitionID_COMP], [GenreID_GENRE] in table 'PossessesASet'
ALTER TABLE [dbo].[PossessesASet]
ADD CONSTRAINT [PK_PossessesASet]
    PRIMARY KEY CLUSTERED ([CompetitionID_COMP], [GenreID_GENRE] ASC);
GO

-- Creating primary key on [JuryMemberJMBG_SIN], [GenreID_GENRE] in table 'IsExpertSet'
ALTER TABLE [dbo].[IsExpertSet]
ADD CONSTRAINT [PK_IsExpertSet]
    PRIMARY KEY CLUSTERED ([JuryMemberJMBG_SIN], [GenreID_GENRE] ASC);
GO

-- Creating primary key on [IsExpertJuryMemberJMBG_SIN], [IsExpertGenreID_GENRE], [MusicPerformanceID_PERF] in table 'Evaluations'
ALTER TABLE [dbo].[Evaluations]
ADD CONSTRAINT [PK_Evaluations]
    PRIMARY KEY CLUSTERED ([IsExpertJuryMemberJMBG_SIN], [IsExpertGenreID_GENRE], [MusicPerformanceID_PERF] ASC);
GO

-- Creating primary key on [OrganizePublishingHouseID_PH], [OrganizeCompetitionID_COMP], [PerformanceHallID_HALL] in table 'Reservations'
ALTER TABLE [dbo].[Reservations]
ADD CONSTRAINT [PK_Reservations]
    PRIMARY KEY CLUSTERED ([OrganizePublishingHouseID_PH], [OrganizeCompetitionID_COMP], [PerformanceHallID_HALL] ASC);
GO

-- Creating primary key on [CompetitorJMBG_SIN], [OrganizePublishingHouseID_PH], [OrganizeCompetitionID_COMP] in table 'Competitings'
ALTER TABLE [dbo].[Competitings]
ADD CONSTRAINT [PK_Competitings]
    PRIMARY KEY CLUSTERED ([CompetitorJMBG_SIN], [OrganizePublishingHouseID_PH], [OrganizeCompetitionID_COMP] ASC);
GO

-- Creating primary key on [JMBG_SIN] in table 'Users_JuryMember'
ALTER TABLE [dbo].[Users_JuryMember]
ADD CONSTRAINT [PK_Users_JuryMember]
    PRIMARY KEY CLUSTERED ([JMBG_SIN] ASC);
GO

-- Creating primary key on [JMBG_SIN] in table 'Users_Competitor'
ALTER TABLE [dbo].[Users_Competitor]
ADD CONSTRAINT [PK_Users_Competitor]
    PRIMARY KEY CLUSTERED ([JMBG_SIN] ASC);
GO

-- Creating primary key on [JMBG_SIN] in table 'Users_Administrator'
ALTER TABLE [dbo].[Users_Administrator]
ADD CONSTRAINT [PK_Users_Administrator]
    PRIMARY KEY CLUSTERED ([JMBG_SIN] ASC);
GO

-- Creating primary key on [JMBG_SIN] in table 'Users_EventOrganizer'
ALTER TABLE [dbo].[Users_EventOrganizer]
ADD CONSTRAINT [PK_Users_EventOrganizer]
    PRIMARY KEY CLUSTERED ([JMBG_SIN] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [PublishingHouseID_PH] in table 'Organizations'
ALTER TABLE [dbo].[Organizations]
ADD CONSTRAINT [FK_PublishingHouseOrganize]
    FOREIGN KEY ([PublishingHouseID_PH])
    REFERENCES [dbo].[PublishingHouses]
        ([ID_PH])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [CompetitionID_COMP] in table 'Organizations'
ALTER TABLE [dbo].[Organizations]
ADD CONSTRAINT [FK_CompetitionOrganize]
    FOREIGN KEY ([CompetitionID_COMP])
    REFERENCES [dbo].[Competitions]
        ([ID_COMP])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CompetitionOrganize'
CREATE INDEX [IX_FK_CompetitionOrganize]
ON [dbo].[Organizations]
    ([CompetitionID_COMP]);
GO

-- Creating foreign key on [JuryMemberJMBG_SIN] in table 'HiredForSet'
ALTER TABLE [dbo].[HiredForSet]
ADD CONSTRAINT [FK_JuryMemberHiredFor]
    FOREIGN KEY ([JuryMemberJMBG_SIN])
    REFERENCES [dbo].[Users_JuryMember]
        ([JMBG_SIN])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [CompetitionID_COMP] in table 'HiredForSet'
ALTER TABLE [dbo].[HiredForSet]
ADD CONSTRAINT [FK_CompetitionHiredFor]
    FOREIGN KEY ([CompetitionID_COMP])
    REFERENCES [dbo].[Competitions]
        ([ID_COMP])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CompetitionHiredFor'
CREATE INDEX [IX_FK_CompetitionHiredFor]
ON [dbo].[HiredForSet]
    ([CompetitionID_COMP]);
GO

-- Creating foreign key on [CompetitionID_COMP] in table 'PossessesASet'
ALTER TABLE [dbo].[PossessesASet]
ADD CONSTRAINT [FK_CompetitionPossessesA]
    FOREIGN KEY ([CompetitionID_COMP])
    REFERENCES [dbo].[Competitions]
        ([ID_COMP])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [GenreID_GENRE] in table 'PossessesASet'
ALTER TABLE [dbo].[PossessesASet]
ADD CONSTRAINT [FK_GenrePossessesA]
    FOREIGN KEY ([GenreID_GENRE])
    REFERENCES [dbo].[Genres]
        ([ID_GENRE])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GenrePossessesA'
CREATE INDEX [IX_FK_GenrePossessesA]
ON [dbo].[PossessesASet]
    ([GenreID_GENRE]);
GO

-- Creating foreign key on [JuryMemberJMBG_SIN] in table 'IsExpertSet'
ALTER TABLE [dbo].[IsExpertSet]
ADD CONSTRAINT [FK_JuryMemberIsExpert]
    FOREIGN KEY ([JuryMemberJMBG_SIN])
    REFERENCES [dbo].[Users_JuryMember]
        ([JMBG_SIN])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [GenreID_GENRE] in table 'IsExpertSet'
ALTER TABLE [dbo].[IsExpertSet]
ADD CONSTRAINT [FK_GenreIsExpert]
    FOREIGN KEY ([GenreID_GENRE])
    REFERENCES [dbo].[Genres]
        ([ID_GENRE])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GenreIsExpert'
CREATE INDEX [IX_FK_GenreIsExpert]
ON [dbo].[IsExpertSet]
    ([GenreID_GENRE]);
GO

-- Creating foreign key on [GenreID_GENRE] in table 'MusicPerformances'
ALTER TABLE [dbo].[MusicPerformances]
ADD CONSTRAINT [FK_MusicPerformanceGenre]
    FOREIGN KEY ([GenreID_GENRE])
    REFERENCES [dbo].[Genres]
        ([ID_GENRE])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MusicPerformanceGenre'
CREATE INDEX [IX_FK_MusicPerformanceGenre]
ON [dbo].[MusicPerformances]
    ([GenreID_GENRE]);
GO

-- Creating foreign key on [IsExpertJuryMemberJMBG_SIN], [IsExpertGenreID_GENRE] in table 'Evaluations'
ALTER TABLE [dbo].[Evaluations]
ADD CONSTRAINT [FK_IsExpertEvaluate]
    FOREIGN KEY ([IsExpertJuryMemberJMBG_SIN], [IsExpertGenreID_GENRE])
    REFERENCES [dbo].[IsExpertSet]
        ([JuryMemberJMBG_SIN], [GenreID_GENRE])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [MusicPerformanceID_PERF] in table 'Evaluations'
ALTER TABLE [dbo].[Evaluations]
ADD CONSTRAINT [FK_MusicPerformanceEvaluate]
    FOREIGN KEY ([MusicPerformanceID_PERF])
    REFERENCES [dbo].[MusicPerformances]
        ([ID_PERF])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MusicPerformanceEvaluate'
CREATE INDEX [IX_FK_MusicPerformanceEvaluate]
ON [dbo].[Evaluations]
    ([MusicPerformanceID_PERF]);
GO

-- Creating foreign key on [OrganizePublishingHouseID_PH], [OrganizeCompetitionID_COMP] in table 'Reservations'
ALTER TABLE [dbo].[Reservations]
ADD CONSTRAINT [FK_OrganizeReserve]
    FOREIGN KEY ([OrganizePublishingHouseID_PH], [OrganizeCompetitionID_COMP])
    REFERENCES [dbo].[Organizations]
        ([PublishingHouseID_PH], [CompetitionID_COMP])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [PerformanceHallID_HALL] in table 'Reservations'
ALTER TABLE [dbo].[Reservations]
ADD CONSTRAINT [FK_PerformanceHallReserve]
    FOREIGN KEY ([PerformanceHallID_HALL])
    REFERENCES [dbo].[PerformanceHalls]
        ([ID_HALL])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PerformanceHallReserve'
CREATE INDEX [IX_FK_PerformanceHallReserve]
ON [dbo].[Reservations]
    ([PerformanceHallID_HALL]);
GO

-- Creating foreign key on [CompetitorJMBG_SIN] in table 'Competitings'
ALTER TABLE [dbo].[Competitings]
ADD CONSTRAINT [FK_CompetitorCompetiting]
    FOREIGN KEY ([CompetitorJMBG_SIN])
    REFERENCES [dbo].[Users_Competitor]
        ([JMBG_SIN])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [OrganizePublishingHouseID_PH], [OrganizeCompetitionID_COMP] in table 'Competitings'
ALTER TABLE [dbo].[Competitings]
ADD CONSTRAINT [FK_OrganizeCompetiting]
    FOREIGN KEY ([OrganizePublishingHouseID_PH], [OrganizeCompetitionID_COMP])
    REFERENCES [dbo].[Organizations]
        ([PublishingHouseID_PH], [CompetitionID_COMP])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrganizeCompetiting'
CREATE INDEX [IX_FK_OrganizeCompetiting]
ON [dbo].[Competitings]
    ([OrganizePublishingHouseID_PH], [OrganizeCompetitionID_COMP]);
GO

-- Creating foreign key on [CompetitingCompetitorJMBG_SIN], [CompetitingOrganizePublishingHouseID_PH], [CompetitingOrganizeCompetitionID_COMP] in table 'MusicPerformances'
ALTER TABLE [dbo].[MusicPerformances]
ADD CONSTRAINT [FK_CompetitingMusicPerformance]
    FOREIGN KEY ([CompetitingCompetitorJMBG_SIN], [CompetitingOrganizePublishingHouseID_PH], [CompetitingOrganizeCompetitionID_COMP])
    REFERENCES [dbo].[Competitings]
        ([CompetitorJMBG_SIN], [OrganizePublishingHouseID_PH], [OrganizeCompetitionID_COMP])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CompetitingMusicPerformance'
CREATE INDEX [IX_FK_CompetitingMusicPerformance]
ON [dbo].[MusicPerformances]
    ([CompetitingCompetitorJMBG_SIN], [CompetitingOrganizePublishingHouseID_PH], [CompetitingOrganizeCompetitionID_COMP]);
GO

-- Creating foreign key on [JMBG_SIN] in table 'Users_JuryMember'
ALTER TABLE [dbo].[Users_JuryMember]
ADD CONSTRAINT [FK_JuryMember_inherits_User]
    FOREIGN KEY ([JMBG_SIN])
    REFERENCES [dbo].[Users]
        ([JMBG_SIN])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [JMBG_SIN] in table 'Users_Competitor'
ALTER TABLE [dbo].[Users_Competitor]
ADD CONSTRAINT [FK_Competitor_inherits_User]
    FOREIGN KEY ([JMBG_SIN])
    REFERENCES [dbo].[Users]
        ([JMBG_SIN])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [JMBG_SIN] in table 'Users_Administrator'
ALTER TABLE [dbo].[Users_Administrator]
ADD CONSTRAINT [FK_Administrator_inherits_User]
    FOREIGN KEY ([JMBG_SIN])
    REFERENCES [dbo].[Users]
        ([JMBG_SIN])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [JMBG_SIN] in table 'Users_EventOrganizer'
ALTER TABLE [dbo].[Users_EventOrganizer]
ADD CONSTRAINT [FK_EventOrganizer_inherits_User]
    FOREIGN KEY ([JMBG_SIN])
    REFERENCES [dbo].[Users]
        ([JMBG_SIN])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------