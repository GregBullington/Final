CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS keeps(
  id int AUTO_INCREMENT NOT NULL primary key COMMENT 'primary key',
  creatorId VARCHAR(255) NOT NULL,
  name TEXT NOT NULL,
  description TEXT NOT NULL,
  img TEXT NOT NULL,
  views int,
  shares int,
  keeps int,
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS vaults(
  id int AUTO_INCREMENT NOT NULL primary key COMMENT 'primary key',
  creatorId VARCHAR(255) NOT NULL,
  name TEXT NOT NULL,
  description TEXT NOT NULL,
  isPrivate TINYINT DEFAULT false,
  img TEXT NOT NULL,
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS vaultKeeps(
  id int AUTO_INCREMENT NOT NULL primary key COMMENT 'primary key',
  creatorId VARCHAR(255) NOT NULL,
  vaultId int NOT NULL,
  keepId int NOT NULL,
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE,
  FOREIGN KEY (vaultId) REFERENCES vaults(id) ON DELETE CASCADE,
  FOREIGN KEY (keepId) REFERENCES keeps(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS profiles(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  name varchar(255) COMMENT 'User Name',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';


SELECT * FROM vaultKeeps 
      WHERE id = 25 LIMIT 1