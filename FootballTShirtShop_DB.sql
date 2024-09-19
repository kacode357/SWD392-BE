USE [master]
GO

IF DB_ID('FootballTShirtShop_DB') IS NOT NULL
BEGIN
    --DROP DATABASE [FootballTShirtShop_DB]
	ALTER DATABASE [FootballTShirtShop_DB] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE [FootballTShirtShop_DB];
END
GO
CREATE DATABASE [FootballTShirtShop_DB]
GO

USE [FootballTShirtShop_DB]
GO


CREATE TABLE [User] 
(
    Id INT IDENTITY(1,1) NOT NULL,
    UserName NVARCHAR(50) NOT NULL,
    [Password] NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    DOB DATE NOT NULL,
    [Address] NVARCHAR(100) NOT NULL,
    Phone_Number NVARCHAR(50) NOT NULL,
    [RoleName] NVARCHAR(50) NOT NULL,
    [Gender] NVARCHAR(50) NOT NULL,
    [ImgURL] NVARCHAR(MAX) NOT NULL,
    Created_Date DATE NOT NULL,
    Modified_Date DATE NULL,
    [Rating_Count] INT NULL,
    [Status] BIT NOT NULL,
PRIMARY KEY CLUSTERED ([Id] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
GO


CREATE TABLE [Club] 
(
    Id INT IDENTITY(1,1) NOT NULL,
    [Name] NVARCHAR(100) NOT NULL,
	[Country] NVARCHAR(100) NOT NULL,
	[EstablishedYear] DATETIME NOT NULL,
	[StadiumName] NVARCHAR(100) NOT NULL,
	[ClubLogo] NVARCHAR(100) NOT NULL,
    [Description] NVARCHAR(MAX) NULL,
    [Status] BIT NOT NULL, 
PRIMARY KEY CLUSTERED ([Id] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
GO

CREATE TABLE [Player] (
    Id INT IDENTITY(1,1),
    FullName NVARCHAR(50) NOT NULL,
	height FlOAT NULL,
    [weight] FlOAT NULL,
    Birthday DATE NULL,
    Nationality NVARCHAR(50) NULL,
PRIMARY KEY CLUSTERED ([Id] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
GO

CREATE TABLE [Session] 
(
    Id INT IDENTITY(1,1) NOT NULL,
    [Name] NVARCHAR(100) NOT NULL,
	[StartDdate] DATETIME NOT NULL,
	[EndDdate] DATETIME NOT NULL,
    [Description] NVARCHAR(MAX) NULL,
    [Status] BIT NOT NULL, 
PRIMARY KEY CLUSTERED ([Id] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
GO

CREATE TABLE [Model] 
(
    Id INT IDENTITY(1,1) NOT NULL,
	[Session_Id] INT NOT NULL,
	[Club_Id] INT NOT NULL,
    [Name] NVARCHAR(100) NOT NULL,
    [Description] NVARCHAR(MAX) NULL,
    [Status] BIT NOT NULL, 
FOREIGN KEY ([Session_Id]) REFERENCES [Session] ([Id]),
FOREIGN KEY ([Club_Id]) REFERENCES [Club] ([Id]),
PRIMARY KEY CLUSTERED ([Id] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
GO


CREATE TABLE [Shirt] 
(
    Id INT IDENTITY(1,1) NOT NULL,
    [Model_Id] INT NOT NULL,
	[Player_Id] INT NOT NULL,
    [Name] NVARCHAR(100) NOT NULL,
	[Number] INT NULL,
	[Quantity] INT NULL,
    [Price] FLOAT NOT NULL,
	[Date] DATETIME NOT NULL,
    [Description] NVARCHAR(MAX) NULL,
    [Url_IMG] NVARCHAR(MAX) NULL, 
    [Status] INT NOT NULL, 
FOREIGN KEY ([Player_Id]) REFERENCES [Player] ([Id]),
FOREIGN KEY ([Model_Id]) REFERENCES [Model]([Id]),
PRIMARY KEY CLUSTERED ([Id] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
GO


CREATE TABLE [Notification] 
(
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [User_Id] INT NOT NULL,
    [Content] NVARCHAR(MAX) NOT NULL,
FOREIGN KEY ([User_Id]) REFERENCES [User] ([Id]),
) ON [PRIMARY];
GO

CREATE TABLE [Order] 
(
    Id INT IDENTITY(1,1) NOT NULL,
    [User_Id] INT NOT NULL,
    [Total_Price] FLOAT NOT NULL,
	[ShipPrice] FLOAT NOT NULL,
	[Deposit] FLOAT NOT NULL,
    [Date] DATE NOT NULL,
	[RefundStatus] BIT NOT NULL,
    [Status] INT NOT NULL,
FOREIGN KEY ([User_Id]) REFERENCES [User] ([Id]),
PRIMARY KEY CLUSTERED ([Id] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
GO

CREATE TABLE [Payment] 
(
    [Id] INT IDENTITY(1,1) NOT NULL,
	[User_Id] INT NOT NULL,
	[Order_Id] INT NOT NULL,
    [Date] NVARCHAR(MAX) NOT NULL,
    [Amount] FLOAT NOT NULL,
    [Method] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(MAX) NULL, 
    [Status] BIT NOT NULL, 
FOREIGN KEY ([Order_Id]) REFERENCES [Order]([Id]),
PRIMARY KEY CLUSTERED ([Id] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
GO

CREATE TABLE [OrderDetails] 
(
    Id INT IDENTITY(1,1) NOT NULL,
    [Order_Id] INT NOT NULL,
    [Shirt_Id] INT NOT NULL,
	[Quantity] INT NOT NULL,
    [Price] FLOAT NOT NULL,
	[Status_Rating] BIT NOT NULL,
	[Comment] NVARCHAR(MAX) NULL,
	[Score] INT NULL,
    [Status] BIT NOT NULL,
FOREIGN KEY ([Shirt_Id]) REFERENCES [Shirt]([Id]),
FOREIGN KEY ([Order_Id]) REFERENCES [Order] ([Id]),
PRIMARY KEY CLUSTERED ([Id] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
GO

CREATE TABLE [PlayerClub] 
(
    Id INT IDENTITY(1,1) NOT NULL,
    [Player_Id] INT NOT NULL,
    [Club_Id] INT NOT NULL,
    [StartDate] DATE NOT NULL,
    [EndDate] DATE NULL,
    [Status] NVARCHAR(50) NOT NULL,
FOREIGN KEY ([Player_Id]) REFERENCES [Player] ([Id]),
FOREIGN KEY ([Club_Id]) REFERENCES [Club] ([Id]),
PRIMARY KEY CLUSTERED ([Id] ASC)
) ON [PRIMARY];
GO


INSERT INTO  [Club] ([Name], [Country], [EstablishedYear], [StadiumName], [ClubLogo], [Description], [Status])
VALUES 
('Manchester City', 'England', '1880-11-23', 'Etihad Stadium', 'man_city_logo.png', 'Manchester City has become a dominant force in English football in recent years.', 1),
('FC Barcelona', 'Spain', '1899-11-29', 'Camp Nou', 'barcelona_logo.png', 'Barcelona is one of the most successful clubs in Spain.', 1),
('Real Madrid', 'Spain', '1902-03-06', 'Santiago Bernabeu', 'real_madrid_logo.png', 'Real Madrid has won the most European Cups.', 1),
('Manchester United', 'England', '1878-01-01', 'Old Trafford', 'man_utd_logo.png', 'One of the most successful English clubs.', 1),
('Bayern Munich', 'Germany', '1900-02-27', 'Allianz Arena', 'bayern_munich_logo.png', 'The most successful club in Germany.', 1),
('Paris Saint-Germain', 'France', '1970-08-12', 'Parc des Princes', 'psg_logo.png', 'PSG is one of the top clubs in France.', 1),
('Juventus', 'Italy', '1897-11-01', 'Allianz Stadium', 'juventus_logo.png', 'Juventus has dominated Italian football.', 1),
('Chelsea', 'England', '1905-03-10', 'Stamford Bridge', 'chelsea_logo.png', 'Chelsea is one of the top English clubs.', 1),
('Liverpool', 'England', '1892-06-03', 'Anfield', 'liverpool_logo.png', 'Liverpool has a rich history in both English and European football.', 1),
('Arsenal', 'England', '1886-10-01', 'Emirates Stadium', 'arsenal_logo.png', 'Arsenal is a historic club in North London.', 1),
('AC Milan', 'Italy', '1899-12-16', 'San Siro', 'ac_milan_logo.png', 'AC Milan has won numerous European trophies.', 1),
('Atletico Madrid', 'Spain', '1903-04-26', 'Wanda Metropolitano', 'atletico_madrid_logo.png', 'Atletico Madrid is known for its competitive spirit.', 1),
('Sevilla FC', 'Spain', '1890-01-25', 'Ramon Sanchez-Pizjuan', 'sevilla_logo.png', 'Sevilla has a strong record in European competitions.', 1),
('Valencia CF', 'Spain', '1919-03-18', 'Mestalla', 'valencia_logo.png', 'Valencia is a historic Spanish club.', 1),
('Villarreal CF', 'Spain', '1923-03-10', 'Estadio de la Ceramica', 'villarreal_logo.png', 'Villarreal is a rising power in Spain.', 1),
('Real Sociedad', 'Spain', '1909-09-07', 'Anoeta', 'real_sociedad_logo.png', 'Real Sociedad is a Basque club with a proud history.', 1),
('Athletic Bilbao', 'Spain', '1898-07-20', 'San Mames', 'athletic_bilbao_logo.png', 'Athletic Bilbao has a tradition of fielding only Basque players.', 1),
('Tottenham Hotspur', 'England', '1882-09-05', 'Tottenham Hotspur Stadium', 'tottenham_logo.png', 'Tottenham is a top club in North London.', 1),
('Leicester City', 'England', '1884-11-01', 'King Power Stadium', 'leicester_logo.png', 'Leicester won the Premier League in 2016 in a fairy-tale season.', 1),
('Everton', 'England', '1878-03-01', 'Goodison Park', 'everton_logo.png', 'Everton is one of England"s oldest and most storied clubs.', 1),
('West Ham United', 'England', '1895-06-29', 'London Stadium', 'west_ham_logo.png', 'West Ham is known for its passionate fan base.', 1),
('AS Roma', 'Italy', '1927-07-22', 'Stadio Olimpico', 'as_roma_logo.png', 'Roma is one of the top clubs in Italy.', 1),
('Inter Milan', 'Italy', '1908-03-09', 'San Siro', 'inter_milan_logo.png', 'Inter Milan has a rich history in both domestic and European football.', 1),
('Napoli', 'Italy', '1926-08-01', 'Stadio Diego Armando Maradona', 'napoli_logo.png', 'Napoli is a proud club in southern Italy.', 1),
('Lazio', 'Italy', '1900-01-09', 'Stadio Olimpico', 'lazio_logo.png', 'Lazio is a major club in Rome.', 1),
('Fiorentina', 'Italy', '1926-08-26', 'Stadio Artemio Franchi', 'fiorentina_logo.png', 'Fiorentina is a strong club in Italian football.', 1),
('Borussia Dortmund', 'Germany', '1909-12-19', 'Signal Iduna Park', 'dortmund_logo.png', 'Borussia Dortmund is known for its vibrant fan base and competitive play.', 1),
('RB Leipzig', 'Germany', '2009-05-19', 'Red Bull Arena', 'rb_leipzig_logo.png', 'RB Leipzig is a modern powerhouse in German football.', 1),
('Bayer Leverkusen', 'Germany', '1904-07-01', 'BayArena', 'leverkusen_logo.png', 'Leverkusen is consistently competitive in the Bundesliga.', 1),
('Schalke 04', 'Germany', '1904-05-04', 'Veltins-Arena', 'schalke_logo.png', 'Schalke has a passionate following in German football.', 1),
('Eintracht Frankfurt', 'Germany', '1899-03-08', 'Deutsche Bank Park', 'frankfurt_logo.png', 'Eintracht Frankfurt has a proud history in Germany.', 1),
('Olympique Lyonnais', 'France', '1950-08-03', 'Groupama Stadium', 'lyon_logo.png', 'Lyon is one of the top clubs in France.', 1),
('Olympique de Marseille', 'France', '1899-08-31', 'Stade Velodrome', 'marseille_logo.png', 'Marseille is a historic club in French football.', 1),
('AS Monaco', 'France', '1919-08-23', 'Stade Louis II', 'monaco_logo.png', 'Monaco is known for developing young talent.', 1),
('Lille OSC', 'France', '1944-09-23', 'Stade Pierre-Mauroy', 'lille_logo.png', 'Lille has become a rising power in French football.', 1),
('Rennes', 'France', '1901-03-10', 'Roazhon Park', 'rennes_logo.png', 'Rennes is a historic club in Brittany.', 1),
('Benfica', 'Portugal', '1904-02-28', 'Estadio da Luz', 'benfica_logo.png', 'Benfica is one of the most successful clubs in Portugal.', 1),
('FC Porto', 'Portugal', '1893-09-28', 'Estadio do Dragao', 'porto_logo.png', 'Porto is a dominant force in Portuguese football.', 1),
('Sporting CP', 'Portugal', '1906-07-01', 'Estadio Jose Alvalade', 'sporting_logo.png', 'Sporting is known for developing top talents.', 1),
('Ajax', 'Netherlands', '1900-03-18', 'Johan Cruyff Arena', 'ajax_logo.png', 'Ajax is a powerhouse in Dutch football.', 1),
('PSV Eindhoven', 'Netherlands', '1913-08-31', 'Philips Stadion', 'psv_logo.png', 'PSV Eindhoven is a major club in the Netherlands.', 1),
('Feyenoord', 'Netherlands', '1908-07-19', 'De Kuip', 'feyenoord_logo.png', 'Feyenoord is a historic club in Rotterdam.', 1),
('Rangers FC', 'Scotland', '1872-03-15', 'Ibrox Stadium', 'rangers_logo.png', 'Rangers is one of the top clubs in Scotland.', 1),
('Celtic FC', 'Scotland', '1888-11-06', 'Celtic Park', 'celtic_logo.png', 'Celtic is known for its dominance in Scottish football.', 1),
('Zenit St. Petersburg', 'Russia', '1925-05-25', 'Gazprom Arena', 'zenit_logo.png', 'Zenit is the top club in Russian football.', 1),
('CSKA Moscow', 'Russia', '1911-08-27', 'VEB Arena', 'cska_moscow_logo.png', 'CSKA Moscow is a traditional power in Russian football.', 1),
('Spartak Moscow', 'Russia', '1922-04-18', 'Otkritie Arena', 'spartak_moscow_logo.png', 'Spartak Moscow has a rich history in Russia.', 1),
('Shakhtar Donetsk', 'Ukraine', '1936-05-24', 'Donbass Arena', 'shakhtar_logo.png', 'Shakhtar Donetsk is the top club in Ukrainian football.', 1),
('Dynamo Kyiv', 'Ukraine', '1927-04-13', 'NSC Olimpiyskiy', 'dynamo_kyiv_logo.png', 'Dynamo Kyiv is a legendary club in Ukraine.', 1),
('Galatasaray', 'Turkey', '1905-10-20', 'Nef Stadium', 'galatasaray_logo.png', 'Galatasaray is one of the biggest clubs in Turkey.', 1),
('Fenerbahce', 'Turkey', '1907-05-03', 'Sukru Saracoglu Stadium', 'fenerbahce_logo.png', 'Fenerbahce is a top club in Turkish football.', 1),
('Besiktas', 'Turkey', '1903-03-19', 'Vodafone Park', 'besiktas_logo.png', 'Besiktas is a historic club in Turkey.', 1),
('Inter Miami', 'USA', '2018-01-28', 'DRV PNK Stadium', 'link-to-logo-inter-miami', 'Inter Miami is an American professional soccer team based in Miami, Florida.', 1),
('Al-Nassr', 'Saudi Arabia', '1955-10-24', 'Mrsool Park', 'link-to-logo-al-nassr', 'Al-Nassr FC is a Saudi Arabian professional football club based in Riyadh.', 1),
('Sporting CP', 'Portugal', '1906-07-01', 'Estádio José Alvalade', 'sporting_cp_logo.png', 'Sporting CP is one of the top football clubs in Portugal, known for its youth academy and strong domestic performance.', 1);
GO


INSERT INTO [Player] ([FullName], [height], [weight], [Birthday], [Nationality])
VALUES
('Lionel Messi', 1.70, 72, '1987-06-24', 'Argentina'),
('Cristiano Ronaldo', 1.87, 83, '1985-02-05', 'Portugal'),
('Kylian Mbappé', 1.78, 73, '1998-12-20', 'France'),
('Robert Lewandowski', 1.85, 80, '1988-08-21', 'Poland'),
('Kevin De Bruyne', 1.81, 68, '1991-06-28', 'Belgium'),
('Virgil van Dijk', 1.93, 92, '1991-07-08', 'Netherlands'),
('Mohamed Salah', 1.75, 71, '1992-06-15', 'Egypt'),
('Sergio Ramos', 1.84, 82, '1986-03-30', 'Spain'),
('Neymar Jr', 1.75, 68, '1992-02-05', 'Brazil'),
('Luka Modrić', 1.72, 66, '1985-09-09', 'Croatia'),
('Gianluigi Donnarumma', 1.96, 90, '1999-02-25', 'Italy'),
('Harry Kane', 1.88, 89, '1993-07-28', 'England'),
('Joshua Kimmich', 1.76, 70, '1995-02-08', 'Germany'),
('Paulo Dybala', 1.81, 74, '1993-11-15', 'Argentina'),
('Son Heung-min', 1.83, 77, '1992-07-08', 'South Korea'),
('Marco Verratti', 1.65, 60, '1992-11-05', 'Italy'),
('Jorginho', 1.80, 70, '1991-12-20', 'Brazil'),
('Frenkie de Jong', 1.80, 77, '1997-05-12', 'Netherlands'),
('João Félix', 1.81, 70, '1999-11-10', 'Portugal'),
('Andreas Christensen', 1.87, 82, '1996-04-10', 'Denmark'),
('Koke', 1.80, 78, '1992-01-08', 'Spain'),
('Edouard Mendy', 1.97, 90, '1992-03-01', 'Senegal'),
('Dusan Vlahovic', 1.90, 85, '2000-01-28', 'Serbia'),
('Ciro Immobile', 1.85, 77, '1990-02-20', 'Italy'),
('Karim Benzema', 1.85, 81, '1987-12-19', 'France'),
('Christian Pulisic', 1.73, 70, '1998-09-18', 'United States'),
('Gareth Bale', 1.83, 82, '1989-07-16', 'Wales'),
('Sadio Mané', 1.75, 69, '1992-04-10', 'Senegal'),
('Dani Carvajal', 1.73, 73, '1992-01-11', 'Spain'),
('Angel Di María', 1.80, 76, '1988-02-14', 'Argentina'),
('Leon Goretzka', 1.89, 85, '1995-02-06', 'Germany'),
('Gerard Moreno', 1.77, 72, '1992-04-07', 'Spain'),
('Bruno Fernandes', 1.79, 73, '1994-09-08', 'Portugal'),
('Jadon Sancho', 1.80, 75, '2000-03-25', 'England'),
('Rodri', 1.91, 84, '1996-06-22', 'Spain'),
('Mason Mount', 1.78, 70, '1999-01-10', 'England'),
('Luis Suárez', 1.82, 86, '1987-01-24', 'Uruguay'),
('Nicolò Barella', 1.71, 70, '1997-02-07', 'Italy'),
('Ben Yedder', 1.73, 68, '1990-03-12', 'France'),
('Wilfred Ndidi', 1.83, 76, '1996-12-16', 'Nigeria'),
('Houssem Aouar', 1.76, 68, '1998-06-30', 'France'),
('Leonardo Bonucci', 1.90, 85, '1987-05-01', 'Italy'),
('Marc-André ter Stegen', 1.87, 85, '1992-04-30', 'Germany'),
('Lucas Hernandez', 1.82, 80, '1996-02-14', 'France'),
('Martin Ødegaard', 1.78, 76, '1998-12-17', 'Norway'),
('Toni Kroos', 1.83, 76, '1990-01-04', 'Germany'),
('Ricardo Pereira', 1.80, 75, '1993-10-06', 'Portugal'),
('Matthijs de Ligt', 1.89, 89, '1999-08-12', 'Netherlands'),
('José Giménez', 1.85, 80, '1995-01-20', 'Uruguay'),
('Kieran Trippier', 1.78, 75, '1990-09-19', 'England'),
('Cengiz Ünder', 1.75, 70, '1997-07-14', 'Turkey'),
('Hakan Çalhanoğlu', 1.81, 74, '1993-02-08', 'Turkey'),
('Mikel Oyarzabal', 1.82, 76, '1997-04-21', 'Spain'),
('Alphonso Davies', 1.82, 70, '2000-11-02', 'Canada'),
('Danilo Pereira', 1.87, 83, '1991-09-09', 'Portugal'),
('Lautaro Martínez', 1.74, 70, '1997-08-22', 'Argentina'),
('João Cancelo', 1.82, 80, '1994-05-27', 'Portugal'),
('Ben White', 1.82, 76, '1997-10-08', 'England'),
('Yuri Berchiche', 1.80, 72, '1989-03-16', 'Spain'),
('Serge Gnabry', 1.75, 75, '1995-07-14', 'Germany'),
('Fikayo Tomori', 1.85, 76, '1997-12-19', 'England'),
('Dani Olmo', 1.77, 70, '1998-05-07', 'Spain'),
('Julian Brandt', 1.84, 80, '1996-05-02', 'Germany'),
('Luis Alberto', 1.79, 72, '1992-09-29', 'Spain'),
('Ivan Rakitić', 1.84, 78, '1988-03-10', 'Croatia'),
('Nico Elvedi', 1.89, 80, '1996-09-30', 'Switzerland'),
('Andrey Arshavin', 1.71, 68, '1981-05-29', 'Russia'),
('Gerson', 1.82, 76, '1997-05-20', 'Brazil'),
('Ederson', 1.88, 83, '1993-08-17', 'Brazil'),
('Antoine Griezmann', 1.76, 73, '1991-03-21', 'France'),
('Yannick Carrasco', 1.80, 78, '1993-09-04', 'Belgium'),
('Jules Koundé', 1.78, 73, '1998-11-12', 'France');
GO

INSERT INTO [PlayerClub] (Player_Id, Club_Id, StartDate, EndDate, Status)
VALUES 
-- Lionel Messi
(1, 2, '2000-07-01', '2021-06-30', 0),	--Barca
(1, 6, '2021-08-10', '2023-06-30', 0),	--PSG
(1, 54, '2023-07-15', NULL, 1),			--Inter Miami
-- Cristiano Ronaldo
(2, 39, '2002-08-01', '2023-08-11', 0),	--Sporting CP
(2, 4, '2003-08-12', '2009-07-05', 0),	--MU
(2, 3, '2009-07-01', '2018-06-30', 0),	--Real Madrid
(2, 7, '2018-07-10', '2021-08-31', 0),	--Juventus
(2, 4, '2021-08-27', '2022-11-22', 0),	--MU
(2, 55, '2022-12-30', NULL, 1),			--Al-Nassr
-- Kylian Mbappé
(3, 34, '2015-07-01', '2016-02-20', 0), --AS Monaco
(3, 6, '2017-07-01', '2024-05-10', 0),	--PSG
(3, 3, '2024-09-01', NULL, 1),			--Real Madrid
-- Robert Lewandowski
(4, 27, '2010-07-01', '2014-06-30', 0),	--Dortmund
(4, 5, '2014-07-01', '2022-07-16', 0),  --Bayern
(4, 2, '2022-07-16', NULL, 1),			--Barca
-- Kevin De Bruyne
(5, 8, '2012-02-01', '2014-01-31', 0),	--Chelsea
(5, 1, '2015-08-30', NULL, 1),			--Manchester City
-- Virgil van Dijk
(6, 8, '2015-07-01', NULL, 1),			--Liverpool
-- Mohamed Salah
(7, 9, '2017-07-01', NULL, 1),			--Liverpool
-- Sergio Ramos
(8, 12, '2005-07-01', '2021-06-30', 0), --Real
-- Neymar Jr
(9, 6, '2017-08-03', '2023-08-15', 0),  --PSG
-- Luka Modric
(10, 13, '2012-08-27', NULL, 1);		--Real
GO