using Models;

namespace HiddenVilla.Client.Service.IService;

public interface IStripePaymentService
{
    public Task<SuccessModel> CheckOut(StripePaymentDTO model);
}
