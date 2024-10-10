using System;
using _game.Scripts.GameData;
using UnityEngine;
using UnityEngine.UIElements;

namespace _game.Scripts.UI
{
    public class Game : BaseUI
    {
    
        [SerializeField] private GameData.GameData gameData;
        [SerializeField] private VisualTreeAsset storeContainer;
        
        
        private void Start()
        {
            InitGameUI();
        }

        private void InitGameUI()
        {
            
            CreateStoreButtons();

        }

        private void CreateStoreButtons()
        {
            foreach (var storeData in gameData.Stores)
            {
                container.style.flexShrink = 0; //TODO: Fix style sheet ??
                var storeObj = new Store(storeData, storeContainer.CloneTree(), container);
                /*var store = storeContainer.CloneTree();
                container.Add(store);
                store.Q<Button>("use-btn").style.backgroundImage = storeData.StoreImage.texture;
                store.Q<Label>("price").text = storeData.BaseStoreCost.ToString();
                store.Q<Label>("count").text = storeData.StoreCount.ToString();
                store.Q<Button>("buy-btn").clickable.clicked += () =>
                {
                    storeObj.BuyStore();
                };*/
            }
        }
    }

    [Serializable, UxmlElement]
    public partial class Store : VisualElement
    {
        private string storeName;
        private Texture2D storeImage;
        private float baseStoreCost;
        private float baseStoreProfit;
        private float storeTimer;
        public int storeCount {private set; get;}
        private float storeMultiplier;
        private int storeTimerDivision;
        private float managerCost;
        private bool managerUnlocked;
        private bool storeUnlocked;

        private VisualElement _root;
        private VisualElement _storePreset;

        public Store(){ }
        
        public Store(StoreData soStoreData, VisualElement storePreset, VisualElement root)
        {
            _root = root;
            storeName = soStoreData.StoreName;
            storeImage = soStoreData.StoreImage.texture;
            baseStoreCost = soStoreData.BaseStoreCost;
            baseStoreProfit = soStoreData.BaseStoreProfit;
            storeTimer = soStoreData.StoreTimer;
            storeCount = soStoreData.StoreCount;
            storeMultiplier = soStoreData.StoreMultiplier;
            storeTimerDivision = soStoreData.StoreTimerDivision;
            managerCost = soStoreData.ManagerCost;
            managerUnlocked = false;
            storeUnlocked = false || storeCount > 0;

            _storePreset = storePreset;
            
            RegisterButtons();
            
            root.Add(storePreset);
        }

        private void RegisterButtons()
        {
            var buyButton = _storePreset.Q<Button>("buy-btn");
            buyButton.clickable.clicked += BuyStore;
            
            UpdateUIElements();
        }

        //TODO: Data binding
        public void UpdateUIElements()
        {
            _storePreset.Q<Button>("use-btn").style.backgroundImage = storeImage;
            _storePreset.Q<Label>("price").text = baseStoreCost.ToString("F2") + "$";
            _storePreset.Q<Label>("count").text = storeCount.ToString();
            _storePreset.Q<Button>("buy-btn").SetEnabled(storeUnlocked);
        }


        public void BuyStore()
        {
            storeCount++;

            float amount = -baseStoreCost;

            baseStoreCost = baseStoreCost * Mathf.Pow(storeMultiplier, storeCount);
            //GameManager.Instance.AddToBalance(amount);

            if (storeCount % storeTimerDivision == 0)
            {
                storeTimer /= 2;
            }
            Debug.Log($"Store bought {storeCount} : {baseStoreCost}");
            UpdateUIElements();
        }

    }
    
}


