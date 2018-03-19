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
using OxyPlot.Xamarin.Android;

namespace LeitorThingspeak2
{
    class OxyPlot : IChart
    {
        private PlotView plotView;

        public async Task<IChart> CreateAsync(ThingSpeakResponse response)
        {
            throw new NotImplementedException();
        }

        public async Task<IChart> UpdateAsync(IChart chart)
        {
            throw new NotImplementedException();
        }
    }
}