using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tm.Data.Common
{
    public static class Utilities
    {
        // Calculate time string
        public static string TimespanToString(DateTime DateNow,DateTime pastTime)
        {
            TimeSpan timeDiff = DateNow.Subtract(pastTime);

            if (timeDiff.TotalHours < 1)
            {
                return Math.Round(timeDiff.TotalMinutes, 0) + " phút trước";
            }

            if (timeDiff.TotalHours < 24)
            {
                return Math.Round(timeDiff.TotalHours, 0) + " giờ trước";
            }

            if (timeDiff.TotalHours < 48)
            {
                return "Hôm qua";
            }

            if (timeDiff.TotalHours < 168)
            {
                return Math.Round(timeDiff.TotalDays, 0) + " ngày trước";
            }

            if (timeDiff.TotalDays < 42)
            {
                return Math.Round((timeDiff.TotalDays / 7), 0) + " tuần trước";
            }

            if (timeDiff.TotalDays < 365)
            {
                return Math.Round((timeDiff.TotalDays / 30), 0) + " tháng trước";
            }

            return Math.Round((timeDiff.TotalDays / 365), 0) + " năm trước";
        }
        // Bỏ dấu tiếng việt
        public static string RemoveUnicode(string text)
        {
            string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
            "đ",
            "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
            "í","ì","ỉ","ĩ","ị",
            "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
            "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
            "ý","ỳ","ỷ","ỹ","ỵ",};
            string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
            "d",
            "e","e","e","e","e","e","e","e","e","e","e",
            "i","i","i","i","i",
            "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
            "u","u","u","u","u","u","u","u","u","u","u",
            "y","y","y","y","y",};
            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]);
                text = text.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
            }
            return text;
        }
    }
}
