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

    private GameObject colChildren;  // Parent of child colliders to disable
    private GameObject handChildObject;  // Reference to "hands:r_hand_world" child object

    private GameObject heldObject = null;
    private Rigidbody heldObjectRb;
    private Collider heldObjectCollider;
    private Vector3 previousHandPosition;
    private Vector3 handVelocity;
    private bool isObjectThrown = false;

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

    /*
    private void Start()
    {
        // Find the child ojects with the hand collider to disable when throwing"
        handChildObject = FindChildByName("hands:l_hand_world", colChildren.transform);
        handChildObject = FindChildByName("hands:r_hand_world", colChildren.transform);
    }
    */

    private void OnGripPerformed(InputAction.CallbackContext context)
    {

        Debug.Log("Grippy");
        if (heldObject == null && handCollider != null)
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


                // Disable the "hands:r_hand_world" child object
                if (handChildObject != null)
                {
                    handChildObject.SetActive(false);
                }

                isObjectThrown = true;  // Mark the object as thrown
                Debug.Log("Object thrown with velocity: " + handVelocity);
            }

            Debug.Log("Object released!");
            heldObject = null;
        }
    }

    private void FixedUpdate()
    {
        if (heldObject != null && heldObjectRb != null)
        {
            // Move the held object towards the grab position
            heldObject.transform.position = grabPosition.position;
            heldObject.transform.rotation = grabPosition.rotation;

            // Calculate hand velocity
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

                // Re-enable the "hands:r_hand_world" child object
                if (handChildObject != null)
                {
                    handChildObject.SetActive(true);
                }

                isObjectThrown = false;  // Reset the thrown status
                Debug.Log("Child object 'hands:r_hand_world' re-enabled after traveling distance: " + distanceFromHand);
            }
        }
    }

    /*
    // Helper function to find a child object by name recursively
    private GameObject FindChildByName(string name, Transform parent)
    {
        foreach (Transform child in parent)
        {
            if (child.name == name)
            {
                return child.gameObject;
            }
            GameObject result = FindChildByName(name, child);  // Search recursively
            if (result != null)
            {
                return result;
            }
        }
        return null;  // Return null if no child object was found with the specified name
    }
    */
}
