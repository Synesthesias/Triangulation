# Class VectorCalculator
ベクトル計算を行うクラス
回転行列の適用や法線ベクトルの計算、XY平面投影時に必要な角度の計算などを提供

---
Syntax
```csharp
public static class VectorCalculator
```

### Methods
#### RotateByMatrix(Vector3, Matrix4x4)
指定した回転行列を使用してベクトルを回転

Declaration
```csharp
private static Vector3 RotateByMatrix(Vector3 vertex, Matrix4x4 rotationMatrix)
```
Parameters

| Type      | Name           | Description      |
|-----------|----------------|------------------|
| Vector3   | vertex         | 回転させる3D空間上の頂点座標  |
| Matrix4x4 | rotationMatrix | 適用する回転行列         |

Returns

| Type    | Description |
|---------|-------------|
| Vector3 | 回転後の頂点座標    |

#### GetAdjustedHull(Vector3[], Vector3)
最初の頂点をoriginとした相対座標に変更する(Hull版)

Declaration
```csharp
public static Vector3[] GetAdjustedHull(Vector3[] hull, Vector3 origin)
```
Parameters

| Type      | Name   | Description  |
|-----------|--------|--------------|
| Vector3[] | hull   |              |
| Vector3   | origin | 最初の頂点座標(原点)  |

Returns

| Type      | Description |
|-----------|-------------|
| Vector3[] | 調整後のhull    |

#### GetAdjustedHoles(Vector3[][], Vector3)
最初の頂点をoriginとした相対座標に変更する

Declaration
```csharp
public static Vector3[][] GetAdjustedHoles(Vector3[][] holes, Vector3 origin)
```
Parameters

| Type        | Name   | Description |
|-------------|--------|-------------|
| Vector3[][] | holes  |             |
| Vector3     | origin | 最初の頂点座標(原点) |

Returns

| Type        | Description |
|-------------|-------------|
| Vector3[][] | 調整後のholes   |

#### GetInvertRotationMatrix(Quaternion)
指定した角度の逆回転行列を取得

Declaration
```csharp
public static Matrix4x4 GetInvertRotationMatrix(Quaternion rotation)
```
Parameters

| Type        | Name     | Description |
|-------------|----------|-------------|
| Quaternion  | rotation |             |

Returns

| Type        | Description |
|-------------|-------------|
| Matrix4x4   | 逆回転行列       |

#### GetRestoredVertices(Vector3[], Matrix4x4)
指定した逆回転行列を適用して頂点群を元の位置に戻す

Declaration
```csharp
public static Vector3[] GetRestoredVertices(Vector3[] vertices, Matrix4x4 inverseMatrix)
```
Parameters

| Type      | Name          | Description  |
|-----------|---------------|--------------|
| Vector3[] | vertices      | 回転された頂点配列    |
| Matrix4x4 | inverseMatrix | 適用する逆回転行列    |

Returns

| Type    | Description |
|---------|-------------|
| Vector3 | 回転後の頂点座標    |

#### GetHullVertices2d(Vector3[])
Vector3の頂点配列からVector2の頂点配列を取得(Hull版)

Declaration
```csharp
public static Vector2[] GetHullVertices2d(Vector3[] hullVertices)
```
Parameters

| Type      | Name           | Description  |
|-----------|----------------|--------------|
| Vector3[] | hullVertices   | Vector3の頂点配列 |

Returns

| Type      | Description            |
|-----------|------------------------|
| Vector2[] | Vector2に変換されたhullの頂点配列 |

#### GetHolesVertices2d(Vector3[][])
Vector3の頂点配列からVector2の頂点配列を取得(Holes版)

Declaration
```csharp
public static Vector2[][] GetHolesVertices2d(Vector3[][] holesVertices)
```
Parameters

| Type        | Name          | Description      |
|-------------|---------------|------------------|
| Vector3[][] | holesVertices | Vector3の多次元配列    |

Returns

| Type        | Description              |
|-------------|--------------------------|
| Vector2[][] | Vector2に変換されたholesの多次元配列 |

#### GetQuaternionFromVertices(Vector3[])
頂点群の法線ベクトルをVector3.backへ投影するのに必要なQuaternionを取得

Declaration
```csharp
public static Quaternion GetQuaternionFromVertices(Vector3[] vertices)
```
Parameters

| Type      | Name     | Description |
|-----------|----------|-------------|
| Vector3[] | vertices | 回転対象の頂点配列   |

Returns

| Type        | Description |
|-------------|-------------|
| Quaternion  |             |

#### GetRotatedVertices(Vector3[], Quaternion)
指定したQuaternionを適用して頂点配列を回転させる

Declaration
```csharp
public static Vector3[] GetRotatedVertices(Vector3[] vertices, Quaternion rotation)
```
Parameters

| Type            | Name     | Description  |
|-----------------|----------|--------------|
| Vector3         | vertex   | 回転対象の頂点配列    |
| Quaternion      | rotation | 適用する回転行列     |

Returns

| Type    | Description |
|---------|-------------|
| Vector3 | 回転後の頂点配列    |

#### NormalVectorFrom3d(Vector3[])
Vector3の頂点配列から法線ベクトルを計算する

Declaration
```csharp
private static Vector3 NormalVectorFrom3d(Vector3[] vertices)
```
Parameters

| Type      | Name           | Description |
|-----------|----------------|-------------|
| Vector3[] | vertices       | 頂点配列        |

Returns

| Type    | Description |
|---------|-------------|
| Vector3 | 法線ベクトル      |