using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Simulator;

public static class Simulator
{
    public delegate void ThreadStart();
    public delegate void ParameterizedThreadStart(object obj);
}
