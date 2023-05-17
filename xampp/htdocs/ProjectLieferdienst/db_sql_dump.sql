-- login
-- mysql -u root
--
-- Datenbank: `lieferdienst`
--
DROP DATABASE IF EXISTS `lieferdienst`;
CREATE DATABASE IF NOT EXISTS `lieferdienst` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE `lieferdienst`;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `essen`
--
CREATE TABLE essen(
    eid INT AUTO_INCREMENT PRIMARY KEY,
    bezeichnung VARCHAR(50),
    preis DECIMAL(5,2),
    info VARCHAR(70)
);
CREATE TABLE `essen` (
  `eid` int(11) NOT NULL,
  `bezeichnung` varchar(50) CHARACTER SET utf8 DEFAULT NULL,
  `preis` decimal(5,2) DEFAULT NULL,
  `bemerkung` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
-- mukup data für Tabelle `essen`
INSERT INTO essen (bezeichnung, preis, info) VALUES ("Pizza Fungi", 8.89, "Mit pilzen");
INSERT INTO essen (bezeichnung, preis, info) VALUES ("Pizza Tonno", 9.99, "veggy");
INSERT INTO essen (bezeichnung, preis, info) VALUES ("Boulette", 3.20, "");
INSERT INTO essen (bezeichnung, preis, info) VALUES ("Gemuesepfanne", 6.45, "veggy, kann spuren von Nuessen enthalten");
INSERT INTO essen (bezeichnung, preis, info) VALUES ("Creme Catalan", 5.59, "kalorienbombe");

--
-- Tabellenstruktur für Tabelle `bestellung`
--

CREATE TABLE bestellung (
    bid INT AUTO_INCREMENT PRIMARY KEY,
    datum DATETIME,
    eid INT NOT NULL,
    anzahl INT UNSIGNED,
    FOREIGN KEY (eid) REFERENCES essen (eid)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
-- mokup data for previous orders
INSERT INTO bestellung (datum, eid, anzahl) values (now(), 2 , 3);


--
-- setup our user if he doesnt already exist:
--
CREATE USER ronny@localhost IDENTIFIED BY "1234";
GRANT SELECT ON lieferdienst.essen TO ronny@localhost;
GRANT SELECT,INSERT ON lieferdienst.bestellung TO ronny@localhost;
