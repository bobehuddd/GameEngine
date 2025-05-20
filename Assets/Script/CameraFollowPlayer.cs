using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public GameObject player; // Reference to the player GameObject

    [SerializeField] Vector3 offset = new Vector3(99, 7, -97); // Set the offset directly

    // Start is called before the first frame update
    void Start()
    {
        // Optional: You can log the offset to verify it's set correctly
        Debug.Log("Camera Offset: " + offset);
    }

    // LateUpdate is called once per frame after all Update methods
    void LateUpdate()
    {
        // Set the camera position based on the player's position and the offset
        transform.position = offset;
    }
}