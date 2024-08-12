using Microsoft.AspNetCore.Mvc.Rendering;

namespace RPMS.Extensions
{
    public static class IEnumerableExtensions
    {

        public static IEnumerable<SelectListItem> ToSelectListItem<T> (this IEnumerable<T> Items, string textProperty, string valueProperty)
        {
            List<SelectListItem> selectList = new List<SelectListItem> ();

            SelectListItem selectListItem = new SelectListItem
            {
                Text = "- - - Select - - - ",
                Value = ""
            };

            selectList.Add (selectListItem);

            foreach (var item in Items)
            {
                selectListItem = new SelectListItem { Text = item.GetPropertyValue(textProperty), Value = item.GetPropertyValue(valueProperty) };
                selectList.Add (selectListItem);    
            }

            return selectList;
        }

    }
}
