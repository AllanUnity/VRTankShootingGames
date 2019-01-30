using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSSingletonDemo : CSSingleton<CSSingletonDemo>
{
    public void Test()
    {
        CSSingletonDemo.Instance.Log();
    }
    public void Log()
    {

    }
}
