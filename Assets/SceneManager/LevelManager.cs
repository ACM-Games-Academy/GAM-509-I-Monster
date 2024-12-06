using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Animator crossFade;
    public int menuIndex;
    public int level1Index;
    public float transTime = 1f;
    
    public void LoadMainMenu()
    {
        StartCoroutine(ChangeScene(0));
    }
    public void LoadGameScene()
    {
        StartCoroutine(ChangeScene(1));
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    IEnumerator ChangeScene(int index)
    {
        crossFade.SetTrigger("Start");
        yield return new WaitForSeconds(transTime);
        SceneManager.LoadScene(index);
    }
}
