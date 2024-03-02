using System;

namespace PlayerInterfaces
{
    public interface IPlayerView
    {
        void SetBalanceText(int value);
        void SetBetText(int value);
        void AddSpinClickListener(Action listener);  
        void AddIncreaseBetClickListener(Action listener);
        void AddDecreaseBetClickListener(Action listener);
    }
}