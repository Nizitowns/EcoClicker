using UnityEngine;
using UnityEngine.UIElements;

namespace _game.Scripts.UI
{
    public class Game : BaseUI
    {
    
        [SerializeField] private GameData.GameDataSo gameDataSo;
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
            foreach (var storeData in gameDataSo.Stores)
            {
                container.style.flexShrink = 0; //TODO: Fix style sheet ??
                //var storeObj = new Store(storeData, storeContainer.CloneTree(), container);
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
}


