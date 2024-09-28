using System;
using _game.Scripts.GameData;
using _game.Scripts.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Initialize game data from scriptable object
namespace _game.Scripts
{
    public class InitGameData : MonoBehaviour
    {
        public static event Action OnLoadDataComplete;

        [SerializeField] GameData.GameData gameData;
        [SerializeField] GameObject storePrefab;
        [SerializeField] GameObject storePanel;

        [SerializeField] GameObject managerPrefab;
        [SerializeField] GameObject managerPanel;


        public void Start()
        {
            LoadData();
            OnLoadDataComplete?.Invoke();
        }

        public void LoadData()
        {
            LoadGameManagerData();
            InitStores();
        }

        private void LoadGameManagerData()
        {
            GameManager.Instance.AddToBalance(gameData.StartingBalance);
            GameManager.Instance.SetCompanyName(gameData.CompanyName);
        }

        private void InitStores()
        {
            foreach (var store in gameData.Stores)
            {
                CreateNewStore(store);
            }
        }
    
        private void CreateNewStore(StoreData store)
        {
            GameObject newStore = (GameObject)Instantiate(storePrefab);
            newStore.name = store.StoreName;
            Store storeObject = newStore.GetComponent<Store>();

            SetStoreObject(newStore, storeObject, store);

            storeObject.SetNextStoreCost(storeObject.baseStoreCost);
            newStore.transform.SetParent(storePanel.transform);
        }
    
        private void SetStoreObject(GameObject newStore, Store storeObject, StoreData store)
        {
            storeObject.SetStoreName(store.StoreName);

            storeObject.SetStoreImage(store.StoreImage);

            storeObject.baseStoreCost = store.BaseStoreCost;
            storeObject.baseStoreProfit = store.BaseStoreProfit;
            storeObject.storeTimer = store.StoreTimer;
            storeObject.storeCount = store.StoreCount;
            storeObject.storeMultiplier = store.StoreMultiplier;
            storeObject.storeTimerDivision = store.StoreTimerDivision;
            storeObject.managerCost = store.ManagerCost;

            CreateManager(store.ManagerName, storeObject, store);
        }
    
        private void CreateManager(string managerName, Store storeObject, StoreData store)
        {
            GameObject newManager = (GameObject)Instantiate(managerPrefab);
            newManager.transform.SetParent(managerPanel.transform);

            TMP_Text managerNameText = newManager.transform.Find("Manager Name Text (TMP)").GetComponent<TMP_Text>();
            managerNameText.text = managerName;

            storeObject.managerCost = store.ManagerCost;
            Button managerButton = newManager.transform.Find("Unlock Manager Button").GetComponent<Button>();
            TMP_Text buttonText = managerButton.transform.Find("Unlock Manager Button Text (TMP)").GetComponent<TMP_Text>();
            buttonText.text = "Unlock " + store.ManagerCost.ToString("C2");

            UIStore UIManager = storeObject.GetComponent<UIStore>();
            UIManager.managerButton = managerButton;

            managerButton.onClick.AddListener(storeObject.UnlockManager);
        }
    }
}