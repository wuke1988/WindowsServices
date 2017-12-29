﻿using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2OrleansGrainStateDemo_Interface
{
    public interface IPersonGrain: IGrainWithIntegerKey
    {
        Task SayHelloAsync();
    }
}
