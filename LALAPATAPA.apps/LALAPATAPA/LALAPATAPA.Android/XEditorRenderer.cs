using Xamarin.Forms;
using LALAPATAPA.Controls;
using Xamarin.Forms.Platform.Android;
using Android.Graphics.Drawables;
using LALAPATAPA.Droid;

[assembly: ExportRenderer(typeof(XEditor), typeof(XEditorRenderer))]
namespace LALAPATAPA.Droid
{
    public class XEditorRenderer : EditorRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.Background = new ColorDrawable(Android.Graphics.Color.Transparent);
            }
        }
    }
}