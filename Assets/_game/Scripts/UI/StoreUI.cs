using System;
using System.Collections;
using _game.Scripts.Extensions;
using _game.Scripts.GameData;
using _game.Scripts.Managers;
using UnityEngine;
using UnityEngine.UIElements;

namespace _game.Scripts.UI
{
    public class StoreUI : BaseUI
    {

        [SerializeField] private StoreManager storeManager;
        [SerializeField] private GameDataSo gameDataSo;
        [SerializeField] private VisualTreeAsset buttonTemplate;

        private Label _currentBalance;

        protected override void Awake()
        {
            base.Awake();
            _currentBalance = container.CreateChild<Label>("store-balance");
            
            storeManager.OnStoreCreated += CreateButton;
            storeManager.OnStoreChanged += UpdateStore;
            storeManager.OnBalanceChanged += UpdateBalance;
            storeManager.OnStoreActivated += UpdateProgressBar;
            
            UpdateBalance(storeManager.GetCurrentBalance());
        }
        
        //TODO: Data bind visual elements to stores
        private void CreateButton(StoreData storeData)
        {
            Debug.Log($"{storeData.StoreName} created");
            var store = buttonTemplate.CloneTree().Q<VisualElement>("store");
            store.visible = false;
            //button.Q<Label>("storeName").text = storeData.StoreName;
            var useButton = store.Q<Button>("store-use-btn");
            var storeCost = store.Q<Label>("store-cost");
            var buyButton = store.Q<Button>("store-buy-btn");
            var progressBar = store.Q<ProgressBar>("store-progress-bar");
            var storeCount = store.Q<Label>("store-count");

            store.name = $"store-{storeData.Id}";
            useButton.style.backgroundImage = gameDataSo.GetStoreImageFromID(storeData.Id);
            storeCount.text = storeData.StoreCount.ToString();
            progressBar.title = storeData.BaseStoreProfit.ToString("F2") + "$";
            useButton.clicked += () => storeManager.ActivateStore(storeData);
            storeCost.text = storeData.GetStoreCost().ToString("F2") + "$";
            buyButton.clicked += () => storeManager.BuyStore(storeData);

            if(storeData.StoreUnlocked)
                store.visible = true;
            
            container.Add(store);
            UpdateBalance(storeManager.GetCurrentBalance());
        }
        
        private void UpdateBalance(float value)
        {
            _currentBalance.text = "Balance: " + value.ToString("F2") + "$";
        }
        
        private void UpdateStore(StoreData storeData)
        {
            var store = container.Q<VisualElement>($"store-{storeData.Id}");
            var storeCount = store.Q<Label>("store-count");
            var progressBar = store.Q<ProgressBar>("store-progress-bar");
            var storeCost = store.Q<Label>("store-cost");
            var nextStore = container.Q<VisualElement>($"store-{int.Parse(storeData.Id) + 1}");
            nextStore.visible = true;
            
            storeCount.text = storeData.StoreCount.ToString();
            storeCost.text = storeData.GetStoreCost().ToString("F2") + "$";
            progressBar.title = (storeData.BaseStoreProfit * storeData.StoreCount).ToString("F2") + "$";
            
            //StartCoroutine(AdvanceProgressBar(progressBar, progressBar.highValue, store.StoreTimer));
        }
        
        private void UpdateProgressBar(StoreData store)
        {
            var storeButton = container.Q<VisualElement>($"store-{store.Id}");
            var progressBar = storeButton.Q<ProgressBar>("store-progress-bar");
            if(store.TimerRunning) return;
            StartCoroutine(AdvanceProgressBar(progressBar, progressBar.highValue, store.StoreTimer, store));
        }

        //TODO: Move to update
        private IEnumerator AdvanceProgressBar(ProgressBar progressBar, float targetValue, float duration, StoreData store)
        {
            float startValue = 0;
            float time = 0;
            store.TimerRunning = true;
            while (time < duration)
            {
                time += Time.deltaTime;
                progressBar.value = Mathf.Lerp(startValue, targetValue, time / duration);
                yield return null;
            }
            store.TimerRunning = false;
            progressBar.value = 0;
            storeManager.AddToBalance(store.BaseStoreProfit * store.StoreCount);
            
            if(store.ManagerUnlocked)
                StartCoroutine(AdvanceProgressBar(progressBar, progressBar.highValue, store.StoreTimer, store));
            
            
        }

        
        protected void OnDestroy()
        {
            storeManager.OnStoreCreated -= CreateButton;
            storeManager.OnStoreChanged -= UpdateStore;
            storeManager.OnBalanceChanged -= UpdateBalance;
            storeManager.OnStoreActivated -= UpdateProgressBar;
        }
    }
}
