using UnityEngine;

public class QuitButton : MonoBehaviour
{
    public void Quit()
    {
        Debug.Log("Quitting application");
        Application.Quit();
    }
}
