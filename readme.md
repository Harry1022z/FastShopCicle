Base de datos.



-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema: tiendaciclismo
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `tiendaciclismo` DEFAULT CHARACTER SET utf8mb4;
USE `tiendaciclismo`;

-- -----------------------------------------------------
-- Tabla: __efmigrationshistory
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `tiendaciclismo`.`__efmigrationshistory` (
  `MigrationId` VARCHAR(150) NOT NULL,
  `ProductVersion` VARCHAR(32) NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE = InnoDB DEFAULT CHARACTER SET = utf8mb4;

-- -----------------------------------------------------
-- Tabla: aspnetroles
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `tiendaciclismo`.`aspnetroles` (
  `Id` VARCHAR(255) NOT NULL,
  `Name` VARCHAR(256) NULL DEFAULT NULL,
  `NormalizedName` VARCHAR(256) NULL DEFAULT NULL,
  `ConcurrencyStamp` LONGTEXT NULL DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE INDEX `RoleNameIndex` (`NormalizedName` ASC)
) ENGINE = InnoDB DEFAULT CHARACTER SET = utf8mb4;

-- -----------------------------------------------------
-- Tabla: aspnetroleclaims
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `tiendaciclismo`.`aspnetroleclaims` (
  `Id` INT(11) NOT NULL AUTO_INCREMENT,
  `RoleId` VARCHAR(255) NOT NULL,
  `ClaimType` LONGTEXT NULL DEFAULT NULL,
  `ClaimValue` LONGTEXT NULL DEFAULT NULL,
  PRIMARY KEY (`Id`),
  INDEX `IX_AspNetRoleClaims_RoleId` (`RoleId` ASC),
  CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId`
    FOREIGN KEY (`RoleId`) REFERENCES `tiendaciclismo`.`aspnetroles` (`Id`) ON DELETE CASCADE
) ENGINE = InnoDB DEFAULT CHARACTER SET = utf8mb4;

-- -----------------------------------------------------
-- Tabla: aspnetusers
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `tiendaciclismo`.`aspnetusers` (
  `Id` VARCHAR(255) NOT NULL,
  `NombreCompleto` VARCHAR(100) NOT NULL,
  `UserName` VARCHAR(256) NULL DEFAULT NULL,
  `NormalizedUserName` VARCHAR(256) NULL DEFAULT NULL,
  `Email` VARCHAR(256) NULL DEFAULT NULL,
  `NormalizedEmail` VARCHAR(256) NULL DEFAULT NULL,
  `EmailConfirmed` TINYINT(1) NOT NULL,
  `PasswordHash` LONGTEXT NULL DEFAULT NULL,
  `SecurityStamp` LONGTEXT NULL DEFAULT NULL,
  `ConcurrencyStamp` LONGTEXT NULL DEFAULT NULL,
  `PhoneNumber` LONGTEXT NULL DEFAULT NULL,
  `PhoneNumberConfirmed` TINYINT(1) NOT NULL,
  `TwoFactorEnabled` TINYINT(1) NOT NULL,
  `LockoutEnd` DATETIME(6) NULL DEFAULT NULL,
  `LockoutEnabled` TINYINT(1) NOT NULL,
  `AccessFailedCount` INT(11) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE INDEX `UserNameIndex` (`NormalizedUserName` ASC),
  INDEX `EmailIndex` (`NormalizedEmail` ASC)
) ENGINE = InnoDB DEFAULT CHARACTER SET = utf8mb4;

-- -----------------------------------------------------
-- Tabla: vendedores
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `tiendaciclismo`.`vendedores` (
  `Id` INT(11) NOT NULL AUTO_INCREMENT,
  `Nombre` VARCHAR(100) NOT NULL,
  `Correo` VARCHAR(255) NOT NULL,
  `Telefono` VARCHAR(20) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE = InnoDB AUTO_INCREMENT = 4 DEFAULT CHARACTER SET = utf8mb4;

-- -----------------------------------------------------
-- Tabla: compras
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `tiendaciclismo`.`compras` (
  `Id` INT(11) NOT NULL AUTO_INCREMENT,
  `UsuarioId` VARCHAR(255) NOT NULL,
  `VendedorId` INT(11) NOT NULL,
  `Total` DECIMAL(18,2) NOT NULL,
  `Fecha` DATETIME(6) NOT NULL,
  PRIMARY KEY (`Id`),
  INDEX `IX_Compras_VendedorId` (`VendedorId` ASC),
  CONSTRAINT `FK_Compras_Vendedores_VendedorId`
    FOREIGN KEY (`VendedorId`) REFERENCES `tiendaciclismo`.`vendedores` (`Id`) ON DELETE CASCADE
) ENGINE = InnoDB DEFAULT CHARACTER SET = utf8mb4;

-- -----------------------------------------------------
-- Tabla: proveedores
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `tiendaciclismo`.`proveedores` (
  `Id` INT(11) NOT NULL AUTO_INCREMENT,
  `Nombre` VARCHAR(100) NOT NULL,
  `Contacto` VARCHAR(100) NULL DEFAULT NULL,
  `Direccion` VARCHAR(200) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE = InnoDB AUTO_INCREMENT = 3 DEFAULT CHARACTER SET = utf8mb4;

-- -----------------------------------------------------
-- Tabla: productos
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `tiendaciclismo`.`productos` (
  `Id` INT(11) NOT NULL AUTO_INCREMENT,
  `Nombre` VARCHAR(100) NOT NULL,
  `Precio` DECIMAL(18,2) NOT NULL,
  `Descripcion` VARCHAR(500) NOT NULL,
  `Categoria` VARCHAR(100) NOT NULL,
  `Stock` INT(11) NOT NULL,
  `ImagenUrl` VARCHAR(255) NOT NULL,
  `ProveedorId` INT(11) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`),
  INDEX `IX_Productos_ProveedorId` (`ProveedorId` ASC),
  CONSTRAINT `FK_Productos_Proveedores_ProveedorId`
    FOREIGN KEY (`ProveedorId`) REFERENCES `tiendaciclismo`.`proveedores` (`Id`) ON DELETE SET NULL
) ENGINE = InnoDB AUTO_INCREMENT = 4 DEFAULT CHARACTER SET = utf8mb4;

-- -----------------------------------------------------
-- Tabla: facturas
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `tiendaciclismo`.`facturas` (
  `Id` INT(11) NOT NULL AUTO_INCREMENT,
  `Fecha` DATETIME(6) NOT NULL,
  `Total` DECIMAL(18,2) NOT NULL,
  `VendedorId` INT(11) NOT NULL,
  `ProductoId` INT(11) NOT NULL,
  `Cantidad` INT(11) NOT NULL,
  `Descripcion` VARCHAR(500) NOT NULL,
  PRIMARY KEY (`Id`),
  INDEX `IX_Facturas_ProductoId` (`ProductoId` ASC),
  INDEX `IX_Facturas_VendedorId` (`VendedorId` ASC),
  CONSTRAINT `FK_Facturas_Productos_ProductoId`
    FOREIGN KEY (`ProductoId`) REFERENCES `tiendaciclismo`.`productos` (`Id`),
  CONSTRAINT `FK_Facturas_Vendedores_VendedorId`
    FOREIGN KEY (`VendedorId`) REFERENCES `tiendaciclismo`.`vendedores` (`Id`) ON DELETE CASCADE
) ENGINE = InnoDB AUTO_INCREMENT = 9 DEFAULT CHARACTER SET = utf8mb4;

-- -----------------------------------------------------
-- Tabla: inventarios
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `tiendaciclismo`.`inventarios` (
  `Id` INT(11) NOT NULL AUTO_INCREMENT,
  `ProductoId` INT(11) NOT NULL,
  `CantidadVendida` INT(11) NOT NULL,
  `TotalVendido` DECIMAL(18,2) NOT NULL,
  `VendedorId` INT(11) NOT NULL,
  `FechaVenta` DATETIME(6) NOT NULL,
  PRIMARY KEY (`Id`),
  INDEX `IX_Inventarios_ProductoId` (`ProductoId` ASC),
  INDEX `IX_Inventarios_VendedorId` (`VendedorId` ASC),
  CONSTRAINT `FK_Inventarios_Productos_ProductoId`
    FOREIGN KEY (`ProductoId`) REFERENCES `tiendaciclismo`.`productos` (`Id`),
  CONSTRAINT `FK_Inventarios_Vendedores_VendedorId`
    FOREIGN KEY (`VendedorId`) REFERENCES `tiendaciclismo`.`vendedores` (`Id`)
) ENGINE = InnoDB DEFAULT CHARACTER SET = utf8mb4;
