﻿using Core.Utilities.Business.Abstract;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Linq.Expressions;

namespace Business.Abstract
{
    public interface IPaymentService : IEntityServiceBase<Payment>
    {
        IDataResult<List<Payment>> GetByUserId(int id);
        IDataResult<Payment> GetByCreditCardtId(int id);
        IDataResult<List<PaymentDetailDto>> GetPaymentDetails(Expression<Func<PaymentDetailDto, bool>> filter = null);
    }
}