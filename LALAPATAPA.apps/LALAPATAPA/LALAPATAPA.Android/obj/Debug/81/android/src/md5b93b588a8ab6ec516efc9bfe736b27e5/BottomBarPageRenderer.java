package md5b93b588a8ab6ec516efc9bfe736b27e5;


public class BottomBarPageRenderer
	extends md51558244f76c53b6aeda52c8a337f2c37.VisualElementRenderer_1
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onAttachedToWindow:()V:GetOnAttachedToWindowHandler\n" +
			"n_onDetachedFromWindow:()V:GetOnDetachedFromWindowHandler\n" +
			"n_onLayout:(ZIIII)V:GetOnLayout_ZIIIIHandler\n" +
			"";
		mono.android.Runtime.register ("BottomBar.Droid.Renderers.BottomBarPageRenderer, BottomBar.Droid", BottomBarPageRenderer.class, __md_methods);
	}


	public BottomBarPageRenderer (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == BottomBarPageRenderer.class)
			mono.android.TypeManager.Activate ("BottomBar.Droid.Renderers.BottomBarPageRenderer, BottomBar.Droid", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public BottomBarPageRenderer (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == BottomBarPageRenderer.class)
			mono.android.TypeManager.Activate ("BottomBar.Droid.Renderers.BottomBarPageRenderer, BottomBar.Droid", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public BottomBarPageRenderer (android.content.Context p0)
	{
		super (p0);
		if (getClass () == BottomBarPageRenderer.class)
			mono.android.TypeManager.Activate ("BottomBar.Droid.Renderers.BottomBarPageRenderer, BottomBar.Droid", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public void onAttachedToWindow ()
	{
		n_onAttachedToWindow ();
	}

	private native void n_onAttachedToWindow ();


	public void onDetachedFromWindow ()
	{
		n_onDetachedFromWindow ();
	}

	private native void n_onDetachedFromWindow ();


	public void onLayout (boolean p0, int p1, int p2, int p3, int p4)
	{
		n_onLayout (p0, p1, p2, p3, p4);
	}

	private native void n_onLayout (boolean p0, int p1, int p2, int p3, int p4);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
