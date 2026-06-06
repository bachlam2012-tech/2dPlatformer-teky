using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HideUI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject canvas;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void Close()
    {
        canvas.SetActive(false);
    }
}
