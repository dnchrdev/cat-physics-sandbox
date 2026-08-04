using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Feature.PhysicsInteraction
{
    public class InteractableVisual
    {
        private readonly MeshRenderer _meshRenderer;
        private readonly VisualConfig _visualConfig;

        private Material[] _materials;
        private static readonly HashSet<Mesh> _smoothNormalsApplied = new HashSet<Mesh>();
        private Material _outlineMaskMaterial;
        private Material _outlineFillMaterial;


        public InteractableVisual(MeshRenderer meshRenderer, VisualConfig visualConfig)
        {
            _meshRenderer = meshRenderer;
            _visualConfig = visualConfig;

            if (_meshRenderer == null || _visualConfig == null) return;

            _materials = meshRenderer.materials;
        }

        public void ShowVisualOnFocus()
        {
            if (_meshRenderer == null || _visualConfig == null || _visualConfig.UseOutlineOnFocus == false) return;

            ApplyOutlineColor(_visualConfig.OutlineColorOnFocus, _visualConfig.OutlineWidthOnFocus, _visualConfig.OutlineModeOnFocus);
        }

        private void ApplyOutlineColor(Color color, float width, Mode mode)
        {
            if (_outlineMaskMaterial == null)
                _outlineMaskMaterial = Object.Instantiate(Resources.Load<Material>(@"Materials/OutlineMask"));

            if (_outlineFillMaterial == null)
                _outlineFillMaterial = Object.Instantiate(Resources.Load<Material>(@"Materials/OutlineFill"));

            ApplySmoothNormals();

            _outlineFillMaterial.SetColor("_OutlineColor", color);

            switch (mode)
            {
                case Mode.OutlineAll:
                    _outlineMaskMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.Always);
                    _outlineFillMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.Always);
                    _outlineFillMaterial.SetFloat("_OutlineWidth", width);
                    break;

                case Mode.OutlineVisible:
                    _outlineMaskMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.Always);
                    _outlineFillMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.LessEqual);
                    _outlineFillMaterial.SetFloat("_OutlineWidth", width);
                    break;

                case Mode.OutlineHidden:
                    _outlineMaskMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.Always);
                    _outlineFillMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.Greater);
                    _outlineFillMaterial.SetFloat("_OutlineWidth", width);
                    break;

                case Mode.OutlineAndSilhouette:
                    _outlineMaskMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.LessEqual);
                    _outlineFillMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.Always);
                    _outlineFillMaterial.SetFloat("_OutlineWidth", width);
                    break;

                case Mode.SilhouetteOnly:
                    _outlineMaskMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.LessEqual);
                    _outlineFillMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.Greater);
                    _outlineFillMaterial.SetFloat("_OutlineWidth", 0f);
                    break;
            }

            var mats = _meshRenderer.materials.ToList();
            mats.Add(_outlineMaskMaterial);
            mats.Add(_outlineFillMaterial);
            _meshRenderer.materials = mats.ToArray();
        }

        private void ApplySmoothNormals()
        {
            var meshFilter = _meshRenderer.GetComponent<MeshFilter>();
            if (meshFilter == null || meshFilter.sharedMesh == null) return;

            var mesh = meshFilter.sharedMesh;

            if (!_smoothNormalsApplied.Add(mesh)) return;

            var groups = mesh.vertices
                .Select((vertex, index) => new KeyValuePair<Vector3, int>(vertex, index))
                .GroupBy(pair => pair.Key);

            var smoothNormals = new List<Vector3>(mesh.normals);

            foreach (var group in groups)
            {
                if (group.Count() == 1) continue;

                var smoothNormal = Vector3.zero;
                foreach (var pair in group)
                    smoothNormal += smoothNormals[pair.Value];

                smoothNormal.Normalize();

                foreach (var pair in group)
                    smoothNormals[pair.Value] = smoothNormal;
            }

            mesh.SetUVs(3, smoothNormals);
        }

        public void ShowVisualOnGrab()
        {
            if (_meshRenderer == null || _visualConfig == null) return;

            if (_visualConfig.OnGrabMaterial != null) 
            { 
                var mats = _meshRenderer.materials;
                for (var i = 0; i < mats.Length; i++)
                {
                    mats[i] = _visualConfig.OnGrabMaterial;
                }
                _meshRenderer.materials = mats;
            }

            if (_visualConfig.UseOutlineOnGrab)
            {
                ApplyOutlineColor(_visualConfig.OutlineColorOnGrab, _visualConfig.OutlineWidthOnGrab, _visualConfig.OutlineModeOnGrab);
            }

        }

        public void Reset()
        {
            if (_meshRenderer == null || _materials == null) return;

            _meshRenderer.materials = _materials;

        }
    }
}