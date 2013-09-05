using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ValueOffice.Excel;
using ValueOffice.Interface;
using ValueOffice.Infrastructure;
using TeacherTitle.DAL.Infrastructure;
using TeacherTitle.DAL.DB;
using System.Data.SqlClient;



namespace TeacherTitle.DAL.DAO
{
    public class ExcelDAO
    {
        BaseDAO baseDAO = new BaseDAO();
        UserDAO userDAO = new UserDAO();

        /// <summary>
        /// 导入教师信息
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public ArgsHelper AddTeachers(string fileName)
        {
            IExcel excelHelper = ValueExcel.GetInstance(OfficeType.V2007);
            string sql = "SELECT [职工号],[姓名],[性别],[出生年月],[民族],[所在院系部处],[具体部门],[来校年月],[参加工作年月]," +
                "[现从事学科],[从事专业],[研究方向],[专业技术资格],[资格级别],[专业技术职务聘任时间],[是否双职称],[长号],[短号],[邮箱],[QQ],[备注] FROM [Sheet1$]";
            DataTable dt = excelHelper.QuerySQL(fileName, sql, true);
            using (TTitleDBEntities db = new TTitleDBEntities())
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (string.IsNullOrEmpty(dt.Rows[i][j].ToString()) && j != 17)
                        {
                            return new ArgsHelper("工号为" + dt.Rows[i][0].ToString() + "的用户信息不完整,请修改后在上传");
                        }


                    }
                    if (userDAO.GetAllUsers().FirstOrDefault(x => x.U_Account == dt.Rows[i][0].ToString()) != null)
                    {
                        return new ArgsHelper("工号" + dt.Rows[i][0].ToString() + "已存在,请修改后在上传");
                    }
                    else if (userDAO.GetAllUsers().FirstOrDefault(x => x.U_Mail == dt.Rows[i][18].ToString()) != null)
                    {
                        return new ArgsHelper("邮箱" + dt.Rows[i][18].ToString() + "已存在,请修改后在上传");
                    }
                    var i_Code = baseDAO.GetAllInstitute().FirstOrDefault(x => x.I_Name == dt.Rows[i][5].ToString()).I_Code;
                    var d_Code = baseDAO.GetAllDepartment().FirstOrDefault(x => x.D_Name == dt.Rows[i][6].ToString()).D_Code;
                    Users users = new Users()
                    {
                        U_Account = dt.Rows[i][0].ToString(),
                        U_PassWord = dt.Rows[i][0].ToString(),
                        U_Name = dt.Rows[i][1].ToString(),
                        U_Sex = Convert.ToInt32(dt.Rows[i][2].ToString() == "男" ? "1" : "0"),
                        U_Birth = DateTime.Parse(dt.Rows[i][3].ToString()).ToString("yyyy/MM/dd"),
                        U_Nation = dt.Rows[i][4].ToString(),
                        I_Code = i_Code,
                        D_Code = d_Code,
                        U_SchoolTime = DateTime.Parse(dt.Rows[i][7].ToString()).ToString("yyyy/MM"),
                        U_WorkTime = DateTime.Parse(dt.Rows[i][8].ToString()).ToString("yyyy/MM"),
                        U_Subject = dt.Rows[i][9].ToString(),
                        U_Major = dt.Rows[i][10].ToString(),
                        U_Research = dt.Rows[i][11].ToString(),
                        U_Title = dt.Rows[i][12].ToString(),
                        U_TitleLevel = dt.Rows[i][13].ToString(),
                        U_EngageTime = DateTime.Parse(dt.Rows[i][14].ToString()).ToString("yyyy/MM"),
                        U_IsDoubleTitle = Convert.ToInt32(dt.Rows[i][15].ToString() == "是" ? "1" : "0"),
                        U_LongPhone = dt.Rows[i][16].ToString(),
                        U_ShortPhone = dt.Rows[i][17].ToString(),
                        U_Mail = dt.Rows[i][18].ToString(),
                        U_QQNum = dt.Rows[i][19].ToString(),
                        UT_Code = 2,//用户
                        U_Remark = dt.Rows[i][20].ToString(),
                    };

                    db.Users.AddObject(users);
                }
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return new ArgsHelper(ex.Message);
                }
            }
            return new ArgsHelper(true, "导入成功");
        }
    }
}
