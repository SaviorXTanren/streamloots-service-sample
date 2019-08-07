using System.Collections.Generic;
using System.Linq;

namespace StreamlootsSample
{
    public class StreamlootsCardModel
    {
        public string type { get; set; }
        public string imageUrl { get; set; }
        public StreamlootsCardDataModel data { get; set; }
    }

    public class StreamlootsCardDataModel
    {
        public string cardName { get; set; }
        public List<StreamlotsCardDataFieldModel> fields { get; set; }

        public string Message
        {
            get
            {
                StreamlotsCardDataFieldModel field = this.fields.FirstOrDefault(f => f.name.Equals("message"));
                return (field != null) ? field.value : string.Empty;
            }
        }

        public string Username
        {
            get
            {
                StreamlotsCardDataFieldModel field = this.fields.FirstOrDefault(f => f.name.Equals("username"));
                return (field != null) ? field.value : string.Empty;
            }
        }
    }

    public class StreamlotsCardDataFieldModel
    {
        public string name { get; set; }
        public string value { get; set; }
    }
}
