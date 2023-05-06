using DG.Tweening;
using UnityEngine;

namespace Extensions
{
    public class RotHelperExtension 
    {
        public static void LookAtTheTarget(Transform lookAtTarget, Transform transform,float lerpValue)
        {
            Vector3 target = lookAtTarget.position;
            target.y = transform.position.y;
            Vector3 dir = target - transform.position;
        
            transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.LookRotation(dir),lerpValue * Time.deltaTime);
            
        }
        public static void LookAtTheTargetWithTween(Transform lookAtTarget, Transform transform,float lerpValue)
        {
            transform.DOKill();
            Vector3 target = lookAtTarget.position;
            target.y = transform.position.y;
            Vector3 dir = target - transform.position;

            transform.DORotateQuaternion(Quaternion.LookRotation(dir), lerpValue);
        }
    }
}