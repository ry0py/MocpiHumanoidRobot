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
                    Hips = animator.GetBoneTransform(HumanBodyBones.Hips).localRotation.eulerAngles,
                    LeftUpperLeg = animator.GetBoneTransform(HumanBodyBones.LeftUpperLeg).localRotation.eulerAngles,
                    RightUpperLeg = animator.GetBoneTransform(HumanBodyBones.RightUpperLeg).localRotation.eulerAngles,
                    LeftLowerLeg = animator.GetBoneTransform(HumanBodyBones.LeftLowerLeg).localRotation.eulerAngles,
                    RightLowerLeg = animator.GetBoneTransform(HumanBodyBones.RightLowerLeg).localRotation.eulerAngles,
                    LeftFoot = animator.GetBoneTransform(HumanBodyBones.LeftFoot).localRotation.eulerAngles,
                    RightFoot = animator.GetBoneTransform(HumanBodyBones.RightFoot).localRotation.eulerAngles,
                    Spine = animator.GetBoneTransform(HumanBodyBones.Spine).localRotation.eulerAngles,
                    Chest = animator.GetBoneTransform(HumanBodyBones.Chest).localRotation.eulerAngles,
                    UpperChest = animator.GetBoneTransform(HumanBodyBones.UpperChest).localRotation.eulerAngles,
                    Neck = animator.GetBoneTransform(HumanBodyBones.Neck).localRotation.eulerAngles,
                    Head = animator.GetBoneTransform(HumanBodyBones.Head).localRotation.eulerAngles,
                    LeftShoulder = animator.GetBoneTransform(HumanBodyBones.LeftShoulder).localRotation.eulerAngles,
                    RightShoulder = animator.GetBoneTransform(HumanBodyBones.RightShoulder).localRotation.eulerAngles,
                    LeftUpperArm = animator.GetBoneTransform(HumanBodyBones.LeftUpperArm).localRotation.eulerAngles,
                    RightUpperArm = animator.GetBoneTransform(HumanBodyBones.RightUpperArm).localRotation.eulerAngles,
                    LeftLowerArm = animator.GetBoneTransform(HumanBodyBones.LeftLowerArm).localRotation.eulerAngles,
                    RightLowerArm = animator.GetBoneTransform(HumanBodyBones.RightLowerArm).localRotation.eulerAngles,
                    LeftHand = animator.GetBoneTransform(HumanBodyBones.LeftHand).localRotation.eulerAngles,
                    RightHand = animator.GetBoneTransform(HumanBodyBones.RightHand).localRotation.eulerAngles,
                };
                var json = JsonUtility.ToJson(servoJson);
                var message = Encoding.UTF8.GetBytes(json);
                client.Send(message, message.Length);
            }
        }

        private void OnDestroy() {
            client.Close();
        }
    }
}