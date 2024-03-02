using System;
using PlayerInterfaces;
using SlotsInterface;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Player
{
    public class PlayerView : MonoBehaviour, IPlayerView
    {
        [SerializeField] private Text _balanceText;
        [SerializeField] private Text _betText;
        
        [SerializeField] private Image _balanceBackground;
        [SerializeField] private Sprite _defaultBackground;
        [SerializeField] private Sprite _winningBackground;
        
        [Space(10)]
        [SerializeField] private Button _spinButton;
        [SerializeField] private Button _increaseBetButton;
        [SerializeField] private Button _decreaseBetButton;

        [Inject]
        public void Construct(IFieldController fieldController, IPlayerModel playerModel)
        {
            fieldController.OnRerollEnded += () =>
            {
                _balanceBackground.sprite = _defaultBackground;
                _balanceBackground.SetNativeSize();
                _spinButton.interactable = true;
            };
            fieldController.OnWin += (float win) =>
            {
                _balanceBackground.sprite = _winningBackground;
                _balanceBackground.SetNativeSize();
                _spinButton.interactable = false;
            };
            playerModel.OnSuccessfulSpin += () =>
            {
                _spinButton.interactable = false;
            };
        }

        public void AddIncreaseBetClickListener(Action listener) => _increaseBetButton.onClick.AddListener(() => listener());
        public void AddDecreaseBetClickListener(Action listener) => _decreaseBetButton.onClick.AddListener(() => listener());

        public void SetBalanceText(int balance) => _balanceText.text = balance.ToString();
        public void SetBetText(int bet) => _betText.text = bet.ToString();
        public void AddSpinClickListener(Action listener) => _spinButton.onClick.AddListener(() => listener());
    }
}