using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    #region Global Variables

    // Public Static Variables
    public static bool fadeDir;     // Fade in (true) or out (false)?

    // Private Variables
    private GameObject fader;       // Black image
    private float speed = 0.1f;     // Speed of the fade
    private float alpha = 1;        // Initial alpha (100%)

    #endregion

    #region Unity Functions

    /// <summary>
    /// Start Function
    /// </summary>
    void Start()
    {
        fader = GameObject.FindWithTag("Fader");
        fadeDir = true;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        Fade(fadeDir ? -1 : 1);
    }

    #endregion

    #region Other Functions

    /// <summary>
    /// Controls the black image's alpha covering the screen
    /// </summary>
    /// <param name="Dir"></param>
    void Fade(int Dir)
    {
        // Apha will increase or decrease depending on the direction
        alpha += speed * Dir;
        // Alpha will be between 0 and 1
        alpha = Mathf.Clamp01(alpha);

        // Change the alpha of the image
        fader.GetComponent<Image>().color = new Color(0, 0, 0, alpha);

        // When it almost reaches 0, disable image component
        if (alpha <= 0.01f)
        {
            fader.GetComponent<Image>().enabled = false;
        }
        // If the alpha is greater than 0 activate the image component again
        else
        {
            fader.GetComponent<Image>().enabled = true;
        }
    }

    #endregion

}