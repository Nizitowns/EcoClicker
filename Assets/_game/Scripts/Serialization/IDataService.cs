namespace _game.Scripts.Serialization
{
    public interface IDataService
    {
        public bool SaveData<T>(string relativePath, T data);
        public T LoadData<T>(string relativePath);
    }
}