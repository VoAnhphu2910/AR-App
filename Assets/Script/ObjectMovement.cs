using UnityEngine;

public class ObjectMovement : MonoBehaviour
{ 
    public GameObject _objectMovement;
    [SerializeField]
    private Raycast raycast;

    [SerializeField]
    private GameObject panelDelete;
    //[SerializeField]
    //private GameObject panelSelected;


    Vector3 offset;

    // Di chuyển theo chiều ngang (trái phải)
    public void MoveHorizontal(float dir)
    {
        _objectMovement.transform.position += new Vector3(0.1f * dir, 0, 0);
    }

    // Di chuyển theo chiều dọc(trên dưới)
    public void MoveVertical(float dir)
    {
        _objectMovement.transform.position += new Vector3(0, 0.1f * dir, 0);
    }

    // Thay đổi tỉ lệ(lớn nhỏ)
    public void Scale(float scale)
    {
        // nếu tỉ lệ nhỏ hơn 0.3 hoặc lớn hơn 2.5 thì không cho thu phóng
        if ((_objectMovement.transform.localScale.x <= 0.3 && scale < 0) || (_objectMovement.transform.localScale.x >= 2.5 && scale > 0))
        {
            return;
        }

        _objectMovement.transform.localScale += new Vector3(scale * 0.2f, scale * 0.2f, scale * 0.2f);

        offset += new Vector3(0, scale * 0.15f, 0);
    }

    // DI chuyển lên phía trước
    public void MoveForward(float dir)
    {
        _objectMovement.transform.position += new Vector3(0, 0, 0.1f * dir);
    }

    // Di chuyển ra phía sau
    public void Rotate(float dir)
    {
        _objectMovement.transform.localEulerAngles += new Vector3(0, 5 * dir, 0);
    }

    // Hiện panel lựa chọn xóa hay không và ẩn panel select
    public void Destroy()
    {
        panelDelete.SetActive(true);
        raycast.panelSelected.SetActive(false);
    }

    // Xác nhận xóa object và thiết lập lại các panel
    public void Yes()
    {
        panelDelete.SetActive(false);
        Destroy(_objectMovement);
        UIController.instance.panelMenuGame.SetActive(true);
        raycast.panelSelected.SetActive(false);
    }

}
