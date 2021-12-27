using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject startPanel; // ���� ȭ��
    [SerializeField] GameObject mainPanel; // ���� ȭ��
    [SerializeField] GameObject optionPanel; // �ɼ� ȭ��
    [SerializeField] GameObject pointCheckPanel; // ���� Ȯ�� ȭ��
    [SerializeField] GameObject pausePanel; // �Ͻ����� ȭ��
    [SerializeField] GameObject gameOverPanel; // ���ӿ��� ȭ��
    bool isPause; // �Ͻ����� ����
    string logPath; // �ְ� ������ ����Ǵ� ��Ʈ ���� ��ġ
    Stream fileStream; // ���Ͻ�Ʈ��
    byte[] fileByte = new byte[4]; // ���Ͽ� �ְ������� �а������� ����Ʈ �迭
    int maxPointPlus; // ���� �ְ�����
    int maxPointMinus; // ���� �ְ�����
    int maxPointX; // ���� �ְ�����
    int maxPointDiv; // ������ �ְ����� 
    int maxPointAll; // ���� �ְ�����

    int option; // ������ ���� ����
    public bool Plus { get; set; } // ���� ���� �Ҹ���
    public bool Minus { get; set; } // ���� ���� �Ҹ���
    public bool Multipication { get; set; } // ���� ���� �Ҹ���
    public bool Division { get; set; } // ������ ���� �Ҹ���

    private void Awake()
    {
        isPause = false; // �Ͻ��������� ���� ���·� �ʱ�ȭ

        SetResolution(); // �ػ� �缳��

        // ��� ��Ģ������ �����ϴ� ���·� �ʱ�ȭ
        Plus = true;
        Minus = true;
        Multipication = true;
        Division = true;
        option = 5;

        // ����� �ְ��� �α׿��� �ְ������� �޾ƿ´�
        logPath = Application.persistentDataPath + "log.txt";
        fileStream = new FileStream(logPath, FileMode.OpenOrCreate);

        // �� ������ �ְ� ������ �α׿��� �޾ƿ� �ְ� ���� â �ؽ�Ʈ�� �����Ѵ�
        fileStream.Read(fileByte, 0, fileByte.Length);
        maxPointPlus = BitConverter.ToInt32(fileByte, 0);
        pointCheckPanel.transform.GetChild(1).GetComponent<Text>().text = maxPointPlus.ToString() + "��";

        fileStream.Read(fileByte, 0, fileByte.Length);
        maxPointMinus = BitConverter.ToInt32(fileByte, 0);
        pointCheckPanel.transform.GetChild(2).GetComponent<Text>().text = maxPointMinus.ToString() + "��";

        fileStream.Read(fileByte, 0, fileByte.Length);
        maxPointX = BitConverter.ToInt32(fileByte, 0);
        pointCheckPanel.transform.GetChild(3).GetComponent<Text>().text = maxPointX.ToString() + "��";

        fileStream.Read(fileByte, 0, fileByte.Length);
        maxPointDiv = BitConverter.ToInt32(fileByte, 0);
        pointCheckPanel.transform.GetChild(4).GetComponent<Text>().text = maxPointDiv.ToString() + "��";

        fileStream.Read(fileByte, 0, fileByte.Length);
        maxPointAll = BitConverter.ToInt32(fileByte, 0);
        pointCheckPanel.transform.GetChild(5).GetComponent<Text>().text = maxPointAll.ToString() + "��";

        fileStream.Close();
    }

    void Update()
    {
        // �ڷΰ��� ��ư�� ������ �Ͻ������Ѵ�
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

    // �ػ󵵸� �缳���ϴ� �޼ҵ�
    public void SetResolution()
    {
        Camera camera = Camera.main;
        Rect rect = camera.rect;

        float scaleheight = ((float)Screen.width / Screen.height) / ((float)9 / 16); // 9:16�� ���Ͽ� ���̰� �� ������ 1���� ũ��
        float scalewidth = 1f / scaleheight; // 9:16�� ���Ͽ� ���̰� �� ������ 1���� ũ��

        if(scaleheight < 1) // ���̰� 9:16�� ���� ���� ���
        {
            rect.height = scaleheight; // ���̴� ����
            rect.y = (1f - scaleheight) / 2f; // �¿�� �þ ������ŭ �ڸ�
        }
        else // ���̰� 9:16�� ���� ���� ���
        {
            rect.width = scalewidth; // �ʺ�� ����
            rect.x = (1f - scalewidth) / 2f; // ���Ʒ��� �þ ������ŭ �ڸ�
        }

        camera.rect = rect;
    }

    // ���� ���� ��ư �޼ҵ�
    public void StartGame()
    {
        mainPanel.SetActive(true);
        startPanel.SetActive(false);
    }

    // ���� ���� ��ư �޼ҵ�
    public void OptionGame()
    {
        optionPanel.SetActive(true);
    }

    // ���� ���� �ɼ� �޼ҵ�
    public void PlusOption(bool check)
    {
        if(check)
        {
            Plus = true;
            Minus = false;
            Multipication = false;
            Division = false;
            option = 1;
        }
    }

    // ���� ���� �ɼ� �޼ҵ�
    public void MinusOption(bool check)
    {
        if (check)
        {
            Plus = false;
            Minus = true;
            Multipication = false;
            Division = false;
            option = 2;
        }
    }

    // ���� ���� �ɼ� �޼ҵ�
    public void MultiOption(bool check)
    {
        if (check)
        {
            Plus = false;
            Minus = false;
            Multipication = true;
            Division = false;
            option = 3;
        }
    }

    // ������ ���� �ɼ� �޼ҵ�
    public void DivisionOption(bool check)
    {
        if (check)
        {
            Plus = false;
            Minus = false;
            Multipication = false;
            Division = true;
            option = 4;
        }
    }

    // ���� ���� �ɼ� �޼ҵ�
    public void AllOption(bool check)
    {
        if(check)
        {
            Plus = true;
            Minus = true;
            Multipication = true;
            Division = true;
            option = 5;
        }
    }

    // �ɼ� â�� �ݴ� �޼ҵ�
    public void OptionClose()
    {
        optionPanel.SetActive(false);
    }

    // ���� Ȯ�� â�� ���� �޼ҵ�
    public void PointCheckOpen()
    {
        pointCheckPanel.SetActive(true);
    }

    // ���� Ȯ�� â�� �ݴ� �޼ҵ�
    public void PointCheckClose()
    {
        pointCheckPanel.SetActive(false);
    }

    // ���� ���� ��ư �޼ҵ�
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
    public void GameOver(int point)
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
        isPause = true;

        // �ְ� ������ �����ϸ� �α� ���Ͽ� �ְ������� �����ϰ�, maxPoint�� �ְ������� �����Ѵ�
        if (option == 1 && point > maxPointPlus)
        {
            fileStream = new FileStream(logPath, FileMode.Open);
            fileByte = BitConverter.GetBytes(point);
            fileStream.Seek(0, SeekOrigin.Begin);
            fileStream.Write(fileByte, 0, fileByte.Length);
            fileStream.Close();

            maxPointPlus = point;
            pointCheckPanel.transform.GetChild(1).GetComponent<Text>().text = maxPointPlus.ToString() + "��";
        }
        if(option == 2 && point > maxPointMinus)
        {
            fileStream = new FileStream(logPath, FileMode.Open);
            fileByte = BitConverter.GetBytes(point);
            fileStream.Seek(4, SeekOrigin.Begin);
            fileStream.Write(fileByte, 0, fileByte.Length);
            fileStream.Close();

            maxPointMinus = point;
            pointCheckPanel.transform.GetChild(2).GetComponent<Text>().text = maxPointMinus.ToString() + "��";
        }
        if (option == 3 && point > maxPointX)
        {
            fileStream = new FileStream(logPath, FileMode.Open);
            fileByte = BitConverter.GetBytes(point);
            fileStream.Seek(8, SeekOrigin.Begin);
            fileStream.Write(fileByte, 0, fileByte.Length);
            fileStream.Close();

            maxPointX = point;
            pointCheckPanel.transform.GetChild(3).GetComponent<Text>().text = maxPointX.ToString() + "��";
        }
        if (option == 4 && point > maxPointDiv)
        {
            fileStream = new FileStream(logPath, FileMode.Open);
            fileByte = BitConverter.GetBytes(point);
            fileStream.Seek(12, SeekOrigin.Begin);
            fileStream.Write(fileByte, 0, fileByte.Length);
            fileStream.Close();

            maxPointDiv = point;
            pointCheckPanel.transform.GetChild(4).GetComponent<Text>().text = maxPointDiv.ToString() + "��";
        }
        if (option == 5 && point > maxPointAll)
        {
            fileStream = new FileStream(logPath, FileMode.Open);
            fileByte = BitConverter.GetBytes(point);
            fileStream.Seek(16, SeekOrigin.Begin);
            fileStream.Write(fileByte, 0, fileByte.Length);
            fileStream.Close();

            maxPointAll = point;
            pointCheckPanel.transform.GetChild(5).GetComponent<Text>().text = maxPointAll.ToString() + "��";
        }
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
