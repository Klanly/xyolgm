using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneCtrl : MonoBehaviour {

    /// <summary>
    /// UI场景控制器
    /// </summary>
    [SerializeField]
    private UISceneLoadingCtrl m_UILoadingCtrl;

    private AssetBundleCreateRequest request;

    private AssetBundle bundle;

    private AsyncOperation m_Async = null;

    /// <summary>
    /// 当前进度 
    /// </summary>
    private int m_CurrProgress = 0;



	void Start () {

        DelegateDefine.Instance.OnSceneLoadOk += OnSceneLoadOk;
        StartCoroutine(LoadingScene());
	}

    //当场景加载完后
    private void OnSceneLoadOk()
    {
        if (m_UILoadingCtrl != null)
        {
            Destroy(m_UILoadingCtrl.gameObject);
            Destroy(gameObject);
        }
    }

    private IEnumerator LoadingScene()
    {

        string strSceneName = string.Empty;

        strSceneName = "Logon_Scene";

        if (string.IsNullOrEmpty(strSceneName))
        {
            yield break;
        }

        m_Async = SceneManager.LoadSceneAsync(strSceneName, LoadSceneMode.Additive);
        m_Async.allowSceneActivation = false;
        yield return m_Async;

    }


	// Update is called once per frame
	void Update () {

        if (m_Async == null)
        {
            m_UILoadingCtrl.SetProgressValue(0.01f);
            return;
        }

        int toProgress = 0;
        if (m_Async.progress <0.9f)
        {
            toProgress = Mathf.Clamp((int)m_Async.progress * 100, 1, 100);
        }
        else
        {
            toProgress = 100;
           
        }

        if (m_CurrProgress <toProgress)
        {
            m_CurrProgress++;
        }
        else
        {
            DelegateDefine.Instance.OnSceneLoadOk();
            m_Async.allowSceneActivation = true;
            
        }
        m_UILoadingCtrl.SetProgressValue(m_CurrProgress * 0.01f);
	}

    private void OnDestroy()
    {
        DelegateDefine.Instance.OnSceneLoadOk -= OnSceneLoadOk;
        if (bundle != null)
        {
            bundle.Unload(false);
        }
        request = null;
        bundle = null;
    }
}
