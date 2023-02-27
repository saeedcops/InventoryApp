using Inventory.Core.Entity;
using Inventory.Core.Interfaces;
using Inventory.Web.ViewModel;
using System.Text.RegularExpressions;

namespace Inventory.Web.Filter
{
    public class InputFilter
    {
        public static bool TakeOutFilter(TakeOutParams item)
        {
            bool results = true;
            if (item.Count < 0 || item.Status < 0 ||
                item.Customer == null || item.Employee == null || item.Category == null)
            {
                results = false;
            }
            return results;

        }

        public static bool ItemCreateFilter(Item item)
        {
            if (string.IsNullOrEmpty(item.Name) || string.IsNullOrEmpty(item.Brand) || string.IsNullOrEmpty(item.Description))
            
                return false;
            
            string specialChars = "^[a-zA-Z]{3,}$";
            if (!Regex.IsMatch(item.Name, specialChars, RegexOptions.IgnoreCase))
            
                return false;
            

            if (!Regex.IsMatch(item.Description, specialChars, RegexOptions.IgnoreCase))
            
                return false;
            

            if (!Regex.IsMatch(item.Brand, specialChars, RegexOptions.IgnoreCase))
            
                return false;
            
          return true;
        }
       
        public static bool ItemCreateFilter(ItemViewModel item)
        {
            string specialChars = "^[a-zA-Z]{3,}$";
            if (!Regex.IsMatch(item.Name, specialChars, RegexOptions.IgnoreCase))
            {
                return false;
            }

            if (!Regex.IsMatch(item.Description, specialChars, RegexOptions.IgnoreCase))
            {
                return false;
            }

            if (!Regex.IsMatch(item.Brand, specialChars, RegexOptions.IgnoreCase))
            {
                return false;
            }
            return true;
        }
    }
}
