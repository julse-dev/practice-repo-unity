using UnityEngine;
using System.IO;

public class LoadFromFileAssetBundle : MonoBehaviour
{
    void Awake()
    {
        // Load AssetBundle Path
        var myLoadedAssetBundle =
            AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath,
            "/Users/minjisu/Documents/GitHub/Unity_Practice_Repository/My project/Assets/Test_AssetBundle/ab_ui_practice"));


        // Check Load AssetBundle
        if (myLoadedAssetBundle == null)
        {
            Debug.Log("Failed to load AssetBundle!");

            return;
        }

        // Load AssetBundle
        Debug.Log(myLoadedAssetBundle);
        var prefab = myLoadedAssetBundle.LoadAsset<GameObject>("UI_Practice.unity");  // 불러올 에셋(프레펩)의 정확한 명칭
        Instantiate(prefab);

    }


}
