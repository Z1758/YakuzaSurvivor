using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] PlayerInput input;
    [SerializeField] GameObject UI_s;
    [SerializeField] GameObject UI_pause;
    [SerializeField] GameObject gameoverCamera;
    [SerializeField] GameObject button;
    
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

    public void GameOver()
    {

        gameoverCamera.SetActive(true);
        StopObjects();
        StartCoroutine(CallGameEnd());

    }

    public void StopObjects()
    {
        UI_s.SetActive(false);
        controller.End();
        input.enabled = false;
        MobPool.Instance.AllStopEnemy();
        SkillManager.Instance.StopSkills?.Invoke();
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
            Debug.Log("일시정지 해제");
        }
    }

    public void LoadTilteScene()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("TitleScene");
    }
}
