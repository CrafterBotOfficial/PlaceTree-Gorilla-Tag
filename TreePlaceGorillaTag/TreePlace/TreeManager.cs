﻿using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Rendering;
using HarmonyLib;

namespace TreePlaceGorillaTag.TreePlace
{
    internal class TreeManager : MonoBehaviour
    {
        private static List<GameObject> PlacedTrees = new List<GameObject>();

        public static GameObject TreePrefab;
        private static GameObject TreeParent;
        private void Start()
        {
            TreeParent = GameObject.Find("Level/forest/ForestObjects/SmallTrees/");
            TreePrefab = Instantiate<GameObject>(TreeParent.transform.GetChild(0).gameObject);

            // For the record I tried to use Find() but the stupid fucking this refused to work so fuck off pls:)
             
            GameObject SpringToFall = TreePrefab.transform.GetChild(0).gameObject;
            GameObject SmallTree = SpringToFall.transform.GetChild(0).gameObject;

            Destroy(SmallTree.GetComponent<MeshCollider>());

            SpringToFall.SetActive(true);
            TreePrefab.SetActive(false);
            TreePrefab.transform.GetChild(1).gameObject.SetActive(false);
        }

        // Mod Allowed
        private void Update() => Main.Instance.ModAllowed = Main.Instance._roomValid && Main.Instance._modEnabled;

        public static void DisplayTree(Vector3 Position, float Rotation, float ScaleFactor)
        {
            TreePrefab.SetActive(true);

            Quaternion quaternion = Quaternion.Euler(0, Rotation, 0);
            Vector3 Scale = Vector3.one / 10 * ScaleFactor;

            Transform TreePrefabTransform = TreePrefab.transform;
            TreePrefabTransform.localScale = Scale;
            TreePrefabTransform.rotation = quaternion;
            TreePrefabTransform.position = Position;
        }

        public static void PlaceTree(Vector3 Position, float Rotation, float ScaleFactor)
        {
            Quaternion quaternion = Quaternion.Euler(0, Rotation, 0);
            Vector3 Scale = Vector3.one / 10 * ScaleFactor;

            GameObject Tree = Instantiate(TreePrefab, Position, quaternion);
            Tree.transform.localScale = Scale;
            Tree.transform.GetChild(1).gameObject.SetActive(true);

            PlacedTrees.Add(Tree);
        }

        public static void ClearAllTree()
        {
            foreach (GameObject Tree in PlacedTrees) Destroy(Tree);
        }
    }
}

/*
    Tree Location: Level/forest/ForestObjects/SmallTrees/PrefabSmallTree
    Mesh Location: PrefabSmallTree(Clone)/SmallTreeNewLeavesSnow/SmallTree
 */