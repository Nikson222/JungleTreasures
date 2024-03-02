using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace SlotsInterface
{
    public interface ISlotView
    {
        Sprite ElementSprite { get; set; }
        Image ElementImage { get;}
        void InitSprite(Sprite sprite);
        public void SetElementImage(Sprite sprite);
    }
}