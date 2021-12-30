using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerMain : MonoBehaviour
{
    [SerializeField] GameObject GameManager; // 게임 매니저 오브젝트
    [SerializeField] Text timeText; // 남은 시간을 표시하는 텍스트
    [SerializeField] Text pointText;
    int nowtime;
    int nowpoint;
    Coroutine timePlay;

    private void OnEnable()
    {
        nowtime = 180;
        nowpoint = 0;
        timePlay = StartCoroutine("TimePlay");
        timeText.text = "남은 시간 : 0분 0초";
        pointText.text = "점수 : 0점";
    }

    private void OnDisable()
    {
        StopCoroutine(timePlay); // 시간을 재는 코루틴 종료
    }

    // 시간을 재는 코루틴
    IEnumerator TimePlay()
    {
        while (nowtime > 0)
        {
            timeText.text = "남은 시간 : " + (nowtime / 60).ToString() + "분 " + (nowtime % 60).ToString() + "초";
            nowtime--;
            yield return new WaitForSeconds(1.0f);
        }

        GameManager.GetComponent<GameManager>().GameOver(nowpoint, true);
        yield break;
    }

    public void GetPoint(int point)
    {
        nowpoint += point;
        pointText.text = "점수 : " + nowpoint.ToString() + "점";
    }

    public void LosePoint()
    {
        GameManager.GetComponent<GameManager>().GameOver(nowpoint, false);
    }
}