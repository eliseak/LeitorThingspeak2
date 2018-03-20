using System;
using System.Collections.Generic;
using System.IO;
using System.Json;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using LeitorThingspeak2.Utils;
using LeitorThingspeak2.Utils.Charts;
using Microcharts;
using Microcharts.Droid;
using SkiaSharp;

namespace LeitorThingspeak2
{
    public class MicrochartsFragment : Fragment
    {
        private ChartView chartView;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.MicrochartTab, container, false);
            chartView = view.FindViewById<ChartView>(Resource.Id.chartView);

            return view;
        }

        public async override void OnStart()
        {
            base.OnStart();

            var channel = Resources.GetString(Resource.String.channel);
            var field = Resources.GetString(Resource.String.field);
            var results = Convert.ToInt32(Resources.GetString(Resource.String.num_results));

            var response = await new RequestThingSpeakData(channel, field).CustomAsync(results);

            new LinearMicrocharts(chartView).Create(field, response);
        }

        //public async override void OnStart()
        //{
        //    base.OnStart();

        //    var channel = Resources.GetString(Resource.String.channel);
        //    var num_results = Resources.GetString(Resource.String.num_results);

        //    // HTTP Request to Thingspeak
        //    string url = "https://api.thingspeak.com/channels/"+ channel + 
        //                 "/feeds.json?results=" + num_results;

        //    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
        //    request.ContentType = "application/json";
        //    request.Method = "GET";

        //    // Send the request to the server and wait for the response:
        //    using (WebResponse response = await request.GetResponseAsync())
        //    {
        //        // Get a stream representation of the HTTP web response:
        //        using (Stream stream = response.GetResponseStream())
        //        {
        //            // Use this stream to build a JSON document object:
        //            JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));

        //            // Return the JSON document:
        //            Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());
        //            int max = jsonDoc["feeds"].Count;

        //            //Console.WriteLine(jsonDoc["channel"].ToString());
        //            //Console.WriteLine(jsonDoc["feeds"].ToString());
        //            Console.WriteLine("Teste: " + jsonDoc["feeds"][0]["field2"].ToString().Substring(1, 4));

        //            var entries = new Entry[max];

        //            for (int i = 0; i < max; i++)
        //            {
        //                var time = DateTime.Parse(jsonDoc["feeds"][i]["created_at"].ToString().Substring(1, 20)).ToString();
                        
        //                if (i % 2 == 1)
        //                {
        //                    entries[i] = new Entry(jsonDoc["feeds"][i]["field1"])
        //                    {
        //                        Label = time,
        //                        ValueLabel = jsonDoc["feeds"][i]["field2"].ToString().Substring(1, 4),
        //                        Color = SKColor.Parse("#266489"),
        //                        TextColor = SKColor.Parse("#266489")
        //                    };
        //                }
        //                else
        //                {
        //                    entries[i] = new Entry(jsonDoc["feeds"][i]["field1"])
        //                    {
        //                        Label = time,
        //                        ValueLabel = jsonDoc["feeds"][i]["field2"].ToString().Substring(1, 4),
        //                        Color = SKColor.Parse("#90D585"),
        //                        TextColor = SKColor.Parse("#90D585")
        //                    };
        //                }
        //            }

        //            // var chart = new BarChart() { Entries = entries };
        //            // var chart = new PointChart() { Entries = entries };
        //            var chart = new LineChart() { Entries = entries, };

        //            chartView.Chart = chart;
        //        }
        //    }

        //}
    }
}