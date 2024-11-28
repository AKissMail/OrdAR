using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.XR.ARFoundation;

public class SequentialArticleModeManager : MonoBehaviour
{
    

    [System.Serializable]
    public class ArticleNounPair
    {
        public string articleMarkerID;
        public string nounMarkerID;
        public GameObject prefab;
    }
    [SerializeField] private ARTrackedImageManager trackedImageManager;
    [SerializeField] private List<ArticleNounPair> validPairs = new();
    
    public delegate void CombinationSuccessEventHandler(string message);
    public static event CombinationSuccessEventHandler OnCombinationSuccess;

    private Dictionary<string, GameObject> _spawnedObjects = new();
    private string _currentArticleMarker; 
    private string _currentNounMarker;    
    private GameObject _spawnedPrefab;
    private int _currentStep;

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

        foreach (var trackedImage in eventArgs.updated)
        {
            HandleTrackedImage(trackedImage);
        }
    }

    private void HandleTrackedImage(ARTrackedImage trackedImage)
    {
        string markerID = trackedImage.referenceImage.name;
        OnCombinationSuccess?.Invoke(trackedImage.referenceImage.name);
        
        switch (_currentStep)
        {
            case 0 when IsArticleMarker(markerID):
                _currentArticleMarker = markerID;
                _currentStep++;
                OnCombinationSuccess?.Invoke("Recognize");
                break;
            case 1 when IsNounMarker(markerID):
                _currentNounMarker = markerID;
                _currentStep++;
                OnCombinationSuccess?.Invoke("Recognize");
                CheckCombination(trackedImage);
                break;
        }
    }

    private void CheckCombination(ARTrackedImage trackedImage)
    {
        foreach (var pair in validPairs.Where(pair => pair.articleMarkerID == _currentArticleMarker && pair.nounMarkerID == _currentNounMarker))
        {
            SpawnPrefab(pair, trackedImage);
            OnCombinationSuccess?.Invoke($"Success: {pair.articleMarkerID} + {pair.nounMarkerID}!");
            ResetScan();
            return;
        }
        OnCombinationSuccess?.Invoke("Invalid combination!");
        ResetScan();
    }

    private void SpawnPrefab(ArticleNounPair pair, ARTrackedImage trackedImage)
    {
        GameObject spawned = Instantiate(pair.prefab, trackedImage.transform);
        spawned.transform.localPosition = Vector3.zero;
        spawned.transform.localRotation = Quaternion.identity;
        _spawnedObjects[pair.nounMarkerID] = spawned;
    }
    

    private void ResetScan()
    {
        _currentArticleMarker = null;
        _currentNounMarker = null;
        _currentStep = 0;
        if (!_spawnedPrefab) return;
        Destroy(_spawnedPrefab);
        _spawnedPrefab = null;
    }

    private bool IsArticleMarker(string markerID)
    {
        return validPairs.Any(pair => pair.articleMarkerID == markerID);
    }

    private bool IsNounMarker(string markerID)
    {
        return validPairs.Any(pair => pair.nounMarkerID == markerID);
    }
}
