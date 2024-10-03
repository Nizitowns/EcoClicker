using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Author: Eduardo "edu05" Chiaratto
//Last Update: 10/03/2024 BR

public class Manager : MonoBehaviour
{
    [Header("Instances")]
    public static Manager Instance;
    public static DeciLevel01 deciLevel01;

    [Header("Water Manager")]
    [Tooltip("The time for water regen in sec")]
    public float waterRegenTime;
    private float waterTimer = 0f;

    [Header("New tree Manager")]
    public GameObject newTree;
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
        actualTreePositionVector++;
        //will see if reach in max line value
        if (actualTreePositionVector == maxTreeLine)
        {
            actualTreePositionVector = 0;
            Vector3 newPosition = trees[trees.Count - maxTreeLine].transform.position;
            newPosition.z += zSpace;
            GameObject go = Instantiate(newTree, newPosition, Quaternion.identity); //the prefab will be change in future
            trees.Add(go);
            //Debug.Log(trees.Count);
        } else
        {
            Vector3 newPosition = trees[trees.Count - 1].transform.position;
            newPosition.x += xSpace;
            GameObject go = Instantiate(newTree, newPosition, Quaternion.identity);
            trees.Add(go);
            //Debug.Log(trees.Count);
        }
    }
}
