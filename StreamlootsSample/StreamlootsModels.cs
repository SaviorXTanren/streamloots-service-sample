using System.Collections.Generic;
using System.Linq;

namespace StreamlootsSample
{
    public class StreamlootsPurchaseModel
    {
        public string type { get; set; }
        public StreamlootsPurchaseDataModel data { get; set; }
    }

    public class StreamlootsPurchaseDataModel
    {
        public List<StreamlootsDataFieldModel> fields { get; set; }

        // This is the person receiving the action (if gifted)
        public string Giftee
        {
            get
            {
                var field = this.fields.FirstOrDefault(f => f.name.Equals("giftee"));
                return (field != null) ? field.value : string.Empty;
            }
        }

        public int Quantity
        {
            get
            {
                var field = this.fields.FirstOrDefault(f => f.name.Equals("quantity"));
                return (field != null) ? int.Parse(field.value) : 0;
            }
        }

        // This is the person doing the action (purchase or gifter)
        public string Username
        {
            get
            {
                var field = this.fields.FirstOrDefault(f => f.name.Equals("username"));
                return (field != null) ? field.value : string.Empty;
            }
        }
    }

    public class StreamlootsCardModel
    {
        public string type { get; set; }
        public string imageUrl { get; set; }
        public string videoUrl { get; set; }
        public string soundUrl { get; set; }
        public StreamlootsCardDataModel data { get; set; }
    }

    public class StreamlootsCardDataModel
    {
        public string cardName { get; set; }
        public List<StreamlootsDataFieldModel> fields { get; set; }

        public string Message
        {
            get
            {
                StreamlootsDataFieldModel field = this.fields.FirstOrDefault(f => f.name.Equals("message"));
                return (field != null) ? field.value : string.Empty;
            }
        }

        public string LongMessage
        {
            get
            {
                StreamlootsDataFieldModel field = this.fields.FirstOrDefault(f => f.name.Equals("longMessage"));
                return (field != null) ? field.value : string.Empty;
            }
        }

        public string Username
        {
            get
            {
                StreamlootsDataFieldModel field = this.fields.FirstOrDefault(f => f.name.Equals("username"));
                return (field != null) ? field.value : string.Empty;
            }
        }
    }

    public class StreamlootsDataFieldModel
    {
        public string name { get; set; }
        public string value { get; set; }
    }
}
