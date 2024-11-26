using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ARUI : MonoBehaviour
{
    
    [SerializeField] private Button backButton;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject textbox;
    [SerializeField] private  TextMeshProUGUI text;
    [SerializeField] private int textDelay = 4000;

    void Start()
    {
        backButton.onClick.AddListener(()=>
        {
            SceneManager.LoadSceneAsync(0);
        });
    }
    
    private void OnEnable()
    {
        InteractableObject.OnPlayAudio += PlayAudio;
        InteractableObject.OnSendMessage += SendMessage;
        
    }

    private void OnDisable()
    {
        InteractableObject.OnPlayAudio -= PlayAudio;
        InteractableObject.OnSendMessage -= SendMessage;
    }

    private void PlayAudio(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    private new async void SendMessage(string message)
    {
        text.text = message;
        textbox.gameObject.SetActive(true);
        await Task.Delay(textDelay);
        textbox.gameObject.SetActive(false);
    }
}
