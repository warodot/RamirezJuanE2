using TMPro;
using UnityEngine;

public class UIButton : MonoBehaviour
{
    public TMP_InputField inputField;
    public FlexibleColorPicker fcp;
    public PlayerBehaviour player;

    public void Login()
    {
        player.SetData(inputField.text, fcp.color);
        inputField.gameObject.SetActive(false);
        transform.parent.gameObject
        .SetActive(false);
    }
}
