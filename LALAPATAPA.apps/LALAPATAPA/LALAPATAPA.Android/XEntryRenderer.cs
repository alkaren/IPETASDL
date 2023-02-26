using Xamarin.Forms;
using LALAPATAPA.Controls;
using Xamarin.Forms.Platform.Android;
using Android.Graphics.Drawables;
using LALAPATAPA.Droid;

[assembly: ExportRenderer(typeof(XEntry), typeof(XEntryRenderer))]
namespace LALAPATAPA.Droid
{
    public class XEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.Background = new ColorDrawable(Android.Graphics.Color.Transparent);
            }
        }
    }
}