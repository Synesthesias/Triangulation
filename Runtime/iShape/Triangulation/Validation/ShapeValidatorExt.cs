using iShape.Geometry.Container;
using iShape.Triangulation.Runtime;
using System.Collections.Generic;
using iShape.Geometry;
using UnityEngine;

namespace iShape.Triangulation.Validation
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
            var normal = VectorCalculator.NormalVectorFromShape(shape:shape);
            var normalXY = new Vector3(0, 0, -1).normalized; //2つのベクトルの内積が-1の時，反時計まわり
            var isCounterClockwise = (Vector3.Dot(normal, normalXY) <= -1);
            
            return isCounterClockwise;
        }
    }
}