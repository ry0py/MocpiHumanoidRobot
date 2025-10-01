using UnityEngine;
using Mocopi.Receiver;

namespace HumanoidRobot
{
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
                var servoJson = new JsonData.ServoJson
                {
                    leftShoulder = leftShoulder != null ? ConvertToServoAngle(leftShoulder.localRotation.eulerAngles.x) : 90f,
                    leftUpperArm = leftUpperArm != null ? ConvertToServoAngle(leftUpperArm.localRotation.eulerAngles.x) : 90f,
                    leftLowerArm = leftLowerArm != null ? ConvertToServoAngle(leftLowerArm.localRotation.eulerAngles.x) : 90f,
                    rightShoulder = rightShoulder != null ? ConvertToServoAngle(rightShoulder.localRotation.eulerAngles.x) : 90f,
                    rightUpperArm = rightUpperArm != null ? ConvertToServoAngle(rightUpperArm.localRotation.eulerAngles.x) : 90f,
                    rightLowerArm = rightLowerArm != null ? ConvertToServoAngle(rightLowerArm.localRotation.eulerAngles.x) : 90f,
                    leftUpperLeg0 = leftUpperLeg != null ? ConvertToServoAngle(leftUpperLeg.localRotation.eulerAngles.x) : 90f,
                    leftUpperLeg1 = leftUpperLeg != null ? ConvertToServoAngle(leftUpperLeg.localRotation.eulerAngles.z) : 90f,
                    leftLowerLeg = leftLowerLeg != null ? ConvertToServoAngle(leftLowerLeg.localRotation.eulerAngles.x) : 90f,
                    leftFoot0 = leftFoot != null ? ConvertToServoAngle(leftFoot.localRotation.eulerAngles.x) : 90f,
                    leftFoot1 = leftFoot != null ? ConvertToServoAngle(leftFoot.localRotation.eulerAngles.z) : 90f,
                    rightUpperLeg0 = rightUpperLeg != null ? ConvertToServoAngle(rightUpperLeg.localRotation.eulerAngles.x) : 90f,
                    rightUpperLeg1 = rightUpperLeg != null ? ConvertToServoAngle(rightUpperLeg.localRotation.eulerAngles.z) : 90f,
                    rightLowerLeg = rightLowerLeg != null ? ConvertToServoAngle(rightLowerLeg.localRotation.eulerAngles.x) : 90f,
                    rightFoot0 = rightFoot != null ? ConvertToServoAngle(rightFoot.localRotation.eulerAngles.x) : 90f,
                    rightFoot1 = rightFoot != null ? ConvertToServoAngle(rightFoot.localRotation.eulerAngles.z) : 90f,
                };
                var json = JsonUtility.ToJson(servoJson);
                Debug.Log(json);
            }
        }

        float ConvertToServoAngle(float unityAngle)
        {
            // 角度変換とクランプ処理
            if (unityAngle > 180f) unityAngle -= 360f;
            return Mathf.Clamp(unityAngle + 90f, 0f, 180f);
        }
    }
}