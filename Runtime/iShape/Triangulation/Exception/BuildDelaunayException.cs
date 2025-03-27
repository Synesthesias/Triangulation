using System;

namespace iShape.Triangulation.Validation
{
    /// <summary>
    /// ドロネー図作成エラー
    /// </summary>
    public class BuildDelaunayException : Exception
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public BuildDelaunayException(string message)
            : base($"ValidationResult:{message}")
        {
        }
    }
}