using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public Image img;   //Ảnh để tạo hiệu ứng load màn chơi
    public AnimationCurve curve;    // Đường cong biểu đồ biểu diễn tốc tộ thực hiện load

    [SerializeField]
    private float timeIn = 2;

    [Space]
    [SerializeField]
    private Slider sliderLoad;  // Thanh load
    [SerializeField]
    private Text textPercent;   // Phần trăm đang chạy

    [SerializeField]
    private float minValue;
    [SerializeField]
    private float maxValue;

    private float startValue;
    private float currentValue = 0;

    [Space]
    [SerializeField]
    private bool isMenuScene;
    void Start()
    {
        // Nếu là scene menu thì chạy thanh load còn lại thì không
        if (isMenuScene && DataStorage.isStart)
        {
            startValue = Random.Range(minValue, maxValue);
            //startValue = currentValue;
            currentValue = 0;
            sliderLoad.maxValue = startValue;
            StartCoroutine(LoadBar());
        }
        else
        {
            StartCoroutine(FadeIn());
        }
    }

    // Hàm load thanh trạng thái
    IEnumerator LoadBar()
    {
        while (currentValue <= startValue)
        {
            currentValue += Time.deltaTime;
            sliderLoad.value = currentValue;
            textPercent.text =Mathf.RoundToInt(((sliderLoad.value * 100) / startValue)) + "%";
            yield return currentValue >= startValue;
        }

        StartCoroutine(FadeIn());
        yield return new WaitForSeconds(0.8f);
        sliderLoad.transform.gameObject.SetActive(false);
    }


    IEnumerator FadeIn()    // Tạo hiệu ứng khi load vào màn chơi
    {
        if(isMenuScene)
        {
            sliderLoad.gameObject.SetActive(false);
        }

        while (timeIn > 0f)
        {
            timeIn -= Time.deltaTime;    // Trừ theo khung hình để lấy thời gian load màn
            float a = curve.Evaluate(timeIn);    // Tốc độ làm mờ đi theo  đường cong
            img.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }
        this.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void FadeTo(string scene)    // Hàm dùng để load màn chơi và hiệu ứng khi gọi
    {
        StartCoroutine(FadeOut(scene));
    }

    IEnumerator FadeOut(string scene)   // Load hiệu ứng khi rời khỏi màn chơi
    {
        this.transform.GetChild(0).gameObject.SetActive(true);
        float t = 0f;

        while (t < 1.5f)
        {
            t += Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }

        SceneManager.LoadScene(scene);  // Load màn chơi
    }

    public void SetFalse()
    {
        DataStorage.isStart = false;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
