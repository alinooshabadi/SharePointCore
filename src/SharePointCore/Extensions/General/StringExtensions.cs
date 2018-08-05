﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharePointCore.Extensions.General
{
    public static class StringExtensions
    {
        /// <summary>
        /// Indicates whether the specified System.String object is null or an System.String.Empty string
        /// </summary>
        /// <param name="instance">todo: describe instance parameter on IsNullOrEmpty</param>
        public static bool IsNullOrEmpty(this string instance)
        {
            return string.IsNullOrEmpty(instance);
        }
    }
}
