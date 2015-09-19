using UnityEngine;
using System.Collections;

 public class Instantiate : MonoBehaviour
{
     public GameObject music;
     //public GameObject parseInitializer;
     
     void Awake()
     {
         // Game Controller
         if (gameObject.tag == "GameController")
         {
             // Check if there's a music gameobject
             if (!GameObject.FindWithTag("Music"))
             {
                 music.tag = "Music";
                 Instantiate(music);
             }
             //// Check if there's a Parse Initializer gameobject
             //if (!GameObject.FindWithTag("Parse"))
             //{
             //    parseInitializer.tag = "Parse";
             //    Instantiate(parseInitializer);
             //}
         }
     }
}
