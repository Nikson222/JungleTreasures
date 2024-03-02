using PlayerInterfaces;
using UnityEngine;

namespace Player
{
    public class PlayerController : IPlayerController
    {
        private readonly IPlayerView _view;
        private readonly IPlayerModel _model;

        public PlayerController(IPlayerView view, IPlayerModel model)
        {
            _view = view;
            _model = model;
            
            _view.AddIncreaseBetClickListener(() => HandleBetChangeClick(true));
            _view.AddDecreaseBetClickListener(() => HandleBetChangeClick(false));
            _view.AddSpinClickListener(HandleSpinClick);
            
            _model.OnBetChanged += HandleBetChanged;
            _model.OnBalanceChanged += HandleBalanceChanged;
            
            _model.SendSignalToView();
        }

        private void HandleBetChangeClick(bool isAdding)
        {
            if (isAdding)
                _model.AddBet();
            else
                _model.RemoveBet();
        }

        private void HandleBetChanged(int bet)
        {
            _view.SetBetText(bet);
        }

        private void HandleSpinClick()
        {
            _model.RemoveBalance();
        }

        private void HandleBalanceChanged(int balance)
        {
            _view.SetBalanceText(balance);
        }
    }
}