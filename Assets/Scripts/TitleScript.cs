using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScript : MonoBehaviour
{
    [SerializeField] GameObject loadingBack;
    [SerializeField] Image loadingBar;
    [SerializeField] Transform target;
    [SerializeField] float speed;
    [SerializeField] TextMeshProUGUI text;
    public void Loading()
    {

        StartCoroutine(StartGame());

    }

    IEnumerator StartGame()
    {
        loadingBack.SetActive(true);
        AsyncOperation loading = SceneManager.LoadSceneAsync("GameScene");


        float timer = 0;
        int cnt = 0;
        while (!loading.isDone)
        {
            timer +=  Time.deltaTime;
            if(timer >= 0.05f)
            {
                timer = 0;
                cnt++;

                if(cnt > 3)
                {
                    cnt = 0;
                }
            }
          
       
            StringBuilder sb = new StringBuilder("·Îµù Áß");
           

            for (int i = 0; i < cnt; i++)
            {
                sb.Append(".");
         
            }

            text.text = sb.ToString();
            yield return null;
        }

        loading.allowSceneActivation = true;
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit(); 
        #endif
    }
}
