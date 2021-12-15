using UnityEngine;
using GoogleMobileAds.Api;

/// <summary>
/// ��� ���� Ŭ����
/// </summary>
public class GoogleAds : MonoBehaviour
{
    private readonly string unitID = "ca-app-pub-1830536930925733/2116277000";
    private readonly string testID = "ca-app-pub-3940256099942544/6300978111";

    //List<string> testDeviceIds = new List<string>() { "3FA2C55314C644D8" };

    private BannerView bannerView;

    void Start()
    {
        // ���� ����� ads �ʱ�ȭ
        MobileAds.Initialize(initState => { RequestBanner(); });
    }

    private void RequestBanner()
    {
        // ���� ID �ʱ�ȭ, ����� ����� �׽�Ʈ�� ���� ID�� ����Ѵ�
        string id = testID;
        /*
        if(Debug.isDebugBuild)
            id = testID;
        else
            id = unitID;
        */

        // ����Ʈ��ʸ� ȭ�� ���� ��ġ
        bannerView = new BannerView(id, AdSize.SmartBanner, AdPosition.Bottom);

        AdRequest request = new AdRequest.Builder().Build();

        bannerView.LoadAd(request);
    }
}
