-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Erstellungszeit: 15. Mai 2023 um 14:57
-- Server-Version: 10.4.25-MariaDB
-- PHP-Version: 8.1.10

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

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

CREATE TABLE `essen` (
  `eid` int(11) NOT NULL,
  `bezeichnung` varchar(50) CHARACTER SET utf8 DEFAULT NULL,
  `preis` decimal(5,2) DEFAULT NULL,
  `bemerkung` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Daten für Tabelle `essen`
--

INSERT INTO `essen` (`eid`, `bezeichnung`, `preis`, `bemerkung`) VALUES
(1, 'Pizza Funghi', '8.89', 'mit Fliegenpilz'),
(2, 'Bulette', '4.99', 'veggi style - aus Erbsen'),
(3, 'Gemuesepfanne', '6.99', 'kann Spuren von Nuessen enthalten'),
(4, 'Creme brulee', '5.59', 'mit Raucharoma');

--
-- Indizes der exportierten Tabellen
--

--
-- Indizes für die Tabelle `essen`
--
ALTER TABLE `essen`
  ADD PRIMARY KEY (`eid`);

--
-- AUTO_INCREMENT für exportierte Tabellen
--

--
-- AUTO_INCREMENT für Tabelle `essen`
--
ALTER TABLE `essen`
  MODIFY `eid` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
