using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// A support class with functions for UI
// This class can be referenced by any other script in the project

// -- SETUP: -- //
// Change Utils to whatever static class contains the references
// Add a canvasTransform variable to Utils that holds the transform of the canvas object
public class CanvasUtils
{   
    // -- INTERACTION FUNCTIONS -- //
    // These use the order of children in the heirarchy to check interaction with the cursor

    // Detect whether the cursor is interacting with a supplied object
    public static bool IsCursorInteract(GameObject inputObject, bool ignoreChildren) {
        RectTransform[] canvasObjects = Utils.canvasTransform.GetComponentsInChildren<RectTransform>();

        // loop backwards through the canvas objects
        // only works if they are organized in order
        for (int i = canvasObjects.Length - 1; i > 0; i--) {
            if (canvasObjects[i].gameObject == inputObject && IsCursorInBounds(inputObject)) {
                return true; // if we reach the input return true
            }
            else if (canvasObjects[i].gameObject.activeSelf && IsCursorInBounds(canvasObjects[i].gameObject) && canvasObjects[i].gameObject.GetComponent<Image>() != null) {
                if (ignoreChildren && canvasObjects[i].transform.IsChildOf(inputObject.transform)) { continue; }
                return false;
            }
        }

        if (inputObject == Utils.canvasTransform.gameObject) { return true; }

        return false; // this should happen if the cursor isn't in the bounding box of the object at all
    }

    // Detect whether the cursor is within the bounds of a supplied object
    public static bool IsCursorInBounds(GameObject inputObject) {
        // The object needs to have a recttransform in order for the function to work
        Vector2 scale = inputObject.GetComponent<RectTransform>().sizeDelta;
        Vector2 offset = new Vector2(inputObject.GetComponent<RectTransform>().pivot.x * scale.x, inputObject.GetComponent<RectTransform>().pivot.y * scale.y);

        // check if the cursor is inside the object's 'bounding box'
        if (Input.mousePosition.x > inputObject.transform.position.x - offset.x
        && Input.mousePosition.x < inputObject.transform.position.x - offset.x + scale.x
        && Input.mousePosition.y > inputObject.transform.position.y - offset.y
        && Input.mousePosition.y < inputObject.transform.position.y - offset.y + scale.y) {
            return true;
        }
        else {
            return false;
        }
    }
}
