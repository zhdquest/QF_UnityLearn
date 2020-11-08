using UnityEngine;
using System.Collections;

public class SliderController : MonoBehaviour {


    public void OnSliderValueChanged(float val)
    {
        Debug.Log("Current Slider Value is " + val);
    }
}