# 動作確認
## 事前準備
1. [TriangulationDebug](https://github.com/iShapeUnity/TriangulationDebug)をクローンし、Package Managerを開いてiShape.TriangulationをRemove

2. Package Managerから`Install package from git URL...`を選択

3. `https://github.com/Synesthesias/Triangulation.git` を入力 

4. Addボタンをクリック

※新規プロジェクトに導入する場合は以下のpackageも同様にインポートする
- https://github.com/iShapeUnity/Geometry.git
- https://github.com/iShapeUnity/Mesh2d.git

## 確認手順
### 頂点座標が2次元の場合
- [TessellationScene.cs](https://github.com/iShapeUnity/TriangulationDebug/blob/main/Assets/Source/TessellationScene.cs)を参照
### 頂点座標が3次元の場合
1. 空オブジェクトを作成し、MeshFilterとMeshRendererを追加
2. 1.で作成したオブジェクトに以下のスクリプトをアタッチし、このオブジェクトからPrefabを作成する。作成後、ヒエラルキー上にいる1.で作成したオブジェクトを削除する
```csharp
using UnityEngine;

/// <summary>
/// メッシュのView
/// </summary>
public class MeshView : MonoBehaviour
{
    [SerializeField] private MeshFilter meshFilter;
    [SerializeField] private MeshRenderer meshRenderer;
    
    /// <summary>
    /// メッシュのFilter
    /// </summary>
    public MeshFilter MeshFilter
        => meshFilter;

    /// <summary>
    /// メッシュのRenderer
    /// </summary>
    public MeshRenderer MeshRenderer
        => meshRenderer;
}
```

3. 再び空オブジェクトを作成し、以下のスクリプトをアタッチ
4. 2.で作成したPrefabをmeshViewTemplateにアタッチして実行
```csharp
using iShape.Triangulation.Runtime;
using UnityEngine;

///<summary>
/// Triangulationに渡す構造体
///</summary>
public struct TestShape3d
{
    public Vector3[] Hull;
    public Vector3[][] Holes;
}

///<summary>
/// 3次元座標からTriangulationを実行するサンプル
///</summary>
public class TestFromVertices3d : MonoBehaviour
{
    [SerializeField]
    private MeshView meshViewTemplate;

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

    private void Awake()
    {
        CreateMeshes();
    }
    
    /// <summary>
    /// メッシュを生成する
    /// </summary>
    private void CreateMeshes()
    {
        for(int index = 0; index < data3d.Length; index++)
        {
            var mesh = CreateMesh(
                hull: data3d[index].Hull,
                holes: data3d[index].Holes);
        
            meshViewTemplate.MeshFilter.mesh = mesh;
        
            var meshView = UnityEngine.Object.Instantiate(
                meshViewTemplate,
                data3d[index].Hull[0], // 最初の頂点をオブジェクトの原点として設定する
                Quaternion.identity);
        }
    }
    
    /// <summary>
    /// メッシュを生成する
    /// </summary>
    private Mesh CreateMesh(
        Vector3[] hull,
        Vector3[][] holes)
    {
        var shapeMeshCreator = new ShapeMeshCreatorExt();
    
        var mesh = shapeMeshCreator.CreateMesh(
            hull: hull,
            holes: holes
        );
    
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        return mesh;
    }
}   
```

4. Unityシーン上にTessellateの結果が可視化される

## トラブルシューティング
### 頂点の重複
- 頂点群が重複している場合、メッシュを生成することができません

### 頂点のCW/CCW
- XY平面上（2次元座標）では頂点の順番が時計回りである必要があります
- 3次元座標の場合は、メッシュの表裏は変わりますが反時計回りでも問題ありません
