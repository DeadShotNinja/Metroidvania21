namespace Metro
{
	public struct ScreenFadeEvent
	{
		public ScreenFadeType FadeType;
		public float FadeDuration;
		
		public ScreenFadeEvent(ScreenFadeType fadeType, float fadeDuration)
		{
			FadeType = fadeType;
			FadeDuration = fadeDuration;
		}
	}
}