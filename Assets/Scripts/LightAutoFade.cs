using UnityEngine;

public class LightAutoFade : MonoBehaviour
{
    private Light _light;
    private float _startIntensity;
    private float _lifetime = 1.25f;
    private float _timer = 0f;

    void Start()
    {
        _light = GetComponent<Light>();
        _startIntensity = _light.intensity;

    }

    void Update()
    {
        if (_timer < _lifetime)
        {
            _timer += Time.deltaTime;
            

            float ratio = _timer / _lifetime;
            _light.intensity = Mathf.Lerp(_startIntensity, 0, ratio);
        }
    }
}
