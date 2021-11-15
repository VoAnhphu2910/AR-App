using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class CreateAssetbundle : MonoBehaviour
{
   [MenuItem("Assets/Build AssetBundle")]

   static void BuildAssetBundle()
    {
        string assetBundleDirectory = "Assets/AssetBundle";

        if(!Directory.Exists(assetBundleDirectory))
        {
            Directory.CreateDirectory(assetBundleDirectory);
        }

        BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, BuildTarget.Android);
    }
}
