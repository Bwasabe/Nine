using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCol : MonoBehaviour
{
    #region 충돌체크

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Box")){
            GameManager.Instance.Player.MinusJumpCount(1);
        }
    }
    #endregion
}
