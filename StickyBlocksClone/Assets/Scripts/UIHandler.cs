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
        ScriptInfrastructure.instance.startLevelText.text = ScriptInfrastructure.instance.levelCounter.ToString();
        ScriptInfrastructure.instance.nextLevelText.text = (ScriptInfrastructure.instance.levelCounter + 1).ToString();
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

    }

}
