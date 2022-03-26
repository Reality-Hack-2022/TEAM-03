using UnityEngine;
using ReadyPlayerMe;
using UnityEngine.SceneManagement;

public class AvatarImporter : MonoBehaviour
{
    public GameObject currentAvatar;
    private GameObject importedAvatar;
    private bool avatarHasBeenImported = false;
    public TMPro.TMP_InputField inputField;
    public GameObject loadAvatarButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void LoadAvatar()
    {
        if (inputField.text == "")
        {
            inputField.text = "Input a URL";
        }
        else
        {
            if (avatarHasBeenImported)
            {
                return;
            }
            AvatarLoader avatarLoader = new AvatarLoader();
            Debug.Log("Loading avatar");
            avatarLoader.LoadAvatar(inputField.text, StoreAvatar);
        }
    }

    void StoreAvatar(GameObject avatar)
    {
        avatarHasBeenImported = true;
        importedAvatar = avatar;

        // Update imported avatar's scale, rotation, and position
        importedAvatar.transform.localScale = currentAvatar.transform.localScale;
        //importedAvatar.transform.position = currentAvatar.transform.position;
        importedAvatar.transform.rotation = currentAvatar.transform.rotation;

        // Disable current avatar
        currentAvatar.SetActive(false);
    }
}
