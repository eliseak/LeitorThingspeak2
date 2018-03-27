using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using LeitorThingspeak2.Fragments;

namespace LeitorThingspeak2.Activities
{
    [Activity(Label = "HandlerChartActivity", Theme = "@android:style/Theme.DeviceDefault.Light")]
    public class HandlerChartActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Chart);
            this.ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;

            // Adding tabs
            // AddTab("MicroCharts", Resource.Drawable.ic_show_chart_black_24dp, new MicrochartsFragment());
            AddTab("Oxyplot", Resource.Drawable.ic_show_chart_black_24dp, new HandlerOxyplotFragment());
        }

        void AddTab(string tabText, int iconResourceId, Fragment fragment)
        {
            var tab = this.ActionBar.NewTab();
            tab.SetText(tabText);
            tab.SetIcon(iconResourceId);

            // Event handler for replacing tabs tab when selected
            tab.TabSelected += delegate (object sender, ActionBar.TabEventArgs e)
            {
                e.FragmentTransaction.Replace(Resource.Id.fragmentContainer, fragment);
            };

            this.ActionBar.AddTab(tab);
        }

    }
}