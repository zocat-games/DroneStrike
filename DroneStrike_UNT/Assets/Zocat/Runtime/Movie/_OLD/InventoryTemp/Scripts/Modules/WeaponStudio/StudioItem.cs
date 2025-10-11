using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Zocat
{
    public class StudioItem : InstanceBehaviour
    {
        public List<RendererMat> RendererMats;

        [Button(ButtonSizes.Medium)]
        public void GetMaterials()
        {
            RendererMats.Clear();
            var renderers = GetComponentsInChildren<MeshRenderer>();
            foreach (var item in renderers)
            {
                var match = new RendererMat(item, item.sharedMaterial);
                RendererMats.Add(match);
            }
        }

        // public void SetState(bool materialised)
        // {
        //     foreach (var item in RendererMats)
        //     {
        //         var mat = materialised ? item.Material : WeaponStudio.StudioDisable;
        //         item.Renderer.material = mat;
        //     }
        // }
    }


    [Serializable]
    public struct RendererMat
    {
        public MeshRenderer Renderer;
        public Material Material;

        public RendererMat(MeshRenderer renderer, Material material)
        {
            Renderer = renderer;
            Material = material;
        }
    }
}