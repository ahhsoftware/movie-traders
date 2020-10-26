/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
:r .\Seed\StateSeed.sql
:r .\Seed\CountySeed.sql
:r .\Seed\GenreSeed.sql
:r .\Seed\FormatSeed.sql
:r .\Seed\TradeStatusSeed.sql
:r .\Seed\UserSeed.sql