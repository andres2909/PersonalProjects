using UnityEngine;
using System.Collections;

public class BtnAnim : MonoBehaviour 
{
    //Public Variables
    public AudioSource mouseOverSound; //Mouse Over button sound
    public AudioSource clickSound; //Click Sound
    public Material normalMaterial; //Red button material
    public Material hoverMaterial; //Yellow button material applied when mouseover
    public GameObject BtnCube; //Button's Cube component
    public ParticleSystem particles; //Particles displayed when mouse is over a button
    public Transform pauseMenu; //In-game pause menu
    public GameObject ConfirmPurchaseMenu;
    public GameObject NoMoneyMenu;
    public GameObject YourCoins;
    public GameObject Price;
    public GameObject Level1;
    public GameObject Level2;

    private GameObject cam; //Main Camera
    private string currentScene; //displays the current scene
    private string currentShowing = "Level1";


    /// <summary>
    /// Initialization
    /// </summary>
    void Start() 
    {
        //Find the Main Camera
        cam = GameObject.Find("Main Camera");
        //Current scene name stored at start
        currentScene = Application.loadedLevelName;
    }

    void Update() 
    {
       
    }

    /// <summary>
    /// Click event of a button
    /// </summary>
    void OnMouseUp()
    {
        //Play Button - Main Menu
        if (gameObject == GameObject.Find("Play"))
        {
            //Play click sound
            clickSound.Play();
            //Call "PlayGame" method after 1 second
            Invoke("PlayGame", 1);
        }

        //Levels Button - Main Menu
        if (gameObject == GameObject.Find("Levels"))
        {
            //Play click sound
            clickSound.Play();
            //Play Levels Animation
            cam.animation.Play("ShowLevels");
            Invoke("ShowYourCoins", 1);
        }
        
        //Level's Back Button - Main Menu
        if (gameObject == GameObject.Find("BackLvl"))
        {
            //Play camera's second animation to return to main menu
            cam.animation.Play("ShowLevels_back");
            //Play click sound
            clickSound.Play();
            YourCoins.transform.position = new Vector3(-20.14165f, 20.91975f, -6.212461f);
        }

        //HighScores Button - Main Menu
        if (gameObject == GameObject.Find("HighScores"))
        {
            //Play click sound
            clickSound.Play();
            //Play camera's animation moving it to the Show HighScores section
            cam.animation.Play("ShowHS");
        }

        //High Score's Back Button - Main Menu
        if (gameObject == GameObject.Find("BackHS"))
        {
            //Play camera's second animation to return to main menu
            cam.animation.Play("ShowHS_back");
            //Play click sound
            clickSound.Play();
        }

        //Pause Button - In-game
        if (gameObject == GameObject.Find("Pause"))
        {
            //Check the game state. If the game is playing
            if (GeneralControls.gameState == GameState.playing)
            {
                //Play the pause menu animation
                pauseMenu.animation.Play("ShowPauseMenu");
                //change the game's state to paused
                GeneralControls.gameState = GameState.paused;

            }
            //If the game is paused when the button is pressed
            else if (GeneralControls.gameState == GameState.paused)
            {
                //Play the other pause menu animation to hide it
                pauseMenu.animation.Play("HidePauseMenu");
                //Change the game's state to playing
                GeneralControls.gameState = GameState.playing;
            }
        }

        //Resume button - Pause menu
        if (gameObject == GameObject.Find("Resume"))
        {
            clickSound.Play();
            //Play the animation to hide the menu
            pauseMenu.animation.Play("HidePauseMenu");
            //Chenge the game state back to playing
            GeneralControls.gameState = GameState.playing;
            
        }

        //Restart button - Pause menu
        if (gameObject == GameObject.Find("Restart"))
        {
            //Reload the current Scene
            Application.LoadLevel(currentScene);
        }

        //Main Menu button - Pause menu
        if (gameObject == GameObject.Find("MainMenu"))
        {
            clickSound.Play();

            //Check the value of currentScene
            CheckLevel();
        }

        //Quit button - Pause menu
        if (gameObject == GameObject.Find("Quit"))
        {
            //Quit the game
            Application.Quit();
        }

        //Retry button - ScoreBoard
        if (gameObject == GameObject.Find("Retry"))
        {
            //Reload the current Scene
            Application.LoadLevel(currentScene);
        }

        //Main Menu button - ScoreBoard
        if (gameObject == GameObject.Find("MainMenu2"))
        {
            //Check the value of currentScene
            CheckLevel();
        }

        //Yes Button - Confirm Purchase Menu
        if (gameObject == GameObject.Find("Yes"))
        {
            clickSound.Play();
            if (PlayerPrefs.GetInt("PurchaseBeach") == 1)
            {
                PlayerPrefs.SetInt("LvlBeach", 1);
            }

            if (PlayerPrefs.GetInt("PurchaseFire") == 1)
            {
                PlayerPrefs.SetInt("LvlFire", 1);
            }
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - 800);
            ConfirmPurchaseMenu.animation.Play("HideConfirm");
            
            
        }

        //No Button - Confirm Purchase Menu
        if (gameObject == GameObject.Find("No"))
        {
            ConfirmPurchaseMenu.animation.Play("HideConfirm");
            PlayerPrefs.SetInt("PurchaseBeach", 0);
            PlayerPrefs.SetInt("PurchaseFire", 0);
            clickSound.Play();
        }

        //Ok Buton - No Enough Money Menu
        if (gameObject == GameObject.Find("Ok"))
        {
            NoMoneyMenu.animation.Play("HideNoMoney");
            clickSound.Play();
        }

        //Right button - Level Selection
        if (gameObject == GameObject.Find("Right"))
        {
            if (currentShowing == "Level1")
            {
                Level1.animation.Play("GoRight");
                Level2.animation.Play("ReturnLeft");
                currentShowing = "Level2";
            }
            else if (currentShowing == "Level2")
            {
                Level1.animation.Play("ReturnLeft");
                Level2.animation.Play("GoRight");
                currentShowing = "Level1";
            }
        }

        //Share button - Scoreboard
        if (gameObject == GameObject.Find("Share"))
        {
            //If we're on windows 8, open share charm
            #if UNITY_METRO
            WindowsGateway.ShareHighScore();
            #endif
        }

        //Customize Button - Main Menu
        if (gameObject == GameObject.Find("Customize"))
        {
            //Camera animation
            cam.animation.Play("ShowCustomize");
            //Sound Effect
            clickSound.Play();
            //Invokes the method after 1 second
            Invoke("ActivateCustomizeMenu", 1);
            if (!PlayerPrefs.HasKey("CurrentSkin") && !PlayerPrefs.HasKey("SkinsBought"))
            {
                PlayerPrefs.SetString("SkinsBought", "0");
                PlayerPrefs.SetInt("CurrentSkin", 0);
            }
        }
    }

    /// <summary>
    /// Mouse enters the button
    /// </summary>
    void OnMouseEnter()
    {
        //Play the mouse over sound
        mouseOverSound.Play();
        //Change the material to Yellow
        BtnCube.renderer.material = hoverMaterial;
        //Play fire particles
        PlayParticles(true);
    }

    /// <summary>
    /// When mouse exits the button
    /// </summary>
    void OnMouseExit()
    {
        //Change the material back to the normal state (Red color)
        BtnCube.renderer.material = normalMaterial;
        PlayParticles(false);
    }

    /// <summary>
    ///Play Game method
    /// </summary>
    void PlayGame() 
    {
        //Check the value of currentScene
        CheckLevel();
    }

    /// <summary>
    /// Method for the particles when mouse is over any button
    /// </summary>
    /// <param name="canPlay">Variable to control the particles</param>
    void PlayParticles(bool canPlay)
    {
        //If there are any particles attached
        if (particles != null)
        {
            //If the variable is set to true
            if (canPlay == true)
            {
                //Play the fire particles of the button
                particles.Play();
            }
            //If the variable is set to false
            else if (canPlay == false)
            {
                //Stop the fire particles of the button
                particles.Stop();
            }
        }
    }

    void ShowYourCoins() 
    {
        YourCoins.transform.position = new Vector3(-20.14165f, 10.91975f, -6.212461f);
    }

    void CheckLevel() 
    {
        switch (currentScene)
        {
            //If we're on the normal level's main menu
            case "MainMenu":
                //Load the Normal level
                Application.LoadLevel("Level00");
                break;
                    
            //If we're on the Fire level's main menu
            case "MainMenuFire":
                //Load the Fire level's main menu
                Application.LoadLevel("LevelFire");
                break;

            //If we're on the Fire level's main menu
            case "MainMenuBeach":
                //Load the Fire level's main menu
                Application.LoadLevel("LevelBeach");
                break;

            //If we're on the normal level
            case "Level00":
                //Load the Normal level's main menu
                Application.LoadLevel("MainMenu");
                break;

            //If we're on the Fire level
            case "LevelFire":
                //Load the Fire level's main menu
                Application.LoadLevel("MainMenuFire");
                break;

            //If we're on the normal level
            case "LevelBeach":
                //Load the Normal level's main menu
                Application.LoadLevel("MainMenuBeach");
                break;

            default:
                Debug.Log("Not yet implemented");
                break;
        }
    }

    void ActivateCustomizeMenu() 
    {
        cam.GetComponent<CustomizeBall>().enabled = true;
    }
}
