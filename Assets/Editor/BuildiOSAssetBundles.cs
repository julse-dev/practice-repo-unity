using UnityEngine;
using UnityEditor;
using System.IO;

public class BuildiOSAssetBundles : Editor
{
    [MenuItem("Bundle/Build iOS AssetBundle")]
    static void BuildAssetBundles()
    {
        string assetBundleDirectory = "Assets/AssetBundles_ios";
        if (!Directory.Exists(assetBundleDirectory))
        {
            Directory.CreateDirectory(assetBundleDirectory);
        }

        BuildAssetBundleOptions options = BuildAssetBundleOptions.None;

        bool shouldCheckOdr = EditorUserBuildSettings.activeBuildTarget == BuildTarget.iOS;

#if UNITY_TVOS
            shouldCheckOdr |= EditorUserBuildSettings.activeBuildTarget == BuildTarget.tvOS;
#endif

        if (shouldCheckOdr)
        {
#if ENABLE_IOS_ON_DEMAND_RESOURCES
            if (PlayerSettings.iOS.useOnDemandResources)
                options |= BuildAssetBundleOptions.None;
#endif

#if ENABLE_IOS_APP_SLICING
            options |= BuildAssetBundleOptions.None;
#endif
        }

        BuildPipeline.BuildAssetBundles(assetBundleDirectory, options, EditorUserBuildSettings.activeBuildTarget);
    }


}
