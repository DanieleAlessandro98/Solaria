using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private GameObject tooltipContainer;

    [SerializeField]
    private float yOffset = 150;

    [SerializeField]
    private string skillInfoPathText;

    [SerializeField]
    private GameObject skillInfoText;

    private void Start()
    {
        TextManager.LoadSkillInfoText(skillInfoText, skillInfoPathText);
        tooltipContainer.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Vector3 tooltipPosition = transform.position;
        tooltipPosition.y -= yOffset;

        tooltipContainer.transform.position = tooltipPosition;
        tooltipContainer.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltipContainer.SetActive(false);
    }
}
