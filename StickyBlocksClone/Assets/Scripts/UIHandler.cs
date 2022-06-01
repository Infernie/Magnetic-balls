using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] Transform endLine;
    [SerializeField] Transform player;

    float maxDistance;

    // Start is called before the first frame update
    void Start()
    {
        maxDistance = getDistance();
    }



    // Update is called once per frame
    void Update()
    {
        if (player.position.z <= maxDistance && player.position.z <= endLine.position.z)
        {
            float distance = 1 - (getDistance() / maxDistance);
            SetProgress(distance);
        }

    }
    private float getDistance()
    {
        return Vector3.Distance(player.position, endLine.position);
    }
    void SetProgress(float progressAdder)
    {
        slider.value = progressAdder;
        //    slider.minValue = 0;
        //    slider.maxValue = 1000;
        //    slider.value = progressAdder;
        //    if (slider.value == slider.maxValue)
        //    {
        //        Debug.Log("POG");
        //    }
    }

}
