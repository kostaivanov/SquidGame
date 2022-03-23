using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;

internal class OnClickOpenPanel : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject Panel;

    public void OnPointerDown(PointerEventData eventData)
    {
        OpenPanel();
    }

    internal void OpenPanel()
    {
        if (Panel != null)
        {
            bool isActive = Panel.activeSelf;
            Panel.SetActive(!isActive);
        }
    }

}
