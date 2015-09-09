using UnityEngine;
using System.Collections;

public class ScoreBoard : MonoBehaviour
{
    //Public Variables
    public TextMesh score; //Score number in the board
    public GUIText scoreGUI; //Score number on the screen
    public TextMesh distanceNumber; //Distance traveled (number)
    public TextMesh timeNumber; //Time played (number)
    public TextMesh coinsNumber; //Coins earned (number)
    public TextMesh distanceTxt; //Distance traveled (text label)
    public TextMesh timeTxt; //Time played (text label)
    public TextMesh coinsTxt; //Coins earned (text label)
    public GameObject coinIcon; //Icon of a coin
    public AudioSource BellSound; //Sound effect of the board

    //Private Variables
    private bool playAnim = true; //Prevents the score animation to play more than once
    private float timer; //Timer that starts when the level is loaded
    private int totalTime; //Stores the time number when player dies
    private float distanceCount; //Stores the Y position of the player
    private int totalDistance; //Stores the players Y position when he dies
    private int totalCoins; //Total number of coins earned

    /// <summary>
    /// Initialization
    /// </summary>
    void Start()
    {
        //At the start of the animation, all the texts will not be visible
        score.renderer.enabled = false;
        distanceTxt.renderer.enabled = false;
        timeTxt.renderer.enabled = false;
        coinsTxt.renderer.enabled = false;
        distanceNumber.renderer.enabled = false;
        timeNumber.renderer.enabled = false;
        coinsNumber.renderer.enabled = false;
        coinIcon.renderer.enabled = false;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        timer = Time.time;
        distanceCount = transform.position.y / 3f;

        //Whenever the game's state changes to gameover
        if (GeneralControls.gameState == GameState.gameover && playAnim == true)
        {
            //variable set to false so animation plays only once
            playAnim = false;
            //Scoreboard's Animation
            animation.Play();

            totalTime = (int)timer;
            totalDistance = (int)distanceCount;
            totalCoins = int.Parse(scoreGUI.text) / 2;

            //Show Distance, time and coins.
            StartCoroutine(ScoreBoardAnim());
            
        }
    }

    /// <summary>
    /// Scores will have 
    /// </summary>
    /// <returns></returns>
    IEnumerator ScoreBoardAnim() 
    {
            //Each element in the board will appear after a short delay and play the sound effect
        
        //Score Number
        yield return new WaitForSeconds(0.5F);
        score.renderer.enabled = true;
        score.text = scoreGUI.text;
        BellSound.Play();

        //Distance
        yield return new WaitForSeconds(0.3F);
        distanceTxt.renderer.enabled = true;
        distanceNumber.renderer.enabled = true;
        distanceNumber.text = totalDistance + "m";
        BellSound.Play();

        //Time
        yield return new WaitForSeconds(0.3F);
        timeTxt.renderer.enabled = true;
        timeNumber.renderer.enabled = true;
        timeNumber.text = totalTime + "s";
        BellSound.Play();

        //Coins
        yield return new WaitForSeconds(0.3F);
        coinsTxt.renderer.enabled = true;
        coinsNumber.renderer.enabled = true;
        coinIcon.renderer.enabled = true;
        BellSound.Play();

        //The coins will increase one by one. The speed will depend on the final score
        //Less than 50 points
        if (totalCoins < 50)
        {
            for (int i = 0; i < totalCoins + 1; i++)
            {
                yield return new WaitForSeconds(0.025F);
                coinsNumber.text = "+" + i + "";
            }
        }
        //Between 50 and 200 poins
        else if (totalCoins > 50 && totalCoins < 200)
        {
            for (int i = 0; i < totalCoins + 1; i++)
            {
                yield return new WaitForSeconds(0.0125F);
                coinsNumber.text = "+" + i + "";
            }
        }
        //More than 200 points
        else if (totalCoins > 200)
        {
            for (int i = 0; i < totalCoins + 1; i += 5)
            {
                yield return new WaitForSeconds(0.00625F);
                coinsNumber.text = i + "";
            }
        }
        
    }


}