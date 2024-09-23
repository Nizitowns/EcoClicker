using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadGameData : MonoBehaviour
{
    public delegate void LoadDataComplete();
    public static event LoadDataComplete OnLoadDataComplete;

    [SerializeField] TextAsset gameData;
    [SerializeField] GameObject storePrefab;
    [SerializeField] GameObject storePanel;
    [SerializeField] GameObject managerPrefab;
    [SerializeField] GameObject managerPanel;


    public void Start()
    {
        LoadData();
        if (OnLoadDataComplete != null)
        {
            OnLoadDataComplete();
        }
    }

    public void LoadData()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(gameData.text);

        LoadGameManagerData(xmlDoc);

        LoadStores(xmlDoc);
    }

    private void LoadGameManagerData(XmlDocument xmlDoc)
    {
        float startingBalance = float.Parse(xmlDoc.GetElementsByTagName("startingBalance")[0].InnerText);
        GameManager.Instance.AddToBalance(startingBalance);

        string companyName = xmlDoc.GetElementsByTagName("companyName")[0].InnerText;
        GameManager.Instance.companyName = companyName;
    }

    private void LoadStores(XmlDocument xmlDoc)
    {
        XmlNodeList storeList = xmlDoc.GetElementsByTagName("store");
        foreach (XmlNode storeInfo in storeList)
        {
            LoadStoreNodes(storeInfo);
        }
    }

    private void LoadStoreNodes(XmlNode storeInfo)
    {
        GameObject newStore = (GameObject)Instantiate(storePrefab);

        Store storeObject = newStore.GetComponent<Store>();

        XmlNodeList storeNodes = storeInfo.ChildNodes;
        foreach (XmlNode storeNode in storeNodes)
        {
            SetStoreObject(newStore, storeObject, storeNode);
        }

        storeObject.SetNextStoreCost(storeObject.baseStoreCost);
        newStore.transform.SetParent(storePanel.transform);
    }

    private void SetStoreObject(GameObject newStore, Store storeObject, XmlNode storeNode)
    {
        if (storeNode.Name == "name")
        {
            TMP_Text storeText = newStore.transform.Find("Store Name Text (TMP)").GetComponent<TMP_Text>(); // To Refactor Move to UIStore
            storeText.text = storeNode.InnerText;
        }
        if (storeNode.Name == "image")
        {
            Sprite newSprite = Resources.Load<Sprite>(storeNode.InnerText);
            Image storeImage = newStore.transform.Find("Image Button Click").GetComponent<Image>();
            storeImage.sprite = newSprite;
        }
        if (storeNode.Name == "baseStoreCost")
        {
            storeObject.baseStoreCost = float.Parse(storeNode.InnerText);
        }
        if (storeNode.Name == "baseStoreProfit")
        {
            storeObject.baseStoreProfit = float.Parse(storeNode.InnerText);
        }
        if (storeNode.Name == "storeTimer")
        {
            storeObject.storeTimer = float.Parse(storeNode.InnerText);
        }

        if (storeNode.Name == "storeCount")
        {
            storeObject.storeCount = int.Parse(storeNode.InnerText);
        }
        if (storeNode.Name == "storeMultiplier")
        {
            storeObject.storeMultiplier = float.Parse(storeNode.InnerText);
        }
        if (storeNode.Name == "storeTimerDivision")
        {
            storeObject.storeTimerDivision = int.Parse(storeNode.InnerText);
        }
        if (storeNode.Name == "managerCost")
        {
            CreateManager();
        }
    }

    private void CreateManager()
    {
        GameObject newManager = (GameObject)Instantiate(managerPrefab);
    }
}
