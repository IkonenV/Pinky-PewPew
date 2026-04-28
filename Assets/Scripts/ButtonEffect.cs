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
        // Muutetaan kokoa pehmeästi joka framessa
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * lerpSpeed);
    }

    // Kun hiiri tulee napin päälle
    public void OnPointerEnter(PointerEventData eventData)
    {
        targetScale = initialScale * hoverScale;
    }

    // Kun hiiri poistuu napin päältä
    public void OnPointerExit(PointerEventData eventData)
    {
        targetScale = initialScale;
    }

    // Kun nappia painetaan (hiiri pohjassa)
    public void OnPointerDown(PointerEventData eventData)
    {
        targetScale = initialScale * clickScale;
    }

    // Kun napista päästetään irti
    public void OnPointerUp(PointerEventData eventData)
    {
        // Palataan joko hover-kokoon tai normaaliin riippuen onko hiiri vielä päällä
        if (eventData.hovered.Contains(gameObject))
            targetScale = initialScale * hoverScale;
        else
            targetScale = initialScale;
    }
}
