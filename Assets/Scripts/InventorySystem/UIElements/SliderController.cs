using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public PlayerInfo playerInfo;
    public Gradient healthGradient;
    public float lerpSpeed = 5f; // Velocidad de interpolación

    private Slider slider;
    private Image fillImage;
    private float targetValue; // Valor objetivo del slider

    private void Start()
    {
        slider = GetComponent<Slider>();
        fillImage = slider.fillRect.GetComponent<Image>();
        targetValue = playerInfo.Health / playerInfo.GetMaxHealth();

        // Inicializar el slider correctamente
        slider.value = targetValue;
        fillImage.color = healthGradient.Evaluate(targetValue);
    }

    private void OnEnable()
    {
        PlayerInfo.OnHealthChanged += SetTargetValue;
    }

    private void OnDisable()
    {
        PlayerInfo.OnHealthChanged -= SetTargetValue;
    }

    private void Update()
    {
        // Interpolación suave del slider
        slider.value = Mathf.Lerp(slider.value, targetValue, Time.deltaTime * lerpSpeed);

        // Actualizar color según el valor interpolado
        fillImage.color = healthGradient.Evaluate(slider.value);
    }

    private void SetTargetValue()
    {
        if (playerInfo == null) return;
        targetValue = playerInfo.Health / playerInfo.GetMaxHealth();
    }
}
