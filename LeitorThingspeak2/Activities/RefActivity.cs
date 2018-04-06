using Android.App;
using Android.OS;

/// <summary>
/// Exibe as bibliotes utilizadas
/// </summary>

namespace LeitorThingspeak2
{
    [Activity(Label = "RefActivity")]
    public class RefActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.References);
        }

    }
}