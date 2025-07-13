using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class B6SceneManager : MonoBehaviour
{

    public CanvasGroup fadeimg;
    public static float distance;
    public GameObject e;
    public GameObject player;
    public GameObject laddercheck;
    public GameObject enter;
    public GameObject light;
    public GameObject entercheck;
    public GameObject doorcheck;
    public GameObject lightcheck;
    public GameObject bg1;
    public GameObject bg2;
    public GameObject bg3;
    public GameObject bg4;
    public GameObject bg5;
    public Text text1;
    public Text text2;
    public static bool key;
    public static bool key2;
    public static bool key3;
    public static bool key4;
    public static bool key5;
    bool key6;
    void Start()
    {
        key = false;
        key2 = false;
        key3 = false;
        key4 = false;
        key5 = false;
        key6 = false;
        e.SetActive(false);
        enter.SetActive(false);
        light.SetActive(false);
        doorcheck.SetActive(true);
        lightcheck.SetActive(false);
        entercheck.SetActive(false);
        laddercheck.SetActive(false);
        bg1.SetActive(true);
        bg2.SetActive(true);
        bg3.SetActive(true);
        bg4.SetActive(true);
        bg5.SetActive(true);
        Color c = text1.color;
        c.a = 1;
        text1.color = c;
        Color d = text2.color;
        d.a = 0;
        text2.color = d;
        player.transform.position = new Vector2(-7.5f, -2);
        Moving.isCanmove = false;
        fadeimg.DOFade(0, 3)
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
            key = false;
        }
        if (key2 && GameManager.B6_num == 1)
        {
            StartCoroutine(Enter1());
            key2 = false;
        }
        if (key3 && GameManager.B6_num == 2)
        {
            B6_2();
            key3 = false;
        }
        if (key4 && GameManager.B6_num == 2)
        {
            StartCoroutine(Enter2());
            key4 = false;
        }
        if (key5 && GameManager.B6_num == 2)
        {
            StartCoroutine(B6_3());
            key5 = false;
        }
        if (distance <= 2 && GameManager.B6_num == 2)
        {
            e.SetActive(true);
        }
        else
        {
            e.SetActive(false);
        }
        distance = Vector3.Distance(player.transform.position, laddercheck.transform.position);
    }

    void B6_1()
    {
        Color c = text1.color;
        c.a = 0;
        text1.color = c;
        Moving.isCanmove = false;
        fadeimg.blocksRaycasts = true;
        fadeimg.DOFade(1, 1)
        .OnComplete(() =>
        {
            bg1.SetActive(false);
            enter.SetActive(true);
            entercheck.SetActive(true);
            player.transform.position = new Vector2(-7.5f, -3);
            fadeimg.DOFade(0, 1)
            .OnComplete(() =>
            {
                Moving.isCanmove = true;
                fadeimg.blocksRaycasts = false;
            });
        });
    }

    void B6_2()
    {
        Moving.isCanmove = false;
        fadeimg.blocksRaycasts = true;
        fadeimg.DOFade(1, 1)
        .OnComplete(() =>
        {
            bg3.SetActive(false);
            light.SetActive(true);
            doorcheck.SetActive(false);
            lightcheck.SetActive(true);
            player.transform.position = new Vector2(-7.5f, -3);
            fadeimg.DOFade(0, 1)
            .OnComplete(() =>
            {
                Moving.isCanmove = true;
                fadeimg.blocksRaycasts = false;
            });
        });
    }

    IEnumerator B6_3()
    {
        Moving.isCanmove = false;
        fadeimg.blocksRaycasts = true;
        fadeimg.DOFade(1, 1)
        .OnComplete(() =>
        {
        });
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("B5Scene");
    }

    IEnumerator Enter1()
    {
        Moving.isCanmove = false;
        yield return new WaitForSeconds(2f);
        bg2.SetActive(false);
        StartCoroutine(Enter1_1());
        yield break;
    }
    IEnumerator Enter1_1()
    {
        yield return new WaitForSeconds(2f);
        bg2.SetActive(true);
        StartCoroutine(Enter1_2());
        yield break;
    }
    IEnumerator Enter1_2()
    {
        yield return new WaitForSeconds(2f);
        enter.SetActive(false);
        entercheck.SetActive(false);
        bg2.SetActive(false);
        Moving.isCanmove = true;
        yield break;
    }
    IEnumerator Enter2()
    {
        Moving.isCanmove = false;
        yield return new WaitForSeconds(2f);
        bg4.SetActive(false);
        lightcheck.SetActive(false);
        laddercheck.SetActive(true);
        light.SetActive(false);
        Color d = text2.color;
        d.a = 1;
        text2.color = d;
        Moving.isCanmove = true;
        yield break;
    }
}
