using System.Collections.Generic;
using _game.Scripts.Managers;

namespace _game.Scripts.Serialization
{
    public interface IDataService
    {
        public void Save(SaveData data, bool overwrite = true);
        public T Load<T>(string name);
        public void Delete(string name);
        public void DeleteAll();
        public IEnumerable<string> ListAllSaves();
        
        public bool SaveExists(string name);
    }
}