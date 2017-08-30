using System;

namespace TeddyNetCore_EngineEnum {
    [AttributeUsage(
        AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Method,
        AllowMultiple = false)]

    public sealed class DisplayAttribute : Attribute {
        #region Member Fields
        private string _name;
        private Type _resourceType;
        #endregion

        #region Properties
        public string Name {
            get { return _name; }
            set {
                if (_name != value) {
                    _name = value;
                }
            }
        }

        public Type ResourceType {
            get { return _resourceType; }
            set {
                if (_resourceType != value) {
                    _resourceType = value;
                }
            }
        }
        #endregion

        #region Methods
        public string GetName() {
            return _name;
        }
        #endregion
    }
}
