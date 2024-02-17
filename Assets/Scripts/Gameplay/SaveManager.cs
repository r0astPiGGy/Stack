using Utilities;

namespace Gameplay
{
    public class SaveManager : ISaveManager
    {
        private const string FileName = "save.dat";
        
        public void Save(GameData gameData)
        {
            var json = gameData.ToJson();

            FileManager.WriteToFile(FileName, json);
        }

        public GameData Load()
        {
            var data = new GameData();

            if (!FileManager.FileExists(FileName)) return data; 
            
            if (FileManager.LoadFromFile(FileName, out var json))
            {
                data.LoadFromJson(json);
            }
            
            return data;
        }
    }
}