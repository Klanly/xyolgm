using UnityEngine;
using System.Collections;
using DG.Tweening;

public class CameraCtrl : MonoBehaviour {

    public static CameraCtrl Instance;



    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        m_CameraUpAndDown.transform.localEulerAngles = new Vector3(0, 0, Mathf.Clamp(m_CameraUpAndDown.transform.localEulerAngles.z, 5f, 60f));
    }

    /// <summary>
    /// 控制摄像机的上下
    /// </summary>
    [SerializeField]
    private Transform m_CameraUpAndDown;

    /// <summary>
    /// 控制摄像机缩放父物体
    /// </summary>
    [SerializeField]
    private Transform m_CameraZoomContainer;

    /// <summary>
    /// 摄像机容器
    /// </summary>
    [SerializeField]
    private Transform m_CameraContainer;

    // Use this for initialization
    void Start () {
	
	}
	
    

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,10f );

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 9f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 8f);
    }


    /// <summary>
    /// 设置摄像机旋转
    /// </summary>
    /// <param name="type">0= 左  1=右</param>
    public void SetCameraRotate(int type)
    {
        transform.Rotate(0, 60 * Time.deltaTime * (type == 0 ? -1 : 1), 0);
    }

    /// <summary>
    /// 设置摄像机上下 0=上 1=下
    /// </summary>
    /// <param name="type"></param>
    public void SetCameraUpAndDown(int type)
    {
        m_CameraUpAndDown.transform.Rotate(0,0,40*Time.deltaTime*(type == 0 ? -1 :1));
        m_CameraUpAndDown.transform.localEulerAngles = new Vector3(0,0,Mathf.Clamp(m_CameraUpAndDown.transform.localEulerAngles.z,5f,40f));
    }

    /// <summary>
    /// 设置摄像机 缩放
    /// </summary>
    /// <param name="type">0=拉近 1=拉远</param>
    public void SetCameraZoom(int type)
    {
        m_CameraContainer.Translate(Vector3.forward * 10 *  Time.deltaTime * ((type == 1 ? - 1 : 1)));
        m_CameraContainer.localPosition = new Vector3(0,0,Mathf.Clamp(m_CameraContainer.localPosition.z,-5f,5f));
    }

    /// <summary>
    /// 实时看着主角
    /// </summary>
    /// <param name="pos"></param>
    public void AutoLookAt(Vector3 pos)
    {
        m_CameraZoomContainer.LookAt(pos);
    }

    /// <summary>
    /// 震屏幕
    /// </summary>
    /// <param name="delay">延迟时间</param>
    /// <param name="duration">持续时间</param>
    /// <param name="strength">强度</param>
    /// <param name="vibrato">震幅</param>
    /// <returns></returns>
    public void CameraShake(float delay = 0, float duration = 0.5f, float strength = 1, int vibrato = 10)
    {
        StartCoroutine(DOcameraShake(delay,duration,strength,vibrato));
    }


     
    private IEnumerator DOcameraShake(float delay = 0, float duration = 0.5f, float strength = 1, int vibrato = 10)
    {
        yield return new WaitForSeconds(delay);
        Camera.main.transform.DOShakePosition(0.5f, 1f, 100);
    }

}
