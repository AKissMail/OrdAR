using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    
    [SerializeField] private PrefabData data;
    
    public delegate void AudioEventHandler(AudioClip clip);
    public static event AudioEventHandler OnPlayAudio;

    public delegate void MessageEventHandler(string message);
    public static event MessageEventHandler OnSendMessage;

    private void OnMouseDown() // Wird beim Anklicken ausgelöst
    {
        if (data != null)
        {
            // Events auslösen mit den Daten aus PrefabData
            OnPlayAudio?.Invoke(data.PronunciationAudio);
            OnSendMessage?.Invoke($"{data.NorwegianWord} \n ({data.EnglishWord})");
        }
    }
}
