using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UpdateTime : MonoBehaviour
{
    public Text TimeText;
    DateTime TimeNow;
    String TimeGO = "2022-09-01";

    void FixedUpdate()
    {
        //TimeUpdate();
        TimeMinusUpdate();
    }

    public void TimeUpdate()
    {
        TimeNow = DateTime.Now;
        string TimeNowStr = TimeNow.ToString();
        TimeText.text = "This Time is : " + TimeNowStr;
    }

    public void TimeMinusUpdate()
    {
        DateTime TimeGoal = DateTime.ParseExact(TimeGO, "yyyy-MM-dd", null);
        TimeNow = DateTime.Now;

        TimeSpan TimeReturn = TimeGoal - TimeNow;
        //TimeText.text = "남은 시간 : " + TimeReturn.ToString();
        TimeText.text = string.Format("남은 시간 : {0:dd} Days {0:hh} Hours {0:mm} Minutes {0:ss} Seconds", TimeReturn);
    }

}
