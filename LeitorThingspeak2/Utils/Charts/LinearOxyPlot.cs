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
        private string field;

        public LinearOxyPlot (PlotView plotView, string field)
        {
            this.plotView = plotView;
            this.field = field;
        }
        
        // Método que cria o gráfico 
        public PlotView Create(ThingSpeakResponse data)
        {
            if (data == null) throw new Exception("Dados nulos.");

            PlotModel plotModel = new PlotModel
            {
                Title = data.Channel.GetValueFromField(field)
            };
            
            var feeds = data.Feeds;
            
            // Inicializações
            InitializeAxis(plotModel, feeds);
            InitializeLineSeries(plotModel, feeds);
            
            plotView.Model = plotModel;
            return plotView;
        }

        // Método que inicializa uma linha do gráfico
        private void InitializeLineSeries(PlotModel plotModel, IList<Feed> feeds)
        {
            var series1 = new LineSeries
            {
                MarkerType = MarkerType.Circle,
                MarkerSize = 4,
                MarkerStroke = OxyColors.White
            };

            // Adiciona cada feed como ponto no gráfico
            foreach (Feed f in feeds)
            {
                double value = f.GetValueFromField(field);
                double date = DateTimeAxis.ToDouble(f.Created_at);
                series1.Points.Add(new DataPoint(date, value));
            }

            // Adiciona a linha no gráfico
            plotModel.Series.Add(series1);
        }

        // Método que inicializa os eixcos do gráfico
        private void InitializeAxis(PlotModel plotModel, IList<Feed> feeds)
        {
            if (feeds == null) throw new ArgumentNullException(nameof(feeds));

            var minDate = DateTimeAxis.ToDouble(feeds.ToList().First().Created_at);
            var maxDate = DateTimeAxis.ToDouble(feeds.ToList().Last().Created_at.AddMinutes(10));

            var minRead = feeds.Min(f => f.GetValueFromField(field) - 0.5);
            var maxRead = feeds.Max(f => f.GetValueFromField(field) + 0.5);

            plotModel.Axes.Add(
                new DateTimeAxis
                {
                    Position = AxisPosition.Bottom,
                    Minimum = minDate,
                    Maximum = maxDate,
                    StringFormat = "M/d HH:mm"
                });
            plotModel.Axes.Add(
                new LinearAxis
                {
                    Position = AxisPosition.Left,
                    Minimum = minRead,
                    Maximum = maxRead
                });
        }

        // Método que atualiza o gráfico
        public PlotView Update(IList<Feed> feeds)
        {
            if (plotView.Model == null) throw new ArgumentNullException(nameof(plotView.Model));

            LineSeries series1 = (LineSeries) plotView.Model.Series.FirstOrDefault();

            foreach (Feed f in feeds)
            {
                double value = f.GetValueFromField(field);
                double date = DateTimeAxis.ToDouble(f.Created_at);

                var point = new DataPoint(date, value);

                if (!series1.Points.Contains(point))
                {
                    series1.Points.Add(point);
                }
                
            }

            plotView.Model.InvalidatePlot(true); // Atualiza os dados

            return plotView;
        }

    }
}