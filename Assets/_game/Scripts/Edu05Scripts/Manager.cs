using _game.Scripts.GameData;
using _game.Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

//Author: Eduardo "edu05" Chiaratto
//Last Update: 10/03/2024 BR

public class Manager : MonoBehaviour
{
    [Header("Instances")]
    public static Manager Instance;
    [SerializeField] private GameDataSo gameDataSo;
    public static DeciLevel01 deciLevel01;
    public GameObject shopPanel;

    public List<TreeDataSo> tree;

    public UIDocument shopDocument;
    public VisualTreeAsset buttomShopPanel;
    //public 

    [SerializeField] private Grid grid;
    [SerializeField] private Vector2Int _size;
    [SerializeField] private int actualX;
    [SerializeField] private int actualY;

    [Header("Water Manager")]
    [Tooltip("The time for water regen in sec")]
    public float waterRegenTime;
    private float waterTimer = 0f;

    [Header("New tree Manager")]
    public GameObject newTree;
    [SerializeField] public TreeDataSo newTreeData;
    public List<GameObject> trees;
    [Tooltip("The value of x spacing in each tree")]
    public float xSpace = 5.0f;
    [Tooltip("The value of z spacing in each tree")]
    public float zSpace = 2.5f;
    [Tooltip("The max value of line")]
    public int maxTreeLine = 8;
    private int actualTreePositionVector = 0;

    [Header("UI Text")]
    public GameObject waterText;
    public GameObject energyText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        if (Instance == null) Instance = this;
        if(deciLevel01 == null) deciLevel01 = new DeciLevel01();

        
        //SaveLoadManager.Instance.NewGame();
    }

    void Start()
    {
        deciLevel01.Water = 100;
    }

    void Update()
    {
        waterText.GetComponent<Text>().text = "Water: " + deciLevel01.Water;
        energyText.GetComponent<Text>().text = "Energy: " + deciLevel01.Energy;

        waterTimer += Time.deltaTime;
        if(waterTimer > waterRegenTime && deciLevel01.Water < 100f)
        {
            waterTimer = 0;
            deciLevel01.Water++;
        }
    }

    public void AddSeed()
    {
        actualX++;
        if(trees.Count == 0) actualX = 0;

        if (actualX == maxTreeLine)
        {
            actualY++;
            actualX = 0;
        }

        var worldPosition = grid.GetCellCenterWorld(new Vector3Int(actualX, 0, actualY)); //this is better for a retangular or hexagonal grid
        var go = Instantiate(newTree, worldPosition, Quaternion.identity); //the prefab will be change in future
        go.GetComponent<TreeManager>().treeData = newTreeData;
        trees.Add(go);

        /*for (int x = 0; x < _size.x; x++)
        {
            for (int y = 0; y < _size.y; y++)
            {
                var worldPosition = grid.GetCellCenterWorld(new Vector3Int(x, y));
                GameObject go = Instantiate(newTree, worldPosition, Quaternion.identity); //the prefab will be change in future
                go.GetComponent<TreeManager>().treeData = newTreeData;
                trees.Add(go);
            }
        }*/

    }

    public void ShowShopPanel()
    {
        if (shopPanel.activeSelf == false)
        {
            shopPanel.SetActive(true);
            //shopDocument = GetComponent<UIDocument>();
            foreach (TreeDataSo treeDataSo in tree)
            {
                CreateShopButton(treeDataSo);
            }
        }
        else shopPanel.SetActive(false);
    }

    public void CreateShopButton(TreeDataSo tree)
    {
        var shop = buttomShopPanel.CloneTree().Q<VisualElement>("Shop");
        var buttomShop = shop.Q<UnityEngine.UIElements.Button>("BuySeed");
        var nameShop = shop.Q<UnityEngine.UIElements.Label>("TreeName");
        var quantityShop = shop.Q<UnityEngine.UIElements.Label>("TreeName");

        buttomShop.clicked += () => AddSeed();
        nameShop.text = tree.name;
        quantityShop.text = "0";

        shop.visible = true;
        if (shopDocument) shopDocument.rootVisualElement.Q<VisualElement>("Shop").Add(shop);
    }
}
