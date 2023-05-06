using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MainCams : MonoBehaviour
{
    public static MainCams instance;

        
        public Transform thirdPersonCam;
        
        public Transform target;
        public Vector3 offset;
        public float lerpValue;

        public float camMoveTime = 1;

        private bool isRotating = false;

        public Vector3  firstPos;
        public Vector3 firstRot;
        private bool isOnThird = false;
        
        private void Awake()
        {
            if (!instance)
            {
                instance = this;
            }
        }

        private void Start()
        {
            offset = transform.localPosition - target.transform.localPosition;
            firstPos = transform.localPosition;
            firstRot = transform.localEulerAngles;
        }


        private void LateUpdate()
        {
            /*if (!isRotating)
            {
                Vector3 desPos = offset + this.target.position;
                transform.position = Vector3.Lerp(transform.position, desPos, lerpValue);
                
            }
            */
            
           
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("bla");
                isRotating = true;
                if (!isOnThird)
                {
                    GoToThirdPerson();
                }
                else
                {
                    GoBackToFirst();
                }
            }
        }

        public void GoToThirdPerson()
        {

            offset = thirdPersonCam.localPosition - target.transform.localPosition;
            isOnThird = true;
            transform.DOLocalMove(thirdPersonCam.transform.localPosition, 0.15f).OnComplete(()=>isRotating = false);
            transform.DOLocalRotate(thirdPersonCam.transform.localEulerAngles, 0.15f);
            
            
        }

        public void GoBackToFirst()
        {
            offset = firstPos - target.transform.localPosition;

            isOnThird = false;
            transform.DOLocalMove(firstPos, 0.15f).OnComplete(()=>isRotating = false);
            transform.DOLocalRotate(firstRot, 0.15f);

        }
        

        public void MoveToTarget(Transform targetTemp)
        {
            isRotating = true;
            transform.parent = targetTemp.transform.parent;
           
            
            StartCoroutine(StartRotating(targetTemp));
            transform.DOLocalMove(targetTemp.localPosition, camMoveTime).OnComplete(() =>
            {
                //isRotating = false;
               
            });

        }

        private IEnumerator StartRotating(Transform target)
        {
            yield return new WaitForSeconds(0.14f);
            transform.DOLocalRotate(target.transform.localEulerAngles, camMoveTime-0.25f).OnComplete(() =>
            {
                Invoke("GetBackParent", 0.2f);
                //transform.parent = null;
                
            });

        }

        private void GetBackParent()
        {
            transform.parent = null;
        }
        
        /*public void GoToThirdPerson()
        {
            isRotating = true;
            Vector3 pos = offset + this.target.position;
            
            //transform.parent = thirdPersonCam.transform.parent;
            
            transform.DOLocalRotate(thirdPersonCam.transform.localEulerAngles, camMoveTime);
            transform.DOLocalMove(pos, camMoveTime).OnComplete(() =>
            {
                //isRotating = false;
                
                transform.parent = null;
            });

        }
        */


       
}

