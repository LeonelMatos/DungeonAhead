using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using Cinemachine;

[System.Serializable]
public class Effects: MonoBehaviour
{
    /// <summary>
    /// Construction zone, proceed carefully.
    /// </summary>



    //When creating potion: add SetEffect(int), add to list, notification, effect
    //Add to CancelEffect(int)

    //To Update
    [HideInInspector]
    public List<int> activeEffects = new List<int>();

    private Notification notification;
    CinemachineVirtualCamera cinemachineCam;
    [Tooltip("Duration of a potion effect")]
    public float maxTime = 180f;
    [HideInInspector]
    public float time = 180f;  //Reset o Player script @ StartNewScene()
    [HideInInspector]
    public List<float> timeCounter;

    private void Start()
    {
        time = maxTime;
    }

    public void SetReferences()
    {
        notification = GameObject.FindGameObjectWithTag("Notification").GetComponent<Notification>();
        cinemachineCam = GameObject.FindGameObjectWithTag("CM VCam").GetComponent<CinemachineVirtualCamera>();
    }

    public void SetEffect(int effect)
    {
        Debug.Log("Setting effect");
        if(activeEffects.Contains(effect))
        {
            switch (effect)
            {
                case 1:
                    StopCoroutine("UseNightVisionPotion");
                    CancelEffect(1);
                    StartCoroutine("UseNightVisionPotion");
                    break;
                case 2:
                    StopCoroutine("UseSpeedPotion");
                    CancelEffect(2);
                    StartCoroutine("UseSpeedPotion");
                    break;
            }
        }
        else
        {
            switch (effect)
            {
                case 1:
                    StartCoroutine("UseNightVisionPotion");
                    break;
                case 2:
                    StartCoroutine("UseSpeedPotion");
                    break;
            }
        }
        time = maxTime;
        StartCoroutine("TimeCounter");
    }

    public void CancelEffect(int effect)    //Add potion here
    {
        timeCounter.RemoveAt(activeEffects.IndexOf(effect));
        activeEffects.Remove(effect);
        switch (effect)
        {
            case 1: //NightVision
                GetComponentInChildren<Light2D>().intensity -= 0.5f;
                notification.StartNotification("Night Vision ended.", "potion");
                break;
            case 2: //SpeedBoost
                StartCoroutine("CameraFadeIn");
                GetComponent<PlayerMovement>().runSpeed /= 1.5f;
                notification.StartNotification("Speed Boost ended.", "potion");
                break;
        }
    }

    private IEnumerator TimeCounter()
    {
        timeCounter.Add(0f);
        int index = timeCounter.Count - 1;
        for (int i = 0; i < time; i++)
        {
            yield return new WaitForSeconds(0.99f);
            timeCounter[index]++;
            //Debug.Log(timeCounter[index]);
        }
    }
////Milk
    public void UseMilk()
    {
        //Cancel all potions effects
        StopAllCoroutines();

        if (activeEffects.Count != 0)
            notification.StartNotification("All potion effects ended.", "potion");

        //Revert all potion effects
        for (int i = 0; i < activeEffects.Count; i++)
        {
            CancelEffect(activeEffects[i]);
        }
        activeEffects.Clear();
    }

    public void UseRandomPotion() //Random Potion -20 nrg //Effect 0
    {
        int rndm = Random.Range(1, 2);  /////////////Add number count of potions here!
        Debug.Log("RandomPotion: " + rndm);
        SetEffect(rndm);
    }

////Camera effects
    public IEnumerator CameraFadeOut()
    {
        for (int i = 0; i < 10; i++)
        {
            cinemachineCam.m_Lens.OrthographicSize += 0.05f;
            yield return new WaitForSeconds(0.01f);
        }
    }

    public IEnumerator CameraFadeIn()
    {
        for (int i = 0; i < 10; i++)
        {
            cinemachineCam.m_Lens.OrthographicSize -= 0.05f;
            yield return new WaitForSeconds(0.01f);
        }
    }

    public IEnumerator UseNightVisionPotion()    //Night Vision (3:00) -5 nrg   //Effect 1
    {   //To add visual output and potion levels that change
        //the effect strenght
        activeEffects.Add(1);
        
        notification.StartNotification("Night Vision started. " + time + "s", "potion");
        GetComponent<PlayerStats>().TakeEnergy(5);
        GetComponentInChildren<Light2D>().intensity += 0.5f;  //May vary depending the level
        yield return new WaitForSeconds(time);
        CancelEffect(1);
    }

    public IEnumerator UseSpeedPotion() //Speed Potion (1:30) -10 nrg //Effect 2
    {
        activeEffects.Add(2);

        notification.StartNotification("Speed Boost started. 3:00", "potion");
        StartCoroutine("CameraFadeOut");
        GetComponent<PlayerMovement>().runSpeed *= 1.5f;
        yield return new WaitForSeconds(time);
        CancelEffect(2);
    }

}
