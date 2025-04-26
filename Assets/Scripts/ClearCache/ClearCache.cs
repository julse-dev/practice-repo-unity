using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using TMPro;

using System.IO;
using System.Linq;


struct SSDInfo
{
    public string objName;
    public string driveName;
    public float totalSize;
    public float freeSize;
};


public class ClearCache : MonoBehaviour
{
    public GameObject controlMain;

    //const float TestInputNum = 20;

    // Directory Information
    private string AssetBundlesDirPath = "/Users/johan/Desktop/TestDirectory/Test1";
    private string SoundTrackDirPath = "/Users/johan/Desktop/TestDirectory/Test2";
    private string VolumatricDirPath = "/Users/johan/Desktop/TestDirectory/Test3";

    // Canvas UI
    public TextMeshProUGUI AssetBundlesPathSize;
    public TextMeshProUGUI SoundTrackPathSize;
    public TextMeshProUGUI VolumetricPathSize;

    public Image AllStorageBar;
    public Image AssetBundlesStorageBar;
    public Image SoundTrackStorageBar;
    public Image VolumetricStorageBar;

    private RectTransform AllStorageBarRectTransform;
    private RectTransform AssetBundlesStorageBarRectTransform;
    private RectTransform SoundTrackStorageBarRectTransform;
    private RectTransform VolumetricStorageBarRectTransform;


    SSDInfo[] ssdInfo = new SSDInfo[1000];

    // Start is called before the first frame update
    void Start()
    {
        //StorageBar();
        //CheckStorage();

        AllStorageBarRectTransform = AllStorageBar.GetComponent<RectTransform>();
        AssetBundlesStorageBarRectTransform = AssetBundlesStorageBar.GetComponent<RectTransform>();
        SoundTrackStorageBarRectTransform = SoundTrackStorageBar.GetComponent<RectTransform>();
        VolumetricStorageBarRectTransform = VolumetricStorageBar.GetComponent<RectTransform>();


        //float AllStorageBarWidth = AllStorageBarRectTransform.sizeDelta.x;
        float BarHeight = AllStorageBarRectTransform.sizeDelta.y;

        float BarWidth = AllStorageBarRectTransform.sizeDelta.x;

        float ABsStorageBarWidth = (GetDirectorySize(AssetBundlesDirPath) / CheckStorage()) * BarWidth;
        float STStorageBarWidth = (GetDirectorySize(SoundTrackDirPath) / CheckStorage()) * BarWidth;
        float VolStorageBarWidth = (GetDirectorySize(VolumatricDirPath) / CheckStorage()) * BarWidth;



        AssetBundlesStorageBarRectTransform.sizeDelta = new Vector2(ABsStorageBarWidth, BarHeight);
        float ABsStorageBarPosX = AssetBundlesStorageBarRectTransform.sizeDelta.x;
        AssetBundlesStorageBarRectTransform.anchoredPosition = new Vector2(ABsStorageBarPosX / 2, 0);

        SoundTrackStorageBarRectTransform.sizeDelta = new Vector2(STStorageBarWidth * 2, BarHeight);
        float STStorageBarPosX = SoundTrackStorageBarRectTransform.sizeDelta.x;
        SoundTrackStorageBarRectTransform.anchoredPosition = new Vector2(ABsStorageBarPosX + (STStorageBarPosX / 2), 0);

        VolumetricStorageBarRectTransform.sizeDelta = new Vector2(VolStorageBarWidth * 4, BarHeight);
        float VolStorageBarPosX = VolumetricStorageBarRectTransform.sizeDelta.x;
        VolumetricStorageBarRectTransform.anchoredPosition = new Vector2(ABsStorageBarPosX + STStorageBarPosX + (VolStorageBarPosX / 2), 0);

    }

    public void StorageBar()
    {
        AllStorageBarRectTransform = AllStorageBar.GetComponent<RectTransform>();
        AssetBundlesStorageBarRectTransform = AssetBundlesStorageBar.GetComponent<RectTransform>();
        SoundTrackStorageBarRectTransform = SoundTrackStorageBar.GetComponent<RectTransform>();
        VolumetricStorageBarRectTransform = VolumetricStorageBar.GetComponent<RectTransform>();


        //float AllStorageBarWidth = AllStorageBarRectTransform.sizeDelta.x;
        float BarHeight = AllStorageBarRectTransform.sizeDelta.y;

        float BarWidth = AllStorageBarRectTransform.sizeDelta.x;

        float ABsStorageBarWidth = 0;
        float STStorageBarWidth = 0;
        float VolStorageBarWidth = 0;



        AssetBundlesStorageBarRectTransform.sizeDelta = new Vector2(ABsStorageBarWidth, BarHeight);
        float ABsStorageBarPosX = AssetBundlesStorageBarRectTransform.sizeDelta.x;
        AssetBundlesStorageBarRectTransform.anchoredPosition = new Vector2(ABsStorageBarPosX / 2, 0);

        SoundTrackStorageBarRectTransform.sizeDelta = new Vector2(STStorageBarWidth * 2, BarHeight);
        float STStorageBarPosX = SoundTrackStorageBarRectTransform.sizeDelta.x;
        SoundTrackStorageBarRectTransform.anchoredPosition = new Vector2(ABsStorageBarPosX + (STStorageBarPosX / 2), 0);

        VolumetricStorageBarRectTransform.sizeDelta = new Vector2(VolStorageBarWidth * 4, BarHeight);
        float VolStorageBarPosX = VolumetricStorageBarRectTransform.sizeDelta.x;
        VolumetricStorageBarRectTransform.anchoredPosition = new Vector2(ABsStorageBarPosX + STStorageBarPosX + (VolStorageBarPosX / 2), 0);






    }

    private float GetDirectorySize(string DirectoryPath)
    {
        float DirectorySize = 0;
        DirectoryInfo dirInfo = new DirectoryInfo(DirectoryPath);

        foreach (FileInfo fileInfo in dirInfo.GetFiles("*", SearchOption.AllDirectories))
        {
            DirectorySize += fileInfo.Length;
        }

        return DirectorySize;
    }

    public long FormatBytes(long bytes)
    {
        Debug.Log("Change Format");
        int Scale = 1024;

        bytes = bytes / Scale;

        return bytes;
    }

    public void ClearAssetBundles()
    {
        // Load AssetBundles List
        string ABsStr = "ab_*";
        string[] ABsStrFiles = Directory.GetFiles(AssetBundlesDirPath, ABsStr);
        int AbsStrFilesLength = ABsStrFiles.Length;

        var ABsStrFilesList = ABsStrFiles.ToList();

        // Check MainStillcuts and ...
        string[] ABsStillStr = { "/Users/johan/Desktop/TestDirectory/ab_Test_Clear", "/Users/johan/Desktop/TestDirectory/ab_Test_Clear 2" };
        int ABsStillStrLength = ABsStillStr.Length;

        foreach (string i in ABsStrFilesList)
        {
            Debug.Log(i);
        }

        for (int i = 0; i < AbsStrFilesLength; i++)
        {
            for (int j = 0; j < ABsStillStrLength; j++)
            {
                if (ABsStrFilesList[i] == ABsStillStr[j])
                {
                    ABsStrFilesList.RemoveAt(i);
                    AbsStrFilesLength--;
                }
                //Debug.Log(ABsStrFilesList[i]);
                //Debug.Log(ABsStillStr[j]);
            }
        }

        ABsStrFiles = ABsStrFilesList.ToArray();

        foreach (string f in ABsStrFiles)
        {
            File.Delete(f);
        }

        Debug.Log("Clear AssetBundles");

        GetDirectorySize(AssetBundlesDirPath);

    }

    public void ClearSoundTracks()
    {
        string SoundStr = "*.mp3";

        string[] SoundStrFiles = Directory.GetFiles(SoundTrackDirPath, SoundStr);

        foreach (string f in SoundStrFiles)
        {
            File.Delete(f);
        }

        Debug.Log("Clear SoundTracks");

        GetDirectorySize(SoundTrackDirPath);

    }

    public void ClearVolumetric()
    {
        string VolStr = "ab_v*";
        string XRStr = "ab_xr*";

        string[] VolStrFiles = Directory.GetFiles(VolumatricDirPath, VolStr);
        string[] XRStrFiles = Directory.GetFiles(VolumatricDirPath, XRStr);

        foreach (string f in VolStrFiles)
        {
            File.Delete(f);
        }

        foreach (string f in XRStrFiles)
        {
            File.Delete(f);
        }

        Debug.Log("Clear Volumetric");

        GetDirectorySize(VolumatricDirPath);

    }

    public float CheckStorage()
    {
        DriveInfo[] allDrives = DriveInfo.GetDrives();

        foreach (DriveInfo d in allDrives)
        {
            string[] words = d.Name.Split('/');

            // ========================== Check SSD ==========================
            for (int i = 0; i < 1; i++)
            {
                if (words[words.Length - 1] == "Data")
                {
                    if (d.IsReady == true)
                    {
                        //ssdInfo[i].driveName = words[words.Length - 1];
                        ssdInfo[i].totalSize = d.TotalSize;
                        //ssdInfo[i].freeSize = d.TotalFreeSpace;

                        //double TotalSize = (double) ssdInfo[i].totalSize;
                        //double freesize = (double) d.TotalFreeSpace;

                        return ssdInfo[i].totalSize;
                    }
                }
            }
        }

        return -1;

    }
}
