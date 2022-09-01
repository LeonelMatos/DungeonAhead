using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    public string test_string;

    private LinearStoryController lsc;

    public void test(LinearStoryController controller)
    {
        Debug.Log(test_string);
        lsc = controller;
        test_end();
    }

    public void test_wait(LinearStoryController controller)
    {
        Debug.Log(test_string);
        lsc = controller;
        StartCoroutine(Wait());
    }

    private void test_end()
    {
        lsc.RunEventList();
    }

    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(5);
        test_end();
    }
}
