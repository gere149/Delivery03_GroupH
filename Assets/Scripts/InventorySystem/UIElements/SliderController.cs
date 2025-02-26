using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public PlayerInfo playerInfo;
    public Gradient healthGradient;
    public float lerpSpeed = 5f;

    private Slider slider;
    private Image fillImage;
    private float targetValue;

    private void Start()
    {
        slider = GetComponent<Slider>();
        fillImage = slider.fillRect.GetComponent<Image>();
        targetValue = playerInfo.Health / playerInfo.GetMaxHealth();

        slider.value = targetValue;
        fillImage.color = healthGradient.Evaluate(targetValue);
    }

    public void ResetSliderValues()
    {
        slider = GetComponent<Slider>();
        fillImage = slider.fillRect.GetComponent<Image>();
        targetValue = playerInfo.Health / playerInfo.GetMaxHealth();

        slider.value = targetValue;
        fillImage.color = healthGradient.Evaluate(targetValue);
    }

    private void OnEnable()
    {
        ResetSliderValues();
        PlayerInfo.OnHealthChanged += SetTargetValue;
    }

    private void OnDisable()
    {
        ResetSliderValues();
        PlayerInfo.OnHealthChanged -= SetTargetValue;
    }

    private void Update()
    {
        slider.value = Mathf.Lerp(slider.value, targetValue, Time.deltaTime * lerpSpeed);
        fillImage.color = healthGradient.Evaluate(slider.value);
    }

    private void SetTargetValue()
    {
        if (playerInfo == null) return;
        targetValue = playerInfo.Health / playerInfo.GetMaxHealth();
    }
}