using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f; // Kecepatan gerakan pemain
    private CharacterController characterController;

    void Start(){
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        // Mengambil input dari keyboard
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0 , Input.GetAxis("Vertical"));
        characterController.Move(move * Time.deltaTime * moveSpeed);
    }
}