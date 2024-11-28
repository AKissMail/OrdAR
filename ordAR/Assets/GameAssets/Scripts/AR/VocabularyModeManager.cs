using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class VocabularyModeManager : MonoBehaviour
{
    

    [System.Serializable]
    public class VocabularyEntry
    {
        public string markerID;
        public GameObject prefab;
    }
    [SerializeField] private ARTrackedImageManager trackedImageManager;
    [SerializeField] private List<VocabularyEntry> vocabularyEntries = new();
    
    private Dictionary<string, GameObject> spawnedObjects = new();
    
    public delegate void CombinationSuccessEventHandler(string message);
    public static event CombinationSuccessEventHandler OnCombinationSuccess;

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
        foreach (var added in eventArgs.added)
        {
            HandleTrackedImage(added);
        }

        foreach (var updated in eventArgs.updated)
        {
            if (updated.trackingState == TrackingState.Tracking)
            {
                HandleTrackedImage(updated);
            }
            else
            {
                DespawnObject(updated.referenceImage.name);
            }
        }

        foreach (var removed in eventArgs.removed)
        {
            DespawnObject(removed.referenceImage.name);
        }
    }

    private void HandleTrackedImage(ARTrackedImage trackedImage)
    {
        OnCombinationSuccess?.Invoke(trackedImage.referenceImage.name);
        foreach (var entry in vocabularyEntries)
        {
            if (trackedImage.referenceImage.name == entry.markerID)
            {
                if (!spawnedObjects.ContainsKey(entry.markerID) || spawnedObjects[entry.markerID] == null)
                {
                    GameObject spawned = Instantiate(entry.prefab, trackedImage.transform);
                    spawned.transform.localPosition = Vector3.zero; 
                    spawned.transform.localRotation = Quaternion.identity;
                    spawnedObjects[entry.markerID] = spawned;
                }
            }
        }
    }

    private void DespawnObject(string markerID)
    {
        if (spawnedObjects.ContainsKey(markerID) && spawnedObjects[markerID] != null)
        {
            Destroy(spawnedObjects[markerID]);
            spawnedObjects.Remove(markerID);
        }
    }
}
