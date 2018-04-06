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

/// <summary>
/// Exibe o gráfico de um campo num canal do ThingSpeak.
/// Possui a tab que atualiza automaticamente o gráfico Oxyplot.
/// </summary>

namespace LeitorThingspeak2.Activities
{
    [Activity(Label = "HandlerChartActivity", Theme = "@android:style/Theme.DeviceDefault.Light")]
    public class AsyncChartActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Chart);
            this.ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;

            // Adicionando a tab
            AddTab("Oxyplot", Resource.Drawable.ic_show_chart_black_24dp, new AsyncOxyplotFragment());
        }

        // Método para adicionar tab
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