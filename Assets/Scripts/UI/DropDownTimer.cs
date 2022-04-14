using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDownTimer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void HandleInputData(int input)
    {
        GameParameter.SetGameTimer((float) input + 1f);
    }
}
