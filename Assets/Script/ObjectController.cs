using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectController : MonoBehaviour
{
    // Panel select dùng để hiển thị khi click vào object
    public GameObject panelSelect;
    public Raycast raycast; // Class raycast
    [Space]
    public ObjectMovement objectMovement;   // Class ObjectMovement

    public GameObject selectedEffect;
    // Start is called before the first frame update

    void Start()
    {
         selectedEffect = transform.GetChild(0).transform.GetChild(0).gameObject;
    }

   
    //Hàm sẽ gọi khi nhấn vào object
    private void OnMouseDown()
    {
        Select();
    }

    Touch touch;
    public void Select()
    {
        //// Nếu có UI phía trước thì không chọn được
        if (IsPointerOverUI(touch))
        {
            return;
        }

        // Nếu có object khác đang được chọn thì sẽ tăt effect của object đó trước khi hiển thị effect của object mới
        if (objectMovement._objectMovement != null)
        {
            objectMovement._objectMovement.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
        }
        
        // Truyền object để di chuyển vào class ObjectMovement
        this.objectMovement._objectMovement = this.gameObject;
        
        // Set ẩn panel chọn các object
        this.raycast.panelButtonSelect.SetActive(false);

        this.raycast.PlayAudio();
        // Hiện panel các chức năng khi click vào đối tượng
        this.panelSelect.SetActive(true);


        // Hiển thị effect đang chọn
        this.selectedEffect.SetActive(true);
        // Ẩn panel menu main khi chọn object 
        UIController.instance.panelMenuGame.SetActive(false);
    }


    
     //Hàm kiểm tra phía trước object có UI hiển thị không
    bool IsPointerOverUI(Touch touch)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = new Vector2(touch.position.x, touch.position.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }
    
}
