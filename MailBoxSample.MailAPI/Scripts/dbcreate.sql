CREATE DATABASE IF NOT EXISTS `mail`;
USE `mail`;

CREATE TABLE IF NOT EXISTS `mail` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Guid` varchar(255) DEFAULT NULL,
  `IsActive` tinyint(1) DEFAULT NULL,
  `IsDeleted` tinyint(1) DEFAULT NULL,
  `CreatedAt` datetime DEFAULT NULL,
  `UpdatedAt` datetime DEFAULT NULL,
  `Subject` varchar(255) DEFAULT NULL,
  `Content` text,
  `IsRead` tinyint(1) DEFAULT NULL,
  `FromUserId` int(11) DEFAULT NULL,
  `ToUserId` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

CREATE TABLE IF NOT EXISTS `users` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Guid` varchar(255) DEFAULT NULL,
  `IsActive` tinyint(1) DEFAULT NULL,
  `IsDeleted` tinyint(1) DEFAULT NULL,
  `CreatedAt` datetime DEFAULT NULL,
  `UpdatedAt` datetime DEFAULT NULL,
  `FirstName` varchar(255) DEFAULT NULL,
  `LastName` varchar(255) DEFAULT NULL,
  `UserName` varchar(255) DEFAULT NULL,
  `Password` varchar(255) DEFAULT NULL,
  `Token` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;