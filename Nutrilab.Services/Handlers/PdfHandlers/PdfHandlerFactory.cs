using Nutrilab.Shared.Enums;

namespace Nutrilab.Services.Handlers.PdfHandlers
{
    public sealed class PdfHandlerFactory(IEnumerable<IPdfHandler> handlers)
    {
        public IPdfHandler GetHandler(PdfReportType type)
        {
            return handlers.FirstOrDefault(h => h.ReportType == type)
                ?? throw new NotImplementedException($"PDF handler for '{type}' is not implemented.");
        }
    }
}
