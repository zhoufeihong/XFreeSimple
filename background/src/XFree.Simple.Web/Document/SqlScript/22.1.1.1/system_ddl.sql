ALTER DATABASE CHARACTER SET utf8mb4;
CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET utf8mb4;

START TRANSACTION;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220106075026_Initial220102') THEN

    ALTER DATABASE CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220106075026_Initial220102') THEN

    CREATE TABLE `sys_background_api` (
        `id` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
        `primary_node` tinyint(1) NOT NULL,
        `module` varchar(100) CHARACTER SET utf8mb4 NULL,
        `parent_permission_code` varchar(256) CHARACTER SET utf8mb4 NULL,
        `permission_code` varchar(256) CHARACTER SET utf8mb4 NULL,
        `name` varchar(256) CHARACTER SET utf8mb4 NULL,
        `path` varchar(256) CHARACTER SET utf8mb4 NULL,
        `method` varchar(20) CHARACTER SET utf8mb4 NULL,
        `sort_order` int NOT NULL,
        CONSTRAINT `PK_sys_background_api` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220106075026_Initial220102') THEN

    CREATE TABLE `sys_depart` (
        `id` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
        `parent_id` varchar(256) CHARACTER SET utf8mb4 NULL,
        `org_name` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
        `org_name_en` varchar(256) CHARACTER SET utf8mb4 NULL,
        `org_category` int NOT NULL,
        `org_level_type` int NOT NULL,
        `org_type` varchar(256) CHARACTER SET utf8mb4 NULL,
        `org_code` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
        `contact` varchar(256) CHARACTER SET utf8mb4 NULL,
        `memo` varchar(256) CHARACTER SET utf8mb4 NULL,
        `status` int NOT NULL,
        `del_flag` tinyint(1) NOT NULL,
        `sort_order` int NULL,
        `address` varchar(256) CHARACTER SET utf8mb4 NULL,
        `extra_properties` longtext CHARACTER SET utf8mb4 NULL,
        `concurrency_stamp` varchar(40) CHARACTER SET utf8mb4 NULL,
        `creation_time` datetime(6) NOT NULL,
        `creator_id` char(36) COLLATE ascii_general_ci NULL,
        `last_modification_time` datetime(6) NULL,
        `last_modifier_id` char(36) COLLATE ascii_general_ci NULL,
        CONSTRAINT `PK_sys_depart` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220106075026_Initial220102') THEN

    CREATE TABLE `sys_depart_role` (
        `id` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
        `depart_id` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
        `role_id` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
        CONSTRAINT `PK_sys_depart_role` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220106075026_Initial220102') THEN

    CREATE TABLE `sys_dict` (
        `id` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
        `dict_code` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
        `dict_name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
        `description` varchar(256) CHARACTER SET utf8mb4 NULL,
        `del_flag` tinyint(1) NOT NULL,
        `extra_properties` longtext CHARACTER SET utf8mb4 NULL,
        `concurrency_stamp` varchar(40) CHARACTER SET utf8mb4 NULL,
        `creation_time` datetime(6) NOT NULL,
        `creator_id` char(36) COLLATE ascii_general_ci NULL,
        `last_modification_time` datetime(6) NULL,
        `last_modifier_id` char(36) COLLATE ascii_general_ci NULL,
        CONSTRAINT `PK_sys_dict` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220106075026_Initial220102') THEN

    CREATE TABLE `sys_dict_item` (
        `id` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
        `dict_id` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
        `item_text` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
        `item_value` varchar(256) CHARACTER SET utf8mb4 NOT NULL,
        `description` varchar(256) CHARACTER SET utf8mb4 NULL,
        `sort_order` int NULL,
        `enabled` tinyint(1) NOT NULL,
        `del_flag` tinyint(1) NOT NULL,
        `extra_properties` longtext CHARACTER SET utf8mb4 NULL,
        `concurrency_stamp` varchar(40) CHARACTER SET utf8mb4 NULL,
        `creation_time` datetime(6) NOT NULL,
        `creator_id` char(36) COLLATE ascii_general_ci NULL,
        `last_modification_time` datetime(6) NULL,
        `last_modifier_id` char(36) COLLATE ascii_general_ci NULL,
        CONSTRAINT `PK_sys_dict_item` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220106075026_Initial220102') THEN

    CREATE TABLE `sys_operation_info` (
        `id` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
        `user_id` varchar(256) CHARACTER SET utf8mb4 NULL,
        `title` varchar(100) CHARACTER SET utf8mb4 NULL,
        `content` varchar(500) CHARACTER SET utf8mb4 NULL,
        `ip` varchar(256) CHARACTER SET utf8mb4 NULL,
        `address` varchar(256) CHARACTER SET utf8mb4 NULL,
        `status` int NOT NULL,
        `creation_time` datetime(6) NOT NULL,
        `extra_properties` longtext CHARACTER SET utf8mb4 NULL,
        `concurrency_stamp` varchar(40) CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `PK_sys_operation_info` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220106075026_Initial220102') THEN

    CREATE TABLE `sys_post` (
        `id` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
        `code` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
        `name` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
        `status` int NOT NULL,
        `sort_order` int NULL,
        `memo` varchar(500) CHARACTER SET utf8mb4 NULL,
        `extra_properties` longtext CHARACTER SET utf8mb4 NULL,
        `concurrency_stamp` varchar(40) CHARACTER SET utf8mb4 NULL,
        `creation_time` datetime(6) NOT NULL,
        `creator_id` char(36) COLLATE ascii_general_ci NULL,
        `last_modification_time` datetime(6) NULL,
        `last_modifier_id` char(36) COLLATE ascii_general_ci NULL,
        CONSTRAINT `PK_sys_post` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220106075026_Initial220102') THEN

    CREATE TABLE `sys_role` (
        `id` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
        `code` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
        `name` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
        `memo` varchar(256) CHARACTER SET utf8mb4 NULL,
        `status` int NOT NULL,
        `role_access_type` int NOT NULL,
        `access_value` varchar(256) CHARACTER SET utf8mb4 NULL,
        `extra_properties` longtext CHARACTER SET utf8mb4 NULL,
        `concurrency_stamp` varchar(40) CHARACTER SET utf8mb4 NULL,
        `creation_time` datetime(6) NOT NULL,
        `creator_id` char(36) COLLATE ascii_general_ci NULL,
        `last_modification_time` datetime(6) NULL,
        `last_modifier_id` char(36) COLLATE ascii_general_ci NULL,
        CONSTRAINT `PK_sys_role` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220106075026_Initial220102') THEN

    CREATE TABLE `sys_role_ui_permission` (
        `id` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
        `role_id` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
        `ui_permission_id` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
        `extra_properties` longtext CHARACTER SET utf8mb4 NULL,
        `concurrency_stamp` varchar(40) CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `PK_sys_role_ui_permission` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220106075026_Initial220102') THEN

    CREATE TABLE `sys_tenant` (
        `id` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
        `code` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
        `name` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
        `phone` varchar(100) CHARACTER SET utf8mb4 NULL,
        `email` varchar(100) CHARACTER SET utf8mb4 NULL,
        `status` int NOT NULL,
        `del_flag` tinyint(1) NOT NULL,
        `extra_properties` longtext CHARACTER SET utf8mb4 NULL,
        `concurrency_stamp` varchar(40) CHARACTER SET utf8mb4 NULL,
        `creation_time` datetime(6) NOT NULL,
        `creator_id` char(36) COLLATE ascii_general_ci NULL,
        `last_modification_time` datetime(6) NULL,
        `last_modifier_id` char(36) COLLATE ascii_general_ci NULL,
        CONSTRAINT `PK_sys_tenant` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220106075026_Initial220102') THEN

    CREATE TABLE `sys_ui_permission` (
        `id` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
        `parent_id` varchar(256) CHARACTER SET utf8mb4 NULL,
        `name` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
        `url` varchar(256) CHARACTER SET utf8mb4 NULL,
        `component_name` varchar(256) CHARACTER SET utf8mb4 NULL,
        `component` varchar(256) CHARACTER SET utf8mb4 NULL,
        `redirect` varchar(256) CHARACTER SET utf8mb4 NULL,
        `ui_menu_type` int NOT NULL,
        `perms` varchar(256) CHARACTER SET utf8mb4 NULL,
        `perms_type` varchar(256) CHARACTER SET utf8mb4 NULL,
        `sort_order` int NOT NULL,
        `icon` varchar(256) CHARACTER SET utf8mb4 NULL,
        `is_route` tinyint(1) NOT NULL,
        `is_leaf` tinyint(1) NOT NULL,
        `keep_alive` tinyint(1) NOT NULL,
        `hidden` tinyint(1) NOT NULL,
        `description` varchar(256) CHARACTER SET utf8mb4 NULL,
        `del_flag` tinyint(1) NOT NULL,
        `enabled` tinyint(1) NOT NULL,
        `open_mode` tinyint(1) NOT NULL,
        `extra_properties` longtext CHARACTER SET utf8mb4 NULL,
        `concurrency_stamp` varchar(40) CHARACTER SET utf8mb4 NULL,
        `creation_time` datetime(6) NOT NULL,
        `creator_id` char(36) COLLATE ascii_general_ci NULL,
        `last_modification_time` datetime(6) NULL,
        `last_modifier_id` char(36) COLLATE ascii_general_ci NULL,
        CONSTRAINT `PK_sys_ui_permission` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220106075026_Initial220102') THEN

    CREATE TABLE `sys_ui_with_api` (
        `id` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
        `ui_permission_id` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
        `permission_code` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
        CONSTRAINT `PK_sys_ui_with_api` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220106075026_Initial220102') THEN

    CREATE TABLE `sys_user` (
        `id` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
        `tenant_id` varchar(256) CHARACTER SET utf8mb4 NULL,
        `supper_user` tinyint(1) NOT NULL,
        `depart_id` varchar(256) CHARACTER SET utf8mb4 NULL,
        `post_id` varchar(256) CHARACTER SET utf8mb4 NULL,
        `login_name` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
        `nickname` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
        `password` varchar(256) CHARACTER SET utf8mb4 NULL,
        `salt` varchar(256) CHARACTER SET utf8mb4 NULL,
        `avatar` varchar(256) CHARACTER SET utf8mb4 NULL,
        `employee_id_number` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
        `birthday` datetime(6) NULL,
        `sex` int NOT NULL,
        `user_type` int NOT NULL,
        `email` varchar(100) CHARACTER SET utf8mb4 NULL,
        `phone` varchar(50) CHARACTER SET utf8mb4 NULL,
        `memo` varchar(256) CHARACTER SET utf8mb4 NULL,
        `status` int NOT NULL,
        `del_flag` tinyint(1) NOT NULL,
        `login_ip` varchar(256) CHARACTER SET utf8mb4 NULL,
        `lock_login` tinyint(1) NOT NULL,
        `login_date` datetime(6) NULL,
        `pwd_update_date` datetime(6) NULL,
        `extra_properties` longtext CHARACTER SET utf8mb4 NULL,
        `concurrency_stamp` varchar(40) CHARACTER SET utf8mb4 NULL,
        `creation_time` datetime(6) NOT NULL,
        `creator_id` char(36) COLLATE ascii_general_ci NULL,
        `last_modification_time` datetime(6) NULL,
        `last_modifier_id` char(36) COLLATE ascii_general_ci NULL,
        CONSTRAINT `PK_sys_user` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220106075026_Initial220102') THEN

    CREATE TABLE `sys_user_role` (
        `id` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
        `user_id` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
        `role_id` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
        CONSTRAINT `PK_sys_user_role` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220106075026_Initial220102') THEN

    CREATE INDEX `IX_sys_background_api_parent_permission_code` ON `sys_background_api` (`parent_permission_code`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220106075026_Initial220102') THEN

    CREATE UNIQUE INDEX `IX_sys_background_api_permission_code` ON `sys_background_api` (`permission_code`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220106075026_Initial220102') THEN

    CREATE UNIQUE INDEX `IX_sys_depart_org_code` ON `sys_depart` (`org_code`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220106075026_Initial220102') THEN

    CREATE INDEX `IX_sys_depart_org_name` ON `sys_depart` (`org_name`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220106075026_Initial220102') THEN

    CREATE INDEX `IX_sys_depart_role_depart_id` ON `sys_depart_role` (`depart_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220106075026_Initial220102') THEN

    CREATE INDEX `IX_sys_depart_role_role_id` ON `sys_depart_role` (`role_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220106075026_Initial220102') THEN

    CREATE INDEX `IX_sys_dict_dict_code` ON `sys_dict` (`dict_code`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220106075026_Initial220102') THEN

    CREATE INDEX `IX_sys_dict_item_dict_id` ON `sys_dict_item` (`dict_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220106075026_Initial220102') THEN

    CREATE INDEX `IX_sys_dict_item_item_text` ON `sys_dict_item` (`item_text`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220106075026_Initial220102') THEN

    CREATE INDEX `IX_sys_operation_info_user_id` ON `sys_operation_info` (`user_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220106075026_Initial220102') THEN

    CREATE UNIQUE INDEX `IX_sys_post_code` ON `sys_post` (`code`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220106075026_Initial220102') THEN

    CREATE UNIQUE INDEX `IX_sys_role_code` ON `sys_role` (`code`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220106075026_Initial220102') THEN

    CREATE INDEX `IX_sys_role_name` ON `sys_role` (`name`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220106075026_Initial220102') THEN

    CREATE INDEX `IX_sys_role_ui_permission_role_id` ON `sys_role_ui_permission` (`role_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220106075026_Initial220102') THEN

    CREATE INDEX `IX_sys_role_ui_permission_ui_permission_id` ON `sys_role_ui_permission` (`ui_permission_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220106075026_Initial220102') THEN

    CREATE UNIQUE INDEX `IX_sys_tenant_code` ON `sys_tenant` (`code`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220106075026_Initial220102') THEN

    CREATE INDEX `IX_sys_tenant_name` ON `sys_tenant` (`name`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220106075026_Initial220102') THEN

    CREATE INDEX `IX_sys_ui_permission_name` ON `sys_ui_permission` (`name`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220106075026_Initial220102') THEN

    CREATE INDEX `IX_sys_ui_permission_parent_id` ON `sys_ui_permission` (`parent_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220106075026_Initial220102') THEN

    CREATE INDEX `IX_sys_ui_with_api_permission_code` ON `sys_ui_with_api` (`permission_code`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220106075026_Initial220102') THEN

    CREATE INDEX `IX_sys_ui_with_api_ui_permission_id` ON `sys_ui_with_api` (`ui_permission_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220106075026_Initial220102') THEN

    CREATE INDEX `IX_sys_user_employee_id_number` ON `sys_user` (`employee_id_number`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220106075026_Initial220102') THEN

    CREATE UNIQUE INDEX `IX_sys_user_login_name` ON `sys_user` (`login_name`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220106075026_Initial220102') THEN

    CREATE INDEX `IX_sys_user_phone` ON `sys_user` (`phone`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220106075026_Initial220102') THEN

    CREATE INDEX `IX_sys_user_role_role_id` ON `sys_user_role` (`role_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220106075026_Initial220102') THEN

    CREATE INDEX `IX_sys_user_role_user_id` ON `sys_user_role` (`user_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220106075026_Initial220102') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20220106075026_Initial220102', '5.0.13');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

