using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// 场景加载完毕
/// </summary>
public class DelegateDefine : Singleton<DelegateDefine>
{

    /// <summary>
    /// 场景加载完毕
    /// </summary>
    public Action OnSceneLoadOk;

}

