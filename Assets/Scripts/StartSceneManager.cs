using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class StartSceneManager : MonoBehaviour
{
    private bool key;
    private bool Issave;
    public CanvasGroup fadeimg;
    public CanvasGroup uitext;
    void Awake()
    {
        if (!PlayerPrefs.HasKey("HasSave"))
        {
            PlayerPrefs.SetInt("HasSave", 0);
            PlayerPrefs.Save();
        }

        if (!PlayerPrefs.HasKey("ScreenMode"))
        {
            Screen.SetResolution(1920, 1080, FullScreenMode.Windowed);
            PlayerPrefs.SetInt("ScreenMode", 1);
            PlayerPrefs.Save();
        }
        int hassave = PlayerPrefs.GetInt("HasSave");
        if (hassave == 0)
        {
            Issave = false;
        }
        else
        {
            Issave = true;
        }
        uitext.blocksRaycasts = false;
        fadeimg.DOFade(0, 3)
        .OnComplete(() =>
        {
            fadeimg.blocksRaycasts = false;
            uitext.DOFade(1, 2)
            .OnComplete(()=>{
                uitext.blocksRaycasts = true;
            });
        });
        
    }
    void Start()
    {

    }

    void Update()
    {
        if (MouseOnUI.isMouseOver)
        {
            if (MouseOnUI.gameObj.name == "Startgame")
            {
                MouseOnUI.gameObj.transform.DOScale(1.15f, 0.5f);
                key = true;
                if (Input.GetMouseButtonDown(0))
                {
                    SceneManager.LoadScene("Scene1");
                }
            }
            else if (MouseOnUI.gameObj.name == "LoadGame" && Issave == true)
            {
                MouseOnUI.gameObj.transform.DOScale(1.15f, 0.5f);
                key = true;
                if (Input.GetMouseButtonDown(0))
                {
                }
            }
            else if(MouseOnUI.gameObj.name == "Quitgame") 
            { 
                MouseOnUI.gameObj.transform.DOScale(1.15f, 0.5f);
                key = true;
                if (Input.GetMouseButtonDown(0))
                {
            #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
            #else
                    Application.Quit(); 
            #endif
                }
            }
        }
        else if (key == true)
        {
            if (MouseOnUI.gameObj != null) MouseOnUI.gameObj.transform.DOScale(1f, 0.5f);
            key = false;
        }
    }

    public static void ScreenMode()
    {
        int screenmode = PlayerPrefs.GetInt("ScreenMode");
        if (screenmode == 0)
        {
            Screen.SetResolution(1920, 1080, FullScreenMode.Windowed);
            PlayerPrefs.SetInt("ScreenMode", 1);
        }
        else
        {
            Screen.SetResolution(Screen.width, Screen.height, FullScreenMode.FullScreenWindow);
            PlayerPrefs.SetInt("ScreenMode", 0);
        }
        PlayerPrefs.Save();
    }
}
