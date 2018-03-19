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
    public class Feed
    {
        public DateTime Created_at { get; set; }
        public int Entry_id { get; set; }
        public double Field1 { get; set; }
        public double Field2 { get; set; }
        public double Field3 { get; set; }
        public double Field4 { get; set; }
        public double Field5 { get; set; }
        public double Field6 { get; set; }
        public double Field7 { get; set; }
        public double Field8 { get; set; }

        public override string ToString()
        {
            return "created_at=" + Created_at +
                    ",entry_id=" + Entry_id +
                    ",field1=" + Field1 +
                    ",field2=" + Field2 +
                    ",field3=" + Field3 +
                    ",field4=" + Field4 +
                    ",field5=" + Field5 +
                    ",field6=" + Field6 +
                    ",field7=" + Field7 +
                    ",field8=" + Field8;
        }
    }

}