using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataBases;
using DG.Tweening;
using PlayerInterfaces;
using SlotsInterface;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Slots
{
    public class FieldController : MonoBehaviour, IFieldController
    {
        private const int SLOT_COUNT = 30;
        private const int ROW_COUNT = 5;
        
        private IElementsDB _elementsDB;

        ISlotFactory _slotFactory;

        private List<SlotController> _slots;

        [SerializeField] private Button SpinButton;
        
        private bool _isRollingProcess = false;
        public event Action<float> OnWin;
        public event Action OnRerollEnded;

        [Inject]
        public void Construct(ISlotFactory slotFactory, IElementsDB elementsDB, IPlayerModel playerModel)
        {
            _elementsDB = elementsDB;

            _slots = new List<SlotController>();

            _slotFactory = slotFactory;
            InitSlots();

            playerModel.OnSuccessfulSpin += SpinSlotsTypes;
            OnWin += playerModel.AddBalance;
        }

        private void InitSlots()
        {
            for (int i = 0; i < SLOT_COUNT; i++)
            {
                var slots = _slotFactory.Create(i);
                _slots.Add(slots);
            }
            
            // var ySpacing = _slotFactory.SlotParent.YSpacing + _slotFactory.SlotParent.YCellSize/2;
            //
            // _slotFactory.SlotParent.transform.localPosition = 
            //     new Vector3(_slotFactory.SlotParent.transform.localPosition.x, 
            //         _slotFactory.SlotParent.transform.localPosition.y - ySpacing * ROW_COUNT, 0);
        }

        private void SpinSlotsTypes()
        {
            if (_isRollingProcess)
                return;
            
            _isRollingProcess = true;
            
            Sequence sequence = DOTween.Sequence();
            
            foreach (var slot in _slots)
            {
                ElementType type = _elementsDB.GetRandomElement();
                slot.HandleSpinSlot(type, false);
                
                sequence = slot.LastAnimateSequence;
            }

            sequence.OnComplete(CalculateWin);
        }

        private void CalculateWin()
        {
            if (CheckWinCondition())
            {
                var maxElement = GetSomeMaxElementCount();
                var winRate = _elementsDB.GetElementInfo(maxElement.Type).GetWinRate(maxElement.Count);
                
                Debug.Log("win" + " " + winRate);

                SpinWinnedSlots(maxElement.Type);
                
                OnWin?.Invoke(winRate);
            }
            else
            {
                OnRerollEnded?.Invoke();
                _isRollingProcess = false;
            }
        }

        private void SpinWinnedSlots(ElementType type)
        {
            Sequence sequence = DOTween.Sequence();
            
            foreach (var slot in _slots)
            {
                if (slot.ElementType == type)
                {
                    ElementType newType = _elementsDB.GetRandomElement();
                    slot.HandleSpinSlot(newType, true);

                    sequence = slot.LastAnimateSequence;
                }
            }

            sequence.OnComplete(CalculateWin);
        }
        
        private bool CheckWinCondition()
        {
            var maxElement = GetSomeMaxElementCount();

            var winRate = _elementsDB.GetElementInfo(maxElement.Type).GetWinRate(maxElement.Count);

            return winRate > 0;
        }

        private MaxElement GetSomeMaxElementCount()
        {
            var winElement = _slots.GroupBy(x => x.ElementType)
                .Select(g => new MaxElement() { Type = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count).FirstOrDefault();

            return winElement;
        }

        private struct MaxElement
        {
            public ElementType Type;
            public int Count;
        }
    }
}