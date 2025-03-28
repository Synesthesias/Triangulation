# ShapeValidatorExt
頂点のバリデーション結果を取得するクラス

---
Syntax
```csharp
public static class ShapeValidatorExt
```

### Methods
#### GetValidationResult(PlainShape)
頂点のバリデーションの結果を返す関数

Declaration
```csharp
public static ValidationResult GetValidationResult(PlainShape shape)
```
Parameters

| Type       | Name  | Description  |
|------------|-------|--------------|
| PlainShape | shape | 2次元同一平面上の頂点群 |

Returns

| Type             | Description |
|------------------|-------------|
| ValidationResult | バリデーションの結果  |

#### IsOverlappingVertices(PlainShape)
頂点の重複を検知する関数

Declaration
```csharp
private static bool IsOverlappingVertices(PlainShape shape)
```
Parameters

| Type       | Name  | Description  |
|------------|-------|--------------|
| PlainShape | shape | 2次元同一平面上の頂点群 |

Returns

| Type | Description |
|------|-------------|
| bool | 頂点が重複しているか  |

#### IsCounterClockwise(PlainShape)
頂点座標が反時計回りかどうかを判定する関数

Declaration
```csharp
private static bool IsCounterClockwise(PlainShape shape)
```
Parameters

| Type       | Name  | Description  |
|------------|-------|--------------|
| PlainShape | shape | 2次元同一平面上の頂点群 |

Returns

| Type | Description |
|------|-------------|
| bool | 頂点座標が反時計周りかどうか  |