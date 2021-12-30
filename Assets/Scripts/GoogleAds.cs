using System;
using UnityEngine;
using GoogleMobileAds.Api;

/// <summary>
/// 배너 광고 클래스
/// </summary>
public class GoogleAds : MonoBehaviour
{
    private readonly string unitID = "ca-app-pub-1830536930925733/2116277000";
    private readonly string testID = "ca-app-pub-3940256099942544/6300978111";

    //List<string> testDeviceIds = new List<string>() { "3FA2C55314C644D8" };

    private BannerView bannerView;

    void Start()
    {
        // 구글 모바일 ads 초기화
        MobileAds.Initialize(initState => { });

        this.RequestBanner();
    }

    private void RequestBanner()
    {
        // 광고 ID 초기화, 디버깅 빌드면 테스트용 광고 ID를 사용한다
        string id;
        if (Debug.isDebugBuild) 
            id = testID;
        else
            id = unitID;

        AdSize adaptiveSize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);

        // 스마트배너를 화면 위에 설치
        this.bannerView = new BannerView(id, adaptiveSize, AdPosition.Bottom);

        // 비어있는 광고 생성 리퀘스트
        AdRequest request = new AdRequest.Builder().Build();

        // 리퀘스트와 함께 배너를 로드
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
