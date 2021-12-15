using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerMain : MonoBehaviour
{
    [SerializeField] Text hpText; // hp 줄어드는 시스템을 개발하자
    [SerializeField] Text pointText;
    int nowtime;
    int nowpoint;
    int HP;
    Coroutine timePlay;

    private void OnEnable()
    {
        nowtime = 0;
        nowpoint = 0;
        HP = 10;
        timePlay = StartCoroutine("TimePlay");
        pointText.text = "점수 : 0점";
    }

    private void OnDisable()
    {
        StopCoroutine(timePlay);
    }

    IEnumerator TimePlay()
    {
        while (true)
        {
            nowtime++;
            yield return new WaitForSeconds(1.0f);
        }
    }

    public void GetPoint()
    {
        nowpoint += 10;
        pointText.text = "점수 : " + nowpoint.ToString() + "점";
    }

    public void LosePoint()
    {
        HP--;
    }
}
