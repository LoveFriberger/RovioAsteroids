using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MenuButton : MonoBehaviour
{
    [SerializeField]
    GameObject selectionIndicator = null;
    [SerializeField]
    UnityEvent onClick = null;

    public static MenuButton Selected { get; private set; }

    public void Select()
    {
        if (Selected != null)
            Selected.Deselect();

        Selected = this;
        selectionIndicator.SetActive(true);

    }

    public void Deselect()
    {
        selectionIndicator.SetActive(false);
    }

    public void Click()
    {
        onClick?.Invoke();

    }
}
