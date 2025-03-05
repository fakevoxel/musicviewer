using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PopupMenu : MonoBehaviour
{
    public bool isVisible;

    public Vector3 hiddenPosition;
    public Vector3 visiblePosition;

    public float lerpSpeed;
    private Vector3 targetPosition;

    void Start() {
        targetPosition = visiblePosition;
        isVisible = true;
    }

    void FixedUpdate() {
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, lerpSpeed);

        if (Input.GetKeyDown(KeyCode.Tab)) {
            ToggleMenu();
        }
    }

    public void ToggleMenu() {
        if (isVisible) {
            targetPosition = hiddenPosition;
        }
        else {
            targetPosition = visiblePosition;
        }

        isVisible = !isVisible;
    }
}
