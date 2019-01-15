using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBoxCtrl : MonoBehaviour {

    /// <summary>
    /// 移动的目标
    /// </summary>
    private Vector3 m_TargetPos = Vector3.zero;

    [SerializeField]
    private float m_Speed = 10f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //点击屏幕

        if (Input.GetMouseButton(0) || Input.touchCount ==1)
        {
            Debug.Log("点击屏幕了");

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;

            if (Physics.Raycast(ray,out hitInfo))
            {
                if (hitInfo.collider.gameObject.name.Equals("Ground",System.StringComparison.CurrentCultureIgnoreCase))
                {
                    m_TargetPos = hitInfo.point;
                }
            }
        }

        //如果目标点不是原点 进行移动
        if (m_TargetPos != Vector3.zero)
        {
            Debug.DrawLine(Camera.main.transform.position, m_TargetPos);

            if (Vector3.Distance(m_TargetPos,transform.position) > 0.1f)
            {
                transform.LookAt(m_TargetPos);
                transform.Translate(Vector3.forward * Time.deltaTime * m_Speed);
            }
        }
	}
}
