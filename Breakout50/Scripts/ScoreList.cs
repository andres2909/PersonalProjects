using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
//using Parse;

public class ScoreList : MonoBehaviour
{
    #region Global Variables

    public GameObject textPrefab;
    public GameObject localContentPanel;
    public GameObject worldContentPanel;
    public Image loadingIcon;
    
    private GameObject scoreText;
    private bool onlineScoresLoaded;

    #endregion

    #region Unity Functions

    // Use this for initialization
    void Start()
    {
        for (int i = 1; i <= 5; i++)
        {
            if (PlayerPrefs.HasKey("HS_name" + i) && PlayerPrefs.HasKey("HS_score" + i))
            {
                scoreText = Instantiate(textPrefab) as GameObject;
                scoreText.transform.SetParent(localContentPanel.transform);
                scoreText.GetComponent<Text>().text = PlayerPrefs.GetString("HS_name" + i) + " - " + PlayerPrefs.GetInt("HS_score" + i);
            }
            else
            {
                break;
            }
        }

        onlineScoresLoaded = false;
        loadingIcon.enabled = false;

        //var query = ParseObject.GetQuery("Scores")
        //            .OrderByDescending("Score")
        //            .Limit(10);

        //query.FindAsync().ContinueWith(t =>
        //{
        //    if (t.IsFaulted)
        //    {
        //        IEnumerable<ParseObject> results = t.Result;
        //        foreach (var result in results)
        //        {
        //            scoreText = Instantiate(textPrefab) as GameObject;
        //            scoreText.transform.parent = worldContentPanel.transform;
        //            scoreText.GetComponent<Text>().text = result.Get<string>("Name") + " - " + result.Get<int>("Score");
        //        }
        //        onlineScoresLoaded = true;
        //    }
        //});
        onlineScoresLoaded = true;

        worldContentPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!onlineScoresLoaded)
        {
            loadingIcon.transform.Rotate(new Vector3(0, 0, -7));
        }
        else
        {
            loadingIcon.enabled = false;
        }
        
    }

    #endregion

    #region Other Functions

    /// <summary>
    /// Displays Global or World High scores depending on what is passed in
    /// </summary>
    /// <param name="display">True for Global. False for Local</param>
    public void GlobalOrLocal(bool display)
    {
        worldContentPanel.SetActive(display);
        loadingIcon.enabled = display;
        localContentPanel.SetActive(!display);
    }

    #endregion

}
