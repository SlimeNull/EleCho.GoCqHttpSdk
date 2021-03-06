using EleCho.GoCqHttpSdk.Action.Result.Model.Data;

namespace EleCho.GoCqHttpSdk.Action.Result
{
    public class CqSendMsgActionResult : CqActionResult
    {
        public int MessageId { get; set; }

        internal override void ReadDataModel(CqActionResultDataModel? model)
        {
            if (model is CqSendMsgActionResultDataModel dataModel)
            {
                MessageId = dataModel.message_id;
            }
        }
    }
}