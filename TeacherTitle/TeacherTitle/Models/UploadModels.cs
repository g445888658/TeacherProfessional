using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherTitle.Models
{
    public class UploadModels
    {
        private string[] _fileName;

        /// <summary>
        /// 文件名
        /// </summary>
        public string[] FileName
        {
            get
            {
                return _fileName;
            }
            set
            {
                _fileName = value;
            }
        }


        private string[] _saveName;

        /// <summary>
        /// 保存名
        /// </summary>
        public string[] SaveName
        {
            get
            {
                return _saveName;
            }
            set
            {
                _saveName = value;
            }
        }



    }
}