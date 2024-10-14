using System.Collections.Generic;
using System.IO;
using System.Linq;
using _game.Scripts.Managers;
using UnityEngine;

namespace _game.Scripts.Serialization
{
    public class WebDataService : IDataService
    {
        private ISerializer _serializer;
        private string _dataPath;

        public WebDataService(ISerializer serializer)
        {
            _serializer = serializer;
            _dataPath = "/idbfs/";
        }
        
        public void Save(SaveData data, bool overwrite = true)
        {
            var fileLocation = GetFilePath(data.Name);
            Debug.Log($"{fileLocation} web save location");
            if(!overwrite && File.Exists(fileLocation))
                throw new IOException($"File already exists at {fileLocation}");
            File.WriteAllText(fileLocation, _serializer.Serialize(data));
            Application.ExternalEval("FS.syncfs(false, function (err) { });");
        }

        public T Load<T>(string name)
        {
            var fileLocation = GetFilePath(name);
            
            if(!File.Exists(fileLocation))
                throw new IOException($"File does not exist at {fileLocation}");
            
            return _serializer.Deserialize<T>(File.ReadAllText(fileLocation));
        }

        public void Delete(string name)
        {
            var fileLocation = GetFilePath(name);

            if (File.Exists(fileLocation))
            {
                File.Delete(fileLocation);
                Application.ExternalEval("FS.syncfs(false, function (err) { });");
            }
            else
                throw new IOException($"File does not exist at {fileLocation}");
        }

        public void DeleteAll()
        {
            //TODO: Implement this
            return;
            foreach (var file in Directory.GetFiles(_dataPath))
            {
                File.Delete(file);
            }
        }

        public IEnumerable<string> ListAllSaves()
        {
            return Directory.EnumerateFiles(_dataPath, "*").Select(file => Path.GetFileNameWithoutExtension(file));
        }

        public bool SaveExists(string name)
        {
            return File.Exists(GetFilePath(name));
        }

        private string GetFilePath(string name)
        {
            return Path.Combine(_dataPath, name);
        }
    }
}