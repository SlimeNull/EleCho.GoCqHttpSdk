using EleCho.GoCqHttpSdk.DataStructure;

namespace EleCho.GoCqHttpSdk.Post
{
    internal class CqMsgSenderModel
    {
        public CqMsgSenderModel()
        { }

        public CqMsgSenderModel(CqMsgSender srcData)
        {
            user_id = srcData.UserId;
            nickname = srcData.Nickname;
            sex = srcData.Sex;
            age = srcData.Age;
        }

        public long user_id { get; set; }
        public string nickname { get; set; }
        public string sex { get; set; }
        public int age { get; set; }
    }
}