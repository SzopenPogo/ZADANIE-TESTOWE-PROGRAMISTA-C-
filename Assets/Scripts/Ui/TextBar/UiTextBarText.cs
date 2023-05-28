using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiTextBarText : MonoBehaviour
{
    [field: SerializeField] private TextMeshProUGUI textObject;

    public void SetText(string text) => textObject.text = text;
}
