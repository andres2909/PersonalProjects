using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//using Parse;
using System.Collections.Generic;

public class HSManager : MonoBehaviour
{
    #region Global Variables

    // Public Variables
    public GameObject HSBoard;              // High Score board
    public GameObject localHS;              // New Local high score message
    public GameObject worldHS;              // New Global high score message
    public GameObject GOMenu;               // Game Over menu
    public GameObject uiBackground;         // Black background
    public InputField nameInput;            // Input field for the player's name
    public Text errorMessage;               // Error message (in case player doesnt enter a name)
    public Button next;                     // Next button on the High score board
    public Image loadingIcon;               // Loading online scores spinning icon

    // Private Variables
    private bool save = false;              // Makes sure code to save game executes only once
    private bool newHighScore = false;      // True if there's a score to save
    //private bool saveOnline = false;        // True if score has to be saved online
    private bool saveLocally = false;       // True if score has to be saved offline
    //private bool onlineHSLoaded = false;    // Checks if online score has been loaded
    private string localNameKey;            // Name of the name key in PlayerPrefs
    private string localScoreKey;           // Name of the score key in PlayerPrefs
    private string playerName;              // Name inputed by the user in the Input box

    #endregion

    #region Unity Functions

    /// <summary>
    /// Start Function
    /// </summary>
    void Start()
    {
        saveLocally = false;
        //saveOnline = false;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // When the game ends...
        if (GameManager.gameState == GameState.End && !save)
        {
            // Stop movement
            Time.timeScale = 0;

            // Check for high scores
            CheckHighScore();
            save = true;
        }

        // Spin the loading icon.
        loadingIcon.transform.Rotate(new Vector3(0, 0, -7));

        //// Show world high score message and hide the loading icon
        //if (onlineHSLoaded && saveOnline)
        //{
        //    loadingIcon.color = Color.clear;
        //    worldHS.SetActive(true);
        //}
        //// Hide the loading icon
        //else if (onlineHSLoaded && !saveOnline)
        //{
        //    loadingIcon.color = Color.clear;
        //}
    }

    #endregion

    #region Other Functions

    /// <summary>
    /// Check both online and locally if player reached a new high scores
    /// </summary>
    void CheckHighScore()
    {
        // Local High Scores
        for (int i = 1; i <= 5; i++)
        {
            // Replace local score and name if "score" is greater than the one stored. GetInt() returns 0 if key is not found
            if (GameManager.score > PlayerPrefs.GetInt("HS_score" + i))
            {
                saveLocally = true;
                newHighScore = true;

                // Save name and score PlayerPrefs keys
                localNameKey = "HS_name" + i;
                localScoreKey = "HS_score" + i;

                // Show "New local high score" message
                localHS.SetActive(true);

                // When new high score is found, break out of loop
                break;
            }
        }

        // //World High Scores query
        //var query = ParseObject.GetQuery("Scores")
        //        .OrderByDescending("Score")
        //        .Limit(10);

        //query.FindAsync().ContinueWith(t =>
        //{
        //    // If there's no error querying the database...
        //    if (!t.IsFaulted)
        //    {
        //        // Iterate through every element
        //        IEnumerable<ParseObject> results = t.Result;
        //        foreach (var result in results)
        //        {
        //            // Replace database score and name if "score" is greater than the one stored
        //            if (GameManager.score > result.Get<int>("Score"))
        //            {
        //                saveOnline = true;
        //                newHighScore = true;

        //                // When new high score is found, break out of loop
        //                break;
        //            }
        //        }
        //        onlineHSLoaded = true;
        //    }
        //    else
        //    {
        //    }
        //});

        // If player has new high score, show the new high score box
        if (newHighScore)
        {
            ShowUI(false, true, true);
        }
        // If there's no new high score show Game over menu
        else
        {
            ShowUI(true, false, true);
        }
        save = true;
    }

    /// <summary>
    /// Will control what is displayed on screen depending on what is passed in.
    /// </summary>
    /// <param name="_GOMenu"> Shows/Hides the Game Over menu </param>
    /// <param name="_HSBoard"> Shows/Hides the High Score Board </param>
    /// <param name="_uiBackground"> Shows/Hides the semi-transparent background </param>
    void ShowUI(bool _GOMenu, bool _HSBoard, bool _uiBackground)
    {
        // Game over menu
        if (_GOMenu)
        {
            GOMenu.transform.localScale = Vector3.one;
        }
        else
        {
            GOMenu.transform.localScale = Vector3.zero;
        }

        // High score board
        if (_HSBoard)
        {
            HSBoard.transform.localScale = Vector3.one;
        }
        else
        {
            HSBoard.transform.localScale = Vector3.zero;
        }

        // Black UI Background
        uiBackground.SetActive(_uiBackground);
    }

    /// <summary>
    /// Saves the High Score locally
    /// </summary>
    void SaveLocally()
    {
        if (saveLocally)
        {
            PlayerPrefs.SetInt(localScoreKey, GameManager.score);
            PlayerPrefs.SetString(localNameKey, playerName);
        }
    }

    /// <summary>
    /// Saves the High Score Online
    /// </summary>
    void SaveOnline()
    {
        //if (saveOnline)
        //{
        //    ParseObject storeScore = new ParseObject("Scores");
        //    storeScore["Name"] = playerName;
        //    storeScore["Score"] = GameManager.score;
        //    storeScore.SaveAsync();
        //    saveOnline = false;
        //}
    }

    /// <summary>
    /// Saves the High Score. Called only when "Next" button is pressed.
    /// </summary>
    public void GetPlayerName()
    {
        // Check that the input box is not empty
        if (nameInput.text == "")
        {
            errorMessage.text = "You must provide a name!";
        }
        // Save high score when user types a name
        else
        {
            playerName = nameInput.text;
            SaveLocally();
            //SaveOnline();
            ShowUI(true, false, true);
        }
    }

    #endregion

}
