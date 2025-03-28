﻿using iShape.Geometry.Container;
using System.Collections.Generic;
using iShape.Geometry;
using iShape.Triangulation.Validation;
using System;
using UnityEngine;

namespace iShape.Triangulation.Extension
{   
    public static class ShapeValidatorExt
    {
        /// <summary>
        /// 頂点のバリデーションの結果を返す関数
        /// </summary>
        /// <param name="shape">2次元同一平面上の頂点群</param>
        /// <returns>バリデーションの結果</returns>
        public static ValidationResult GetValidationResult(PlainShape shape)
        {
            if (IsOverlappingVertices(shape)) return ValidationResult.OverLap;
            else if (IsCounterClockwise(shape)) return ValidationResult.CounterClockwise;
            
            return ValidationResult.Valid;
        }
        
        /// <summary>
        /// 頂点の重複を検知する関数
        /// shape.pointsにはhullとholesの両方の頂点が格納される
        /// </summary>
        /// <returns>頂点が重複しているか</returns>
        private static bool IsOverlappingVertices(PlainShape shape)
        {
            var hashset = new HashSet<IntVector>();
            var overlap = false;

            foreach (var p in shape.points)
            {
                if (hashset.Add(p)) continue;
                
                overlap = true;
                break;
            }
            
            return overlap;
        }

        /// <summary>
        /// 頂点座標が反時計回りかどうかを判定する関数
        /// </summary>
        /// /// <returns>頂点座標が反時計周りかどうか</returns>
        private static bool IsCounterClockwise(PlainShape shape)
        {
            var normal = NormalVectorFromShape(shape:shape);
            var normalXY = new Vector3(0, 0, -1).normalized; //2つのベクトルの内積が-1の時，反時計まわり
            var isCounterClockwise = (Vector3.Dot(normal, normalXY) <= -1);
            
            return isCounterClockwise;
        }

        /// <summary>
        /// shapeのXZ平面に対する角度がXY平面上(2D上)でメッシュ生成することのできない角度(無効な角度)かどうかを判定する関数
        /// </summary>
        /// <param name="shape"></param>
        /// <returns>shapeのXZ平面に対する角度が2度以下または178以上かどうか</returns>
        private static bool IsInvalidAngle(PlainShape shape)
        {
            var normal = NormalVectorFromShape(shape);
            var angle =  Vector3.Angle(normal,Vector3.up);
            var isInvalidAngle = (Math.Abs(angle) <= 2 || Math.Abs(angle) >= 178);

            return isInvalidAngle;
        } 
        
        /// <summary>
        // /// 2次元同一平面上の頂点群から法線ベクトルを計算する関数
        // /// </summary>
        // /// <param name="shape">2次元同一平面上の頂点群</param>
        // /// <returns>法線ベクトル(Vector3)</returns>
        private static Vector3 NormalVectorFromShape(PlainShape shape)
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