using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public enum State
    {
        Main,
        Managers
    }

    [SerializeField] TMP_Text currentBalanceText;
    [SerializeField] TMP_Text companyNameText;
    [SerializeField] GameObject managerPanel;

    public State currentState;

    // Start is called before the first frame update
    void Start()
    {
        currentState = State.Main;
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

    void OnShowManagers()
    {
        currentState = State.Managers;
        managerPanel.SetActive(true);
    }

    void OnShowMain()
    {
        currentState = State.Main;
        managerPanel.SetActive(false);
    }

    public void OnClickManagers()
    {
        if (currentState == State.Main)
        {
            OnShowManagers();
        }
        else
        {
            OnShowMain();
        }
    }
}
