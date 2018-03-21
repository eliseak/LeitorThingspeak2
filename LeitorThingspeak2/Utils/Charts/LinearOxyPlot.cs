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
using LeitorThingspeak2.Utils;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Xamarin.Android;

namespace LeitorThingspeak2
{
    public class LinearOxyPlot : IChartView<PlotView>
    {
        private PlotView plotView;
        private PlotModel plotModel;

        public LinearOxyPlot (PlotView plotView)
        {
            this.plotView = plotView;
            this.plotModel = new PlotModel();
        }
        
        public PlotView Create(string field,ThingSpeakResponse data)
        {
            if (data == null) throw new Exception("Dados nulos.");

            var channel = data.Channel;
            var feeds = data.Feeds;
            
            var title = channel.GetValueFromField(field);
            plotModel.Title = (string) title;

            var minDate = DateTimeAxis.ToDouble(feeds.ToList().First().Created_at);
            var maxDate = DateTimeAxis.ToDouble(feeds.ToList().Last().Created_at);

            var minRead = (double) feeds.Min(f => f.GetValueFromField(field));
            var maxRead = (double) feeds.Max(f => f.GetValueFromField(field));

            plotModel.Axes.Add(
                new DateTimeAxis {
                    Position = AxisPosition.Bottom,
                    Minimum = minDate,
                    Maximum = maxDate,
                    StringFormat = "M/d HH:mm"
                });
            plotModel.Axes.Add(
                new LinearAxis {
                    Position = AxisPosition.Left,
                    Minimum = minRead,
                    Maximum = maxRead
                });

            var series1 = new LineSeries
            {
                MarkerType = MarkerType.Circle,
                MarkerSize = 4,
                MarkerStroke = OxyColors.White
            };

            foreach (Feed f in feeds)
            {
                double value = f.GetValueFromField(field);
                double date = DateTimeAxis.ToDouble(f.Created_at);
                series1.Points.Add(new DataPoint(date, value));
            }

            plotModel.Series.Add(series1);

            plotView.Model = plotModel;
            return plotView;
        }
        
        public PlotView Update()
        {
            throw new NotImplementedException();
        }

    }
}