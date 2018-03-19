using System;
using System.Collections.Generic;
using System.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using LeitorThingspeak2.Model;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Xamarin.Android;

namespace LeitorThingspeak2
{
    public class LinearOxyPlot : IChart<PlotModel>
    {
        private PlotModel plotModel;

        public LinearOxyPlot ()
        {
            this.plotModel = new PlotModel();
        }


        // TODO: Leitura por Field
        public PlotModel Create(string field,ThingSpeakResponse data)
        {
            if (data == null) throw new Exception("Dados nulos.");

            var channel = data.Channel;
            var feeds = data.Feeds;

            var title = channel.Field1;
            plotModel.Title = title;

            var minDate = DateTimeAxis.ToDouble(feeds.ToList().First().Created_at);
            var maxDate = DateTimeAxis.ToDouble(feeds.ToList().Last().Created_at);
            var minRead = (double) feeds.Min(f => f.Field1);
            var maxRead = (double) feeds.Max(f => f.Field1);

            plotModel.Axes.Add(new DateTimeAxis { Position = AxisPosition.Bottom, Minimum = minDate, Maximum = maxDate, StringFormat = "M/d HH:mm" });
            plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Minimum = minRead, Maximum = maxRead });

            var series1 = new LineSeries
            {
                MarkerType = MarkerType.Circle,
                MarkerSize = 4,
                MarkerStroke = OxyColors.White
            };

            foreach (Feed f in feeds)
            {
                double value = f.Field1;
                double date = DateTimeAxis.ToDouble(f.Created_at);
                series1.Points.Add(new DataPoint(date, value));
            }

            plotModel.Series.Add(series1);

            return plotModel;
        }


        public PlotModel UpdateAsync()
        {
            throw new NotImplementedException();
        }
        
    }
}