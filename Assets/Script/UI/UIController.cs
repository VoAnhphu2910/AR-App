using UnityEngine;

public class UIController : MonoBehaviour
{
    //public GameObject[] listObject;
    public Raycast raycast;
    public GameObject panelSelect;
    [Space]
    public GameObject panelButtonSelectObject;
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
        // truyền game object và các giá trị vào button
        raycast.objectToPlace = _object;
        raycast.panelNotification.SetActive(true);

        raycast.SetObject(_object);

        // Hàm play audio khi click vào
        raycast.PlayAudio();
        //Khi nhấn sẽ ẩn panel select
        panelButtonSelectObject.SetActive(false);
    }

    // Hàm để set active các game ocject trên UI
    public void SetActiveFalse(GameObject _object)
    {
        _object.SetActive(!_object.activeSelf);
    }

    // Hàm set active true
    public void SetTrue(GameObject obj)
    {
        obj.SetActive(true);
    }

    // Hàm sẽ được gọi khi nhấn nút exit của panel select
    public void SetEffectObjectSelect()
    {
        objectMovement._objectMovement.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
    }
}
