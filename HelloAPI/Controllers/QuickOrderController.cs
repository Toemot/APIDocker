using HelloAPI.ApiModel;
using HelloAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace HelloAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuickOrderController : ControllerBase
    {
        private readonly IQuickOrderLogic _orderLogic;

        public QuickOrderController(IQuickOrderLogic orderLogic)
        {
            _orderLogic = orderLogic;
        }

        [HttpPost]
        public Guid SubmitQuickOrder(QuickOrder order)
        {
            Log.Information("Starting the Submit Order controller {order}", order);

            return _orderLogic.PlaceQuickOrder(order, 1234);
        }
    }
}
