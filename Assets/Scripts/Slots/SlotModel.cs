using System;
using DataBases;
using SlotsInterface;

namespace Slots
{
    public class SlotModel : ISlotModel
    {
        private ElementType _type;
        private readonly int _index;

        public ElementType ElementType { get => _type; set => SetElementType(value); }
        
        public event Action<ElementType, ElementType> OnTypeChanged;
        
        public SlotModel(ElementType type, int index)
        {
            _type = type;
            _index = index;
        }

        public void SetElementType(ElementType type)
        {
            ElementType oldType = _type;
            
            _type = type;
            
            OnTypeChanged?.Invoke(oldType, _type);
        }
    }
}