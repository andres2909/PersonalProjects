using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


//State of the game
public enum GameState { Playing, Pause, End } 

public class GameManager : MonoBehaviour
{
    #region Global Variables

    // Public Static Variables
    public static int score;                // Player's score
    public static int lives = 3;            // Players lives
    public static bool gameOn = false;      // Controls when the ball should be moving
    public static GameState gameState;      // State of the game

    // Private Variables
    private Text scoreText;                 // Score's UI text
    private Image[] livesImgs;              // Hearts representing lives

    #endregion

    #region Unity Functions

    /// <summary>
    /// Awake Function
    /// </summary>
    void Awake()
    {
        // Get the score UI text
        scoreText = GameObject.FindWithTag("Score").GetComponent<Text>();
        
        // Prepare for a new game
        score = 0;
        lives = 3;
        gameOn = false;
        gameState = GameState.Playing;

        // Get all the UI Images and store them in an array
        livesImgs = new Image[3];
        int imgIndex = 0;
        foreach (var image in GameObject.FindGameObjectsWithTag("Life"))
        {
            livesImgs[imgIndex] = image.GetComponent<Image>();
            imgIndex++;
        }
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // Display the current score on screen
        scoreText.text = score.ToString();

        // When the game is playing...
        if (gameState == GameState.Playing)
        {
            // Check the value of lives
            switch (lives)
            {
                // 2 Lives
                case 2:
                    livesImgs[2].color = Color.black;
                    break;

                // 1 Life
                case 1:
                    livesImgs[0].color = Color.black;
                    livesImgs[2].color = Color.black;
                    break;

                // 0 Lives
                case 0:
                    livesImgs[0].color = Color.black;
                    livesImgs[1].color = Color.black;
                    livesImgs[2].color = Color.black;
                    gameOn = false;
                    gameState = GameState.End;
                    break;
            }
        }
    }

    #endregion

    #region Other Functions

    /// <summary>
    /// Pause/Unpause the game
    /// </summary>
    public void PauseGame()
    {
        Time.timeScale = Time.timeScale == 1 ? 0 : 1;
    }

    #endregion



}
