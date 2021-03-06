using EleCho.GoCqHttpSdk.Action.Model.Params;
using EleCho.GoCqHttpSdk.Enumeration;

namespace EleCho.GoCqHttpSdk.Action
{
    public class CqGetImageAction : CqAction
    {
        public override CqActionType Type => CqActionType.GetImage;

        public string Filename { get; set; }

        public CqGetImageAction(string filename) => Filename = filename;

        internal override CqActionParamsModel GetParamsModel()
        {
            return new CqGetImageActionParamsModel(Filename);
        }
    }
}