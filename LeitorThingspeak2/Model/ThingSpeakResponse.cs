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

namespace LeitorThingspeak2.Model
{
    public class ThingSpeakResponse
    {
        public Channel Channel { get; set; }
        public List<Feed> Feeds { get; set; }

        public override string ToString()
        {
            return "channel=[" + Channel.ToString() + "],feeds=[" + string.Join(";", Feeds) + "]";
        }
    }
}