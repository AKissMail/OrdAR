using UnityEngine;

[CreateAssetMenu(fileName = "PrefabData", menuName = "AR/PrefabData")]
public class PrefabData : ScriptableObject
{
    [SerializeField] private string markerID;
    [SerializeField] private string norwegianWord;
    [SerializeField] private string englishWord;
    [SerializeField] private AudioClip pronunciationAudio;

    public string MarkerID => markerID;
    public string NorwegianWord => norwegianWord;
    public string EnglishWord => englishWord;
    public AudioClip PronunciationAudio => pronunciationAudio;
}