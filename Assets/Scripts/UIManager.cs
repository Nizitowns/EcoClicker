using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TMP_Text currentBalanceText;
    [SerializeField] TMP_Text companyNameText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        GameManager.OnUpdateBalance += UpdateUI;
        LoadGameData.OnLoadDataComplete += UpdateUI;
    }

    private void OnDisable()
    {
        GameManager.OnUpdateBalance -= UpdateUI;
        LoadGameData.OnLoadDataComplete -= UpdateUI;
    }

    public void UpdateUI()
    {
        currentBalanceText.text = GameManager.Instance.GetCurrentBalance().ToString("C2");
        companyNameText.text = GameManager.Instance.companyName;
    }
}
