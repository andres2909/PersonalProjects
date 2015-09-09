using UnityEngine;
using System.Collections;

public class SkeletonDeathSound : MonoBehaviour 
{
    public AudioSource deathSound;
    public TextMesh addScore;

	// Use this for initialization
	void Start () 
    {
        deathSound.Play();
        GameObject.Instantiate(addScore, transform.position, Quaternion.identity);
        PlayerPrefs.SetInt("EnemyKillScore", PlayerPrefs.GetInt("EnemyKillScore") + 10);
	}

    void Update() 
    {

    }
}
