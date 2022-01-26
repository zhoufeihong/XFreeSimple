SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for __efmigrationshistory
-- ----------------------------
DROP TABLE IF EXISTS `__efmigrationshistory`;
CREATE TABLE `__efmigrationshistory`  (
  `MigrationId` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `ProductVersion` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`MigrationId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Table structure for sys_background_api
-- ----------------------------
DROP TABLE IF EXISTS `sys_background_api`;
CREATE TABLE `sys_background_api`  (
  `id` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '数据唯一标识',
  `primary_node` tinyint(1) NOT NULL COMMENT '一级节点',
  `module` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '模块',
  `parent_permission_code` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '父节点权限编码',
  `permission_code` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '权限编码',
  `name` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '名称',
  `path` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '接口路径',
  `method` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '方法',
  `sort_order` int(11) NOT NULL COMMENT '排序',
  `en_name` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '英文名称',
  `tenant_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL COMMENT '租户Id',
  `multi_tenancy_sides` int(11) NOT NULL DEFAULT 0,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `IX_sys_background_api_parent_permission_code`(`parent_permission_code`) USING BTREE,
  INDEX `IX_sys_background_api_permission_code`(`permission_code`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Table structure for sys_database_connection
-- ----------------------------
DROP TABLE IF EXISTS `sys_database_connection`;
CREATE TABLE `sys_database_connection`  (
  `id` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '数据唯一标识',
  `database_provider_type` int(11) NOT NULL COMMENT '0: NotSpecified1: EntityFrameworkCore2: MongoDb',
  `name` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '名称',
  `memo` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '备注',
  `status` int(11) NOT NULL COMMENT '状态',
  `range_tenant_ids` varchar(4000) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '限定商户范围,默认不限定',
  `connection_string` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '数据库连接字符串',
  `extra_properties` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  `concurrency_stamp` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `creation_time` datetime(6) NOT NULL COMMENT '创建时间',
  `creator_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL COMMENT '创建人Id',
  `last_modification_time` datetime(6) NULL DEFAULT NULL COMMENT '最后修改时间',
  `last_modifier_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL COMMENT '最后人Id',
  `is_deleted` tinyint(1) NOT NULL DEFAULT 0 COMMENT 'Used to mark an Entity as \'Deleted\'.',
  `deleter_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL COMMENT 'Id of the deleter user.',
  `deletion_time` datetime(6) NULL DEFAULT NULL COMMENT 'Deletion time.',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `IX_sys_database_connection_name`(`name`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Table structure for sys_depart
-- ----------------------------
DROP TABLE IF EXISTS `sys_depart`;
CREATE TABLE `sys_depart`  (
  `id` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '数据唯一标识',
  `parent_id` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '父机构ID',
  `org_name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '机构/部门名称',
  `org_name_en` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '英文名',
  `org_category` int(11) NOT NULL COMMENT '机构类别 1组织机构，2岗位',
  `org_level_type` int(11) NOT NULL COMMENT '机构类型 1一级部门 2子部门',
  `org_type` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '类型',
  `org_code` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '机构编码',
  `contact` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '联系方式',
  `memo` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '备注',
  `status` int(11) NOT NULL COMMENT '状态',
  `sort_order` int(11) NULL DEFAULT NULL COMMENT '排序',
  `address` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '地址',
  `extra_properties` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  `concurrency_stamp` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `creation_time` datetime(6) NOT NULL COMMENT '创建时间',
  `creator_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL COMMENT '创建人Id',
  `last_modification_time` datetime(6) NULL DEFAULT NULL COMMENT '最后修改时间',
  `last_modifier_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL COMMENT '最后人Id',
  `deleter_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL COMMENT 'Id of the deleter user.',
  `deletion_time` datetime(6) NULL DEFAULT NULL COMMENT 'Deletion time.',
  `is_deleted` tinyint(1) NOT NULL DEFAULT 0 COMMENT 'Used to mark an Entity as \'Deleted\'.',
  `tenant_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL COMMENT '租户Id',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `IX_sys_depart_org_name`(`org_name`) USING BTREE,
  INDEX `IX_sys_depart_org_code`(`org_code`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Table structure for sys_depart_role
-- ----------------------------
DROP TABLE IF EXISTS `sys_depart_role`;
CREATE TABLE `sys_depart_role`  (
  `id` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '数据唯一标识',
  `depart_id` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `role_id` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `tenant_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL COMMENT '租户Id',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `IX_sys_depart_role_depart_id`(`depart_id`) USING BTREE,
  INDEX `IX_sys_depart_role_role_id`(`role_id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Table structure for sys_dict
-- ----------------------------
DROP TABLE IF EXISTS `sys_dict`;
CREATE TABLE `sys_dict`  (
  `id` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '数据唯一标识',
  `dict_code` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '字典编码',
  `dict_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '字典名称',
  `description` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '描述',
  `extra_properties` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  `concurrency_stamp` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `creation_time` datetime(6) NOT NULL COMMENT '创建时间',
  `creator_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL COMMENT '创建人Id',
  `last_modification_time` datetime(6) NULL DEFAULT NULL COMMENT '最后修改时间',
  `last_modifier_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL COMMENT '最后人Id',
  `deleter_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL COMMENT 'Id of the deleter user.',
  `deletion_time` datetime(6) NULL DEFAULT NULL COMMENT 'Deletion time.',
  `is_deleted` tinyint(1) NOT NULL DEFAULT 0 COMMENT 'Used to mark an Entity as \'Deleted\'.',
  `dict_en_name` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '字典名称',
  `tenant_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL COMMENT '租户Id',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `IX_sys_dict_dict_code`(`dict_code`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Table structure for sys_dict_item
-- ----------------------------
DROP TABLE IF EXISTS `sys_dict_item`;
CREATE TABLE `sys_dict_item`  (
  `id` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '数据唯一标识',
  `dict_id` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '字典id',
  `item_text` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '字典项文本',
  `item_value` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '字典项值',
  `description` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '描述',
  `sort_order` int(11) NULL DEFAULT NULL COMMENT '排序',
  `enabled` tinyint(1) NOT NULL COMMENT '状态（1启用 0不启用）',
  `extra_properties` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  `concurrency_stamp` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `creation_time` datetime(6) NOT NULL COMMENT '创建时间',
  `creator_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL COMMENT '创建人Id',
  `last_modification_time` datetime(6) NULL DEFAULT NULL COMMENT '最后修改时间',
  `last_modifier_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL COMMENT '最后人Id',
  `deleter_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL COMMENT 'Id of the deleter user.',
  `deletion_time` datetime(6) NULL DEFAULT NULL COMMENT 'Deletion time.',
  `is_deleted` tinyint(1) NOT NULL DEFAULT 0 COMMENT 'Used to mark an Entity as \'Deleted\'.',
  `item_en_text` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '字典项英文文本',
  `tenant_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL COMMENT '租户Id',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `IX_sys_dict_item_dict_id`(`dict_id`) USING BTREE,
  INDEX `IX_sys_dict_item_item_text`(`item_text`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Table structure for sys_operation_info
-- ----------------------------
DROP TABLE IF EXISTS `sys_operation_info`;
CREATE TABLE `sys_operation_info`  (
  `id` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '数据唯一标识',
  `user_id` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '用户Id',
  `title` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `content` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '内容',
  `ip` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `address` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '地址',
  `status` int(11) NOT NULL COMMENT '状态(1-成功 2-失败)',
  `creation_time` datetime(6) NOT NULL COMMENT '时间',
  `extra_properties` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  `concurrency_stamp` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `tenant_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL COMMENT '租户Id',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `IX_sys_operation_info_user_id`(`user_id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Table structure for sys_post
-- ----------------------------
DROP TABLE IF EXISTS `sys_post`;
CREATE TABLE `sys_post`  (
  `id` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '数据唯一标识',
  `code` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '编码',
  `name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '名称',
  `status` int(11) NOT NULL COMMENT '状态',
  `sort_order` int(11) NULL DEFAULT NULL COMMENT '排序',
  `memo` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '备注',
  `extra_properties` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  `concurrency_stamp` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `creation_time` datetime(6) NOT NULL COMMENT '创建时间',
  `creator_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL COMMENT '创建人Id',
  `last_modification_time` datetime(6) NULL DEFAULT NULL COMMENT '最后修改时间',
  `last_modifier_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL COMMENT '最后人Id',
  `deleter_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL COMMENT 'Id of the deleter user.',
  `deletion_time` datetime(6) NULL DEFAULT NULL COMMENT 'Deletion time.',
  `is_deleted` tinyint(1) NOT NULL DEFAULT 0 COMMENT 'Used to mark an Entity as \'Deleted\'.',
  `tenant_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL COMMENT '租户Id',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `IX_sys_post_code`(`code`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Table structure for sys_role
-- ----------------------------
DROP TABLE IF EXISTS `sys_role`;
CREATE TABLE `sys_role`  (
  `id` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '数据唯一标识',
  `code` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '角色编码',
  `name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '角色名称',
  `memo` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '备注',
  `status` int(11) NOT NULL COMMENT '状态',
  `role_access_type` int(11) NOT NULL COMMENT '可访问权限类型',
  `access_value` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '访问权限编码',
  `extra_properties` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  `concurrency_stamp` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `creation_time` datetime(6) NOT NULL COMMENT '创建时间',
  `creator_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL COMMENT '创建人Id',
  `last_modification_time` datetime(6) NULL DEFAULT NULL COMMENT '最后修改时间',
  `last_modifier_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL COMMENT '最后人Id',
  `deleter_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL COMMENT 'Id of the deleter user.',
  `deletion_time` datetime(6) NULL DEFAULT NULL COMMENT 'Deletion time.',
  `is_deleted` tinyint(1) NOT NULL DEFAULT 0 COMMENT 'Used to mark an Entity as \'Deleted\'.',
  `tenant_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL COMMENT '租户Id',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `IX_sys_role_name`(`name`) USING BTREE,
  INDEX `IX_sys_role_code`(`code`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Table structure for sys_role_ui_permission
-- ----------------------------
DROP TABLE IF EXISTS `sys_role_ui_permission`;
CREATE TABLE `sys_role_ui_permission`  (
  `id` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '数据唯一标识',
  `role_id` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '角色Id',
  `ui_permission_id` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '权限Id',
  `extra_properties` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  `concurrency_stamp` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `tenant_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL COMMENT '租户Id',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `IX_sys_role_ui_permission_role_id`(`role_id`) USING BTREE,
  INDEX `IX_sys_role_ui_permission_ui_permission_id`(`ui_permission_id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Table structure for sys_tenant
-- ----------------------------
DROP TABLE IF EXISTS `sys_tenant`;
CREATE TABLE `sys_tenant`  (
  `id` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '数据唯一标识',
  `code` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '租户编码',
  `name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '名称',
  `phone` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '手机号/电话',
  `email` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '邮箱',
  `status` int(11) NOT NULL COMMENT '商户状态',
  `extra_properties` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  `concurrency_stamp` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `creation_time` datetime(6) NOT NULL COMMENT '创建时间',
  `creator_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL COMMENT '创建人Id',
  `last_modification_time` datetime(6) NULL DEFAULT NULL COMMENT '最后修改时间',
  `last_modifier_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL COMMENT '最后人Id',
  `deleter_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL COMMENT 'Id of the deleter user.',
  `deletion_time` datetime(6) NULL DEFAULT NULL COMMENT 'Deletion time.',
  `is_deleted` tinyint(1) NOT NULL DEFAULT 0 COMMENT 'Used to mark an Entity as \'Deleted\'.',
  `language` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '语言',
  `default_connection_string_name` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '数据库连接配置名称',
  `is_standalone_database` tinyint(1) NOT NULL DEFAULT 0 COMMENT '采用独立的数据库',
  `standalone_database_connection_string` varchar(512) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '采用独立的数据库连接字符串',
  `memo` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '备注',
  `tenant_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL COMMENT '租户Id',
  `initial_data_status` int(11) NOT NULL DEFAULT 0 COMMENT '数据初始化状态TODO: Job重试',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `IX_sys_tenant_name`(`name`) USING BTREE,
  INDEX `IX_sys_tenant_code`(`code`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Table structure for sys_ui_permission
-- ----------------------------
DROP TABLE IF EXISTS `sys_ui_permission`;
CREATE TABLE `sys_ui_permission`  (
  `id` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '数据唯一标识',
  `parent_id` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '父id',
  `name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '菜单标题',
  `url` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '路径',
  `component_name` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '组件名称',
  `component` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '组件',
  `redirect` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '一级菜单跳转地址',
  `ui_menu_type` int(11) NOT NULL COMMENT '菜单类型(0:一级菜单; 1:子菜单:2:按钮权限)',
  `perms` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '菜单权限编码',
  `perms_type` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '权限策略1显示2禁用',
  `sort_order` int(11) NOT NULL COMMENT '菜单排序',
  `icon` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '菜单图标',
  `is_route` tinyint(1) NOT NULL COMMENT '是否路由菜单: 0:不是  1:是（默认值1）',
  `is_leaf` tinyint(1) NOT NULL COMMENT '是否叶子节点:    1:是   0:不是',
  `keep_alive` tinyint(1) NOT NULL COMMENT '是否缓存该页面:    1:是   0:不是',
  `hidden` tinyint(1) NOT NULL COMMENT '是否隐藏路由: 0否,1是',
  `description` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '描述',
  `enabled` tinyint(1) NOT NULL COMMENT '按钮权限状态(0无效1有效)',
  `open_mode` tinyint(1) NOT NULL COMMENT '菜单打开方式 0/内部打开 1/外部打开',
  `extra_properties` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  `concurrency_stamp` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `creation_time` datetime(6) NOT NULL COMMENT '创建时间',
  `creator_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL COMMENT '创建人Id',
  `last_modification_time` datetime(6) NULL DEFAULT NULL COMMENT '最后修改时间',
  `last_modifier_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL COMMENT '最后人Id',
  `deleter_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL COMMENT 'Id of the deleter user.',
  `deletion_time` datetime(6) NULL DEFAULT NULL COMMENT 'Deletion time.',
  `is_deleted` tinyint(1) NOT NULL DEFAULT 0 COMMENT 'Used to mark an Entity as \'Deleted\'.',
  `en_name` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '菜单英文标题',
  `tenant_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL COMMENT '租户Id',
  `multi_tenancy_sides` int(11) NOT NULL DEFAULT 0,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `IX_sys_ui_permission_name`(`name`) USING BTREE,
  INDEX `IX_sys_ui_permission_parent_id`(`parent_id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Table structure for sys_ui_with_api
-- ----------------------------
DROP TABLE IF EXISTS `sys_ui_with_api`;
CREATE TABLE `sys_ui_with_api`  (
  `id` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '数据唯一标识',
  `ui_permission_id` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `permission_code` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '接口资源编码',
  `tenant_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL COMMENT '租户Id',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `IX_sys_ui_with_api_permission_code`(`permission_code`) USING BTREE,
  INDEX `IX_sys_ui_with_api_ui_permission_id`(`ui_permission_id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Table structure for sys_user
-- ----------------------------
DROP TABLE IF EXISTS `sys_user`;
CREATE TABLE `sys_user`  (
  `id` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '数据唯一标识',
  `tenant_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL COMMENT '租户Id',
  `supper_user` tinyint(1) NOT NULL COMMENT '管理员用户',
  `depart_id` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '部门Id',
  `post_id` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '职务Id',
  `login_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '登录账号',
  `nickname` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '昵称/姓名',
  `password` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '密码',
  `salt` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '密码盐',
  `avatar` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '头像',
  `employee_id_number` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '工号',
  `birthday` datetime(6) NULL DEFAULT NULL COMMENT '生日',
  `sex` int(11) NOT NULL COMMENT '性别(0-默认未知,1-男,2-女)',
  `user_type` int(11) NOT NULL COMMENT '账号类型: ( 1: 平台   2:租户用户)',
  `email` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '邮箱',
  `phone` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '电话',
  `memo` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '备注',
  `status` int(11) NOT NULL COMMENT '状态(1-正常,2-锁定)',
  `login_ip` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '最后登录ip',
  `lock_login` tinyint(1) NOT NULL COMMENT '登录锁定',
  `login_date` datetime(6) NULL DEFAULT NULL COMMENT '最后登录时间',
  `pwd_update_date` datetime(6) NULL DEFAULT NULL COMMENT '最后密码更改时间',
  `extra_properties` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  `concurrency_stamp` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `creation_time` datetime(6) NOT NULL COMMENT '创建时间',
  `creator_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL COMMENT '创建人Id',
  `last_modification_time` datetime(6) NULL DEFAULT NULL COMMENT '最后修改时间',
  `last_modifier_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL COMMENT '最后人Id',
  `deleter_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL COMMENT 'Id of the deleter user.',
  `deletion_time` datetime(6) NULL DEFAULT NULL COMMENT 'Deletion time.',
  `is_deleted` tinyint(1) NOT NULL DEFAULT 0 COMMENT 'Used to mark an Entity as \'Deleted\'.',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `IX_sys_user_employee_id_number`(`employee_id_number`) USING BTREE,
  INDEX `IX_sys_user_phone`(`phone`) USING BTREE,
  INDEX `IX_sys_user_login_name`(`login_name`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Table structure for sys_user_role
-- ----------------------------
DROP TABLE IF EXISTS `sys_user_role`;
CREATE TABLE `sys_user_role`  (
  `id` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '数据唯一标识',
  `user_id` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '员工Id',
  `role_id` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '角色Id',
  `tenant_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL COMMENT '租户Id',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `IX_sys_user_role_role_id`(`role_id`) USING BTREE,
  INDEX `IX_sys_user_role_user_id`(`user_id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = DYNAMIC;

SET FOREIGN_KEY_CHECKS = 1;
