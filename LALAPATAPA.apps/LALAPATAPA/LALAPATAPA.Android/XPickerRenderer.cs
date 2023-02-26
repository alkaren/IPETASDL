using Xamarin.Forms;
using LALAPATAPA.Controls;
using Xamarin.Forms.Platform.Android;
using Android.Graphics.Drawables;
using LALAPATAPA.Droid;

[assembly: ExportRenderer(typeof(XPicker), typeof(XPickerRenderer))]
namespace LALAPATAPA.Droid
{
    public class XPickerRenderer : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.Background = new ColorDrawable(Android.Graphics.Color.Transparent);
            }
        }
    }
}