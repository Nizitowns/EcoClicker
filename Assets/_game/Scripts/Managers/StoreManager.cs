using System;
using System.Collections;
using System.Collections.Generic;
using _game.Scripts.GameData;
using _game.Scripts.Serialization;
using UnityEngine;
using UnityEngine.UIElements;

namespace _game.Scripts.Managers
{
    public class StoreManager : MonoBehaviour, IBind<StoreManagerData>
    {
        public static StoreManager Instance { get; private set; }
        
        [field: SerializeField] public string Id { get; set; } = "StoreManager";

        [SerializeField] private GameData.GameDataSo gameData;
        [SerializeField] private StoreManagerData storeManagerData;

        public event Action<StoreData> OnStoreCreated;
        public event Action<StoreData> OnStoreChanged;
        public event Action<float> OnBalanceChanged;
        public event Action<StoreData> OnStoreActivated;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void NewGame()
        {
            storeManagerData.CurrentBalance = gameData.StartingBalance;
            storeManagerData.Stores = new StoreData[gameData.Stores.Length];
            for (var i = 0; i < gameData.Stores.Length; i++)
            {
                if (storeManagerData.Stores[i] != null) continue;
                var store = gameData.Stores[i];
                 var newStore = new StoreData
                {
                    Id = store.Id,
                    StoreName = store.StoreName,
                    BaseStoreCost = store.BaseStoreCost,
                    CurrentStoreCost = store.BaseStoreCost,
                    BaseStoreProfit = store.BaseStoreProfit,
                    StoreTimer = store.StoreTimer,
                    StoreCount = store.StoreCount,
                    StoreMultiplier = store.StoreMultiplier,
                    StoreTimerDivision = store.StoreTimerDivision,
                    ManagerCost = store.ManagerCost,
                };
                storeManagerData.Stores[i] = newStore;
                OnStoreCreated?.Invoke(newStore);
            }
        }

        public void Bind(StoreManagerData data)
        {
            data.Id = Id;
            storeManagerData = data;
            foreach (var store in storeManagerData.Stores)
            {
                store.TimerRunning = false;
                OnStoreCreated?.Invoke(store);
            }
        }

        public void BuyStore(StoreData data)
        {
            if(!CanBuyStore(data)) return;
            AddToBalance(-data.GetStoreCost());
            
            data.StoreCount++;
            data.CurrentStoreCost = data.BaseStoreCost * Mathf.Pow(data.StoreMultiplier, data.StoreCount);
            OnStoreChanged?.Invoke(data);
            
            if (data.StoreCount % data.StoreTimerDivision == 0)
            {
                data.StoreTimer /= 2;
            }
        }
        
        private bool CanBuyStore(StoreData data)
        {
            return storeManagerData.CurrentBalance >= data.GetStoreCost();
        }
        
        public void AddToBalance(float amount)
        {
            storeManagerData.CurrentBalance += amount;
            OnBalanceChanged?.Invoke(storeManagerData.CurrentBalance);
        }

        public void ActivateStore(StoreData sd)
        {
            OnStoreChanged?.Invoke(sd);
            if (sd.StoreCount > 0)
            {
                sd.StartTimer = true;
                sd.StoreUnlocked = true;
                OnStoreActivated?.Invoke(sd);
            }
        }
        
        public float GetCurrentBalance() => storeManagerData.CurrentBalance;

        public void BuyManager(StoreData data)
        {
            if (!data.CanBuyManager()) return;
            AddToBalance(-data.ManagerCost);
            data.ManagerUnlocked = true;
            OnStoreChanged?.Invoke(data);
        }
    }

    [Serializable]
    public class StoreManagerData : ISaveable
    {
        [field: SerializeField] public string Id { get; set; }
        public float CurrentBalance;
        public StoreData[] Stores;
    }
    
    [Serializable]
    public class StoreData : ISaveable
    {
        [field: SerializeField] public string Id { get; set; }
        public string ManagerName { get; set; }
        public string StoreName;
        public float BaseStoreCost;
        public float CurrentStoreCost;
        public float BaseStoreProfit;
        public float StoreTimer;
        public int StoreCount;
        public float StoreMultiplier;
        public int StoreTimerDivision;
        public float ManagerCost;
        public bool ManagerUnlocked;
        public bool StoreUnlocked;
        public bool StartTimer;
        public bool TimerRunning;

        
        public float GetStoreCost() => BaseStoreCost * Mathf.Pow(StoreMultiplier, StoreCount);

        public bool CanBuyManager() => StoreUnlocked && StoreCount > 0 && StoreManager.Instance.GetCurrentBalance() >= ManagerCost;
    }
}