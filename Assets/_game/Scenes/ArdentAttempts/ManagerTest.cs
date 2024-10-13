using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TestTools;

public class ManagerTest : MonoBehaviour
{
    private Manager manager;
    public GameObject seed;
    private GameObject waterText;
    private GameObject energyText;

    [SetUp]
    public void SetUp()
    {
        // Initialize Manager and seed GameObject
        manager = new GameObject().AddComponent<Manager>();
        seed = new GameObject();
        seed.AddComponent<DragDrop>();

        // Create UI Text GameObjects
        waterText = new GameObject();
        waterText.AddComponent<Text>();
        manager.waterText = waterText;

        energyText = new GameObject();
        energyText.AddComponent<Text>();
        manager.energyText = energyText;

        // Initialize other necessary fields
        manager.newTree = new GameObject();
        manager.trees = new List<GameObject> { new GameObject() };
        manager.xSpace = 5.0f;
        manager.zSpace = 2.5f;
        manager.maxTreeLine = 8;
    }

    [TearDown]
    public void TearDown()
    {
        Object.Destroy(manager.gameObject);
        Object.Destroy(seed);
        Object.Destroy(waterText);
        Object.Destroy(energyText);
    }

   
    public IEnumerator TestAddSeed()
    {
        // Initial tree count
        int initialTreeCount = manager.trees.Count;

        // Call AddSeed method
        manager.AddSeed();

        // Wait for end of frame to ensure the tree is added
        yield return new WaitForEndOfFrame();

        // Check if a new tree is added
        Assert.AreEqual(initialTreeCount + 1, manager.trees.Count);
    }

    
    public IEnumerator TestDragAndDrop()
    {
        // Initial tree count
        int initialTreeCount = manager.trees.Count;

        // Simulate mouse down
        seed.GetComponent<DragDrop>().OnMouseDown();

        // Simulate dragging
        yield return new WaitForSeconds(0.1f);
        seed.transform.position = new Vector3(10, 0, 10);

        // Simulate mouse up
        seed.GetComponent<DragDrop>().OnMouseUp();

        // Wait for end of frame to ensure the tree is added
        yield return new WaitForEndOfFrame();

        // Check if a new tree is added
        Assert.AreEqual(initialTreeCount + 1, manager.trees.Count);
    }
}