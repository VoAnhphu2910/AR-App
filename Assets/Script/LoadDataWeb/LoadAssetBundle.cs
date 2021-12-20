using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class LoadAssetBundle : MonoBehaviour
{
    //public AssetBundle[] bundle;
    public Button[] btn;

    public Button btnPrefabs;

    [SerializeField]
    private Material materialCube;
    [SerializeField]
    private CreateButton createButton;
    // Start is called before the first frame update
    void Awake()
    {
        //Invoke("load", 5);

        //StartCoroutine(GetTexture());
    }

    void load()
    {
        //bundle = new AssetBundle[DataStorage.row.Length];
        DataStorage.furnitures = new GameObject[DataStorage.row.Length];
        //DataStorage.images = new Texture[DataStorage.row.Length - 1];


        //for (int i = 0; i < bundle.Length; i++)
        //{
        //    //DataStorage.images[i] = LoadDataFromSQL.tt[i];
        //    StartCoroutine(GetAssetBundle(i));
        //}
    }


    AssetBundle bundle;
    public IEnumerator GetAssetBundle(int i, string link)
    {
        using (UnityWebRequest uwr = UnityWebRequestAssetBundle.GetAssetBundle(DataStorage.row[i].linkModel))
        {
            yield return uwr.SendWebRequest();

            if (uwr.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(uwr.error);
            }
            else
            {
                // Get downloaded asset bundle
                bundle  = DownloadHandlerAssetBundle.GetContent(uwr);
            }
        }

        DataStorage.furnitures[i] = (GameObject)bundle.LoadAsset(DataStorage.row[i].name);
        DataStorage.furnitures[i].AddComponent<ObjectController>();
        MeshRenderer my_renderer = DataStorage.furnitures[i].transform.GetChild(0).GetComponent<MeshRenderer>();
        if (my_renderer != null)
        {
            Material my_material = my_renderer.sharedMaterial;
        }

        my_renderer.material = materialCube;

        //Instantiate(DataStorage.furnitures[i]);
    }


    public void LoadScene()
    {
        SceneManager.LoadScene(1);
    }


}
