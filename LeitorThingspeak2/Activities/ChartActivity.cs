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

/// <summary>
/// Exibe o gráfico de um campo num canal do ThingSpeak.
/// A leitura nas duas abas são iguais, mudando apenas a biblioteca que contrói o gráfico
/// </summary>

namespace LeitorThingspeak2
{
    [Activity(Label = "ChartActivity", Theme = "@android:style/Theme.DeviceDefault.Light")]
    public class ChartActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Chart);
            this.ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;

            // Adicionando as tabs
            AddTab("MicroCharts", Resource.Drawable.ic_show_chart_black_24dp, new MicrochartsFragment());
            AddTab("Oxyplot", Resource.Drawable.ic_show_chart_black_24dp, new OxyplotFragment());
        }

        /*
		 * Método para criar e adicionar tabs na View
		 * @Param,
		 *  tabText: Texto da tab
		 *  iconResourceId: ID do ícone da tab
		 *  fragment: referência do Fragment
		*/
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