using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkChain
{
    public List<WorkRing> chain = new List<WorkRing>();
    private MonoBehaviour g;
    public WorkChain() { }
    public WorkChain(MonoBehaviour g)
    {
        this.g = g;
    }

    public IEnumerator DoWorkCO()
    {
        for (int i = 0; i < chain.Count; i++)
        {
            yield return chain[i].DoWorkRing();
            yield return chain[i].CheckReturnValueDoWorkRing();
        }
        yield return null;
    }

    public WorkChain AddWork(Action action)
    {
        return AddWork(action, 0);
    }

    public WorkChain AddWork(Action action, float delay)
    {
        chain.Add(new WorkRing(action, delay));
        return this;
    }
    public WorkChain WaitReturn(Func<bool> func, float delay)
    {
        chain.Add(new WorkRing(func, delay));
        return this;
    }
    public WorkChain WaitTime(float time)
    {
        return AddWork(null, time);
    }
    public void RunAt(MonoBehaviour behaviour)
    {
        behaviour.StartCoroutine(DoWorkCO());
    }
    public void Run()
    {
        if (g != null)
            RunAt(g);
        else
            Debug.LogError("Null MonoBehaviour,use RumAt()");
    }
}
public static class MonoBehaviourExtensions
{
    public static WorkChain Work(this MonoBehaviour g)
    {
        WorkChain works = new WorkChain(g);
        return works;
    }
}
public class WorkRing
{

    public Action action;
    public float delay;
    public Func<bool> func;
    public WorkRing(Action action, float delay)
    {
        this.action = action;
        this.delay = delay;
    }
    public WorkRing(Func<bool> func, float delay)
    {
        this.func = func;
        this.delay = delay;
    }
    public IEnumerator DoWorkRing()
    {
        yield return new WaitForSecondsRealtime(delay);
        if (action != null)
        {
            PopulateAction(action);
        }
    }

    public IEnumerator CheckReturnValueDoWorkRing()
    {
        if (func != null)
        {
            yield return new WaitForSecondsRealtime(delay);
            yield return new WaitUntil(func);
        }
    }

    public void PopulateAction(Action action)
    {
        //if (action == null)
        //{
        //    action = () =>
        //    {
        //        Debug.Log("Null Callback");
        //    };
        //}
        action();
    }

}
