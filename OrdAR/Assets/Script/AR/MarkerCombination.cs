using System.Collections.Generic;
using UnityEngine;


namespace Script.AR
{
    [System.Serializable]
    public class MarkerCombination
    {
        [SerializeField] private List<string> markersID;
        [SerializeField] GameObject combinationPrefab;
    }
}