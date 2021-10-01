using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Linko.Helper
{
    public interface ILoggerRepository
    {
        public void Write(Exception exception, string message);
        public Task WriteAsync(Exception exception, string message);
    }
}
