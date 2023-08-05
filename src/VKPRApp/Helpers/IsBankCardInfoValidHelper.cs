using System.Text.RegularExpressions;
using VKPRApp.Shared.Models;

namespace VKPRApp.Helpers
{
    static class IsBankCardInfoValidHelper
    {
        public static bool IsBankCardInfoValid(this BankCard card)
        {
            Regex cardCheck = new Regex(@"^(1298|1267|4512|4567|8901|8933)([\-\s]?[0-9]{4}){3}$");
            Regex monthCheck = new Regex(@"^(0[1-9]|1[0-2])$");
            Regex yearCheck = new Regex(@"^20[0-9]{2}$");
            Regex cvvCheck = new Regex(@"^\d{3}$");

            if (!cardCheck.IsMatch(card.CardNumber)) 
                return false;
            if (!cvvCheck.IsMatch(card.Cvv)) 
                return false;

            string[] dateParts = card.ExpireDate.Split('/');     
            if (!monthCheck.IsMatch(dateParts[0]) || !yearCheck.IsMatch(dateParts[1])) 
                return false; 

            int year = int.Parse(dateParts[1]);
            int month = int.Parse(dateParts[0]);
            int lastDateOfExpiryMonth = DateTime.DaysInMonth(year, month); 
            DateTime cardExpiry = new DateTime(year, month, lastDateOfExpiryMonth, 23, 59, 59);
            
            return (cardExpiry > DateTime.Now && cardExpiry < DateTime.Now.AddYears(6));
        }
    }
}
