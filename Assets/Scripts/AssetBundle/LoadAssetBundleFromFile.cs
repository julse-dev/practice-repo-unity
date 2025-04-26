using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadAssetBundleFromFile : MonoBehaviour
{
    //public List<string> nameList;

    public List<AssetBundle> assetBundles;
    private Shader defaultShader;

    // Start is called before the first frame update
    void Start()
    {
        defaultShader = Shader.Find("Universal Render Pipeline/Lit");
        assetBundles = new List<AssetBundle>();

        for (int i = 0; i < 1; i++)
        {
            AssetBundle bundle = AssetBundle.LoadFromFile(Path.Combine("/Users/johan/Desktop/testproject/My project/Assets/TestBundle/ab_ptc_xjcomets"));
            //AssetBundle bundle = assetBundles[i];
            Debug.Log(bundle);
            if (bundle == null)
            {
                Debug.Log("Failed to load AssetBundle!");
                return;
            }

            var prefab = bundle.LoadAsset<GameObject>("textured_model");

            Instantiate(prefab);

            for(int j = 0; j < prefab.transform.GetChild(0).GetComponent<Renderer>().sharedMaterials.Length; j++)
            {
                prefab.transform.GetChild(0).GetComponent<Renderer>().sharedMaterials[j].shader = null;
                prefab.transform.GetChild(0).GetComponent<Renderer>().sharedMaterials[j].shader = defaultShader;
                prefab.transform.GetChild(0).GetComponent<Renderer>().sharedMaterials[j].SetFloat("_Smoothness", 0.1f);
            }

            Debug.Log("hello");
            //prefab.transform.GetChild(0).GetComponent<Renderer>().sharedMaterial.shader = defaultShader;
        }
    }

    public void testLoad()
    {
        var load_object = Resources.Load("/Users/johan/Desktop/testproject/My project/Assets/TestBundle/ab_mat_day1", typeof(GameObject));
        if (load_object != null)
        {
            Instantiate(load_object);
        }
        else
        {
            Debug.Log("Load Fail");
        }        
    }
        
}
