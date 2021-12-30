using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerMain : MonoBehaviour
{
    [SerializeField] GameObject GameManager; // ���� �Ŵ��� ������Ʈ
    [SerializeField] Text timeText; // ���� �ð��� ǥ���ϴ� �ؽ�Ʈ
    [SerializeField] Text pointText;
    int nowtime;
    int nowpoint;
    Coroutine timePlay;

    private void OnEnable()
    {
        nowtime = 180;
        nowpoint = 0;
        timePlay = StartCoroutine("TimePlay");
        timeText.text = "���� �ð� : 0�� 0��";
        pointText.text = "���� : 0��";
    }

    private void OnDisable()
    {
        StopCoroutine(timePlay); // �ð��� ��� �ڷ�ƾ ����
    }

    // �ð��� ��� �ڷ�ƾ
    IEnumerator TimePlay()
    {
        while (nowtime > 0)
        {
            timeText.text = "���� �ð� : " + (nowtime / 60).ToString() + "�� " + (nowtime % 60).ToString() + "��";
            nowtime--;
            yield return new WaitForSeconds(1.0f);
        }

        GameManager.GetComponent<GameManager>().GameOver(nowpoint, true);
        yield break;
    }

    public void GetPoint(int point)
    {
        nowpoint += point;
        pointText.text = "���� : " + nowpoint.ToString() + "��";
    }

    public void LosePoint()
    {
        GameManager.GetComponent<GameManager>().GameOver(nowpoint, false);
    }
}