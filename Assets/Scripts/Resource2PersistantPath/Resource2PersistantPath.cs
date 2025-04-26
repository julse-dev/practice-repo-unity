using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;


public class Resource2PersistantPath : MonoBehaviour
{
    // Debug를 위한 Text UI
    public Text Text_Debug;

    [SerializeField] private string GlbFileName = "T3_glb.glb";     // 대상 파일이름
    [SerializeField] private string StreamingAssetsDir = "";        // Unity Project 내부의 Streaming Assets Path, 여기서 날 것의 형태의 파일을 복사가 가능하다.
    [SerializeField] private string DevicePath = "";                // Device의 Persistent Data Path를 입력받을 곳
    [SerializeField] private string DeviceFinalPath = "";           // 복사될 최종 경로

    private void Awake()
    {
        StreamingAssetsDir = Application.streamingAssetsPath;
        DevicePath = Application.persistentDataPath;
        DeviceFinalPath = DevicePath + "/" + "Resource";
    }

    private void Start()
    {
        bool isDirCheck = Directory.Exists(DeviceFinalPath);
        if (!isDirCheck)
        {
            Directory.CreateDirectory(DeviceFinalPath);
        }
    }

    public void Copy2Device()
    {
        bool isGlbFileExist = File.Exists(DeviceFinalPath + "/" + GlbFileName);
        if (!isGlbFileExist)
        {
            File.Copy(StreamingAssetsDir + "/" + GlbFileName, DeviceFinalPath + "/" + GlbFileName);
            Text_Debug.text = "glb file copy Success";
        }
        else
        {
            Text_Debug.text = "glb file copy already";
        }

        
    }

    public void CheckFile()
    {
        string CheckFilePath = DeviceFinalPath + "/" + GlbFileName;
        bool isFileExist = File.Exists(CheckFilePath);

        if (isFileExist == true)
        {
            Text_Debug.text = "glb file is Exist";
        }
        else
        {
            Text_Debug.text = "glb file is Not Exist";
        }


    }
    
}
