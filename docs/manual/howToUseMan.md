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

    /*実装略*/
}
```

## 追加した機能
※リンク未対応(PRマージ後対応予定)
### 頂点のバリデーションに関する機能
#### [ShapeValidatorExt.cs](https://www.google.com/)(iShape.Triangulation.Extension)
#### [BuildDelaunayException.cs](https://www.google.com/)(iShape.Triangulation.Validation)
#### [ValidaitonContext.cs](https://www.google.com/)(iShape.Triangulation.Validation)
#### [ValidationResult.cs](https://www.google.com/)(iShape.Triangulation.Validation)
### 3次元座標の対応
#### [ShapeMeshCreatorExt.cs](https://www.google.com/)(iShape.Triangulation.Runtime)
#### [PlainShapeCreator.cs](https://www.google.com/)(iShape.Triangulation.Runtime)
#### [VectorCalculator.cs](https://www.google.com/)(iShape.Triangulation.Runtime)

## データサンプル
- Cube
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