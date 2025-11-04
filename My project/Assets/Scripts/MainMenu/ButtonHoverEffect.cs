using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Настройки затемнения")]
    [SerializeField] private float hoverDarkenAmount = 0.7f;
    [SerializeField] private float transitionDuration = 0.2f;

    private Image buttonImage;
    private Color normalColor;
    private Color darkenedColor;
    private bool isTransitioning = false;

    void Start()
    {
        buttonImage = GetComponent<Image>();
        normalColor = buttonImage.color;
        darkenedColor = new Color(
            normalColor.r * hoverDarkenAmount,
            normalColor.g * hoverDarkenAmount,
            normalColor.b * hoverDarkenAmount,
            normalColor.a
        );
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isTransitioning)
            StartCoroutine(TransitionColor(darkenedColor));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isTransitioning)
            StartCoroutine(TransitionColor(normalColor));
    }

    private System.Collections.IEnumerator TransitionColor(Color targetColor)
    {
        isTransitioning = true;
        Color startColor = buttonImage.color;
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            buttonImage.color = Color.Lerp(startColor, targetColor, elapsedTime / transitionDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        buttonImage.color = targetColor;
        isTransitioning = false;
    }
}