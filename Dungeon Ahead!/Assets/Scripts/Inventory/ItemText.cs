using System.Collections;
using System.Collections.Generic;
using TriInspector;
using UnityEngine;

[System.Serializable]
public class ItemText
{
    [Title("Title"), HideLabel]
    public string title;
    [Title("Text"), HideLabel]
    [TextArea(10, 10)]
    public string text;
}
