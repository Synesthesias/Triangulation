﻿using iShape.Geometry;
using Unity.Collections;
using iShape.Mesh2d;
using iShape.Triangulation.Shape.Delaunay;
using Unity.Mathematics;
using UnityEngine;
using System.Linq;
using System;

namespace iShape.Triangulation.Runtime
{
    /// <summary>
    /// 3次元座標からTriangulationを行い、メッシュを生成するクラス
    /// </summary>
    public class ShapeMeshCreatorExt
    {
        /// <summary>
        /// メッシュを生成する
        /// </summary>
        public Mesh CreateMesh(
            Vector3[] hull,
            Vector3[][] holes)
        {
            if (hull.Length < 1)
            {
                Debug.LogWarning("頂点が1つもありません");
                return null;
            }
            
            // 各頂点のオフセットを調整
            var origin = hull[0];
            hull = VectorCalculator.GetAdjustedHull(
                hull: hull, 
                origin: origin);
            holes = VectorCalculator.GetAdjustedHoles(
                holes: holes,
                origin: origin);
            
            // メッシュを生成する用の頂点座標を設定
            var rotation = VectorCalculator.GetQuaternionFromVertices(hull);

            var rotatedHullVertices = VectorCalculator.GetRotatedVertices(
                vertices: hull, 
                rotation: rotation);
        
            var rotatedHolesVertices = holes?.Select(hole => VectorCalculator.GetRotatedVertices(
                vertices: hole, 
                rotation: rotation)).ToArray();
            
            var iGeom = IntGeom.DefGeom;

            var vector2Hull = VectorCalculator.GetHullVertices2d(rotatedHullVertices);
            var vector2Holes = VectorCalculator.GetHolesVertices2d(rotatedHolesVertices);
            
            var plainShapeCreator = new PlainShapeCreator();
            var pShape = plainShapeCreator.CreatePlainShape(
                hull: vector2Hull,
                holes: vector2Holes,
                iGeom: iGeom,
                Allocator.Persistent);

            var extraPoints = new NativeArray<IntVector>(0, Allocator.Persistent);

            var minX = hull.Min(v => v.x);
            var maxX = hull.Max(v => v.x);
            var minY = hull.Min(v => v.y);
            var maxY = hull.Max(v => v.y);
            
            var width = maxX - minX;
            var height = maxY - minY;
            var maxDimension = math.max(width, height);
            var maxEdgeValue = maxDimension * 0.5F; // 2分の1の長さ

            maxEdgeValue = math.clamp(maxEdgeValue, 0.5F, 10F);
            var maxEdge = iGeom.Int(maxEdgeValue);

            var delaunay = pShape.Delaunay(
                maxEdge: maxEdge,
                extraPoints: extraPoints,
                allocator: Allocator.Persistent);

            pShape.Dispose();

            var totalArea = width * height;
            var maxAreaValue = totalArea * 0.5F; // 2分の1の面積
            maxAreaValue = math.clamp(maxAreaValue, 0.5F, 50F);
            var maxArea = iGeom.Int(maxAreaValue);

            delaunay.Tessellate(iGeom, maxArea);

            extraPoints.Dispose();

            var triangles = delaunay.Indices(Allocator.Persistent);
            var vertices = delaunay.Vertices(Allocator.Persistent, iGeom, 0);

            delaunay.Dispose();
            // set each triangle as a separate mesh

            var subVertices = new NativeArray<float3>(3, Allocator.Persistent);
            var subIndices = new NativeArray<int>(new[] { 0, 1, 2 }, Allocator.Persistent);

            var colorMesh = new NativeColorMesh(triangles.Length, Allocator.Persistent);

            for (int i = 0; i < triangles.Length; i += 3)
            {
                for (int j = 0; j < 3; j += 1)
                {
                    var v = vertices[triangles[i + j]];
                    subVertices[j] = new float3(v.x, v.y, v.z);
                }

                var subMesh = new StaticPrimitiveMesh(subVertices, subIndices, Allocator.Persistent);

                colorMesh.AddAndDispose(subMesh, Color.black);
            }
            
            subIndices.Dispose();
            subVertices.Dispose();
            vertices.Dispose();
            triangles.Dispose();

            var mesh = new Mesh();
            mesh.MarkDynamic();
            colorMesh.FillAndDispose(mesh);

            // verticesに渡す頂点を作成
            var invertRotationMatrix = VectorCalculator.GetInvertRotationMatrix(
                rotation: rotation);

            var restoredVertices = VectorCalculator.GetRestoredVertices(
                mesh.vertices,
                invertRotationMatrix);

            mesh.vertices = restoredVertices;
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();

            return mesh;
        }
    }
    
}