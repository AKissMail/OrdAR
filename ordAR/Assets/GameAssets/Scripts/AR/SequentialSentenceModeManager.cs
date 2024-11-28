using UnityEngine;
using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;

public class SequentialSentenceModeManager : MonoBehaviour
{
    [System.Serializable]
    public class SentenceSequence
    {
        public List<string> markerSequence; 
        public GameObject prefab;
    }
    [SerializeField] private ARTrackedImageManager trackedImageManager;
    [SerializeField] private List<SentenceSequence> sentence = new();
    
    public delegate void ScanProgressEventHandler(int currentStep, int totalSteps);
    public static event ScanProgressEventHandler OnScanProgress;
    public delegate void CombinationSuccessEventHandler(string message);
    public static event CombinationSuccessEventHandler OnCombinationSuccess;
    
    private HashSet<string> _processedMarkers = new();
    private Dictionary<string, GameObject> _spawnedObjects = new();
    private SentenceSequence _currentSequence;
    private List<string> _maker;
    private GameObject _spawnedPrefab;

    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            HandleTrackedImage(trackedImage);
        }
    }

private void HandleTrackedImage(ARTrackedImage trackedImage)
{
    string markerID = trackedImage.referenceImage.name;
    if (_currentSequence == null)
    {
        _currentSequence = FindSequenceStartingWith(markerID);
        _maker = _currentSequence.markerSequence;
        
        if (_currentSequence != null)
        {
            _processedMarkers.Add(markerID);
            OnCombinationSuccess?.Invoke("First word correct!");
            _maker.RemoveAt(0);
            return;
        }
    }
    if (_processedMarkers.Contains(markerID))
    {
        OnCombinationSuccess?.Invoke("We had already that word!");
        return;
    }
    
    if (_maker[0] == markerID)
    {
        _maker.RemoveAt(0);
        OnCombinationSuccess?.Invoke(_maker.Count.ToString());
        _processedMarkers.Add(markerID);
        OnCombinationSuccess?.Invoke("Correct!");
        if (_maker.Count != 0) return;
        if (_spawnedObjects.ContainsKey(string.Join("-", _currentSequence.markerSequence))) return;
        SpawnPrefab(_currentSequence, trackedImage);
        OnCombinationSuccess?.Invoke("Well done!");
    }
    else
    {
        OnCombinationSuccess?.Invoke("Oh no, that is not correct. What about a other word?");
    }
}

    private SentenceSequence FindSequenceStartingWith(string markerID)
    {
        foreach (var sequence in sentence)
        {
            if (sequence.markerSequence.Count > 0 && sequence.markerSequence[0] == markerID)
            {
                return sequence;
            }
        }
        return null;
    }
    
    private void SpawnPrefab(SentenceSequence sequence, ARTrackedImage trackedImage)
    {
        GameObject spawned = Instantiate(sequence.prefab, trackedImage.transform);
        spawned.transform.localPosition = Vector3.zero;
        spawned.transform.localRotation = Quaternion.identity;
        _spawnedObjects[string.Join("-", sequence.markerSequence)] = spawned;
        OnCombinationSuccess?.Invoke("Spawned prefab: {sequence.prefab.name}");
    }
}
