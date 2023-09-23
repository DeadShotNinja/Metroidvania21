namespace Metro
{
    public struct GameStateChangedEvent
    {
        public GameState State;

        public GameStateChangedEvent(GameState state)
        {
            State = state;
        }
    }
}
