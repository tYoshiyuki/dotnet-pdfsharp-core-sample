using System.Net;
using DotNetPdfSharpCoreSample.Lib.Interfaces;
using DotNetPdfSharpCoreSample.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetPdfSharpCoreSample.Controllers
{
    /// <summary>
    /// PDFマージコントローラ
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PdfController : Controller
    {
        private readonly IPdfSharpService _pdfSharpService;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="pdfSharpService"><see cref="IPdfSharpService"/></param>
        public PdfController(IPdfSharpService pdfSharpService)
        {
            _pdfSharpService = pdfSharpService;
        }

        /// <summary>
        /// PDFファイルリストをマージします。
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("MergePdfList")]
        [Produces("application/octet-stream", Type = typeof(FileResult))]
        public async Task<IActionResult> MergePdfList([FromForm] MergePdfListRequest request)
        {
            IEnumerable<Stream> inputStreamList = Enumerable.Empty<Stream>();

            try
            {
                if (request.FileList.Count < 2)
                {
                    return Problem("Number of files must be 2 or more.", statusCode: (int)HttpStatusCode.BadRequest);
                }

                IEnumerable<Task<MemoryStream>> inputTask = request.FileList.Select(async x =>
                {
                    var inputStream = new MemoryStream();
                    await x.CopyToAsync(inputStream);

                    return inputStream;
                });

                inputStreamList = await Task.WhenAll(inputTask);

                // マージ処理の実行
                Stream outputStream = _pdfSharpService.Merge(inputStreamList);

                return File(outputStream, "application/octet-stream", fileDownloadName: "Merged" + DateTime.Now.Ticks + ".pdf");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                throw;
            }
            finally
            {
                inputStreamList.ToList().ForEach(x => x.Dispose());
            }
        }
    }
}
