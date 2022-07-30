using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ArutalaLogoHandler : MonoBehaviour
{
    [Header("Rotating Attribute")]
    public bool isSwipe;
    public float rotationSpeed;
    public Vector2 rotationFactor;

    [Header("Scalling Attribute")]
    public bool isPinch;
    public float scalationFactor;
    public float deltaTouchPosition;
    public float initObjectScale;
    Vector2 firstTouchPos;
    Vector2 secondTouchPos;
    int initDeltaPos;

    [Header("Blinking Attribute")]
    public GameObject blinkingObject;
    public Slider blinkingSlider;
    public Color firstColor;
    public Color secondColor;
    [Range(0f, 10f)]
    public float blinkingSpeed;

    // Update is called once per frame
    void Update()
    {
        print(blinkingSlider.value);
        blinkingObject.GetComponent<Renderer>().material.color = Color.Lerp(firstColor, secondColor, Mathf.PingPong(blinkingSpeed * Time.time, 1));
        blinkingSpeed = blinkingSlider.value * 10;
        GetComponent<Animator>().enabled = !isSwipe;

        if (Input.touchCount == 2) 
            PinchToZoom();
        else if (Input.touchCount == 0)
            isPinch = isSwipe = false;
    }

    void PinchToZoom()
    {
        firstTouchPos = Input.GetTouch(0).position;
        secondTouchPos = Input.GetTouch(1).position;

        if (!isPinch) 
        {
            initObjectScale = transform.localScale.x;
            initDeltaPos = (int)Vector2.Distance(firstTouchPos, secondTouchPos); 
            isPinch = true; 
        }
        else
        {
            deltaTouchPosition = ((int)Vector2.Distance(firstTouchPos, secondTouchPos) - initDeltaPos) * scalationFactor;
            transform.localScale = new Vector3(deltaTouchPosition + initObjectScale,
                                               deltaTouchPosition + initObjectScale,
                                               deltaTouchPosition + initObjectScale);
        }
    }

    void OnMouseDrag()
    {
        isSwipe = true;
        rotationFactor = new Vector2(Input.GetAxis("Mouse X") * rotationSpeed * Mathf.Deg2Rad,
                                     Input.GetAxis("Mouse Y") * rotationSpeed * Mathf.Deg2Rad);

        transform.Rotate(Vector3.up, -rotationFactor.x);
        transform.Rotate(Vector3.right, rotationFactor.y);
    }
}
