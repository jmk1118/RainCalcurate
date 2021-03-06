using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class BackButton : MonoBehaviour
{
    [SerializeField] Text numberScreen; // 버튼을 누르면 숫자가 출력될 Text상자
    StringBuilder screenText;
    Button button;

    /// <summary>
    /// screenText 초기화
    /// 버튼 오브젝트에 ButtonClick 메소드 연결
    /// </summary>
    private void Awake()
    {
        screenText = new StringBuilder();
        button = this.GetComponent<Button>();
    }

    private void Start()
    {
        button.onClick.AddListener(ButtonClick);
    }

    /// <summary>
    /// 버튼을 클릭하면 실행되는 함수
    /// 입력된 숫자로 정답 처리를 한다
    /// 입력된 숫자를 제거한다
    /// </summary>
    public void ButtonClick()
    {
        if (numberScreen.text.Length <= 0)
            return;

        screenText.Clear();
        screenText.Append(numberScreen.text);
        screenText.Remove(screenText.Length - 1, 1);
        numberScreen.text = screenText.ToString();
    }
}
