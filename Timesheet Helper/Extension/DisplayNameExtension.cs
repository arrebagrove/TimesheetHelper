using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace TimesheetHelper.Extension
{
    class DisplayNameExtension: MarkupExtension
    {
        public Type Type { get; set; }
        public string PropertyName { get; set; }
        public DisplayNameExtension() { }
        public DisplayNameExtension(string propertyName)
        {
            PropertyName = propertyName;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            // TODO(batuhan): Fail-check
            var property = Type.GetProperty(PropertyName);
            var attrs = property.GetCustomAttributes(typeof(DisplayNameAttribute), false);
            return (attrs[0] as DisplayNameAttribute).DisplayName;
        }
    }
}
