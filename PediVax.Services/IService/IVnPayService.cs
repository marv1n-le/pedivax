using Microsoft.AspNetCore.Http;
using PediVax.BusinessObjects.DTO.PaymentDTO;
using PediVax.BusinessObjects.DTO.VnPayDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.Services.IService
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(HttpContext context, VnPaymentRequestModel model);
        VnPaymentResponseModel PaymentExecute(IQueryCollection collections);
    }
}
