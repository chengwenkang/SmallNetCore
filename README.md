# SmallNetCore

#### 介绍
SmallNetCore是采用NET6搭建的一套轻量级后端开发项目,对于中小型企业或者个人项目可以直接使用，项目主要使用的技术栈包括：NetCore、Autofac、Sqlsugar、AutoMapper、Log4、JWT、Swagger等，基础框架功能都已经封装好，对相关技术稍加了解就能开箱即用，项目基本没有冗余代码。

+ [gitee地址](https://gitee.com/chengwenkang123/small-net-core)
+ [github地址](https://github.com/chengwenkang/SmallNetCore)

#### 软件架构
以MVC三层架构为基础的架构体系，整体的架构是每张表对应一个实体类、一个数据访问层的类。业务逻辑层则按照具体的业务进行组装，摒弃了一张表一个业务类的设计。例如下单业务，就会涉及到订单表、用户表、支付表进行组合。

+ 表现层 
  + SmallNetCore.UI
    + Controllers 控制器
    + appsettings.json 系统配置文件
    + log4net.config 日志配置文件
    + Program.cs 启动类

+ 业务逻辑层
  + SmallNetCore.IServices【用于IOC注入,面向接口编程】
  + SmallNetCore.Services
    + Base 当前层的公用方法，*例如基类*
+ 数据库访问层
  + SmallNetCore.IRepository【用于IOC注入,面向接口编程】
  + SmallNetCore.Repository
    + Base 当前层的公用方法，*例如基类*
+ 实体层
  + SmallNetCore.Models
    + Base 当前层的公用方法
    + Configs 配置集合
      + CenterConfigs.cs 动态配置读取
      + Consts.cs 常量的配置
    + DBModels 数据表实体
    + Entitys 公共的实体，*例如用户的登录实体，配置链接的实体等*
    + Enums 枚举集合
    + ViewModels Dto相关的实体
      + Base 公共的实体，*例如一些基类定义*
      + Request 所有请求的实体
      + Response 所有返回的实体

+ 其他
  + SmallNetCore.Common【公共方法层】
    + ApIInfo Http请求相关
    + Convets 各种数据转换
    + Encrypt 加密相关
    + Serialize 序列化相关
    + Utils 其他方法
    
  + SmallNetCore.Extensions【扩展层】，*主要用于一些公用的**非业务**逻辑层，例如,autofac的注入、automapper的构造等，但是像一些订单类、商品类的业务逻辑还是要放在SmallNetCore.Services层，最好不用弄混了*
    + AutoMapper AutoMapper相关的配置
    + Filter 过滤器配置
      + GlobalExceptionsFilter.cs 全局异常捕获
      + MyActionFilterAttribute.cs 请求管道跟踪
    + ServiceExtensions 服务注册相关扩展
  + SmallNetCore.Remotes【远程服务调用层】，*主要用于一些第三方的服务封装，例如微信支付、阿里支付等第三方的服务*

#### 安装使用教程

1.  当前项目使用的MySql数据库，会有个初始化的数据结构，脚本Sql如下:

+ 数据库库名称：FirstTestDb

    ```
       DROP TABLE IF EXISTS `role`;
        CREATE TABLE `role`  (
        `RoleId` int NOT NULL AUTO_INCREMENT COMMENT '角色ID',
        `RoleName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '角色名称',
        PRIMARY KEY (`RoleId`) USING BTREE
        ) ENGINE = InnoDB AUTO_INCREMENT = 36 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '角色表' ROW_FORMAT = Dynamic;

        DROP TABLE IF EXISTS `user`;
        CREATE TABLE `user`  (
        `Id` int NOT NULL AUTO_INCREMENT COMMENT '用户主键',
        `UserName` varchar(45) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '用户姓名',
        `Sex` tinyint(1) NOT NULL DEFAULT 0,
        PRIMARY KEY (`Id`) USING BTREE
        ) ENGINE = InnoDB AUTO_INCREMENT = 18 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '用户表' ROW_FORMAT = Dynamic;

        SET FOREIGN_KEY_CHECKS = 1;
    ```
+ 数据库库名称：SerondTestDb

    ```
     DROP TABLE IF EXISTS `order`;
        CREATE TABLE `order`  (
        `OrderId` int NOT NULL AUTO_INCREMENT COMMENT '主键',
        `UserId` int NOT NULL COMMENT '用户ID',
        `ProductId` int NOT NULL COMMENT '商品ID',
        PRIMARY KEY (`OrderId`) USING BTREE
        ) ENGINE = InnoDB AUTO_INCREMENT = 11 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

       SET FOREIGN_KEY_CHECKS = 1;
    ```
2.  数据库创建完成之后，进行更换项目的数据库链接，位置：SmallNetCore.UI -> appsettings.json -> MYSQL节点
3.  当前数据库完全只是测试数据库，并非必要数据库，正式项目可以替换自己的数据库，甚至更换Mysql,使用其他类型的数据库
4.  实体生成，可以使用[sqlsugar官方推荐的链接](https://www.donet5.com/Home/Doc?typeId=1207)，生成的实体需要包含Tenant特性，如下图，主要是解决当前项目存在多个数据库问题，如果单库可以通过设置默认链接不用设置Tenant特性

    ```
    [Tenant(MySqlConnEnum.FisrtTestDb)]
    public class Role
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int RoleId { get; set; }
    ```
5.  后期针对数据访问层也会集成到代码生成器里面，这个后期进行完善....

#### 其他

+  项目Gitee请看 [https://gitee.com/chengwenkang123/small-net-core](https://gitee.com/chengwenkang123/small-net-core)

+  项目GitHub请看 [https://github.com/chengwenkang/SmallNetCore](https://github.com/chengwenkang/SmallNetCore)

+   有问题可以加QQ：445056007
