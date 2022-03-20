using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsManager.Entities
{
    public abstract class BaseMetric : BaseEntity
    {
        public int Value { get; set; }

        public long Time { get; set; }

        //public AgentInfo Agent {  get; set; }
        //public long AgentId { get; set; }
    }
}
