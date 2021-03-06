/*==============================================================*/
/* Database name:  TTitleDB                                     */
/* DBMS name:      Microsoft SQL Server 2005                    */
/* Created on:     2013/9/4 14:02:13                            */
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
/* Table: ActivityAttachment                                    */
/*==============================================================*/
create table ActivityAttachment (
   AA_Code              int                  identity,
   AP_Code              int                  not null,
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
   ASU_Code             int                  null,
   AM_Name              nvarchar(100)        not null,
   AM_SavePath          nvarchar(200)        not null,
   constraint PK_ACTIVITYMATERIAL primary key (AM_Code)
)
go

/*==============================================================*/
/* Table: ActivityPlan                                          */
/*==============================================================*/
create table ActivityPlan (
   AP_Code              int                  identity,
   TA_Code              int                  not null,
   AP_Theme             nvarchar(100)        not null,
   AP_Speaker           nvarchar(20)         null,
   AP_SchoolYear        char(10)             not null,
   AP_StartTime         nvarchar(20)         not null,
   AP_EndTime           nvarchar(20)         not null,
   AP_Place             nvarchar(100)        not null,
   AP_Limit             int                  null,
   AP_Left              int                  null,
   AP_Candidate         int                  null,
   AP_CandidateLeft     int                  null,
   AP_ReleaseTime       nvarchar(20)         not null,
   AP_ClassHour         nvarchar(20)         not null,
   AP_StatusKey         int                  not null,
   AP_StatusValue       nvarchar(10)         not null,
   constraint PK_ACTIVITYPLAN primary key (AP_Code)
)
go

/*==============================================================*/
/* Table: ActivityRemark                                        */
/*==============================================================*/
create table ActivityRemark (
   AR_Code              numeric              identity,
   AR_Content           nvarchar(Max)        null,
   constraint PK_ACTIVITYREMARK primary key (AR_Code)
)
go

/*==============================================================*/
/* Table: ActivitySignUp                                        */
/*==============================================================*/
create table ActivitySignUp (
   ASU_Code             int                  identity,
   U_Code               int                  not null,
   AP_Code              int                  null,
   ASU_Time             varchar(20)          null,
   ASU_IsCandidateKey   int                  not null,
   ASU_IsCandidateVal   nvarchar(10)         not null,
   ASU_StatusKey        int                  not null,
   ASU_StatusValue      nvarchar(10)         not null,
   constraint PK_ACTIVITYSIGNUP primary key (ASU_Code)
)
go

/*==============================================================*/
/* Table: ClassHourSum                                          */
/*==============================================================*/
create table ClassHourSum (
   CH_Code              int                  identity,
   ASU_Code             int                  null,
   CH_GetTime           nvarchar(20)         null,
   CH_Content           nvarchar(1000)       null,
   CH_GetHour           int                  null,
   CH_Remark            nvarchar(1000)       null,
   constraint PK_CLASSHOURSUM primary key (CH_Code)
)
go

/*==============================================================*/
/* Table: Department                                            */
/*==============================================================*/
create table Department (
   D_Code               int                  identity,
   D_Name               nvarchar(50)         not null,
   D_IsValid            int                  not null,
   constraint PK_DEPARTMENT primary key (D_Code)
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
/* Table: SysConfig                                             */
/*==============================================================*/
create table SysConfig (
   SC_Code              int                  identity,
   SC_FieldName         varchar(30)          not null,
   SC_FieldAttr         varchar(30)          not null,
   SC_FieldCode         int                  not null,
   constraint PK_SYSCONFIG primary key (SC_Code)
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
/* Table: UserType                                              */
/*==============================================================*/
create table UserType (
   UT_Code              int                  identity,
   UTType               nvarchar(10)         not null,
   constraint PK_USERTYPE primary key (UT_Code)
)
go

/*==============================================================*/
/* Table: Users                                                 */
/*==============================================================*/
create table Users (
   U_Code               int                  identity,
   U_Account            nvarchar(20)         not null,
   U_PassWord           nvarchar(20)         not null,
   U_Name               nvarchar(20)         not null,
   U_Sex                int                  not null,
   U_Birth              nvarchar(20)         not null,
   U_Nation             nvarchar(20)         not null,
   I_Code               int                  not null,
   D_Code               int                  not null,
   U_SchoolTime         nvarchar(20)         not null,
   U_WorkTime           nvarchar(20)         not null,
   U_Subject            nvarchar(20)         not null,
   U_Major              nvarchar(20)         not null,
   U_Research           nvarchar(20)         not null,
   U_Title              nvarchar(20)         not null,
   U_TitleLevel         nvarchar(20)         not null,
   U_EngageTime         nvarchar(20)         not null,
   U_IsDoubleTitle      int                  not null,
   U_LongPhone          varchar(20)          not null,
   U_ShortPhone         varchar(20)          null,
   U_Mail               varchar(20)          not null,
   U_QQNum              varchar(20)          not null,
   UT_Code              int                  not null,
   U_Remark             nvarchar(1000)       null,
   constraint PK_USERS primary key (U_Code),
   constraint AK_KEY_2_USERS unique (U_Account),
   constraint AK_KEY_3_USERS unique (U_Mail)
)
go

alter table ActivityAttachment
   add constraint FK_ACTIVITY_REFERENCE_ACTIVITY foreign key (AP_Code)
      references ActivityPlan (AP_Code)
go

alter table ActivityMaterial
   add constraint FK_ACTIVITY_REFERENCE_ACTIVITY_1 foreign key (ASU_Code)
      references ActivitySignUp (ASU_Code)
go

alter table ActivityPlan
   add constraint FK_ACTIVITY_REFERENCE_TEACHING foreign key (TA_Code)
      references TeachingActivity (TA_Code)
go

alter table ActivitySignUp
   add constraint FK_ACTIVITY_REFERENCE_ACTPLAN_1 foreign key (AP_Code)
      references ActivityPlan (AP_Code)
go

alter table ActivitySignUp
   add constraint FK_ACTIVITY_REFERENCE_USERS foreign key (U_Code)
      references Users (U_Code)
go

alter table ClassHourSum
   add constraint FK_CLASSHOU_REFERENCE_ACTIVITY foreign key (ASU_Code)
      references ActivitySignUp (ASU_Code)
go

alter table Users
   add constraint FK_USERS_REFERENCE_INSTITUT foreign key (I_Code)
      references Institute (I_Code)
go

alter table Users
   add constraint FK_USERS_REFERENCE_DEPARTME foreign key (D_Code)
      references Department (D_Code)
go

alter table Users
   add constraint FK_USERS_REFERENCE_USERTYPE foreign key (UT_Code)
      references UserType (UT_Code)
go

