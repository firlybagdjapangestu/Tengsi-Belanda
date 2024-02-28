using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class InteractionWithUi : MonoBehaviour
{
    [SerializeField] private float timeToSelect;
    [SerializeField] private float timer;
    [SerializeField] private bool lookAt;
    [SerializeField] private GameObject buttonPlay;

    void Update()
    {
        if (lookAt)
        {
            timer += Time.deltaTime; // menghitung waktu
            if(timer >= timeToSelect) // membandingkan dengan batas waktu memilih
            {
                Debug.Log("Select"); // akan melaksanakan pointer down 
                ExecuteEvents.Execute(buttonPlay, new PointerEventData(EventSystem.current), ExecuteEvents.pointerDownHandler);
                timer = 0;
            }
        }
        
    }

    public void PointerEnter() // mendeteksi pointer lagi berada di ui(Button)
    {
        lookAt = true;
        Debug.Log("Masuk");
    }
    public void PointerExit() // Mendeteksi Pointer lagi tidak berada di ui(Button)
    {
        lookAt = false;
        Debug.Log("Keluar");
    }
    public void LoadScane(int i)// untuk meload scene
    {
        SceneManager.LoadScene(i);
    }
}
