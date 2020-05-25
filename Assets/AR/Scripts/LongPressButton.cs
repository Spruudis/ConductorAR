using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class LongPressButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool pointerDown;
    private float pointerDownTimer;

    private bool longClickTriggered = false;

    [SerializeField]
    private float requieredHoldTime;

    public UnityEvent onLongClick;

    public UnityEvent onLongClickRelease;

    [SerializeField]
    private Image fillImage;

    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //Reset button functionality when the finger is lifted
        if (onLongClickRelease != null && longClickTriggered)
        {
            Reset();
            longClickTriggered = false;
            Debug.Log("LongPressButton: OnPointerUp --- Invoking long press release functionality");
            onLongClickRelease.Invoke();
        }
        longClickTriggered = false;
        Reset();

    }

    private void Update()
    {
        if (pointerDown)
        {
            pointerDownTimer += Time.deltaTime;
            if(pointerDownTimer > requieredHoldTime)
            {
                //delete object
                Reset();
                Debug.Log("LongPressButton: Update --- Invoking long press functionality");
                if (onLongClick != null)
                    onLongClick.Invoke();
                longClickTriggered = true;
            }
            fillImage.fillAmount = pointerDownTimer / requieredHoldTime;
        }
    }

    private void Reset()
    {
        pointerDown = false;
        pointerDownTimer = 0.0f;
        fillImage.fillAmount = 0.0f;
    }

}
