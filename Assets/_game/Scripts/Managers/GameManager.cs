using System;
using UnityEngine;

namespace _game.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        
        public string CompanyName { get; private set;}
        
        public static event Action OnUpdateBalance;

        private float _currentBalance = 0.0f;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            OnUpdateBalance?.Invoke();
        }

        public void AddToBalance(float amount)
        {
            _currentBalance += amount;
            OnUpdateBalance?.Invoke();
        }

        public bool CanBuy(float amountToSpend) => _currentBalance >= amountToSpend;

        public float GetCurrentBalance() => _currentBalance;
        
        public void SetCompanyName(string companyName)
        {
            CompanyName = companyName;
        }
    }
}
