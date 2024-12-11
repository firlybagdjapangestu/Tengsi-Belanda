using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float minAngle, maxAngle; // sudut untuk membuat kapan karakter akan berjalan
    public float ms; // kecepatan berjalan
    public float gravity = 9.81f;
    [SerializeField] private bool isMoving; // untuk aktif/non aktifkan jalan
    [SerializeField] private CharacterController characterController; // mengambil compponent
    [SerializeField] private Transform camera; // mengambil component


    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioNow;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        LookDownTOMove();
        Moving();
    }

    private void LookDownTOMove()
    {
        if(camera.eulerAngles.x > minAngle && camera.eulerAngles.x < maxAngle) // mengambil rotasi kamera
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }

    private void Moving()
    {
        if (!isMoving) return; // pengembalin langsung
        Vector3 toward = camera.TransformDirection(Vector3.forward); // mengambil direksi kamera
        characterController.Move(toward * ms * Time.deltaTime); // membuat karakter bergerak sesuai arah kamera
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
