using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickUp : MonoBehaviour
{
    public InputActionReference gripButton;
    public GameObject handCollider;  // Hand collider (can be left or right hand)
    public Transform grabPosition;   // Position where the object is held (can be left or right hand)
    public float distanceToEnableCollision = 1.0f;  // Distance threshold to re-enable collision
    public float moveToGrabSpeed = 5.0f;  // Speed at which the object moves to the grab position
    public float positionTolerance = 0.01f;  // Tolerance to consider the object "reached" the grab position

    private GameObject heldObject = null;
    private Rigidbody heldObjectRb;
    private Collider heldObjectCollider;
    private Vector3 previousHandPosition;
    private Vector3 handVelocity;
    private bool isObjectThrown = false;
    private bool isMovingToGrabPosition = false;  // Flag to check if object is moving to grab position
    private bool isFollowingHand = false;  // Flag to check if object should follow the hand directly

    private static bool isObjectHeld = false;  // Tracks if any hand is holding the object

    private void OnEnable()
    {
        gripButton.action.performed += OnGripPerformed;
        gripButton.action.canceled += OnGripCanceled;
    }

    private void OnDisable()
    {
        gripButton.action.performed -= OnGripPerformed;
        gripButton.action.canceled -= OnGripCanceled;
    }

    private void OnGripPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("Grippy");

        // Only allow pickup if no hand is holding the object
        if (heldObject == null && handCollider != null && !isObjectHeld)
        {
            // Try to grab an object within the hand collider
            Collider[] colliders = Physics.OverlapSphere(handCollider.transform.position, 0.1f);
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Grabbable"))
                {
                    heldObject = collider.gameObject;
                    heldObjectRb = heldObject.GetComponent<Rigidbody>();
                    heldObjectCollider = heldObject.GetComponent<Collider>();

                    if (heldObjectRb != null)
                    {
                        heldObjectRb.isKinematic = true;  // Disable physics while holding
                    }

                    isObjectHeld = true;  // Set the object as held
                    isMovingToGrabPosition = true;  // Start moving the object to the grab position
                    isFollowingHand = false;  // Reset following flag
                    Debug.Log("Object grabbed!");
                    break;
                }
            }
        }
    }

    private void OnGripCanceled(InputAction.CallbackContext context)
    {
        if (heldObject != null)
        {
            // Release the object and apply throw velocity
            if (heldObjectRb != null)
            {
                heldObjectRb.isKinematic = false;  // Re-enable physics interactions
                heldObjectRb.velocity = handVelocity;  // Apply hand's velocity to the object

                isObjectThrown = true;  // Mark the object as thrown
                Debug.Log("Object thrown with velocity: " + handVelocity);
            }

            Debug.Log("Object released!");
            heldObject = null;
            isObjectHeld = false;  // Set the object as not held, so another hand can pick it up
            isMovingToGrabPosition = false;  // Stop moving to grab position
            isFollowingHand = false;  // Stop following hand
        }
    }

    private void Update()
    {
        if (heldObject != null && heldObjectRb != null)
        {
            if (isMovingToGrabPosition)
            {
                // Gradually move and rotate the held object towards the grab position
                heldObject.transform.position = Vector3.Lerp(heldObject.transform.position, grabPosition.position, Time.deltaTime * moveToGrabSpeed);
                heldObject.transform.rotation = Quaternion.Slerp(heldObject.transform.rotation, grabPosition.rotation, Time.deltaTime * moveToGrabSpeed);

                // Check if the object has reached the grab position
                if (Vector3.Distance(heldObject.transform.position, grabPosition.position) < positionTolerance)
                {
                    // Snap the object to the grab position and start following
                    heldObject.transform.position = grabPosition.position;
                    heldObject.transform.rotation = grabPosition.rotation;
                    isMovingToGrabPosition = false;  // Stop moving to grab position
                    isFollowingHand = true;  // Start following the hand
                }
            }
            else if (isFollowingHand)
            {
                // Directly set the object's position and rotation to match the grab position
                heldObject.transform.position = grabPosition.position;
                heldObject.transform.rotation = grabPosition.rotation;
            }

            // Calculate hand velocity for throwing
            handVelocity = (grabPosition.position - previousHandPosition) / Time.fixedDeltaTime;
            previousHandPosition = grabPosition.position;
        }

        // Check if the thrown object has traveled far enough to re-enable the child object
        if (isObjectThrown && heldObjectRb != null)
        {
            float distanceFromHand = Vector3.Distance(grabPosition.position, heldObjectRb.position);

            if (distanceFromHand > distanceToEnableCollision)
            {
                if (heldObjectCollider != null)
                {
                    heldObjectCollider.enabled = true;  // Re-enable object's own collision
                }

                isObjectThrown = false;  // Reset the thrown status
            }
        }
    }
}
