using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerMain : MonoBehaviour
{
    [SerializeField] Text hpText; // hp �پ��� �ý����� ��������
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
        pointText.text = "���� : 0��";
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
        pointText.text = "���� : " + nowpoint.ToString() + "��";
    }

    public void LosePoint()
    {
        HP--;
    }
}
