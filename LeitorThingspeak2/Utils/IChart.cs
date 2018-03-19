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

namespace LeitorThingspeak2
{
    interface IChart
    { 
        Task<IChart> CreateAsync(ThingSpeakResponse response);

        Task<IChart> UpdateAsync(IChart chart);
    }
}