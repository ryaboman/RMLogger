using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RMLogging {

    class TargetAttribute : Attribute {
        public string typeTarget { get; set; }

        public TargetAttribute(string typeTarget) {
            this.typeTarget = typeTarget;
        }
    }


    public abstract class Target {
        public Dictionary<string, string> attributes { get; set; }
        public abstract bool Write(string message);
    }
}
