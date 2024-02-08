using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MenuButton : MonoBehaviour
{
    [SerializeField]
    GameObject selectionIndicator;
    [SerializeField]
    UnityEvent onClick;

    public static MenuButton Selected { get; private set; }

    public void Select()
    {
        if (Selected != null)
            Selected.Deselect();

        Selected = this;
        if(selectionIndicator != null)
            selectionIndicator.SetActive(true);

        Debug.Log(string.Format("Selected {0} menu button", this.name));
    }

    public void Deselect()
    {
        if (selectionIndicator != null)
            selectionIndicator.SetActive(false);
    }

    public void Click()
    {
        Debug.Log(string.Format("Clicked menu button", this.name));
        onClick?.Invoke();
    }
}
