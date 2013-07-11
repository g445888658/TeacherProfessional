using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TeacherTitle.DAL.Infrastructure
{
    public class ArgsHelper
    {
        private bool flag;
        public bool Flag
        {
            get { return flag; }
            set { flag = value; }
        }

        private string msg;
        public string Msg
        {
            get { return msg; }
            set { msg = value; }
        }

        public ArgsHelper()
        {
            flag = true;
            msg = "";
        }

        public ArgsHelper(string Msg)
        {
            flag = false;
            msg = Msg;
        }

        public ArgsHelper(bool Flag, string Msg)
        {
            flag = Flag;
            msg = Msg;
        }


    }
}
