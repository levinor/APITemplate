using System;
using System.Collections.Generic;
using System.Text;

namespace Levinor.Business.Domain
{
    public class Token
    {
        public Guid token { get; set; }
        public bool needsToBeUpdated { get; set; }
    }
}
