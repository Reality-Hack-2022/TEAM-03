using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Interaction;
using Leap;
using System;
using LookingGlass;

public class DebugHandTracking : MonoBehaviour
{
    public Leap.Unity.CapsuleHand leftHand;
    public Leap.Unity.CapsuleHand rightHand;
    public GameObject player;
    public Holoplay holoplay;
    private Hand leftLeapHand;
    private Hand rightLeapHand;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        leftLeapHand = leftHand.GetLeapHand();
        rightLeapHand = rightHand.GetLeapHand();
        CheckHandMovement();
    }

    private void CheckHandMovement()
    {

        if (leftLeapHand == null && rightLeapHand == null)
        {
            return;
        }

        if (leftLeapHand == null || rightLeapHand == null)
        {
            CheckSingleHandMovement(leftLeapHand);
            CheckSingleHandMovement(rightLeapHand);
        } else // If both hands are in frame
        {
            if (leftLeapHand.GrabStrength > 0.5 && rightLeapHand.GrabStrength > 0.5)
            {
                // which hand has greater magnitude
                Hand greaterVelocityHand = CheckGreaterVelocity();
                HandleZoom(greaterVelocityHand);

            } else
            {
                CheckSingleHandMovement(leftLeapHand);
                CheckSingleHandMovement(rightLeapHand);
            }
        }

    }

    private Hand CheckGreaterVelocity()
    {
        if (Math.Abs(rightLeapHand.PalmVelocity.x) > Math.Abs(leftLeapHand.PalmVelocity.x))
        {
            return rightLeapHand;
        } else
        {
            return leftLeapHand;
        }
    }

    private void HandleZoom(Hand hand)
    {
        //clamp size between 1 and .25
        float scaleFactor = .05f;
        
        if (Math.Abs(hand.PalmVelocity.x) < 0.03)
        {
            return;
        }
       
        if ((hand.PalmVelocity.x < 0 && hand.IsRight) || (hand.PalmVelocity.x > 0 && hand.IsLeft))
        {
            scaleFactor = 1.04f + scaleFactor;
        } else
        {
            scaleFactor = 0.98f - scaleFactor;
        }

        holoplay.size = Mathf.Clamp(holoplay.size * scaleFactor, 0.25f, 1f);
    }

    private void CheckSingleHandMovement(Hand hand)
    {
        if (hand == null || hand.GrabStrength < 0.5)
        {
            return;
        }

        if (hand.PalmVelocity.x > 0.075)
        {
            player.transform.Rotate(0, -600 * Time.deltaTime, 0);
            // Debug.Log("Left hand moving right");
        }
        else if (hand.PalmVelocity.x > -0.075)
        {
             // Do nothing
        }
        else
        {
            // Debug.Log("Moving left");
            player.transform.Rotate(0, 600 * Time.deltaTime, 0);
        }
    }
}
