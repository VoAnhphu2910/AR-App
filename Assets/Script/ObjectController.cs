using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ObjectController : MonoBehaviour
{
    // Panel select dùng để hiển thị khi click vào object
    public GameObject panelSelect;
    public Raycast raycast; // Class raycast
    public ObjectMovement objectMovement;   // Class ObjectMovement

    public GameObject selectedEffect;
    // Start is called before the first frame update

    void Start()
    {
         selectedEffect = transform.GetChild(0).gameObject;


        //DataStorage.furnitures[i].AddComponent<LeanPinchScale>();
        //DataStorage.furnitures[i].AddComponent<LeanDragTranslate>();
        //DataStorage.furnitures[i].AddComponent<LeanTwistRotateAxis>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Touch touch;
    private void OnMouseDown()
    {
       
        touch = Input.GetTouch(0);

        // Nếu có UI phía trước thì không chọn được
        if (IsPointerOverUI(touch))
        {
            return;
        }

        //selectedEffect.SetActive(true);

        //raycast.objLeanTouch.transform.position = this.transform.position;
        //raycast.objLeanTouch.transform.rotation = this.transform.rotation;
        //raycast.objLeanTouch.transform.localScale = this.transform.localScale;

        //transform.SetParent(raycast.objLeanTouch.transform);

        // Truyền object để di chuyển vào class ObjectMovement
        objectMovement._objectMovement = this.gameObject;

        // Biến để không hiện thị cửa sổ chọn object khi click vào màn hình
        //raycast.isPointerObject = true;

        // Set ẩn panel chọn các object
        raycast.panelButtonSelect.SetActive(false);

        // Hiện panel các chức năng khi click vào đối tượng
        panelSelect.SetActive(true);

        //
        selectedEffect.SetActive(true);
        UIController.instance.panelMenuGame.SetActive(false);
        
    }



    // Hàm kiểm tra phía trước object có UI hiển thị không
    bool IsPointerOverUI(Touch touch)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = new Vector2(touch.position.x, touch.position.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }
}
