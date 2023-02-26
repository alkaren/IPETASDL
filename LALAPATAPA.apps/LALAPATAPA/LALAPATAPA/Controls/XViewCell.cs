using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace LALAPATAPA.Controls
{
    public class XViewCell : ViewCell
    {
        public static readonly BindableProperty SelectedItemBackgroundColorProperty =
        BindableProperty.Create("SelectedItemBackgroundColor",
                                typeof(Color),
                                typeof(XViewCell),
                                Color.Default);

        public Color SelectedItemBackgroundColor
        {
            get { return (Color)GetValue(SelectedItemBackgroundColorProperty); }
            set { SetValue(SelectedItemBackgroundColorProperty, value); }
        }
    }
}
