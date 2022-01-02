using UnityEngine;

public class RotateCamera : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        //rotY = rot.y;
        //rotx = rot.x;
    }

    // Update is called once per frame
    void Update()
    {
        cameraRotationAccelerometer();
    }

    float xValue;
    float xValueMinMax = 1.0f;
    float cameraSpeed = 20.0f;
    Vector3 accelerometerSmoothValue;

    void cameraRotationAccelerometer()
    {
        if(xValue < - xValueMinMax)
        {
            xValue = -xValueMinMax;
        }

        if(xValue > xValueMinMax)
        {
            xValue = xValueMinMax;
        }

        //accelerometerSmoothValue = Lowpass();
        xValue += accelerometerSmoothValue.x;

        transform.rotation = new Quaternion(0, xValue, 0, cameraSpeed);
    }
}
