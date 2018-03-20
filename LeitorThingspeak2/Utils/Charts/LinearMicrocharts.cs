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
using LeitorThingspeak2.Model;
using Microcharts;
using Microcharts.Droid;
using SkiaSharp;

namespace LeitorThingspeak2.Utils.Charts
{
    class LinearMicrocharts : IChartView<ChartView>
    {
        private ChartView chartView;

        public LinearMicrocharts (ChartView chartView)
        {
            this.chartView = chartView;
        }

        public ChartView Create(string field, ThingSpeakResponse response)
        {
            var feeds = response.Feeds;
            var channel = response.Channel;

            var max = response.Feeds.Count();
            var entries = new List<Entry>();

            feeds.ForEach(f => AddEntry(f, entries, field));

            //var chart = new BarChart() { Entries = entries };
            //var chart = new PointChart() { Entries = entries };
            var chart = new LineChart() { Entries = entries, };

            chartView.Chart = chart;
            return chartView;

        }

        private void AddEntry(Feed f, List<Entry> entries, string field)
        {
            var value = (float)(double) GetPropertyValue(f, "Field" + field);

            entries.Add(new Entry(value)
            {
                Label = f.Created_at.ToShortDateString(),
                ValueLabel = value.ToString(),
                Color = SKColor.Parse("#266489"),
                TextColor = SKColor.Parse("#266489")
            });
        }

        public ChartView Update()
        {
            throw new NotImplementedException();
        }

        private static object GetPropertyValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
    }
}