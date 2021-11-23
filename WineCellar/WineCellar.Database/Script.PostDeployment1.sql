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
INSERT INTO dbo.Country
(Name) VALUES
('Nederland'),
('Frankrijk'),
('Griekenland'),
('Spanje'),
('Argentinia'),
('Italië'),
('Duitsland'),
('Zuid-Afrika'),
('Chili'),
('Oostenrijk');

INSERT INTO dbo.Note
(Name) VALUES
('Sterk'),
('Krachtig'),
('Pittig'),
('Duur');

INSERT INTO dbo.Type
(Name) VALUES
('Malbec'),
('Tempranillo'),
('Corvina'),
('Merlot'),
('Sauvignon Blanc'),
('Grüner Veltliner'),
('Chardonnay'),
('Carmenère');

INSERT INTO dbo.Wine
(Name,                                      Buy,    Sell,   Type,   Country,    Picture,    Year,   Content,    Alcohol,    Rating,     Description) VALUES
('Catena Malbec',                           11.99,  19.92,  1,      5,          'empty',    2019,   700,        12.5,       5,          'Beschrijving van de Catena Malbec'),
('Faustino V Rioja Reserva',                11.99,  15.86,  2,      4,          'empty',    2016,   500,        15.0,       3,          'Beschrijving van de Faustino V Rioja Reserva'),
('Cantina di Verona Valpolicella Ripasso',  10.65,  13.32,  3,      6,          'empty',    2019,   700,        10.0,       2,          'Beschrijving van de Cantina di Verona Valpolicella Ripasso'),
('Mucho Más Tinto',                         5.98,   23.99,  2,      4,          'empty',    2000,   1200,       10.0,       2,          'Beschrijving van de Mucho Más Tinto'),
('Valdivieso Merlot',                       5.99,   20.99,  4,      9,          'empty',    2020,   700,        12.5,       1,          'Beschrijving van de Valdivieso Merlot'),
('Bruce Jack',                              6.99,   23.99,  5,      8,          'empty',    2019,   1200,       17.5,       10,         'Beschrijving van de Bruce Jack'),
('Alamos Merlot',                           6.59,   10.99,  4,      5,          'empty',    2020,   700,        15.0,       5,          'Beschrijving van de Alamos Merlot'),
('Domaine Andau',                           7.99,   12.35,  6,      10,         'empty',    2019,   700,        12.5,       7,          'Beschrijving van de Domaine Andau'),
('La Palma',                                5.99,   9.50,   7,      9,          'empty',    2019,   500,        7.5,        9,          'Beschrijving van de La Palma (2019)'),
('La Palma',                                5.99,   10.20,  8,      9,          'empty',    2020,   500,        12.5,       8,          'Beschrijving van de La Palma (2020)');

INSERT INTO dbo.Wine_Note
(IdWine, IdNote) VALUES
(1, 1),
(1, 2),
(2, 2),
(2, 3),
(2, 4),
(3, 2),
(4, 4),
(5, 2),
(5, 4),
(6, 1),
(7, 2),
(7, 3),
(8, 3),
(9, 1),
(9, 3),
(10, 1),
(10, 2);

INSERT INTO dbo.StorageLocation
(IdWine, Shelf, Col, Row) VALUES
(1, 'A', 1, 2),
(1, 'A', 2, 2),
(1, 'A', 3, 2),
(1, 'A', 4, 2),
(1, 'A', 5, 2),
(2, 'B', 1, 3),
(2, 'B', 3, 3),
(2, 'B', 4, 3),
(3, 'A', 2, 4),
(3, 'A', 3, 4),
(3, 'A', 4, 4),
(3, 'A', 5, 4),
(3, 'A', 6, 4),
(3, 'A', 7, 4),
(4, 'D', 3, 5),
(4, 'D', 4, 5),
(5, 'A', 4, 6),
(5, 'A', 5, 6),
(6, 'A', 1, 7),
(6, 'A', 3, 7),
(6, 'A', 5, 7),
(6, 'A', 7, 7),
(6, 'A', 8, 7),
(6, 'A', 9, 7),
(6, 'A', 10, 7),
(7, 'E', 2, 8),
(8, 'B', 5, 9),
(9, 'A', 5, 10);