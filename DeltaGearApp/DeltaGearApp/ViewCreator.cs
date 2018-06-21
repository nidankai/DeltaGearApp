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

namespace DeltaGearApp
{
    class ViewCreator
    {
        public RelativeLayout Layout { get; private set; }
        public Button Button { get; private set; }

        public ViewCreator()
        {
            CreateView();

            Button.SetText(Resource.String.app_name);
            Button.SetX(100);
            Layout.AddView(Button);
        }

        private void CreateView()
        {
            Layout = new RelativeLayout(Application.Context);
            Button = new Button(Application.Context);
        }

        public void DestroyView()
        {
            Layout.RemoveAllViews();
        }
    }
}