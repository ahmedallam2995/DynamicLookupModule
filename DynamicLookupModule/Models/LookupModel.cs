using System.Collections.Generic;

namespace DynamicLookupModule.Models
{
    public class LookupModel
    {
        public BaseLookup LookupHeader { get; set; }
        public List<BaseLookup> LookupList { get; set; }
    }
}
