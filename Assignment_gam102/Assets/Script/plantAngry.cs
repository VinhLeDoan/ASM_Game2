using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plantAngry : MonoBehaviour
{

    public GameObject _bullet;
    public Transform _bulletPos;

    public float _timer;
    private Animator _Animator;
    // Start is called before the first frame update
    void Start()
    {
        _Animator = GetComponent<Animator>();
        _timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if(_timer > 3f)
        {
            _Animator.SetTrigger("atk");
            _timer = 0;
        }
    }

    private void plainShoot()
    {
        Instantiate(_bullet, _bulletPos.position, Quaternion.identity, transform);
    }
}
