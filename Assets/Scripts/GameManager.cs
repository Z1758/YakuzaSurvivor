using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    [SerializeField] TimeLineScript tls;
    [SerializeField] PlayController controller;
    [SerializeField] GameObject defaultSkill;
    [SerializeField] PlayerInput input;
    [SerializeField] GameObject UI_s;
    [SerializeField] GameObject UI_pause;
    [SerializeField] GameObject gameoverCamera;
    [SerializeField] GameObject button;
    [SerializeField] WaitForSeconds timerWFS;
    [SerializeField] int timerCount;
    [SerializeField] TextMeshProUGUI timerText;

    private void Awake()
    {
        if (null == instance)
        {

            instance = this;

        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static GameManager Instance
    {

        get
        {

            return instance;
        }

    }


    public void StartGame()
    {
        timerWFS = new WaitForSeconds(1.0f);
        StartCoroutine(ClearTimer());
        UI_s.SetActive(true);
    
        controller.SetChangeStyle();
        input.enabled = true;
        defaultSkill.SetActive(true);
    }
    public void Clear()
    {
        StopObjects();
      
        tls.StartClear();
    }

    public void GameEnd()
    {
        Cursor.lockState = CursorLockMode.None;

        Cursor.visible = true;
        button.SetActive(true);
        Time.timeScale = 0.0f;
    }

    IEnumerator CallGameEnd()
    {
        yield return new WaitForSeconds(3.0f);
        GameEnd();
    }

    IEnumerator ClearTimer()
    {
        timerText.text = timerCount.ToString();
        while (timerCount>0)
        {
            yield return timerWFS;
            timerCount--;
            timerText.text = timerCount.ToString();
        }

        Clear();
    }

    public void GameOver()
    {

        gameoverCamera.SetActive(true);
        StopObjects();
        StartCoroutine(CallGameEnd());

    }

    public void StopObjects()
    {
        UI_s.SetActive(false);
        input.enabled = false;
        controller.End();
 
        MobPool.Instance.AllStopEnemy();
        SkillManager.Instance.StopSkills?.Invoke();
        StopAllCoroutines();
    }

    public void PauseGame(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            
            if (Time.timeScale > 0.8f)
            {
                UI_pause.SetActive(true);
                Pause();
            }
        }

    }

    public void Pause()
    {
        if (Time.timeScale > 0.8f)
        {

            Cursor.lockState = CursorLockMode.None;

            Cursor.visible = true;

            Time.timeScale = 0;

            input.enabled = false;

            Debug.Log("일시정지");
        }
    }

    public void Resume()
    {
        if (Time.timeScale == 0)
        {
            UI_pause.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;

            Cursor.visible = false;

            Time.timeScale = 1.0f;

            input.enabled = true;

            Debug.Log("일시정지 해제");
        }
    }

    public void LoadTilteScene()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("TitleScene");
    }
}
