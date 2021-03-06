using EleCho.GoCqHttpSdk.Message.DataModel;
using EleCho.GoCqHttpSdk.Util;
using System;

namespace EleCho.GoCqHttpSdk.Message
{
    public class CqTextMsg : CqMsg
    {
        public override string Type => Consts.MsgType.Text;

        public string Text { get; set; }

        internal CqTextMsg()
        { }

        public CqTextMsg(string text)
        {
            Text = text;
        }

        internal override CqMsgDataModel GetDataModel() => new CqTextMsgDataModel(Text);

        internal override void ReadDataModel(CqMsgDataModel model)
        {
            var m = model as CqTextMsgDataModel;
            if (m == null)
                throw new ArgumentException();

            Text = m.text;
        }
    }
}