using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    public static UIManager Instance
    {
        get
        {
            return instance;
        }
        private set
        {
            instance = value;
        }
    }
    
    private AudioSource audioSource;
    public Button playButton;
    public Button audioButton;
    public GameObject logo;
    private bool _isMuted = false;
    public bool isStart = false;
    public Sprite audioOnSprite;
    public Sprite audioOffSprite;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;

    private void Awake()
    {
        if (instance != null)
        {
             Destroy( gameObject);
             return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        audioSource = Camera.main.GetComponent<AudioSource>();
        playButton.onClick.AddListener(StartGame);
        audioButton.onClick.AddListener(OnClickAudioButton);
        restartButton.onClick.AddListener(RestartGame);
        restartButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void  OnClickAudioButton()
    {
        _isMuted = !_isMuted;
        audioSource.volume = _isMuted ? 0 : 1;
        UpdateAudioButtonImage();
    }

    public void StartGame()
    { 
        playButton.gameObject.SetActive(false);
        audioButton.gameObject.SetActive(false);
        logo.gameObject.SetActive(false);
        isStart = true;
        GameManager.Instance.liveText.gameObject.SetActive(true);
        GameManager.Instance.scoreText.gameObject.SetActive(true);
    }

    void UpdateAudioButtonImage()
    {
        if (_isMuted == true)
        {
            audioButton.GetComponent<Image>().sprite = audioOffSprite;
        }
        else
        {
            audioButton.GetComponent<Image>().sprite = audioOnSprite;
        }
    }
    private void RestartGame()
    {
        //PlayerController.Instance = null;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
