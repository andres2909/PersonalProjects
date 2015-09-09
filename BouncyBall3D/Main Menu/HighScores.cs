using UnityEngine;
using System.Collections;

public class HighScores : MonoBehaviour 
{
    //Public Variables
    public TextMesh scoreTextMesh;

	// Use this for initialization
	void Start () 
    {
        if (gameObject == GameObject.Find("HighScore1"))
        {
            scoreTextMesh.text = "First Place: " + PlayerPrefs.GetInt("HighScore1");
        }
        else if (gameObject == GameObject.Find("HighScore2"))
        {
            scoreTextMesh.text = "Second Place: " + PlayerPrefs.GetInt("HighScore2");
        }
        else if (gameObject == GameObject.Find("HighScore3"))
        {
            scoreTextMesh.text = "Third Place: " + PlayerPrefs.GetInt("HighScore3");
        }
	}
}
