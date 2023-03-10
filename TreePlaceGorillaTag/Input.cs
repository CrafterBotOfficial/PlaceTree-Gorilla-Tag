using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace TreePlaceGorillaTag
{
    internal class Input : MonoBehaviour
    {
        public static bool TriggerDown;
        public static float Scale;
        public static float Rotation;

        private bool _triggerDown;

        private void Awake() => Scale = 1f;
        private void Update()
        {
            if (!Main.Instance.ModAllowed) return;

            List<InputDevice> InputDeviceStatusList = new List<InputDevice>();
            InputDevices.GetDevicesWithCharacteristics(UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Left | UnityEngine.XR.InputDeviceCharacteristics.Controller, InputDeviceStatusList);
            var LeftHandController = InputDeviceStatusList[0];

            if (InputDeviceStatusList.Count > 1) return;

            LeftHandController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.trigger, out float Trigger);
            LeftHandController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out Vector2 Axis);

            TriggerDown = Trigger > 0.5f;
            Rotation += Mathf.Round(Axis.x) * 5;

            if (Scale <= 0.1) Scale = 0.1f;
            else if (Scale > 9.5f) Scale = 9.5f;
            else Scale += Scale > 10f ? ((Scale - 10)) : Mathf.Round(Axis.y) / 5;

            // Tree place handler, raycasting

            RaycastHit hit;
            Transform LeftHandTransform = GorillaLocomotion.Player.Instance.leftHandTransform;
            Physics.Raycast(LeftHandTransform.position, LeftHandTransform.forward, out hit, 1000);

            bool Success = hit.collider != null;
            Vector3 HitPoint = hit.point;

            if (TriggerDown)
            {
                if (Success)
                {
                    TreePlace.TreeManager.DisplayTree(HitPoint, Rotation, Scale);
                }
                _triggerDown = true;
            }

            if (!TriggerDown && _triggerDown)
            {
                if (Success)
                {
                    TreePlace.TreeManager.PlaceTree(HitPoint, Rotation, Scale);
                }
                _triggerDown = false;
                TreePlace.TreeManager.TreePrefab.SetActive(false);
            }
        }
    }
}
