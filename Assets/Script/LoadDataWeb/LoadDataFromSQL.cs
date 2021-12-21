using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace Lean.Touch
{
    public class LoadDataFromSQL : MonoBehaviour
    {
        [SerializeField]
        private string urlData;

        [SerializeField]
        private CreateButton createButton;

        [SerializeField]
        private Material materialCube;

        // Start is called before the first frame update
        void Awake()
        {
            StartCoroutine(GetDB());
        }


        string[] dataRows;
        string[] dataColumn;
        IEnumerator GetDB()
        {
            Debug.Log("Start");
            UnityWebRequest request = UnityWebRequest.Get(urlData);
            yield return request.SendWebRequest();
            string dataSelect = request.downloadHandler.text;

            dataRows = dataSelect.Split(';');

            // Khởi tạo độ dài mảng
            DataStorage.row = new DataStorage.Data[dataRows.Length - 1];
            //DataStorage.images = new Texture[DataStorage.row.Length ];
            DataStorage.images = new Texture[dataRows.Length - 1];
            DataStorage.furnitures = new GameObject[dataRows.Length - 1];

            //g = new GameObject[dataRows.Length - 1];
            //tt = new Texture[dataRows.Length - 1];

            List<string> t = new List<string>();

            //Load dữ liệu vào class lưu trữ
            for (int i = 0; i < dataRows.Length - 1; i++)
            {
                dataColumn = dataRows[i].Split('@');
                DataStorage.row[i].text = dataColumn[0];
                DataStorage.row[i].linkModel = dataColumn[1];
                DataStorage.row[i].name = dataColumn[2];
                DataStorage.row[i].type = dataColumn[3];
                DataStorage.row[i].image = dataColumn[4];

                t.Add(DataStorage.row[i].type);

                //------------------------------------------------------------------
                StartCoroutine(GetTexture(DataStorage.row[i].image, i));
            }
            var nt = t.Distinct().ToList();

            createButton.LoadTypes(nt);

            Debug.Log("done");
            //Debug.Log("Text " + DataStorage.row[0].text + " -link " + DataStorage.row[0].linkModel + " -type " + vrow[0].type + " -name " + DataStorage.row[0].name);

            //for (int i = 0; i < DataStorage.row.Length; i++)
            //{


            //    Debug.Log(DataStorage.row[i].text + " " + DataStorage.row[i].linkModel + " " + DataStorage.row[i].type + " " + DataStorage.row[i].name + " " + DataStorage.row[i].image);
            //    //Debug.Log(DataStorage.row[i].image);
            //}


        }

        AssetBundle bundle;
        //public GameObject[] g;
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
                    bundle = DownloadHandlerAssetBundle.GetContent(uwr);
                }
            }

            DataStorage.furnitures[i] = (GameObject)bundle.LoadAsset(DataStorage.row[i].name);
            //g[i] = DataStorage.furnitures[i];
            DataStorage.furnitures[i].AddComponent<ObjectController>();

            //DataStorage.furnitures[i].AddComponent<LeanPinchScale>();
            //DataStorage.furnitures[i].AddComponent<LeanDragTranslate>();
            //DataStorage.furnitures[i].AddComponent<LeanTwistRotateAxis>();



            MeshRenderer my_renderer = DataStorage.furnitures[i].transform.GetChild(0).GetComponent<MeshRenderer>();
            if (my_renderer != null)
            {
                Material my_material = my_renderer.sharedMaterial;
            }

            my_renderer.material = materialCube;

            createButton.CreateButtonInGameObject(i, DataStorage.furnitures[i]);

            //Instantiate(DataStorage.furnitures[i]);
        }

        //public Texture []tt;
        IEnumerator GetTexture(string link, int i)
        {
            //tt = new List<Texture>();
            UnityWebRequest request;

            request = UnityWebRequestTexture.GetTexture(link);
            yield return request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(request.error);
            }
            else
            {
                DataStorage.images[i] = ((DownloadHandlerTexture)request.downloadHandler).texture;

                StartCoroutine(GetAssetBundle(i, DataStorage.row[i].linkModel));
                //tt[i] = DataStorage.images[i];
            }
        }
    }
}
