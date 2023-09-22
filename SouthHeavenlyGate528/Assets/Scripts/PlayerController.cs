using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// 玩家角色
    /// 后续可能有武器等等
    /// </summary>
    private Transform charactor;

    /// <summary>
    /// 玩家动画控制器
    /// </summary>
    private Animator playerAnimator;

    private void Start()
    {
        // 初始化查找角色
        charactor = transform.Find("Charactor");
        // 初始化查找动画控制那个hi其
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
