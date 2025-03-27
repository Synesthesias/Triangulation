# 使い方
## 頂点データの編集
- TestFromVertices3dのdata3dの中身を変更することで、色々な図形を描画させることができます。
- HullとHolesの頂点座標のCW/CCWは以下のどちらかの組み合わせである必要があります。
    - Hull:CW, Holes:CCW
    - Hull:CCW, Holes:CW 
- HullとHoles共にCWまたはCCWである場合、Holes部分が表示されません。

```csharp
public class TestFromVertices3d : MonoBehaviour
{
    [SerializeField]
    private MeshView meshViewTemplate;

    private CancellationToken cancellationToken;

    private TestShape3d[] data3d = new TestShape3d[]
    {
        new TestShape3d
        {
            Hull = new Vector3[]
            {
                //CW
                new Vector3(0, 0, 0),
                new Vector3(-30, 0, 0),
                new Vector3(-30, 30, 0),
                new Vector3(0, 30, 0)
            },
            Holes = new Vector3[][]
            {
                //CCW
                new Vector3[]
                {
                    new Vector3(-10, 20, 0),
                    new Vector3(-20, 20, 0),
                    new Vector3(-20, 10, 0),
                    new Vector3(-10, 10, 0)
                }
            }
        }
    };

    /*以下略 -> 動作確認に記載されているTestFromVertices3d.cs参照*/
}
```

## 追加した機能
※リンク未対応(PRマージ後対応予定)
### 頂点のバリデーションに関する機能
#### [ShapeValidatorExt.cs](https://www.google.com/)(iShape.Triangulation.Validation)
頂点のバリデーション結果を取得するクラス
#### [BuildDelaunayException.cs](https://www.google.com/)(iShape.Triangulation.Validation)
ドロネー図作成エラー
#### [ValidationResultMessage.cs](https://www.google.com/)(iShape.Triangulation.Validation)
指定されたバリデーション結果に対応する説明分を返却するクラス
#### [ValidationResult.cs](https://www.google.com/)(iShape.Triangulation.Validation)
バリデーション結果を表すEnum

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
### 3次元座標の対応
#### [ShapeMeshCreatorExt.cs](https://www.google.com/)(iShape.Triangulation.Runtime)
3次元座標からTriangulationを行い、メッシュを生成するクラス
#### [PlainShapeCreator.cs](https://www.google.com/)(iShape.Triangulation.Runtime)
hull(Vector2[])とholes(Vector2[][])からiShape.GeometryのPlainShapeを作成するクラス
#### [VectorCalculator.cs](https://www.google.com/)(iShape.Triangulation.Runtime)
ベクトル計算を行うクラス
回転行列の適用や法線ベクトルの計算、XY平面投影時に必要な角度の計算などを提供

## データサンプル
Cube
```csharp
private TestShape3d[] data3d = new TestShape3d[]
{
    new TestShape3d
    {
        Hull = new Vector3[]
        {
            //CCW
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 30),
            new Vector3(-30, 0, 30),
            new Vector3(-30, 0, 0)
        }
    },
    new TestShape3d
    {
        Hull = new Vector3[]
        {
            //CW
            new Vector3(0, 0, 0),
            new Vector3(-30, 0, 0),
            new Vector3(-30, 30, 0),
            new Vector3(0, 30, 0)
        }
    },
    new TestShape3d
    {
        Hull = new Vector3[]
        {
            //CW
            new Vector3(0, 0, 0),
            new Vector3(0, 30, 0),
            new Vector3(0, 30, 30),
            new Vector3(0, 0, 30)
        }
    },
    new TestShape3d
    {
        Hull = new Vector3[]
        {
            //CCW
            new Vector3(-30, 0, 0),
            new Vector3(-30, 0, 30),
            new Vector3(-30, 30, 30),
            new Vector3(-30, 30, 0)
        }
    },
    new TestShape3d
    {
        Hull = new Vector3[]
        {
            //CCW
            new Vector3(0, 0, 30),
            new Vector3(0, 30, 30),
            new Vector3(-30, 30, 30),
            new Vector3(-30, 0, 30)
        }
    },
    new TestShape3d
    {
        Hull = new Vector3[]
        {
            //CW
            new Vector3(0, 30, 0),
            new Vector3(-30, 30, 0),
            new Vector3(-30, 30, 30),
            new Vector3(0, 30, 30)
        }
    }
};
```