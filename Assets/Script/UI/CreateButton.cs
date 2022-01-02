using System;
using UnityEngine;
using UnityEngine.UI;


// Hàm để add onclick vào button với param đi kèm
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

    // Dropdown thể loại
    [SerializeField]
    private Dropdown dropdownType;


    // Start is called before the first frame update
    void Awake()
    {
        DataStorage.type.Insert(0, "All");

        dropdownType.AddOptions(DataStorage.type);

    }

    public void Down()
    {
        CreateButtonInGameObject();
    }

    private void CreateButtonInGameObject()
    {
        
        for (int i = 0; i < DataStorage.furnitures.Length; i++)
        {
            //Debug.Log(i);
            // Tạo button và gán tên bằng loại object
            var button = Instantiate(buttonPrefab);
            button.name = DataStorage.row[i].type;

            // Add sự kiện onclick cho nút và truyền game object tương ứng
            button.GetComponent<Button>().AddEventListener(DataStorage.furnitures[i], UIController.instance.ButtonSelectObject);
            //Add vào panel select
            button.transform.SetParent(this.gameObject.transform, false);
            //Tên hiển thị của button
            button.transform.GetChild(0).GetComponent<Text>().text = DataStorage.row[i].text;
            //Gán hình ảnh vào button
            button.transform.GetChild(1).GetComponent<RawImage>().texture = DataStorage.images[i];

        }
    }

    // Hàm chọn loại object
    public void SelecType()
    {
        // Lấy giá trị hiện tại đang được chọn
        string dropdownSelected = dropdownType.options[dropdownType.value].text;

        // Nếu là all thì sẽ hiển thị tất cả các button
        if(dropdownSelected == "All")
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                gameObject.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
        else
        {
            // Duyệt từng phần tử con của panel
            for (int i = 0; i < transform.childCount; i++)
            {
                // Nếu khác với loại đang được chọn thì ẩn đi ngược lại thì không ẩn
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
