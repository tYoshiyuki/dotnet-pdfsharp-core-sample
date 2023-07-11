using System.Runtime.Serialization;

namespace DotNetPdfSharpCoreSample.Lib
{
    /// <summary>
    /// <see cref="PdfSharpCore"/> を用いたサービスの例外クラス
    /// </summary>
    public class PdfSharpServiceException : Exception
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PdfSharpServiceException(){ }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="message"></param>
        public PdfSharpServiceException(string? message) : base(message) { }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public PdfSharpServiceException(string? message, Exception exception) : base(message, exception) { }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected PdfSharpServiceException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
