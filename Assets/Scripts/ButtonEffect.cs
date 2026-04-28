using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler

{
[Header("Asetukset")]
    [SerializeField] private float hoverScale = 1.1f;  // Koko kursorin ollessa päällä
    [SerializeField] private float clickScale = 0.9f;  // Koko klikattaessa
    [SerializeField] private float lerpSpeed = 15f;   // Kuinka nopeasti koko muuttuu

    private Vector3 targetScale;
    private Vector3 initialScale;

    void Start()
    {
        initialScale = transform.localScale;
        targetScale = initialScale;
    }

    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.unscaledDeltaTime * lerpSpeed);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetScale = initialScale * hoverScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetScale = initialScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        targetScale = initialScale * clickScale;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.hovered.Contains(gameObject))
            targetScale = initialScale * hoverScale;
        else
            targetScale = initialScale;
    }
}
