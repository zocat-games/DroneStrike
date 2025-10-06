using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasRenderer))]
public class UIDashedLineRenderer : Graphic
{
    public enum LineStyle
    {
        Solid,
        Dashed
    }

    [Header("Line Style")]
    public LineStyle lineStyle = LineStyle.Dashed;

    public List<RectTransform> points = new();
    public float thickness = 5f;

    [Header("Dash Settings")]
    public float dashLength = 20f;
    public float gapLength = 10f;

    [Header("Offsets")]
    public float startOffset;
    public float endOffset;

    private void Update()
    {
        SetVerticesDirty(); // her frame yeniden çiz
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();
        if (points == null || points.Count < 2) return;

        for (var i = 0; i < points.Count - 1; i++)
        {
            if (points[i] == null || points[i + 1] == null) continue;

            var p1 = WorldToLocal(points[i].position);
            var p2 = WorldToLocal(points[i + 1].position);

            var dir = (p2 - p1).normalized;
            var totalDist = Vector2.Distance(p1, p2);

            // İlk segmentte startOffset uygula
            if (i == 0)
                p1 += dir * startOffset;

            // Son segmentte endOffset uygula
            if (i == points.Count - 2)
                p2 -= dir * endOffset;

            var adjustedDist = Vector2.Distance(p1, p2);
            if (adjustedDist <= 0f) continue;

            var normal = new Vector2(-dir.y, dir.x) * (thickness * 0.5f);

            if (lineStyle == LineStyle.Solid)
            {
                // Tek parça düz çizgi
                AddQuad(vh, p1, p2, normal);
            }
            else
            {
                // Dash çizgiler
                var drawn = 0f;
                var drawDash = true;

                while (drawn < adjustedDist)
                {
                    var segmentLength = drawDash ? dashLength : gapLength;
                    var end = Mathf.Min(drawn + segmentLength, adjustedDist);

                    if (drawDash)
                    {
                        var segStart = p1 + dir * drawn;
                        var segEnd = p1 + dir * end;

                        AddQuad(vh, segStart, segEnd, normal);
                    }

                    drawn += segmentLength;
                    drawDash = !drawDash; // dash ↔ gap
                }
            }
        }
    }

    private void AddQuad(VertexHelper vh, Vector2 start, Vector2 end, Vector2 normal)
    {
        var v1 = UIVertex.simpleVert;
        v1.color = color;
        v1.position = start - normal;
        var v2 = UIVertex.simpleVert;
        v2.color = color;
        v2.position = start + normal;
        var v3 = UIVertex.simpleVert;
        v3.color = color;
        v3.position = end + normal;
        var v4 = UIVertex.simpleVert;
        v4.color = color;
        v4.position = end - normal;

        var index = vh.currentVertCount;
        vh.AddVert(v1);
        vh.AddVert(v2);
        vh.AddVert(v3);
        vh.AddVert(v4);
        vh.AddTriangle(index + 0, index + 1, index + 2);
        vh.AddTriangle(index + 2, index + 3, index + 0);
    }

    private Vector2 WorldToLocal(Vector3 worldPos)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform,
            RectTransformUtility.WorldToScreenPoint(null, worldPos),
            null,
            out var localPoint
        );
        return localPoint;
    }
}