using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Abstraction
{
    public interface IModelStateErrorMessageStore
    {
        string GetErrorMessage(string key);
    }
}