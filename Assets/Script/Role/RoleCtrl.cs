using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleCtrl : MonoBehaviour {


    /// <summary>
    /// 移动的目标
    /// </summary>
    private Vector3 m_TargetPos = Vector3.zero;

    [SerializeField]
    private float m_Speed = 10f;

    /// <summary>
    /// 转身速度
    /// </summary>
    private float m_RotationSpeed = 0.2f;

    /// <summary>
    /// 转身的目标方向
    /// </summary>
    private Quaternion m_TargetQuaternion;


    private CharacterController m_characterController;

    // Use this for initialization
    void Start()
    {
        m_characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_characterController == null) return;

        //点击屏幕

        if (Input.GetMouseButton(0) || Input.touchCount == 1)
        {
            Debug.Log("点击屏幕了");

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.collider.gameObject.name.Equals("Ground", System.StringComparison.CurrentCultureIgnoreCase))
                {
                    m_TargetPos = hitInfo.point;
                    m_RotationSpeed = 0;
                }
            }
        }

        if (!m_characterController.isGrounded)
        {
            m_characterController.Move(transform.position + new Vector3(0,-1000,0) - transform.position);
        }

        //如果目标点不是原点 进行移动
        if (m_TargetPos != Vector3.zero)
        {
            //画出相机照到点击位置的点
            Debug.DrawLine(Camera.main.transform.position, m_TargetPos);

            //如果目标点和当前的的距离 大于 0.1f就移动
            if (Vector3.Distance(m_TargetPos, transform.position) > 0.1f)
            {
                //得到距离的差值
                Vector3 direction =  m_TargetPos - transform.position;
                direction = direction.normalized; //归一化
                direction = direction * Time.deltaTime * m_Speed;
                direction.y = 0;
               

                if (m_RotationSpeed <= 1)
                {
                    m_RotationSpeed += 5f * Time.deltaTime;
                    //让角色缓慢转身
                    m_TargetQuaternion = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Lerp(transform.rotation, m_TargetQuaternion, m_RotationSpeed);
                }
                m_characterController.Move(direction);
                //transform.LookAt(new Vector3(m_TargetPos.x,transform.position.y, m_TargetPos.z));
                //移动物体的写法 
                //transform.LookAt(m_TargetPos);
                //transform.Translate(Vector3.forward * Time.deltaTime * m_Speed);
            }
        }

        CameraAutoFollow();

        
    }

    /// <summary>
    /// 摄像机自动 跟随
    /// </summary>
    private void CameraAutoFollow()
    {
        if (CameraCtrl.Instance == null) return;
        CameraCtrl.Instance.transform.position = gameObject.transform.position;
        CameraCtrl.Instance.AutoLookAt(gameObject.transform.position);

        if (Input.GetKeyUp(KeyCode.A))
        {
            CameraCtrl.Instance.SetCameraRotate(0);
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            CameraCtrl.Instance.SetCameraRotate(1);
        }
    }


    
}
