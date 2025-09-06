using UnityEngine;
using LearnDash;
using Mocopi.Receiver;
namespace LearnDash
{
    public class RecognizeMotion : MonoBehaviour
    {
        [Header("閾値調整")]
        [SerializeField] private float jumpHeight; // ジャンプの高さ
        [SerializeField] private float slideDepth;
        [SerializeField] private float laneWidth;

        private Vector3 _iniPos;
        private Vector3 _curPos;
        public static RecognizeMotion Instance { get; private set; }

        // Update is called once per frame

        void Awake()
        {
            ResetIniPos();
            Instance = this;
        }
        void Update()
        {
            _curPos = this.transform.position;
        }

        public bool CheckJump()
        {
            // ジャンプの高さを超えた場合にtrueを返す
            if (_curPos.y - _iniPos.y > jumpHeight)
            {
                return true;
            }
            return false;
        }

        public bool CheckSliding()
        {
            // ジャンプの高さを超えた場合にtrueを返す
            if (_curPos.y - _iniPos.y < slideDepth)
            {
                return true;
            }
            return false;
        }

        public bool CheckLaneMoveLeft()
        {
            // スライドの深さを超えた場合にtrueを返す
            if (_curPos.x - _iniPos.x < -laneWidth)
            {
                return true;
            }
            return false;
        }
        public bool CheckLaneMoveCenter()
        {
            // スライドの深さを超えた場合にtrueを返す
            if (_curPos.x - _iniPos.x > -laneWidth && _curPos.x - _iniPos.x < laneWidth)
            {
                return true;
            }
            return false;
        }

        public bool CheckLaneMoveRight()
        {
            // スライドの深さを超えた場合にtrueを返す
            if (_curPos.x - _iniPos.x > laneWidth)
            {
                return true;
            }
            return false;
        }

        void ResetIniPos()
        {
            // 初期位置をリセット
            _iniPos = this.transform.position;
        }
    }
}