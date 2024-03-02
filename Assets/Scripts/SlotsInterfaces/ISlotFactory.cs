namespace Slots
{
    public interface ISlotFactory
    {
        SlotController Create(int index);
        public SlotParent SlotParent { get; }
    }
}