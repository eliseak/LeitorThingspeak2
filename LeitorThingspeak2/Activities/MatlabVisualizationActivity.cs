﻿using Android.App;
using Android.OS;
using Android.Webkit;

/// <summary>
/// Exibe gráfico iframe(WebView) - defeituoso
/// </summary>
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