namespace MyLibrary.Manager
{
    enum GameState
    {
        Play,
        Pause,
        Resume,
        End,
        None
    }

    enum InGameState
    {
        Win,
        Lose,
        Draw,
        None
    }

    public interface IGameState
    {
        void GamePlayCallBack();
        void GamePauseCallBack();
        void GameResumeCallBack();
        void GameEndCallBack();
    }

    public interface IInGameState
    {
        void InGameWinCallback();
        void InGameLoseCallback();
        void InGameDrawCallback();
    }
}