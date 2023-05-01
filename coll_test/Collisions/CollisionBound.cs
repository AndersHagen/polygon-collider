using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace coll_test.Collisions;

public class CollisionBound
{
    public List<Edge> Edges => BuildEdges(VerticesXY());

    private List<Vector2> _vertices { get; set; }

    public Vector2 Center { get; set; }

    private float _angle;

    public CollisionBound(int x, int y, List<Vector2> vertices)
    {
        _angle = 0f;
        Center = new Vector2(x, y);
        _vertices = vertices;
    }

    public void Rotate(float angle)
    {
        _angle = angle;
    }

    public void SetCenter(int x, int y)
    {
        Center = new Vector2(x, y);
    }

    private List<Edge> BuildEdges(List<Vector2> vertices)
    {
        var edges = new List<Edge>();

        for (var i = 0; i < vertices.Count; i++)
        {
            edges.Add(new Edge(vertices[i], vertices[i + 1 < vertices.Count ? i + 1 : 0]));
        }

        return edges;
    }

    public List<Vector2> VerticesXY()
    {
        if (_angle == 0f)
        {
            return _vertices.Select(v => v + Center).ToList();
        }

        var rotated = _vertices.Select(v => Rotate(v, _angle) + Center).ToList();

        return rotated;
    }

    private Vector2 Rotate(Vector2 v, float angle)
    {
        var initAngle = Math.Atan2(v.Y, v.X);
        var newX = (float)Math.Cos(angle + initAngle) * v.Length();
        var newY = (float)Math.Sin(angle + initAngle) * v.Length();

        return new Vector2(newX, newY);
    }

    public Projection ProjectToAxis(Vector2 axis)
    {
        var verts = VerticesXY();

        var min = axis.Dot(verts[0]);
        var max = min;

        for (var i = 1; i < verts.Count; i++)
        {
            var p = axis.Dot(verts[i]);

            if (p < min)
            {
                min = p;
            }
            else if (p > max)
            {
                max = p;
            }
        }

        return new Projection(min, max);
    }
}

