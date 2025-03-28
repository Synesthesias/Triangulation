# Namespace iShape.Triangulation.Validation
### Classes
#### [BuildDelaunayException](buildDelaunayException.md)
ドロネー図作成エラー
#### [ShapeValidatorExt](shapeValidatorExt.md)
頂点のバリデーション結果を取得するクラス
#### [ValidationResult](validationResult.md)
バリデーション結果を表すEnum
#### [ValidationResultMessage](validationResultMessage.md)
指定されたバリデーション結果に対応する説明分を返却するクラス

---
使用例
```csharp
// PlainShape shapeからバリデーション結果を取得(Overlap or CounterClockWise or Valid)
var validationResult = ShapeValidatorExt.GetValidationResult(shape);

if (validationResult != ValidationResult.Valid)
{
    // ドロネー図作成エラー
    // ValidationResult.Valid以外でドロネー図のビルドを行おうとするとクラッシュする
    throw new BuildDelaunayException(
        ValidationResultMessage.GetValidationContext(validationResult));
}
```