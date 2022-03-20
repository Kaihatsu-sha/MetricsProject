
using System;

namespace MetricsManager.Services.Repository
{
    public class AgenDto : BaseEntityDto
    {
        public int Id {  get; set; }
        public string Name { get; set; }

        public Uri Address { get; set; }
    }
}
