using UnityEngine;
using UnityEngine.UI;
//using Parse;

public class ScoreList : MonoBehaviour
{
    #region Global Variables

    public GameObject textPrefab;
    public GameObject localContentPanel;
    //public GameObject worldContentPanel;
    //public Image loadingIcon;
    
    private GameObject scoreText;
    //private bool onlineScoresLoaded;
    //private bool onlineScoresShown = false;
    //private Dictionary<string, int> onlineScores;

    #endregion

    #region Unity Functions

    // Use this for initialization
    void Start()
    {
        // Show Local Scores in the screen
        for (int i = 1; i <= 5; i++)
        {
            // Loop untill there are no more keys (max. 5)
            if (PlayerPrefs.HasKey("HS_name" + i) && PlayerPrefs.HasKey("HS_score" + i))
            {
                scoreText = Instantiate(textPrefab) as GameObject;
                scoreText.transform.SetParent(localContentPanel.transform);
                scoreText.GetComponent<Text>().text = PlayerPrefs.GetString("HS_name" + i) + " - " + PlayerPrefs.GetInt("HS_score" + i);
                scoreText.transform.localScale = Vector3.one;
            }
            else
            {
                break;
            }
        }

        //onlineScores = new Dictionary<string, int>();
        //onlineScoresLoaded = false;
        //loadingIcon.enabled = false;



        //var query = ParseObject.GetQuery("HighScores")
        //            .OrderByDescending("Score")
        //            .Limit(10);
        
        //query.FindAsync().ContinueWith(t =>
        //{
        //    if (!t.IsFaulted)
        //    {
        //        IEnumerable<ParseObject> results = t.Result;
        //        foreach (var result in results)
        //        {
        //            onlineScores.Add(result.Get<string>("Name"), result.Get<int>("Score"));
        //        }
        //        onlineScoresLoaded = true;
        //        onlineScoresShown = true;
        //    }
        //});

        //worldContentPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //if (!onlineScoresLoaded)
        //{
        //    loadingIcon.transform.Rotate(new Vector3(0, 0, -7));
        //}
        //else if (onlineScoresLoaded && onlineScoresShown)
        //{
        //    loadingIcon.enabled = false;
        //    foreach (var hs in onlineScores)
        //    {
        //        scoreText = Instantiate(textPrefab) as GameObject;
        //        scoreText.transform.SetParent(worldContentPanel.transform);
        //        scoreText.GetComponent<Text>().text = hs.Key + " - " + hs.Value;
        //    }
        //    onlineScoresShown = false;
        //}
        
    }

    #endregion

    #region Other Functions

    /// <summary>
    /// Displays Global or World High scores depending on what is passed in. 
    /// This method is called by both "Local" and "World" buttons.
    /// </summary>
    /// <param name="display">True for Global. False for Local</param>
    public void GlobalOrLocal(bool display)
    {
        //worldContentPanel.SetActive(display);
        //if (!onlineScoresLoaded)
        //{
        //    loadingIcon.enabled = display;
        //}
        //localContentPanel.SetActive(!display);
    }

    #endregion

}
