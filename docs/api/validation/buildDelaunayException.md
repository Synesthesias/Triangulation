# Class BuildDelaunayException
ドロネー図作成エラー

---
### Syntax
```csharp
public class BuildDelaunayException : Exception
```
### Constructors
Declaration
```csharp
public BuildDelaunayException(string message)
            : base($"ValidationResult:{message}")
```
Parameters

| Type   | Name    | Description |
|--------|---------|-------------|
| string | message ||