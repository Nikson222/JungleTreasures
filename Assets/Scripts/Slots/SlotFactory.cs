using DataBases;
using DefaultNamespace;
using UnityEngine;
using Zenject;

namespace Slots
{
    public class SlotFactory : ISlotFactory
    {
        private readonly SlotParent _slotParent;
        private readonly IElementsDB _elementsDB;
        private readonly SlotView _slotViewPrefab;
        private readonly SlotsAnimator _slotsAnimator;
        
        public SlotParent SlotParent => _slotParent;
        public SlotFactory(SlotParent slotParent, IElementsDB elementsDB, SlotsAnimator slotsAnimator)
        {
            _slotParent = slotParent;
            _elementsDB = elementsDB;
            
            _slotsAnimator = slotsAnimator;
            
            _slotViewPrefab = Resources.Load<SlotView>("SlotView");
        }
        
        public SlotController Create(int index)
        {
            SlotView slotView = Object.Instantiate(_slotViewPrefab);
            slotView.transform.SetParent(_slotParent.transform);
            slotView.transform.localScale = Vector3.one;
            slotView.transform.localPosition = Vector3.zero;
            
            var slotModel = new SlotModel(0, index);
            var slotController = new SlotController(slotView, slotModel, _elementsDB, _slotsAnimator);
            
            return slotController;
        }
    }
}