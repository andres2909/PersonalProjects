using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CustomizeBall : MonoBehaviour 
{
    //Public Variables
    public GameObject Ball; //Ball
    public GameObject Skin; //Second sphere rendered outside the Ball
    public GUISkin GUIskin; //Custom GUI
    public Texture LargePreview; //Big preview image."No Skin Selected" image by default
    public Texture OwnedTexture; //"Owned" image to let the pplayer know they already have the selected skin
    public Texture coinTexture; //Texture of a coin
    public AudioSource buyButtonEffect; //Audio whenever a user buys a skin
    public float test;

    //Private Variables
    private Vector2 scrollView; //Vector 2 of the scrollview
    private List<Texture> textureList; //List of all the small preview images
    private List<Material> matList; //List of all the materials
    private int selectedSkin = -1; //Current skin selected in the menu. Default value: -1(No skin selected)
    private bool disableBuyButton = true; //Boolean to enable and disable the "Buy" Button
    private bool disableUseButton = true;//Boolean to enable and disable the "Use" Button
    private int coinBalance;

                                        //**The following variables change the position and scale of the GUI elements depending on the resolution of the screen**\\
    //Height of the scrollview
    private float scrollHeight = 1.2f;
    //Font Size
    private float fontSize = 25.0f;
    //Position and Sacle of the Menu
    private float menuPosX = 2.1f;
    private float menuPosY = 4.0f;
    private float menuScaleX = 2.25f;
    private float menuScaleY = 2.0f;
    //Position and Scale of the Buttonss
    private float buttonPosX = 4.6f;
    private float buttonPosY = 18.0f;
    private float buttonScaleX = 4.75f;
    private float buttonScaleY = 11.0f;
    //Position and Scale of the preview images (small and large)
    private float smallPreviewScaleX = 25.0f;
    private float smallPreviewPosY = 250.0f;
    private float largePreviewPosX = 2.0f;
    private float largePreviewPosY = 3.4f;
    private float largePreviewScaleX = 5.7f;
    private float largePreviewScaleY = 3.75f;
    //Position and Scale of the "Owned" image
    private float ownedTexturePosY = 2.0f;
    private float ownedTextureScaleY = 16.26f;
    //Position and Scale of the Skin Cost
    private float costLabelPosX = 2.29f;
    private float costPosY = 1.48f;
    private float costTextPosY = 1.65f;
    private float costNumberPosX = 2.08f;
    private float costCoinPosX = 1.64f;
    //Position and Scale of the "Buy" and "Use" buttons
    private float buyButtonPosX = 1.69f;
    private float buyButtonPosY = 1.7f;
    private float buyButtonScaleX = 11.0f;
    private float buyButtonScaleY = 15.0f;
    private float useButtonPosX = 2.02f;
    //Position and Scale of the Back button
    private float backButtonPosX = 100.0f;
    private float backButtonPosY = 1.2f;
    private float backButtonScaleX = 8.0f;
    private float backButtonScaleY = 8.0f;
    //Position and Scale of the Coin Balance
    private float coinBoxPosX = 1.21f;
    private float coinBoxSccaleX = 6.42f;
    private float coinNumberPosX = 1.229f;
    private float coinNumberPosY = 1.23f;
    private float coinImgPosX = 1.06f;
    private float coinImgPosY = 1.125f;
    private float coinImgScaleX = 35.63f;
    private float coinImgScaleY = 20.77f;

    private string[] assetNameList = { "Default", "Beach Ball", "Eight Ball", "Happy Face", "Lava Ball", "Moon", "Planet Earth", "Pokeball" };

	// Use this for initialization
	void Start ()
    {

        //Variable initialization
        textureList = new List<Texture>();
        matList = new List<Material>();

        //Get the preview images from the Previews folder in the Resources
        foreach (Texture tex in Resources.LoadAll<Texture>("Previews"))
        {
            //Add them to the texture List
            textureList.Add(tex);
            //assetNameList.Add(tex.ToString().Replace("(UnityEngine.Texture2D)", "").Replace('_', ' ').Replace("a ", ""));
        }

        //Get the Materials from the Skins folder in the Resources
        foreach (Material mat in Resources.LoadAll<Material>("Skins"))
        {
            //Add them to the Materials List
            matList.Add(mat);
        }
    }
	
	// Update is called once per frame
	void Update () 
    {
        //Once the script is enabled, the ball will stop bouncing and freeze its Y position
        if (Ball.transform.position.y > 3.0f && Ball.transform.position.y < 3.1f)
        {
            Ball.transform.rigidbody.constraints = RigidbodyConstraints.FreezePositionY;
        }
        coinBalance = PlayerPrefs.GetInt("Coins");

	}

    void OnGUI() 
    {
        //Menu's skin
        GUI.skin = GUIskin;
        //Change the GUI button font size
        GUIskin.button.fontSize = (int)(Screen.height / fontSize);
        GUIskin.label.fontSize = (int)(Screen.height / fontSize);
        GUIskin.box.fontSize = (int)(Screen.height / fontSize);

        //Scrollview
        #region Scrollview
        scrollView = GUI.BeginScrollView(new Rect(Screen.width / menuPosX, Screen.height / menuPosY, Screen.width / menuScaleX, Screen.height / menuScaleY),
                                         scrollView,
                                         new Rect(0, 0, 304, Screen.height / scrollHeight),
                                         false,
                                         true);

        //Variable that determines the Y position of all the buttons
        float posY = 0;
        //Skin Buttons
        for (int skinIndex = 0; skinIndex < textureList.Count; skinIndex++)
        {
            //Button click event
            if (GUI.Button(new Rect(Screen.width / buttonPosX, 10 + posY, Screen.width / buttonScaleX, Screen.height / buttonScaleY), assetNameList[skinIndex]))
            {
                //Change the Large Preview display image to the selected skin
                LargePreview = textureList[skinIndex];

                //"selectedSkin" value changes to the index of the button pressed
                selectedSkin = skinIndex;

                //Check if the selected skin is already bought
                if (PlayerPrefs.GetString("SkinsBought").Contains(skinIndex.ToString()))
                {
                    //if it is, the "Buy" button will be disabled
                    disableBuyButton = true;
                    //and the "Use" button will be enabled
                    disableUseButton = false;
                }
                else
                {
                    //otherwise, the "Buy" button will be enabled
                    disableBuyButton = false;
                    //and the "Use" button will be disabled
                    disableUseButton = true;
                    //The cost of the skin will appear
                    costPosY = 1.48f;
                    costTextPosY = 1.65f;
                }
            }
            //Small preview image of the each skin
            GUI.DrawTexture(new Rect(Screen.width / buttonPosX + 5, (Screen.width / smallPreviewPosY) + 10 + posY, Screen.width / smallPreviewScaleX, Screen.width / smallPreviewScaleX), textureList[skinIndex]);
            //Position of the buttons changes each loop
            posY += Screen.width / buttonPosY;
        }
        //End of the scrollview
        GUI.EndScrollView();
        #endregion

        //Large image Preview
        GUI.DrawTexture(new Rect(Screen.width / largePreviewPosX, Screen.height / largePreviewPosY, Screen.width / largePreviewScaleX, Screen.height / largePreviewScaleY), LargePreview);

        //If the selected skin is already bought, the buy button will be disabled
        if (disableBuyButton)
        {
            //The cost of the skin will disappear
            costPosY = 0;
            costTextPosY = 0;

            //When a skin is selected
            if (selectedSkin != -1)
            {
                //The "Owned" image will appear if the "Buy" button is disabled
                GUI.DrawTexture(new Rect(Screen.width / largePreviewPosX, Screen.height / ownedTexturePosY, Screen.width / largePreviewScaleX, Screen.height / ownedTextureScaleY), OwnedTexture);
            }
            GUI.enabled = false;
        }
        //If the "Buy" button is enabled
        else
        {
            //Check if the player has enough money
            if (PlayerPrefs.GetInt("Coins") < 150)
            {
                //If he doesn't, change the color of the price's label to red
                GUIskin.label.normal.textColor = Color.red;
                //And disable the "Buy" button
                GUI.enabled = false;
            }
        }

        //Buy button
        if(GUI.Button(new Rect(Screen.width / buyButtonPosX, Screen.height / buyButtonPosY, Screen.width / buyButtonScaleX, Screen.height / buyButtonScaleY), "Buy"))
        {
            //Adds the skin to the "bought" list in PlayerPrefs
            PlayerPrefs.SetString("SkinsBought", PlayerPrefs.GetString("SkinsBought").Insert(0, selectedSkin.ToString()));
            //Plays thee button sound effect
            buyButtonEffect.Play();
            //Disables the "Buy" button
            disableBuyButton = true;
            //Enables the "Use" button
            disableUseButton = false;
            //Reduces player's balance by the Skin cost (150)
            StartCoroutine(ReduceCoinBalance());
        }
        GUI.enabled = true;

        //Skin cost (Text)
        GUI.Label(new Rect(Screen.width / costLabelPosX, Screen.height / costTextPosY, Screen.width / backButtonScaleX, Screen.height / backButtonScaleY), "Cost:");
        //Skin cost (Number)
        GUI.Label(new Rect(Screen.width / costNumberPosX, Screen.height / costTextPosY, Screen.width / backButtonScaleX, Screen.height / backButtonScaleY), "150");
        //Coin Image
        GUI.DrawTexture(new Rect(Screen.width / costCoinPosX, Screen.height / costPosY, Screen.width / coinImgScaleX, Screen.height / coinImgScaleY), coinTexture);
        //Set the color of the labels back to white
        GUIskin.label.normal.textColor = Color.white;
        
        //The "Use" button will be disabled when the user selects a skin he already bought or buys a skin
        if (disableUseButton || PlayerPrefs.GetInt("CurrentSkin") == selectedSkin)
        {
            GUI.enabled = false;
        }

        //Use button
        if (GUI.Button(new Rect(Screen.width / useButtonPosX, Screen.height / buyButtonPosY, Screen.width / buyButtonScaleX, Screen.height / buyButtonScaleY), "Use"))
        {
            //Sets the current skin index to the currentSkin variable in the player prefs
            PlayerPrefs.SetInt("CurrentSkin", selectedSkin);
            //Change the material of the Ball
            Skin.renderer.material = matList[selectedSkin];
            //Disables the "Use" button
            disableUseButton = true;
        }
        GUI.enabled = true;

        //Coin Balance Box
        GUI.Box(new Rect(Screen.width / coinBoxPosX, Screen.height / backButtonPosY, Screen.width / coinBoxSccaleX, Screen.height / backButtonScaleY), "Your Coins:");
        //Coin Balance Label (Number)
        GUI.Label(new Rect(Screen.width / coinNumberPosX, Screen.height / coinNumberPosY, Screen.width / backButtonScaleX, Screen.height / backButtonScaleY), coinBalance.ToString());
        //Coin Image
        GUI.DrawTexture(new Rect(Screen.width / coinImgPosX, Screen.height / coinImgPosY, Screen.width / coinImgScaleX, Screen.height / coinImgScaleY), coinTexture);

        //Back button
        if (GUI.Button(new Rect(Screen.width / backButtonPosX, Screen.height / backButtonPosY, Screen.width / backButtonScaleX, Screen.height / backButtonScaleY), "Back"))
        {
            //When pressed, the ball will start bouncing again
            Ball.transform.rigidbody.constraints = RigidbodyConstraints.None;
            //The script will be disabled
            GameObject.Find("Main Camera").GetComponent<CustomizeBall>().enabled = false;
            //The camera animation to go back will play
            GameObject.Find("Main Camera").GetComponent<Animation>().Play("ShowCustomize_back");
        }
    }

    /// <summary>
    /// Purchase animation to reduce the coin balance
    /// </summary>
    /// <returns></returns>
    IEnumerator ReduceCoinBalance() 
    {
        for (int i = 0; i < 150; i++)
        {
            yield return new WaitForSeconds(0.00625F);
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - 1);
        }
    }
}
