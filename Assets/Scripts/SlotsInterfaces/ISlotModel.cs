using System;
using DataBases;

namespace SlotsInterface
{
    public interface ISlotModel
    {
        ElementType ElementType { get; set; }
        
        public event Action<ElementType, ElementType> OnTypeChanged; 
    }
}
