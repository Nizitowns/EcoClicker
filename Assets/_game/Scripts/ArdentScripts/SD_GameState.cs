using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;
using System.IO;
public class SD_GameState : MonoBehaviour
{
     DeciLevel01 deciLevel01 = new DeciLevel01();
    Scene scene;
    IDataService dataService = new JsonDataService();
    // Start is called before the first frame update
    void Start()
    {
   scene = SceneManager.GetActiveScene();
        if(scene.name == "DeciLevel01")
        {
           DeciLevel01 deciLevel01 = LoadData<DeciLevel01>("DeciLevel01.json");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Saves Data to AppData>Local>DefaultCompany>DeciLevel01.json

      //  dataService.SaveData("/DeciLevel01.json", deciLevel01, false);
    }
     public void SaveData<T>(string relativePath, T data)
    {
        string path = Application.persistentDataPath + relativePath;

        string jsonData = JsonConvert.SerializeObject(data);
        File.WriteAllText(path, jsonData);
    }

    public T LoadData<T>(string relativePath)
    {
        string path = Application.persistentDataPath + relativePath;

        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<T>(jsonData);
        }
        else
        {
            // If the file doesn't exist, return a new instance of T
            return default(T);
        }
    }
}
