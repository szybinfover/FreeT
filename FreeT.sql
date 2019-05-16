IF (NOT EXISTS (SELECT *
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_NAME = 'Uzytkownik'))
BEGIN
    CREATE TABLE Uzytkownik(
ID BIGINT  NOT NULL IDENTITY(1,1) PRIMARY KEY,
login VARCHAR(50) UNIQUE NOT NULL,
imie VARCHAR(50),
nazwisko VARCHAR(200)  NOT NULL,
haslo VARCHAR(50) NOT NULL,
zespol_id BIGINT NOT NULL
);
END


IF (NOT EXISTS (SELECT *
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_NAME = 'Urlop'))
BEGIN
CREATE TABLE Urlop(
ID BIGINT  NOT NULL IDENTITY(1,1) PRIMARY KEY,
dataod DATE NOT NULL,
datado DATE NOT NULL,
uzytkownik_id BIGINT NOT NULL
);
END

IF (NOT EXISTS (SELECT *
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_NAME = 'Zespol'))
BEGIN
CREATE TABLE Zespol(
ID BIGINT NOT NULL IDENTITY(1,1) PRIMARY KEY,
nazwa VARCHAR(50) UNIQUE NOT NULL,
);
END


INSERT INTO Zespol(nazwa) VALUES ('Zespol1')
INSERT INTO Zespol(nazwa) VALUES ('Zespol2')


INSERT INTO Uzytkownik(login,imie,nazwisko,haslo,zespol_id) VALUES('Login1','Jan','Kowalski','password', '1')
INSERT INTO Uzytkownik(login,imie,nazwisko,haslo,zespol_id) VALUES('Login2','Tomasz','Krab','password', '1')
INSERT INTO Uzytkownik(login,imie,nazwisko,haslo,zespol_id) VALUES('Login3','Łukasz','Nowak','password', '2')

INSERT INTO Urlop(dataod,datado,uzytkownik_id) VALUES('2019-05-20','2019-05-26','1')
INSERT INTO Urlop(dataod,datado,uzytkownik_id) VALUES('2019-05-27','2019-06-02','2')
INSERT INTO Urlop(dataod,datado,uzytkownik_id) VALUES('2019-06-10','2019-06-16','3')


SELECT * FROM Uzytkownik;
SELECT * FROM Urlop;
SELECT * FROM Zespol;

-- Hash MD5 do hasła:password
--5F4DCC3B5AA765D61D8327DEB882CF99

--DROP TABLE Uzytkownik; DROP TABLE Urlop;DROP TABLE Zespol;