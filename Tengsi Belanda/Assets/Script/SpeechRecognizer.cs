using UnityEngine;
using UnityEngine.UI;
using static SpeechRecognizerPlugin; // gunakan library plugin nya

public class SpeechRecognizer : MonoBehaviour, ISpeechRecognizerPlugin
{
    private SpeechRecognizerPlugin plugin = null; // deklarasi variabel nya
    [SerializeField] private Text yourCommand;
    [SerializeField] private Text acceptCommand;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private string[] everyCommand;
    // Start is called before the first frame update
    void Start()
    {
        plugin = SpeechRecognizerPlugin.GetPlatformPluginVersion(this.gameObject.name); // dapatin nama gameobject yang ada isi plugin
        plugin.SetMaxResultsForNextRecognition(1); // untuk mengisi berapa jumlah max hasil 
        plugin.SetContinuousListening(true); // untuk membuat plugin terus menerus mendengarkan
        plugin.SetLanguageForNextRecognition("en-US"); // untuk mengatur bahasa yang di dengar
        Invoke("StartListening", 3); // untuk mengaktifkan listening
    }
    public void StartListening()
    {
        plugin.StartListening();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnResult(string recognizedResult)
    {
        char[] delimiterChars = { '~' };
        string[] results = recognizedResult.Split(delimiterChars); // hasil pendengaran

        yourCommand.text = results[0];
        if (results[0] == "go")
        {
            playerController.ms = 10;
            acceptCommand.text = "Succesfull";
        }
        else if (results[0] == "stop")
        {
            playerController.ms = 0;
            acceptCommand.text = "Succesfull";
        }
        else if (results[0] == "slow down")
        {
            playerController.ms += 2;
            acceptCommand.text = "Succesfull";
        }
        else if (results[0] == "speed up")
        {
            playerController.ms -= 2;
            acceptCommand.text = "Succesfull";
        }
        else
        {
            acceptCommand.text = "Error";
        }
        // kalian bisa melakukan apapun dengan hasil pendengaran
    }

    public void OnError(string recognizedError)
    {
        ERROR error = (ERROR)int.Parse(recognizedError); // jika terjadi error
        switch (error)
        {
            case ERROR.UNKNOWN:
                Debug.Log("<b>ERROR: </b> Unknown");
                break;
            case ERROR.INVALID_LANGUAGE_FORMAT:
                Debug.Log("<b>ERROR: </b> Language format is not valid");
                break;
            default:
                break;
        }
    }
}
