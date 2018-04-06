using System.Collections.Generic;
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