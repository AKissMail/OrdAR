using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ARUI : MonoBehaviour
{
    
    [Header("UI-Component")]
    [SerializeField] private Button backButton;
    [SerializeField] private GameObject textbox;
    [SerializeField] private  TextMeshProUGUI text;
    [SerializeField] private Button playButton; 
    [SerializeField] private GameObject playButtonGameObject; 
    
    [Header("Audio-Component")]
    [SerializeField] private AudioSource audioSource;
    [Header("Settings")]
    [SerializeField] private int textDisplayDuration = 4000;

    private AudioClip _transcriptAudio;
    private string _transcript; 

    void Start()
    {
        backButton.onClick.AddListener(() =>
        {
            SceneManager.LoadSceneAsync(0);
        });

        playButton.onClick.AddListener(Play); 
        textbox.SetActive(false);
        playButtonGameObject.SetActive(false);
    }
    
    private void OnEnable()
    {
        InteractableObject.OnPlayAudio += SaveAudio;
        InteractableObject.OnSendMessage += SaveMessage;
        SequentialArticleModeManager.OnCombinationSuccess += UpdateText;
        SequentialSentenceModeManager.OnScanProgress += UpdateCount;
        SequentialSentenceModeManager.OnCombinationSuccess += UpdateText;
        
    }
    
    private void OnDisable()
    {
        InteractableObject.OnPlayAudio -= SaveAudio;
        InteractableObject.OnSendMessage -= SaveMessage;
        SequentialArticleModeManager.OnCombinationSuccess -= UpdateText;
        SequentialSentenceModeManager.OnScanProgress -= UpdateCount;
        SequentialSentenceModeManager.OnCombinationSuccess -= UpdateText;
    }

    private void SaveAudio(AudioClip clip)
    {
        if (clip != null)
        {
            _transcriptAudio = clip;
            CheckIfReadyToPlay();
        }
    }

    private void SaveMessage(string message)
    {
        if (!string.IsNullOrEmpty(message))
        {
            _transcript = message;
            CheckIfReadyToPlay();
        }
        else
        {
            Debug.LogWarning("Empty massage in ARUI!");
        }
    }

    private void CheckIfReadyToPlay()
    {
        if (_transcriptAudio != null && _transcript != null)
        {
            playButtonGameObject.SetActive(true);
        }
    }

    private async void Play()
    {
        try
        {
            audioSource.clip = _transcriptAudio;
            audioSource.Play();

            text.text = _transcript;
            textbox.SetActive(true);
            await Task.Delay(textDisplayDuration);
            textbox.SetActive(false);
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }
    
    private async void UpdateCount(int currentStep, int totalSteps)
    {
        try
        {
            currentStep = currentStep == 0 ? 2 : 1; 
            text.text = $"Progress: {currentStep}/{totalSteps}";
            textbox.SetActive(true);
            await Task.Delay(textDisplayDuration);
            textbox.SetActive(false);
        }
        catch (Exception e)
        {
            Debug.Log(e); 
        }
    }

    private async void UpdateText(string message)
    {
        try
        {text.text = message;
            textbox.SetActive(true);
            await Task.Delay(textDisplayDuration);
            textbox.SetActive(false);
        }
        catch (Exception e)
        {
            Debug.Log(e); 
        }
    }
}
