using System.Linq;
using UnityEngine;

namespace _game.Scripts.GameData
{
    [CreateAssetMenu(fileName = "GameData", menuName = "Data/Game Data")]
    public class GameData : ScriptableObject
    {
        [field: SerializeField] public float StartingBalance { get; private set; }
        [field: SerializeField] public string CompanyName { get; private set; }
        [field: SerializeField] public StoreData[] Stores { get; private set; }
        
        private void OnValidate()
        {
            SortStoresByPrice();
        }
        
        public void SortStoresByPrice()
        {
            Stores = Stores.OrderBy(store => store.BaseStoreCost).ToArray();
        }
    }
}
