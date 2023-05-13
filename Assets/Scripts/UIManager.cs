using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Cinemachine.DocumentationSortingAttribute;

public class UIManager : MonoBehaviour
{
    public GameObject wallpaper, p1, p2;
    public static int state = 1;
    private Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = GameObject.Find("Window").transform.localPosition;
    }

    public void NextButtonView()
    {
        //GameObject.Find("storyteller").GetComponent<TMP_Text>().text = "As you venture deeper into the pyramid, the air grows thick with the scent of ancient magic, and the walls seem to pulse with a mysterious energy. You can feel the weight of history bearing down on you, but your determination never wavers.";
        //wallpaper.SetActive(true);
        //state = 2;
        GameObject.Find("News").transform.DOLocalMoveY(1200f, 0.5f).SetEase(Ease.Linear);
        p1.SetActive(true);
        p2.SetActive(true);
        MapGenerator.restartF = true;
    }

    public void New()
    {
        SceneManager.LoadScene("Game");
        MapGenerator.level = 1;
        MapGenerator.crystal_level = 1;
    }

    public void Pause()
    {
        GameObject.Find("Window").transform.DOLocalMoveY(startPosition.y - 680f, 0.5f).SetEase(Ease.Linear);
    }

    public void Resume()
    {
        
        GameObject.Find("Window").transform.DOLocalMoveY(600f, 0.5f).SetEase(Ease.Linear);
    }

    public void NextChallenge()
    {
        if (MapGenerator.passed)
        {
            MapGenerator.crystal_level++;
            MapGenerator.level++;
            MapGenerator.restartF = true;
            GameObject.Find("Window2").transform.DOLocalMoveY(600f, 0.5f).SetEase(Ease.Linear);
        }
    }

    public void Restart()
    {
        GameObject.Find("Window").transform.DOLocalMoveY(600f, 0.5f).SetEase(Ease.Linear);
        
        MapGenerator.restartF = true;
    }

    public void Restart3()
    {
        GameObject.Find("Window3").transform.DOLocalMoveY(600f, 0.5f).SetEase(Ease.Linear);

        MapGenerator.restartF = true;
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("StartGame");
    }

    public void Quit()
    {
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #else
		    Application.Quit();
    #endif
    }
}
