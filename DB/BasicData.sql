insert into SysConfig(SC_FieldName,SC_FieldAttr,SC_FieldCode) values('kingdeetestmail@126.com','445888658','1');

insert into UserType(UTType) values('管理员');
insert into UserType(UTType) values('用户');

insert into Department(D_Name,D_IsValid) values('计算机与信息管理系',1);

insert into Institute(I_Name,I_IsValid) values('信息与工程学院',1);
insert into Institute(I_Name,I_IsValid) values('仁济学院',1);

insert into Users(U_Account,U_PassWord,U_Name,U_Sex,U_Birth,U_Nation,I_Code,D_Code,U_SchoolTime,U_WorkTime,U_Subject,U_Major,U_Research,U_Title,U_TitleLevel,U_EngageTime,U_IsDoubleTitle,U_LongPhone,U_ShortPhone,U_Mail,U_QQNum,UT_Code,U_Remark) values('adm','123','顾哲航','1','1992-10-24','汉','1','1','2011-10-1','2011-10-1','计算机软件与理论','计算机科学与技术','信息系统与移动医疗','讲师','中级','2012-10-1','0','18267858552','688552','445888658@qq.com','445888658','1','无备注');

insert into ActivityRemark(AR_Content) values('每位教师参加培训后需提交培训学时申请（校级沙龙除外），并提供培训证书、邀请函/通知书、学术论文、活动内容介绍、活动过程影像资料等证明材料。各二级学院在每一学年结束前将青年教师培训学时汇总报教师教学发展中心。');

insert into TeachingActivity (TA_Form,TA_Way,TA_Material,TA_ClassHour,TA_PlaceKey,TA_PlaceValue)
values('各类讲座','校相关部门、学院或召集人组织，教师自愿报名达到最低参加人数','通知、活动影音资料、会议记录、活动照片、电子讲稿、报道等','本中心组织的活动参与者计4学时/次；其他部门、学院等组织的活动，参与者计2学时/次；每年最高计16学时。召集人学时按2倍计算',0,'校内');

insert into TeachingActivity (TA_Form,TA_Way,TA_Material,TA_ClassHour,TA_PlaceKey,TA_PlaceValue)
values('工作坊','校相关部门、学院或召集人组织，教师自愿报名达到最低参加人数','通知、活动影音资料、会议记录、活动照片、电子讲稿、报道等','本中心组织的活动参与者计4学时/次；其他部门、学院等组织的活动，参与者计2学时/次；每年最高计16学时。召集人学时按2倍计算',0,'校内');

insert into TeachingActivity (TA_Form,TA_Way,TA_Material,TA_ClassHour,TA_PlaceKey,TA_PlaceValue)
values('教学研讨','校相关部门、学院或召集人组织，教师自愿报名达到最低参加人数','通知、活动影音资料、会议记录、活动照片、电子讲稿、报道等','本中心组织的活动参与者计4学时/次；其他部门、学院等组织的活动，参与者计2学时/次；每年最高计16学时。召集人学时按2倍计算',0,'校内');

insert into TeachingActivity (TA_Form,TA_Way,TA_Material,TA_ClassHour,TA_PlaceKey,TA_PlaceValue)
values('沙龙','校相关部门、学院或召集人组织，教师自愿报名达到最低参加人数','通知、活动影音资料、会议记录、活动照片、电子讲稿、报道等','本中心组织的活动参与者计4学时/次；其他部门、学院等组织的活动，参与者计2学时/次；每年最高计16学时。召集人学时按2倍计算',0,'校内');

insert into TeachingActivity (TA_Form,TA_Way,TA_Material,TA_ClassHour,TA_PlaceKey,TA_PlaceValue)
values('教研活动','系或教研室组织实施','影音资料、会议记录','2学时/次，每年最高计16学时',0,'校内');

insert into TeachingActivity (TA_Form,TA_Way,TA_Material,TA_ClassHour,TA_PlaceKey,TA_PlaceValue)
values('听课观摩','根据实际自行联系听课','听课记录、教学照片','按实际学时计，每年最高计16学时',0,'校内');

insert into TeachingActivity (TA_Form,TA_Way,TA_Material,TA_ClassHour,TA_PlaceKey,TA_PlaceValue)
values('教学互动','教师之间根据需要组织临时团队开展教学活动','活动内容介绍、活动影音资料、教学记录等','2学时/次，每年最高计8学时',0,'校内');

insert into TeachingActivity (TA_Form,TA_Way,TA_Material,TA_ClassHour,TA_PlaceKey,TA_PlaceValue)
values('教学资源建设','教师制作优质教学资源','上传学校教学资源平台','6学时/门，每年最高计18学时',0,'校内');

insert into TeachingActivity (TA_Form,TA_Way,TA_Material,TA_ClassHour,TA_PlaceKey,TA_PlaceValue)
values('学习成果汇报','国内外进修学习成果汇报会','影音资料、报道等','半天4学时，与调研合计每年最高16学时',0,'校内');

insert into TeachingActivity (TA_Form,TA_Way,TA_Material,TA_ClassHour,TA_PlaceKey,TA_PlaceValue)
values('教学导师','聘为青年教师教学导师','二级学院文件','2学时/人.周，每年最高计48学时',0,'校内');

insert into TeachingActivity (TA_Form,TA_Way,TA_Material,TA_ClassHour,TA_PlaceKey,TA_PlaceValue)
values('助教计划完成','经人事处考核合格','证书或人事处文件','2学时/周，每年最高72学时',0,'校内');

insert into TeachingActivity (TA_Form,TA_Way,TA_Material,TA_ClassHour,TA_PlaceKey,TA_PlaceValue)
values('校级及以上精品课程、教改项目','','相关部门文件','校级12学时/项，省级24学时/项，主持人每年最高计24学时，团队其他成员最高计8学时',0,'校内');

insert into TeachingActivity (TA_Form,TA_Way,TA_Material,TA_ClassHour,TA_PlaceKey,TA_PlaceValue)
values('校级教学比赛','含教学技能、教学基本功、教学设计等比赛，获三等奖以上','教务处文件或证书','4学时/次',0,'校内');

insert into TeachingActivity (TA_Form,TA_Way,TA_Material,TA_ClassHour,TA_PlaceKey,TA_PlaceValue)
values('工会活动、素质拓展活动','','相关部门通知、照片','半天4学时，每年最高计8学时',0,'校内');

insert into TeachingActivity (TA_Form,TA_Way,TA_Material,TA_ClassHour,TA_PlaceKey,TA_PlaceValue)
values('3个月以上脱产进修','相关部门和学院委派','进修考核表','24学时/月，6个月及以上计144学时',1,'校外');

insert into TeachingActivity (TA_Form,TA_Way,TA_Material,TA_ClassHour,TA_PlaceKey,TA_PlaceValue)
values('短期进修（不足3个月）','相关部门和学院委派','进修考核表或学习总结','一天2学时，一次最高48学时',1,'校外');

insert into TeachingActivity (TA_Form,TA_Way,TA_Material,TA_ClassHour,TA_PlaceKey,TA_PlaceValue)
values('青年教师教育理论培训','网络培训、集中考试','合格证书','64学时',1,'校外');

insert into TeachingActivity (TA_Form,TA_Way,TA_Material,TA_ClassHour,TA_PlaceKey,TA_PlaceValue)
values('挂职实践','相关部门和学院委派','文件、照片、实践总结','一天3学时，一次最高24学时',1,'校外');

insert into TeachingActivity (TA_Form,TA_Way,TA_Material,TA_ClassHour,TA_PlaceKey,TA_PlaceValue)
values('调研','部门学院委派，或项目等需要','调研报告、影音资料','半天4学时，一次最高8学时，与成果汇报合计每年最高16学时',1,'校外');

insert into TeachingActivity (TA_Form,TA_Way,TA_Material,TA_ClassHour,TA_PlaceKey,TA_PlaceValue)
values('教研与学术会','部门、学院委派，或专业、教学、科研等学术机构组织','会议通知、影音资料、会议总结报告','半天4学时，一次最高8学时，每年最高计16学时',1,'校外');

insert into TeachingActivity (TA_Form,TA_Way,TA_Material,TA_ClassHour,TA_PlaceKey,TA_PlaceValue)
values('教学比赛','部门、学院委派，或专业、教学、科研等学术机构组织','比赛通知和证书','省级及以上6学时/次，省级以下4学时/次',1,'校外');

insert into TeachingActivity (TA_Form,TA_Way,TA_Material,TA_ClassHour,TA_PlaceKey,TA_PlaceValue)
values('项目报告会','省部级以上教改、科研项目进展报告会','会议通知、影音资料、会议总结报告','6学时',1,'校外');

insert into TeachingActivity (TA_Form,TA_Way,TA_Material,TA_ClassHour,TA_PlaceKey,TA_PlaceValue)
values('网络在线学习','','网络平台记录','系统认定学时',1,'校外');

insert into TeachingActivity (TA_Form,TA_Way,TA_Material,TA_ClassHour,TA_PlaceKey,TA_PlaceValue)
values('问题辅导','教师教学发展中心组织','影音资料，网络记录','提问4次1学时，每年最高2学时；答复一次1学时',1,'校外');

insert into TeachingActivity (TA_Form,TA_Way,TA_Material,TA_ClassHour,TA_PlaceKey,TA_PlaceValue)
values('社会服务','政府或相关部门组织','照片、活动成果材料','一次2学时',1,'校外');

insert into TeachingActivity (TA_Form,TA_Way,TA_Material,TA_ClassHour,TA_PlaceKey,TA_PlaceValue)
values('其他','有助于教学成长的活动','相关活动成果的材料','酌情计学时',1,'校外');
