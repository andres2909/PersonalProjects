using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour
{
    #region Other Functions

    /// <summary>
    /// Loads a specified Scene
    /// </summary>
    /// <param name="Scene">Scene to load</param>
    public void LoadScene(string Scene)
    {
        Time.timeScale = 1;
        StartCoroutine(Load(Scene));
    }

    public void Exit()
    {
        Application.Quit();
    }
    
    IEnumerator Load(string Scene)
    {
        FadeInOut.fadeDir = false;
        yield return new WaitForSeconds(0.5f);
        Application.LoadLevel(Scene);
    }

    #endregion
}
