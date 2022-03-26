using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAvatarButtonBehavior : MonoBehaviour
{

    public AvatarImporter importer;
    private bool loadAvatarSelected = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Load avatar selected");
        if (!loadAvatarSelected)
        {
            Debug.Log("Load avatar button pressed");

            loadAvatarSelected = true;
            importer.LoadAvatar();

            // Remove input text field and avatar load button
            // inputField.gameObject.SetActive(false);
            //this.gameObject.SetActive(false);
        }
    }

}
