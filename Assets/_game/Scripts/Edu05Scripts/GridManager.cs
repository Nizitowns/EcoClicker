using _game.Scripts;
using _game.Scripts.GameData;
using _game.Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    [SerializeField] private Grid grid;
    //[SerializeField] private Vector2Int _size;
    [SerializeField] private int actualX;
    [SerializeField] private int actualY;
    [SerializeField] private int maxTreeLine;
    [SerializeField] private GameObject treeModel;
    [SerializeField] private List<GameObject> trees;

    private void Awake()
    {
        if(Instance == null) { Instance = this;}
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AddTreeGrid();
    }

    public void AddTreeGrid()
    {
        actualX++;
        if (trees.Count == 0) actualX = 0;

        if (actualX == maxTreeLine)
        {
            actualY++;
            actualX = 0;
        }

        var worldPosition = grid.GetCellCenterWorld(new Vector3Int(actualX, 0, actualY)); //this is better for a retangular or hexagonal grid
        var go = Instantiate(treeModel, worldPosition, Quaternion.identity); //the prefab will be change in future
        //go.GetComponent<TreeManager>().treeData = newTreeData;
        trees.Add(go);
    }
}
