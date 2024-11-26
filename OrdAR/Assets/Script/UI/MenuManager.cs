using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Screen")]
    [SerializeField] private GameObject splashScreen;
    [SerializeField] private GameObject helpScreen;
    [SerializeField] private GameObject loadScreen1;
    [SerializeField] private GameObject loadScreen2;
    [SerializeField] private GameObject loadScreen3;
    [SerializeField] private GameObject loadScreen4;
    [SerializeField] private GameObject loadScreen5;
    [SerializeField] private GameObject loadIcon;
    [Header("Buttons main menu")]
    [SerializeField] private Button lesson1Button;
    [SerializeField] private Button lesson2Button;
    [SerializeField] private Button lesson3Button;
    [SerializeField] private Button lesson4Button;
    [SerializeField] private Button lesson5Button;
    [SerializeField] private Button helpButton;
    
    [Header("Buttons lesson 1 info")]
    [SerializeField] private Button lesson1StartButton;
    [SerializeField] private Button lesson1BackButton;
    
    [Header("Buttons lesson 2 info")]
    [SerializeField] private Button lesson2StartButton;
    [SerializeField] private Button lesson2BackButton;
    
    [Header("Buttons lesson 3 info")]
    [SerializeField] private Button lesson3StartButton;
    [SerializeField] private Button lesson3BackButton;
    
    [Header("Buttons lesson 4 info")]
    [SerializeField] private Button lesson4StartButton;
    [SerializeField] private Button lesson4BackButton;
    
    [Header("Buttons lesson 5 info")]
    [SerializeField] private Button lesson5StartButton;
    [SerializeField] private Button lesson5BackButton;
    
    [Header("Buttons help")]
    [SerializeField] private Button helpBackButton;
    
    
    
    void Start()
    {
        
      lesson1StartButton.onClick.AddListener(()=> LoadSceneAsync(1));
      lesson2StartButton.onClick.AddListener(()=> LoadSceneAsync(2));
      lesson3StartButton.onClick.AddListener(()=> LoadSceneAsync(3));
      lesson4StartButton.onClick.AddListener(()=> LoadSceneAsync(4));
      lesson5StartButton.onClick.AddListener(()=> LoadSceneAsync(5));
      
      lesson1Button.onClick.AddListener(()=>loadScreen1.gameObject.SetActive(true));
      lesson2Button.onClick.AddListener(()=>loadScreen2.gameObject.SetActive(true));
      lesson3Button.onClick.AddListener(()=>loadScreen3.gameObject.SetActive(true));
      lesson4Button.onClick.AddListener(()=>loadScreen4.gameObject.SetActive(true));
      lesson5Button.onClick.AddListener(()=>loadScreen5.gameObject.SetActive(true));
      
      lesson1BackButton.onClick.AddListener(()=>loadScreen1.gameObject.SetActive(false));
      lesson2BackButton.onClick.AddListener(()=>loadScreen2.gameObject.SetActive(false));
      lesson3BackButton.onClick.AddListener(()=>loadScreen3.gameObject.SetActive(false));
      lesson4BackButton.onClick.AddListener(()=>loadScreen4.gameObject.SetActive(false));
      lesson5BackButton.onClick.AddListener(()=>loadScreen5.gameObject.SetActive(false));
      
      helpButton.onClick.AddListener(()=>helpScreen.gameObject.SetActive(true));
      helpBackButton.onClick.AddListener(()=>helpScreen.gameObject.SetActive(false));
      
    }

    private void Awake()
    {
        loadIcon.GameObject().SetActive(false);
    }


    private void LoadSceneAsync(int sceneIndex)
    {
        loadIcon.GameObject().SetActive(true);
        SceneManager.LoadSceneAsync(sceneIndex);
    }
}
