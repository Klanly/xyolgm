using UnityEngine;
using System.Collections;

public class InitSceneCtrl : MonoBehaviour
{


    private void Start()
    {
        StartCoroutine(LoadLogOn());
    }

    private IEnumerator LoadLogOn()
    {
        yield return new WaitForSeconds(0.1f);
        SceneMgr.Instance.LoadToLogOn();
    }

}
