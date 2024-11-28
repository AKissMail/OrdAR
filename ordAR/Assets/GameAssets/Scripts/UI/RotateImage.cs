using UnityEngine;

public class RotateImage : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 30f; 

    private RectTransform _rectTransform;

    private void Awake()
    {
       
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {

        _rectTransform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}