CREATE DATABASE IF NOT EXISTS DeviceSystemData;

USE DeviceSystemData;

CREATE TABLE IF NOT EXISTS tb_device (
    id_device    VARCHAR(36)  NOT NULL DEFAULT (UUID()),
    name         VARCHAR(36)  NOT NULL,
    brand        VARCHAR(36)  NOT NULL,
    state        VARCHAR(9)   NOT NULL,
    creation_time DATETIME    NOT NULL,
    CONSTRAINT PRIMARY KEY (id_device)
);
