using SV20T1080053.BusinessLayers;
using Microsoft.AspNetCore.Mvc.Rendering; 

namespace SV20T1080053.Web
{
    public class SelectListHelper
    {
        public static List<SelectListItem> Province () 
        { 
            List<SelectListItem> list = new List<SelectListItem> ();

            list.Add(new SelectListItem()
            {
                Value = "",
                Text = "--Chọn tỉnh/thành--"
            });

            foreach (var item in CommonDataService.ListOfProvinces())
                list.Add(new SelectListItem()
                {
                    Value = item.ProvinceName,
                    Text = item.ProvinceName
                });
            {
                return list;
            }
        }
    }
}
