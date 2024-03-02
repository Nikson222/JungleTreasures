using DataBases;
using DG.Tweening;

namespace SlotsInterface
{
    public interface ISlotController
    {
        public void HandleSpinSlot(ElementType type, bool isWin);
        public Sequence LastAnimateSequence { get; }
    }
}