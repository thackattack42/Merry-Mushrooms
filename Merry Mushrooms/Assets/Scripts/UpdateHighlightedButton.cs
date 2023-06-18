using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpdateHighlightedButton : MonoBehaviour, IPointerEnterHandler
{
    public Button btn;
    public Slider slider;
    public Toggle toggle;
    
    void Start()
    {
        btn = gameObject.GetComponent<Button>();
        slider = gameObject.GetComponent<Slider>();
        toggle = gameObject.GetComponent<Toggle>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (btn != null)
        {
            btn.Select();
        }
        else if (slider != null)
        {
            slider.Select();
        }
        else if (toggle != null)
        {
            toggle.Select();
        }
    }
}
