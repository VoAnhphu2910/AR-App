using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    //public GameObject[] listObject;
    public Raycast raycast;
    public GameObject panelSelect;
    public ObjectMovement objectMovement;
    // Start is called before the first frame update

    ObjectController objectController;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonSelectObject(GameObject _object)
    {
        // Set các giá trị của class Object controller
        raycast.objectToPlace = _object;
        raycast.panelNotification.SetActive(true);
        objectController = raycast.objectToPlace.GetComponent<ObjectController>();
        objectController.panelSelect = panelSelect;
        objectController.raycast = raycast;
        objectController.objectMovement = objectMovement;

        //objectMovement.objectMovement = _object;
        raycast.SetObject(_object);
    }

    public void SetActiveFalse(GameObject _object)
    {
        _object.SetActive(false);
    }

    public void SetRaycat()
    {
        raycast.isPointerObject = false;
        raycast.panelButtonSelect.SetActive(true);
        objectMovement._objectMovement.GetComponent<ObjectController>().selectedEffect.SetActive(false);
        //Debug.Log(objectMovement.objectMovement.GetComponent<ObjectController>().selectedEffect.activeSelf);

    }
}
