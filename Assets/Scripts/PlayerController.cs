using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    [SerializeField] private float speed = 5;

    private float horizontalInput;
    private AudioSource _audioSource;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.gameOver && UIManager.Instance.isStart)
        {
            // ABSTRACTION
            movePlayer();
        }
        
    }

    private void movePlayer()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(speed * horizontalInput * Time.deltaTime * Vector2.right);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            Target food = other.gameObject.GetComponent<Target>();
            GameManager.Instance.UpdateScore(food.point);
            Destroy(other.gameObject);
            _audioSource.Play();
        }
        else if (other.gameObject.CompareTag("Bad"))
        {
            Debug.Log("Game over");
            GameManager.Instance.GameOver();
            
        }
    }
}
