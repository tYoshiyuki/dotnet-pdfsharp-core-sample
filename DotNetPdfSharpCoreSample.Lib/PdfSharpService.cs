using DotNetPdfSharpCoreSample.Lib.Interfaces;
using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.IO;

namespace DotNetPdfSharpCoreSample.Lib
{
    /// <summary>
    /// <see cref="PdfSharpCore"/> を用いたサービスです。
    /// </summary>
    public class PdfSharpService : IPdfSharpService
    {
        /// <inheritdoc />
        public Stream Merge(IEnumerable<Stream> streams)
        {
            using var merged = new PdfDocument();

            foreach (Stream file in streams)
            {
                foreach (PdfPage? page in PdfReader.Open(file, PdfDocumentOpenMode.Import).Pages)
                {
                    merged.AddPage(page);
                }
            }

            var stream = new MemoryStream();
            merged.Save(stream, false);
            return stream;
        }
    }
}
