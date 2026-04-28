using UnityEngine;
using UnityEngine.UI;

public class VolumeSync : MonoBehaviour
{
public Slider slider;
public string parameterName;

    void Start()
    {
        float savedValue = PlayerPrefs.GetFloat(parameterName, 0.75f);

        if(slider != null)
        {
            slider.value = savedValue;
        }
    }
}
