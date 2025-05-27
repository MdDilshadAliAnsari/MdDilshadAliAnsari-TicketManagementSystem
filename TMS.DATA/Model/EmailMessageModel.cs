using Newtonsoft.Json;

namespace TMS.DATA.Model
{
    public class RootJson
    {
        [JsonProperty("@odata.context")]
        public string ODataContext { get; set; }
        public List<EmailMessageData> Value { get; set; }
    }

    public class EmailMessageData
    {
        [JsonProperty("@odata.etag")]
        public string ODataEtag { get; set; }
        public string Id { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime LastModifiedDateTime { get; set; }
        public string ChangeKey { get; set; }
        public List<string> Categories { get; set; }
        public DateTime ReceivedDateTime { get; set; }
        public DateTime SentDateTime { get; set; }
        public bool HasAttachments { get; set; }
        public string InternetMessageId { get; set; }
        public string Subject { get; set; }
        public string BodyPreview { get; set; }
        public string Importance { get; set; }
        public string ParentFolderId { get; set; }
        public string ConversationId { get; set; }
        public string ConversationIndex { get; set; }
        public bool IsDeliveryReceiptRequested { get; set; }
        public bool IsReadReceiptRequested { get; set; }
        public bool IsRead { get; set; }
        public bool IsDraft { get; set; }
        public string WebLink { get; set; }
        public string InferenceClassification { get; set; }
        public Body Body { get; set; }
        public EmailAddressWrapper Sender { get; set; }
        public EmailAddressWrapper From { get; set; }
        public List<EmailAddressWrapper> ToRecipients { get; set; }
        public List<EmailAddressWrapper> CcRecipients { get; set; }
        public List<object> BccRecipients { get; set; }
        public List<object> ReplyTo { get; set; }
        public Flag Flag { get; set; }
    }

    public class Body
    {
        public string ContentType { get; set; }
        public string Content { get; set; }
    }

    public class EmailAddressWrapper
    {
        public EmailAddress EmailAddress { get; set; }
    }

    public class EmailAddress
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }

    public class Flag
    {
        public string FlagStatus { get; set; }
    }
}
