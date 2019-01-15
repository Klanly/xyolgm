using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISceneLoadingCtrl : MonoBehaviour
{

    /// <summary>
    /// 进度条上的文本
    /// </summary>
    [SerializeField]
    private Text progressValue;

    /// <summary>
    /// 进度条
    /// </summary>
    [SerializeField]
    private Slider progress;

    /// <summary>
    /// 设置进度条值
    /// </summary>
    /// <param name="value"></param>
    public void SetProgressValue(float value)
    {
        progress.value = value;
        progressValue.text = string.Format("{0}%",(int)(value*100));
    }

    
}
