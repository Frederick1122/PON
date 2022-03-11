using System;
using UnityEngine;
using UnityEngine.UI;

public class AttackEnergy : MonoBehaviour
{
    [Header("PrefabSettings")]
    [SerializeField] private Slider energySlider;


    [NonSerialized]public float speed;
    [NonSerialized]public float energy;
    
    public void ChangeEnergy(float _modificationenergy)
    {

        if (energy < 100)
            energy += _modificationenergy * speed;
        energySlider.value = energy;
    }
    private void Start()
    {
        speed = DataHolder.main.RangeSpeed;
    }

    private void LateUpdate()
    {
        ChangeEnergy(Time.deltaTime);
    }
}
