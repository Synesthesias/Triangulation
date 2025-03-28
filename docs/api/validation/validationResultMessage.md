# ValidationResultMessage
指定されたバリデーション結果に対応する説明分を返却するクラス

---

Syntax
```csharp
public static class ValidationResultMessage
```

### Methods
#### GetValidationContext(ValidationResult)
指定されたバリデーション結果に対応する説明文を取得

Declaration
```csharp
public static string GetValidationContext(ValidationResult result)
```
Parameters

| Type             | Name   | Description |
|------------------|--------|-------------|
| ValidationResult | result | バリデーション結果   |

Returns

| Type   | Description            |
|--------|------------------------|
| string | バリデーション結果に対応する説明文の文字列  |

