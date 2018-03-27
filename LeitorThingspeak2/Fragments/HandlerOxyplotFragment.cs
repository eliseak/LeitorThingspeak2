using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using LeitorThingspeak2.Utils;
using OxyPlot.Xamarin.Android;

namespace LeitorThingspeak2.Fragments
{
    public class HandlerOxyplotFragment : Fragment
    {
        private Context context; 
        private PlotView plotView;
        private Handler handler;
        private int time = 5000;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.OxyplotTab, container, false);
            context = view.Context;

            plotView = view.FindViewById<PlotView>(Resource.Id.plot_view);

            return view;
        }

        public override void OnStart()
        {
            base.OnStart();

            handler = new Handler();
            handler.PostDelayed(UpdateChartAsync, time);

        }

        private async void UpdateChartAsync()
        {
            Toast.MakeText(context, "!", ToastLength.Short).Show();

            var channel = Resources.GetString(Resource.String.channel);
            var field = Resources.GetString(Resource.String.field);
            var results = Convert.ToInt32(Resources.GetString(Resource.String.num_results));

            var response = await new RequestThingSpeakData(channel, field).CustomAsync(results);

            if (response != null) new LinearOxyPlot(plotView).Create(field, response);
            handler.PostDelayed(UpdateChartAsync, 5000);
        }

        public override void OnPause()
        {
            base.OnPause();
            handler.RemoveCallbacks(UpdateChartAsync);
        }
    }


}