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
            var streamList = streams.ToList();
            if (streamList.Count < 2)
            {
                throw new ArgumentException("Number of streams must be 2 or more.", nameof(streams));
            }

            try
            {
                using var merged = new PdfDocument();

                foreach (Stream file in streamList)
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
            catch (Exception ex)
            {
                throw new PdfSharpServiceException("MergePdfList failed.", ex);
            }
        }
    }
}
