using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ButtonControl : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject mapPanel;
    public GameObject smallMapPanel;

    public Sprite soundOnIcon;
    public Sprite soundOffIcon;

    public GameObject Sound;



    public void StartGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
        Debug.Log("starting game...");
    }


    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("quitting game...");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void PauseControl()
    {
        if (Time.timeScale == 1)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
            pausePanel.SetActive(false);
        }
    }

    public void MapControl()
    {
      if (Time.timeScale == 1)
        {
            mapPanel.SetActive(true);
           smallMapPanel.SetActive(false);
            Time.timeScale = 0;
       }
        else
        {
            Time.timeScale = 1;
           mapPanel.SetActive(false);
            smallMapPanel.SetActive(true);
        }
    }

    public void GameSound()
    {
        if (Sound.activeSelf)
        {
            Sound.SetActive(false);
            this.gameObject.GetComponent<SpriteRenderer>().sprite = soundOffIcon;
        } else
        {
            Sound.SetActive(true);
            this.gameObject.GetComponent<SpriteRenderer>().sprite = soundOnIcon;
        }
    }

    public void ChangeSoundSprite()
    {
        //if (SpriteRenderer.sprite == soundOnIcon)
        //{
        //    SpriteRenderer.sprite = soundOffIcon;
        //}
        //else
        //{
        //    SpriteRenderer.sprite = soundOnIcon;
        //}
    }



}
