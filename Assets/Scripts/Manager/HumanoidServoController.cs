using UnityEngine;
using Mocopi.Receiver;

public class HumanoidServoController : MonoBehaviour
{
    public MocopiAvatar mocopiAvatar;
    
    void Update()
    {
        if (mocopiAvatar?.Animator != null)
        {
            Animator animator = mocopiAvatar.Animator;

            // HumanBodyBonesからHumanoidようにボーンを限定する
            Transform leftShoulder = animator.GetBoneTransform(HumanBodyBones.LeftShoulder);
            Transform leftUpperArm = animator.GetBoneTransform(HumanBodyBones.LeftUpperArm);
            Transform leftLowerArm = animator.GetBoneTransform(HumanBodyBones.LeftLowerArm);
            Transform rightShoulder = animator.GetBoneTransform(HumanBodyBones.RightShoulder);
            Transform rightUpperArm = animator.GetBoneTransform(HumanBodyBones.RightUpperArm);
            Transform rightLowerArm = animator.GetBoneTransform(HumanBodyBones.RightLowerArm);
            Transform leftUpperLeg = animator.GetBoneTransform(HumanBodyBones.LeftUpperLeg); // 2つの軸
            Transform leftLowerLeg = animator.GetBoneTransform(HumanBodyBones.LeftLowerLeg);
            Transform leftFoot = animator.GetBoneTransform(HumanBodyBones.LeftFoot); // 2つの軸
            Transform rightUpperLeg = animator.GetBoneTransform(HumanBodyBones.RightUpperLeg); // 2つの軸
            Transform rightLowerLeg = animator.GetBoneTransform(HumanBodyBones.RightLowerLeg);
            Transform rightFoot = animator.GetBoneTransform(HumanBodyBones.RightFoot); // 2つの軸
            
            if (leftUpperArm != null)
            {
                Vector3 rotation = leftUpperArm.localRotation.eulerAngles;
                Debug.Log($"Left Upper Arm Rotation: {rotation}");
                
                // サーボ角度に変換
                float servoAngle = ConvertToServoAngle(rotation.x);
                // SendToServo(ServoID.LeftShoulder, servoAngle);
            }
        }
    }
    
    float ConvertToServoAngle(float unityAngle)
    {
        // 角度変換とクランプ処理
        if (unityAngle > 180f) unityAngle -= 360f;
        return Mathf.Clamp(unityAngle + 90f, 0f, 180f);
    }
}