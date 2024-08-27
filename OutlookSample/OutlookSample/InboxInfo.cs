using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutlookSample
{
    public class InboxInfo
    {
        public string? Name { get; set; }

        public string? ProfileName { get; set; }

        public string? Subject { get; set; }

        public string? Description { get; set; }

        public ImageSource? Image { get; set; }

        public bool? IsAttached { get; set; }

        public bool IsImportant { get; set; }

    }
}
