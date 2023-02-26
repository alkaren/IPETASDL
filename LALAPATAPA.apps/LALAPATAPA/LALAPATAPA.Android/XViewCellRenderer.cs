using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using LALAPATAPA.Controls;
using LALAPATAPA.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ExportRenderer(typeof(XViewCell), typeof(XViewCellRenderer))]
namespace LALAPATAPA.Droid
{
    public class XViewCellRenderer : ViewCellRenderer
    {
        private Android.Views.View _cell;
        private Drawable _normalBackground; // add this line
        private bool _isSelected;

        protected override Android.Views.View GetCellCore(Cell item, Android.Views.View convertView, ViewGroup parent, Context context)
        {
            _cell = base.GetCellCore(item, convertView, parent, context);

            _normalBackground = _cell.Background; // add this line
            _isSelected = false;

            return _cell;
        }

        protected override void OnCellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnCellPropertyChanged(sender, e);

            if (e.PropertyName == "IsSelected")
            {
                _isSelected = !_isSelected;

                if (_isSelected)
                {
                    _cell.SetBackgroundColor(Color.FromHex("#E6FFF2").ToAndroid());
                }
                else
                {
                    _cell.Background = _normalBackground; // change this line
                }
            }
        }
    }
}