# Class ShapeMeshCreatorExt
3次元座標からTriangulationを行い、メッシュを生成するクラス

---
Syntax
```csharp
public class ShapeMeshCreatorExt
```

### Methods
#### CreateMesh(Vector3[],Vector3[][])
メッシュを生成する

Declaration
```csharp
public Mesh CreateMesh(
            Vector3[] hull,
            Vector3[][] holes)
```
Parameters

| Type        | Name      | Description |
|-------------|-----------|-------------|
| Vector3[]   | hull      ||
| Vector3[][] | holes     ||

Returns

| Type | Description |
|------|----------|
| Mesh ||