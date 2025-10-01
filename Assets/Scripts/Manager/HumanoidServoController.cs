using UnityEngine;
using System.Net.Sockets;
using System.Text;
using Mocopi.Receiver;

namespace HumanoidRobot
{
    public class HumanoidServoController : MonoBehaviour
    {
        public MocopiAvatar mocopiAvatar;

        private string host = "127.0.0.1";
        private int port = 9000;
        private UdpClient client;
        void Start()
        {
            client = new UdpClient();
            client.Connect(host, port);
        }

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

                var servoJson = new JsonData.ServoJson
                {
                    leftShoulder = leftShoulder != null ? ConvertToServoAngle(leftShoulder.localRotation.eulerAngles.x) : 90f,
                    leftUpperArm = leftUpperArm != null ? ConvertToServoAngle(leftUpperArm.localRotation.eulerAngles.z) : 90f,
                    leftLowerArm = leftLowerArm != null ? ConvertToServoAngle(leftLowerArm.localRotation.eulerAngles.x) : 90f,
                    rightShoulder = rightShoulder != null ? ConvertToServoAngle(rightShoulder.localRotation.eulerAngles.x) : 90f,
                    rightUpperArm = rightUpperArm != null ? ConvertToServoAngle(rightUpperArm.localRotation.eulerAngles.z) : 90f,
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
                var message = Encoding.UTF8.GetBytes(json);
                client.Send(message, message.Length);
            }
        }

        private void OnDestroy() {
            client.Close();
        }

        float ConvertToServoAngle(float unityAngle)
        {
            // 角度変換とクランプ処理
            if (unityAngle > 180f) unityAngle -= 360f;
            return Mathf.Clamp(unityAngle + 90f, 0f, 180f);
        }
    }
}