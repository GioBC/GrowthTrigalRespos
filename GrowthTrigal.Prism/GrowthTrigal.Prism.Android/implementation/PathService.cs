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

  
[assembly: Xamarin.Forms.Dependency(typeof(GrowthTrigal.Prism.Droid.implementation.PathService))]

namespace GrowthTrigal.Prism.Droid.implementation
{
    using GrowthTrigal.Common.Interfaces;
    using System;
    using System.IO;

    public class PathService : IPathService
    {
        public string GetDatabasePath() 
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, "GrowthTrigal.db3");
        }

    }
}