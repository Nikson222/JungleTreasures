using System;
using PlayerInterfaces;
using UnityEngine;

namespace Player
{
    public class PlayerModel : IPlayerModel
    {
        private int[] BET_VALUES = {10, 50, 100, 500, 1000};
        
        private int _balance = 0;
        private int _betIndex = 0;
        
        public event Action<int> OnBalanceChanged; 
        public event Action<int> OnBetChanged; 
        public event Action OnSuccessfulSpin;
        
        private bool _isBuyingSpin = false;
        
        public PlayerModel(int startBalance)
        {
            _balance = startBalance;
            
            PlayerPrefs.SetInt("Balance", _balance);
        }
        
        public int BetIndex
        {
            get => _betIndex;
            set
            {
                if (value <= BET_VALUES.Length - 1 && value >= 0)
                    _betIndex = value;
                else if (value > BET_VALUES.Length - 1)
                    _betIndex = BET_VALUES.Length - 1;
                else
                    _betIndex = 0;
                
                OnBetChanged?.Invoke(CurrentBet);
            }
        }

        public int Balance
        {
            get => _balance;
            private set
            {
                if (value >= 0)
                {
                    _balance = value;
                    OnBalanceChanged?.Invoke(_balance);
                    if (_isBuyingSpin)
                    {
                        OnSuccessfulSpin?.Invoke();
                        _isBuyingSpin = false;                        
                    }
                    
                    PlayerPrefs.SetInt("Balance", _balance);
                }
            }
        }

        public int CurrentBet
        {
            get
            {
                if (_betIndex <= BET_VALUES.Length - 1)
                    return BET_VALUES[_betIndex];
                else
                    return BET_VALUES[BET_VALUES.Length - 1];
            }
        }

        public void AddBalance(float coeff) => Balance += (int)(CurrentBet * coeff);
        public void RemoveBalance()
        {
            _isBuyingSpin = true;
            Balance -= CurrentBet;
        }

        public void AddBet() => BetIndex++;
        public void RemoveBet() => BetIndex--;

        public void SendSignalToView()
        {
            OnBetChanged?.Invoke(CurrentBet);
            OnBalanceChanged?.Invoke(_balance);
        }
    }
}