using UnityEngine;
using System.Collections;

public class SelectLevel : MonoBehaviour 
{
    //Public Variables
    public Material normalMat; //Normal material
    public Material hoverMat; //Mouse over material
    public GameObject background1; //background of the board
    public GameObject background2; //background of the text
    public GameObject background3;
    public TextMesh title;
    public TextMesh cost;
    public GameObject ConfirmPurchaseMenu;
    public GameObject NoMoneyMenu;
    public GameObject PriceFire;
    public GameObject PriceBeach;

    void Start() 
    {
       
    }

    void Update() 
    {
        if (PlayerPrefs.GetInt("LvlFire") == 1)
        {
            GameObject.Destroy(PriceFire);
        }

        if (PlayerPrefs.GetInt("LvlBeach") == 1)
        {
            GameObject.Destroy(PriceBeach);
        }
    }

    /// <summary>
    /// Mouse enters the level board
    /// </summary>
    void OnMouseEnter()
    {
        //Change the material of the board's background
        background1.renderer.material = hoverMat;
        //Change the material of the text's background
        background2.renderer.material = hoverMat;
        if (background3 != null)
        {
            //Change the material of the price background
            background3.renderer.material = hoverMat;
        }
    }

    /// <summary>
    /// Mouse exits the board
    /// </summary>
    void OnMouseExit() 
    {
        //Change the material of the board's background back to normal
        background1.renderer.material = normalMat;
        //Change the material of the title's background back to normal
        background2.renderer.material = normalMat;
        if (background3 != null)
        {
            //Change the material of the price background back to normal
            background3.renderer.material = normalMat;
        }
    }

    /// <summary>
    /// Whenever the level board is clicked
    /// </summary>
    void OnMouseUp() 
    {
        //If the user clicks the normal level
        if (gameObject == GameObject.Find("Normal"))
        {
            //Load the normal level
            Application.LoadLevel("MainMenu");
        }

        //If the user clicks on the fire level
        if (gameObject == GameObject.Find("Fire"))
        {

            if (PlayerPrefs.HasKey("LvlFire") == false)
            {
                PlayerPrefs.SetInt("LvlFire", 0);
            }
            if (PlayerPrefs.GetInt("LvlFire") == 0 && PlayerPrefs.GetInt("Coins") >= 800)
            {
                ConfirmPurchaseMenu.animation.Play("ShowConfirm");
                PlayerPrefs.SetInt("PurchaseFire", 1);
            }
            else if (PlayerPrefs.GetInt("LvlFire") == 0 && PlayerPrefs.GetInt("Coins") < 800)
            {
                NoMoneyMenu.animation.Play("ShowNoMoney");
            }
            else if (PlayerPrefs.GetInt("LvlFire") == 1)
            {
                Application.LoadLevel("MainMenuFire");
            }
        }

        //If the user clicks on the beach level
        if (gameObject == GameObject.Find("Beach"))
        {
            if (PlayerPrefs.HasKey("LvlBeach") == false)
            {
                PlayerPrefs.SetInt("LvlBeach", 0);
            }
            if (PlayerPrefs.GetInt("LvlBeach") == 0 && PlayerPrefs.GetInt("Coins") >= 800)
            {
                ConfirmPurchaseMenu.animation.Play("ShowConfirm");
                PlayerPrefs.SetInt("PurchaseBeach", 1);
            }
            else if (PlayerPrefs.GetInt("LvlBeach") == 0 && PlayerPrefs.GetInt("Coins") < 800)
            {
                NoMoneyMenu.animation.Play("ShowNoMoney");
            }
            else if (PlayerPrefs.GetInt("LvlBeach") == 1)
            {
                Application.LoadLevel("MainMenuBeach");
            }
        }
    }
}
