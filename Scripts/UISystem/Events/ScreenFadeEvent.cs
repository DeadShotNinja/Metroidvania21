namespace Metro
{
	public struct ScreenFadeEvent
	{
		public ScreenFadeType FadeType;
		public float FadeSpeed;
		
		public ScreenFadeEvent(ScreenFadeType fadeType, float fadeSpeed)
		{
			FadeType = fadeType;
			FadeSpeed = fadeSpeed;
		}
	}
}