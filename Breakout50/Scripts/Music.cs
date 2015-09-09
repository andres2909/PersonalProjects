using UnityEngine;
using System.Collections;

 public class Music : MonoBehaviour
{
     public GameObject music;
     private GameObject musicObject;
     
     void Awake()
     {
         // Game Controller
         if (gameObject.tag == "GameController")
         {
             // Check if there's a music gameobject
             if (!GameObject.FindWithTag("Music"))
             {
                 musicObject = Instantiate(music);
                 musicObject.tag = "Music";
             }
         }

         // Music GameObject
         if (gameObject.tag == "Music")
         {
             DontDestroyOnLoad(gameObject);
         }
     }
}
