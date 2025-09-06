using UnityEngine;

namespace LearnDash
{
    public class PlayerAnimator : MonoBehaviour
    {
        private Animator _animator;

        void Awake()
        {
            _animator = this.GetComponent<Animator>();
            MotionManager.Instance.OnMotionChanged += ChangeAnimation;
        }

        void Oestroy()
        {
            MotionManager.Instance.OnMotionChanged -= ChangeAnimation;
        }

        void Update()
        {
            // アニメーションの動きによってモーションを設定する
            AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0); // 0はBase Layer
            if (stateInfo.IsName("Dash")) MotionManager.Instance.SetMotionState(MotionEnum.Dash, false);
            if (stateInfo.IsName("Jump")) MotionManager.Instance.SetMotionState(MotionEnum.Jump, false);
            if (stateInfo.IsName("Sliding")) MotionManager.Instance.SetMotionState(MotionEnum.Sliding,false);

        }


        void ChangeAnimation(MotionEnum motion)
        {
            if (motion == MotionEnum.Jump)
            {
                _animator.SetTrigger("Jump");
            }
            if (motion == MotionEnum.Sliding)
            {
                _animator.SetTrigger("Sliding");
            }
        }
    }
}