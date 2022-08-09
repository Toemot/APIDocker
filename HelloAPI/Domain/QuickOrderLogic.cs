using HelloAPI.ApiModel;
using HelloAPI.Interfaces;

namespace HelloAPI.Domain
{
    public class QuickOrderLogic : IQuickOrderLogic
    {
        private readonly ILogger<QuickOrderLogic> _logger;

        public QuickOrderLogic(ILogger<QuickOrderLogic> logger)
        {
            _logger = logger;
        }

        public Guid PlaceQuickOrder(QuickOrder order, int customerId)
        {
            _logger.LogInformation("Placing order");

            return Guid.NewGuid();
        }
    }
}
