using System;
using System.Runtime.InteropServices;
using DefaultNamespace;
using DG.Tweening;
using SlotsInterface;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Slots
{
    public class SlotView : MonoBehaviour, ISlotView
    {
        private SlotsAnimator _slotsAnimator;

        private bool _isInited = false;
        
        [SerializeField] private Image _elementImage;
        
        public Image ElementImage => _elementImage;
        
        public Sprite ElementSprite { get => _elementImage.sprite; set => SetElementImage(value); }
        
        public event Action OnSlotRolled;
        
        [Inject]
        public void Construct(SlotsAnimator slotsAnimator)
        {
            _slotsAnimator = slotsAnimator;
        }
        
        public void InitSprite(Sprite sprite) => _elementImage.sprite = sprite;
        
        public void SetElementImage(Sprite sprite)
        {
            _elementImage.sprite = sprite;
        }
    }
}