using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;

public class B6ScriptManager : MonoBehaviour
{
    public static B6ScriptManager Instance;
    public CanvasGroup Script;
    public new TextMeshProUGUI name;
    public TextMeshProUGUI script;
    public Image character1;
    public Image character2;
    public Image character3;
    public float speed = 0.05f;
    public Sprite michael;
    public Sprite raphael;
    public Sprite uriel;
    public Sprite nullimg;
    Dictionary<string, Sprite> Charas;    
    bool isCantalk;
    int index;

    void Awake()
    {
        Instance = this;
        Script.alpha = 0;
        name.text = "";
        script.text = "";
        character1.sprite = null;
        character2.sprite = null;
        character3.sprite = null;
        Charas = new Dictionary<string, Sprite>()
        {
            {"michael", michael},
            {"raphael", raphael},
            {"uriel", uriel},
            {"null", nullimg}
        };
    }

    void Update()
    {

    }
    public IEnumerator PrintScript(int index)
    {
        isCantalk = false;
        if (DialogueManager.lines[index].image2 == "null")
        {
            character1.sprite = Charas["null"];
            character2.sprite = Charas["null"];
            character3.sprite = Charas[DialogueManager.lines[index].image1];
        }
        else
        {
            character1.sprite = Charas[DialogueManager.lines[index].image1];
            character2.sprite = Charas[DialogueManager.lines[index].image2];
            character3.sprite = Charas["null"];
        }
        for (int i = 0; i < DialogueManager.lines[index].detail.Length; i++)
        {
            name.text = DialogueManager.lines[index].character[..];
            script.text = DialogueManager.lines[index].detail[..(i + 1)];
            yield return new WaitForSeconds(speed);
        }
        isCantalk = true;
        yield return null;
        yield break;
    }
    
    public IEnumerator PrintScriptNum(int startindex, int endindex)
    {
        Script.DOFade(1, 0.5f)
        .OnComplete(() =>
        {
        });
        index = startindex;
        Moving.isCanmove = false;
        StartCoroutine(PrintScript(index));
        while (index <= endindex)
        {
            yield return StartCoroutine(PrintScript(index));
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0) && isCantalk);
            index += 1;
        }
        Moving.isCanmove = true;
        Script.DOFade(0, 0.5f)
        .OnComplete(() =>
        {
        });
        yield return null;
        yield break;
    }
}
