#if UNITY_IOS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.Notifications.iOS;

public class GetToken : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RequestAuthorization();
    }    

    IEnumerator RequestAuthorization()
    {
        var authorizationOption = AuthorizationOption.Alert | AuthorizationOption.Badge | AuthorizationOption.Sound;
        using (var req = new AuthorizationRequest(authorizationOption, true))
        {
            //while (!req.IsFinished)
            //{
            //    yield return null;
            //};

            string res = "\n RequestAuthorization:";
            res += "\n finished: " + req.IsFinished;
            res += "\n granted :  " + req.Granted;
            res += "\n error:  " + req.Error;
            res += "\n deviceToken:  " + req.DeviceToken;
            Debug.Log(res);

            string DeviceToken = req.DeviceToken;

            TokenReceive(DeviceToken);

            yield return this;
        }

    }

    public void TokenReceive(string deviceToken)
    {
        Debug.Log("iOS deviceToken is " + deviceToken);
    }

}

#endif