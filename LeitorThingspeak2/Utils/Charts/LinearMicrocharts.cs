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
        private string field;

        public LinearMicrocharts (ChartView chartView, string field)
        {
            this.chartView = chartView;
            this.field = field;
        }

        public ChartView Create(ThingSpeakResponse response)
        {
            var feeds = response.Feeds;
            var channel = response.Channel;

            var max = response.Feeds.Count();
            var entries = new List<Entry>();

            feeds.ForEach(f => AddEntry(f, entries, field));

            //var chart = new BarChart() { Entries = entries };
            //var chart = new PointChart() { Entries = entries };
            var chart = new LineChart() { Entries = entries, LineMode = LineMode.Straight };

            chartView.Chart = chart;
            return chartView;

        }

        private void AddEntry(Feed f, List<Entry> entries, string field)
        {
            var value = (float) f.GetValueFromField(field);

            entries.Add(new Entry(value)
            {
                Label = f.Created_at.ToShortDateString(),
                ValueLabel = value.ToString(),
                Color = SKColor.Parse("#266489"),
                TextColor = SKColor.Parse("#266489")
            });
        }

        public ChartView Update(IList<Feed> feeds)
        {
            throw new NotImplementedException();
        }

    }
}