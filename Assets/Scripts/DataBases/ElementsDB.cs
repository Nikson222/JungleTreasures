using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace DataBases
{
    [CreateAssetMenu(fileName = "ElementsData", menuName = "Elements Data", order = 51)]
    public class ElementsDB : ScriptableObject, IElementsDB
    {
        [SerializeField] private List<ElementInfo> _elements;
        
        public List<ElementInfo> Elements => _elements;
        
        public ElementInfo GetElementInfo(ElementType type)
        {  
            if(_elements.Count == 0)
                throw new Exception($"Elements data list is empty");
            
            return _elements.FirstOrDefault(element => element.Type == type);
        }

        public ElementType GetRandomElement()
        {
            return _elements[Random.Range(0, _elements.Count)].Type;
        }

        private void OnValidate()
        {
            foreach (var element in _elements)
            {
                element.name = element.Type.ToString();
                foreach (var winRate in element.WinRates)
                {
                    winRate.name = winRate.WinsRate.ToString();
                }
            }
        }

        [Serializable]
        public class ElementInfo
        {
            public string name;
            
            [SerializeField] private ElementType _type;
            [SerializeField] private Sprite _sprite;
            [SerializeField] private Sprite _winSprite;
            [SerializeField] private List<WinRate> _winRates;
            
            public ElementType Type => _type;
            public Sprite Sprite => _sprite;
            public Sprite WinSprite => _winSprite;
            public List<WinRate> WinRates => _winRates;
            
            public float GetWinRate(int count)
            {
                if(_winRates.Count == 0)
                    throw new Exception($"Win rates of type {_type} list is empty");
                
                foreach (var winRate in _winRates)
                {
                    if(count >= winRate.MinCount && count <= winRate.MaxCount)
                        return winRate.WinsRate;
                }
                
                return 0;
            }
        }

        [Serializable]
        public class WinRate
        {
            public string name;
            
            [SerializeField] private int _minCount;
            [SerializeField] private int _maxCount;
            [SerializeField] private float _winRate;
            
            public int MinCount => _minCount;
            public int MaxCount => _maxCount;
            public float WinsRate => _winRate;
        }
    }

    public enum ElementType
    {
        first,
        second,
        third,
        fourth,
        fifth,
        sixth,
        seventh,
        eighth
    }
}