-- phpMyAdmin SQL Dump
-- version 4.9.5deb2
-- https://www.phpmyadmin.net/
--
-- Host: mysql.ansatt.ntnu.no
-- Generation Time: May 09, 2025 at 10:20 AM
-- Server version: 8.0.42-0ubuntu0.22.04.1
-- PHP Version: 7.4.3-4ubuntu2.29

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `olavik_ArealUtstyr`
--

-- --------------------------------------------------------

--
-- Table structure for table `Bygning`
--

CREATE TABLE `Bygning` (
  `IDBygning` int NOT NULL,
  `BygningNavn` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `Bygning`
--

INSERT INTO `Bygning` (`IDBygning`, `BygningNavn`) VALUES
(1, 'Nevro Øst');

-- --------------------------------------------------------

--
-- Table structure for table `KategoriUtstyr`
--

CREATE TABLE `KategoriUtstyr` (
  `IDKategori` int NOT NULL,
  `Kategori` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `KategoriUtstyr`
--

INSERT INTO `KategoriUtstyr` (`IDKategori`, `Kategori`) VALUES
(1, 'Bygningsfast'),
(2, 'Medisinsk teknisk');

-- --------------------------------------------------------

--
-- Table structure for table `Romoversikt`
--

CREATE TABLE `Romoversikt` (
  `Romnr` varchar(15) NOT NULL,
  `IDBygning` int NOT NULL COMMENT 'Fremmednøkkel ID i tabell Bygning',
  `Romtype` text NOT NULL,
  `AntallArbeidsplasser` int NOT NULL,
  `Areal (Netto)` float NOT NULL,
  `Cleandesk` tinyint(1) DEFAULT NULL,
  `GjennomsnittligKvadratmeterpris` float NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Table structure for table `SLALeverandoer`
--

CREATE TABLE `SLALeverandoer` (
  `IDSLA` int NOT NULL,
  `SLALeverandoer` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Kontaktperson` text,
  `Epost` text
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `SLALeverandoer`
--

INSERT INTO `SLALeverandoer` (`IDSLA`, `SLALeverandoer`, `Kontaktperson`, `Epost`) VALUES
(1, 'St.Ola', 'Ola Nordmann', 'ola.nordmann@ntnu.no');

-- --------------------------------------------------------

--
-- Table structure for table `UnderkategoriUtstyr`
--

CREATE TABLE `UnderkategoriUtstyr` (
  `IDUnderkategori` int NOT NULL,
  `IDKategori` int NOT NULL COMMENT 'Fremmednøkkel ID Kategori',
  `UnderKategori` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Table structure for table `Utstyr`
--

CREATE TABLE `Utstyr` (
  `IDUtstyr` varchar(15) NOT NULL,
  `Romnr` varchar(15) NOT NULL,
  `IDUnderkategori` int NOT NULL,
  `IDSLA` int NOT NULL,
  `Innkjoepsdato` date NOT NULL,
  `LevetidAar` int DEFAULT NULL,
  `DatoAvskrevet` date DEFAULT NULL,
  `DatoSLA` date DEFAULT NULL,
  `DatoSistVedlikehold` date DEFAULT NULL,
  `UtloepDatoLevGaranti` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `Bygning`
--
ALTER TABLE `Bygning`
  ADD PRIMARY KEY (`IDBygning`) USING BTREE;

--
-- Indexes for table `KategoriUtstyr`
--
ALTER TABLE `KategoriUtstyr`
  ADD PRIMARY KEY (`IDKategori`);

--
-- Indexes for table `Romoversikt`
--
ALTER TABLE `Romoversikt`
  ADD PRIMARY KEY (`Romnr`),
  ADD KEY `IDBygning` (`IDBygning`);

--
-- Indexes for table `SLALeverandoer`
--
ALTER TABLE `SLALeverandoer`
  ADD PRIMARY KEY (`IDSLA`);

--
-- Indexes for table `UnderkategoriUtstyr`
--
ALTER TABLE `UnderkategoriUtstyr`
  ADD PRIMARY KEY (`IDUnderkategori`),
  ADD KEY `IDKategori` (`IDKategori`);

--
-- Indexes for table `Utstyr`
--
ALTER TABLE `Utstyr`
  ADD PRIMARY KEY (`IDUtstyr`),
  ADD KEY `Romnr` (`Romnr`),
  ADD KEY `IDSLA` (`IDSLA`),
  ADD KEY `IDUnderkategori` (`IDUnderkategori`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `Bygning`
--
ALTER TABLE `Bygning`
  MODIFY `IDBygning` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `KategoriUtstyr`
--
ALTER TABLE `KategoriUtstyr`
  MODIFY `IDKategori` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `SLALeverandoer`
--
ALTER TABLE `SLALeverandoer`
  MODIFY `IDSLA` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `UnderkategoriUtstyr`
--
ALTER TABLE `UnderkategoriUtstyr`
  MODIFY `IDUnderkategori` int NOT NULL AUTO_INCREMENT;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `Romoversikt`
--
ALTER TABLE `Romoversikt`
  ADD CONSTRAINT `Romoversikt_ibfk_1` FOREIGN KEY (`IDBygning`) REFERENCES `Bygning` (`IDBygning`) ON DELETE RESTRICT ON UPDATE RESTRICT;

--
-- Constraints for table `UnderkategoriUtstyr`
--
ALTER TABLE `UnderkategoriUtstyr`
  ADD CONSTRAINT `UnderkategoriUtstyr_ibfk_1` FOREIGN KEY (`IDKategori`) REFERENCES `KategoriUtstyr` (`IDKategori`) ON DELETE RESTRICT ON UPDATE RESTRICT;

--
-- Constraints for table `Utstyr`
--
ALTER TABLE `Utstyr`
  ADD CONSTRAINT `Utstyr_ibfk_1` FOREIGN KEY (`Romnr`) REFERENCES `Romoversikt` (`Romnr`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `Utstyr_ibfk_2` FOREIGN KEY (`IDSLA`) REFERENCES `SLALeverandoer` (`IDSLA`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `Utstyr_ibfk_3` FOREIGN KEY (`IDUnderkategori`) REFERENCES `UnderkategoriUtstyr` (`IDUnderkategori`) ON DELETE RESTRICT ON UPDATE RESTRICT;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
