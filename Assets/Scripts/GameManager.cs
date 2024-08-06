using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // ENCAPSULATION
    private static GameManager instance;

    public static GameManager Instance
    {
        get => instance;
        private set
        {
            instance = value;
        }
    }
    [SerializeField] private List<GameObject> prefab;
    
     public TextMeshProUGUI scoreText;
     public TextMeshProUGUI liveText;
     public TextMeshProUGUI levelText;
    private float timeStart = 1;
    private float timeDelay = 1;
    private int score ;
    private int level = 0;
    private int lostFood = 0;
    private int live = 8;
    private int maxLostOfFood = 3;
    [SerializeField] private List<int> levelThresholds; // Danh sách ngưỡng điểm cho mỗi cấp độ
    private float spawnDelayDecrese = 0.2f;
    //private UIManager _uIManager;
    private float xRange = 7;
    private float y = 6;
    private float xBound = 8;
    
    public bool gameOver = false;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // ABSTRACTION
        UpdateScore(0);
        UpdateLevel(0);
        LostFood(0);
       // _uIManager = GameObject.FindObjectOfType<UIManager>();
        InvokeRepeating("SpawnObject", timeStart, timeDelay);
        
    }

    // Update is called once per frame
    void Update()
    {
        BoundOfPlayer();
    }
    void SpawnObject()
    {
        if(!gameOver && UIManager.Instance.isStart)
        {
            
            int index = Random.Range(0, prefab.Count);
            Vector3 spawnPos = new Vector3(Random.Range(-xRange, xRange), y);
            Instantiate(prefab[index], spawnPos, Quaternion.identity);
            
        }
        
    }
    void BoundOfPlayer()
    {
        if (PlayerController.Instance.transform.position.x < -xBound )
        {
            PlayerController.Instance.transform.position = new Vector3(-xBound, PlayerController.Instance.transform.position.y) ;
        }else
        if (PlayerController.Instance.transform.position.x > xBound )
        {
            PlayerController.Instance.transform.position = new Vector3(xBound, PlayerController.Instance.transform.position.y);
        }           
    }
     //
    public void GameOver()
    {
        gameOver = true;
        liveText.gameObject.SetActive(false);
        //_uIManager.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(false);
        UIManager.Instance.restartButton.gameObject.SetActive(true);
    }
// health player
    
    public void LostFood(int amountOfFood)
    {
        if(lostFood >= live ) GameOver();
        liveText.text = "Lives: " + (live - lostFood);
        lostFood += amountOfFood;
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
        CheckLevel();
    }
    private void CheckLevel()
    {
        if (score >= levelThresholds[level] && level < levelThresholds.Count)
        {
            UpdateLevel(1);
            IncreseDifficulty();
        }
    }
    private void IncreseDifficulty()
    {
        timeDelay = Mathf.Max(0.1f, timeDelay, spawnDelayDecrese);
        CancelInvoke("SpawnObject");
        InvokeRepeating("SpawnObject", timeStart, timeDelay );
    }
    private void UpdateLevel(int levelToAdd)
    {
        level += levelToAdd;
        levelText.text = "Level: " + level;
    }
}
