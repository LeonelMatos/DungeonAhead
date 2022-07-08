using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryFunction : MonoBehaviour
{
    ///Receives a variable and changes it with a given value
    ///\arg var: variable to change value \arg val: value to change in given variable
    public void ChangeVar (int var, int val)
    {
        var = val;
    }
}
