using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public InventoryManager tabMaster;
    public Image tabImage;

    public void OnPointerClick(PointerEventData eventData)
    {
        tabMaster.OnTabSelected(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tabMaster.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tabMaster.OnTabExit(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        tabImage = GetComponent<Image>();
        tabMaster.AddTab(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
