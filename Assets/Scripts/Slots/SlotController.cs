using DataBases;
using DefaultNamespace;
using DG.Tweening;
using SlotsInterface;
using UnityEngine;

namespace Slots
{
    public class SlotController : ISlotController
    {
        private ISlotView _slotView;
        private ISlotModel _slotModel;
        private SlotsAnimator _slotsAnimator;
        
        private Sequence _lastAnimateSequence;

        public Sequence LastAnimateSequence => _lastAnimateSequence;
        
        private readonly IElementsDB _elementsDB;
        
        public ElementType ElementType => _slotModel.ElementType;
        
        public ISlotView SlotView => _slotView;

        private bool _isWin;
        
        public SlotController(ISlotView slotView, ISlotModel slotModel, IElementsDB elementsDB, SlotsAnimator slotsAnimator)
        {
            _elementsDB = elementsDB;
            
            _slotView = slotView;
            _slotModel = slotModel;
            _slotsAnimator = slotsAnimator;
            
            Sprite newSprite = _elementsDB.GetElementInfo(0).Sprite;
            _slotView.InitSprite(newSprite);  
            _slotModel.ElementType = 0;
            
            _slotModel.OnTypeChanged += HandleElementTypeChanged;
        }

        public void HandleSpinSlot(ElementType type, bool isWin)
        {
            _isWin = isWin;
            _slotModel.ElementType = type;
        }

        private void HandleElementTypeChanged(ElementType oldType, ElementType newType)
        {
            Sprite newSprite = _elementsDB.GetElementInfo(newType).Sprite;
            Sprite winSprite = _elementsDB.GetElementInfo(oldType).WinSprite;
            
            _lastAnimateSequence = !_isWin ? _slotsAnimator.SimpleChangeIconAnimate(_slotView.ElementImage, newSprite) 
                : _slotsAnimator.WinChangeIconAnimate(_slotView.ElementImage, newSprite, winSprite);
        }
    }
}