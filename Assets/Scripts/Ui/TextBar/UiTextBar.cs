using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class UiTextBar : MonoBehaviour
{
    //Consts
    private const int MaxTextLines = 100;

    [field: Header("Text Container")]
    [field: SerializeField] private GameObject textContainer;
    [field: SerializeField] private Transform textContainerContentTransform;
    [field: SerializeField] private GameObject textBarTextPrefab;

    [field: Header("Buttons")]
    [field: SerializeField] private Button generateTextButton;
    [field: SerializeField] private Button clearTextButton;

    private bool isTextContainerActive;

    private void Start()
    {
        generateTextButton.onClick.AddListener(FillAndDisplayTextWindow);
        clearTextButton.onClick.AddListener(ClearAndHideTextWindow);
    }

    private void OnDestroy()
    {
        generateTextButton.onClick.RemoveAllListeners();
        clearTextButton.onClick.RemoveAllListeners();
    }

    private void ClearTextContent()
    {
        foreach(Transform child in textContainerContentTransform)
            Destroy(child.gameObject);
    }

    private string GetNumberText(int number)
    {
        //Can be divided by 3 and 5
        if (number % 3 == 0 && number % 5 == 0)
            return "MarkoPolo";

        //Can be divided by 3
        if (number % 3 == 0)
            return "Marko";

        //Can be divided by 5
        if (number % 5 == 0)
            return "Polo";

        //Default
        return "";
    }

    private void GenerateText()
    {
        ClearTextContent();

        for (int i = 0; i < MaxTextLines; i++)
        {
            //Data
            int numberFromOne = i + 1;
            StringBuilder text = new();
            string numberText = GetNumberText(numberFromOne);

            //Format text
            text.Append($"TEXT: {numberFromOne}");

            if (!String.IsNullOrEmpty(numberText))
                text.Append($" - {numberText}");

            if(i != MaxTextLines)
                text.AppendLine();

            //Create Text visual
            GameObject textBarText = Instantiate(textBarTextPrefab, textContainerContentTransform);
            if(textBarText.TryGetComponent(out UiTextBarText textBarTextScript))
                textBarTextScript.SetText(text.ToString());
        }
    }

    private void FillAndDisplayTextWindow()
    {
        if (!isTextContainerActive)
            textContainer.SetActive(true);

        GenerateText();
    }

    private void ClearAndHideTextWindow()
    {
        ClearTextContent();
        textContainer.SetActive(false);
    }
}
