using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemText
{
    public string title;
    [TextArea(10, 10)]
    public string text;
}
