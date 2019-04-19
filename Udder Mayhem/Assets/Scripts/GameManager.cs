using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    // Time Elements
    public float time;
    public float maxTime = 15;

    // Game Elements
    public int score = 0;
    public GameObject player;
    public GameObject[] enemies;
    public bool gameOver = false;
    public bool playerDead = false;

    // UI Elements
    public Text enemiesRemainingText;
    public Text timeRemainingText;
    public Text scoreText;
    public Text gameOverText;

    void Awake() {
        // Check Singleton setup
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}

	void Update () {
        time += Time.deltaTime;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        UpdateGameConditions();
        UpdateUI();
	}

    private void UpdateGameConditions() {
        if (!player) {
            gameOver = true;
            playerDead = true;
        } else if (time > maxTime && enemies.Length == 0) { // Player not dead and game won
            gameOver = true;
        }

        // Update Scores
        if (gameOver) {
            if(score > PlayerPrefs.GetInt("HighScore", 0)) {
                PlayerPrefs.SetInt("HighScore", score);
            }
        }
    }

    private void UpdateUI() {
        enemiesRemainingText.text = "Enemies Remaining: " + enemies.Length;

        if(time < maxTime) {
            timeRemainingText.text = "Time Remaining: " + (int)(maxTime - time);
        }
           
        scoreText.text = "Score: " + score;
        if (gameOver && playerDead) {
            gameOverText.text = "Game Over";
        } else if (gameOver) {
            gameOverText.text = "Mission Complete";
        }
    }
}
