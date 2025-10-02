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

                var servoJson = new JsonData.ServoJson
                {
                    leftShoulder = ConvertToServoAngle(animator.GetBoneTransform(HumanBodyBones.LeftUpperArm).localEulerAngles.x),
                    leftUpperArm = ConvertToServoAngle(animator.GetBoneTransform(HumanBodyBones.LeftUpperArm).localEulerAngles.z),
                    leftLowerArm = ConvertToServoAngle(animator.GetBoneTransform(HumanBodyBones.LeftLowerArm).localEulerAngles.y),
                    rightShoulder = ConvertToServoAngle(animator.GetBoneTransform(HumanBodyBones.RightShoulder).localEulerAngles.x),
                    rightUpperArm = ConvertToServoAngle(animator.GetBoneTransform(HumanBodyBones.RightUpperArm).localEulerAngles.z),
                    rightLowerArm = ConvertToServoAngle(animator.GetBoneTransform(HumanBodyBones.RightLowerArm).localEulerAngles.y),
                    leftUpperLeg0 = ConvertToServoAngle(animator.GetBoneTransform(HumanBodyBones.LeftUpperLeg).localEulerAngles.x),
                    leftUpperLeg1 = ConvertToServoAngle(animator.GetBoneTransform(HumanBodyBones.LeftUpperLeg).localEulerAngles.z),
                    leftLowerLeg = ConvertToServoAngle(animator.GetBoneTransform(HumanBodyBones.LeftLowerLeg).localEulerAngles.x),
                    leftFoot0 = ConvertToServoAngle(animator.GetBoneTransform(HumanBodyBones.LeftFoot).localEulerAngles.x),
                    leftFoot1 = ConvertToServoAngle(animator.GetBoneTransform(HumanBodyBones.LeftFoot).localEulerAngles.z),
                    rightUpperLeg0 = ConvertToServoAngle(animator.GetBoneTransform(HumanBodyBones.RightUpperLeg).localEulerAngles.x),
                    rightUpperLeg1 = ConvertToServoAngle(animator.GetBoneTransform(HumanBodyBones.RightUpperLeg).localEulerAngles.z),
                    rightLowerLeg = ConvertToServoAngle(animator.GetBoneTransform(HumanBodyBones.RightLowerLeg).localEulerAngles.x),
                    rightFoot0 = ConvertToServoAngle(animator.GetBoneTransform(HumanBodyBones.RightFoot).localEulerAngles.x),
                    rightFoot1 = ConvertToServoAngle(animator.GetBoneTransform(HumanBodyBones.RightFoot).localEulerAngles.z),
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