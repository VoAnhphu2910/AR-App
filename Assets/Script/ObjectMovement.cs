using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectMovement : MonoBehaviour
{ 
    public GameObject _objectMovement;
    public Raycast raycast;

    // Di chuyển theo chiều ngang (trái phải)
    public void MoveHorizontal(float dir)
    {
        _objectMovement.transform.position += new Vector3(0.1f * dir, 0, 0);
        //_objectMovement.transform.position += new Vector3(0.1f * dir * Time.deltaTime, 0, 0);
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
        
    }

    // DI chuyển lên phía trước
    public void MoveForward(float dir)
    {
        _objectMovement.transform.position += new Vector3(0, 0, 0.1f * dir);
    }

    // Di chuyển ra phía sau
    public void Rotate(float dir)
    {
        _objectMovement.transform.localEulerAngles += new Vector3(0, 3 * dir, 0);
    }

    // Xóa và đặt lại các panel
    public void Destroy()
    {
        Destroy(_objectMovement);
        UIController.instance.panelMenuGame.SetActive(true);
        raycast.panelSelected.SetActive(false);
    }

}
