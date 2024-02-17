namespace Gameplay
{
    public interface ISaveManager
    {

        public void Save(GameData gameData);

        public GameData Load();

    }
}