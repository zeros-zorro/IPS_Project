using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Intro : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            StartCoroutine(FadeTextToZeroAlphaAndLaunchNewScene(1f, GetComponentInChildren<TextMeshProUGUI>()));
        }
    }

    private void DisplayMenu()
    {
        gameObject.GetComponent<Menu>().GoToMainMenu();
    }

    public IEnumerator FadeTextToZeroAlphaAndLaunchNewScene(float t, TextMeshProUGUI i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
        DisplayMenu();
    }
}
