using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class SlotsAnimator
    {
        public Sequence ScaleUpIcon(Image image)
        {
            Sequence sequence = DOTween.Sequence();
            
            sequence.Append(image.transform.DOScale(new Vector3(1.05f, 1.05f, 1f), 0.5f)).SetEase(Ease.InSine);
            
            return sequence;
        }

        public Sequence ScaleDownIcon(Image image)
        {
            Sequence sequence = DOTween.Sequence();
            
            sequence.Append(image.transform.DOScale(Vector3.zero, 0.25f)).SetEase(Ease.OutSine);
            
            return sequence;
        }

        public Sequence ScaleToStandartSizeIcon(Image image)
        {
            Sequence sequence = DOTween.Sequence();
            
            sequence.Append(image.transform.DOScale(Vector3.one, 0.25f)).SetEase(Ease.Linear);
            
            return sequence;
        }

        public Sequence SimpleChangeIconAnimate(Image image, Sprite newSprite)
        {
            var upScaleSeq = ScaleUpIcon(image);
            var downScaleSeq = ScaleDownIcon(image);
            var secondUpScaleSeq = ScaleUpIcon(image);
            var standartScaleSeq = ScaleToStandartSizeIcon(image);

            downScaleSeq.Pause();
            secondUpScaleSeq.Pause();
            standartScaleSeq.Pause();

            upScaleSeq.OnComplete(() => downScaleSeq.Play());
            downScaleSeq.OnComplete(() =>
            {
                image.sprite = newSprite;
                secondUpScaleSeq.Play();
            });
            secondUpScaleSeq.OnComplete(() => standartScaleSeq.Play());
            
            return standartScaleSeq;
        }
        
        public Sequence WinChangeIconAnimate(Image image, Sprite newSprite, Sprite winSprite)
        {
            var upScaleSeq = ScaleUpIcon(image);
            var downScaleSeq = ScaleDownIcon(image);
            var secondUpScaleSeq = ScaleUpIcon(image);
            var standartScaleSeq = ScaleToStandartSizeIcon(image);

            downScaleSeq.SetDelay(1f);
            
            downScaleSeq.Pause();
            secondUpScaleSeq.Pause();
            standartScaleSeq.Pause();
            
            image.sprite = winSprite;
            
            upScaleSeq.OnComplete(() =>
            {
                
                downScaleSeq.Play();
            });
            
            downScaleSeq.OnComplete(() =>
            {
                image.sprite = newSprite;
                secondUpScaleSeq.Play();
            });
            secondUpScaleSeq.OnComplete(() => standartScaleSeq.Play());
            
            return standartScaleSeq;
        }
    }
}