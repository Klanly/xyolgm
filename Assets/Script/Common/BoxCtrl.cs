using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCtrl : MonoBehaviour {


    /// <summary>
    /// 移动目标点
    /// </summary>
    private Vector3 m_TargetPos = Vector3.zero;


    /// <summary>
    /// 移动速度
    /// </summary>
    private float m_Speed = 10;

    private void Update()
    {

        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray,out hitInfo))
            {
                if (hitInfo.collider.gameObject.name.Equals("pPlane2", System.StringComparison.CurrentCultureIgnoreCase))
                {
                    m_TargetPos = hitInfo.point;
                }
            }
        }
        if (m_TargetPos != Vector3.zero)
        {
            if (Vector3.Distance(m_TargetPos,transform.position) > 0.1f)
            {
                transform.LookAt(m_TargetPos);
                transform.Translate(Vector3.forward * Time.deltaTime * m_Speed);
            }
            
        }
    }
}
