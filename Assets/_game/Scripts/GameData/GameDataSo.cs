using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace _game.Scripts.GameData
{
    [CreateAssetMenu(fileName = "GameData", menuName = "Data/Game Data")]
    public class GameDataSo : ScriptableObject
    {
        [field: SerializeField] public float StartingBalance { get; private set; }
        [field: SerializeField] public string CompanyName { get; private set; }
        [field: SerializeField] public StoreDataSo[] Stores { get; private set; }
        
        private void OnValidate()
        {
            SortStoresByPrice();
        }
        
        public void SortStoresByPrice()
        {
            Stores = Stores.OrderBy(store => store.BaseStoreCost).ToArray();
            for (var i = 0; i < Stores.Length; i++)
            {
                var store = Stores[i];
                store.Id = (i).ToString();
            }
        }


        public Texture2D GetStoreImageFromID(string storeId)
        {
            var store = Stores.FirstOrDefault(store => store.Id == storeId);
            return store != null ? store.StoreImage.texture : null;
        }
    }
}
