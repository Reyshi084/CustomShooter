using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 使う予定なし（Playerではなく敵を動かすため）
public class FrameMove : MonoBehaviour
{
    [SerializeField] private GameObject _playerObject;

    private Rigidbody2D _rb;

    public void SetPlayer()
    {
        _rb = GetComponent<Rigidbody2D>();

    }

    // Start is called before the first frame update
    void Start()
    {
        SetPlayer();
        _rb.velocity = new Vector3(0, Utils.PlayerMoveSpeed);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
