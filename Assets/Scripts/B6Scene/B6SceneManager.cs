using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class B6SceneManager : MonoBehaviour
{
    
    public CanvasGroup fadeimg;
    public GameObject player;
    public GameObject bg1;
    public Text text1;
    public static bool key = false;
    
    void Awake()
    {
        bg1.SetActive(true);
        Color c = text1.color;
        c.a = 1;      
        text1.color = c;
        player.transform.position = new Vector2(-7.5f, -2);
        Moving.isCanmove = false;
        fadeimg.DOFade(0, 2)
        .OnComplete(() =>
        {
            fadeimg.blocksRaycasts = false;
            Moving.isCanmove = true;
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (key && GameManager.B6_num == 1)
        {
            B6_1();
        }
    }

    void B6_1()
    {
        Color c = text1.color;
        c.a = 0;      
        text1.color = c;
        key = false;
        Moving.isCanmove = false;
        fadeimg.blocksRaycasts = true;
        fadeimg.DOFade(1, 1)
        .OnComplete(() =>
        {
            bg1.SetActive(false);
            player.transform.position = new Vector2(-7.5f, -2);
            fadeimg.DOFade(0, 1)
            .OnComplete(() =>
            {
                Moving.isCanmove = true;
                fadeimg.blocksRaycasts = false;
            });
        });
    }
}
