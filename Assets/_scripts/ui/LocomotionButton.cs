using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LocomotionButton : MonoBehaviour
{

    public Player player;
    public Vector2 dir;

    bool isPressed;

    // Start is called before the first frame update
    void Start()
    {
        Button button = GetComponent<Button>();
        if (button != null) {
            //button.onClick.AddListener(move);

            EventTrigger trigger = button.gameObject.AddComponent<EventTrigger>();

            var pointerDown = new EventTrigger.Entry();
            pointerDown.eventID = EventTriggerType.PointerDown;
            pointerDown.callback.AddListener((e) => isPressed = true);
            trigger.triggers.Add(pointerDown);

            var pointerUp = new EventTrigger.Entry();
            pointerUp.eventID = EventTriggerType.PointerUp;
            pointerUp.callback.AddListener((e) => isPressed = false);
            trigger.triggers.Add(pointerUp);
        }
    }

    private void Update() {
        if (isPressed) {
            move();
        }
    }

    private void move() {
        if (player != null) {
            player.Move(dir);
        }
    }
}
