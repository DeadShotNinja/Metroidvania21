namespace Metro
{
    public struct TimeChangedEvent
    {
        public bool IsPresent;
        public float SwapDuration;

        public TimeChangedEvent(bool toPresent, float swapDuration)
        {
            IsPresent = toPresent;
            SwapDuration = swapDuration;
        }
    }
}
