namespace EleCho.GoCqHttpSdk.Post.Model
{
    internal class CqNoticeGroupUploadPostModel : CqNoticePostModel
    {
        public override string notice_type => "group_upload";

        public long group_id { get; set; }
        public long user_id { get; set; }
        public CqGroupFileModel file { get; set; }
    }
}