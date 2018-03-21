using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace LeitorThingspeak2.Utils
{
    public class PropValueSearcher
    {
        public static object ByName(object src, string propName) 
            => src.GetType().GetProperty(propName).GetValue(src, null);
    }
}