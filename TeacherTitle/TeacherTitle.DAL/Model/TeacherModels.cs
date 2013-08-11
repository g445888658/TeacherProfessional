using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeacherTitle.DAL.Infrastructure;

namespace TeacherTitle.DAL.Model
{
    public class TeacherDetailModels
    {
        /// <summary>
        /// 学院
        /// </summary>
        public KeyValueModel institute { get; set; }

        /// <summary>
        /// 教师名字
        /// </summary>
        public KeyValueModel[] TeacherName { get; set; }
    }

    //public class TeacherModels
    //{
    //    public TeacherDetailModels[] institute { get; set; }
    //}
}
