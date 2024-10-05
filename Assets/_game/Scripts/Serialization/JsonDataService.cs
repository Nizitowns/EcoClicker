using System;
using System.IO;
using _game.Scripts.Managers;
using UnityEngine;
using Newtonsoft.Json;

namespace _game.Scripts.Serialization
{
    //TODO: open file and write to it as needed
    //TODO: Cleanup and refactor
    public class JsonDataService : IDataService
    {
        public bool SaveData<T>(string relativePath, T data)
        {
            
            var path = SetPath(relativePath);
            if (File.Exists(path))
            {
                try
                {
                    Debug.Log($"Save File {path} exists. Deleting old file and writing a new one!");
                    File.Delete(path);
                    using FileStream stream = File.Create(path);
                    stream.Close();
                    File.WriteAllText(path, JsonConvert.SerializeObject(data, Formatting.Indented));
#if UNITY_WEBGL && !UNITY_EDITOR
                    Application.ExternalEval("FS.syncfs(false, function (err) { console.log(err); });");
#endif
                    return true;
                }

                catch (Exception e)
                {
                    Debug.LogError($"Error unable to save file due to: {e.Message} {e.StackTrace}");
                    return false;
                }
            }

            else
            {
                try
                {
                    Debug.Log($"Creating {path} Save Data for the first time");
                    using FileStream stream = File.Create(path);
                    stream.Close();
                    File.WriteAllText(path, JsonConvert.SerializeObject(data, Formatting.Indented));
#if UNITY_WEBGL && !UNITY_EDITOR
                    Application.ExternalEval("FS.syncfs(false, function (err) { console.log(err); });");
#endif
                    return true;
                }

                catch (Exception e)
                {
                    Debug.LogError($"Error unable to save file due to: {e.Message} {e.StackTrace}");
                    return false;
                }
            }
        }

        public T LoadData<T>(string relativePath)
        {
            var path = SetPath(relativePath);
            Debug.Log($"Loading Data from {path}");
            if (!File.Exists(path))
            {
                Debug.LogError($"Error. Failed To Load {path}.");
                throw new FileNotFoundException($"{path} Does not exist");
            }
            try
            {
                return JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
            }
            catch (Exception e)
            {
                Debug.LogError($"Error unable to load file due to: {e.Message} {e.StackTrace}");
                return default;
            }
        }
        
        private string SetPath(string relativePath)
        {
            
            var path = Path.Combine(Application.persistentDataPath, relativePath);
#if UNITY_WEBGL && !UNITY_EDITOR
            path = Path.Combine("/idbfs/", relativePath);   //Sets the path to the IndexedDB file system
#endif
            return path;
        }

    }
}