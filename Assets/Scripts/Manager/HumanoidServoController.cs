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
                    Hips = ToRightHanded(animator.GetBoneTransform(HumanBodyBones.Hips).position),
                    LeftUpperLeg = ToRightHanded(animator.GetBoneTransform(HumanBodyBones.LeftUpperLeg).position),
                    RightUpperLeg = ToRightHanded(animator.GetBoneTransform(HumanBodyBones.RightUpperLeg).position),
                    LeftLowerLeg = ToRightHanded(animator.GetBoneTransform(HumanBodyBones.LeftLowerLeg).position),
                    RightLowerLeg = ToRightHanded(animator.GetBoneTransform(HumanBodyBones.RightLowerLeg).position),
                    LeftFoot = ToRightHanded(animator.GetBoneTransform(HumanBodyBones.LeftFoot).position),
                    RightFoot = ToRightHanded(animator.GetBoneTransform(HumanBodyBones.RightFoot).position),
                    Spine = ToRightHanded(animator.GetBoneTransform(HumanBodyBones.Spine).position),
                    Chest = ToRightHanded(animator.GetBoneTransform(HumanBodyBones.Chest).position),
                    UpperChest = ToRightHanded(animator.GetBoneTransform(HumanBodyBones.UpperChest).position),
                    Neck = ToRightHanded(animator.GetBoneTransform(HumanBodyBones.Neck).position),
                    Head = ToRightHanded(animator.GetBoneTransform(HumanBodyBones.Head).position),
                    LeftShoulder = ToRightHanded(animator.GetBoneTransform(HumanBodyBones.LeftShoulder).position),
                    RightShoulder = ToRightHanded(animator.GetBoneTransform(HumanBodyBones.RightShoulder).position),
                    LeftUpperArm = ToRightHanded(animator.GetBoneTransform(HumanBodyBones.LeftUpperArm).position),
                    RightUpperArm = ToRightHanded(animator.GetBoneTransform(HumanBodyBones.RightUpperArm).position),
                    LeftLowerArm = ToRightHanded(animator.GetBoneTransform(HumanBodyBones.LeftLowerArm).position),
                    RightLowerArm = ToRightHanded(animator.GetBoneTransform(HumanBodyBones.RightLowerArm).position),
                    LeftHand = ToRightHanded(animator.GetBoneTransform(HumanBodyBones.LeftHand).position),
                    RightHand = ToRightHanded(animator.GetBoneTransform(HumanBodyBones.RightHand).position),
                };
                Debug.Log("左腕: " + servoJson.LeftLowerArm + " 左手: " + servoJson.LeftHand+ "差分は" + (servoJson.LeftHand - servoJson.LeftLowerArm));
                var json = JsonUtility.ToJson(servoJson);
                var message = Encoding.UTF8.GetBytes(json);
                client.Send(message, message.Length);
            }
        }

        private static Vector3 ToRightHanded(Vector3 unityPosition)
        {
            return new Vector3(-unityPosition.x, unityPosition.y, unityPosition.z);
        }

        private void OnDestroy() {
            client.Close();
        }
    }
}