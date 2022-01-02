using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;


public class Raycast : MonoBehaviour
{
    // Tâm hiện vị trí để đặt đối tượng (Ảnh)
    public GameObject cursorChildObject;

    // Đối tượng được tạo
    public GameObject objectToPlace;

    // Object được dùng để hiển thị vật thể đang được chọn
    private GameObject _objectPlace;

    [Space]
    [SerializeField]
    private ARRaycastManager raycastManager;

    // Camera chính dùng để lấy tọa độ để đặt vật thể
    [SerializeField]
    private Camera mainCam;

    [Space]
    // Các panel dùng để hiển thị các bảng chức năng trên UI
    public GameObject panelButtonSelect;
    public GameObject panelNotification;
    public GameObject panelSelected;

    [Space]
    [SerializeField]
    public ObjectMovement objectMovement;   // Class ObjectMovement

    private ObjectController objectController;

    [SerializeField]
    private AudioSource audioSource;

    private void Awake()
    {
    }

    void Update()
    {
            UpdateCursor();
    }

    GameObject obj;
    GameObject o;
    //BoxCollider boxCollider;
    // Nút Yes sẽ đặt vật thể ra màn hình
    public void Yes()
    {
        // Tạo game object được chọn
        o = Instantiate(objectToPlace, cursorChildObject.transform.position, cursorChildObject.transform.rotation);

        // Tạo game object mới để chứa game object được tạo ra
        obj = new GameObject();
        // Đặt lại vị trí và hướng cho game object
        obj.transform.position = cursorChildObject.transform.position;
        obj.transform.rotation = cursorChildObject.transform.rotation;
        // Đổi tên game object
        obj.name = _objectPlace.name;

        // Add boxcolider vào object để xử lý nhấn chọn do collider có sẵn không dùng được
        obj.AddComponent<BoxCollider>();
        // Chỉnh sửa lại colider dựa vào giá trị của collider trong asset bundle
        obj.GetComponent<BoxCollider>().size = o.GetComponent<BoxCollider>().size;
        obj.GetComponent<BoxCollider>().center = o.GetComponent<BoxCollider>().center;

        // Xóa collider của asset bundle để tránh gây xung đột
        Destroy(o.GetComponent<BoxCollider>());
        // Set game object được tạo ban đầu là con của game object obj
        o.transform.SetParent(obj.transform);

        // Add script vào game object và set các giắ trị của class
        obj.AddComponent<ObjectController>();
        objectController = obj.GetComponent<ObjectController>();
        objectController.panelSelect = panelSelected;
        objectController.raycast = this;

        objectController.objectMovement = objectMovement;
        
        Destroy(_objectPlace);
        
        // Ẩn panel đi
        panelNotification.SetActive(false);
    }

    // Nút No để ẩn đi panel 
    public void No()
    {
        UIController.instance.panelButtonSelectObject.SetActive(true);
        // Ẩn panel đi
        panelNotification.SetActive(false);

        //Vật thể đang được chọn
        objectToPlace = null;

        // Xóa vật thể đang được chọn
        Destroy(_objectPlace);
    }


    // Thiết lập vật thể được chọn
    public void SetObject(GameObject setObject)
    {
        // cho vật thể được chọn và hiển thị ra ngay vị trí được chọn trên màn hình
        _objectPlace = Instantiate(setObject, cursorChildObject.transform.position, cursorChildObject.transform.rotation);

        // Xóa class không cần thiết trên vật thể
        Destroy(_objectPlace.gameObject.GetComponent<ObjectController>());
        //objectMovement.objectMovement = _objectPlace;
    }

    void UpdateCursor()
    {
        // Lấy vị trí ngay giữa màn hình
        //Vector2 screenPosition = mainCam.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));

        List<ARRaycastHit> hits = new List<ARRaycastHit>();


        //raycastManager.Raycast(screenPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.All);
        raycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

        if (hits.Count > 0)
        {

            //Đặt vị trí của tâm cộng thêm phần bù để hiển thị theo ý muốn
            cursorChildObject.transform.position = hits[0].pose.position;
            cursorChildObject.transform.rotation = hits[0].pose.rotation;

            // Nếu có object được chọn sẽ đật object ngay vị trí của tâm
            if (_objectPlace != null)
            {
                _objectPlace.transform.position = cursorChildObject.transform.position;
                _objectPlace.transform.rotation = cursorChildObject.transform.rotation;

                _objectPlace.transform.rotation = hits[0].pose.rotation;
            }
        }
    }

    public void PlayAudio()
    {
        audioSource.Play();
    }

}
