﻿using UnityEngine;
using UnityEngine.UI;

namespace _game.Scripts.GameData
{
    [CreateAssetMenu(menuName = "Data/StoreData", fileName = "StoreData", order = 0)]
    public class StoreData : ScriptableObject
    {
        [field: SerializeField] public Sprite StoreImage { get; private set; }
        [field: SerializeField] public string StoreName { get; private set; }
        [field: SerializeField] public float BaseStoreCost { get; private set; }
        [field: SerializeField] public float BaseStoreProfit { get; private set; }
        [field: SerializeField] public float StoreTimer { get; private set; }
        [field: SerializeField] public int StoreCount { get; private set; }
        [field: SerializeField] public float StoreMultiplier { get; private set; }
        [field: SerializeField] public int StoreTimerDivision { get; private set; }
        [field: SerializeField] public string ManagerName { get; private set; }
        [field: SerializeField] public float ManagerCost { get; private set; }
        
    }
}