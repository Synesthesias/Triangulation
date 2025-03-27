using System;
using System.Collections.Generic;
using iShape.Geometry.Container;
using iShape.Geometry;
using UnityEngine;
using System.Linq;

namespace iShape.Triangulation.Runtime
{
    /// <summary>
    /// ベクトル計算を行うクラス
    /// 回転行列の適用や法線ベクトルの計算、メッシュの中心座標の取得などを提供
    /// </summary>
    public static class VectorCalculator
    {
        /// <summary>
        /// 指定した回転行列を使用してベクトルを回転
        /// </summary>
        /// <param name="vertex">回転させる3D空間上の頂点座標</param>
        /// <param name="rotationMatrix">適用する回転行列</param>
        /// <returns>回転後の頂点座標</returns>
        private static Vector3 RotateByMatrix(Vector3 vertex, Matrix4x4 rotationMatrix)
        {
            return rotationMatrix.MultiplyPoint3x4(vertex);
        }

        /// <summary>
        /// 指定した角度の逆回転行列を取得
        /// </summary>
        /// <param name="rotationAxisX">X軸回転角度（度単位）</param>
		/// <param name="rotationAxisY">Y軸回転角度（度単位）</param>
        /// <returns>逆回転行列</returns>
        public static Matrix4x4 GetInvertRotationMatrix(float rotationAxisX, float rotationAxisY)
        {
            // var rotationAxis = Vector3.up;
            // var rotationQuaternion = Quaternion.AngleAxis(rotationAxisY, rotationAxis);
            // var rotationQuaternion = Quaternion.Euler(rotationAngleX, rotationAngleY, 0);
            var rotationMatrix = Matrix4x4.Rotate(Quaternion.Euler(rotationAxisX, rotationAxisY, 0));
            return rotationMatrix.inverse;
        }

        /// <summary>
        /// 指定した逆回転行列を適用して頂点群を元の位置に戻す
        /// </summary>
        /// <param name="vertices">回転された頂点配列</param>
        /// <param name="inverseMatrix">適用する逆回転行列</param>
        /// <returns>元の位置に戻した頂点配列</returns>
        public static Vector3[] GetRestoredVertices(Vector3[] vertices, Matrix4x4 inverseMatrix)
        {
            return vertices.Select(vertex => RotateByMatrix(vertex, inverseMatrix)).ToArray();
        }

        /// <summary>
        /// 3Dの頂点配列から2Dの頂点配列を取得
        /// (x,y,z) -> (x,y,0)
        /// </summary>
        /// <param name="hullVertices">3D空間上の頂点配列</param>
        /// <returns>2D座標に変換された頂点配列</returns>
        public static Vector2[] GetHullVertices2d(Vector3[] hullVertices)
        {
            return hullVertices.Select(vertex => new Vector2(vertex.x, vertex.y)).ToArray();
        }

        /// <summary>
        /// 3D空間上の穴の頂点配列を2D座標に変換する。
        /// </summary>
        /// <param name="holesVertices">3D空間上の穴の頂点リストの配列</param>
        /// <returns>2D座標に変換された穴の頂点リストの配列</returns>
        public static Vector2[][] GetHolesVertices2d(Vector3[][] holesVertices)
        {
            if (holesVertices == null || holesVertices.Length == 0)
            {
                return Array.Empty<Vector2[]>();
            }

            return holesVertices
                .Select(hole => hole?.Select(vertex => new Vector2(vertex.x, vertex.y)).ToArray() ?? Array.Empty<Vector2>())
                .ToArray();
        }

        /// <summary>
        /// 3D頂点群をXY平面上に投影するために必要なY軸回転角度を計算する
        /// </summary>
        /// <param name="vertices">対象の頂点群。/param>
        /// <returns>Y軸回転角度（度単位）</returns>
        public static float GetRotationAxisY(Vector3[] vertices)
        {
            var normal = NormalVectorFrom3d(vertices);
            var fromNormal = Vector3.ProjectOnPlane(normal, Vector3.up);

            return Vector3.SignedAngle(fromNormal, Vector3.back, Vector3.up);
        }
        
        /// <summary>
        /// 3D頂点群をXY平面上に投影するために必要なX軸回転角度を計算する
        /// </summary>
        /// <param name="vertices">対象の頂点群</param>
        /// <returns>X軸回転角度（度単位）</returns>
        public static float GetRotationAxisX(Vector3[] vertices)
        {
            var normal = NormalVectorFrom3d(vertices);
            var fromNormal = Vector3.ProjectOnPlane(normal, Vector3.right);
            var angle = Vector3.SignedAngle(fromNormal, Vector3.back, Vector3.right);

            if (angle >= -90 && angle <= 90)
            {
                return angle;
            }
            else
            {
                return angle < -90 ? -(180 + angle) : 180 - angle;
            }
        }

        /// <summary>
        /// 指定した回転角度を適用して頂点群を回転させる
        /// </summary>
        /// <param name="vertices">回転対象の頂点群</param>
        /// <param name="rotationAxisY">X軸回転角度（度単位）</param>
		/// <param name="rotationAxisY">Y軸回転角度（度単位）</param>
        /// <returns>回転後の頂点群</returns>
        public static Vector3[] GetRotatedVertices(Vector3[] vertices, float rotationAxisX, float rotationAxisY)
        {
            var rotationMatrix = Matrix4x4.Rotate(Quaternion.Euler(rotationAxisX, rotationAxisY, 0));
            return vertices.Select(vertex => RotateByMatrix(vertex, rotationMatrix)).ToArray();
        }
        
        /// <summary>
        /// 3D空間の頂点群から法線ベクトルを計算する。
        /// </summary>
        /// <param name="vertices">対象の頂点群。</param>
        /// <returns>計算された法線ベクトル。</returns>
        public static Vector3 NormalVectorFrom3d(Vector3[] vertices)
        {
            var normal = Vector3.zero;

            for (var i = 0; i < vertices.Length; i++)
            {
                var edge1 = vertices[(i + 1) % vertices.Length] - vertices[i];
                var edge2 = vertices[(i + 2) % vertices.Length] - vertices[i];
                normal += Vector3.Cross(edge1, edge2);
            }

            return normal.normalized;
        }

        /// <summary>
        /// 2D空間の頂点群から法線ベクトルを計算する。
        /// </summary>
        /// <param name="vertices">対象の頂点群。</param>
        /// <returns>計算された法線ベクトル。</returns>
        public static Vector3 NormalVectorFrom2d(Vector2[] vertices)
        {
            var normal = Vector3.zero;

            for (var i = 0; i < vertices.Length; i++)
            {
                var edge1 = (Vector3)(vertices[(i + 1) % vertices.Length] - vertices[i]);
                var edge2 = (Vector3)(vertices[(i + 2) % vertices.Length] - vertices[i]);
                normal += Vector3.Cross(edge1, edge2);
            }

            return normal.normalized;
        }
        
        /// <summary>
        /// 2次元同一平面上の頂点群から法線ベクトルを計算する関数
        /// </summary>
        /// <param name="shape">2次元同一平面上の頂点群</param>
        /// <returns>法線ベクトル(Vector3)</returns>
        public static Vector3 NormalVectorFromShape(PlainShape shape)
        {
            var n = shape.layouts[0].length; // hullの頂点数
            var normal = Vector3.zero;
            var iGeom = IntGeom.DefGeom;

            for (var i = 0; i < n; i++)
            {
                var p0 = shape.points[i];
                var p1 = shape.points[(i + 1) % n];
                var p2 = shape.points[(i + 2) % n];

                var edge1 = iGeom.Float(p1 - p0);
                var edge2 = iGeom.Float(p2 - p0);

                var cross = Vector3.Cross(edge1, edge2);
                if (cross.magnitude > 0)
                {
                    normal += cross;
                }
            }

            return normal.normalized;
        }
    }
}
