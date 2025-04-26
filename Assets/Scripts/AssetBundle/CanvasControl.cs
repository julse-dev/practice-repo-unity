using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasControl : MonoBehaviour
{
    public GameObject Bundle;
    public GameObject Canvas_Destroy;
        
    public void UnloadAssetBundle()
    {
        Destroy(Bundle);
        AssetBundle.UnloadAllAssetBundles(Bundle);
        Debug.Log("Unload AssetBundle");
        SceneManager.LoadScene("TestUI");
    }
        
}
