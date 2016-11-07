using System.Collections.Generic;

namespace Workflow
{
    public class Sprint : SprintPrototype
    {
        public IEnumerable<Story> Stories { get; set; }
        public override SprintPrototype Clone()
        {
            return (SprintPrototype)MemberwiseClone();
        }
    }

}
