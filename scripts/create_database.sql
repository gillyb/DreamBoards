CREATE TABLE `db3069420bf1e240a8b8e0a08300fdc045`.`dreamboards`(
	`Id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
	`UserId` INT UNSIGNED NOT NULL,
	`Title` VARCHAR(255),
	`Description` TEXT(2000),
	`CreatedDate` DATETIME NOT NULL,
	PRIMARY KEY (`Id`)
) ENGINE=INNODB; 