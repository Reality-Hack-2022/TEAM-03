using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TextInputBehavior : MonoBehaviour
{

    public TMPro.TMP_InputField inputField;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Input field selected");
        inputField.text = "";
        inputField.ActivateInputField();

        //EventSystem.current.SetSelectedGameObject(inputField.gameObject, null);
        //inputField.OnPointerClick(null);
    }
}
