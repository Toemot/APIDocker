using HelloAPI.ApiModel;
using HelloAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HelloAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuickOrderController : ControllerBase
    {
        private readonly ILogger<QuickOrderController> _logger;
        private readonly IQuickOrderLogic _orderLogic;

        public QuickOrderController(ILogger<QuickOrderController> logger, IQuickOrderLogic orderLogic)
        {
            _logger = logger;
            _orderLogic = orderLogic;
        }

        [HttpPost]
        public Guid SubmitQuickOrder(QuickOrder order)
        {
            return _orderLogic.PlaceQuickOrder(order, 1234);
        }
    }
}
