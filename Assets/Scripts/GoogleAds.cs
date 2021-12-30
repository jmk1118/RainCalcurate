using System;
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
        MobileAds.Initialize(initState => { });

        this.RequestBanner();
    }

    private void RequestBanner()
    {
        // ���� ID �ʱ�ȭ, ����� ����� �׽�Ʈ�� ���� ID�� ����Ѵ�
        string id;
        if (Debug.isDebugBuild) 
            id = testID;
        else
            id = unitID;

        AdSize adaptiveSize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);

        // ����Ʈ��ʸ� ȭ�� ���� ��ġ
        this.bannerView = new BannerView(id, adaptiveSize, AdPosition.Bottom);

        // ����ִ� ���� ���� ������Ʈ
        AdRequest request = new AdRequest.Builder().Build();

        // ������Ʈ�� �Բ� ��ʸ� �ε�
        this.bannerView.LoadAd(request);
    }

    public void OnBanner()
    {
        bannerView.Show();
    }

    public void OffBanner()
    {
        bannerView.Hide();
    }
}
