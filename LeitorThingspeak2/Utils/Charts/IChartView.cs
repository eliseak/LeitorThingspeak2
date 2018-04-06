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

/// <summary>
/// Interface para criar gráficos
/// (VERIFICAR)
/// </summary>

namespace LeitorThingspeak2
{
    public interface IChartView<T>
    {
        T Create(ThingSpeakResponse response);

        T Update(IList<Feed> feeds);
    }
}