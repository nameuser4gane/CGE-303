using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
//include this to use Slider
using UnityEngine.UI;

public class DisplayBar : MonoBehaviour
{
    //Slider for healthbar
    public Slider slider;


    public Gradient gradient;

    public Image fill;


    public void SetValue(float value)
    {
        slider.value = value;


        //set color of fill
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void SetMaxValue(float value)
    {

        //set max value of slider
        slider.maxValue = value;


        //set current value of slier to max value
        slider.value = value;

        fill.color = gradient.Evaluate(1f);

    }



}
