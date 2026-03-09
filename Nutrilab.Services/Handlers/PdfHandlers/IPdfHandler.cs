using Nutrilab.Shared.Enums;

namespace Nutrilab.Services.Handlers.PdfHandlers
{
    public interface IPdfHandler
    {
        PdfReportType ReportType { get; }
        Task<byte[]> GenerateAsync(long id);
    }
}
