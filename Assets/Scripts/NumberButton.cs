using UnityEngine;
using UnityEngine.UI;
using System.Text;

/// <summary>
/// 숫자 입력 버튼용 클래스
/// </summary>
public class NumberButton : MonoBehaviour
{
    [SerializeField] int buttonNumber; // 이 버튼에 배당된 숫자
    [SerializeField] Text numberScreen; // 버튼을 누르면 숫자가 출력될 Text상자
    StringBuilder screenText; // 저장용 stringbuilder
    Button button; // 이 객체에 할당된 버튼

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
    /// 기존 텍스트에 숫자를 추가한다
    /// </summary>
    public void ButtonClick()
    {
        screenText.Clear();
        screenText.Append(numberScreen.text);
        if(screenText.Length < 9) // 10 이상은 int형의 범위를 넘어간다
        {
            screenText.Append(buttonNumber);
            numberScreen.text = screenText.ToString();
        }
        else
        {
            // 에러 처리
        }
    }
}
