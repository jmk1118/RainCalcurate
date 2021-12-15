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
        MobileAds.Initialize(initState => { RequestBanner(); });
    }

    private void RequestBanner()
    {
        // 광고 ID 초기화, 디버깅 빌드면 테스트용 광고 ID를 사용한다
        string id = testID;
        /*
        if(Debug.isDebugBuild)
            id = testID;
        else
            id = unitID;
        */

        // 스마트배너를 화면 위에 설치
        bannerView = new BannerView(id, AdSize.SmartBanner, AdPosition.Bottom);

        AdRequest request = new AdRequest.Builder().Build();

        bannerView.LoadAd(request);
    }
}
