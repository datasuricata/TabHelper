﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace TabHelper.Helpers
{
    public static class Utils
    {
        /// <summary>
        /// encrypt password with MD5 format encoding
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string EncryptPassword(this string password)
        {
            if (string.IsNullOrEmpty(password))
                return "";
            
            var data = MD5.Create()
                .ComputeHash(Encoding.Default.GetBytes(password += "frg654-fh56g-19ilf3-hkyd43ds"));
            
            var builder = new StringBuilder();
            
            foreach (var x in data)
                builder.Append(x.ToString("x2").ToUpper());
            
            return builder.ToString();
        }
        
        public static string GetEnumDescription(Enum value)
        {
            DisplayAttribute attribute = value.GetType()
                .GetField(value.ToString())
                .GetCustomAttributes(typeof(DisplayAttribute), false)
                .SingleOrDefault() as DisplayAttribute;
            return attribute == null ? value.ToString() : attribute.Description;
        }
    }
}
