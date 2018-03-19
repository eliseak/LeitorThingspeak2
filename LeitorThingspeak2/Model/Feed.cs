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

namespace LeitorThingspeak2
{
    class Feed
    {
        public DateTime Created_at { get; set; }
        public int Entry_id { get; set; }
        public double Field1 { get; set; }
        public double Field2 { get; set; }

        public override string ToString()
        {
            return "created_at=" + Created_at +
                    ",entry_id=" + Entry_id +
                    ",field1=" + Field1 +
                    ",field2=" + Field2;
        }
    }

}