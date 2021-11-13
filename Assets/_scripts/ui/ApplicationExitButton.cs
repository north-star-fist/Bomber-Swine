using UnityEngine;
using UnityEngine.UI;

public class ApplicationExitButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button but = GetComponent<Button>();
        if (but != null) {
            but.onClick.AddListener(()=>Application.Quit());
        }
    }

}
