﻿using Framework.Domain.Requests;
using YQB.DomainModels.People.Enums;

namespace YQB.Contracts.People.Requests
{
    public class AddPerson : IRequest<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
    }
}