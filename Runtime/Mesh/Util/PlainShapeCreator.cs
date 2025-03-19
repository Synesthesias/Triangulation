using iShape.Geometry;
using iShape.Geometry.Container;
using System;
using Unity.Collections;
using UnityEngine;

namespace iShape.Triangulation.Runtime
{
    /// <summary>
    /// GeometryのPlainShapeに移行予定(移行が完了したら削除)
    /// </summary>
    public class PlainShapeCreator
    {
        /// <summary>
        /// IShapeをPlainShapeに変換する
        /// </summary>
        public PlainShape CreatePlainShape(
            Vector2[] hull,
            Vector2[][] holes,
            IntGeom iGeom,
            Allocator allocator)
        {
            var iHull = iGeom.Int(hull);

            IntShape iShape;

            if (holes != null && holes.Length > 0)
            {
                var iHoles = iGeom.Int(holes);
                iShape = new IntShape(iHull, iHoles);
            }
            else
            {
                iShape = new IntShape(iHull, Array.Empty<IntVector[]>());
            }

            var pShape = new PlainShape(iShape, allocator);

            return pShape;
        }
    }
}