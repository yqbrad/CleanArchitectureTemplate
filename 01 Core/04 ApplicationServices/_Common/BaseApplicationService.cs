﻿using YQB.Contracts._Common;
using Framework.Domain.EventBus;

namespace YQB.ApplicationServices._Common
{
    public abstract class BaseApplicationService
    {
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly IServiceBus ServiceBus;

        protected BaseApplicationService(IUnitOfWork unitOfWork, IServiceBus serviceBus)
        {
            UnitOfWork = unitOfWork;
            ServiceBus = serviceBus;
        }
    }
}