using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// ��ҽ�ɫ
    /// ���������������ȵ�
    /// </summary>
    private Transform charactor;

    /// <summary>
    /// ��Ҷ���������
    /// </summary>
    private Animator playerAnimator;

    private void Start()
    {
        // ��ʼ�����ҽ�ɫ
        charactor = transform.Find("Charactor");
        // ��ʼ�����Ҷ��������Ǹ�hi��
        playerAnimator = charactor.Find("CharctorMesh").GetComponent<Animator>();

        // Debug.Log(playerAnimator);
    }

    private void Update()
    {
        // Horizontal
        float horizontal = Input.GetAxisRaw("Horizontal");
        // Direction
        Vector3 dir = new Vector3 (0, 0, horizontal);

        // Movement
        if(dir != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(dir);

            transform.Translate(new Vector3(1, 0, 0) * 2 * Time.deltaTime);
        }


    }
}
