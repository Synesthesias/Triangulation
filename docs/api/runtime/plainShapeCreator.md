# PlainShapeCreator
hull(Vector2[])とholes(Vector2[][])からiShape.GeometryのPlainShapeを作成するクラス

---
Syntax
```csharp
public class PlainShapeCreator
```

### Methods
#### CreatePlainShape(Vector2[],Vector2[][],IntGeom,Allocator)
IShapeをPlainShapeに変換する

Declaration
```csharp
public PlainShape CreatePlainShape(
            Vector2[] hull,
            Vector2[][] holes,
            IntGeom iGeom,
            Allocator allocator)
```
Parameters

| Type        | Name      | Description |
|-------------|-----------|-------------|
| Vector2[]   | hull      ||
| Vector2[][] | holes     ||
| IntGeom     | iGeom     ||
| Allocator   | allocator ||

Returns

| Type       | Description |
|------------|----------|
| PlainShape ||