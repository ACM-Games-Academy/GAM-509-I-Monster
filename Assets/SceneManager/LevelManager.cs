using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Animator crossFade;
    public int menuIndex;
    public int level1Index;
    public int failScreenIndex;
    public int winScreenIndex;
    public float transTime = 1f;

    [SerializeField] private playerController player;
    [SerializeField] private WaveManager waveManager;

    private void Start()
    {    
        GameObject.Find("Player").TryGetComponent<playerController>(out player);
        GameObject.Find("PoolingManager").TryGetComponent<WaveManager>(out waveManager);

        player.playerDeath += levelFailed;
        waveManager.levelComplete += levelWin;
    }

    public void LoadMainMenu()
    {
        StartCoroutine(ChangeScene(0));
    }
    public void LoadGameScene()
    {
        StartCoroutine(ChangeScene(1));
    }

    private void levelFailed(object sender, EventArgs e)
    {
        StartCoroutine(ChangeScene(failScreenIndex));
    }

    private void levelWin(object sender, EventArgs e)
    {
        StartCoroutine(ChangeScene(winScreenIndex));
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
