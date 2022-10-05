using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TestController : MonoBehaviour
{
    [Header("DebugText")]
    [Space(20)]
    public string test_string;

    private LinearStoryController lsc;

    public void test(LinearStoryController controller)
    {
        Debug.Log(test_string);
        lsc = controller;
        test_end();
    }

    private void test_end()
    {
        lsc.RunEventList();
    }
}
