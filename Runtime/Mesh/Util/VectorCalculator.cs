using System;
using iShape.Geometry.Container;
using iShape.Geometry;
using UnityEngine;
using System.Linq;

namespace iShape.Triangulation.Runtime
{
    /// <summary>
    /// ベクトル計算を行うクラス
    /// 回転行列の適用や法線ベクトルの計算、XY平面投影時に必要な角度の計算などを提供
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
		/// 最初の頂点をoriginとした相対座標に変更する
        /// </summary>
        /// <param name="hull">hullの頂点配列</param>
        /// <param name="origin">最初の頂点座標(原点)</param>>
        /// <returns>調整後のhull</returns>
        public static Vector3[] GetAdjustedHull(Vector3[] hull, Vector3 origin)
        {
            return hull.Select(vertex => vertex - origin).ToArray();
        }
        
        /// <summary>
		/// 最初の頂点をoriginとした相対座標に変更する
        /// </summary>
        /// <param name="holes">holesの頂点配列</param>
        /// <param name="origin">最初の頂点座標(原点)</param>>
        /// <returns>調整後のHoles</returns>
        public static Vector3[][] GetAdjustedHoles(Vector3[][] holes, Vector3 origin)
        {
            return holes?
                .Select(hole => hole?.Select(vertex => vertex - origin).ToArray() ?? Array.Empty<Vector3>())
                .ToArray();
        }

        /// <summary>
        /// 指定した角度の逆回転行列を取得
        /// </summary>
        /// <param name="rotation">Quaternion</param>
        /// <returns>逆回転行列</returns>
        public static Matrix4x4 GetInvertRotationMatrix(Quaternion rotation)
        {
            var rotationMatrix = Matrix4x4.Rotate(rotation);
            return rotationMatrix.inverse;
        }

        /// <summary>
        /// 指定した逆回転行列を適用して頂点配列を元の位置に戻す
        /// </summary>
        /// <param name="vertices">回転された頂点配列</param>
        /// <param name="inverseMatrix">適用する逆回転行列</param>
        /// <returns>元の位置に戻した頂点配列</returns>
        public static Vector3[] GetRestoredVertices(Vector3[] vertices, Matrix4x4 inverseMatrix)
        {
            return vertices.Select(vertex => RotateByMatrix(vertex, inverseMatrix)).ToArray();
        }

        /// <summary>
        /// Vector3の頂点配列からVector2の頂点配列を取得(Hull版)
        /// (x,y,z) -> (x,y,0)
        /// </summary>
        /// <param name="hullVertices">Vector3の頂点配列</param>
        /// <returns>Vector2に変換された頂点配列</returns>
        public static Vector2[] GetHullVertices2d(Vector3[] hullVertices)
        {
            return hullVertices.Select(vertex => new Vector2(vertex.x, vertex.y)).ToArray();
        }

        /// <summary>
        /// Vector3の頂点配列からVector2の頂点配列を取得(Holes版)
		/// (x,y,z) -> (x,y,0)
        /// </summary>
        /// <param name="holesVertices">Vector3の多次元配列/param>
        /// <returns>Vector2に変換された穴の頂点リストの配列</returns>
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
        /// 頂点配列の法線ベクトルをVector3.backへ投影するのに必要なQuaternionを取得
        /// </summary>
        /// <param name="vertices">頂点配列</param>
        /// <returns>回転量(Quaternion)</returns>
        public static Quaternion GetQuaternionFromVertices(Vector3[] vertices)
        {
            var normal = NormalVectorFrom3d(vertices);

            return Quaternion.FromToRotation(normal, Vector3.back);
        }

        /// <summary>
        /// 指定したQuaternionを適用して頂点配列を回転させる
        /// </summary>
        /// <param name="vertices">回転対象の頂点配列</param>
        /// <param name="rotation">Quaternion</param>
        /// <returns>回転後の頂点配列</returns>
        public static Vector3[] GetRotatedVertices(Vector3[] vertices, Quaternion rotation)
        {
            var rotationMatrix = Matrix4x4.Rotate(rotation);
            return vertices.Select(vertex => RotateByMatrix(vertex, rotationMatrix)).ToArray();
        }
        
        /// <summary>
        /// Vector3の頂点配列から法線ベクトルを計算する
        /// </summary>
        /// <param name="vertices">対象の頂点配列</param>
        /// <returns>計算された法線ベクトル</returns>
        private static Vector3 NormalVectorFrom3d(Vector3[] vertices)
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
        /// Vector2の頂点配列から法線ベクトルを計算する
        /// </summary>
        /// <param name="vertices">対象の頂点配列</param>
        /// <returns>計算された法線ベクトル</returns>
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
        /// 2次元同一平面上の頂点配列から法線ベクトルを計算する関数
        /// </summary>
        /// <param name="shape">2次元同一平面上の頂点配列</param>
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
