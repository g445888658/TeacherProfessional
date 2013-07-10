/*==============================================================*/
/* Database name:  TTitleDB                                     */
/* DBMS name:      Microsoft SQL Server 2005                    */
/* Created on:     2013/7/9 16:29:48                            */
/*==============================================================*/


drop database TTitleDB
go

/*==============================================================*/
/* Database: TTitleDB                                           */
/*==============================================================*/
create database TTitleDB
on (
    name= "TTitle",
    filename= 'D:\DB\TTitle\TTitle.mdf'
)
log on (
    name= "TTitle_log",
    filename= 'D:\DB\TTitle\TTitle_log.mdf'
)
go

use TTitleDB
go

/*==============================================================*/
/* Table: ActPlan                                               */
/*==============================================================*/
create table ActPlan (
   AP_Code              int                  identity,
   TA_Code              int                  not null,
   AP_Theme             nvarchar(100)        not null,
   AP_StartTime         nvarchar(20)         not null,
   AP_EndTime           nvarchar(20)         not null,
   AP_Place             nvarchar(100)        not null,
   AP_Limit             int                  not null,
   AP_Left              int                  not null,
   AP_Candidate         int                  null,
   AP_StatusKey         int                  not null,
   AP_StatusValue       nvarchar(10)         not null,
   constraint PK_ACTPLAN primary key (AP_Code)
)
go

/*==============================================================*/
/* Table: ActivityAttachment                                    */
/*==============================================================*/
create table ActivityAttachment (
   AA_Code              int                  identity,
   AP_Code              int                  null,
   AA_Name              nvarchar(100)        not null,
   AA_Path              nvarchar(200)        not null,
   constraint PK_ACTIVITYATTACHMENT primary key (AA_Code)
)
go

/*==============================================================*/
/* Table: ActivityMaterial                                      */
/*==============================================================*/
create table ActivityMaterial (
   AM_Code              int                  identity,
   CH_Code              int                  not null,
   AM_Name              nvarchar(100)        not null,
   AM_SavePath          nvarchar(200)        not null,
   constraint PK_ACTIVITYMATERIAL primary key (AM_Code)
)
go

/*==============================================================*/
/* Table: ClassHourSum                                          */
/*==============================================================*/
create table ClassHourSum (
   CH_Code              int                  identity,
   U_Code               int                  not null,
   AP_Code              int                  not null,
   CH_GetTime           nvarchar(20)         null,
   CH_GetHour           int                  not null,
   CH_Remark            nvarchar(1000)       null,
   constraint PK_CLASSHOURSUM primary key (CH_Code)
)
go

/*==============================================================*/
/* Table: Institute                                             */
/*==============================================================*/
create table Institute (
   I_Code               int                  identity,
   I_Name               nvarchar(50)         not null,
   I_IsValid            int                  not null,
   constraint PK_INSTITUTE primary key (I_Code)
)
go

/*==============================================================*/
/* Table: Major                                                 */
/*==============================================================*/
create table Major (
   M_Code               int                  identity,
   M_Name               nvarchar(50)         not null,
   M_IsValid            int                  not null,
   constraint PK_MAJOR primary key (M_Code)
)
go

/*==============================================================*/
/* Table: TeachingActivity                                      */
/*==============================================================*/
create table TeachingActivity (
   TA_Code              int                  identity,
   TA_Form              nvarchar(100)        not null,
   TA_Way               nvarchar(500)        not null,
   TA_Material          nvarchar(500)        not null,
   TA_ClassHour         nvarchar(500)        not null,
   TA_PlaceKey          int                  not null,
   TA_PlaceValue        nvarchar(10)         not null,
   constraint PK_TEACHINGACTIVITY primary key (TA_Code)
)
go

/*==============================================================*/
/* Table: "User"                                                */
/*==============================================================*/
create table "User" (
   U_Code               int                  identity,
   U_Account            nvarchar(50)         not null,
   U_Name               nvarchar(50)         not null,
   I_Code               int                  not null,
   M_Code               int                  not null,
   UT_Code              int                  null,
   U_Degree             nvarchar(20)         not null,
   U_Title              nvarchar(20)         not null,
   U_Mail               nvarchar(20)         not null,
   U_Phone              nvarchar(20)         not null,
   U_Remark             nvarchar(1000)       null,
   constraint PK_USER primary key (U_Code)
)
go

/*==============================================================*/
/* Table: UserType                                              */
/*==============================================================*/
create table UserType (
   UT_Code              int                  identity,
   UTTtype              nvarchar(10)         not null,
   constraint PK_USERTYPE primary key (UT_Code)
)
go

alter table ActPlan
   add constraint FK_ACTPLAN_REFERENCE_TEACHING foreign key (TA_Code)
      references TeachingActivity (TA_Code)
go

alter table ActivityAttachment
   add constraint FK_ACTIVITY_REFERENCE_ACTPLAN foreign key (AP_Code)
      references ActPlan (AP_Code)
go

alter table ActivityMaterial
   add constraint FK_ACTIVITY_REFERENCE_CLASSHOU foreign key (CH_Code)
      references ClassHourSum (CH_Code)
go

alter table ClassHourSum
   add constraint FK_CLASSHOU_REFERENCE_USER foreign key (U_Code)
      references "User" (U_Code)
go

alter table ClassHourSum
   add constraint FK_CLASSHOU_REFERENCE_ACTPLAN foreign key (AP_Code)
      references ActPlan (AP_Code)
go

alter table "User"
   add constraint FK_USER_REFERENCE_INSTITUT foreign key (I_Code)
      references Institute (I_Code)
go

alter table "User"
   add constraint FK_USER_REFERENCE_MAJOR foreign key (M_Code)
      references Major (M_Code)
go

alter table "User"
   add constraint FK_USER_REFERENCE_USERTYPE foreign key (UT_Code)
      references UserType (UT_Code)
go
