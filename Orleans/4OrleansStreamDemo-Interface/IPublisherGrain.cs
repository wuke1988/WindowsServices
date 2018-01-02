﻿using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4OrleansStreamDemo
{
    public interface IPublisherGrain:IGrainWithGuidKey
    {
        Task PublishMessageAsync(string data);
    }
}
