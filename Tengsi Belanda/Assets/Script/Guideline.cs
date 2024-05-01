using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guideline : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClip;
    public int repeatCount = 3; // Jumlah ulangan audio clip
    public float delayBetweenRepeats = 1f; // Waktu jeda antara ulangan (dalam detik)
    public float initialDelay = 2f; // Jeda awal sebelum pemutaran pertama (dalam detik)

    private int currentRepeat = 0;

    void Start()
    {
        if (audioSource == null)
        {
            // Jika AudioSource tidak ditetapkan di Inspector, gunakan AudioSource dari GameObject ini
            audioSource = GetComponent<AudioSource>();
        }

        if (audioSource == null || audioClip == null)
        {
            Debug.LogError("AudioSource atau AudioClip tidak ditetapkan di AudioClipRepeater.");
            enabled = false; // Matikan script jika tidak ada AudioSource atau AudioClip
        }
        else
        {
            // Mulai ulang audio clip setelah jeda awal
            Invoke("StartRepeating", initialDelay);
        }
    }

    void StartRepeating()
    {
        // Mainkan AudioClip
        audioSource.PlayOneShot(audioClip);

        // Tambah jumlah ulangan
        currentRepeat++;

        // Cek apakah sudah mencapai jumlah ulangan maksimum
        if (currentRepeat < repeatCount)
        {
            // Jeda sebelum mengulangi
            Invoke("RepeatAudioClip", delayBetweenRepeats);
        }
    }

    void RepeatAudioClip()
    {
        // Mainkan AudioClip
        audioSource.PlayOneShot(audioClip);

        // Tambah jumlah ulangan
        currentRepeat++;

        // Cek apakah sudah mencapai jumlah ulangan maksimum
        if (currentRepeat < repeatCount)
        {
            // Jeda sebelum mengulangi
            Invoke("RepeatAudioClip", delayBetweenRepeats);
        }
    }
}
