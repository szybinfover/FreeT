CREATE TABLE Uzytkownik(
ID BIGINT  NOT NULL IDENTITY(1,1) PRIMARY KEY,
login VARCHAR(50) UNIQUE NOT NULL,
imie VARCHAR(50),
nazwisko VARCHAR(200)  NOT NULL,
haslo VARCHAR(50) NOT NULL,
);

CREATE TABLE Urlop(
ID BIGINT  NOT NULL IDENTITY(1,1) PRIMARY KEY,
dataod DATE NOT NULL,
datado DATE NOT NULL,
uzytkownik_id BIGINT NOT NULL
);




INSERT INTO Uzytkownik(login,imie,nazwisko,haslo) VALUES('Login1','Jan','Kowalski','5F4DCC3B5AA765D61D8327DEB882CF99')
INSERT INTO Uzytkownik(login,imie,nazwisko,haslo) VALUES('Login2','Tomasz','Krab','5F4DCC3B5AA765D61D8327DEB882CF99')
INSERT INTO Uzytkownik(login,imie,nazwisko,haslo) VALUES('Login3','Łukasz','Nowak','5F4DCC3B5AA765D61D8327DEB882CF99')

INSERT INTO Urlop(dataod,datado,uzytkownik_id) VALUES('2019-05-20','2019-05-26','1')
INSERT INTO Urlop(dataod,datado,uzytkownik_id) VALUES('2019-05-27','2019-06-02','2')
INSERT INTO Urlop(dataod,datado,uzytkownik_id) VALUES('2019-06-10','2019-06-16','3')


SELECT * FROM Uzytkownik;
SELECT * FROM Urlop;

-- Hash MD5 do hasła:password
--5F4DCC3B5AA765D61D8327DEB882CF99