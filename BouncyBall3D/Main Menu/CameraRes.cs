using UnityEngine;
using System.Collections;

public class CameraRes : MonoBehaviour 
{
    public GameObject fadeInMaterial;

    private float timer = 1;

	// Use this for initialization
	void Start () 
    {
        audio.PlayDelayed(0.5f);

        Screen.SetResolution(1366, 768, true);

        //if (PlayerPrefs.HasKey("LastLevel") == false)
        //{
        //    PlayerPrefs.SetString("LastLevel", "Default");
        //}
        //else if(PlayerPrefs.HasKey("LastLevel"))
        //{
        //    switch (PlayerPrefs.GetString("LastLevel"))
        //    {
        //        //Default
        //        case "Default":
        //            Application.LoadLevel("MainMenu");
        //            break;

        //        //Fire
        //        case "Fire":
        //            Application.LoadLevel("MainMenuFire");
        //            break;

        //        //Beach
        //        case "Beach":
        //            Application.LoadLevel("MainMenuBeach");
        //            break;

        //        default:
        //            break;
        //    }
        //}
	}

    void Update() 
    {
        FadeInAnimation();
    }

    void FadeInAnimation()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            fadeInMaterial.renderer.material.color = new Color(0, 0, 0, timer);
        }
        if (timer < 0)
        {
            timer = 0;
        }
    }
}
