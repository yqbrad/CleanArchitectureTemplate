﻿using RawRabbit.Configuration;

namespace YQB.Infra.Service.RabbitMq
{
    //https://rawrabbit.readthedocs.io/en/master/configuration.html
    public class RabbitMqOptions : RawRabbitConfiguration
    {
        public bool IsEnable { get; set; }
    }
}