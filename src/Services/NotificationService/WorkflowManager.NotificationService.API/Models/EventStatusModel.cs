﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkflowManager.NotificationService.API.Models
{
    public class EventStatusModel
    {
        public Guid CorrelationId { get; set; }
        public string Result { get; set; }

    }
}
