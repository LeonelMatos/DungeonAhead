using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueBar : MonoBehaviour
{
    private Slider slider;

    public void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }

    //Health on max Animation
     //Used on UseItem() at Player for visual feedback
    public IEnumerator maxValueAnimation(GameObject image)
    {
        for (int i = 0; i < 10; i++)
        {
            Debug.Log("yes");

        }
        yield return null;
    }

}
