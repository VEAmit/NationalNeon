using NationalNeon.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NationalNeon.Web.Helpers
{
    public static class Common
    {
        public static SelectList CreateSelectList(IList<IdValueModel> items, string placeholder = "[SELECT]")
        {
            return CreateSelectList(items, null, placeholder);
        }

        public static SelectList CreateSelectList(IList<IdValueModel> items, int? selectedValue, string placeholder = "[SELECT]")
        {
            //if (!string.IsNullOrWhiteSpace(placeholder))
            //    items.Insert(0, new IdValueModel { Id = null, Value = placeholder });
            return new SelectList(items, "Id", "Value", selectedValue);
        }
    }
}
