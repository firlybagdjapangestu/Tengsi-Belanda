using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamepadControllerFor3DSpace : MonoBehaviour
{
    public float speed = 5f; // Kecepatan pergerakan
    public float gravity = 9.81f; // Besar gravitasi
    private CharacterController controller;
    [SerializeField] private Transform mainCamera;
    [SerializeField] private Animator charAnimation;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        mainCamera = Camera.main.transform;
    }

    void Update()
    {
        // Mendapatkan input dari gamepad
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Menentukan vektor pergerakan
        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical).normalized;

        if (movement != Vector3.zero)
        {
            // Jika bergerak, aktifkan animasi running
            charAnimation.SetBool("IsRunning", true);
        }
        else
        {
            // Jika tidak bergerak, matikan animasi running
            charAnimation.SetBool("IsRunning", false);
        }

        // Menghadapkan karakter ke arah hadap kamera
        Vector3 moveDirection = Quaternion.Euler(0, mainCamera.eulerAngles.y, 0) * movement;

        // Menggerakkan objek menggunakan CharacterController dengan gravitasi
        if (controller.isGrounded)
        {
            // Reset kecepatan vertikal jika sudah menyentuh tanah
            moveDirection.y = 0f;
        }
        else
        {
            // Terapkan gaya gravitasi ke kecepatan vertikal
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Menggerakkan karakter
        controller.Move(moveDirection * speed * Time.deltaTime);
    }
}
