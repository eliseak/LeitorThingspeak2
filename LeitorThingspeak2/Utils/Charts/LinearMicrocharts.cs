using System;
using System.Collections.Generic;
using System.Linq;
using LeitorThingspeak2.Model;
using Microcharts;
using Microcharts.Droid;
using SkiaSharp;

/// <summary>
/// Classe que auxilia a criação do gráfico com Microcharts
/// </summary>

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

        // Método paea criar o gráfico 
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

        // Método para adicionar pontos numa linha do gráfico
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

        // TODO: Update Microcharts
        public ChartView Update(IList<Feed> feeds)
        {
            throw new NotImplementedException();
        }

    }
}