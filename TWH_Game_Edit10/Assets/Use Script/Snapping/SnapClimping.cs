using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class SnapClimping : MonoBehaviour
//{
//    public List<Transform> snapPoints;
//    public List<PlayerMovement> snapObj;
//    public float snapRange = 0.5f;
//    public bool _isSnap;

//    public void SnapObject(Transform obj)
//    {
//        foreach(Transform point in snapPoints)
//        {
//            if (Vector2.Distance(point.position, obj.position) <= snapRange && _isSnap)
//            {
//                obj.position = point.position;
//                return;
//            }
//        }
//    }

//    private void Start()
//    {
//        foreach (PlayerMovement script in snapObj)
//        {
//            script.snapEnable = SnapObject;
//        }
//    }


//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (collision.CompareTag("Player"))
//        {
//            _isSnap = true;
//        }                 
//    }

//    private void OnTriggerExit2D(Collider2D collision)
//    {
//        if (collision.CompareTag("Player"))
//        {
//            _isSnap = false;
//        }
//    }





    //private IEnumerator _snapToCoroutine;
    //public bool _isSnap;

    //public void SnapTo(Vector3 snapPosition, Quaternion snapRotation, float duration)
    //{
    //    if (_isSnap == true)
    //    {
    //        StopCoroutine(_snapToCoroutine);
    //        _snapToCoroutine = SnapToCoroutine(snapPosition, snapRotation, duration);
    //        StartCoroutine(_snapToCoroutine);
    //    }
    //}

    //public void CancelSnap()
    //{
    //    if (_isSnap == false) 
    //    { 
    //        StopCoroutine(_snapToCoroutine);
    //    }
    //}

    //private IEnumerator SnapToCoroutine(Vector3 snapPosition, Quaternion snapRotation, float duration)
    //{
    //    float startTime = Time.time;
    //    Vector3 startPosition = transform.position;
    //    Quaternion startRotation = transform.rotation;

    //    while (Time.time - startTime < duration)
    //    {
    //        float t = (Time.time - startTime) / duration;

    //        transform.SetPositionAndRotation(Vector3.Lerp(startPosition, snapPosition, t), Quaternion.Lerp(startRotation, snapRotation, t));

    //        yield return null;
    //    }

    //    transform.SetPositionAndRotation(snapPosition, snapRotation);
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player"))
    //    {
    //        _isSnap = true;
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player"))
    //    {
    //        CancelSnap();
    //        _isSnap = false;
    //    }
    //}
//}
