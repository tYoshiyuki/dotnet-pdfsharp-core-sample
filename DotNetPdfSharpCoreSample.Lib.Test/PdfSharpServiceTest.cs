using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetPdfSharpCoreSample.Lib.Test
{
    /// <summary>
    /// <see cref="PdfSharpService"/> のテストクラスです。
    /// </summary>
    [TestClass]
    public class PdfSharpServiceTest
    {
        /// <summary>
        /// <see cref="PdfSharpService.Merge"/> が正常に動作することを確認します。
        /// </summary>
        [TestMethod]
        public void Merge_Ok()
        {
            // Arrange
            var service = new PdfSharpService();

            using FileStream fileStream1 = File.OpenRead(Path.Combine(Directory.GetCurrentDirectory(), "Sample-1.pdf"));
            using FileStream fileStream2 = File.OpenRead(Path.Combine(Directory.GetCurrentDirectory(), "Sample-2.pdf"));

            // Act
            using Stream merged = service.Merge(new List<Stream> { fileStream1, fileStream2 });

            // Assert
            Assert.AreEqual(3058, merged.Length);
        }

        /// <summary>
        /// <see cref="PdfSharpService.Merge"/> が正常に動作することを確認します。
        /// 対象ファイルが3ファイルのパターン
        /// </summary>
        [TestMethod]
        public void Merge_Ok_ThreeFiles()
        {
            // Arrange
            var service = new PdfSharpService();

            using FileStream fileStream1 = File.OpenRead(Path.Combine(Directory.GetCurrentDirectory(), "Sample-1.pdf"));
            using FileStream fileStream2 = File.OpenRead(Path.Combine(Directory.GetCurrentDirectory(), "Sample-2.pdf"));
            using FileStream fileStream3 = File.OpenRead(Path.Combine(Directory.GetCurrentDirectory(), "Sample-3.pdf"));

            // Act
            using Stream merged = service.Merge(new List<Stream> { fileStream1, fileStream2, fileStream3 });

            // Assert
            Assert.AreEqual(4190, merged.Length);

            using FileStream fs = new("result.pdf", FileMode.Create);
            merged.CopyTo(fs);
        }
    }
}
