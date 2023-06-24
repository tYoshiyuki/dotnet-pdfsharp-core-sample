namespace DotNetPdfSharpCoreSample.Lib.Interfaces;

/// <summary>
/// <see cref="PdfSharpCore"/> を用いたサービスのインターフェースです。
/// </summary>
public interface IPdfSharpService
{
    /// <summary>
    /// PDFファイルリストをマージします。
    /// </summary>
    /// <param name="streams">マージ対象のストリームリスト</param>
    /// <returns>変換結果のストリーム</returns>
    Stream Merge(IEnumerable<Stream> streams);
}
