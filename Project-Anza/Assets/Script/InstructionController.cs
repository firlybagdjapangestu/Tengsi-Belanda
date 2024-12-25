using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InstructionController : MonoBehaviour
{
    [SerializeField] private GameObject instructionImage;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioNow;
    [SerializeField] private TextMeshProUGUI textPenjelasanButton;
    [SerializeField] private bool exit;
    [SerializeField] private VRController playerController;
    [SerializeField] private bool playerControllerActivated;
    private bool audioIsPlaying;

    // Start is called before the first frame update
    void Start()
    {
        audioIsPlaying = false;
        CheckController();
        if(playerControllerActivated)
        {
            instructionImage.SetActive(true);
            textPenjelasanButton.text = "Anda Tidak Terhubung Controller Untuk Berjalan Tundukan Pandangan Anda";

        }
        else
        {
            instructionImage.SetActive(false); // Pastikan gambar instruksi tidak aktif saat memulai
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        // Memeriksa jika Fire1 ditekan
        if (Input.GetButtonDown("Fire1"))
        {
            if (exit)
            {
                Debug.Log("Keluar");
                Application.Quit(); // Panggil method Quit() untuk keluar dari aplikasi
            }
            // Memulai AudioClip
            if (audioSource != null && audioNow != null)
            {
                audioSource.clip = audioNow;
                if (audioSource.isPlaying)
                {
                    // Menghentikan audio jika sedang diputar
                    audioSource.Stop();
                    audioNow = null;
                }
                else
                {
                    // Memulai audio jika sedang tidak diputar
                    audioSource.Play();
                }
            } 
        }

    }


    private void OnTriggerStay(Collider other)
    {
        IntructionData intructionData = other.GetComponent<IntructionData>();
        if (other.tag == "Instruction") // Memeriksa tag dari collider yang bersentuhan
        {
            intructionData.textPenjelasan.SetActive(true);
            audioNow = intructionData.audioInstruction;
            textPenjelasanButton.text = intructionData.textPenjelasanButton;
            instructionImage.SetActive(true);
        }
        if (other.tag == "Exit") // Memeriksa tag dari collider yang bersentuhan
        {
            textPenjelasanButton.text = intructionData.textPenjelasanButton;
            instructionImage.SetActive(true);
            exit = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        IntructionData intructionData = other.GetComponent<IntructionData>();
        if (other.tag == "Instruction") // Memeriksa tag dari collider yang bersentuhan
        {
            intructionData.textPenjelasan.SetActive(false);
            instructionImage.SetActive(false);     
        }
        if (other.tag == "Exit") // Memeriksa tag dari collider yang bersentuhan
        {
            instructionImage.SetActive(false);
            exit = false;
        }
    }

    private void CheckController()
    {
        // Retrieve a list of all connected joystick names
        string[] connectedControllers = Input.GetJoystickNames();

        // Check if there are any connected controllers
        if (connectedControllers.Length > 0)
        {
            foreach (string controller in connectedControllers)
            {
                if (!string.IsNullOrEmpty(controller))
                {
                    playerController.enabled = false;
                    playerControllerActivated = false;
                    Debug.Log("Controller connected: " + controller);
                }
            }
        }
        else
        {
            playerControllerActivated = true;
            playerController.enabled = true;
            Debug.Log("No controller connected.");
        }
    }
}
