using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    //public GameObject[] listObject;
    public Raycast raycast;
    public GameObject panelSelect;
    [Space]
    public GameObject canvas;
    public ObjectMovement objectMovement;
    // Start is called before the first frame update
    [Space]
    public GameObject panelMenuGame;

    public static UIController instance;
    ObjectController objectController;
    void Start()
    {
        instance = this;
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

        // Set active canvas
        canvas.SetActive(false);
    }

    public void SetActiveFalse(GameObject _object)
    {
        _object.SetActive(!_object.activeSelf);
    }

    public void SetRaycat()
    {
        //raycast.isPointerObject = false;
        //raycast.panelButtonSelect.SetActive(true);
        objectMovement._objectMovement.GetComponent<ObjectController>().selectedEffect.SetActive(false);
        //Debug.Log(objectMovement.objectMovement.GetComponent<ObjectController>().selectedEffect.activeSelf);

    }
}
