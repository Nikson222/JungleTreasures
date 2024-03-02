using System;
using UnityEngine;
using UnityEngine.UI;

namespace Slots
{
    public class SlotParent : MonoBehaviour
    {
        public GridLayoutGroup GridLayoutGroup;
        public float YSpacing => GridLayoutGroup.spacing.y;
        public float YCellSize => GridLayoutGroup.cellSize.y;
    }
}