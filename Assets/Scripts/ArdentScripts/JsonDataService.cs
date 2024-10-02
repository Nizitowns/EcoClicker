using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft;
using System.IO;
using Newtonsoft.Json;
using System;

public class JsonDataService : IDataService
{
    
public bool SaveData<T>(string RelativePath, T Data, bool isEncrypted)
    {
        string path = Application.persistentDataPath + RelativePath;
        if (File.Exists(path))
        {
            try
            {
                Debug.Log("Save File exists. Deleting old file and writing a new one!");
                File.Delete(path);
                using FileStream stream = File.Create(path);
                stream.Close();
                File.WriteAllText (path, JsonConvert.SerializeObject(Data));
                return true;
            }

            catch(Exception e)
            {
               Debug.LogError($"Error file failed to save");
               return false;
            }
        }

        else
        {
            try
            {Debug.Log("Creating Save Data for the first time");
            using FileStream stream = File.Create(path);
            stream.Close();
            File.WriteAllText(path, JsonConvert.SerializeObject(Data));
            return true;
            }

            catch (Exception e)
            {
                Debug.LogError($"Error unable to save file due to: {e.Message} {e.StackTrace}");
                return false;
            }
        }

    }    
    public T LoadData<T>(string RelativePath, bool isEncrypted)
    {
        string path = Application.persistentDataPath + RelativePath;
        if(!File.Exists(path))
        {
            Debug.LogError($"Error. Failed To Load {path}.");
            throw new FileNotFoundException($"{path} Does not exist");
        }
        try
        {
            T data = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
            return data;
        }

        catch(Exception e)
        {
            Debug.LogError($"This is the Error {e.Message} {e.StackTrace}");
            throw e;
        }
        
        
        

    }
}

