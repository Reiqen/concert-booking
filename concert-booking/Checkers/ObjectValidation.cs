using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;

namespace concert_booking.Checkers
{
    public class ObjectValidation
    {
        public static bool IsValidImage(byte[] bytes)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream(bytes))
                    Image.FromStream(ms);
            }
            catch (ArgumentException)
            {
                return false;
            }
            return true;
        }

        public static bool IsValidID(int idToCheck, IEnumerable<int> ids)
        {
            bool checker = false;
            foreach (var id in ids)
            {
                if (idToCheck == id)
                {
                    checker = true;
                }
            }
            return checker;
        }

        public static bool IsValidDate(string date)
        {
            if (DateTime.TryParseExact(date, "dd.MM.yyyy hh:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out _)) return true;
            else return false;
        }

        public static bool IsVarcharString(string str)
        {
            bool checker = true;
            foreach (char c in str)
            {
                if (c >= 128)
                {
                    checker = false;
                }
            }
            return checker;
        }
    }
}