-- MySQL Administrator dump 1.4
--
-- ------------------------------------------------------
-- Server version	5.0.18-nt


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


--
-- Create schema cfdi
--

CREATE DATABASE /*!32312 IF NOT EXISTS*/ cfdi;
USE cfdi;

--
-- Table structure for table `cfdi`.`certificados`
--

DROP TABLE IF EXISTS `certificados`;
CREATE TABLE `certificados` (
  `Folio` int(10) unsigned NOT NULL auto_increment,
  `Clave_emp` int(10) unsigned NOT NULL default '0',
  `Arch_cer` varchar(250) default NULL,
  `Arch_key` varchar(250) default NULL,
  `Estatus` char(1) default NULL,
  `Fecha_Crea` datetime default NULL,
  `Fecha_Can` datetime default NULL,
  `Contrasena` varchar(45) default NULL,
  `NoCertificado` varchar(45) default NULL,
  `ValidoDe` datetime default NULL,
  `ValidoHasta` datetime default NULL,
  `Ruta_cer` varchar(250) default NULL,
  PRIMARY KEY  (`Folio`,`Clave_emp`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `cfdi`.`certificados`
--

/*!40000 ALTER TABLE `certificados` DISABLE KEYS */;
/*!40000 ALTER TABLE `certificados` ENABLE KEYS */;


--
-- Table structure for table `cfdi`.`control_folios`
--

DROP TABLE IF EXISTS `control_folios`;
CREATE TABLE `control_folios` (
  `Folio` int(10) unsigned NOT NULL auto_increment,
  `Clave_Emp` int(10) unsigned NOT NULL default '0',
  `Serie` varchar(10) default NULL,
  `FolioInicial` int(10) unsigned default NULL,
  `FoliosT` int(10) unsigned default NULL,
  `Estatus` char(1) NOT NULL default '',
  `Fecha_Crea` datetime default NULL,
  `Fecha_Can` datetime default NULL,
  `TipoMov` varchar(3) default NULL,
  PRIMARY KEY  (`Folio`,`Clave_Emp`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `cfdi`.`control_folios`
--

/*!40000 ALTER TABLE `control_folios` DISABLE KEYS */;
/*!40000 ALTER TABLE `control_folios` ENABLE KEYS */;


--
-- Table structure for table `cfdi`.`empresa`
--

DROP TABLE IF EXISTS `empresa`;
CREATE TABLE `empresa` (
  `Clave` int(10) unsigned NOT NULL auto_increment,
  `Nombre` varchar(250) default NULL,
  `RFC` varchar(16) default NULL,
  `Estatus` char(1) default NULL,
  `Fecha_Crea` datetime default NULL,
  `Fecha_Can` datetime default NULL,
  `fCalle` varchar(250) NOT NULL default '',
  `fNoExterior` varchar(45) NOT NULL default '',
  `fNoInterior` varchar(45) NOT NULL default '',
  `fColonia` varchar(250) NOT NULL default '',
  `fLocalidad` varchar(250) NOT NULL default '',
  `fMunicipio` varchar(250) NOT NULL default '',
  `fEstado` varchar(250) NOT NULL default '',
  `fPais` varchar(250) NOT NULL default '',
  `fCP` varchar(45) NOT NULL default '',
  `sCalle` varchar(250) NOT NULL default '',
  `sNoExterior` varchar(45) NOT NULL default '',
  `sNoInterior` varchar(45) NOT NULL default '',
  `sColonia` varchar(250) NOT NULL default '',
  `sLocalidad` varchar(250) NOT NULL default '',
  `sMunicipio` varchar(250) NOT NULL default '',
  `sEstado` varchar(250) NOT NULL default '',
  `sPais` varchar(250) NOT NULL default '',
  `sCP` varchar(45) NOT NULL default '',
  `Ruta_fact` varchar(250) NOT NULL default '',
  `Ruta_NoTimb` varchar(250) NOT NULL default '',
  `Ruta_PDF` varchar(250) NOT NULL default '',
  `Directorio_base` varchar(250) NOT NULL default '',
  `Ruta_BMP` varchar(250) NOT NULL default '',
  `TM` varchar(45) NOT NULL default '',
  `Cuenta_Timbrado` varchar(45) NOT NULL default '',
  `Token_Timbrado` varchar(45) NOT NULL default '',
  `Usuario_Timbrado` varchar(45) NOT NULL default '',
  `Pass_Timbrado` varchar(45) NOT NULL default '',
  `Contribuyente` varchar(45) NOT NULL default '',
  `RegFiscal` varchar(100) NOT NULL default '',
  `TipoFac` varchar(45) NOT NULL default '',
  `Ruta_logo` varchar(250) default NULL,
  `Correo` varchar(150) default NULL,
  `Password` varchar(45) default NULL,
  `fTelefono` varchar(45) default NULL,
  `sTelefono` varchar(45) default NULL,
  `Ruta_cbb` varchar(250) default NULL,
  `Reporte_fac` varchar(250) NOT NULL default '', 
  `EmisorRI` varchar(250) default NULL,
  `IEPS` char(1) default NULL,
  PRIMARY KEY  (`Clave`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `cfdi`.`empresa`
--

/*!40000 ALTER TABLE `empresa` DISABLE KEYS */;
/*!40000 ALTER TABLE `empresa` ENABLE KEYS */;


--
-- Table structure for table `cfdi`.`movimientos`
--

DROP TABLE IF EXISTS `movimientos`;
CREATE TABLE `movimientos` (
  `Folio` int(10) unsigned NOT NULL auto_increment,
  `Serie` varchar(10) NOT NULL default '',
  `Clave_Emp` int(10) unsigned NOT NULL default '0',
  `No_Certificado` text,
  `CadenaOriginal` text,
  `SelloCFD` text,
  `SelloSAT` text,
  `Estatus` char(3) default NULL,
  `Factura` int(10) unsigned default NULL,
  `Folio_SAT` varchar(250) default NULL,
  `NoCSD_SAT` text,
  `Operacion` double default NULL,
  `Impuesto` double default NULL,
  `TipoCambio` double default NULL,
  `IVA` double default NULL,
  `MetodoPago` varchar(45) NOT NULL default '',
  `NoCtaPago` varchar(45) NOT NULL default '',
  `AnoAprobacion` varchar(45) NOT NULL default '',
  `NoAprobacion` varchar(45) NOT NULL default '',
  `NotaCredito` int(10) unsigned default NULL,
  `TipoMov` varchar(3) NOT NULL default '',
  `Fecha` datetime NOT NULL DEFAULT '0000-00-00 00:00:00',
  `RFC` varchar(13) NOT NULL DEFAULT '',
  PRIMARY KEY  (`Folio`,`Clave_Emp`,`Serie`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `cfdi`.`movimientos`
--

/*!40000 ALTER TABLE `movimientos` DISABLE KEYS */;
/*!40000 ALTER TABLE `movimientos` ENABLE KEYS */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
