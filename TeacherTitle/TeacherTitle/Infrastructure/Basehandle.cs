using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeacherTitle.DAL.DB;
using TeacherTitle.DAL.Infrastructure;

namespace TeacherTitle.Infrastructure
{
    public class Basehandle
    {
        /// <summary>
        /// 活动地点
        /// </summary>
        public static List<KeyValueModel> YesOrNo = new List<KeyValueModel>()
        {
            new KeyValueModel()
            {
                Key="1",
                Value="是"
            },
            new KeyValueModel()
            {
                Key="0",
                Value="否"
            }
        };


        /// <summary>
        /// 活动地点
        /// </summary>
        public static List<KeyValueModel> AllPlace = new List<KeyValueModel>()
        {
            new KeyValueModel()
            {
                Key="0",
                Value="校内"
            },
            new KeyValueModel()
            {
                Key="1",
                Value="校外"
            }
        };


        /// <summary>
        /// 教师职称
        /// </summary>
        public static List<KeyValueModel> AllTitle = new List<KeyValueModel>()
        {
            new KeyValueModel()
            {
                Key="助教",
                Value="助教"
            },
            new KeyValueModel()
            {
                Key="讲师",
                Value="讲师"
            },
            new KeyValueModel()
            {
                Key="副教授",
                Value="副教授"
            },
            new KeyValueModel()
            {
                Key="教授",
                Value="教授"
            }
        };

        /// <summary>
        /// 性别
        /// </summary>
        public static List<KeyValueModel> AllSex = new List<KeyValueModel>()
        {
            new KeyValueModel()
            {
                Key="1",
                Value="男"
            },
            new KeyValueModel()
            {
                Key="0",
                Value="女"
            },
        };

        /// <summary>
        /// 教师学历
        /// </summary>
        public static List<KeyValueModel> AllDegree = new List<KeyValueModel>()
        {
            new KeyValueModel()
            {
                Key="本科",
                Value="本科"
            },
            new KeyValueModel()
            {
                Key="硕士",
                Value="硕士"
            },
            new KeyValueModel()
            {
                Key="博士",
                Value="博士"
            }
        };

        private static IList<SelectListItem> CreateBaseItems()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem
            {
                Text = "请选择...",
                //Value = "choosePlease"
            });

            return items;
        }


        /// <summary>
        /// 获取学年(当前学年)
        /// </summary>
        /// <returns></returns>
        public static List<KeyValueModel> GetSchoolYear()
        {
            int nowYear = DateTime.Now.Year;
            List<KeyValueModel> keyvalmodel = new List<KeyValueModel>();

            for (int i = 5; i > 0; i--)
            {
                keyvalmodel.Add(new KeyValueModel()
                {
                    Key = (nowYear + i - 1) + "-" + (nowYear + i),
                    Value = (nowYear + i - 1) + "-" + (nowYear + i)
                });
            }

            for (int i = 1; i < 6; i++)
            {
                keyvalmodel.Add(new KeyValueModel()
                {
                    Key = (nowYear - i) + "-" + (nowYear - i + 1),
                    Value = (nowYear - i) + "-" + (nowYear - i + 1)
                });
            }

            //{
            //     new KeyValueModel()
            //    {
            //        Key=(nowYear-1)+"-"+ (nowYear),
            //        Value=(nowYear-1)+"-"+ (nowYear)
            //    },
            //    new KeyValueModel()
            //    {
            //        Key=nowYear+"-"+ (nowYear+1),
            //        Value=nowYear+"-"+ (nowYear+1)
            //    }
            //};
            return keyvalmodel;
        }

        public static SelectList ProduceSelectList(List<string> list)
        {
            var items = CreateBaseItems();
            if (list.Count != 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    items.Add(new SelectListItem
                    {
                        Text = list[i],
                        Value = list[i]
                    });
                }
            }
            return new SelectList(items, "Value", "Text");
        }

        public static SelectList ProduceSelectList(List<KeyValueModel> keyvalmodel)
        {
            var items = CreateBaseItems();
            if (keyvalmodel != null && keyvalmodel.Count != 0)
            {
                for (int i = 0; i < keyvalmodel.Count; i++)
                {
                    items.Add(new SelectListItem
                    {
                        Text = keyvalmodel[i].Value,
                        Value = Convert.ToString(keyvalmodel[i].Key)
                    });
                }
            }
            return new SelectList(items, "Value", "Text");
        }

        public static SelectList ProduceTypeList(List<UserType> userType)
        {
            var items = CreateBaseItems();
            if (userType != null)
            {
                for (int i = 0; i < userType.Count; i++)
                {
                    items.Add(new SelectListItem
                    {
                        Text = userType[i].UTType,
                        Value = Convert.ToString(userType[i].UT_Code)
                    });
                }
            }
            return new SelectList(items, "Value", "Text");
        }

        public static SelectList ProduceInstituteList(List<Institute> institute)
        {
            var items = CreateBaseItems();
            if (institute != null)
            {
                for (int i = 0; i < institute.Count; i++)
                {
                    items.Add(new SelectListItem
                    {
                        Text = institute[i].I_Name,
                        Value = Convert.ToString(institute[i].I_Code)
                    });
                }
            }
            return new SelectList(items, "Value", "Text");
        }

        public static SelectList ProduceMajorList(List<Department> department)
        {
            var items = CreateBaseItems();
            if (department != null)
            {
                for (int i = 0; i < department.Count; i++)
                {
                    items.Add(new SelectListItem
                    {
                        Text = department[i].D_Name,
                        Value = Convert.ToString(department[i].D_Code)
                    });
                }
            }
            return new SelectList(items, "Value", "Text");
        }



    }
}