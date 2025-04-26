#if false
using System;
using System.Collections;

using UnityEngine;

using Amazon;
using Amazon.Runtime;
using Amazon.CognitoIdentity;

using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;

#if UNITY_IOS
using Unity.Notifications.iOS;
#endif

#if UNITY_ANDROID
using Firebase;
using Firebase.Messaging;
using Unity.Notifications.Android;
#endif


public class SetMobileNotification : MonoBehaviour
{
    // sns identity Pool ID
    public string IdentityPoolId = "";

    //sns android platform arn
    public string AndroidPlatformApplicationArn = "";

    //sns ios platform arn
    public string iOSPlatformApplicationArn = "";

    // sns topic arn
    public string TopicArn = "";

    // sns cognito Region
    public string CognitoIdentityRegion = RegionEndpoint.APNortheast2.SystemName;

    private RegionEndpoint _CognitoIdentityRegion
    {
        get { return RegionEndpoint.GetBySystemName(CognitoIdentityRegion); }
    }

    public string SNSRegion = RegionEndpoint.APNortheast2.SystemName;

    private RegionEndpoint _SNSRegion
    {
        get { return RegionEndpoint.GetBySystemName(SNSRegion); }
    }

    private string _endpointArn;

    private AWSCredentials _credentials;

    private AWSCredentials Credentials
    {
        get
        {
            if (_credentials == null)
                _credentials = new CognitoAWSCredentials(IdentityPoolId, _CognitoIdentityRegion);
            return _credentials;
        }

    }

    private IAmazonSimpleNotificationService _snsClient;

    private IAmazonSimpleNotificationService SnsClient
    {
        get
        {
            if (_snsClient == null)
                _snsClient = new AmazonSimpleNotificationServiceClient(Credentials, _SNSRegion);
            return _snsClient;
        }
    }

#if UNITY_ANDROID
    FirebaseApp app;

    public void Awake()
    {
        DontDestroyOnLoad(this);
    }
#endif

    public void Start()
    {
#if UNITY_ANDROID
        GetAndroidDeviceToken();

#elif UNITY_IOS
        ResetAppBadge();
        GetiOSDeviceToken();
#endif
    }

#if UNITY_ANDROID
    public void GetAndroidDeviceToken()
    {
        FirebaseMessaging.TokenReceived += OnTokenReceived;
        FirebaseMessaging.MessageReceived += OnMessageReceived;

        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                app = Firebase.FirebaseApp.DefaultInstance;

                // Set a flag here to indicate whether Firebase is ready to use by your app.
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }

    public void OnTokenReceived(object sender, TokenReceivedEventArgs token)
    {
        Debug.Log("Received Registration Token: " + token.Token);
        string tokenStr = token.Token.ToString();
        RegisterAndroidDevice(tokenStr);
    }

    public void OnMessageReceived(object sender, MessageReceivedEventArgs e)
    {
        Debug.Log("Received a new message from: " + e.Message.From);
    }

    public async void RegisterAndroidDevice(string token)
    {   
        string regId = token;

        Debug.Log("RegisterDevice Token is :" + regId);
        Debug.Log("Arn is :" + AndroidPlatformApplicationArn);

        CreatePlatformEndpointResponse response;
        try
        {
            Debug.Log("response Try");
            response = await SnsClient.CreatePlatformEndpointAsync(
                       new CreatePlatformEndpointRequest
                       {
                           Token = regId,
                           PlatformApplicationArn = AndroidPlatformApplicationArn
                       });
        }
        catch (Exception e)
        {
            // Handle exception
            Debug.Log("## response Fail: " + e);
            return;
        }

        if (response != null)
        {
            _endpointArn = response.EndpointArn;
            Debug.Log("Endpoint Arn is " + _endpointArn);
            SubscribeTopic(TopicArn, _endpointArn);
        }
       
    }

#elif UNITY_IOS
    private async void GetiOSDeviceToken()
    {
        // Approval iOS Notification Request
        RequestAuthorization();
             
        var token = UnityEngine.iOS.NotificationServices.deviceToken;
        var error = UnityEngine.iOS.NotificationServices.registrationError;

        if (!string.IsNullOrEmpty(error))
        {
            CancelInvoke("CheckForDeviceToken");
            Debug.Log(@"Cancel polling");
            return;
        }

        if (token != null)
        {
            string deviceToken = System.BitConverter.ToString(token).Replace("-", "");

            Debug.Log("Your device token is = " + deviceToken);

            CreatePlatformEndpointResponse response;
            try
            {
                response = await SnsClient.CreatePlatformEndpointAsync(
                           new CreatePlatformEndpointRequest
                           {
                               //CustomUserData = null,
                               Token = deviceToken,
                               PlatformApplicationArn = iOSPlatformApplicationArn
                           });
            }
            catch (Exception e)
            {
                // Handle exception
                Debug.Log("## response Fail: " + e);
                return;
            }

            if (response != null)
            {
                _endpointArn = response.EndpointArn;
                Debug.Log("Platform endpoint arn is = " + _endpointArn);
                SubscribeTopic(TopicArn, _endpointArn);
            }
            
        }
    }
   
    // Approval iOS Notification Request
    IEnumerator RequestAuthorization()
    {
        var authorizationOption = AuthorizationOption.Alert | AuthorizationOption.Badge | AuthorizationOption.Sound;
        using (var req = new AuthorizationRequest(authorizationOption, true))
        {
            while (!req.IsFinished)
            {
                yield return null;
            };            

            string res = "\n RequestAuthorization:"; 
            res += "\n finished: " + req.IsFinished;
            res += "\n granted :  " + req.Granted;
            res += "\n error:  " + req.Error;
            res += "\n deviceToken:  " + req.DeviceToken;
            Debug.Log(res);

            //if (req.Granted && req.DeviceToken is not null)
            //{
            //    string DeviceToken = req.DeviceToken;
            //    string DeviceError = req.Error;
            //}
        }

    }

    public void ResetAppBadge()
    {
        Debug.Log("Reset Badge");
        iOSNotificationCenter.ApplicationBadge = 0;
        iOSNotificationCenter.RemoveAllDeliveredNotifications();
    }
#endif

    public async void SubscribeTopic(string TopicArn, string ApplicationEndpoint)
    {
        string topicArn = TopicArn;
        string endpoint = ApplicationEndpoint;
        string protocol = "application";

        Debug.Log("Create Subscribe");

        SubscribeResponse response;
        try
        {
            Debug.Log("Subsciribe response Try");
            response = await SnsClient.SubscribeAsync(
                new SubscribeRequest
                {
                    Protocol = protocol,
                    Endpoint = endpoint,
                    TopicArn = topicArn,
                    ReturnSubscriptionArn = true
                });
        }
        catch(Exception e)
        {
            // Handle exception
            Debug.Log("## response Fail: " + e);
            return;
        }    

        if (response != null)
        {
            string subscriptionArn = response.SubscriptionArn;
            Debug.Log($"Subscribed to the topic {topicArn}.");
            Debug.Log($"Subscription ARN: {subscriptionArn}");
        }
        
    }

}
#endif