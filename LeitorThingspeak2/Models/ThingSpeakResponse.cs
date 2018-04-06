using System.Collections.Generic;

/// <summary>
/// Classe para receber os dados do ThingSpeak
/// (MOVER)
/// </summary>

namespace LeitorThingspeak2.Model
{
    public class ThingSpeakResponse
    {
        public Channel Channel { get; set; }
        public List<Feed> Feeds { get; set; }

        public override string ToString()
        {
            return "channel=[" + Channel.ToString() + "],feeds=[" + string.Join(";", Feeds) + "]";
        }
    }
}