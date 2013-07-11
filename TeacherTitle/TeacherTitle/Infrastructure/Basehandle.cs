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
        private static IList<SelectListItem> CreateBaseItems()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem
            {
                Text = "请选择...",
                Value = "choosePlease"
            });

            return items;
        }

        public static SelectList ProduceSelectList(List<KeyValueModel> keyvalmodel)
        {
            var items = CreateBaseItems();
            if (keyvalmodel != null)
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

        public static SelectList ProduceMajorList(List<Major> major)
        {
            var items = CreateBaseItems();
            if (major != null)
            {
                for (int i = 0; i < major.Count; i++)
                {
                    items.Add(new SelectListItem
                    {
                        Text = major[i].M_Name,
                        Value = Convert.ToString(major[i].M_Code)
                    });
                }
            }
            return new SelectList(items, "Value", "Text");
        }

    }
}