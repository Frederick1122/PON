using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackEnergy : MonoBehaviour
{
    public float speed;
    public float energy;
    [SerializeField] private Slider energySlider;

    public void ChangeEnergy(float _modificationenergy)
    {

        if (energy < 100)
            energy += _modificationenergy * speed;
        energySlider.value = energy;
    }

    private void LateUpdate()
    {
        ChangeEnergy(Time.deltaTime);
    }
}
