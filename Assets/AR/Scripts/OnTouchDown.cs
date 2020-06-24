using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OnTouchDown : MonoBehaviour
{
    //local class to store information about the touch
    private class TouchInfo
    {
        public Vector2 firstTouch = new Vector3();
        public Vector2 lastTouch = new Vector3();
        public RaycastHit hit = new RaycastHit(); //stores the original object hit by the first touch

        public TouchInfo(Vector3 firstTouchIn, Vector3 lastTouchIn, RaycastHit hitIn)
        {
            firstTouch = firstTouchIn;
            lastTouch = lastTouchIn;
            hit = hitIn;
        }
    }


    //Dictionary relating the fingerIDs to the info we need
    private Dictionary<int, TouchInfo> touchDict;
    private float dragDistance;  //minimum distance for a swipe to be registered

    private void Start()
    {
        dragDistance = Screen.height * 10 / 100; //dragDistance is 15% height of the screen
        touchDict = new Dictionary<int, TouchInfo>();
    }

    void Update() {

        for (int i = 0; i < Input.touchCount; ++i)
        {
            Touch touch = Input.GetTouch(i);
            var touchPosition = touch.position;

            if (touch.phase.Equals(TouchPhase.Began)) //check for the first touch
            {

                // Construct a ray from the current touch coordinates
                RaycastHit hit = new RaycastHit();
                Ray ray = Camera.main.ScreenPointToRay(touchPosition);

                if (Physics.Raycast(ray, out hit) && !touchPosition.IsPointOverUIObject())
                {
                    TouchInfo touchInfo = new TouchInfo(touchPosition, touchPosition, hit);
                    touchDict.Add(touch.fingerId, touchInfo);
                    Debug.Log("OnTouchDown: Update --- Touch detected");
                }
            }

            else if (touch.phase == TouchPhase.Moved && touchDict.ContainsKey(touch.fingerId)) // update the last position based on where they moved
            {
                touchDict[touch.fingerId].lastTouch = touchPosition;
            }

            else if (touch.phase == TouchPhase.Ended && touchDict.ContainsKey(touch.fingerId)) //check if the finger is removed from the screen
            {
                touchDict[touch.fingerId].lastTouch = touchPosition;

                //Check if the drag distance is greater than dragDistance
                if ( Vector2.Distance(touchDict[touch.fingerId].firstTouch, touchDict[touch.fingerId].lastTouch) > dragDistance)
                {
                    if(touchDict[touch.fingerId].lastTouch.y > touchDict[touch.fingerId].firstTouch.y)
                    {
                        Debug.Log("OnTouchDown: Update --- Up Swipe Registered");
                        touchDict[touch.fingerId].hit.transform.gameObject.SendMessage("OnSwipeUp");
                    }
                    else
                    {
                        Debug.Log("OnTouchDown: Update --- Down Swipe Registered");
                        touchDict[touch.fingerId].hit.transform.gameObject.SendMessage("OnSwipeDown");
                    }
                }
                else
                {
                    Debug.Log("OnTouchDown: Update --- Tap Registered");
                    touchDict[touch.fingerId].hit.transform.gameObject.SendMessage("OnTap");
                }

                touchDict.Remove(touch.fingerId);
            }
        }
    }
}
