using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject startPanel; // ���� ȭ��
    [SerializeField] GameObject mainPanel; // ���� ȭ��
    [SerializeField] GameObject pausePanel; // �Ͻ����� ȭ��
    [SerializeField] GameObject gameOverPanel; // ���ӿ��� ȭ��
    bool isPause; // �Ͻ����� ����

    private void Awake()
    {
        isPause = false; // �Ͻ��������� ���� ���·� �ʱ�ȭ
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(mainPanel.activeSelf)
            {
                if(!isPause)
                {
                    isPause = true;
                    Time.timeScale = 0;
                    pausePanel.SetActive(true);
                }
            }
        }
    }

    public void StartGame()
    {
        mainPanel.SetActive(true);
        startPanel.SetActive(false);
    }

    public void EndGame()
    {
        Application.Quit();
    }

    // �Ͻ����� UI���� YES�� �����ϸ� ����ȭ������ ���ư��� �޼ҵ�
    public void PauseYes()
    {
        startPanel.SetActive(true);
        mainPanel.SetActive(false);
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        isPause = false;
    }

    // �Ͻ����� UI���� NO�� �����ϸ� ������ �ٽ� �����ϴ� �޼ҵ�
    public void PauseNo()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        isPause = false;
    }

    // ü���� 0�� �Ǹ� ȣ��Ǵ� �޼ҵ�
    // ���� ���� �г��� Ȱ��ȭ�ϰ� ������ ������Ų��
    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
        isPause = true;
    }

    // ���� ���� �гο��� ������� ������ ����Ǵ� �޼ҵ�
    public void ReStart()
    {
        mainPanel.SetActive(false);
        mainPanel.SetActive(true);

        // ���� ���� ȭ���� ���� �������� �ǵ�����
        gameOverPanel.SetActive(false);
        Time.timeScale = 1;
        isPause = false;
    }

    // ���� ���� �гο��� ��������� �ʱ⸦ ������ ����Ǵ� �޼ҵ�
    public void BackStartPanel()
    {
        startPanel.SetActive(true);
        mainPanel.SetActive(false);

        // ���� ���� ȭ���� ���� �������� �ǵ�����
        gameOverPanel.SetActive(false);
        Time.timeScale = 1;
        isPause = false;
    }
}
