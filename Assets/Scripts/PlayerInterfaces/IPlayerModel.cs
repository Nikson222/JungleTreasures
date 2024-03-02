using System;

namespace PlayerInterfaces
{
    public interface IPlayerModel
    {
        void AddBalance(float coeff);
        void RemoveBalance();
        
        void AddBet();
        void RemoveBet();
        
        void SendSignalToView();
        
        event Action<int> OnBalanceChanged;
        event Action<int> OnBetChanged;
        event Action OnSuccessfulSpin;
    }
}