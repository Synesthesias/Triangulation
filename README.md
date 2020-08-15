# Triangulation [Port](https://github.com/iShape-Swift/iShapeTriangulation/)
Complex polygon triangulation, tessellation and split into convex polygons. A fast O(n*log(n)) algorithm based on "Triangulation of monotone polygons". The result can be represented as a Delaunay triangulation.
## Delaunay triangulation
<p align="center">
<img src="https://github.com/iShapeUnity/Triangulation/tree/master/Readme/star_triangle.svg" width="500"/>
</p>

## Triangulation with extra points
<p align="center">
<img src="https://github.com/iShape-Swift/iShapeTriangulation/blob/master/Readme/eagle_triangles_extra_points.svg" width="800" />
</p>

## Tessellation
<p align="center">
<img src="https://github.com/iShape-Swift/iShapeTriangulation/blob/master/Readme/eagle_tessellation.svg" width="800" />
</p>

## Centroid net
<p align="center">
<img src="https://github.com/iShape-Swift/iShapeTriangulation/blob/master/Readme/eagle_centroid.svg" width="800" />
</p>

## Features

💡 Fast O(n*log(n)) algorithm based on "Triangulation of monotone polygons"

💡 All code is written to suit "Data Oriented Design". No reference type like class, just structs.

💡 Supports polygons with holes

💡 Supports plain and Delaunay triangulation

💡 Supports tesselation

💡 Supports building centroid net

💡 Same points is not restricted

💡 Polygon must not have self intersections

💡 Use integer geometry for calculations

💡 More then 100 tests

