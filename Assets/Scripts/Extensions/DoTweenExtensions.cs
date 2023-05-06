using System;
using UnityEngine;
using DG.Tweening;

namespace Extensions
{
    public static class DoTweenExtensions
    {


        public static void SetScaleToZero(Transform target,float time)
        {
            Vector3 firstScale = target.localScale;

            target.DOScale(Vector3.zero, time).OnComplete(() =>
            {
                target.gameObject.SetActive(false);
                target.transform.localScale = firstScale;
            });


        }

        public static void TweenFloat(this float value,float targetValue, float duration)
        {
            DOTween.To(() => value, x => value = x, targetValue, duration);
        }
        public static float TweenFloatAndSet(this float value,float targetValue, float duration,Action<float> whatToDoAction,Action action)
        {
            DOTween.To(x => value = x , value , targetValue , duration).OnUpdate(() =>
            {
                whatToDoAction.Invoke(value);
            }).OnComplete(() =>
            {
                action.Invoke();
            });

            return value;

        }
        
      
        
        
    }
}