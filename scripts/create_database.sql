CREATE TABLE `db3069420bf1e240a8b8e0a08300fdc045`.`boards`(
	`Id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
	`UserId` INT UNSIGNED NOT NULL,
	`Title` VARCHAR(255),
	`Description` TEXT(2000),
	`CreatedDate` DATETIME NOT NULL,
	PRIMARY KEY (`Id`)
) ENGINE=INNODB; 

CREATE TABLE `db3069420bf1e240a8b8e0a08300fdc045`.`board_items`(
	`ItemId` INT NOT NULL AUTO_INCREMENT,
	`BoardId` INT NOT NULL,
	`ProductId` INT,
	`CatalogId` INT,
	`ImageUrl` VARCHAR(255) NOT NULL,
	`PosX` INT,
	`PosY` INT,
	`Width` INT,
	`Height` INT,
	`Rotation` INT DEFAULT 0,
	`Layer` INT DEFAULT 0,
	PRIMARY KEY (`ItemId`)
) ENGINE=INNODB;

ALTER TABLE `db3069420bf1e240a8b8e0a08300fdc045`.`boards` ADD COLUMN `BoardImage` VARCHAR(255) NULL AFTER `CreatedDate`;