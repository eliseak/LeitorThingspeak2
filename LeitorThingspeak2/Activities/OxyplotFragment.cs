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
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Xamarin.Android;

namespace LeitorThingspeak2
{
    public class OxyplotFragment : Fragment
    {
        private PlotView plotView;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.OxyplotTab, container, false);
            plotView = view.FindViewById<PlotView>(Resource.Id.plot_view);

            return view;
        }

        public async override void OnStart()
        {
            base.OnStart();

            var channel = Resources.GetString(Resource.String.channel);
            var field = Resources.GetString(Resource.String.field);
            var results = Convert.ToInt32(Resources.GetString(Resource.String.num_results));

            var response = await new RequestThingSpeakData(channel, field).CustomAsync(results);

            plotView.Model = new LinearOxyPlot().Create(field,response);
        }

        //private async Task<PlotModel> CreatePlotModelAsync()
        //{
        //    var plotModel = new PlotModel { Title = "OxyPlot Demo" };
        //    var series1 = new LineSeries
        //    {
        //        MarkerType = MarkerType.Circle,
        //        MarkerSize = 4,
        //        MarkerStroke = OxyColors.White
        //    };

        //    var channel = Resources.GetString(Resource.String.channel);
        //    var num_results = Resources.GetString(Resource.String.num_results);
        //    string url = "https://api.thingspeak.com/channels/"+ channel +
        //                 "/feeds.json?results=" + num_results;

        //    // Create an HTTP web request using the URL:
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

        //            for (int i = 0; i < max; i++)
        //            {
        //                //Console.WriteLine(jsonDoc["feeds"][i]["created_at"].ToString().Trim().Substring(1, 20));

        //                DateTime created_at = DateTime.Parse(jsonDoc["feeds"][i]["created_at"].ToString().Substring(1, 20));
        //                if (i == 0)
        //                {
        //                    plotModel.Axes.Add(new DateTimeAxis { Position = AxisPosition.Bottom, Minimum = DateTimeAxis.ToDouble(created_at), Maximum = DateTimeAxis.ToDouble(DateTime.Now), StringFormat = "M/d HH:mm" });
        //                    plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Maximum = 50, Minimum = 25 });

        //                }
        //                double value = Convert.ToDouble(jsonDoc["feeds"][i]["field2"].ToString().Substring(1, 4));
        //                double date = DateTimeAxis.ToDouble(created_at);
        //                series1.Points.Add(new DataPoint(date, value));

        //            }
        //        }
        //    }
            
        //    plotModel.Series.Add(series1);

        //    return plotModel;
        //}

    }
}