using UnityEngine;

public class Store : MonoBehaviour
{
    // Gameplay Defining Variables
    [SerializeField] public string storeName; // To Refactor
    [SerializeField] public float baseStoreCost = 1.50f; // To Refactor
    [SerializeField] public float baseStoreProfit = 0.50f; // To Refactor
    [SerializeField] public float storeTimer = 4.0f;  // To Refactor
    [SerializeField] public int storeCount = 1; // To Refactor
    [SerializeField] public float storeMultiplier; // To Refactor
    [SerializeField] public int storeTimerDivision = 20; // To Refactor
    [SerializeField] public float managerCost; // To Refactor

    float currentTimer = 0.0f;
    float nextStoreCost;

    [SerializeField] public bool managerUnlocked = false; // To Refactor
    [SerializeField] public bool storeUnlocked = false; // To Refactor
    bool startTimer = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (startTimer)
        {
            currentTimer += Time.deltaTime;
            if (currentTimer > storeTimer)
            {
                if (!managerUnlocked)
                {
                    startTimer = false;
                }

                currentTimer = 0;
                GameManager.Instance.AddToBalance(baseStoreProfit * storeCount);
            }
        }
    }

    public void BuyStore()
    {
        storeCount++;

        float amount = -nextStoreCost;

        nextStoreCost = baseStoreCost * Mathf.Pow(storeMultiplier, storeCount);
        GameManager.Instance.AddToBalance(amount);

        if (storeCount % storeTimerDivision == 0)
        {
            storeTimer /= 2;
        }
    }

    public void OnStartTimer()
    {
        if (!startTimer && storeCount > 0)
        {
            startTimer = true;
        }
    }

    public float GetCurrentTimer()
    {
        return currentTimer;
    }

    public float GetStoreTimer()
    {
        return storeTimer;
    }

    public float GetNextStoreCost()
    {
        return nextStoreCost;
    }
    public void SetNextStoreCost(float amount)
    {
        nextStoreCost = amount;
    }

    public int GetStoreCount()
    {
        return storeCount;
    }

    public void UnlockManager()
    {
        if (managerUnlocked)
        {
            return;
        }

        if (GameManager.Instance.CanBuy(managerCost))
        {
            GameManager.Instance.AddToBalance(-managerCost);
            managerUnlocked = true;
            this.transform.GetComponent<UIStore>().ManagerUnlocked();
        }

    }
}
