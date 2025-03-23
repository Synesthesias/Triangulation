namespace iShape.Triangulation.Validation
{
    public static class ValidationContext
    {
        /// <summary>
        /// 指定されたバリデーション結果に対応する説明文を取得
        /// </summary>
        /// <param name="result">バリデーション結果</param>
        /// <returns>バリデーション結果に対応する説明文の文字列</returns>
        public static string GetValidationContext(ValidationResult result)
        {
            var context = "";
            switch(result)
            {
                case ValidationResult.None:
                    context = "バリデーションは無効です";
                    break;
                case ValidationResult.OverLap:
                    context = "頂点が重複しています";
                    break;
                case ValidationResult.CounterClockwise:
                    context = "頂点が反時計回りに格納されています";
                    break;
                default:
                    break;
            }

            return context;
        }
    }
}