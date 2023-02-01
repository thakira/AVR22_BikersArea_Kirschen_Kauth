using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimation : MonoBehaviour
{
    [SerializeField] private InputActionProperty pinchAnimation;
    [SerializeField] private InputActionProperty gripAnimation;
    [SerializeField] private InputActionProperty pointAnimation;

    [SerializeField] private Animator handAnimator; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float triggerValue = pinchAnimation.action.ReadValue<float>();
        handAnimator.SetFloat("Trigger", triggerValue);
        float gripValue = gripAnimation.action.ReadValue<float>();
        handAnimator.SetFloat("Grip", gripValue);
        float pointValue = pointAnimation.action.ReadValue<float>();
        handAnimator.SetFloat("Point", pointValue);

    }
}
