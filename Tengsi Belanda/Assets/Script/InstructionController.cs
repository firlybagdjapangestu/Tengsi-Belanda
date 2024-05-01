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

    // Start is called before the first frame update
    void Start()
    {
        instructionImage.SetActive(false); // Pastikan gambar instruksi tidak aktif saat memulai
    }

    // Update is called once per frame
    void Update()
    {
        // Memeriksa jika Fire1 ditekan
        if (Input.GetButtonDown("Fire1"))
        {
            // Memulai AudioClip
            if (audioSource != null && audioNow != null)
            {
                audioSource.clip = audioNow;
                audioSource.Play();
            }
            if (exit)
            {
                Debug.Log("Keluar");
                Application.Quit(); // Panggil method Quit() untuk keluar dari aplikasi
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IntructionData intructionData = other.GetComponent<IntructionData>();
        if (other.tag == "Instruction") // Memeriksa tag dari collider yang bersentuhan
        {
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
        if (other.tag == "Instruction") // Memeriksa tag dari collider yang bersentuhan
        {
            instructionImage.SetActive(false);
            audioNow = null;
            audioSource.Stop();        
        }
        if (other.tag == "Exit") // Memeriksa tag dari collider yang bersentuhan
        {
            instructionImage.SetActive(false);
            exit = false;
        }
    }
}
