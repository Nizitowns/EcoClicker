using System;
using System.Collections.Generic;
using System.Linq;
using _game.Scripts.GameData;
using _game.Scripts.Serialization;
using _game.Scripts.Utility;
using UnityEngine;
using _game.Scripts.Serialization;
using _game.Scripts.UI;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace _game.Scripts.Managers
{
    public class SaveLoadManager : PersistentSingleton<SaveLoadManager>
    {
        [Space(20)]
        [SerializeField] private GameDataSo newGameData;
        [SerializeField] private bool overwriteSave = true;
        [SerializeField] private SaveData saveData;
        [SerializeField] private string saveName = "SaveData";
        [SerializeField] private string mainSceneName = "Main";
        
        private IDataService dataService;
        
        protected override void Awake()
        {
            base.Awake();
            dataService = new FileDataService(new JsonSerializer());
            
            if (!dataService.SaveExists(saveData.Name))
                NewGame();
        }
        
        void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        
        void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode sceneLoadMode)
        {
            if(scene.name == mainSceneName)
                return;
            Bind<StoreManager, StoreManagerData>(saveData.StoreManagerData);
        }

        public void Bind<T, TData>(TData data) where T : MonoBehaviour, IBind<TData> where TData : ISaveable, new()
        {
            var entity = FindObjectsByType<T>(FindObjectsSortMode.None).FirstOrDefault();
            if (entity != null)
            {
                if (data == null)
                {
                    data = new TData{Id = entity.Id
                };
                }
                entity.Bind(data);
            }
        }
        
        public void BindAll<T, TData>(List<TData> data) where T : MonoBehaviour, IBind<TData> where TData : ISaveable, new()
        {
            var entities = FindObjectsByType<T>(FindObjectsSortMode.None);
            foreach (var entity in entities)
            {
                var d = data.FirstOrDefault(x => x.Id == entity.Id);
                if (d == null)
                {
                    d = new TData{Id = entity.Id};
                    data.Add(d);
                }
                entity.Bind(d);
            }
        }

        [ContextMenu("New Game")]
        public void NewGame()
        {
            var storeManagerData = new StoreManagerData
            {
                CurrentBalance = newGameData.StartingBalance,
                Stores = new StoreData[newGameData.Stores.Length]
            };

            for (var i = 0; i < newGameData.Stores.Length; i++)
            {
                if (storeManagerData.Stores[i] != null) continue;
                var store = newGameData.Stores[i];
                storeManagerData.Stores[i] = new StoreData
                {
                    Id = store.Id,
                    StoreName = store.StoreName,
                    BaseStoreCost = store.BaseStoreCost,
                    BaseStoreProfit = store.BaseStoreProfit,
                    StoreTimer = store.StoreTimer,
                    StoreCount = store.StoreCount,
                    StoreMultiplier = store.StoreMultiplier,
                    StoreTimerDivision = store.StoreTimerDivision,
                    ManagerCost = store.ManagerCost,
                };
            }
            
            saveData.Name = saveName;
            saveData.StoreManagerData = storeManagerData;
            saveData.CurrentSceneName = SceneManager.GetActiveScene().name;
            SaveGame();
            LoadGame(saveData.Name); //TODO: fix this workaround
        }

        [ContextMenu("Save Game")]
        private void SaveGame() => dataService.Save(saveData, overwriteSave);
        
        public void LoadGame(string saveName)
        {
            saveData = dataService.Load<SaveData>(saveName);
            
            if(String.IsNullOrEmpty(saveData.CurrentSceneName))
                saveData.CurrentSceneName = mainSceneName;
            
            SceneManager.LoadScene(saveData.CurrentSceneName);
        }
        
        [ContextMenu("Reload Game")]
        public void ReloadGame() => LoadGame(saveData.Name);
        
        public void DeleteSaveGame(string saveName) => dataService.Delete(saveName);
    }
    
    [Serializable]
    public class SaveData
    {
        public string Name;
        public string CurrentSceneName;
        public StoreManagerData StoreManagerData;
    }

    public interface ISaveable
    {
        public string Id { get; set; }
    }
    
    public interface IBind<IData> where IData : ISaveable
    {
        string Id { get; set; }
        public void Bind(IData data);
    }
}