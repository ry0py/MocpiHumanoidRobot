using System;
using UnityEngine;
namespace LearnDash
{
    // シングルトンでインスタンスは一つで個々のステートのみを変えていく
    // 現在のモーションが何かを管理するマネージャー
    public class MotionManager : MonoBehaviour
    {
        public static MotionManager Instance { get; private set; }
        public MotionEnum MotionState { get; private set; } = MotionEnum.Dash;
        public event Action<MotionEnum> OnMotionChanged;
        [SerializeField]
        private float maxMotionCoolTime = 1f;
        private float _coolTime;
        void Awake()
        {
            Instance = this;
            ResetCollTime();
        }

        // 状態を変更するメソッドを追加
        public void SetMotionState(MotionEnum newState, bool isAction = true)
        {
            if (MotionState != newState)
            {
                MotionState = newState;
                if (isAction) OnMotionChanged?.Invoke(MotionState); // イベント発火
            }
        }

        private void Update()
        {
            _coolTime += Time.deltaTime;
            if ((Input.GetKeyDown("space") || RecognizeMotion.Instance.CheckJump()) && _coolTime > maxMotionCoolTime)
            {
                SetMotionState(MotionEnum.Jump);
                ResetCollTime();
            }

            if ((Input.GetKeyDown(KeyCode.LeftControl) || RecognizeMotion.Instance.CheckSliding())  && _coolTime > maxMotionCoolTime)
            {
                SetMotionState(MotionEnum.Sliding);
                ResetCollTime();
            }
        }

        private void ResetCollTime()
        {
            _coolTime = 0f;
        }


    }
}