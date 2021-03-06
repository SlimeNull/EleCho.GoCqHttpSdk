using EleCho.GoCqHttpSdk.Enumeration;
using EleCho.GoCqHttpSdk.Post.Model;

namespace EleCho.GoCqHttpSdk.Post
{
    public class CqHonorChangedPostContext : CqPostContext
    {
        public override CqPostType EventType => throw new System.NotImplementedException();

        public CqHonorType HonorType { get; set; }
        public long GroupId { get; set; }
        public long UserId { get; set; }

        internal CqHonorChangedPostContext()
        { }

        internal override void ReadModel(CqPostModel model)
        {
            base.ReadModel(model);

            if (model is not CqNoticeHonorPostModel msgModel)
                return;

            HonorType = CqEnum.GetHonorType(msgModel.honor_type);
            GroupId = msgModel.group_id;
            UserId = msgModel.user_id;
        }
    }
}