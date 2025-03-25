# デバッグ方法
## 事前準備
1. [TriangulationDebug](https://github.com/iShapeUnity/TriangulationDebug)をクローンし、Package Managerを開いてiShape.TriangulationをRemove

2. Package Managerから`Install package from git URL...`を選択

3. `https://github.com/iShapeUnity/Triangulation.git` を入力

4. Addボタンをクリック

※新規プロジェクトに導入する場合は以下のpackageも同様にインポートする
- https://github.com/iShapeUnity/Geometry.git
- https://github.com/iShapeUnity/Mesh2d.git

## デバッグ手順
### 頂点座標が2次元の場合
- [TessellationScene.cs](https://github.com/iShapeUnity/TriangulationDebug/blob/main/Assets/Source/TessellationScene.cs)を参照
### 頂点座標が3次元の場合
1. 空オブジェクトを作成し、MeshFilterとMeshRendererをアタッチする
2. 以下のようにコードを作成し、先程作ったオブジェクトにアタッチして実行
3. Unityシーン上にTessellateの結果が可視化される
```csharp
using Cysharp.Threading.Tasks;
using System.Threading;
using iShape.Triangulation.Runtime;
using UnityEngine;

public struct TestShape3d
{
    public Vector3[] hull;
    public Vector3[][] holes;
}

public class TestFromVertices3d : MonoBehaviour 
{
    private MeshFilter meshFilter;
    private Mesh mesh;
    private int testIndex = 0;
    private CancellationToken cancellationToken;

    public TestShape3d[] data3d = new TestShape3d[]
    {
        new TestShape3d
        {
            hull = new Vector3[]
            {
                new Vector3(0, 0, 0),
                new Vector3(0, 30, 0),
                new Vector3(15, 30, 15),
                new Vector3(15, 0, 15)
            }
        }
    };
    
    private void Awake()
    {
        Application.targetFrameRate = 60;
        this.meshFilter = gameObject.GetComponent<MeshFilter>();
        this.mesh = new Mesh();
        this.mesh.MarkDynamic();
        cancellationToken = new CancellationToken();
        
        SetTest3d(testIndex);
    }

    private async UniTask SetTest3d(int index)
    {
        var shapeMeshCreator = new ShapeMeshCreatorExt();
        var mesh = await shapeMeshCreator.CreateAsync(
            hull: data3d[index].hull,
            holes: data3d[index].holes,
            cancellationToken: cancellationToken
            );
        
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        
        this.meshFilter.mesh = mesh;
    }
}
```

## トラブルシューティング
### 頂点の重複
- 頂点群が重複している場合、メッシュを生成することができません

### 頂点のCW/CCW
- XY平面上（2次元座標）では頂点の順番が時計回りである必要があります
- 3次元座標の場合は、メッシュの表裏は変わりますが反時計回りでも問題ありません