using System;
using System.Collections;
using System.Collections.Generic;
using _game.Scripts.Serialization;
using Newtonsoft.Json;
using UnityEngine;

namespace _game.Scripts.Managers
{
    public class SaveManager : MonoBehaviour
    {
        public static SaveManager Instance { get; private set; }
        
        [SerializeField] private string relativePath = "saveData.json";
        [SerializeField] private List<Store> stores;

        private JsonDataService _jsonDataService;

        private GameDataSerializable _gameData;
        private float _timer;
        private float _saveInterval = 15.0f;

        private void Awake()
        {
            InitGameData.OnLoadDataComplete += Load;
            Store.OnStoreCreated += OnStoreCreated;
        }

        private void Start()
        {
            
            if (Instance == null)
                Instance = this;
            else
            {
                Destroy(gameObject);
                return;
            }
            
            //InvokeRepeating(nameof(Load), 1f, 1f);
            InvokeRepeating(nameof(Save), _saveInterval, _saveInterval);
        }


        private void OnStoreCreated(Store store)
        {
            stores.Add(store);
        }
        
        private void Load()
        {
            Debug.Log("Try to lod save data");
            if(_jsonDataService == null)
                _jsonDataService = new JsonDataService();
            _gameData = _jsonDataService.LoadData<GameDataSerializable>(relativePath, false);
            GameManager.Instance.SetBalance(_gameData.StartingBalance);
            GameManager.Instance.SetCompanyName(_gameData.CompanyName);
            InitStores();
            
        }

        private void InitStores()
        {
            for (int i = 0; i < stores.Count; i++)
            {
                stores[i].storeCount = _gameData.Stores[i].StoreCount;
                stores[i].storeUnlocked = _gameData.Stores[i].StoreUnlocked;
                stores[i].managerUnlocked = _gameData.Stores[i].ManagerUnlocked;
            }
        }

        private void Save()
        {
            if(_jsonDataService == null)
                _jsonDataService = new JsonDataService();
            var data = CopyGameData();
            _jsonDataService.SaveData(relativePath, data, false);
            Debug.Log("Saved Data");
        }

        //TODO: Refactor
        private GameDataSerializable CopyGameData()
        {
            GameDataSerializable _gameData = new GameDataSerializable();
            _gameData.StartingBalance = GameManager.Instance.GetCurrentBalance();
            _gameData.CompanyName = GameManager.Instance.CompanyName;
            _gameData.Stores = new StoreDataSerializable[stores.Count];
            
            for (int i = 0; i < stores.Count; i++)
            {
                _gameData.Stores[i] = new StoreDataSerializable();
                _gameData.Stores[i].StoreCount = stores[i].storeCount;
                _gameData.Stores[i].StoreUnlocked = stores[i].storeUnlocked;
                _gameData.Stores[i].ManagerUnlocked = stores[i].managerUnlocked;
            }

            return _gameData;
        }

        private void OnDestroy()
        {
            Store.OnStoreCreated -= OnStoreCreated;
            InitGameData.OnLoadDataComplete -= Load;
        }
    }
    
    [System.Serializable]
    public class GameDataSerializable
    {
        public float StartingBalance;
        public string CompanyName;
        public StoreDataSerializable[] Stores;
    }

    public struct StoreDataSerializable
    {
        public int StoreCount;
        public bool StoreUnlocked;
        public bool ManagerUnlocked;
    }
}