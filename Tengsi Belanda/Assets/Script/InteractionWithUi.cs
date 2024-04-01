using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class InteractionWithUi : MonoBehaviour
{
    [SerializeField] private float timeToSelect;
    [SerializeField] private int sceneToLoad;
    [SerializeField] private int typePointerDown;
    private bool isPointerOver;

    private void Start()
    {
        isPointerOver = false;
    }

    private void Update()
    {
        if (isPointerOver)
        {
            timeToSelect -= Time.deltaTime;
            if (timeToSelect <= 0)
            {
                Debug.Log("Select");
                PointerDown(typePointerDown);
                timeToSelect = 0;
            }
        }
    }

    public void PointerEnter()
    {
        isPointerOver = true;
        Debug.Log("Masuk");
    }

    public void PointerExit()
    {
        isPointerOver = false;
        Debug.Log("Keluar");
    }

    public void PointerDown(int _typePointerDown)
    {
        if (_typePointerDown == 0)
        {
            LoadScene(sceneToLoad);
        }
        else if (_typePointerDown == 1)
        {
            QuitApplication();
            // Do something else
        }
    }

    public void SetSceneToLoad(int _selectScene)
    {
        sceneToLoad = _selectScene;
    }

    public void SetPointerType(int _typePointerDown)
    {
        typePointerDown = _typePointerDown;
    }

    private void LoadScene(int _sceneToLoad)
    {
        SceneManager.LoadScene(_sceneToLoad);
    }
    private void QuitApplication()
    {
        Debug.Log("Keluar dari aplikasi");
        Application.Quit(); // Panggil method Quit() untuk keluar dari aplikasi
    }
}
