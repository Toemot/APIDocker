using HelloAPI.ApiModel;

namespace HelloAPI.Interfaces
{
    public interface IQuickOrderLogic
    {
        Guid PlaceQuickOrder(QuickOrder order, int customerId);
    }
}