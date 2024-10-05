using _game.Scripts;
using _game.Scripts.Managers;
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
    public Button managerButton;

    private CanvasGroup myCanvasGroup;

    private void Awake()
    {
        myStore = GetComponent<Store>();
        myCanvasGroup = this.transform.GetComponent<CanvasGroup>();
        myStore.OnStoreUnlocked += UnlockStore;
    }

    // Start is called before the first frame update
    void Start()
    {
        storeCountText.text = myStore.GetStoreCount().ToString();
        Invoke(nameof(UnlockStore), 0.2f);  
    }

    //TODO: Fix this hacky solution
    private void UnlockStore()
    {
        if (myStore.storeCount > 0)
        {
            myStore.storeUnlocked = true;
        }
        
        if (!myStore.storeUnlocked)
        {
            myCanvasGroup.interactable = false;
            myCanvasGroup.alpha = 0;
//            Debug.Log($" {myStore.name} Store is not unlocked");
            return;
        }
//        Debug.Log($" {myStore.name} Store is unlocked");
        myCanvasGroup.interactable = true;
        myCanvasGroup.alpha = 1;
    }

    // Update is called once per frame
    void Update()
    {
        progressSlider.value = myStore.GetCurrentTimer() / myStore.GetStoreTimer();
    }

    private void OnEnable()
    {
        GameManager.OnUpdateBalance += UpdateUI;
        InitGameData.OnLoadDataComplete += UpdateUI;
    }

    private void OnDisable()
    {
        GameManager.OnUpdateBalance -= UpdateUI;
        InitGameData.OnLoadDataComplete += UpdateUI;
    }

    public void ManagerUnlocked()
    {
        if (managerButton == null)
        {
            Debug.LogError("managerButton is not assigned or is null!");
            return;
        }

        // Find the "Unlock Manager Button Text (TMP)" child object
        var textTransform = managerButton.transform.Find("Unlock Manager Button Text (TMP)");
        if (textTransform == null)
        {
            Debug.LogError("'Unlock Manager Button Text (TMP)' not found on managerButton.");
            return;
        }

        // Get the TMP_Text component and update the button text
        TMP_Text buttonText = textTransform.GetComponent<TMP_Text>();
        if (buttonText == null)
        {
            Debug.LogError("TMP_Text component not found on 'Unlock Manager Button Text (TMP)'.");
            return;
        }

        buttonText.text = "PURCHASED";

        //TMP_Text buttonText = managerButton.transform.Find("Unlock Manager Button Text(TMP)").GetComponent<TMP_Text>();
        //buttonText.text = "PURCHASED";
    }

    public void UpdateUI()
    {

        if (!myStore.storeUnlocked && !GameManager.Instance.CanBuy(myStore.GetNextStoreCost()))
        {
            myCanvasGroup.interactable = false;
            myCanvasGroup.alpha = 0;
        }
        else
        {
            if(!myStore.storeUnlocked){
                myCanvasGroup.interactable = true;
                myCanvasGroup.alpha = 1;
                myStore.storeUnlocked = true;
            }
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

        // Update manager button if store can be afforded
        if (!myStore.managerUnlocked && GameManager.Instance.CanBuy(myStore.managerCost))
        {
            managerButton.interactable = true;
        }
        else
        {
            managerButton.interactable = false;
        }

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
