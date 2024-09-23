using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public delegate void UpdateBalance();
    public static event UpdateBalance OnUpdateBalance;

    public string companyName;

    float currentBalance = 0.0f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (OnUpdateBalance != null)
        {
            OnUpdateBalance();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddToBalance(float amount)
    {
        currentBalance += amount;
        if (OnUpdateBalance != null)
        {
            OnUpdateBalance();
        }
    }

    public bool CanBuy(float amountToSpend)
    {
        return currentBalance >= amountToSpend;
    }

    public float GetCurrentBalance()
    {
        return currentBalance;
    }
}
