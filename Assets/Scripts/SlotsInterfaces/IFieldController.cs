using System;

namespace SlotsInterface
{
    public interface IFieldController
    {
        public event Action<float> OnWin;
        public event Action OnRerollEnded;
    }
}