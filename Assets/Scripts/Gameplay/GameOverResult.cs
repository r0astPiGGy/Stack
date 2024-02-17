namespace Gameplay
{
    public record GameOverResult(GameData GameData, int Score)
    {
        public GameData GameData { get; } = GameData;
        public int Score { get; } = Score;
    }
}