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
[API](../api/runtime/runtimeApi.md)に記載

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