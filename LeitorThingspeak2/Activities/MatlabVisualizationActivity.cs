using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;

namespace LeitorThingspeak2
{
    [Activity(Label = "MatlabVisualizationActivity")]
    public class MatlabVisualizationActivity : Activity
    {

        private WebView webView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            SetContentView(Resource.Layout.MatlabVisualization);

            webView = FindViewById<WebView>(Resource.Id.webView);

            WebSettings webSettings = InitializeWebSettings(webView);

            webView.LoadUrl("https://thingspeak.com/apps/matlab_visualizations/212571");

            //string HTMLText = "<html><body>" + 
            //    "<iframe width='450' height='260' style='border: 1px solid #cccccc;' src='https://thingspeak.com/apps/matlab_visualizations/212571'></iframe></ body > </html>";
            //webView.LoadData(HTMLText, "text/html", null);
        }

        public WebSettings InitializeWebSettings(WebView webView)
        {
            WebSettings webSettings = webView.Settings;
            webSettings.JavaScriptEnabled = true;
            webSettings.DomStorageEnabled = true;
            webSettings.LoadWithOverviewMode = true;
            webSettings.UseWideViewPort = true;
            webSettings.BuiltInZoomControls = true;
            webSettings.DisplayZoomControls = false;
            webSettings.SetSupportZoom(true);
            webSettings.DefaultTextEncodingName = "utf-8";
            return webSettings;
        }


    }
}