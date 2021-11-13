using UnityEngine;
using UnityEngine.UI;

public class BombDropButton : MonoBehaviour
{
    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        Button button = GetComponent<Button>();
        if (button != null) {
            button.onClick.AddListener(dropTheBomb);
        }
    }

    void dropTheBomb() {
        if (player != null) {
            player.DropTheBomb();
        }

        
    }
}
