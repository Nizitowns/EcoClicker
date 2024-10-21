﻿using UnityEngine;
using UnityEngine.UI;

namespace _game.Scripts.GameData
{
    [CreateAssetMenu(menuName = "Data/StoreData", fileName = "StoreData", order = 0)]
    public class StoreDataSo : ScriptableObject
    {
        [field: SerializeField] public string Id { get; set; }
        [field: SerializeField] public Sprite StoreImage { get; private set; }
        [field: SerializeField] public string StoreName { get; private set; }
        [field: SerializeField] public float BaseStoreCost { get; private set; }
        [field: SerializeField] public float StoreMultiplier { get; private set; }
        [field: SerializeField] public float StoreTimer { get; private set; }
        [field: SerializeField] public float BaseStoreProfit { get; private set; }
        
        [field: Space(10)]
        [field: SerializeField] public int StoreCount { get; private set; }
        [field: SerializeField] public int StoreTimerDivision { get; private set; }
        [field: SerializeField] public string ManagerName { get; private set; }
        [field: SerializeField] public float ManagerCost { get; private set; }
        [field: SerializeField] public bool IsUnlocked { get; private set; }
        [field: SerializeField] public float StoreUnlockedAt { get; private set; }
    }

    [CreateAssetMenu(menuName = "Data/TreeData", fileName = "Tree", order = 1)]
    public class TreeDataSo : ScriptableObject
    {
        [field: SerializeField] public string Id { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public GameObject Seed { get; private set; }
        [field: SerializeField] public GameObject Young { get; private set; }
        [field: SerializeField] public GameObject Adult { get; private set; }
        [field: SerializeField] public float TimeToGrow { get; private set; }
        [field: SerializeField] public float TimeToGlow { get; private set; }
    }
}