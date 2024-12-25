using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRController : MonoBehaviour
{
    [SerializeField] private float minAngle = 20f; // Sudut minimum untuk mendeteksi menunduk
    [SerializeField] private float maxAngle = 60f; // Sudut maksimum untuk mendeteksi menunduk
    [SerializeField] private float moveSpeed = 3f; // Kecepatan gerak karakter
    [SerializeField] private Transform cameraTransform; // Transform kamera
    [SerializeField] private CharacterController characterController; // Komponen CharacterController
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioNow;
    private bool isMoving; // Status pergerakan

    // Update is called once per frame
    void Update()
    {
        LookDownToMove();
        MoveCharacter();
    }

    private void LookDownToMove()
    {
        // Mendapatkan sudut rotasi kamera pada sumbu X
        float cameraAngle = cameraTransform.eulerAngles.x;

        // Menentukan apakah kamera berada dalam rentang sudut yang ditentukan
        isMoving = cameraAngle > minAngle && cameraAngle < maxAngle;
    }

    private void MoveCharacter()
    {
        if (!isMoving) return; 
        Vector3 direction = cameraTransform.TransformDirection(Vector3.forward);
        characterController.Move(direction * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        IntructionData intructionData = other.GetComponent<IntructionData>();
        if (other.tag == "Instruction") // Memeriksa tag dari collider yang bersentuhan
        {
            if (intructionData != null)
            {
                // Cek apakah audioSource sedang tidak memutar audio
                if (!audioSource.isPlaying)
                {
                    audioNow = intructionData.audioInstruction;
                    audioSource.clip = audioNow;
                    audioSource.Play();
                }
            }
        }
        if (other.tag == "Exit") // Memeriksa tag dari collider yang bersentuhan
        {
            Application.Quit();
        }
    }
}
