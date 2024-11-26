using UnityEngine;

public class RotateImage : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 30f; 

    private RectTransform rectTransform;

    private void Awake()
    {
       
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {

        rectTransform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}