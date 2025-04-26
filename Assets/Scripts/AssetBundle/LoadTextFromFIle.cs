using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadTextFromFIle : MonoBehaviour
{
    string dataPath = Application.persistentDataPath;
    

    // Start is called before the first frame update
    void Start()
    {
        //Load a text file (Persistent DataPath)
        var versionText = Resources.Load<TextAsset>(dataPath + "hubVersion.txt");
        Debug.Log(versionText);

        if (versionText)
        {
            
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
