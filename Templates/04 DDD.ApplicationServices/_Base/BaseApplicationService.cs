﻿using DDD.Contracts._Base;
using Framework.Domain.EventBus;

namespace $safeprojectname$._Base
{
    public abstract class BaseApplicationService
    {
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly IEventBus EventBus;

        protected BaseApplicationService(IUnitOfWork unitOfWork, IEventBus eventBus)
        {
            UnitOfWork = unitOfWork;
            EventBus = eventBus;
        }
    }
}