insert into SysConfig(SC_FieldName,SC_FieldAttr,SC_FieldCode) values('kingdeetestmail@126.com','445888658','1');

insert into UserType(UTType) values('����Ա');
insert into UserType(UTType) values('�û�');

insert into Major(M_Name,M_IsValid) values('��Ϣ��������Ϣϵͳ',1);
insert into Major(M_Name,M_IsValid) values('����ҽѧ����',1);

insert into Institute(I_Name,I_IsValid) values('��Ϣ�빤��ѧԺ',1);
insert into Institute(I_Name,I_IsValid) values('�ʼ�ѧԺ',1);

insert into Users(U_Account,U_PassWord,U_Name,I_Code,M_Code,UT_Code,U_Degree,U_Title,U_Mail,U_Phone,U_Remark) values ('123','123','��ά����',1,1,1,'˶ʿ','��ʦ','123456@qq.com','123456','��ע');

insert into Users(U_Account,U_PassWord,U_Name,I_Code,M_Code,UT_Code,U_Degree,U_Title,U_Mail,U_Phone,U_Remark) values ('111','111','��֮�켣',2,2,2,'��ʿ','��ʦ','123456@126.com','456789','��ע');

insert into Users(U_Account,U_PassWord,U_Name,I_Code,M_Code,UT_Code,U_Degree,U_Title,U_Mail,U_Phone,U_Remark) values ('222','222','Լ����',2,2,2,'˶ʿ','����','123456@163.com','456789','��ע');

insert into Users(U_Account,U_PassWord,U_Name,I_Code,M_Code,UT_Code,U_Degree,U_Title,U_Mail,U_Phone,U_Remark) values ('333','333','��˹�ٶ�',2,2,2,'˶ʿ','������','111@qq.com','8888','���Ǳ�ע');

insert into ActivityRemark(AR_Content) values('ÿλ��ʦ�μ���ѵ�����ύ��ѵѧʱ���루У��ɳ�����⣩�����ṩ��ѵ֤�顢���뺯/֪ͨ�顢ѧ�����ġ�����ݽ��ܡ������Ӱ�����ϵ�֤�����ϡ�������ѧԺ��ÿһѧ�����ǰ�������ʦ��ѵѧʱ���ܱ���ʦ��ѧ��չ���ġ�');

insert into TeachingActivity (TA_Form,TA_Way,TA_Material,TA_ClassHour,TA_PlaceKey,TA_PlaceValue)
values('���ི��','У��ز��š�ѧԺ���ټ�����֯����ʦ��Ը�����ﵽ��Ͳμ�����','֪ͨ���Ӱ�����ϡ������¼�����Ƭ�����ӽ��塢������','��������֯�Ļ�����߼�4ѧʱ/�Σ��������š�ѧԺ����֯�Ļ�������߼�2ѧʱ/�Σ�ÿ����߼�16ѧʱ���ټ���ѧʱ��2������',0,'У��');

insert into TeachingActivity (TA_Form,TA_Way,TA_Material,TA_ClassHour,TA_PlaceKey,TA_PlaceValue)
values('������','У��ز��š�ѧԺ���ټ�����֯����ʦ��Ը�����ﵽ��Ͳμ�����','֪ͨ���Ӱ�����ϡ������¼�����Ƭ�����ӽ��塢������','��������֯�Ļ�����߼�4ѧʱ/�Σ��������š�ѧԺ����֯�Ļ�������߼�2ѧʱ/�Σ�ÿ����߼�16ѧʱ���ټ���ѧʱ��2������',0,'У��');

insert into TeachingActivity (TA_Form,TA_Way,TA_Material,TA_ClassHour,TA_PlaceKey,TA_PlaceValue)
values('��ѧ����','У��ز��š�ѧԺ���ټ�����֯����ʦ��Ը�����ﵽ��Ͳμ�����','֪ͨ���Ӱ�����ϡ������¼�����Ƭ�����ӽ��塢������','��������֯�Ļ�����߼�4ѧʱ/�Σ��������š�ѧԺ����֯�Ļ�������߼�2ѧʱ/�Σ�ÿ����߼�16ѧʱ���ټ���ѧʱ��2������',0,'У��');

insert into TeachingActivity (TA_Form,TA_Way,TA_Material,TA_ClassHour,TA_PlaceKey,TA_PlaceValue)
values('ɳ��','У��ز��š�ѧԺ���ټ�����֯����ʦ��Ը�����ﵽ��Ͳμ�����','֪ͨ���Ӱ�����ϡ������¼�����Ƭ�����ӽ��塢������','��������֯�Ļ�����߼�4ѧʱ/�Σ��������š�ѧԺ����֯�Ļ�������߼�2ѧʱ/�Σ�ÿ����߼�16ѧʱ���ټ���ѧʱ��2������',0,'У��');

insert into TeachingActivity (TA_Form,TA_Way,TA_Material,TA_ClassHour,TA_PlaceKey,TA_PlaceValue)
values('���л','ϵ���������֯ʵʩ','Ӱ�����ϡ������¼','2ѧʱ/�Σ�ÿ����߼�16ѧʱ',0,'У��');

insert into TeachingActivity (TA_Form,TA_Way,TA_Material,TA_ClassHour,TA_PlaceKey,TA_PlaceValue)
values('���ι�Ħ','����ʵ��������ϵ����','���μ�¼����ѧ��Ƭ','��ʵ��ѧʱ�ƣ�ÿ����߼�16ѧʱ',0,'У��');

insert into TeachingActivity (TA_Form,TA_Way,TA_Material,TA_ClassHour,TA_PlaceKey,TA_PlaceValue)
values('��ѧ����','��ʦ֮�������Ҫ��֯��ʱ�Ŷӿ�չ��ѧ�','����ݽ��ܡ��Ӱ�����ϡ���ѧ��¼��','2ѧʱ/�Σ�ÿ����߼�8ѧʱ',0,'У��');

insert into TeachingActivity (TA_Form,TA_Way,TA_Material,TA_ClassHour,TA_PlaceKey,TA_PlaceValue)
values('��ѧ��Դ����','��ʦ�������ʽ�ѧ��Դ','�ϴ�ѧУ��ѧ��Դƽ̨','6ѧʱ/�ţ�ÿ����߼�18ѧʱ',0,'У��');

insert into TeachingActivity (TA_Form,TA_Way,TA_Material,TA_ClassHour,TA_PlaceKey,TA_PlaceValue)
values('ѧϰ�ɹ��㱨','���������ѧϰ�ɹ��㱨��','Ӱ�����ϡ�������','����4ѧʱ������кϼ�ÿ�����16ѧʱ',0,'У��');

insert into TeachingActivity (TA_Form,TA_Way,TA_Material,TA_ClassHour,TA_PlaceKey,TA_PlaceValue)
values('��ѧ��ʦ','ƸΪ�����ʦ��ѧ��ʦ','����ѧԺ�ļ�','2ѧʱ/��.�ܣ�ÿ����߼�48ѧʱ',0,'У��');

insert into TeachingActivity (TA_Form,TA_Way,TA_Material,TA_ClassHour,TA_PlaceKey,TA_PlaceValue)
values('���̼ƻ����','�����´����˺ϸ�','֤������´��ļ�','2ѧʱ/�ܣ�ÿ�����72ѧʱ',0,'У��');

insert into TeachingActivity (TA_Form,TA_Way,TA_Material,TA_ClassHour,TA_PlaceKey,TA_PlaceValue)
values('У�������Ͼ�Ʒ�γ̡��̸���Ŀ','','��ز����ļ�','У��12ѧʱ/�ʡ��24ѧʱ/�������ÿ����߼�24ѧʱ���Ŷ�������Ա��߼�8ѧʱ',0,'У��');

insert into TeachingActivity (TA_Form,TA_Way,TA_Material,TA_ClassHour,TA_PlaceKey,TA_PlaceValue)
values('У����ѧ����','����ѧ���ܡ���ѧ����������ѧ��Ƶȱ����������Ƚ�����','�����ļ���֤��','4ѧʱ/��',0,'У��');

insert into TeachingActivity (TA_Form,TA_Way,TA_Material,TA_ClassHour,TA_PlaceKey,TA_PlaceValue)
values('������������չ�','','��ز���֪ͨ����Ƭ','����4ѧʱ��ÿ����߼�8ѧʱ',0,'У��');

insert into TeachingActivity (TA_Form,TA_Way,TA_Material,TA_ClassHour,TA_PlaceKey,TA_PlaceValue)
values('3���������Ѳ�����','��ز��ź�ѧԺί��','���޿��˱�','24ѧʱ/�£�6���¼����ϼ�144ѧʱ',1,'У��');

insert into TeachingActivity (TA_Form,TA_Way,TA_Material,TA_ClassHour,TA_PlaceKey,TA_PlaceValue)
values('���ڽ��ޣ�����3���£�','��ز��ź�ѧԺί��','���޿��˱��ѧϰ�ܽ�','һ��2ѧʱ��һ�����48ѧʱ',1,'У��');

insert into TeachingActivity (TA_Form,TA_Way,TA_Material,TA_ClassHour,TA_PlaceKey,TA_PlaceValue)
values('�����ʦ����������ѵ','������ѵ�����п���','�ϸ�֤��','64ѧʱ',1,'У��');

insert into TeachingActivity (TA_Form,TA_Way,TA_Material,TA_ClassHour,TA_PlaceKey,TA_PlaceValue)
values('��ְʵ��','��ز��ź�ѧԺί��','�ļ�����Ƭ��ʵ���ܽ�','һ��3ѧʱ��һ�����24ѧʱ',1,'У��');

insert into TeachingActivity (TA_Form,TA_Way,TA_Material,TA_ClassHour,TA_PlaceKey,TA_PlaceValue)
values('����','����ѧԺί�ɣ�����Ŀ����Ҫ','���б��桢Ӱ������','����4ѧʱ��һ�����8ѧʱ����ɹ��㱨�ϼ�ÿ�����16ѧʱ',1,'У��');

insert into TeachingActivity (TA_Form,TA_Way,TA_Material,TA_ClassHour,TA_PlaceKey,TA_PlaceValue)
values('������ѧ����','���š�ѧԺί�ɣ���רҵ����ѧ�����е�ѧ��������֯','����֪ͨ��Ӱ�����ϡ������ܽᱨ��','����4ѧʱ��һ�����8ѧʱ��ÿ����߼�16ѧʱ',1,'У��');

insert into TeachingActivity (TA_Form,TA_Way,TA_Material,TA_ClassHour,TA_PlaceKey,TA_PlaceValue)
values('��ѧ����','���š�ѧԺί�ɣ���רҵ����ѧ�����е�ѧ��������֯','����֪ͨ��֤��','ʡ��������6ѧʱ/�Σ�ʡ������4ѧʱ/��',1,'У��');

insert into TeachingActivity (TA_Form,TA_Way,TA_Material,TA_ClassHour,TA_PlaceKey,TA_PlaceValue)
values('��Ŀ�����','ʡ�������Ͻ̸ġ�������Ŀ��չ�����','����֪ͨ��Ӱ�����ϡ������ܽᱨ��','6ѧʱ',1,'У��');

insert into TeachingActivity (TA_Form,TA_Way,TA_Material,TA_ClassHour,TA_PlaceKey,TA_PlaceValue)
values('��������ѧϰ','','����ƽ̨��¼','ϵͳ�϶�ѧʱ',1,'У��');

insert into TeachingActivity (TA_Form,TA_Way,TA_Material,TA_ClassHour,TA_PlaceKey,TA_PlaceValue)
values('���⸨��','��ʦ��ѧ��չ������֯','Ӱ�����ϣ������¼','����4��1ѧʱ��ÿ�����2ѧʱ����һ��1ѧʱ',1,'У��');

insert into TeachingActivity (TA_Form,TA_Way,TA_Material,TA_ClassHour,TA_PlaceKey,TA_PlaceValue)
values('������','��������ز�����֯','��Ƭ����ɹ�����','һ��2ѧʱ',1,'У��');

insert into TeachingActivity (TA_Form,TA_Way,TA_Material,TA_ClassHour,TA_PlaceKey,TA_PlaceValue)
values('����','�����ڽ�ѧ�ɳ��Ļ','��ػ�ɹ��Ĳ���','�����ѧʱ',1,'У��');
