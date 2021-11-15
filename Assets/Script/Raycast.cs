using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
//using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class Raycast : MonoBehaviour
{
    // Tâm hiện vị trí để đặt đối tượng (Ảnh)
    public GameObject cursorChildObject;
    // Đối tượng được tạo
    public GameObject objectToPlace;

    public ARRaycastManager raycastManager;

    // Camera chính dùng để lấy tọa độ để đặt vật thể
    public Camera mainCam;
    
    // Các panel dùng để hiển thị các bảng chức năng trên UI
    public GameObject panelNotification;
    public GameObject panelButtonSelect;
    public GameObject panelSelected;


    //public ObjectMovement objectMovement;

    public bool isPointerObject = false;
    void Start()
    {
    }

    void Update()
    {
            UpdateCursor();

     //   if (EventSystem.current.IsPointerOverGameObject() &&
     //EventSystem.current.currentSelectedGameObject != null &&
     //EventSystem.current.currentSelectedGameObject.CompareTag("Object"))
     //   {
     //       Debug.Log("aaaaa");
     //       return;
     //   }

        // Nếu chọn vào object thì thoát hàm
        //if(isPointerObject)
        //{
        //    return;
        //}

        //if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        //{
        //    touch = Input.GetTouch(0);

        //    if (!IsPointerOverUI(touch))
        //    {
        //        return;
        //    }


            //if (useCursor)
            //{
            //    //GameObject.Instantiate(objectToPlace, transform.position, transform.rotation);
            //    panelNotification.SetActive(true);
            //}
            /*
            else
            {
                List<ARRaycastHit> hits = new List<ARRaycastHit>();
                raycastManager.Raycast(Input.GetTouch(0).position, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon);
                if (hits.Count > 0)
                {
                    GameObject.Instantiate(objectToPlace, hits[0].pose.position, hits[0].pose.rotation);
                }
            }
            */
        //}
    }

    /*  Hàm để xác định có trỏ lên UI hay không
    bool IsPointerOverUI(Touch touch)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = new Vector2(touch.position.x, touch.position.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }
    */

        // Nút Yes sẽ đặt vật thể ra màn hình
    public void Yes()
    {
        // Tạo vật thể tại vị trí được chọn
        GameObject.Instantiate(objectToPlace, transform.position + new Vector3(0, 0, 1f), transform.rotation);

        // Xóa vật được chọn vì đã tạo ra vật thể mới
        Destroy(_objectPlace);
        
        // Ẩn panel đi
        panelNotification.SetActive(false);
    }

    // Nút No để ẩn đi panel 
    public void No()
    {
        // Ẩn panel đi
        panelNotification.SetActive(false);

        //Vật thể đang được chọn
        objectToPlace = null;

        // Xóa vật thể đang được chọn
        Destroy(_objectPlace);
    }

    // Object được dùng để hiển thị vật thể đang được chọn
    public GameObject _objectPlace;

    // Thiết lập vật thể được chọn
    public void SetObject(GameObject setObject)
    {
        // cho vật thể được chọn và hiển thị ra ngay vị trí được chọn trên màn hình
        _objectPlace = Instantiate(setObject, transform.position + new Vector3(0, 0, 1f), transform.rotation);

        // Xóa class không cần thiết trên vật thể
        Destroy(_objectPlace.gameObject.GetComponent<ObjectController>());
        //objectMovement.objectMovement = _objectPlace;
    }

    void UpdateCursor()
    {
        // Lấy vị trí ngay giữa màn hình
        Vector2 screenPosition = mainCam.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
        List<ARRaycastHit> hits = new List<ARRaycastHit>();


        raycastManager.Raycast(screenPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

        if (hits.Count > 0)
        {
            //Lấy vị trí của tâm 
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;

            //Đặt vị trí của tâm cộng thêm phần bù để hiển thị theo ý muốn
            cursorChildObject.transform.position = hits[0].pose.position + new Vector3(0, 0, 1f);

            // Nếu có object được chọn sẽ đật object ngay vị trí của tâm
            if (_objectPlace != null)
            {
                //_objectPlace = Instantiate(objectToPlace, transform.position, transform.rotation);
                _objectPlace.transform.position = hits[0].pose.position + new Vector3(0,0,1f);
                _objectPlace.transform.rotation = hits[0].pose.rotation;
            }
        }
    }

   
}
