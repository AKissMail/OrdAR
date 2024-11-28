using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    
    [SerializeField] private PrefabData data;
    
    public delegate void AudioEventHandler(AudioClip clip);
    public static event AudioEventHandler OnPlayAudio;

    public delegate void MessageEventHandler(string message);
    public static event MessageEventHandler OnSendMessage;

    private void Awake()
    {
        HandleInteraction(); 
    }
    
    private void HandleInteraction()
    {
        if (data)
        {
            OnPlayAudio?.Invoke(data.PronunciationAudio);
            OnSendMessage?.Invoke($"{data.NorwegianWord} \n ({data.EnglishWord})");
        }
    }
}
