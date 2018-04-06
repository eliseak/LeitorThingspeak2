﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using LeitorThingspeak2.Utils;

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