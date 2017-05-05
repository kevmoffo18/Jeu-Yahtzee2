USE master
GO

DROP DATABASE bd_Yahtzee
PRINT 'Suppression de la base de données bd_Yahtzee'
GO

CREATE DATABASE bd_Yahtzee
PRINT 'Création de la base de données bd_Yahtzee'
GO


USE bd_Yahtzee
GO

CREATE TABLE tblDe
(
idDe INT PRIMARY KEY IDENTITY(1,1),
nomDe VARCHAR(20),
)

PRINT 'Création de la table tblDe'

CREATE TABLE tblFace
(
idLance INT PRIMARY KEY IDENTITY(1,1),
nbLancee  INT,
idDe INT
FOREIGN KEY(idDe) REFERENCES tblDe(idDe)
)

PRINT 'Création de la table tblLance'

INSERT tblDe VALUES('D1')
INSERT tblDe VALUES('D2')
INSERT tblDe VALUES('D3')
INSERT tblDe VALUES('D4')
INSERT tblDe VALUES('D5')

PRINT 'Insertion dans la table tblDe'