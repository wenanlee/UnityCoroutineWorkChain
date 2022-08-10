using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    bool b;
    private int frame;

    // Start is called before the first frame update
    void Start()
    {
        WorkChain works = new WorkChain();
        works.AddWork(() => { Debug.Log("test0"); })
            .RunAt(this);

        this.Work()
            .WaitTime(1)
            .AddWork(() => { Debug.Log("test1"); })
            .WaitReturn(aaa, 0)
            .AddWork(() => { Debug.Log("test1"); })
            .Run();
    }

    private bool aaa()
    {
        return b;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            frame = 100;
            b = true;
        }
    }
}
