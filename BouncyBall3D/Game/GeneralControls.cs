using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum GameState { playing, gameover, paused };

public class GeneralControls : MonoBehaviour
{
    //Public Variables
    public Transform player; //Position of the player in the World
    public Transform platformPrefab; //Prefab of the platforms
    public GUIText ScoreGUI; //GUI Text variable to display the Score
    public AudioSource deathSound; //Audio Source for death animation
    public GameObject deathParticlePrefab; //Explotion's prefab used when player dies
    public static GameState gameState; //Variable to store the game's state at a given time
    //public TextMesh scoreNumber; //Score shown in the score board
    public GameObject fadeInMaterial; //Black plane that will fade when the game starts
    public GameObject skeletonPrefab; //Skeleton 3D model prefab
    //public GameObject flowerPrefab;
    //public GameObject projectile;
    [HideInInspector]
    public float cameraSpeedMultiplier = 1; //Controls camera's Translate speed
    public bool lookTarget = true; //Controls if the camera looks at the ball
    public int enemyKillScore;

    //Private Variables
    private List<Transform> platforms; //List of platforms
    private float nextPlatform = 10.0f; //The first platform will appear 10 units above the ground
    private float nextPlatformCheck = 0.0f; //Variable to check the player's height in order to create platforms
    private float cameraSpeed; //Determines the speed at wich the camera moves upwards
    private int currentScore; //Player's score in the game
    private bool playDeathParticles = true; //Bool variable to spawn the death particles only once
    private int coinsGained;
    private float faddeInTimer = 1; //Time the black material takes to fade when the game starts

    /// <summary>
    /// Initialization
    /// </summary>
    void Start() 
    {
        //Sets the platforms list to a new list of transform variables
        platforms = new List<Transform>();
        //Current Game State: Playing
        gameState = GameState.playing;
        //Camera's speed is 0 at the start of the game
        cameraSpeed = 1;
        PlayerPrefs.SetInt("EnemyKillScore", 0);
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        FadeInAnimation();

        //Camera's speed increases as time goes by
        cameraSpeed += Time.deltaTime / 20;

        //New vector 3 that makes the camera go up
        Vector3 cameraPosition = new Vector3(0, cameraSpeed * Time.deltaTime * cameraSpeedMultiplier, 0);

        Vector3 lookAtPosition = player.position;
        lookAtPosition.y = transform.position.y;
        transform.LookAt(lookAtPosition);

        //Move Camera in the Y axis
        transform.position += cameraPosition;


        //Add Laser maybe?
        //if (Input.GetMouseButtonUp(0))
        //{
        //    Vector3 laserRot = new Vector3(90, 0, Input.mousePosition.x);
        //    GameObject.Instantiate(projectile, player.transform.position, Quaternion.Euler(laserRot));
        //}

        for (int i = 0; i < platforms.Count; i++)
        {
            if (player.rigidbody.velocity.y > 0)
                platforms[i].collider.enabled = false;
            else
                platforms[i].collider.enabled = true;
        }

        //Do we need to spawn new platforms yet? (we do this every X meters we climb)
        float playerHeight = player.position.y;
        if (playerHeight > nextPlatformCheck)
        {
            //Spawn new platforms
            PlatformMaintenaince();
        }

        //If the height of the player is less or equals to the Camera's height - 15 units...
        if (player.position.y <= transform.position.y - 20)
        {
            GameOver();
        }

        //Whenever the game state changes to false
        if (gameState == GameState.paused)
        {
            //Pause the game
            PauseGame();
        }
        //If it changes to playing
        else if (gameState == GameState.playing)
        {
            //Unpause the game
            UnPause();
        }
                
        //Update camera position if the player has climbed
        float newHeight = Mathf.Lerp(transform.position.y, playerHeight - 5, Time.deltaTime * 10);
        if (player.position.y - 5 > transform.position.y)
        {
            //New camera position when the player is above it
            transform.position = new Vector3(transform.position.x, newHeight, transform.position.z);
            
        }
        //Divides the player's position in half and stores it as the current score. Also, add the score stored in Playerprefs for enemy kills
        currentScore = (int)(transform.position.y * 0.5f) + PlayerPrefs.GetInt("EnemyKillScore");

        //Set the GUI text to the current score
        ScoreGUI.text = "" + currentScore;
    }

    /// <summary>
    /// Checks if the player has reached a new highscore
    /// </summary>
    void SaveHighScore()
    {
        //Check if the current score is greater than the first high score
        if (currentScore > PlayerPrefs.GetInt("HighScore1"))
        {
            //Store the current first score in the player prefs as HighScore1
            PlayerPrefs.SetInt("HighScore1", currentScore);
        }
        //If the current score is greater than the third highscore, lower than the first one and greater than
        //the curent second one
        else if (currentScore > PlayerPrefs.GetInt("HighScore3") &&
                currentScore > PlayerPrefs.GetInt("HighScore2") &&
                currentScore < PlayerPrefs.GetInt("HighScore1"))
        {
            //Store the current second high score as HighScore2
            PlayerPrefs.SetInt("HighScore2", currentScore);
        }
        //If the current score is greater than the current third high score but lower than the second one
        else if (currentScore > PlayerPrefs.GetInt("HighScore3") &&
                currentScore < PlayerPrefs.GetInt("HighScore2"))
        {
            //Store the curent third high score as HighScore3
            PlayerPrefs.SetInt("HighScore3", currentScore);
        }
    }

    /// <summary>
    /// First call: player's height greater than 0
    /// </summary>
    void PlatformMaintenaince() 
    {
        //Distance untill next spawn
        nextPlatformCheck += 50;
        //nextPlatformCheck += Random.Range(30, 100);

        //Destroy platforms below the camera
        for (int i = platforms.Count - 1; i >= 0; i--)
        {
            Transform plat = platforms[i];
            if (plat.position.y < transform.position.y - 30)
            {
                Destroy(plat.gameObject);
                platforms.RemoveAt(i);
            }
        }

        //Spawn new platforms, 25 units in advance
        SpawnStuff(nextPlatformCheck + 25);
    }

    void SpawnStuff(float distance)
    {
        float spawnHeight = nextPlatform; //nextPlatform: 10.0f. distance: 100
        while (spawnHeight <= distance) //10 <= 50
        {
            //Random X coordinates between -15 and 15
            float x = Random.Range(-15.0f, 15.0f);
            int n = Random.Range(1, 10);
            Vector3 pos = new Vector3(x, spawnHeight, 0);

            Transform plat = (Transform)Instantiate(platformPrefab, pos, Quaternion.identity);
            platforms.Add(plat);


            //Enemy Skeleton
            if (n == 1)
            {
                Vector3 enemyPos = new Vector3(x, spawnHeight + 2.95896f, 0);
                GameObject skeleton = (GameObject)Instantiate(skeletonPrefab, enemyPos, Quaternion.identity);
                int orientation = Random.Range(1, 3);

                if (orientation == 1)
                    skeleton.transform.rotation = Quaternion.Euler(0, 90, 0);
                else if (orientation == 2)
                    skeleton.transform.rotation = Quaternion.Euler(0, -90, 0);
            }


            //if (n == 2)
            //{
            //    Vector3 enemyPos = new Vector3(x, spawnHeight + 0.97753f, 0);
            //    GameObject plant = (GameObject)Instantiate(flowerPrefab, enemyPos, Quaternion.Euler(0, -90, 0));
            //}
            

            //Next platform spawns at x units from the last one
            spawnHeight += Random.Range(5.0f, 6.5f);
        }
        //nextPlatform = distance + Random.Range(4.6f, 5.5f);
        nextPlatform = distance + 5;
    }
    

    /// <summary>
    /// Ends the current Game
    /// </summary>
    void GameOver()
    {
        //Current Game State: Game Over
        gameState = GameState.gameover;
        //Check for score and if it's a highscore save it
        SaveHighScore();
        //Stop the camera by changing the it's speed to 0
        cameraSpeed = 0;
        lookTarget = false;

        PlayerPrefs.SetInt("LastScore", currentScore);

        //The bool "playDeathParticles" is set to true by default
        if (playDeathParticles == true)
        {
            //Instantiates the particles at players position with a -90 degree rotation in X axis
            Instantiate(deathParticlePrefab, player.position, Quaternion.Euler(new Vector3(-90,0,0)));
            //Play death sound effect
            deathSound.Play();

            Vector3 death = new Vector3(transform.position.z, transform.position.y, transform.position.z);
            transform.LookAt(death);
            
            coinsGained = currentScore / 2;
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + coinsGained);

            //Sets the bool to false so that particles only spawn once
            playDeathParticles = false;
        }
    }

    /// <summary>
    /// Pauses the Game without using TimeScale
    /// </summary>
    void PauseGame() 
    {
        //Freezes the characters position
        player.rigidbody.constraints = RigidbodyConstraints.FreezePosition;
        //Multiplies the camera's speed by 0 to prevent itfrom moving
        cameraSpeedMultiplier = 0;
        fadeInMaterial.renderer.material.color = new Color(0, 0, 0, 0.5f);
    }

    /// <summary>
    /// Unpauses the game
    /// </summary>
    void UnPause() 
    {
        //Unfreezes the player's position
        player.rigidbody.constraints = RigidbodyConstraints.None;
        //Multiplies the camera's speed by 1 to get to normal speed
        cameraSpeedMultiplier = 1;
        fadeInMaterial.renderer.material.color = new Color(0, 0, 0, 0);
    }

    /// <summary>
    /// Animation of a plane changing it's transparency whenever the level is loaded
    /// </summary>
    void FadeInAnimation() 
    {
        if (faddeInTimer > 0)
        {
            faddeInTimer -= Time.deltaTime;
            fadeInMaterial.renderer.material.color = new Color(0, 0, 0, faddeInTimer);
        }
        if (faddeInTimer < 0)
        {
            faddeInTimer = 0;
        }
    }
}