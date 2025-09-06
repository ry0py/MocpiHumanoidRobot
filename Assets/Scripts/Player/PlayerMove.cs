using UnityEngine;
using DG.Tweening;
using LearnDash;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float finishTime;
    [SerializeField]
    private float goalDistance;
    private float _leftTime;

    private enum PositionEnum
    {
        Center,
        Left,
        Right
    }

    void Start()
    {
        this.transform.DOMoveZ(goalDistance, finishTime);
        _leftTime = Time.time + finishTime;
    }

    void Update()
    {
        _leftTime = _leftTime - Time.deltaTime;
        if (RecognizeMotion.Instance.CheckLaneMoveRight()) // 横に動く
        {
            Debug.Log("右に移動します");
            this.transform.DOMoveX(1f, 1f);
        }
        if (RecognizeMotion.Instance.CheckLaneMoveLeft()) // 横に動く
        {
            Debug.Log("左に移動します");
            this.transform.DOMoveX(-1f, 1f);
        }

        if (RecognizeMotion.Instance.CheckLaneMoveCenter()) // 横に動く
        {
            Debug.Log("中央に移動します");
            this.transform.DOMoveX(0f, 1f);
        }
    }
}