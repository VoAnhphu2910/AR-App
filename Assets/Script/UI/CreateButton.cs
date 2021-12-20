using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public static class ButtonExtension
{
    public static void AddEventListener<T>(this Button button, T param, Action<T> OnClick)
    {
        button.onClick.AddListener(delegate () {
            OnClick(param);
        });
    }
}

public class CreateButton : MonoBehaviour
{


    [SerializeField]
    private GameObject buttonPrefab;

    //[SerializeField]
    public Dropdown dropdownType;

    //private GameObject button;

    //public static List<string> types = new List<string>();

    //public static List<string> buttonsType = new List<string>();
    // Start is called before the first frame update
    void Start()
    {

        //Invoke("CreateButtonInGameObject", 15);
        //this.gameObject.AddComponent<ContentSizeFitter>();
    }

    public void LoadTypes(List<string> t)
    {

        t.Insert(0, "All");

        dropdownType.AddOptions(t);
    }

    public void CreateButtonInGameObject(int i, GameObject obj)
    {


        //Debug.Log("Length : " + DataStorage.furnitures.Length);
       
        /*
        for (int i = 0; i < DataStorage.furnitures.Length; i++)
        {
            //Debug.Log(i);
            var button = Instantiate(buttonPrefab);
            button.name = DataStorage.row[i].text;

            button.GetComponent<Button>().AddEventListener(DataStorage.furnitures[i], UIController.instance.ButtonSelectObject);

            button.transform.SetParent(this.gameObject.transform, false);
            button.transform.GetChild(0).GetComponent<Text>().text = DataStorage.row[i].text;
            button.transform.GetChild(1).GetComponent<RawImage>().texture = DataStorage.images[i];

            //GameObject a = Instantiate(DataStorage.furnitures[i]);
        }
        */

        var button = Instantiate(buttonPrefab);
        button.name = DataStorage.row[i].type;

        button.GetComponent<Button>().AddEventListener(obj, UIController.instance.ButtonSelectObject);

        button.transform.SetParent(this.gameObject.transform, false);
        button.transform.GetChild(0).GetComponent<Text>().text = DataStorage.row[i].text;
        button.transform.GetChild(1).GetComponent<RawImage>().texture = DataStorage.images[i];

        
    }

    public void SelecType()
    {
        string dropdownSelected = dropdownType.options[dropdownType.value].text;

        if(dropdownSelected == "All")
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                gameObject.transform.GetChild(i).gameObject.SetActive(true);
                //GameObject a = Instantiate(DataStorage.furnitures[i]);
                //a.gameObject.transform.SetParent(button.transform.GetChild(1),false);
            }
        }
        else
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if(transform.GetChild(i).name != dropdownSelected)
                {
                    gameObject.transform.GetChild(i).transform.gameObject.SetActive(false);
                }
                else
                {
                    gameObject.transform.GetChild(i).transform.gameObject.SetActive(true);
                }
            }
        }
    }

}
