using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStore : MonoBehaviour
{
    [SerializeField] TMP_Text storeCountText;
    [SerializeField] Slider progressSlider;
    [SerializeField] TMP_Text buyButtonText;
    [SerializeField] Button buyButton;

    public Store myStore;

    private void Awake()
    {
        myStore = GetComponent<Store>();
    }

    // Start is called before the first frame update
    void Start()
    {
        storeCountText.text = myStore.GetStoreCount().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        progressSlider.value = myStore.GetCurrentTimer() / myStore.GetStoreTimer();
    }

    private void OnEnable()
    {
        GameManager.OnUpdateBalance += UpdateUI;
        LoadGameData.OnLoadDataComplete += UpdateUI;
    }

    private void OnDisable()
    {
        GameManager.OnUpdateBalance -= UpdateUI;
    }


    public void UpdateUI()
    {
        // Hide panel until you can afford the store
        CanvasGroup myCanvasGroup = this.transform.GetComponent<CanvasGroup>();
        if (!myStore.storeUnlocked && !GameManager.Instance.CanBuy(myStore.GetNextStoreCost()))
        {
            myCanvasGroup.interactable = false;
            myCanvasGroup.alpha = 0;
        }
        else
        {
            myCanvasGroup.interactable = true;
            myCanvasGroup.alpha = 1;
            myStore.storeUnlocked = true;
        }

        // update button if you can afford the store
        if (GameManager.Instance.CanBuy(myStore.GetNextStoreCost()))
        {
            buyButton.interactable = true;
        }
        else
        {
            buyButton.interactable = false;
        }

        buyButtonText.text = "Buy " + myStore.GetNextStoreCost().ToString("C2");
    }

    public void BuyStoreOnClick()
    {
        if (!GameManager.Instance.CanBuy(myStore.GetNextStoreCost()))
        {
            return;
        }

        myStore.BuyStore();
        storeCountText.text = myStore.GetStoreCount().ToString();
        UpdateUI();
    }

    public void OnTimerClick()
    {
        myStore.OnStartTimer();
    }
}
