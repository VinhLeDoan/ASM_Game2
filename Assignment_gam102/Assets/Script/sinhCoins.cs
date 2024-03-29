using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// vẽ coin theo hình sin
// mỗi 5s thì vẽ 1 lần
// xoá coin bị bỏ qua

public class sinhCoins : MonoBehaviour
{
    public Transform _player; //ánh xạ tới nhân vật
    public GameObject _coin; // ánh xạ tới prefab
    public float _nextposX;
    public float _nextposY; //Vị trí sinh ra coin
    private float _khoangCach; //khoảng cách coin cách ra với người chơi
    // độ cong hình sin
    public float _chieuCaoSin;
    public float _doRongSin;
    public float _chieucao; //chiều cao so với mặt đất của coin
    public float _chieuCaoToiThieu;
    public float _thoiGian; //bao lâu vẽ 1 lần
    public float _soLuongCoin; // số lượng coin mỗi lần vẽ ra

    public float _timer; //theo dõi thời gian

    private Vector3 _nextPos;

    // Start is called before the first frame update
    void Start()
    {
        _khoangCach = 20;
        _chieuCaoToiThieu = -5f;
        _thoiGian = 5f;
        _soLuongCoin = 10;
        _timer = 0;
        veCoin2();
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > _thoiGian)
        {
            veCoin2();
            _timer = 0;
        }
    }

    private void veCoin()
    {
        _chieucao = Random.Range(1f, 2f) + _chieuCaoToiThieu;
        _chieuCaoSin = 3.5f;
        _doRongSin = 3.5f;
        //_doCong = Random.Range(0.8f, 1.2f);
        _nextposX = _player.transform.position.x + _khoangCach;
        for (int i = -6; i < 7; i++)
        {
            _nextposY = Mathf.Abs(_chieuCaoSin * Mathf.Sin(_nextposX / _doRongSin)) + _chieucao;
            Instantiate(_coin, new Vector3(_nextposX + i + 6, _nextposY + -(i*i + -20), 12f), Quaternion.identity, transform);
            _nextposX++;
        }
    }
    private void veCoin2()
    {
        float _a;
        float _b;
        _a = Random.Range(0.1f, 0.3f); // độ cong
        _b = Random.Range(-0.5f, 1f); //độ lệch chiều cao

        _nextPos = _player.position + new Vector3(_khoangCach, 0f, 0);
        int _soCoin2 = (int)(_soLuongCoin / 2);
        for (int i = -1 * _soCoin2; i <= _soCoin2; i++)
        {
            // y = -a*x*x . trong đó a quyết định độ cong

            Vector3 _toaDoVe = _nextPos + new Vector3(i + _soCoin2, -1 * _a * i * i + _a * _soLuongCoin * _soLuongCoin / 4 + _b, 0f);
            Instantiate(_coin, _toaDoVe, Quaternion.identity, transform);
        }
    }
}
