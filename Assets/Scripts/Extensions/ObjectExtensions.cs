using System;
using System.Collections;
using UnityEngine;


using DG.Tweening;


namespace Extensions
{
    public static class ObjectExtensions
    {
       

        public static void CloseObject(this GameObject gameObject)
        {
            
            gameObject.SetActive(false);
        }
        public static void ActionObject(this GameObject gameObject,Action<int> action)
        {

            gameObject.SetActive(false);
        }

        public static GameObject CreateObject(GameObject objectType,Vector3 lastGridPos,Transform parent)
        {
            
            GameObject g =  UnityEngine.Object.Instantiate(objectType, lastGridPos,objectType.transform.localRotation,parent);
            g.transform.localPosition = lastGridPos;
            g.transform.localRotation = objectType.transform.localRotation;
            g.transform.DOScale(Vector3.zero, 0.1f).From();
            g.SetActive(true);
            return g;
        }
        
        public static int RandomValue(int count)
        {
            return UnityEngine.Random.Range(0, count);
        }
        
        public static void CloseObject(this GameObject gameObject,float time,MonoBehaviour monoBehaviour)
        {

            gameObject.SetActive(false);
            
            monoBehaviour.StartCoroutine(TurnOnObject(gameObject, time));
            
        }
        public static void WorkWithDelay(this GameObject gameObject)
        {
            
                
        }

        private static IEnumerator TurnOnObject(GameObject gameObject,float time)
        {
            yield return new WaitForSeconds(time);
            gameObject.SetActive(true);
        }

       
       

        public static void  AttachComponent<T>(this GameObject t) where T : UnityEngine.Object
        {
            
        }
        
    }
}