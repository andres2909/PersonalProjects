using UnityEngine;
using System.Collections;

public class CoinBalance : MonoBehaviour 
{
    public TextMesh playerBalance;

	// Use this for initialization
	void Start () 
    {
        
	}
	
	// Update is called once per frame
	void Update () 
    {
        playerBalance.text = PlayerPrefs.GetInt("Coins").ToString();
	}
}
