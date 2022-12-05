﻿using CQRS.Core.Events;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Producers;
public interface IEventProducer {
    Task ProducerAsync<T>(string topic, T @Event) where T : BaseEvent;
}
