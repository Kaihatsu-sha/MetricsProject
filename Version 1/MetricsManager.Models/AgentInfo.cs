using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsManager.Entities
{
    public class AgentInfo : BaseEntity
    {
        public string Name { get; set; }

        public Uri Address { get; set; }
        public bool CanAvailable {  get; set; }
    }
}
